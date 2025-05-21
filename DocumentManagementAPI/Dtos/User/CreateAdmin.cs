namespace DocumentManagementAPI.Dtos.User
{
    public class CreateAdmin
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        private string UserRole { get; set; } = "Admin";
    }
}
