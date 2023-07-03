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
    public class ConfigurationVlanControllerTests
    {
        private readonly IConfigurationVlanRepository _configurationVlanRepository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IVlanRepository _vlanRepository;
        private readonly IMapper _mapper;
        private readonly ConfigurationVlanController _configurationVlanController;
        public ConfigurationVlanControllerTests()
        {
            _configurationVlanRepository = A.Fake<IConfigurationVlanRepository>();
            _configurationRepository = A.Fake<IConfigurationRepository>();
            _vlanRepository = A.Fake<IVlanRepository>();
            _mapper = A.Fake<IMapper>();
            _configurationVlanController = new ConfigurationVlanController(_configurationVlanRepository,
                _configurationRepository, _vlanRepository, _mapper);
        }

        [Fact]
        public void ConfigurationVlanController_GetConfigurationVlans_ReturnsOK()
        {
            //Arrange
            var configurationVlans = A.Fake<ICollection<ConfigurationVlan>>();
            var configurationVlansList = A.Fake<List<GetConfigurationVlanDto>>();

            A.CallTo(() => _mapper.Map<List<GetConfigurationVlanDto>>(configurationVlans))
                .Returns(configurationVlansList);

            //Act
            var result = _configurationVlanController.GetConfigurationVlans();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ConfigurationVlanController_GetConfigurationVlan_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var configurationVlan = A.Fake<ConfigurationVlan>();
            var configurationVlanDto = A.Fake<GetConfigurationVlanDto>();

            A.CallTo(() => _configurationVlanRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetConfigurationVlanDto>(_configurationVlanRepository.GetConfigurationVlan(id)))
                .Returns(configurationVlanDto);

            //Act
            var result = _configurationVlanController.GetConfigurationVlan(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ConfigurationVlanController_GetConfigurationVlansByConfig_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var configurationsVlansDto = A.Fake<List<GetConfigurationVlanDto>>();

            A.CallTo(() => _configurationRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<List<GetConfigurationVlanDto>>(_configurationVlanRepository.GetConfigurationVlansByConfig(id)))
                .Returns(configurationsVlansDto);

            //Act
            var result = _configurationVlanController.GetConfigurationVlansByConfig(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ConfigurationVlanController_CreateConfigurationVlan_ReturnsCreated()
        {
            //Arrange
            var newConfigurationVlan = A.Fake<CreateConfigurationVlanDto>();
            var configurationVlan = A.Fake<ConfigurationVlan>();
            var configurationVlanDto = A.Fake<GetConfigurationVlanDto>();

            A.CallTo(() => _configurationRepository.IsExist(newConfigurationVlan.ConfigurationId)).Returns(true);
            A.CallTo(() => _vlanRepository.IsExist(newConfigurationVlan.VlanId)).Returns(true);
            A.CallTo(() => _mapper.Map<ConfigurationVlan>(newConfigurationVlan)).Returns(configurationVlan);
            A.CallTo(() => _configurationVlanRepository.CreateConfigurationVlan(configurationVlan)).Returns(true);
            A.CallTo(() => _mapper.Map<GetConfigurationVlanDto>(configurationVlan)).Returns(configurationVlanDto);


            //Act
            var result = _configurationVlanController.CreateConfigurationVlan(newConfigurationVlan);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public void ConfigurationVlanController_UpdateConfigurationVlan_ReturnsOK()
        {
            //Arrange
            var updateConfigurationVlan = A.Fake<UpdateConfigurationVlanDto>();
            var configurationVlan = A.Fake<ConfigurationVlan>();
            var configurationVlanDto = A.Fake<GetConfigurationVlanDto>();

            A.CallTo(() => _configurationVlanRepository.IsExist(updateConfigurationVlan.ConfigurationVlanId)).Returns(true);
            A.CallTo(() => _configurationRepository.IsExist(updateConfigurationVlan.ConfigurationId)).Returns(true);
            A.CallTo(() => _vlanRepository.IsExist(updateConfigurationVlan.VlanId)).Returns(true);
            A.CallTo(() => _mapper.Map<ConfigurationVlan>(updateConfigurationVlan)).Returns(configurationVlan);
            A.CallTo(() => _configurationVlanRepository.UpdateConfigurationVlan(configurationVlan)).Returns(true);
            A.CallTo(() => _mapper.Map<GetConfigurationVlanDto>(configurationVlan)).Returns(configurationVlanDto);


            //Act
            var result = _configurationVlanController.UpdateConfigurationVlan(updateConfigurationVlan);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ConfigStatusController_DeleteConfigStatus_ReturnNoContent()
        {
            //Arrange
            int id = 1;
            var configurationVlan = A.Fake<ConfigurationVlan>();

            A.CallTo(() => _configurationVlanRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _configurationVlanRepository.GetConfigurationVlan(id)).Returns(configurationVlan);
            A.CallTo(() => _configurationVlanRepository.DeleteConfigurationVlan(configurationVlan)).Returns(true);

            //Act
            var result = _configurationVlanController.DeleteConfigurationVlan(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();

        }
    }
}
