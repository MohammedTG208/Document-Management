using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos;
using DocumentManagementAPI.Repo;

namespace Project.Test.RepoTest
{
    public class UserRepoTest:IUserRepo
    {
        private List<User> _users = new List<User>();// as Table at Database but this in memory

        public Task<User?> GetAdminByIdAndRole(int adminId, string roleName)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Id == adminId && u.UserRole == roleName));
        }

        public Task<List<string>> GetAllUsersName()
        {
            return Task.FromResult(_users.Select(u => u.UserName).ToList());
        }

        public Task SaveMoreThanOneUsers(List<User> users)
        {
            _users.AddRange(users);
            return Task.CompletedTask;
        }

        public Task<int> DeleteUserByIdAndNotAdmin(int userId)
        {
            var toRemove = _users.RemoveAll(u => u.Id == userId && u.UserRole != "Admin");
            return Task.FromResult(toRemove);
        }

        public Task<User?> GetUserById(int userId)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Id == userId));
        }

        public Task<User?> GetUserByIdAndInclodFolderAsync(int userId)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Id == userId)); // Fake include
        }

        public Task<int> CountAllUsers()
        {
            return Task.FromResult(_users.Count);
        }

        public Task<List<User>> Paganation(int pageNumber, int pageSize)
        {
            return Task.FromResult(_users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        public Task<bool> IsUsernameTaken(string userName)
        {
            return Task.FromResult(_users.Any(u => u.UserName == userName.ToLower()));
        }

        public Task<int> UpdateUserNameById(string newUserName, int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.UserName = newUserName;
                return Task.FromResult(1);
            }
            return Task.FromResult(0);
        }

        public Task AddNewUser(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task<User?> GetUserByUserName(string userName)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.UserName == userName.Trim().ToLower()));
        }

        public Task SaveChangeUser()
        {
            return Task.CompletedTask; // No-op for in-memory
        }

        public ProfileProcDto? ApplayProcedure(int userId)
        {
            return null; // Not applicable for unit testing
        }
    }
}
