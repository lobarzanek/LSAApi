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
    public class RoleControllerTests
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly RoleController _roleController;

        public RoleControllerTests()
        {
            _roleRepository = A.Fake<IRoleRepository>();
            _mapper = A.Fake<IMapper>();
            _roleController = new RoleController(_roleRepository, _mapper);
        }

        [Fact]
        public void RoleController_GetRoles_ReturnsOK()
        {
            //Arrange
            var roles = A.Fake<List<GetRoleDto>>();

            A.CallTo(() => _mapper.Map<List<GetRoleDto>>(_roleRepository.GetRoles())).Returns(roles);
            //Act
            var result = _roleController.GetRoles();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void RoleController_GetRole_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var role = A.Fake<GetRoleDto>();

            A.CallTo(() => _roleRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetRoleDto>(_roleRepository.GetRole(id))).Returns(role);
            //Act
            var result = _roleController.GetRole(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void RoleController_CreateRole_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var newRole = A.Fake<CreateRoleDto>();
            var role = A.Fake<Role>();
            var roleDto = A.Fake<GetRoleDto>();

            A.CallTo(() => _mapper.Map<Role>(newRole)).Returns(role);
            A.CallTo(() => _roleRepository.CreateRole(role)).Returns(true);
            A.CallTo(() => _mapper.Map<GetRoleDto>(role)).Returns(roleDto);
            //Act
            var result = _roleController.CreateRole(newRole);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public void RoleController_UpdateRole_ReturnsOK()
        {
            //Arrange
            var updateRole = A.Fake<UpdateRoleDto>();
            var role = A.Fake<Role>();
            var roleDto = A.Fake<GetRoleDto>();

            A.CallTo(() => _roleRepository.IsExist(updateRole.RoleId)).Returns(true);
            A.CallTo(() => _mapper.Map<Role>(updateRole)).Returns(role);
            A.CallTo(() => _roleRepository.UpdateRole(role)).Returns(true);
            A.CallTo(() => _mapper.Map<GetRoleDto>(role)).Returns(roleDto);
            //Act
            var result = _roleController.UpdateRole(updateRole);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void RoleController_DeleteRole_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var role = A.Fake<Role>();

            A.CallTo(() => _roleRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _roleRepository.GetRole(id)).Returns(role);
            A.CallTo(() => _roleRepository.DeleteRole(role)).Returns(true);

            //Act
            var result = _roleController.DeleteRole(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

    }
}
