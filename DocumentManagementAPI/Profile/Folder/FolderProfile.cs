using DocumentManagementAPI.Dtos.FolderDto;

namespace DocumentManagementAPI.Profile.Folder
{
    public class FolderProfile:AutoMapper.Profile
    {
       public FolderProfile()
        {
            CreateMap<DocumentManagement.Data.Models.Folder, CreateFolderDto>()
                .ReverseMap(); // This allows mapping in both directions

            CreateMap<DocumentManagement.Data.Models.Folder, FolderDto>().ReverseMap();
        }
    }
}
