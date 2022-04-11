using MessageService.BLL.DTOs;
using MessageService.BLL.Models;
using MessageService.DAL.Context;

namespace MessageService.BLL.RepositoryInterfaces
{
    public interface IMessageRepository
    {
        public Task<ServiceResponse<Message>> SendMessage(MessageDTO messageDTO);
    }
}
