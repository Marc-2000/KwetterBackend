
namespace TweetService.BLL.Models
{
    public class TaggedUser
    {
        public Guid UserID { get; set; }

        public Guid TweetID { get; set; }

        public Tweet Tweet { get; set; }
    }
}
