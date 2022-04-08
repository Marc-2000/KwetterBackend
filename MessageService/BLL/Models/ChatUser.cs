namespace MessageService.BLL.Models
{
    public class ChatUser
    {
        public Guid UserID { get; set; }

        public User User { get; set; }

        public Guid ChatID { get; set; }

        public Chat Chat { get; set; }
    }
}
