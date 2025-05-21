using System.ComponentModel.DataAnnotations;

namespace DocumentManagementAPI.Dtos.MessageDto
{
    public class CreateMessageDto
    {
        [StringLength(100)]
        public required string Content { get; set; }

        public bool IsPublic { get; set; } = false;
    }
}
