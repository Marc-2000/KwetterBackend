
using MessageService.BLL.Models;

namespace MessageService.BLL.RepositoryInterfaces
{
    public interface IChatRepository
    {
        public Task<List<Chat>> GetAllByUserID(Guid UserID);
    }
}
