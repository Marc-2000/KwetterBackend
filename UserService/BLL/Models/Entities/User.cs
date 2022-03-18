using System.Text.Json.Serialization;

namespace UserService.BLL.Models.Entities
{
    public class User
    {
        public Guid ID { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }

        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }
    }
}
