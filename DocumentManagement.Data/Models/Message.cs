using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentManagement.Data.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("user_id")]
        public required User User { get; set; }
        [ForeignKey("document_id")]
        public required Document Document { get; set; }

        [StringLength(100)]
        public required string Content { get; set; }

        public bool IsPublic { get; set; } = false;

        public DateTime created_at { get; set; } = DateTime.UtcNow;
    }
}
