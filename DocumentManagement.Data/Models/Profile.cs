using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Models
{
    public class Profile
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        public  string? FirstName { get; set; }

        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage ="Enter valid Email")]
        public string? Email { get; set; }
        [RegularExpression(@"^05\d{8}$", ErrorMessage = "Phone number must start with 05 and be followed by 8 digits.")]
        public string PhoneNumber { get; set; }

        public User user { get; set; }

        [NotMapped]
        public string FullName 
        { 
            get { return $"{FirstName} {LastName}"; } 
        }
    }
}
