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
    public class ConfigurationControllerTests
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ISwitchRepository _switchRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISwitchStatusRepository _switchStatusRepository;
        private readonly IConfigStatusRepository _configStatusRepository;
        private readonly IMapper _mapper;
        private readonly ConfigurationController _configurationController;

        public ConfigurationControllerTests()
        {
            _configurationRepository = A.Fake<IConfigurationRepository>();
            _switchRepository = A.Fake<ISwitchRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _switchStatusRepository = A.Fake<ISwitchStatusRepository>();
            _configStatusRepository = A.Fake<IConfigStatusRepository>();
            _mapper = A.Fake<IMapper>();
            _configurationController = new ConfigurationController(_configurationRepository, _switchRepository,
                _userRepository, _switchStatusRepository, _configStatusRepository, _mapper);
        }

        [Fact]
        public void ConfigurationController_GetConfigurations_ReturnsOK()
        {
            //Arrange
            var configurations = A.Fake<ICollection<GetConfigurationDto>>();
            var configurationsList = A.Fake<List<GetConfigurationDto>>();

            A.CallTo(() => _mapper.Map<List<GetConfigurationDto>>(configurations)).Returns(configurationsList);

            //Act
            var result = _configurationController.GetConfigurations();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void ConfigurationController_GetConfiguration_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var configuration = A.Fake<Configuration>();
            var configurationDto = A.Fake<GetConfigurationDto>();

            A.CallTo(() => _configurationRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetConfigurationDto>(_configurationRepository.GetConfigurationById(id)))
                .Returns(configurationDto);

            //Act
            var result = _configurationController.GetConfiguration(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void ConfigurationController_GetConfigurationsBySwitch_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var configuration = A.Fake<Configuration>();
            var configurationsDto = A.Fake<List<GetConfigurationDto>>();

            A.CallTo(() => _switchRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<List<GetConfigurationDto>>(_configurationRepository.GetConfigurationsBySwitch(id)))
                .Returns(configurationsDto);

            //Act
            var result = _configurationController.GetConfigurationsBySwitch(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ConfigurationController_GetConfigurationsByUser_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var configuration = A.Fake<Configuration>();
            var configurationsDto = A.Fake<List<GetConfigurationDto>>();

            A.CallTo(() => _userRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<List<GetConfigurationDto>>(_configurationRepository.GetConfigurationsByUser(id)))
                .Returns(configurationsDto);

            //Act
            var result = _configurationController.GetConfigurationsByUser(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ConfigurationController_GetConfigurationsByStatus_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var configuration = A.Fake<Configuration>();
            var configurationsDto = A.Fake<List<GetConfigurationDto>>();

            A.CallTo(() => _switchStatusRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<List<GetConfigurationDto>>(_configurationRepository.GetConfigurationsByStatus(id)))
                .Returns(configurationsDto);

            //Act
            var result = _configurationController.GetConfigurationsByStatus(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ConfigurationController_CreateConfiguration_ReturnsCreated()
        {
            //Arrange
            var configuration = A.Fake<Configuration>();
            var createConfiguration = A.Fake<CreateConfigurationDto>();

            A.CallTo(() => _switchRepository.IsExist(createConfiguration.SwitchId)).Returns(true);
            A.CallTo(() => _userRepository.IsExist(createConfiguration.UserId)).Returns(true);
            A.CallTo(() => _mapper.Map<Configuration>(createConfiguration)).Returns(configuration);
            A.CallTo(() => _configurationRepository.CreateConfiguration(configuration)).Returns(true);


            //Act
            var result = _configurationController.CreateConfiguration(createConfiguration);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public void ConfigurationController_UpdateConfiguration_ReturnsOK()
        {
            //Arrange
            var configuration = A.Fake<Configuration>();
            var updateConfiguration = A.Fake<UpdateConfigurationDto>();

            A.CallTo(() => _configurationRepository.IsExist(updateConfiguration.ConfigurationId)).Returns(true);
            A.CallTo(() => _switchRepository.IsExist(updateConfiguration.SwitchId)).Returns(true);
            A.CallTo(() => _userRepository.IsExist(updateConfiguration.UserId)).Returns(true);
            A.CallTo(() => _configStatusRepository.IsExist(updateConfiguration.ConfigStatusId)).Returns(true);
            A.CallTo(() => _mapper.Map<Configuration>(updateConfiguration)).Returns(configuration);
            A.CallTo(() => _configurationRepository.GetConfigurationById(updateConfiguration.ConfigurationId)).Returns(configuration);
            A.CallTo(() => _configurationRepository.UpdateConfiguration(configuration)).Returns(true);

            //Act
            var result = _configurationController.UpdateConfiguration(updateConfiguration);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ConfigurationController_DeleteConfiguration_ReturnsNoContent()
        {
            //Arrange
            int id = 1;
            var configuration = A.Fake<Configuration>();

            A.CallTo(() => _configurationRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _configurationRepository.GetConfigurationById(id)).Returns(configuration);
            A.CallTo(() => _configurationRepository.DeleteConfiguration(configuration)).Returns(true);

            //Act
            var result = _configurationController.DeleteConfiguration(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
