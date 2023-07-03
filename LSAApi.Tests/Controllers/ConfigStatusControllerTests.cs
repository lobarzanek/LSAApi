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
    public class ConfigStatusControllerTests
    {
        private readonly IConfigStatusRepository _configStatusRepository;
        private readonly IMapper _mapper;
        private readonly ConfigStatusController _configStatusController;
        public ConfigStatusControllerTests()
        {
            _configStatusRepository = A.Fake<IConfigStatusRepository>();
            _mapper = A.Fake<IMapper>();
            _configStatusController = new ConfigStatusController(_configStatusRepository, _mapper);
        }

        [Fact]
        public void ConfigStatusController_GetConfigStatuses_ReturnOK()
        {
            //Arrange
            var configStatuses = A.Fake<ICollection<ConfigStatus>>();
            var configStatusesList = A.Fake<List<GetConfigStatusDto>>();
            A.CallTo(() => _mapper.Map<List<GetConfigStatusDto>>(configStatuses))
                .Returns(configStatusesList);

            //Act
            var result = _configStatusController.GetConfigStatuses();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void ConfigStatusController_GetConfigStatus_ReturnOK()
        {
            //Arrange
            int id = 1;
            var configStatusDto = A.Fake<GetConfigStatusDto>();

            A.CallTo(() => _configStatusRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetConfigStatusDto>(_configStatusRepository.GetConfigStatus(id))).Returns(configStatusDto);
            //Act
            var result = _configStatusController.GetConfigStatus(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ConfigStatusController_CreateConfigStatus_ReturnCreated()
        {
            //Arrange
            var createConfigStatus = A.Fake<CreateConfigStatusDto>();
            var configStatus = A.Fake<ConfigStatus>();

            A.CallTo(() => _mapper.Map<ConfigStatus>(createConfigStatus)).Returns(configStatus);
            A.CallTo(() => _configStatusRepository.CreateConfigStatus(configStatus)).Returns(true);

            //Act
            var result = _configStatusController.CreateConfigStatus(createConfigStatus);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();

        }

        [Fact]
        public void ConfigStatusController_UpdateConfigStatus_ReturnOK()
        {
            //Arrange

            var updateConfigStatus = A.Fake<UpdateConfigStatusDto>();
            var configStatus = A.Fake<ConfigStatus>();

            A.CallTo(() => _configStatusRepository.IsExist(updateConfigStatus.ConfigStatusId)).Returns(true);
            A.CallTo(() => _mapper.Map<ConfigStatus>(updateConfigStatus)).Returns(configStatus);
            A.CallTo(() => _configStatusRepository.UpdateConfigStatus(configStatus)).Returns(true);

            //Act
            var result = _configStatusController.UpdateConfigStatus(updateConfigStatus);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public void ConfigStatusController_DeleteConfigStatus_ReturnNoContent()
        {
            //Arrange
            int id = 1;
            var configStatus = A.Fake<ConfigStatus>();

            A.CallTo(() => _configStatusRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _configStatusRepository.GetConfigStatus(id)).Returns(configStatus);
            A.CallTo(() => _configStatusRepository.DeleteConfigStatus(configStatus)).Returns(true);

            //Act
            var result = _configStatusController.DeleteConfigStatus(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();

        }
    }
}
