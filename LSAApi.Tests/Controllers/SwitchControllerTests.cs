using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LSAApi.Controllers;
using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using LSAApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LSAApi.Tests.Controllers
{
    public class SwitchControllerTests
    {
        private readonly ISwitchRepository _switchRepository;
        private readonly IModelRepository _modelRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISwitchStatusRepository _switchStatusRepository;
        private readonly IMapper _mapper;
        private readonly SwitchController _switchController;

        public SwitchControllerTests()
        {
            _switchRepository = A.Fake<ISwitchRepository>();
            _modelRepository = A.Fake<IModelRepository>();
            _sectionRepository = A.Fake<ISectionRepository>();
            _switchStatusRepository = A.Fake<ISwitchStatusRepository>();
            _mapper = A.Fake<IMapper>();
            _switchController = new SwitchController(_switchRepository, _modelRepository, _sectionRepository, _switchStatusRepository, _mapper);
        }

        [Fact]
        public void SwitchController_GetSwitches_ReturnsOk()
        {
            //Arrange
            var switches = A.Fake<List<GetSwitchDto>>();

            A.CallTo(() => _mapper.Map<List<GetSwitchDto>>(_switchRepository.GetSwitches())).Returns(switches);

            //Act
            var result = _switchController.GetSwitches();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SwitchController_GetSwitch_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var ethSwitch = A.Fake<GetSwitchDto>();

            A.CallTo(() => _switchRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetSwitchDto>(_switchRepository.GetSwitch(id))).Returns(ethSwitch);

            //Act
            var result = _switchController.GetSwitch(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SwitchController_GetSwitchesByModel_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var switches = A.Fake<List<GetSwitchDto>>();

            A.CallTo(() => _modelRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<List<GetSwitchDto>>(_switchRepository.GetSwitchesByModel(id))).Returns(switches);

            //Act
            var result = _switchController.GetSwitchesByModel(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SwitchController_GetSwitchesByStatus_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var switches = A.Fake<List<GetSwitchDto>>();

            A.CallTo(() => _switchStatusRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<List<GetSwitchDto>>(_switchRepository.GetSwitchesByStatus(id))).Returns(switches);

            //Act
            var result = _switchController.GetSwitchesByStatus(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SwitchController_GetSwitchesBySection_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var switches = A.Fake<List<GetSwitchDto>>();

            A.CallTo(() => _sectionRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<List<GetSwitchDto>>(_switchRepository.GetSwitchesBySection(id))).Returns(switches);

            //Act
            var result = _switchController.GetSwitchesBySection(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SwitchController_GetSwitchCredentials_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var ethSwitch = A.Fake<GetSwitchCredentialsDto>();

            A.CallTo(() => _switchRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetSwitchCredentialsDto>(_switchRepository.GetSwitch(id))).Returns(ethSwitch);

            //Act
            var result = _switchController.GetSwitchCredentials(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SwitchController_CreateSwitch_ReturnsCreated()
        {
            //Arrange
            var newSwitch = A.Fake<CreateSwitchDto>();
            var ethSwitch = A.Fake<Switch>();
            var ethSwitchDto = A.Fake<GetSwitchDto>();

            A.CallTo(() => _modelRepository.IsExist(newSwitch.ModelId)).Returns(true);
            A.CallTo(() => _sectionRepository.IsExist(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _mapper.Map<Switch>(newSwitch)).Returns(ethSwitch);
            A.CallTo(() => _switchRepository.CreateSwitch(ethSwitch)).Returns(true);
            A.CallTo(() => _mapper.Map<GetSwitchDto>(ethSwitch)).Returns(ethSwitchDto);

            //Act
            var result = _switchController.CreateSwitch(newSwitch);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public void SwitchController_UpdateSwitch_ReturnsOK()
        {
            //Arrange
            var updateSwitch = A.Fake<UpdateSwitchDto>();
            var ethSwitch = A.Fake<Switch>();
            var ethSwitchDto = A.Fake<GetSwitchDto>();

            A.CallTo(() => _switchRepository.IsExist(updateSwitch.SwitchId)).Returns(true);
            A.CallTo(() => _modelRepository.IsExist(updateSwitch.ModelId)).Returns(true);
            A.CallTo(() => _switchStatusRepository.IsExist(updateSwitch.SwitchStatusId)).Returns(true);
            A.CallTo(() => _sectionRepository.IsExist(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _mapper.Map<Switch>(updateSwitch)).Returns(ethSwitch);
            A.CallTo(() => _switchRepository.GetSwitch(ethSwitch.SwitchId)).Returns(ethSwitch);
            A.CallTo(() => _switchRepository.UpdateSwitch(ethSwitch)).Returns(true);
            A.CallTo(() => _mapper.Map<GetSwitchDto>(ethSwitch)).Returns(ethSwitchDto);

            //Act
            var result = _switchController.UpdateSwitch(updateSwitch);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SwitchController_UpdateSwitchCredentials_ReturnsOK()
        {
            //Arrange
            var updateSwitchCr = A.Fake<UpdateSwitchCredentialsDto>();
            var ethSwitch = A.Fake<Switch>();
            var ethSwitchDto = A.Fake<GetSwitchDto>();

            A.CallTo(() => _switchRepository.IsExist(updateSwitchCr.SwitchId)).Returns(true);
            A.CallTo(() => _switchRepository.GetSwitch(updateSwitchCr.SwitchId)).Returns(ethSwitch);
            A.CallTo(() => _switchRepository.UpdateSwitch(ethSwitch)).Returns(true);

            //Act
            var result = _switchController.UpdateSwitchCredentials(updateSwitchCr);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void SwitchController_DeleteSwitch_NoContent()
        {
            //Arrange
            int id = 1;
            var ethSwitch = A.Fake<Switch>();

            A.CallTo(() => _switchRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _switchRepository.GetSwitch(id)).Returns(ethSwitch);
            A.CallTo(() => _switchRepository.DeleteSwitch(ethSwitch)).Returns(true);

            //Act
            var result = _switchController.DeleteSwitch(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }


    }
}
