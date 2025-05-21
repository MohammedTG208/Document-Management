using System.Security.Claims;
using __Document_Management_API.Controllers;
using __Document_Management_API.IService;
using Asp.Versioning;
using DocumentManagementAPI.Dtos.User;
using DocumentManagementAPI.Repo.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementAPI.Controllers
{
    [ApiVersion("2.0")]
    public class AdminAuthController(IAuth authService, Repo.User.IUser userRepository) : BaseController
    {
        [HttpPost("login/admin")]
        
        public async Task<string> AdminLogin(AdminDto adminDto)
        {
            return await authService.LogInAdmin(adminDto);
        }

        [HttpPost("register/admin")]
        public async Task<string> AdminRegister(AdminDto adminDto)
        {
            return await authService.RegisterAdmin(adminDto);
        }

        [HttpPost("add/Admins")]
        [Authorize(Roles ="Admin")]
        public async Task<string> addNewAdmins(List<CreateAdmin> adminDtos)
        {
            var adminId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            int.TryParse(adminId, out int adminid);
            return await userRepository.adminAddNewAdmins(adminid,adminDtos);
        }
    }
}
