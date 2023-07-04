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
    public class ConfigurationRepositoryTests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            if (dataContext.Configurations.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    dataContext.Configurations.Add(
                        new Configuration()
                        {
                            ConfigurationId = i,
                            CreateDate = DateTime.Now,
                            SwitchId = i,
                            Switch = new Switch()
                            {
                                SwitchName = "name",
                                SwitchIpAddress = "1.1.1.1",
                                SwitchLogin = "login",
                                SwitchPassword = "pass",
                                SwitchNetbox = "link",
                                ModelId = i,
                                Model = new Model()
                                {
                                    ModelId = i,
                                    ModelName = "name",
                                    ModelPortNumber = i,
                                    Switchs = new List<Switch>()
                                },
                                SwitchStatusId = i,
                                SectionId = i,
                                Configurations = new List<Configuration>()
                            },
                            ConfigStatusId = i,
                            ConfigStatus = new ConfigStatus()
                            {
                                ConfigStatusName = $"name",
                                Configurations = new List<Configuration>()
                            },
                            UserId = i,
                            User = new User()
                            {
                                UserId = i,
                                UserName = "name",
                                UserLogin = "login",
                                UserPassword = "Password",
                            },
                            ConfigurationVlans = new List<ConfigurationVlan>()


                        }
                        );
                }
            }
            dataContext.SaveChanges();

            return dataContext;
        }

        [Fact]
        public void ConfigurationRepository_GetConfigurationById_ReturnsConfiguration()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new ConfigurationRepository(dataContext);

            //Act
            var result = repository.GetConfigurationById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Configuration>();
        }

        [Fact]
        public void ConfigurationRepository_GetAllConfigurations_ReturnsConfigurationList()
        {
            //Arrange
            var dataContext = GetDataContext();
            var repository = new ConfigurationRepository(dataContext);

            //Act
            var result = repository.GetAllConfigurations();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Configuration>>();
        }

        [Fact]
        public void ConfigurationRepository_GetConfigurationsByStatus_ReturnsConfigurationList()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new ConfigurationRepository(dataContext);

            //Act
            var result = repository.GetConfigurationsByStatus(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Configuration>>();
            result.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void ConfigurationRepository_GetConfigurationsBySwitch_ReturnsConfigurationList()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new ConfigurationRepository(dataContext);

            //Act
            var result = repository.GetConfigurationsBySwitch(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Configuration>>();
            result.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void ConfigurationRepository_GetConfigurationsByUser_ReturnsConfigurationList()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new ConfigurationRepository(dataContext);

            //Act
            var result = repository.GetConfigurationsByUser(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Configuration>>();
            result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void ConfigurationRepository_IsExist_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new ConfigurationRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeTrue();
        }
        [Fact]
        public void ConfigurationRepository_IsExist_ReturnsFalse()
        {
            //Arrange
            int id = 6;
            var dataContext = GetDataContext();
            var repository = new ConfigurationRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeFalse();
        }
    }
}
