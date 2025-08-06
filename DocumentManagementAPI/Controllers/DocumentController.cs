using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using __Document_Management_API.IService;
using Asp.Versioning;
using DocumentManagementAPI.Dtos.DocumentDto;
using DocumentManagementAPI.Dtos.UserDocumentDto;
using DocumentManagementAPI.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;

namespace __Document_Management_API.Controllers
{
    public class DocumentController(IDocument documentService) : BaseController
    {
        /// <summary>
        /// Uploads a document file to a specific folder for the authenticated customer.
        /// </summary>
        /// <param name="formFile">The file to be uploaded. Must not be empty or exceed 10MB.</param>
        /// <param name="folderId">The ID of the folder to which the document will be uploaded.</param>
        /// <param name="docName"> The Document name will display as name to the users how want download this document.</param>
        /// <returns>Returns 200 OK if the document was successfully uploaded.</returns>
        [Authorize(Roles = "Customer")]
        [HttpPost("upload/{folderId}/{docName}")]
        public async Task<ActionResult> uploadDoc(IFormFile formFile, int folderId,string docName)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out int userid);
            await documentService.UploadFile(formFile, userid, folderId,docName);
            return Ok("Upload Doc successfully");
        }

        [HttpGet("download/{docId}")]

        public async Task<ActionResult> DownloadDoc(int docId)
        {
            var file = await documentService.DownloadFile(docId);
            return File(file.DocData, file.ContentType, file.DocName);
        }

        [HttpDelete("delete/{docId}")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> DeleteDoc(int docId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out int userid);
            await documentService.deleteDoucment(docId, userid);
            return Ok("Delete Doc successfully");
        }

        [HttpGet("getMessages/{docId}")]
        [Authorize]
        public async Task<ActionResult> GetMessages(int docId)
        {
            return Ok(await documentService.GetMessages(docId));
        }

        [HttpGet("get/all/doc")]
        public async Task<(ICollection<DocumentDto>, DocumentManagementAPI.Paging.Page)> GetDocuments([FromQuery] string? docName, [FromQuery] string? querySearch, int pageSize = 10, int pageNumber = 1)
        {
            var result = await documentService.GetDocuments(docName, querySearch, pageSize, pageNumber);
            Response.Headers.Append("X-Pagination", System.Text.Json.JsonSerializer.Serialize(result.Item2));
            return result;
        }

        [HttpGet("{folderId}")]
        [Authorize]
        public async Task<ICollection<UserDocumentDto>> GetDocumentByUserId(int folderId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out int userid);
            return await documentService.GetDocumentsByUserId(userid,folderId);
        }
    }
}
