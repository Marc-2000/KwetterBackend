using System.ComponentModel.DataAnnotations;

namespace TweetService.BLL.DTOs
{
    public class TweetDTO
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public Guid UserID { get; set; }
    }
}
