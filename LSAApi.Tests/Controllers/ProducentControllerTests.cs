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
    
    public class ProducentControllerTests
    {
        private readonly IProducentRepository _producentRepository;
        private readonly IMapper _mapper;
        private readonly ProducentController _producentController;

        public ProducentControllerTests()
        {
            _producentRepository = A.Fake<IProducentRepository>();
            _mapper = A.Fake<IMapper>();
            _producentController = new ProducentController(_producentRepository, _mapper);
        }

        [Fact]
        public void ProducentController_GetProducents_ReturnsOK()
        {
            //Arrange
            var producents = A.Fake<List<GetProducentDto>>();

            A.CallTo(()=> _mapper.Map<List<GetProducentDto>>(_producentRepository.GetProducents())).Returns(producents);

            //Act
            var result = _producentController.GetProducents();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ProducentController_GetProducent_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var producent = A.Fake<GetProducentDto>();

            A.CallTo(()=> _producentRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetProducentDto>(_producentRepository.GetProducent(id))).Returns(producent);

            //Act
            var result = _producentController.GetProducent(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ProducentController_CreateProducent_ReturnsCreated()
        {
            //Arrange
            var newProducent = A.Fake<CreateProducentDto>();
            var producent = A.Fake<Producent>();
            var producentDto = A.Fake<GetProducentDto>();

            A.CallTo(() => _mapper.Map<Producent>(newProducent)).Returns(producent);
            A.CallTo(() => _producentRepository.CreateProducent(producent)).Returns(true);
            A.CallTo(() => _mapper.Map<GetProducentDto>(producent)).Returns(producentDto);

            //Act
            var result = _producentController.CreateProducent(newProducent);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();
        }
        [Fact]
        public void ProducentController_UpdateProducent_ReturnsCreated()
        {
            //Arrange
            var updateProducent = A.Fake<UpdateProducentDto>();
            var producent = A.Fake<Producent>();
            var producentDto = A.Fake<GetProducentDto>();

            A.CallTo(() => _producentRepository.IsExist(updateProducent.ProducentId)).Returns(true);
            A.CallTo(() => _mapper.Map<Producent>(updateProducent)).Returns(producent);
            A.CallTo(() => _producentRepository.UpdateProducent(producent)).Returns(true);
            A.CallTo(() => _mapper.Map<GetProducentDto>(producent)).Returns(producentDto);

            //Act
            var result = _producentController.UpdateProducent(updateProducent);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ProducentController_DeleteProducent_ReturnsCreated()
        {
            //Arrange
            int id = 1;
            var producent = A.Fake<Producent>();

            A.CallTo(() => _producentRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _producentRepository.GetProducent(id)).Returns(producent);
            A.CallTo(() => _producentRepository.DeleteProducent(producent)).Returns(true);

            //Act
            var result = _producentController.DeleteProducent(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }



    }
}
