using Shared.Models.Models;

namespace TweetService.BLL.Repositories
{
    public interface IUserRepository
    {
        Task RegisterUser(SharedUser user);
    }
}
