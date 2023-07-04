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
    public class ProducentRepositoryTests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            if (dataContext.Producents.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    dataContext.Producents.Add(
                        new Producent()
                        {
                            ProducentId = i,
                            ProducentName = "name",
                            Models = new List<Model>()
                        }
                        );
                }
            }
            dataContext.SaveChanges();

            return dataContext;
        }

        [Fact]
        public void ProducentRepository_GetProducent_ReturnsProducent()
        {
            //Arrange
            int id = 2;
            var dataContext = GetDataContext();
            var repository = new ProducentRepository(dataContext);

            //Act
            var result = repository.GetProducent(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Producent>();
        }

        [Fact]
        public void ProducentRepository_GetProducents_ReturnsProducentList()
        {
            //Arrange
            var dataContext = GetDataContext();
            var repository = new ProducentRepository(dataContext);

            //Act
            var result = repository.GetProducents();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Producent>>();
            result.Count.Should().Be(5);
        }

        [Fact]
        public void ProducentRepository_IsExist_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new ProducentRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeTrue();
        }
        [Fact]
        public void ProducentRepository_IsExist_ReturnsFalse()
        {
            //Arrange
            int id = 6;
            var dataContext = GetDataContext();
            var repository = new ProducentRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeFalse();
        }
    }
}
