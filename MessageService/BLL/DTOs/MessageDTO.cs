using System.ComponentModel.DataAnnotations;

namespace MessageService.BLL.DTOs
{
    public class MessageDTO
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public Guid ChatID { get; set; }

        [Required]
        public Guid UserID { get; set; }
    }
}
