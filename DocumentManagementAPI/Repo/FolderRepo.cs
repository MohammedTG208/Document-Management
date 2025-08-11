using DocumentManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementAPI.Repo
{
    public class FolderRepo(DocumentDbContext dbContext)
    {
        public async Task AddNewFolder(Folder folder)
        {
            await dbContext.Folders.AddAsync(folder);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Folder?> GetFolderById(int folderId)
        {
            return await dbContext.Folders
                .FirstOrDefaultAsync(f => f.Id == folderId);
        }

        public async Task<Folder?> GetFolderByUserIdAndFolderId(int folderId,int userId)
        {
            return await dbContext.Folders.FirstOrDefaultAsync(fu=> fu.UserId == userId && fu.Id==folderId);
        }


        

        public async Task DeleteFolder(int folderId)
        {
            await dbContext.Folders.Where(f=> f.Id == folderId).ExecuteDeleteAsync();
        }

        public async Task<List<Folder>> GetFoldersByUserId(int userId, bool isAdmin= false)
        {
            return await dbContext.Folders.Where( f => f.Users.Id==userId || isAdmin).ToListAsync();
        }

        public async Task<List<Folder>> paganation(int pageNumber , int pageSize,string folderName)
        {
            return await dbContext.Folders.Where(f=> f.Name==folderName && f.isPublic==true).Skip(pageSize*(pageNumber-1)).Take(pageSize).ToListAsync();
        }

        public async Task<int> CountFolders()
        {
            return await dbContext.Folders.Where(f=> f.isPublic==true).CountAsync();
        }

        public async Task<int> updateFolderStatus(bool Ispublic,int folderId)
        {
            return await dbContext.Folders.Where(f => f.Id == folderId && f.isPublic != Ispublic).ExecuteUpdateAsync(update => update.SetProperty(f => f.isPublic, Ispublic));
        }

        public async Task<bool> isHaveIt(int userId,int fId)
        {
            return await dbContext.Folders.AnyAsync(f=>f.Users.Id==userId && f.Id==fId);
        }

        public async Task UpdateFolder(Folder folder)
        {
            dbContext.Update(folder);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllFolders()
        {
           return await dbContext.Folders.Include(f => f.Users).Where(f=> f.isPublic == true)
                .Select(f => new { f.Id, f.Name, UserName = f.Users.UserName })
                .ToListAsync<object>();
        }
    }
}
