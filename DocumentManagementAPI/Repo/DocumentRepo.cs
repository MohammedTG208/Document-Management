using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos.DocumentDto;
using DocumentManagementAPI.Dtos.UserDocumentDto;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementAPI.Repo
{
    public class DocumentRepo(DocumentDbContext dbContext)
    {
        public async Task<Document?> GetDocumentAndUserAsync(int userId, int docId)
        {
            return await dbContext.Documents
                    .Include(d => d.user)
                    .FirstOrDefaultAsync(d => d.Id == docId && d.user.Id == userId);

        }

        public async Task DeleteDucoment(Document document)
        {
            dbContext.Documents.Remove(document);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Document?> GetDocumnetById(int docId)
        {
            return await dbContext.Documents.FirstOrDefaultAsync(d => d.Id == docId);
        }

        public async Task<IQueryable<Document>> GetFilteredDocumentsAsync(string? docName, string? querySearch)
        {
            var query = dbContext.Documents
                .Include(d => d.user)
                .Include(d => d.Folder)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(docName))
            {
                docName = docName.Trim();
                query = query.Where(d => d.Name == docName);
            }

            if (!string.IsNullOrWhiteSpace(querySearch))
            {
                querySearch = querySearch.Trim();
                query = query.Where(d =>
                    d.Name.Contains(querySearch) ||
                    (d.Folder != null && d.Folder.Name.Contains(querySearch))
                );
            }

            return await Task.FromResult(query); // So it matches async signature
        }

        public async Task<List<Message>> GetDocumentsMessagesById(int docId)
        {
            return await dbContext.Messages
                .Where(m => m.Document.Id == docId && m.IsPublic)
                .Include(m => m.User)
                .Include(m => m.Document)
                .ThenInclude(d => d.Folder)
                .ToListAsync();
        }


        public async Task SaveNewDocument(Document document)
        {
            dbContext.Documents.Add(document);
            await dbContext.SaveChangesAsync();
        }


        public async Task<ICollection<Object>> GetDocumentsByUserIdAsync(int userId, int folderId)
        {
           return await dbContext.Documents
   .Where(doc => doc.user.Id == userId && doc.Folder.Id == folderId).Select(doc => new
   {
       doc.Id,
       doc.Name,
       FolderId = doc.Folder.Id,
       FolderName = doc.Folder.Name
   }).Cast<Object>()
   .ToListAsync();
            
        }
    }
}
