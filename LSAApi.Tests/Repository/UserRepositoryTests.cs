using FluentAssertions;
using LSAApi.Data;
using LSAApi.Models;
using LSAApi.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LSAApi.Tests.Repository
{
    public class UserRepositoryTests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            if (dataContext.Users.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    dataContext.Users.Add(
                        new User()
                        {
                            UserId = i,
                            UserName = "name",
                            UserLogin = "login",
                            UserPassword = "Password",
                            RoleId = i,
                        }
                        );
                }
            }
            dataContext.SaveChanges();

            return dataContext;
        }

        [Fact]
        public void UserRepository_GetUser_ReturnsUser()
        {
            //Arrange
            int id = 2;
            var dataContext = GetDataContext();
            var repository = new UserRepository(dataContext);

            //Act
            var result = repository.GetUser(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
        }

        [Fact]
        public void UserRepository_GetUsers_ReturnsUserList()
        {
            //Arrange
            var dataContext = GetDataContext();
            var repository = new UserRepository(dataContext);

            //Act
            var result = repository.GetUsers();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<User>>();
            result.Count.Should().Be(5);
        }

        [Fact]
        public void UserRepository_GetUsersByRole_ReturnsUserList()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new UserRepository(dataContext);

            //Act
            var result = repository.GetUsersByRole(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<User>>();
            result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void UserRepository_IsExist_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new UserRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeTrue();
        }
        [Fact]
        public void UserRepository_IsExist_ReturnsFalse()
        {
            //Arrange
            int id = 6;
            var dataContext = GetDataContext();
            var repository = new UserRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeFalse();
        }
    }
}
