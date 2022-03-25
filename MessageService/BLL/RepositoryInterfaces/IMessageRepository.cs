using MessageService.BLL.DTOs;
using MessageService.DAL.Context;

namespace MessageService.BLL.RepositoryInterfaces
{
    public interface IMessageRepository
    {
        public Task<ServiceResponse<Guid>> SendMessage(MessageDTO messageDTO);
    }
}
