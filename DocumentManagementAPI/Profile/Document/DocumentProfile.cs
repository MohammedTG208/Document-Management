using DocumentManagementAPI.Dtos.DocumentDto;

namespace DocumentManagementAPI.Profile.Document
{
    public class DocumentProfile: AutoMapper.Profile
    {

        public DocumentProfile()
        {
            CreateMap<DocumentManagement.Data.Models.Document,DocumentDto>().ReverseMap();
        }
    }
}
