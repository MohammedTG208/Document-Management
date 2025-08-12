using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos;

namespace DocumentManagementAPI.Repo
{
    public interface IUserRepo
    {
        Task<DocumentManagement.Data.Models.User?> GetAdminByIdAndRole(int adminId, string roleName);
        Task<List<string>> GetAllUsersName();
        Task SaveMoreThanOneUsers(List<DocumentManagement.Data.Models.User> users);
        Task DeleteUserByIdAndNotAdmin(DocumentManagement.Data.Models.User user);
        Task<DocumentManagement.Data.Models.User?> GetUserById(int userId);
        Task<DocumentManagement.Data.Models.User?> GetUserByIdAndInclodFolderAsync(int userId, bool isAdmin = false);
        Task<int> CountAllUsers();
        Task<List<DocumentManagement.Data.Models.User>> Paganation(int pageNumber, int pageSize);
        Task<bool> IsUsernameTaken(string userName);
        Task<int> UpdateUserNameById(string newUserName, int userId);
        Task AddNewUser(DocumentManagement.Data.Models.User user);
        Task<DocumentManagement.Data.Models.User?> GetUserByUserName(string userName);
        Task SaveChangeUser();
        ProfileProcDto? ApplayProcedure(int userId);
    }

}
