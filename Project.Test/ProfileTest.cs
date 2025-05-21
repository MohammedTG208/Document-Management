using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Data.Models;
using Xunit;

namespace Project.Test
{
    public class ProfileTest
    {
        [Fact]
        public void Get_FullName()
        {
            User user = new User()
            {
                UserName = "Test",
                Password = "Password",
                Salt = "Password"
            };
            Profile profile = new Profile()
            {
                user = user,
                FirstName = "Mohammed",
                LastName = "Tariq",

            };
            Assert.Equal("Mohammed Tariq",profile.FullName);
        }
    }
}
