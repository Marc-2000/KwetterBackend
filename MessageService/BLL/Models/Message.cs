using System.Text.Json.Serialization;

namespace MessageService.BLL.Models
{
    public class Message
    {
        public Guid ID { get; set; }

        public Guid UserID { get; set; }

        public DateTime Time { get; set; }

        public string Text { get; set; }

        [JsonIgnore]
        public Guid ChatID { get; set; }

        [JsonIgnore]
        public Chat Chat { get; set; }
    }
}
