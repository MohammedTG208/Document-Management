using System.Security.Claims;
using __Document_Management_API.Controllers;
using DocumentManagementAPI.Dtos;
using DocumentManagementAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementAPI.Controllers
{
    public class ProfileController(ProfileService profileService) : BaseController
    {
        [HttpGet]
        public async Task<ProfileDto?> GetProfile()
        {
            var userid=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userid,  out int userId);

            return await profileService.GetProfile(userId);
        }

        [HttpPost("add/new")]
        public async Task<string> AddProfile([FromBody] AddProfileDto profileDto)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userid, out int userId);
            return await profileService.addnewProfile(profileDto,userId);
        }

        [HttpPatch("update")]
        public async Task<string> UpdateProfile(UpdateProfileDto dto)
        {
            var userid=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userid, out int userId);
            return await profileService.UpdateProfile(dto,userId);
        }
    }
}
