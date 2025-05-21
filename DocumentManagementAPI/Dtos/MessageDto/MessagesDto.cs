using DocumentManagementAPI.Dtos.User;

namespace DocumentManagementAPI.Dtos.MessageDto
{
    public class MessagesDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public UserDto User { get; set; }

        public DocumentManagementAPI.Dtos.DocumentDto.DocumentDto Document { get; set; }

        public DocumentManagementAPI.Dtos.FolderDto.FolderDto Folder { get; set; }
    }
}
