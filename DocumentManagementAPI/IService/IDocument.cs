using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos.DocumentDto;
using DocumentManagementAPI.Dtos.MessageDto;
using DocumentManagementAPI.Dtos.UserDocumentDto;
using DocumentManagementAPI.Paging;
using Microsoft.AspNetCore.Mvc;

namespace __Document_Management_API.IService
{
    public interface IDocument
    {
        Task UploadFile(IFormFile formFile,int userId, int folderId, string docName);

        Task<DocumentDownloadDto> DownloadFile(int docId);

        Task deleteDoucment(int docId,int userId);

        Task<ICollection<MessagesDto>> GetMessages(int docId);

        Task<ICollection<UserDocumentDto>> GetDocumentsByUserId(int userId,int folderId);

        Task<(ICollection<DocumentDto>, Page)> GetDocuments(string? docName, string? querySearch, int pageSize, int PageNumber);
    }
}
