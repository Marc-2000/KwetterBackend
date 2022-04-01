namespace MessageService.BLL.Models
{
    public class Users
    {
        public Guid UserID { get; set; }

        public Guid ChatID { get; set; }

        public Chat Chat { get; set; }
    }
}
