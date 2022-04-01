using Microsoft.EntityFrameworkCore;
using TweetService.BLL.Models;

namespace TweetService.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<UserTweetTags> UserTweetTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTweetTags>()
                .HasForeignKey(t => t.TweetID);
        }
    }
}
