using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace DocumentManagement.Data.Models    
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="Max length 50 char only")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, digit, and special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        public  string Salt { get; set; }

        public string UserRole { get; set; } = "Customer";

        public DateTime created_at { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public ICollection<Folder> Folders { get; set; }=new List<Folder>();
        [JsonIgnore]
        public ICollection<Document> document { get; set; }=new HashSet<Document>();

        public ICollection<Message> Messages { get; set; }=new List<Message>();

        public Profile profile { get; set; }
    }
}
