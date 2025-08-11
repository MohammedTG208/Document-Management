using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos.FolderDto;
using Microsoft.AspNetCore.JsonPatch;

namespace __Document_Management_API.IService
{
    public interface IFolder
    {
        Task AddNewFolder(CreateFolderDto folder, int userId);

        Task DeleteFolder(int folderId, int userId);

        Task<ICollection<FolderDto>> GetAllFoldersByUserId(int userId);

        Task ChangeFolderName(int folderId, int userId, ChangeFolderNameDto newFolderName);
        Task<(IEnumerable<FolderDto>, DocumentManagementAPI.Paging.Page)> SearchByFolderName(string folderName, int pageNumber = 1, int pageSize = 10);
        Task updateFolderStatus(int folderId, bool ispublic);
        
        Task<ICollection<Object>> GetAllFolders();
    }
}
