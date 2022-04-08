using Microsoft.EntityFrameworkCore;
using MessageService.BLL.Models;

namespace MessageService.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatUser>()
                .HasKey(cu => new { cu.UserID, cu.ChatID });
            modelBuilder.Entity<ChatUser>()
                .HasOne(ch => ch.Chat)
                .WithMany(cu => cu.ChatUsers)
                .HasForeignKey(ch => ch.ChatID);
            modelBuilder.Entity<ChatUser>()
                .HasOne(p => p.User)
                .WithMany(cu => cu.ChatUsers)
                .HasForeignKey(ch => ch.UserID);

            modelBuilder.Entity<Chat>()
                .HasMany(msg => msg.Messages)
                .WithOne(ch => ch.Chat);
        }
    }
}
