//using DocumentManagement.Data.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.SqlServer;
//using Microsoft.Extensions.Configuration;

//namespace DocumentManagement.Data
//{
//    public class DataSeeder
//    {
//        public static DbContextOptions<DocumentDbContext> GetOptions()
//        {
//            var options = new DbContextOptionsBuilder<DocumentDbContext>()
//                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DocumentManagement;Trusted_Connection=True;")
//                .Options;

//            return options;
//        }
//        public static void AddNewUser()
//        {
            

//            var user = new Models.User()
//            {
//                UserName = "mohammed",
//                Password = "M7mdTG@123",
//            };
//            var user2 = new Models.User()
//            {
//                UserName = "Tariq",
//                Password = "PasswordT@"
//            };

            
            
//            using var context = new DocumentDbContext(DataSeeder.GetOptions());
//            context.Users.Add(user);
//            context.Users.Add(user2);
//            context.SaveChanges();

//            Console.WriteLine("User and User2 added.");
//        }
//    }
//}
