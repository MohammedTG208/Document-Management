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
using DocumentManagementAPI.Dtos.UserDocumentDto;

namespace DocumentManagementAPI.Service
{
    public class DocumentService(FolderRepo folderRepo, DocumentRepo documentRepo, IUserRepo userRepo, ILogger<DocumentService> logger, IMapper mapper) : IDocument
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
            if (doc == null)
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

        public async Task<(ICollection<Object>, Page)> GetDocuments(string? docName, string? querySearch, int pageSize, int PageNumber)
        {

            var query = await documentRepo.GetFilteredDocumentsAsync(docName, querySearch);

            var totalItemCount = await query.CountAsync();
            Page page = new Page(pageSize, PageNumber, totalItemCount);

            var document = await query.OrderBy(d => d.Name)
                .Skip((page.PageSize * (page.PageNumber - 1)))
                .Take(page.PageSize)
                .ToListAsync();

            page.TotalItemCount = totalItemCount;

            // var doc = mapper.Map<ICollection<DocumentDto>>(document);
            var doc = document.Select( d => new
            {
                Name = d.Name,
                Id = d.Id,
               folderId = d.Folder?.Id ,
            }).ToList();

            return (doc.Cast<object>().ToList(), page);
        }

        public async Task<ICollection<Object>> GetMessages(int docId)
        {
            var docMessages = await documentRepo.GetDocumentsMessagesById(docId);

            if (docMessages == null)
            {
                logger.LogWarning("Messages not fund with this Document ID: " + docId);
                throw new NotFoundException("This document not found");
            }

            logger.LogInformation("Messages found");
            //return mapper.Map<ICollection<MessagesDto>>(docMessages);
            var messageDto = docMessages.Select(d => new 
            {
                Id = d.Id,
                Content = d.Content,
                User = new
                {
                    Id = d.User.Id,
                    UserName = d.User.UserName,
                    create_at = d.User.created_at,
                },
                
            }
            ).ToList();
            return messageDto.Cast<Object>().ToList();
        }



        // MINIMAL ESSENTIAL VALIDATION - Add these 5 critical checks to existing method
        public async Task UploadFile(IFormFile formFile, int userId, int folderId, string docName)
        {
            const long MaxFileSize = 10L * 1024 * 1024; //  10MB
            const long MinFileSize = 1024; //  1KB minimum

            
            var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png"};
            var allowedMimeTypes = new[] {
                "application/pdf",
                "image/jpeg",
                "image/png",
            };

            
            var fileSignatures = new Dictionary<string, byte[]>
            {
                { ".pdf", new byte[] { 0x25, 0x50, 0x44, 0x46 } }, // %PDF
                { ".jpg", new byte[] { 0xFF, 0xD8, 0xFF } },
                { ".jpeg", new byte[] { 0xFF, 0xD8, 0xFF } },
                { ".png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } },
            };


           
            var user = await userRepo.GetUserByIdAndInclodFolderAsync(userId);
            if (user == null)
            {
                logger.LogError("User not found by this ID: " + userId);
                throw new NotFoundException("User not in the system");
            }

            var folder = await folderRepo.GetFolderById(folderId);
            if (folder == null)
            {
                logger.LogError("Folder not found by this ID: " + folderId);
                throw new NotFoundException("Folder not in the system");
            }

            var hasAccess = user.Folders.Any(f => f.Id == folderId);
            if (!hasAccess)
            {
                logger.LogWarning($"User {userId} tried to access folder {folderId} they don't own.");
                throw new UnauthorizedException("You don't have access to this folder");
            }

            
            if (formFile == null || formFile.Length == 0)
            {
                logger.LogWarning("Document not uploaded");
                throw new BadRequestException("No document uploaded");
            }

            if (formFile.Length > MaxFileSize)
            {
                logger.LogWarning("Document is too large");
                throw new BadRequestException("The file must be less than 10MB"); // Updated message
            }

            // NEW: Check minimum file size
            if (formFile.Length < MinFileSize)
            {
                logger.LogWarning("Document is too small: {FileSize} bytes", formFile.Length);
                throw new BadRequestException("File must be at least 1KB");
            }

            // 2. NEW: Filename validation and sanitization
            if (string.IsNullOrWhiteSpace(formFile.FileName) || formFile.FileName.Length > 255)
            {
                logger.LogWarning("Invalid filename: {FileName}", formFile.FileName);
                throw new BadRequestException("Invalid filename");
            }

            // Check for malicious filename patterns
            var maliciousPatterns = new[] { "..", "\\", "/", ":", "*", "?", "\"", "<", ">", "|", "\0" };
            if (maliciousPatterns.Any(pattern => formFile.FileName.Contains(pattern)))
            {
                logger.LogWarning("Malicious filename pattern detected: {FileName}", formFile.FileName);
                throw new BadRequestException("Filename contains invalid characters");
            }

            // 3. NEW: File extension validation
            var fileExtension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
            {
                logger.LogWarning("Invalid file extension: {Extension}", fileExtension);
                throw new BadRequestException($"File type '{fileExtension}' is not allowed. Allowed: {string.Join(", ", allowedExtensions)}");
            }

            // 4. NEW: MIME type validation
            if (!allowedMimeTypes.Contains(formFile.ContentType))
            {
                logger.LogWarning("Invalid MIME type: {MimeType} for file: {FileName}", formFile.ContentType, formFile.FileName);
                throw new BadRequestException($"Invalid file type. Expected: {string.Join(", ", allowedMimeTypes)}");
            }

            // 5. NEW: Magic bytes validation (CRITICAL SECURITY)
            using var stream = formFile.OpenReadStream();
            var buffer = new byte[8];
            await stream.ReadAsync(buffer, 0, 8);
            stream.Position = 0; // Reset stream

            if (fileSignatures.ContainsKey(fileExtension))
            {
                var expectedSignature = fileSignatures[fileExtension];
                var actualSignature = buffer.Take(expectedSignature.Length).ToArray();

                if (!actualSignature.SequenceEqual(expectedSignature))
                {
                    logger.LogWarning("File signature mismatch for user {UserId}, file: {FileName}. Extension: {Extension}, but signature doesn't match",
                        userId, formFile.FileName, fileExtension);
                    throw new BadRequestException("File content doesn't match its extension");
                }
            }

            // Your existing file processing...
            using var fileBytes = new MemoryStream();
            await formFile.CopyToAsync(fileBytes);

            var doc = new Document
            {
                ContentType = formFile.ContentType,
                File = fileBytes.ToArray(),
                Name = docName,
                user = user,
                Folder = folder
            };

            await documentRepo.SaveNewDocument(doc);
            logger.LogInformation($"Document uploaded successfully for {user.UserName}. File: {formFile.FileName}, Size: {formFile.Length} bytes");
        }

        public async Task<ICollection<Object>> GetDocumentsByUserId(int userId, int folderId)
        {
            var user = await userRepo.GetUserByIdAndInclodFolderAsync(userId);
            if (user == null)
            {
                logger.LogError("User not found by this ID: " + userId);
                throw new UnauthorizedException("User not have access to this folder");
            }
            var doc = await documentRepo.GetDocumentsByUserIdAsync(userId, folderId);
            logger.LogInformation($"Get Documents by User ID: {userId} and Folder ID: {folderId}");
            return doc;
        }



    }
}
