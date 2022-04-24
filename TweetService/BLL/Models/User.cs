using System.ComponentModel.DataAnnotations;

namespace TweetService.BLL.Models
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }

        public string Username { get; set; }

        //profilepicture! 
    }
}
