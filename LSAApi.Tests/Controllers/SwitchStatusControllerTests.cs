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
    public class SwitchStatusControllerTests
    {
        private readonly ISwitchStatusRepository _switchStatusRepository;
        private readonly IMapper _mapper;
        private readonly SwitchStatusController _switchStatusController;

        public SwitchStatusControllerTests()
        {
            _switchStatusRepository = A.Fake<ISwitchStatusRepository>();
            _mapper = A.Fake<IMapper>();
            _switchStatusController = new SwitchStatusController(_switchStatusRepository, _mapper);
        }

        [Fact]
        public void SwitchStatusController_GetConfigStatuses_ReturnsOK()
        {
            //Arrange
            var switchStatuses = A.Fake<List<GetSwitchStatusDto>>();

            A.CallTo(() => _mapper.Map<List<GetSwitchStatusDto>>(_switchStatusRepository.GetSwitchStatuses())).Returns(switchStatuses);

            //Act
            var result = _switchStatusController.GetConfigStatuses();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SwitchStatusController_GetConfigStatus_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var switchStatus = A.Fake<GetSwitchStatusDto>();

            A.CallTo(() => _switchStatusRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetSwitchStatusDto>(_switchStatusRepository.GetSwitchStatus(id))).Returns(switchStatus);

            //Act
            var result = _switchStatusController.GetConfigStatus(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SwitchStatusController_CreateSwitchStatus_ReturnsCreated()
        {
            //Arrange
            var newSwitchstatus = A.Fake<CreateSwitchStatusDto>();
            var switchStatus = A.Fake<SwitchStatus>();
            var switchStatusDto = A.Fake<GetSwitchStatusDto>();

            A.CallTo(() => _mapper.Map<SwitchStatus>(newSwitchstatus)).Returns(switchStatus);
            A.CallTo(() => _switchStatusRepository.CreateSwitchStatus(switchStatus)).Returns(true);
            A.CallTo(() => _mapper.Map<GetSwitchStatusDto>(switchStatus)).Returns(switchStatusDto);

            //Act
            var result = _switchStatusController.CreateSwitchStatus(newSwitchstatus);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public void SwitchStatusController_UpdateSwitchStatus_ReturnsOk()
        {
            //Arrange
            var updateSwitchStatus = A.Fake<UpdateSwitchStatusDto>();
            var switchStatus = A.Fake<SwitchStatus>();
            var switchStatusDto = A.Fake<GetSwitchStatusDto>();

            A.CallTo(() => _switchStatusRepository.IsExist(updateSwitchStatus.SwitchStatusId)).Returns(true);
            A.CallTo(() => _mapper.Map<SwitchStatus>(updateSwitchStatus)).Returns(switchStatus);
            A.CallTo(() => _switchStatusRepository.UpdateSwitchStatus(switchStatus)).Returns(true);
            A.CallTo(() => _mapper.Map<GetSwitchStatusDto>(switchStatus)).Returns(switchStatusDto);

            //Act
            var result = _switchStatusController.UpdateSwitchStatus(updateSwitchStatus);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void SwitchStatusController_DeleteSwitchStatus_ReturnsNoContent()
        {
            //Arrange
            int id = 1;
            var switchStatus = A.Fake<SwitchStatus>();

            A.CallTo(() => _switchStatusRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _switchStatusRepository.GetSwitchStatus(id)).Returns(switchStatus);
            A.CallTo(() => _switchStatusRepository.DeleteSwitchStatus(switchStatus)).Returns(true);

            //Act
            var result = _switchStatusController.DeleteSwitchStatus(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
