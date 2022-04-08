using System.ComponentModel.DataAnnotations;

namespace MessageService.BLL.Models
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }

        public string Username { get; set; }

        public List<ChatUser> ChatUsers { get; set; }
    }
}
