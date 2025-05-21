

using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos.User;


namespace DocumentManagementAPI.Dtos.DocumentDto
{
    public class DocumentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UserDto user { get; set; }

        public DocumentManagementAPI.Dtos.FolderDto.FolderDto Folder { get; set; }
    }
}
