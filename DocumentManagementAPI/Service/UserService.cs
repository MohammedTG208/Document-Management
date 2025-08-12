using DocumentManagement.Data.Models;
using AutoMapper;
using DocumentManagementAPI.Dtos.User;
using DocumentManagementAPI.Paging;
using Microsoft.EntityFrameworkCore;
using DocumentManagementAPI.ExceptionHandling;
using __Document_Management_API.IService;
using System.Security.Cryptography;
using System.Text;
using DocumentManagementAPI.Repo;
using DocumentManagementAPI.Dtos;

namespace DocumentManagementAPI.Service
{
    public class UserService(IUserRepo userRepo, IMapper mapper, ILogger<UserService> logger) : Repo.User.IUser
    {
        public async Task<string> adminAddNewAdmins(int adminId, List<CreateAdmin> admins)
        {
            var checkAdmin = await userRepo.GetAdminByIdAndRole(adminId, "Admin");

            if (checkAdmin == null)
            {
                throw new UnauthorizedException("You dont have access to add new admin to the system");
            }

            var listAdmins = mapper.Map<List<User>>(admins);
            var existingUsernames = await userRepo.GetAllUsersName();

            foreach (var admin in listAdmins)
            {
                if (existingUsernames.Contains(admin.UserName.ToLower()))
                {
                    throw new Exception($"There is already a user with this username: {admin.UserName}");
                }


                using var hmac = new HMACSHA256();
                var Pass = hmac.ComputeHash(Encoding.UTF8.GetBytes(admin.Password));
                admin.Password = Convert.ToBase64String(Pass);
                admin.Salt = Convert.ToBase64String(hmac.Key);
                admin.UserName = admin.UserName.ToLower();
                admin.UserRole = "Admin";

            }

            await userRepo.SaveMoreThanOneUsers(listAdmins);

            return "Add all admins";
        }

        public async Task<string> DeleteUserById(int userId, int adminId)
        {
            var admin = await userRepo.GetUserById(adminId);
            if (admin == null || admin.UserRole != "Admin")
                throw new UnauthorizedException("You don't have access to delete users.");

            var user = await userRepo.GetUserById(userId);
            if (user == null)
                throw new NotFoundException("User not found by this id: " + userId);

            if (user.UserRole == "Admin")
                throw new UnauthorizedException("You cannot delete this user, because he is an admin.");

            await userRepo.DeleteUserByIdAndNotAdmin(user);

            return $"Deleted user with id: {userId}";
        }



        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await userRepo.GetUserById(id);
            return mapper.Map<UserDto>(user);
        }

        public async Task<(IEnumerable<UserDto>, Page)> GetUsersAsync(int pageSize, int PageNumber)
        {

            var totalcount = await userRepo.CountAllUsers();

            var page = new Page(pageSize, PageNumber, totalcount);

            var users = await userRepo.Paganation(page.PageNumber, page.PageSize);

            var users1 = mapper.Map<IEnumerable<UserDto>>(users);

            return (users1, page);
        }

        public async Task<UserDto?> UpdateUserName(string userName, int userId)
        {
            if (!await userRepo.IsUsernameTaken(userName))
            {
                //here update user without track him and update it.
                await userRepo.UpdateUserNameById(userName, userId);


                return mapper.Map<UserDto?>(await userRepo.GetUserById(userId));
            }
            else
            {
                throw new BadRequestException("Enter another UserName");
            }
        }

        public ProfileProcDto GetUserAndProfileAsync(int userId)
        {
            var user = userRepo.ApplayProcedure(userId);

            return mapper.Map<ProfileProcDto>(user);
        }
    }
}
