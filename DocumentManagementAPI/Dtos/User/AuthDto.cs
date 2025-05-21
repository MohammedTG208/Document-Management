using System.ComponentModel.DataAnnotations;

namespace DocumentManagementAPI.Dtos.User
{
    public class AuthDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
