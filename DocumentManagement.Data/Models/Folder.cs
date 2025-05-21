using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DocumentManagement.Data.Models
{
    public class Folder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("user_Id")]
        [JsonIgnore]
        public User Users { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public bool isPublic { get; set; }=true;

        public DateTime created_at { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public ICollection<Document>? Documents { get; set; } = new List<Document>();
    }
}
