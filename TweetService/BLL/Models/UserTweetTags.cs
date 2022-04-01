using System.ComponentModel.DataAnnotations;

namespace TweetService.BLL.Models
{
    public class UserTweetTags
    {
        public Guid UserID { get; set; }

        public Guid TweetID { get; set; }
    }
}
