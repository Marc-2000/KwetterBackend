using TweetService.BLL.Models;
using TweetService.DAL.Context;
using Shared.Models.Models;

namespace TweetService.BLL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task RegisterUser(SharedUser user)
        {
            if (user == null)
            {
                return;
            }

            User newUser = new()
            {
                Id = user.Id,
                Username = user.Username
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
    }
}
