using Microsoft.EntityFrameworkCore;
using TweetService.BLL.Models;

namespace TweetService.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<TaggedUser> TaggedUsers { get; set; }
        public DbSet<TweetHashtag> TweetHashtags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaggedUser>()
                .HasKey(tu => new {tu.UserID, tu.TweetID});
            modelBuilder.Entity<TaggedUser>()
                .HasOne(t => t.Tweet)
                .WithMany(tu => tu.TaggedUsers)
                .HasForeignKey(t => t.TweetID);

            modelBuilder.Entity<TweetHashtag>().HasKey(ur => new { ur.TweetId, ur.HashtagId });
            modelBuilder.Entity<TweetHashtag>()
                .HasOne(th => th.Tweet)
                .WithMany(t => t.TweetHashtags)
                .HasForeignKey(th => th.TweetId);
            modelBuilder.Entity<TweetHashtag>()
                .HasOne(th => th.Hashtag)
                .WithMany(h => h.TweetHashtags)
                .HasForeignKey(th => th.HashtagId);
        }
    }
}
