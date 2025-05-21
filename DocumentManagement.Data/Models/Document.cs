using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DocumentManagement.Data.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }


        public required byte[] File { get; set; }

        public DateTime created_at { get; set; } = DateTime.UtcNow;


        [ForeignKey("user_id")]
        [JsonIgnore]
        public required User user { get; set; }

        [ForeignKey("folder_id")]
        public Folder? Folder { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public required string ContentType { get; set; }

    }
}
