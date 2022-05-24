using System.ComponentModel.DataAnnotations;

namespace MessageService.BLL.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; }
        //profilepicture
        public List<ChatUser> ChatUsers { get; set; }
    }
}
