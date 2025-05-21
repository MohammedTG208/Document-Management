using DocumentManagement.Data.Models;
using __Document_Management_API.IService;
using AutoMapper;
using DocumentManagementAPI.Dtos.FolderDto;
using DocumentManagementAPI.Paging;
using DocumentManagementAPI.Repo;
using DocumentManagementAPI.ExceptionHandling;

namespace DocumentManagementAPI.Service
{
    public class FolderService(IUserRepo userRepo,FolderRepo folderRepo, ILogger<FolderService> logger,IMapper mapper) : IFolder
    {
        public async Task AddNewFolder(CreateFolderDto folder,int userId)
        {
            var checkUser = await userRepo.GetUserById(userId);
            if (checkUser == null)
                throw new Exception("User not found by this Id: " + userId);

            var entityFolder = mapper.Map<Folder>(folder);
            entityFolder.UserId = userId;
            entityFolder.Users=checkUser;
            await folderRepo.AddNewFolder(entityFolder);
        }

        

        public async Task ChangeFolderName(int folderId, int userId, ChangeFolderNameDto update)
        {
            var folder = await folderRepo.GetFolderByUserIdAndFolderId(folderId, userId);

            if (folder == null)
            {
                throw new UnauthorizedException("You don't have access to change the name of this folder.");

            }

            folder.Name=update.Name;
            await folderRepo.UpdateFolder(folder);
        }

        public async Task DeleteFolder(int folderId,int userId )
        { 
            var user = await userRepo.GetUserByIdAndInclodFolderAsync(userId);
            if (!user.Folders.Any(f => f.Id == folderId)) throw new NotFoundException("This folder not found");
            if (user.Id!=userId && !user.Folders.Any(s=>s.Id==folderId)) throw new Exception("you can not delete this folder");
            await folderRepo.DeleteFolder(folderId);

        }

        public async Task<ICollection<FolderDto>> GetAllFoldersByUserId(int userId)
        {
            var folders=await folderRepo.GetFoldersByUserId(userId);

            return mapper.Map<ICollection<FolderDto>>(folders);
        }

        public async Task<(IEnumerable<FolderDto>, Page)> SearchByFolderName(string folderName, int pageNumber = 1, int pageSize = 10)
        {

            var totalCount = await folderRepo.CountFolders();

            var folders = await folderRepo.paganation(pageNumber, pageSize,folderName);

            var folderDtos = mapper.Map<IEnumerable<FolderDto>>(folders);
            var page = new Page(pageSize, pageNumber, totalCount);

            return (folderDtos, page);
        }

        public async Task updateFolderStatus(int folderId, bool ispublic)
        {
            
            if (await folderRepo.updateFolderStatus(ispublic, folderId) == 0)
            {
                throw new BadRequestException($"if status of this folder {!ispublic} most be {ispublic}");
            }
        }
    }
}
