using AutoMapper;
using DocumentManagementAPI.Dtos.MessageDto;

namespace DocumentManagementAPI.Profile.Message
{
    public class MessageProfile: AutoMapper.Profile
    {

        public MessageProfile()
        {
            CreateMap<DocumentManagement.Data.Models.Message,CreateMessageDto>().ReverseMap();

            CreateMap<DocumentManagement.Data.Models.Message,MessagesDto>().ReverseMap();
            CreateMap<MessagesDto,CreateMessageDto>().ReverseMap();
        }
    }
}
