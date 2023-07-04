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
    public class ConfigurationVlanRepositoryTests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            if (dataContext.ConfigurationVlans.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    dataContext.ConfigurationVlans.Add(
                        new ConfigurationVlan()
                        {
                            ConfigurationVlanId= i,
                            portNumber= i,
                            ConfigurationId= i,
                            VlanId= i,
                        }
                        );
                }
            }
            dataContext.SaveChanges();

            return dataContext;
        }

        [Fact]
        public void ConfigurationVlanRepository_GetConfigurationVlan_ReturnsConfigurationVlan()
        {
            //Arrange
            int id = 2;
            var dataContext = GetDataContext();
            var repository = new ConfigurationVlanRepository(dataContext);

            //Act
            var result = repository.GetConfigurationVlan(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ConfigurationVlan>();
        }

        [Fact]
        public void ConfigurationVlanRepository_GetConfigurationVlans_ReturnsConfigurationVlanList()
        {
            //Arrange
            var dataContext = GetDataContext();
            var repository = new ConfigurationVlanRepository(dataContext);

            //Act
            var result = repository.GetConfigurationVlans();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<ConfigurationVlan>>();
            result.Count().Should().Be(5);
        }

        [Fact]
        public void ConfigurationVlanRepository_GetConfigurationVlansByConfig_ReturnsConfigurationVlanList()
        {
            //Arrange
            int id = 2;
            var dataContext = GetDataContext();
            var repository = new ConfigurationVlanRepository(dataContext);

            //Act
            var result = repository.GetConfigurationVlansByConfig(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<ConfigurationVlan>>();
            result.Count().Should().BeGreaterThan(0);

        }

        [Fact]
        public void ConfigurationVlanRepository_IsExist_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new ConfigurationVlanRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeTrue();
        }
        [Fact]
        public void ConfigurationVlanRepository_IsExist_ReturnsFalse()
        {
            //Arrange
            int id = 6;
            var dataContext = GetDataContext();
            var repository = new ConfigurationVlanRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeFalse();
        }
    }
}
