using System.ComponentModel.DataAnnotations;

namespace DocumentManagementAPI.Dtos
{
    public class UpdateProfileDto
    {
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name must contain only letters.")]
        public string FirstName { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name must contain only letters.")]

        public string LastName { get; set; }

        [EmailAddress]

        public string Email { get; set; }

        [RegularExpression(@"^05\d{8}$", ErrorMessage = "Phone number must start with 05 and contain exactly 10 digits.")]
        public string PhoneNumber { get; set; }

    }
}