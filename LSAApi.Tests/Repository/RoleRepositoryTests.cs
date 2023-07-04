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
    public class RoleRepositoryTests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            if (dataContext.Roles.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    dataContext.Roles.Add(
                        new Role()
                        {
                            RoleId = i,
                            RoleName = $"role{i}",
                            Users = new List<User>()
                        }
                        );
                }
            }
            dataContext.SaveChanges();

            return dataContext;
        }

        [Fact]
        public void RoleRepository_GetRole_ReturnsRole()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new RoleRepository(dataContext);

            //Act
            var result = repository.GetRole(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Role>();
        }

        [Fact]
        public void RoleRepository_GetRoles_ReturnsRolesList()
        {
            //Arrange
            var dataContext = GetDataContext();
            var repository = new RoleRepository(dataContext);

            //Act
            var result = repository.GetRoles();

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeOfType<List<Role>>();
            result.Count.Should().Be(5);

        }

        [Fact]
        public void RoleRepository_IsExist_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new RoleRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeTrue();
        }
        [Fact]
        public void RoleRepository_IsExist_ReturnsFalse()
        {
            //Arrange
            int id = 6;
            var dataContext = GetDataContext();
            var repository = new RoleRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeFalse();
        }
    }
}
