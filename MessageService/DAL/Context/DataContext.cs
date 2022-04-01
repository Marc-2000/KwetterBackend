using Microsoft.EntityFrameworkCore;
using MessageService.BLL.Models;

namespace MessageService.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                   .HasKey(cu => new { cu.UserID, cu.ChatID });

            modelBuilder.Entity<Users>()
                .HasOne(ch => ch.Chat)
                .WithMany(cu => cu.Users)
                .HasForeignKey(ch => ch.ChatID);

            modelBuilder.Entity<Users>()
                .HasOne(p => p.User)
                .WithMany(cu => cu.Users)
                .HasForeignKey(ch => ch.UserID);

            modelBuilder.Entity<Chat>()
                .HasMany(msg => msg.Messages)
                .WithOne(ch => ch.Chat);
        }
    }
}
