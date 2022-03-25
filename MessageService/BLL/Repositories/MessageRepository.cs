using MessageService.BLL.DTOs;
using MessageService.BLL.Models;
using MessageService.BLL.RepositoryInterfaces;
using MessageService.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace MessageService.BLL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Guid>> SendMessage(MessageDTO messageDTO)
        {
            ServiceResponse<Guid> response = new ServiceResponse<Guid>();

            Message message = new()
            {
                Text = messageDTO.message,
                UserID = messageDTO.UserID,
                ChatID = messageDTO.ChatID,
                Time = messageDTO.DateTime
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return response;
        }
    }
}
