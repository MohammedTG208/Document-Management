namespace DocumentManagementAPI.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public DateTime created_at { get; set; }
    }
}
