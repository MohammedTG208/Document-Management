using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DocumentManagement.Data.Models;
using __Document_Management_API.IService;
using AutoMapper;
using DocumentManagementAPI.Dtos.DocumentDto;
using DocumentManagementAPI.Dtos.MessageDto;
using DocumentManagementAPI.ExceptionHandling;
using DocumentManagementAPI.Paging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocumentManagementAPI.Repo;
using DocumentManagementAPI.Dtos.User;

namespace DocumentManagementAPI.Service
{
    public class DocumentService(FolderRepo folderRepo,DocumentRepo documentRepo,IUserRepo userRepo,ILogger<DocumentService> logger,IMapper mapper) : IDocument
    {
        public async Task deleteDoucment(int docId, int userId)
        {
            var doc = await documentRepo.GetDocumentAndUserAsync(userId, docId);
            if (doc == null)
            {
                throw new Exception("not found");
            }
            else
            {
                if (doc.user.Id == userId)
                {
                    logger.LogInformation($"Delete Document {doc.Id} by {doc.user.UserName}");
                    await documentRepo.DeleteDucoment(doc);
                }
                else
                {
                    throw new UnauthorizedException("You can not delete this document");
                }
            }
        }

        public async Task<DocumentDownloadDto> DownloadFile(int docId)
        {
            var doc = await documentRepo.GetDocumnetById(docId);
            if(doc == null)
            {
                throw new NotFoundException("not found");
            }
            var docDto = new DocumentDownloadDto
            {
                DocName = doc.Name,
                ContentType = doc.ContentType,
                DocData = doc.File
            };
            logger.LogInformation($"Download Document successful");
            return docDto;
        }

        public async Task<(ICollection<DocumentDto>, Page)> GetDocuments(string? docName, string? querySearch, int pageSize, int PageNumber)
        {

            var query = await documentRepo.GetFilteredDocumentsAsync(docName, querySearch);

            var totalItemCount = await query.CountAsync();
            Page page = new Page(pageSize, PageNumber, totalItemCount);

            var document = await query.OrderBy(d=>d.Name)
                .Skip((page.PageSize * (page.PageNumber - 1)))
                .Take(page.PageSize)
                .ToListAsync();

            page.TotalItemCount = totalItemCount;

            var doc = mapper.Map<ICollection<DocumentDto>>(document);
            //var doc = document.Select( d => new DocumentDto
            //{
            //    Name = d.Name,
            //    Id = d.Id,
            //    user = new Dtos.User.UserDto { UserName = d.Name },
            //    Folder = new Dtos.FolderDto.FolderDto
            //    {
            //        Id = d.Folder.Id,
            //        Name= d.Folder.Name 
            //    },
            //}).ToList();

            return (doc, page);
        }

        public async Task<ICollection<MessagesDto>> GetMessages(int docId)
        {
            var docMessages= await documentRepo.GetDocumentsMessagesById(docId);

            if( docMessages == null)
            {
                logger.LogWarning("Messages not fund with this Document ID: " + docId);
                throw new NotFoundException("This document not found");
            }

            logger.LogInformation("Messages found");
            //return mapper.Map<ICollection<MessagesDto>>(docMessages);
            var messageDto = docMessages.Select(d => new MessagesDto()
            {
                Id = d.Id,
                Content = d.Content,
                Folder = new Dtos.FolderDto.FolderDto()
                {
                    Id = d.Document.Folder.Id,
                    Name = d.Document.Folder.Name
                },
                User = new UserDto()
                {
                    Id = d.User.Id,
                    UserName = d.User.UserName,
                    create_at = d.User.created_at,
                },
                Document = new DocumentDto()
                {
                    Id = d.Document.Id,
                    Name = d.Document.Name,
                    Folder = new Dtos.FolderDto.FolderDto()
                    {
                        Id = d.Document.Folder.Id,
                        Name = d.Document.Folder.Name
                    },
                    user=new UserDto()
                    {
                        Id = d.User.Id,
                        UserName=d.User.UserName,
                    }
                }
            }
            ).ToList();
            return messageDto; 
        }



        public async Task UploadFile(IFormFile formFile, int userId, int folderId)
        {
            const long MaxFileSize = 2L * 1024 * 1024 * 1024; // 2 GB

            // 1. Get user and folders (required for access check)
            var user = await userRepo.GetUserByIdAndInclodFolderAsync(userId);
            if (user == null)
            {
                logger.LogError("User not found by this ID: " + userId);
                throw new NotFoundException("User not in the system");
            }

            // 2. Get the target folder
            var folder = await folderRepo.GetFolderById(folderId);
            if (folder == null)
            {
                logger.LogError("Folder not found by this ID: " + folderId);
                throw new NotFoundException("Folder not in the system");
            }

            // 3. Check if the user owns this folder (access check)
            var hasAccess = user.Folders.Any(f => f.Id == folderId);
            if (!hasAccess)
            {
                logger.LogWarning($"User {userId} tried to access folder {folderId} they don't own.");
                throw new UnauthorizedException("You don't have access to this folder");
            }

            // 4. Validate the uploaded file
            if (formFile == null || formFile.Length == 0)
            {
                logger.LogWarning("Document not uploaded");
                throw new BadRequestException("No document uploaded");
            }

            if (formFile.Length > MaxFileSize)
            {
                logger.LogWarning("Document is too large");
                throw new BadRequestException("The file must be less than 2GB");
            }

            // 5. Convert file to byte[]
            using var fileBytes = new MemoryStream();
            await formFile.CopyToAsync(fileBytes);

            var doc = new Document
            {
                ContentType = formFile.ContentType,
                File = fileBytes.ToArray(),
                Name = Guid.NewGuid().ToString(),
                user = user,
                Folder = folder
            };

            // 6. Save the document
            await documentRepo.SaveNewDocument(doc);
            logger.LogInformation($"Document uploaded successfully for {user.UserName}");
        }

    }
}
