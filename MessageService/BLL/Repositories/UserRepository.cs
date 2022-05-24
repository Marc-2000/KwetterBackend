using MessageService.BLL.Models;
using MessageService.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Models;

namespace MessageService.BLL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task RegisterUser(SharedUser user)
        {
            if (user == null)
            {
                return;
            }

            User newUser = new()
            {
                Id = user.Id,
                Username = user.Username
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(SharedUser user)
        {
            User retrievedUser = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(user.Id));

            if (user == null)
            {
                return;
            }

            _context.Users.Remove(retrievedUser);
            await _context.SaveChangesAsync();
        }
    }
}
