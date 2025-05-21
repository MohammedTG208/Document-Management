using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Data.Models;
using Xunit;

namespace Project.Test
{
    public class MessageTest
    {
        [Fact]
        public void AddNewMessage_NotPublic()
        {
            var user = new User()
            {
                UserName = "Mohammed",
                Password = "Password",
                Salt = "password",
            };

            var folder = new Folder
            {
                isPublic = true,
                Name = "Test Folder",
                Users = user,
            };

            var doc = new Document()
            {
                Name = Guid.NewGuid().ToString(),
                ContentType = "pdf",
                Folder = folder,
                user = user,
                File = Guid.NewGuid().ToByteArray(),
            };
            var userAddMessage = new User()
            {
                UserName = "HowaddMessage",
                Password = "Password",
                Salt = "password",
            };
            var newMessage = new Message()
            {
                Content = "Test message",
                IsPublic = false,
                Document = doc,
                User = userAddMessage,
            };

            Assert.False(newMessage.IsPublic);
        }
        [Fact]
        public void AddNewMessage_Public()
        {
            //Arrange
            //Nothing

            //Act
            var user = new User()
            {
                UserName = "Mohammed",
                Password = "Password",
                Salt = "password",
            };

            var folder = new Folder
            {
                isPublic = true,
                Name = "Test Folder",
                Users = user,
            };

            var doc = new Document()
            {
                Name = Guid.NewGuid().ToString(),
                ContentType = "pdf",
                Folder = folder,
                user = user,
                File = Guid.NewGuid().ToByteArray(),
            };
            var userAddMessage = new User()
            {
                UserName = "HowaddMessage",
                Password = "Password",
                Salt = "password",
            };
            var newMessage = new Message()
            {
                Content = "Test message",
                IsPublic = true,
                Document = doc,
                User = userAddMessage,
            };
            //Assert
            Assert.True(newMessage.IsPublic);
        }
    }
}
