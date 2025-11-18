using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public class MessageResponse
    {
        [Key]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Message is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Message characters quantity is between 2 to 100.")]
        public string MessageText { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string SentimentLabel { get; set; } = String.Empty;
        public float SentimentScore { get; set; }
    }
}
