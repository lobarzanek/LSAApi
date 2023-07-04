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
    public class SwitchRepositoryTests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            if (dataContext.Switches.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    dataContext.Switches.Add(
                        new Switch()
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
                        }
                        );
                }
            }
            dataContext.SaveChanges();

            return dataContext;
        }

        [Fact]
        public void SwitchRepository_GetSwitch_ReturnsSwitch()
        {
            //Arrange
            int id = 2;
            var dataContext = GetDataContext();
            var repository = new SwitchRepository(dataContext);

            //Act
            var result = repository.GetSwitch(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Switch>();
        }

        [Fact]
        public void SwitchRepository_GetSwitches_ReturnsSwitchList()
        {
            //Arrange
            var dataContext = GetDataContext();
            var repository = new SwitchRepository(dataContext);

            //Act
            var result = repository.GetSwitches();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Switch>>();
            result.Count.Should().Be(5);
        }

        [Fact]
        public void SwitchRepository_GetSwitchesByModel_ReturnsSwitchList()
        {
            //Arrange
            int id = 2;
            var dataContext = GetDataContext();
            var repository = new SwitchRepository(dataContext);

            //Act
            var result = repository.GetSwitchesByModel(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Switch>>();
            result.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void SwitchRepository_GetSwitchesBySection_ReturnsSwitchList()
        {
            //Arrange
            int id = 2;
            var dataContext = GetDataContext();
            var repository = new SwitchRepository(dataContext);

            //Act
            var result = repository.GetSwitchesBySection(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Switch>>();
            result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void SwitchRepository_GetSwitchesByStatus_ReturnsSwitchList()
        {
            //Arrange
            int id = 2;
            var dataContext = GetDataContext();
            var repository = new SwitchRepository(dataContext);

            //Act
            var result = repository.GetSwitchesByStatus(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Switch>>();
            result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void SwitchRepository_IsExist_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new SwitchRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeTrue();
        }
        [Fact]
        public void SwitchRepository_IsExist_ReturnsFalse()
        {
            //Arrange
            int id = 6;
            var dataContext = GetDataContext();
            var repository = new SwitchRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeFalse();
        }

    }
}
