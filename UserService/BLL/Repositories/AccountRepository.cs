﻿using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserService.BLL.DTOs;
using UserService.BLL.Models;
using UserService.BLL.RepositoryInterfaces;
using UserService.DAL.Context;

namespace UserService.BLL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AccountRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<User>> Login(UserDTO user)
        {
            // Create new empty response
            ServiceResponse<User> response = new ServiceResponse<User>();

            //Retrieve user with e-mail from request
            User retrievedUser = await _context.Users.Include(pr => pr.UserRoles).ThenInclude(r => r.Role).FirstOrDefaultAsync(x => x.Email.ToLower().Equals(user.Email.ToLower()));

            if (retrievedUser == null || !VerifyPasswordHash(user.Password, retrievedUser.PasswordHash, retrievedUser.PasswordSalt))
            {
                //User didn't give the right credentials, so return error
                response.Success = false;
                response.Message = "Credentials are not valid!";
            }
            else
            {
                //User exists and password is correct, so set token
                response.Token = CreateToken(retrievedUser);
                response.Message = "Successfully logged in!";
            }
            //return the filled response with token or error message
            return response;
        }

        public async Task<ServiceResponse<User>> Register(UserDTO user)
        {
            //Create new empty response
            ServiceResponse<User> response = new ServiceResponse<User>();

            if (await EmailExists(user.Email))
            {
                //Useraccount with Email already exists, so return error
                response.Success = false;
                response.Message = "Email already in use.";
                return response;
            }


            //Create password hash with salt
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User NewUser = new User
            {
                Email = user.Email,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            //Add default role to new user
            Role defaultRole = _context.Roles.FirstOrDefault(x => x.Name.Equals("User"));

            //Set join-table for entity framework
            UserRole newPersonRole = new UserRole
            {
                UserID = NewUser.ID,
                User = NewUser,
                RoleID = defaultRole.ID,
                Role = defaultRole
            };

            //Add user and user-role to database
            await _context.Users.AddAsync(NewUser);
            await _context.SaveChangesAsync();
            await _context.UserRoles.AddAsync(newPersonRole);
            await _context.SaveChangesAsync();

            //set return data
            response.Message = "User registered succesfully.";
            response.Token = CreateToken(NewUser);

            //return userID 
            return response;
        }

        public async Task<bool> EmailExists(string email)
        {
            //Check if user with email already exists
            if (await _context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower())) return true;
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //Create password hash with salt
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //Verify password hash with salt
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateToken(User user)
        {
            //Set claims for token
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
            };
            if (user.Email != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, user.Email));
            }
            foreach (UserRole role in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
            }

            //Add security key to token
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("Auth:Token").Value)
            );

            //Set token credentials
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Fill token descriptor
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            //Create new tokenHandler and create token including tokenDescriptor
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            //Return JWT 
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> Get(Guid id)
        {
            User retrievedUser = await _context.Users.FirstOrDefaultAsync(x => x.ID == id);
            if (retrievedUser != null)
            {
                User user = new User()
                {
                    ID = retrievedUser.ID,
                    Email = retrievedUser.Email
                };
            }
            return retrievedUser;
        }
    }
}
