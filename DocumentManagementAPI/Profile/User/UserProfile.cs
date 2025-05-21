using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos;
using DocumentManagementAPI.Dtos.User;

namespace DocumentManagementAPI.Profile.User
{
    public class UserProfile :AutoMapper.Profile
    {
        public UserProfile() {

            CreateMap<DocumentManagement.Data.Models.User, UserDto>().ReverseMap();

            CreateMap<DocumentManagement.Data.Models.User,CreateAdmin>().ReverseMap();

            CreateMap<DocumentManagement.Data.Models.User,ProfileProcDto>().ReverseMap();
        }
    }
}
