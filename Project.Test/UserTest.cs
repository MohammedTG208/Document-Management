using System.Security.Cryptography;
using System.Text;
using DocumentManagement.Data.Models;
using Xunit;

namespace DocumentManagementAPI
{

    public class UserTest
    {
        [Fact]
        public void RegisterNewUser()
        {
            //Arrange–Act–Assert (AAA)
            using var hdmc = new HMACSHA256();
            var pass = hdmc.ComputeHash(Encoding.UTF8.GetBytes("password"));
            var newUser = new User()//Arrange Nthing here as arrange
            {
                //Act
                UserName = "Mohammed",
                Password = Convert.ToBase64String(pass),
                Salt = Convert.ToBase64String(hdmc.Key),

            };
            //Assert
            Assert.Equal("Mohammed", newUser.UserName);
            Assert.False(newUser.Password.Equals(pass));
        }

    }
}
