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
    public class SwitchStatusRepositoryTests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            if (dataContext.SwitchStatuses.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    dataContext.SwitchStatuses.Add(
                        new SwitchStatus()
                        {
                            SwitchStatusId = i,
                            SwitchStatusName = "name",
                            Switches = new List<Switch>()
                        }
                        );
                }
            }
            dataContext.SaveChanges();

            return dataContext;
        }

        [Fact]
        public void SwitchStatusRepository_GetSwitchStatus_ReturnsSwitchStatus()
        {
            //Arrange
            int id = 2;
            var dataContext = GetDataContext();
            var repository = new SwitchStatusRepository(dataContext);

            //Act
            var result = repository.GetSwitchStatus(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<SwitchStatus>();
        }

        [Fact]
        public void SwitchStatusRepository_GetSwitchStatuses_ReturnsSwitchStatuses()
        {
            //Arrange
            var dataContext = GetDataContext();
            var repository = new SwitchStatusRepository(dataContext);

            //Act
            var result = repository.GetSwitchStatuses();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<SwitchStatus>>();
            result.Count.Should().Be(5);
        }

        [Fact]
        public void SwitchStatusRepository_IsExist_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new SwitchStatusRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeTrue();
        }
        [Fact]
        public void SwitchStatusRepository_IsExist_ReturnsFalse()
        {
            //Arrange
            int id = 6;
            var dataContext = GetDataContext();
            var repository = new SwitchStatusRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeFalse();
        }
    }
}
