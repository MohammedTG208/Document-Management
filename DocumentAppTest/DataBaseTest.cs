using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using DocumentManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentAppTest
{
    [TestClass]
    public sealed class DataBaseTest
    {
        [TestMethod]
        public void InsertNewUser()
        {
            var builder = new DbContextOptionsBuilder<DocumentDbContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Initial Catalog = DocumentTestData");

            using (var context =new DocumentDbContext(builder.Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                using var hmc = new HMACSHA512();
                var password = hmc.ComputeHash(Encoding.UTF8.GetBytes("MohammedTariq@"));
                var newUser = new User()
                {
                    UserName = "Test",
                    Password = Convert.ToBase64String(password),
                    Salt = Convert.ToBase64String(hmc.Key),
                    UserRole = "Customer"

                };
                context.Users.Add(newUser);
                Debug.WriteLine("Before save: " +  newUser.Id);
                context.SaveChanges();
                Debug.WriteLine("After save: "+ newUser.Id);

                Assert.AreEqual("Test",newUser.UserName);

                Assert.AreNotEqual(0,newUser.Id);
            }
        }
    }
}
