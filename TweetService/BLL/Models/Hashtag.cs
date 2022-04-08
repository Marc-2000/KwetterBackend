using System.ComponentModel.DataAnnotations;

namespace TweetService.BLL.Models
{
    public class Hashtag
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<TweetHashtag> TweetHashtags { get; set; }
    }
}
