using System.ComponentModel.DataAnnotations;

namespace TweetService.BLL.Models
{
    public class Tweet
    {
        [Key]
        public Guid ID { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public Guid UserID { get; set; }

        public int Likes { get; set; }

        public int Retweets { get; set; }

        //public List<UserTweetTags> UserTweetTags { get; set; }
    }
}
