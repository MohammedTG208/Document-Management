using System.Threading.Tasks;
using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementAPI.Repo
{
    public class UserRepo(DocumentDbContext dbContext):IUserRepo
    {
       public async Task<DocumentManagement.Data.Models.User?> GetAdminByIdAndRole(int adminId, string roleName)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u=> u.Id==adminId && u.UserRole==roleName);
        }

        public async Task<List<string>> GetAllUsersName()
        {
            return await dbContext.Users.Select(u=> u.UserName).ToListAsync();
        }

        public async Task SaveMoreThanOneUsers(List<DocumentManagement.Data.Models.User> users)
        {
           await dbContext.AddRangeAsync(users);
           await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteUserByIdAndNotAdmin(int userId)
        {
            return await dbContext.Users.Where(u=> u.Id == userId && u.UserRole!="Admin").ExecuteDeleteAsync();
        }

        public async Task<DocumentManagement.Data.Models.User?> GetUserById(int userId)
        {
            return await dbContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
        }
        public async Task<DocumentManagement.Data.Models.User?> GetUserByIdAndInclodFolderAsync(int userId)
        {
            return await dbContext.Users.Include(u=>u.Folders).FirstOrDefaultAsync(u=> u.Id==userId);
        }

        public async Task<int> CountAllUsers()
        {
            return await dbContext.Users.CountAsync();
        }

        public async Task<List<DocumentManagement.Data.Models.User>> Paganation(int pageNumber, int pageSize)
        {
            return await dbContext.Users
                .Skip(pageSize * (pageNumber-1))
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<bool> IsUsernameTaken(string userName)
        {
            return await dbContext.Users.AnyAsync(u => u.UserName == userName.ToLower());
        }

        public async Task<int> UpdateUserNameById(string newUserName,int userId)
        {
            return await dbContext.Users.Where(u=> u.Id==userId).ExecuteUpdateAsync(Update=> Update
            .SetProperty(u=> u.UserName,newUserName));
        }

        public async Task AddNewUser(DocumentManagement.Data.Models.User user)
        {
           await dbContext.Users.AddAsync(user);
           await dbContext.SaveChangesAsync();
        }

        public async Task<DocumentManagement.Data.Models.User?> GetUserByUserName(string userName)
        {
            return await dbContext.Users.Where(u=> u.UserName==userName.Trim().ToLower()).FirstOrDefaultAsync();
        }

        public async Task SaveChangeUser()
        {
            await dbContext.SaveChangesAsync();
        }

        public  ProfileProcDto? ApplayProcedure(int userId)
        {
            return  dbContext.Database.SqlQuery<ProfileProcDto>($"EXEC GetUserAndProfile {userId}").AsEnumerable().FirstOrDefault();
                        
        }
    }
}
