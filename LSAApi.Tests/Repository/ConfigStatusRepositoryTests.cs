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
    public class ConfigStatusRepositoryTests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            if (dataContext.ConfigStatuses.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    dataContext.ConfigStatuses.Add(
                        new ConfigStatus()
                        {
                            ConfigStatusId = i,
                            ConfigStatusName = $"name{i}",
                            Configurations = new List<Configuration>()
                        }
                        );
                }
            }
            dataContext.SaveChanges();

            return dataContext;
        }

        [Fact]
        public void ConfigStatusRepository_GetConfigStatus_ReturnsConfigStatus()
        {
            //Arrange
            int id = 2;
            var dataContext = GetDataContext();
            var repository = new ConfigStatusRepository(dataContext);

            //Act
            var result = repository.GetConfigStatus(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ConfigStatus>();
        }

        [Fact]
        public void ConfigStatusRepository_GetConfigStatuses_ReturnsConfigStatusesList()
        {
            //Arrange
            var dataContext = GetDataContext();
            var repository = new ConfigStatusRepository(dataContext);

            //Act
            var result = repository.GetConfigStatuses();

            //Assert
            result.Should().BeOfType<List<ConfigStatus>>();
            result.Should().NotBeNullOrEmpty();
            result.Count.Should().Be(5);
        }

        [Fact]
        public void ConfigStatusRepository_IsExist_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new ConfigStatusRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeTrue();
        }
        [Fact]
        public void ConfigStatusRepository_IsExist_ReturnsFalse()
        {
            //Arrange
            int id = 6;
            var dataContext = GetDataContext();
            var repository = new ConfigStatusRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeFalse();
        }
    }
}
