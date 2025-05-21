using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace __Document_Management_API.IService
{
    public interface IAuth
    {
        Task<string> LogIn(AuthDto authDto);

        Task<string> Register(NewUserDto user);

        Task<User> ValidAuthentication(string userName, string passWord);

        Task<string> GetToken(User user);

        Task<string> RegisterAdmin(AdminDto admin);

        Task<string> LogInAdmin(AdminDto admin);
    }
}
