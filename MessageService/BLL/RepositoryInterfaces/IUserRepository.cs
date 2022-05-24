using Shared.Models.Models;

namespace MessageService.BLL.Repositories
{
    public interface IUserRepository
    {
        Task RegisterUser(SharedUser user);
        Task DeleteUser(SharedUser user);
    }
}
