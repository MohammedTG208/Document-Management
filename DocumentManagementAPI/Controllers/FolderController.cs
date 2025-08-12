using System.Security.Claims;
using System.Threading.Tasks;
using __Document_Management_API.Controllers;
using __Document_Management_API.IService;
using DocumentManagementAPI.Dtos.FolderDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementAPI.Controllers
{
    public class FolderController(IFolder folderService) : BaseController
    {
        [HttpPost("add")]
        [Authorize]
        public async Task<ActionResult> AddNewFolder([FromBody] CreateFolderDto folder)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out int userid);
            await folderService.AddNewFolder(folder, userid);
            return Ok("add new folder successfully");
        }

        [HttpGet("by-userId")]
        [Authorize]
        public async Task<ActionResult> GetFoldersByUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out int userid);
            return Ok(await folderService.GetAllFoldersByUserId(userid));
        }

        [HttpDelete("delete/{folderId}")]
        [Authorize]
        public async Task<ActionResult> DeleteFolder(int folderId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out int userid);
            await folderService.DeleteFolder(folderId, userid);
            return Ok("Delete folder successfully");
        }

        [HttpPatch("update/{folderId}")]
        [Authorize]
        public async Task<string> ChangeFolderName(int folderId, [FromBody] ChangeFolderNameDto newFolderName)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(user, out int userid);

            await folderService.ChangeFolderName(folderId, userid, newFolderName);
            return "update folder name successfully";
        }

        [HttpGet("search/{folderName}")]

        public async Task<(IEnumerable<Object>, DocumentManagementAPI.Paging.Page)> SearchByName(string folderName, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await folderService.SearchByFolderName(folderName, pageNumber, pageSize);
        }

        [HttpGet("all")]
        public async Task<ICollection<Object>> GetAllFolders()
        {
            return await folderService.GetAllFolders();
        }
    }
}
