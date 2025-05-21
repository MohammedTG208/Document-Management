using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DocumentManagement.Data.Models;
using __Document_Management_API.IService;
using DocumentManagementAPI.Dtos.User;
using DocumentManagementAPI.ExceptionHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using DocumentManagementAPI.Repo;

namespace DocumentManagementAPI.Service
{
    public class AuthService(IUserRepo userRepo, ILogger<AuthService> logger,IConfiguration config) : IAuth
    {
        public async Task<string> LogIn(AuthDto authDto)
        {
           var user=await ValidAuthentication(authDto.UserName, authDto.Password);
            var jwt= await GetToken(user);
            return jwt;
        }

        public async Task<string> Register(NewUserDto user)
        {
            //check from username is it in the DB or not 
            if(await userRepo.IsUsernameTaken(user.UserName))
            {
                throw new BadRequestException("This userName exists");
            }

            using var hmac = new HMACSHA256();
            var Pass = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.passWord));
            var newUser = new User()
            {
                UserName = user.UserName.ToLower(),
                Password = Convert.ToBase64String(Pass),
                Salt = Convert.ToBase64String(hmac.Key)
            };

            await userRepo.AddNewUser(newUser);

            return "User registered successfully";
        }

        public async Task<string> GetToken(User user)
        {
            //i Take the Secret key from json file or appSettings.Develpment.json  
            var securetKey = new SymmetricSecurityKey(Convert.FromBase64String(config["Authntcation:SecretKey"]));
            // here as type of Hashcode we will use 
            var signingCredentials = new SigningCredentials(securetKey, SecurityAlgorithms.HmacSha256);

            //here i will made a clime as what i will add for user or how is this user 
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole) // Correct claim
            };


            var jwtSecurtyToken = new JwtSecurityToken(
                 config["Authntcation:Issuer"],         // who created the token
                 config["Authntcation:Audince"],        // who the token is for
                 claims,                                 // the claims to include in the payload
                 DateTime.UtcNow,                       // token valid from now
                 DateTime.UtcNow.AddHours(1),           // token expires in 1 hour
                 signingCredentials                     // how the token is signed
            );


            var jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurtyToken);

            return jwt;
        }

        public async Task<User> ValidAuthentication(string userName, string passWord)
        {
            logger.LogInformation($"Start validating user: {userName}");

            // Find user (case-insensitive)
            var user = await userRepo.GetUserByUserName(userName);

            if (user == null)
            {
                throw new UnauthorizedException("Username or password is incorrect");
            }

            var salt=Convert.FromBase64String(user.Salt);
            // Check password (case-sensitive here)
            using var hmac = new HMACSHA256(salt);
            var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(passWord));
            var userPass=Convert.FromBase64String(user.Password);

            if (!computedHash.SequenceEqual(userPass))
            {
                throw new UnauthorizedException("Username or password is incorrect");
            }

            return user;
        }

        public async Task<string> RegisterAdmin(AdminDto admin)
        {
            //check from username is it in the DB or not 
            if (await userRepo.IsUsernameTaken(admin.UserName))
            {
                throw new BadRequestException("This userName exists");
            }
            using var hmac = new HMACSHA256();
            var Pass = hmac.ComputeHash(Encoding.UTF8.GetBytes(admin.PassWord));
            var newAdmin = new User()
            {
                UserName = admin.UserName,
                Password = Convert.ToBase64String(Pass),
                Salt = Convert.ToBase64String(hmac.Key),
                UserRole = "Admin"
            };

            await userRepo.AddNewUser(newAdmin);

            return "Admin registered successfully";

        }

        public async Task<string> LogInAdmin(AdminDto admin)
        {
           var user=await ValidAuthentication(admin.UserName,admin.PassWord);
            var jwt =await GetToken(user);
            return jwt;
        }
    }
}
