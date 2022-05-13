using MessageService.BLL.DTOs;
using MessageService.BLL.Models;
using MessageService.BLL.RepositoryInterfaces;
using MessageService.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace MessageService.BLL.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly DataContext _context;

        public ChatRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Chat>> CreateChat(ChatDTO chatDTO)
        {
            ServiceResponse<Chat> response = new();
            //checkexistingchat
            try
            {
                User creator = await _context.Users.FirstOrDefaultAsync(x => x.ID == chatDTO.CreatorID);
                User participant = await _context.Users.FirstOrDefaultAsync(x => x.ID == chatDTO.ParticipantID);

                if (creator == null || participant == null) return response.BadResponse("This user does not exist!");

                Chat chat = new()
                {
                    ID = Guid.NewGuid(),
                    Name = participant.Username
                };

                ChatUser chatCreator = new()
                {
                    UserID = chatDTO.CreatorID,
                    User = creator,
                    ChatID = chat.ID,
                    Chat = chat
                };

                ChatUser chatParticipant = new()
                {
                    UserID = chatDTO.ParticipantID,
                    User = participant,
                    ChatID = chat.ID,
                    Chat = chat
                };

                await _context.Chats.AddAsync(chat);
                await _context.ChatUsers.AddAsync(chatCreator);
                await _context.ChatUsers.AddAsync(chatParticipant);
                await _context.SaveChangesAsync();

                response.Message = "Chat has been created!";
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return response.BadResponse("Something went wrong. Please reload the page and try again.");
            }
        }

        public async Task<List<Chat>> GetAllByUserID(Guid userId)
        {
            List<Chat> chats = new();

            User user = await _context.Users.Include(u => u.ChatUsers).ThenInclude(cu => cu.Chat).ThenInclude(ms => ms.Messages).FirstOrDefaultAsync(u => u.ID == userId);

            for (int i = 0; i < user.ChatUsers.Count; i++)
            {
                chats.Add(user.ChatUsers[i].Chat);
            }

            return chats;
        }

        public async Task<ServiceResponse<Chat>> DeleteChat(Guid ChatId)
        {
            ServiceResponse<Chat> response = new();

            try
            {
                Chat chat = await _context.Chats.FirstOrDefaultAsync(u => u.ID == ChatId);

                if (chat == null) return response.BadResponse("This chat does not exist!");

                _context.Chats.Remove(chat);
                await _context.SaveChangesAsync();

                response.Message = "Chat has been removed!";
                response.Success = true;
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
