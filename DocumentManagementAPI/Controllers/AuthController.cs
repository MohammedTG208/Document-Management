using __Document_Management_API.Controllers;
using __Document_Management_API.IService;
using Asp.Versioning;
using DocumentManagementAPI.Dtos.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DocumentManagementAPI.Controllers
{
    public class AuthController(IAuth authService) : BaseController
    {
        [HttpPost("login")]
        public Task<string> LogIn([FromBody] AuthDto authDto)
        {
            return authService.LogIn(authDto);
        }

        [HttpPost("register")]
        public Task<string> Register([FromBody] NewUserDto userDto)
        {
            return authService.Register(userDto);
        }
        
    }
}
