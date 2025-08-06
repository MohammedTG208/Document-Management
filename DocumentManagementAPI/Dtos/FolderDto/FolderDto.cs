

namespace DocumentManagementAPI.Dtos.FolderDto
{
    public class FolderDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }

        public bool IsPublic { get; set; }
    }
}
