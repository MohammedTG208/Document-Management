using DocumentManagement.Data.Models;
using __Document_Management_API.IService;
using AutoMapper;
using DocumentManagementAPI.Dtos.MessageDto;
using DocumentManagementAPI.Repo;
using DocumentManagementAPI.ExceptionHandling;

namespace DocumentManagementAPI.Service
{
    public class MessagesService(ILogger<MessagesService> logger,MessagesRepo messagesRepo,IUserRepo userRepo,DocumentRepo documentRepo,IMapper mapper) : IMessage
    {
        public async Task addNewMessage(CreateMessageDto message, int userId, int docId)
        {
            var user = await userRepo.GetUserById(userId);
            var doc = await documentRepo.GetDocumnetById(docId);
            
            if (user == null)
            {
                throw new NotFoundException("user not found by this id: " + userId);
            }
            if (doc == null)
            {
                throw new NotFoundException("Doc not found");
            }
            var message0 = mapper.Map<CreateMessageDto>(message);
            var newMessage = new Message
            {
                Content = message0.Content,
                User=user,
                Document=doc,
                IsPublic=message0.IsPublic,
            };
           await messagesRepo.SaveNewMessage(newMessage);
            logger.LogInformation("Save new message successfully");
            
        }

        public async Task removeMessage(int messageId, int userId)
        {
            var message = await messagesRepo.GetMessageById(messageId);
            if (message.User.Id == userId)
            {
               await messagesRepo.DeleteMessage(message);
            }
            else
            {
                throw new Exception("You can not delete this message");
            }
        }
    }
}
