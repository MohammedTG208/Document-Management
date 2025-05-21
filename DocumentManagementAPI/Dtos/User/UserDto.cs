namespace DocumentManagementAPI.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public DateTime create_at { get; set; }
    }
}
