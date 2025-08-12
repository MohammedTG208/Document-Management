using DocumentManagement.Data.Models;
using __Document_Management_API.IService;
using AutoMapper;
using DocumentManagementAPI.Dtos.FolderDto;
using DocumentManagementAPI.Paging;
using DocumentManagementAPI.Repo;
using DocumentManagementAPI.ExceptionHandling;

namespace DocumentManagementAPI.Service
{
    public class FolderService(IUserRepo userRepo, FolderRepo folderRepo, ILogger<FolderService> logger, IMapper mapper) : IFolder
    {
        public async Task AddNewFolder(CreateFolderDto folder, int userId)
        {
            var checkUser = await userRepo.GetUserById(userId);
            if (checkUser == null)
                throw new Exception("User not found by this Id: " + userId);

            var entityFolder = mapper.Map<Folder>(folder);
            entityFolder.UserId = userId;
            entityFolder.Users = checkUser;
            await folderRepo.AddNewFolder(entityFolder);
        }



        public async Task ChangeFolderName(int folderId, int userId, ChangeFolderNameDto update)
        {
            var user = await userRepo.GetUserById(userId);
            var folder = await folderRepo.GetFolderByUserIdAndFolderId(folderId, userId, user!.UserRole.Contains("Admin"));

            if (folder == null)
            {
                throw new UnauthorizedException("You don't have access to change the name of this folder.");

            }

            folder.Name = update.Name;
            folder.isPublic = update.IsPublic;
            await folderRepo.UpdateFolder(folder);
        }

        public async Task DeleteFolder(int folderId, int userId)
        {
            try
            {
                var user = await userRepo.GetUserByIdAndInclodFolderAsync(userId);
                if (user == null)
                    throw new NotFoundException("User not found");

                bool isAdmin = user.UserRole.Contains("Admin");

                if (!isAdmin)
                {
                    bool ownsFolder = user.Folders.Any(f => f.Id == folderId);
                    if (!ownsFolder)
                        throw new UnauthorizedAccessException("You cannot delete this folder");
                }

                await folderRepo.DeleteFolder(folderId, isAdmin);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting folder with ID {FolderId} for user {UserId}", folderId, userId);
                throw new BadRequestException("Failed to delete folder: " + ex.Message);
            }

        }

        public async Task<ICollection<FolderDto>> GetAllFoldersByUserId(int userId)
        {
            var role = await userRepo.GetUserById(userId);

            var folders = await folderRepo.GetFoldersByUserId(userId, role!.UserRole.Contains("Admin"));

            return mapper.Map<ICollection<FolderDto>>(folders);
        }

        public async Task<(IEnumerable<Object>, Page)> SearchByFolderName(string folderName, int pageNumber = 1, int pageSize = 10)
        {

            var totalCount = await folderRepo.CountFolders();

            var folders = await folderRepo.Pagination(pageNumber, pageSize, folderName);

            var page = new Page(pageSize, pageNumber, totalCount);

            return (folders, page);
        }

        public async Task updateFolderStatus(int folderId, bool ispublic)
        {

            if (await folderRepo.updateFolderStatus(ispublic, folderId) == 0)
            {
                throw new BadRequestException($"if status of this folder {!ispublic} most be {ispublic}");
            }
        }

        public async Task<ICollection<Object>> GetAllFolders()
        {
            var folders = await folderRepo.GetAllFolders();
            return folders.Cast<Object>().ToList();
        }
    }
}
