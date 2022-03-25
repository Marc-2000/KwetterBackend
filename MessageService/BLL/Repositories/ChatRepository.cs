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

        public async Task<List<Chat>> GetAllByUserID(Guid UserID)
        {
            User user = await _context.Users.Include(u => u.ChatUsers).ThenInclude(cu => cu.Chat).ThenInclude(ms => ms.Messages).FirstOrDefaultAsync(u => u.ID == UserID);
            List<Chat> chats = new List<Chat>();

            for (int i = 0; i < user.ChatUsers.Count; i++)
            {
                chats.Add(user.ChatUsers[i].Chat);
            }

            return chats;
        }
    }
}
