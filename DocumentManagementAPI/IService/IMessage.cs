using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos.MessageDto;

namespace __Document_Management_API.IService
{
    public interface IMessage
    {
        Task addNewMessage(CreateMessageDto message,int userId, int docId);

        Task removeMessage(int messageId, int userId);
    }
}
