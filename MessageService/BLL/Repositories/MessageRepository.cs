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

        public async Task<ServiceResponse<Message>> SendMessage(MessageDTO messageDTO)
        {
            ServiceResponse<Message> response = new();

            try
            {
                Message message = new()
                {
                    Text = messageDTO.Message,
                    UserID = messageDTO.UserID,
                    ChatID = messageDTO.ChatID,
                    Time = DateTime.Now
                };

                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return response.BadResponse("Something went wrong. Please reload the page and try again.");
            }
        }
    }
}
