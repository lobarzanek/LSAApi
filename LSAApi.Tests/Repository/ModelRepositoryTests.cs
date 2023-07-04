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
    public class ModelRepositoryTests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            if (dataContext.Models.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    dataContext.Models.Add(
                        new Model()
                        {
                            ModelId= i,
                            ModelName = "name",
                            ModelPortNumber= i,
                            ProducentId = i,
                            Switchs = new List<Switch>()
                        }
                        );
                }
            }
            dataContext.SaveChanges();

            return dataContext;
        }

        [Fact]
        public void ModelRepository_GetModel_ReturnsModel()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new ModelRepository(dataContext);

            //Act
            var result = repository.GetModel(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Model>();
        }

        [Fact]
        public void ModelRepository_GetModels_ReturnsModelList()
        {
            //Arrange
            var dataContext = GetDataContext();
            var repository = new ModelRepository(dataContext);

            //Act
            var result = repository.GetModels();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Model>>();
            result.Count.Should().Be(5);

        }

        [Fact]
        public void ModelRepository_GetModelsByProducent_ReturnsModelList()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new ModelRepository(dataContext);

            //Act
            var result = repository.GetModelsByProducent(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Model>>();
            result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void ModelRepository_IsExist_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new ModelRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeTrue();
        }
        [Fact]
        public void ModelRepository_IsExist_ReturnsFalse()
        {
            //Arrange
            int id = 6;
            var dataContext = GetDataContext();
            var repository = new ModelRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeFalse();
        }
    }
}
