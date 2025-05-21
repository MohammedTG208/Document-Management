using System.Security.Claims;
using System.Threading.Tasks;
using __Document_Management_API.Controllers;
using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos;
using DocumentManagementAPI.Dtos.User;
using DocumentManagementAPI.Paging;
using DocumentManagementAPI.Repo.User;
using DocumentManagementAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagementAPI.Controllers
{
    public class UserController(IUser userRepository) : BaseController
    {
        [HttpGet("get/all")]
        [Authorize(Roles ="Admin")]
        public async Task<(IEnumerable<UserDto>,Page)> GetAllUsers(int pageSize = 10, int pageNumber = 1)
        {
            return await userRepository.GetUsersAsync(pageSize,pageNumber);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            return Ok(await userRepository.GetUserByIdAsync(id));
        }
        [HttpPatch("update/userName")]
        [Authorize]
        public async Task<UserDto?> UpdateUserName([FromQuery] string userName)
        {
            var userid=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userid, out var userId);
            return await userRepository.UpdateUserName(userName, userId);
        }

        [HttpDelete("delete/{userId}")]
        [Authorize(Roles ="Admin")]
        public async Task<string> DeleteUser(int userId)
        {
            var adminId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(adminId, out int adminid);

            return await userRepository.DeleteUserById(userId, adminid);
        }

        [HttpGet("user/profile")]
        [Authorize]
        public ProfileProcDto GetUserAndProfile()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse (userid, out int userId);
            return  userRepository.GetUserAndProfileAsync(userId);
        }

        
    }
}
