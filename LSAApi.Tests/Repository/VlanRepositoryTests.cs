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
    public class VlanRepositoryTests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            if (dataContext.Vlans.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    dataContext.Vlans.Add(
                        new Vlan()
                        {
                            VlanId = i,
                            VlanIpAddress = $"10.0.0.{i}",
                            VlanName = i.ToString(),
                            VlanTag = $"v{i}",
                            ConfigurationVlans = new List<ConfigurationVlan>()
                        }
                        );
                }
            }
            dataContext.SaveChanges();

            return dataContext;
        }

        [Fact]
        public void VlanRepository_GetVlan_ReturnsVlan()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new VlanRepository(dataContext);

            //Act
            var result = repository.GetVlan(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Vlan>();
        }

        [Fact]
        public void VlanRepository_GetVlans_ReturnsVlansList()
        {
            //Arrange
            var dataContext = GetDataContext();
            var repository = new VlanRepository(dataContext);

            //Act
            var result = repository.GetVlans();

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeOfType<List<Vlan>>();
            result.Count.Should().Be(5);
        }

        [Fact]
        public void VlanRepository_IsExist_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new VlanRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeTrue();
        }
        [Fact]
        public void VlanRepository_IsExist_ReturnsFalse()
        {
            //Arrange
            int id = 6;
            var dataContext = GetDataContext();
            var repository = new VlanRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeFalse();
        }
    }
}
