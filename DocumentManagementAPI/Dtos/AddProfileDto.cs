using System.ComponentModel.DataAnnotations;

namespace DocumentManagementAPI.Dtos
{
    public class AddProfileDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [RegularExpression(@"^05\d{8}$", ErrorMessage = "Phone number must start with 05 and be followed by 8 digits.")]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}