using AutoMapper;
using DocumentManagementAPI.Dtos;

namespace DocumentManagementAPI.Profile.ProfileMapper
{
    public class ProfileMapper:AutoMapper.Profile
    {
        public ProfileMapper()
        {
            CreateMap<DocumentManagement.Data.Models.Profile, AddProfileDto>().ReverseMap();
            CreateMap<DocumentManagement.Data.Models.Profile, ProfileDto>().ReverseMap();
            CreateMap<DocumentManagement.Data.Models.Profile, UpdateProfileDto>().ReverseMap();
        }
    }
}
