using Google.Apis.Auth;
using System;
using System.Threading.Tasks;
using UserService.BLL.DTOs;
using UserService.BLL.Models;
using UserService.DAL.Context;

namespace UserService.BLL.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        Task<ServiceResponse<User>> Register(UserRegisterDTO user);
        Task<ServiceResponse<User>> Login(UserLoginDTO user);
        Task<User> GetByID(Guid id);
    }
}
