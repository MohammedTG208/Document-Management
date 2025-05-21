using System.ComponentModel.DataAnnotations;

namespace DocumentManagementAPI.Dtos.FolderDto
{
    public class CreateFolderDto
    {
        [Required]
        public string Name { get; set; }

        public bool? isPublic { get; set; }=true;
    }
}
