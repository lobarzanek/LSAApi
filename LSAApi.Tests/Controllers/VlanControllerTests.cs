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
    public class VlanControllerTests
    {
        private readonly IVlanRepository _vlanRepository;
        private readonly IMapper _mapper;
        private readonly VlanController _vlanController;
        public VlanControllerTests()
        {
            _vlanRepository = A.Fake<IVlanRepository>();
            _mapper = A.Fake<IMapper>();
            _vlanController = new VlanController(_vlanRepository, _mapper);
        }

        [Fact]
        public void VlanController_GetVlans_ReturnsOk()
        {
            //Arrange
            var vlans = A.Fake<List<GetVlanDto>>();

            A.CallTo(() => _mapper.Map<List<GetVlanDto>>(_vlanRepository.GetVlans())).Returns(vlans);

            //Act
            var result = _vlanController.GetVlans();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void VlanController_GetVlan_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var vlan = A.Fake<GetVlanDto>();

            A.CallTo(() => _vlanRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetVlanDto>(_vlanRepository.GetVlan(id))).Returns(vlan);

            //Act
            var result = _vlanController.GetVlan(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void VlanController_CreateVlan_ReturnsCreated()
        {
            //Arrange
            var newVlan = A.Fake<CreateVlanDto>();
            var vlan = A.Fake<Vlan>();
            var vlanDto = A.Fake<GetVlanDto>();


            A.CallTo(() => _mapper.Map<Vlan>(newVlan)).Returns(vlan);
            A.CallTo(() => _vlanRepository.CreateVlan(vlan)).Returns(true);
            A.CallTo(() => _mapper.Map<GetVlanDto>(vlan)).Returns(vlanDto);

            //Act
            var result = _vlanController.CreateVlan(newVlan);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public void VlanController_UpdateVlan_ReturnsOk()
        {
            //Arrange
            var updateVlan = A.Fake<UpdateVlanDto>();
            var vlan = A.Fake<Vlan>();
            var vlanDto = A.Fake<GetVlanDto>();


            A.CallTo(() => _vlanRepository.IsExist(updateVlan.VlanId)).Returns(true);
            A.CallTo(() => _mapper.Map<Vlan>(updateVlan)).Returns(vlan);
            A.CallTo(() => _vlanRepository.UpdateVlan(vlan)).Returns(true);
            A.CallTo(() => _mapper.Map<GetVlanDto>(vlan)).Returns(vlanDto);

            //Act
            var result = _vlanController.UpdateVlan(updateVlan);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void VlanController_DeleteVlan_ReturnsNoContent()
        {
            //Arrange
            int id = 1;
            var vlan = A.Fake<Vlan>();


            A.CallTo(() => _vlanRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _vlanRepository.GetVlan(id)).Returns(vlan);
            A.CallTo(() => _vlanRepository.DeleteVlan(vlan)).Returns(true);

            //Act
            var result = _vlanController.DeleteVlan(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
