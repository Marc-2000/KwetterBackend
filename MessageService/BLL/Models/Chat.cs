namespace MessageService.BLL.Models
{
    public class Chat
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public List<Message> Messages { get; set; }

        public List<Users> Users { get; set; }
    }
}
