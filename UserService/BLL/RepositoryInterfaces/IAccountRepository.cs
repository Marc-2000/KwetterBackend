using Google.Apis.Auth;
using System;
using System.Threading.Tasks;
using UserService.BLL.DTOs;
using UserService.Entities;
using UserService.DAL.Context;

namespace UserService.BLL.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        Task<ServiceResponse> Register(UserRegisterDTO user);
        Task<ServiceResponse> Login(UserLoginDTO user);
        Task<User> GetByID(Guid id);
    }
}
