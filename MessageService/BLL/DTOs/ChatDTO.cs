using System.ComponentModel.DataAnnotations;

namespace MessageService.BLL.DTOs
{
    public class ChatDTO
    {
        [Required]
        public Guid CreatorID { get; set; }
        [Required]
        public Guid ParticipantID { get; set; }
    }
}
