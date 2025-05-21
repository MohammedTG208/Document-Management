namespace DocumentManagementAPI.Dtos
{
    public class ProfileProcDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public DateTime UserCreatedAt { get; set; }

        public int ProfileId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
