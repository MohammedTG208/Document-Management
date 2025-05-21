using System.Security.Claims;
using System.Threading.Tasks;
using __Document_Management_API.Controllers;
using __Document_Management_API.IService;
using DocumentManagementAPI.Dtos.MessageDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementAPI.Controllers
{
    public class MessageController(IMessage messageService) : BaseController
    {
        [HttpPost("add/{docId}")]
        [Authorize]
        public async Task<ActionResult> addNewMessage([FromBody]CreateMessageDto messageDto ,int docId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out int userid);
            await messageService.addNewMessage(messageDto , userid, docId);
            return Ok("Message add successfully");
        }

        [HttpDelete("delete/{messageId}")]
        [Authorize]
        public async Task<ActionResult> removeMessage(int messageId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out int userid);
            await messageService.removeMessage(messageId, userid);
            return Ok("Delete message successfully");
        }
    }
}
