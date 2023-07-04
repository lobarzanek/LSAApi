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
    public class SectionRepositoryTests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dataContext = new DataContext(options);
            dataContext.Database.EnsureCreated();

            if (dataContext.Sections.Count() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    dataContext.Sections.Add(
                        new Section()
                        {
                            SectionId = i,
                            SectionName = "name",
                            Switchs= new List<Switch>()
                        }
                        );
                }
            }
            dataContext.SaveChanges();

            return dataContext;
        }

        [Fact]
        public void SectionRepository_GetSection_ReturnsSection()
        {
            //Arrange
            int id = 2;
            var dataContext = GetDataContext();
            var repository = new SectionRepository(dataContext);

            //Act
            var result = repository.GetSection(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Section>();
        }
        [Fact]
        public void SectionRepository_GetSections_ReturnsSectionList()
        {
            //Arrange
            var dataContext = GetDataContext();
            var repository = new SectionRepository(dataContext);

            //Act
            var result = repository.GetSections();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Section>>();
            result.Count.Should().Be(5);
        }

        [Fact]
        public void SectionRepository_IsExist_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dataContext = GetDataContext();
            var repository = new SectionRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeTrue();
        }
        [Fact]
        public void SectionRepository_IsExist_ReturnsFalse()
        {
            //Arrange
            int id = 6;
            var dataContext = GetDataContext();
            var repository = new SectionRepository(dataContext);

            //Act
            var result = repository.IsExist(id);

            //Assert            
            result.Should().BeFalse();
        }
    }
}
