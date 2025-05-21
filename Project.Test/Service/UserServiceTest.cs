using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DocumentManagement.Data.Models;
using DocumentManagementAPI.Dtos.User;
using DocumentManagementAPI.ExceptionHandling;
using DocumentManagementAPI.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Project.Test.RepoTest;
using Xunit;

namespace Project.Test.Service
{
    public class UserServiceTest
    {
        [Fact]
        public async Task RegisterNewUser()
        {
            var loggerMock = new Mock<ILogger<AuthService>>();
            var configMock = new Mock<IConfiguration>();

            configMock.Setup(c => c["Authentication:SecretKey"]).Returns("test_key");
            configMock.Setup(c => c["Authentication:Issuer"]).Returns("test_issuer");
            configMock.Setup(c => c["Authentication:Audience"]).Returns("test_audience");

            UserRepoTest userRepoTest = new UserRepoTest();
            AuthService authService = new AuthService(userRepoTest,loggerMock.Object,configMock.Object);

            var CreateUserDto = new NewUserDto()
            {
                UserName = "Test",
                passWord = "Password208@"
            };

           await authService.Register(CreateUserDto);
        }

        [Fact]
        public async Task GetAllUser()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            var loggerMock=new Mock<ILogger<UserService>>();
            var userRepo = new UserRepoTest();
            UserService userService = new UserService(userRepo, mapperMock.Object, loggerMock.Object);

            var user1 = new User()
            {
                Id = 1,
                UserName = "Test",
                Password = "Password@",
                Salt = "Password@"

            };
            var user2 = new User()
            {
                Id=2,
                UserName = "Test2",
                Password = "Password2@",
                Salt = "Password2@"
            };
            //Act
           await userRepo.AddNewUser(user1);
           await userRepo.AddNewUser(user2);
            //Assert
           await userService.GetUsersAsync(1,2);
           Assert.Equal(await userRepo.GetUserById(1), user1);
        }

        [Fact]
        public async Task Register_AndDisplayExceptionForUserName()
        {
            var userRepo = new UserRepoTest();
            var LoggerMock = new Mock<ILogger<AuthService>>();

            var configMock=new Mock<IConfiguration>();
            configMock.Setup(c => c["Authentication:SecretKey"]).Returns("test_key");
            configMock.Setup(c => c["Authentication:Issuer"]).Returns("issuer_key");
            configMock.Setup(c => c["Authentication:Audience"]).Returns("audience_key");

            AuthService authService = new AuthService(userRepo, LoggerMock.Object, configMock.Object);

            var oldUser = new NewUserDto()
            {
                UserName = "Test",
                passWord = "Password",  
            };

            var newUser = new NewUserDto()
            {
                
                UserName = "Test",
                passWord = "Password",
                
            };

           await authService.Register(oldUser);

           await Assert.ThrowsAsync<BadRequestException>(async() => await authService.Register(newUser));
            
        }
    }
}
