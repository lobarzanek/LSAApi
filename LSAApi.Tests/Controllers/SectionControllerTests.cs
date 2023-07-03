using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LSAApi.Controllers;
using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LSAApi.Tests.Controllers
{
    public class SectionControllerTests
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;
        private readonly SectionController _sectionController;

        public SectionControllerTests()
        {
            _sectionRepository = A.Fake<ISectionRepository>();
            _mapper = A.Fake<IMapper>();
            _sectionController = new SectionController(_sectionRepository, _mapper);

        }

        [Fact]
        public void SectionController_GetSections_ReturnsOK()
        {
            //Arrange
            var sections = A.Fake<List<GetSectionDto>>();

            A.CallTo(() => _mapper.Map<List<GetSectionDto>>(_sectionRepository.GetSections())).Returns(sections);

            //Act
            var result = _sectionController.GetSections();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SectionController_GetSection_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var section = A.Fake<GetSectionDto>();

            A.CallTo(() => _sectionRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetSectionDto>(_sectionRepository.GetSection(id))).Returns(section);

            //Act
            var result = _sectionController.GetSection(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SectionController_CreateSection_ReturnsCreated()
        {
            //Arrange
            var newSection = A.Fake<CreateSectionDto>();
            var section = A.Fake<Section>();
            var sectionDto = A.Fake<GetSectionDto>();

            A.CallTo(() => _mapper.Map<Section>(newSection)).Returns(section);
            A.CallTo(() => _sectionRepository.CreateSection(section)).Returns(true);
            A.CallTo(() => _mapper.Map<GetSectionDto>(section)).Returns(sectionDto);


            //Act
            var result = _sectionController.CreateSection(newSection);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public void SectionController_UpdateSection_ReturnsOK()
        {
            //Arrange
            var updateSection = A.Fake<UpdateSectionDto>();
            var section = A.Fake<Section>();
            var sectionDto = A.Fake<GetSectionDto>();

            A.CallTo(() => _sectionRepository.IsExist(updateSection.SectionId)).Returns(true);
            A.CallTo(() => _mapper.Map<Section>(updateSection)).Returns(section);
            A.CallTo(() => _sectionRepository.UpdateSection(section)).Returns(true);
            A.CallTo(() => _mapper.Map<GetSectionDto>(section)).Returns(sectionDto);


            //Act
            var result = _sectionController.UpdateSection(updateSection);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SectionController_DeleteSection_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var section = A.Fake<Section>();

            A.CallTo(() => _sectionRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _sectionRepository.GetSection(id)).Returns(section);
            A.CallTo(() => _sectionRepository.DeleteSection(section)).Returns(true);

            //Act
            var result = _sectionController.DeleteSection(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
