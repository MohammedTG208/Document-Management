namespace DocumentManagementAPI.Dtos.User
{
    public class NewUserDto
    {
        public required string UserName { get; set; }

        public required string passWord { get; set; }
    }
}
