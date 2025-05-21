using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos;
using DocumentManagementAPI.Dtos.User;
using DocumentManagementAPI.Paging;

namespace DocumentManagementAPI.Repo.User
{
    public interface IUser
    {
        Task<(IEnumerable<UserDto>, Page)> GetUsersAsync(int pageSize,int Pagenumber);
        //This "?" as may return an Object or null.
        Task<UserDto?> GetUserByIdAsync(int id);

        Task<UserDto?> UpdateUserName(string userName ,int userId);

        //Delete User from Admin only.

        Task<string> DeleteUserById(int userId, int adminId);


        Task<string> adminAddNewAdmins(int adminId,List<CreateAdmin> admins);

        ProfileProcDto GetUserAndProfileAsync(int userId);


    }
}
