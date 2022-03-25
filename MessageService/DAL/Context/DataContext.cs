using Microsoft.EntityFrameworkCore;
using MessageService.BLL.Models;

namespace MessageService.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserChat> UserChats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserChat>()
                   .HasKey(cu => new { cu.UserID, cu.ChatID });
            modelBuilder.Entity<UserChat>()
                .HasOne(ch => ch.Chat)
                .WithMany(cu => cu.UserChats)
                .HasForeignKey(ch => ch.ChatID);
            modelBuilder.Entity<UserChat>()
                .HasOne(p => p.User)
                .WithMany(cu => cu.UserChats)
                .HasForeignKey(ch => ch.UserID);

            modelBuilder.Entity<Chat>()
                .HasMany(msg => msg.Messages)
                .WithOne(ch => ch.Chat);
        }
    }
}
