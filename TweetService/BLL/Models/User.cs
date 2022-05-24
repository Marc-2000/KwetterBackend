using System.ComponentModel.DataAnnotations;

namespace TweetService.BLL.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; }

        //profilepicture! 
    }
}
