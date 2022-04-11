
using MessageService.BLL.DTOs;
using MessageService.BLL.Models;
using MessageService.DAL.Context;

namespace MessageService.BLL.RepositoryInterfaces
{
    public interface IChatRepository
    {
        public Task<ServiceResponse<Chat>> CreateChat(ChatDTO chatDTO);
        public Task<List<Chat>> GetAllByUserID(Guid UserID);
    }
}
