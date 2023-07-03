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
    public class UserControllerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly UserController _userController;
        public UserControllerTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _roleRepository= A.Fake<IRoleRepository>();
            _mapper = A.Fake<IMapper>();
            _userController = new UserController(_userRepository, _roleRepository, _mapper);
        }

        [Fact]
        public void UserController_GetUsers_ReturnsOk()
        {
            //Arrange
            var users = A.Fake<List<GetUserDto>>();

            A.CallTo(() => _mapper.Map<List<GetUserDto>>(_userRepository.GetUsers())).Returns(users);

            //Act
            var result = _userController.GetUsers();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void UserController_GetUserById_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var user = A.Fake<GetUserDto>();

            A.CallTo(() => _userRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetUserDto>(_userRepository.GetUser(id))).Returns(user);

            //Act
            var result = _userController.GetUserById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void UserController_GetUsersByRole_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var users = A.Fake<List<GetUserDto>>();

            A.CallTo(() => _roleRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<List<GetUserDto>>(_userRepository.GetUsersByRole(id))).Returns(users);

            //Act
            var result = _userController.GetUsersByRole(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void UserController_CreateUser_ReturnsCreated()
        {
            //Arrange
            var newUser = A.Fake<CreateUserDto>();
            var user = A.Fake<User>();
            var userDto= A.Fake<GetUserDto>();

            A.CallTo(() => _roleRepository.IsExist(newUser.RoleId)).Returns(true);
            A.CallTo(() => _mapper.Map<User>(newUser)).Returns(user);
            A.CallTo(() => _userRepository.CreateUser(user)).Returns(true);
            A.CallTo(() => _mapper.Map<GetUserDto>(user)).Returns(userDto);

            //Act
            var result = _userController.CreateUser(newUser);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public void UserController_UpdateUser_ReturnsOk()
        {
            //Arrange
            var updateUser = A.Fake<UpdateUserDto>();
            var user = A.Fake<User>();
            var userDto = A.Fake<GetUserDto>();

            A.CallTo(() => _userRepository.IsExist(updateUser.UserId)).Returns(true);
            A.CallTo(() => _roleRepository.IsExist(updateUser.RoleId)).Returns(true);
            A.CallTo(() => _userRepository.GetUser(updateUser.UserId)).Returns(user);
            A.CallTo(() => _userRepository.UpdateUser(user)).Returns(true);
            A.CallTo(() => _mapper.Map<GetUserDto>(user)).Returns(userDto);

            //Act
            var result = _userController.UpdateUser(updateUser);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void UserController_UpdateUserPassword_ReturnsNoContent()
        {
            //Arrange
            var updateUserPassword = A.Fake<UpdateUserPasswordDto>();
            var user = A.Fake<User>();
            var userDto = A.Fake<GetUserDto>();

            A.CallTo(() => _userRepository.IsExist(updateUserPassword.UserId)).Returns(true);
            A.CallTo(() => _userRepository.GetUser(updateUserPassword.UserId)).Returns(user);
            A.CallTo(() => _userRepository.UpdateUser(user)).Returns(true);

            //Act
            var result = _userController.UpdateUserPassword(updateUserPassword);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void UserController_DeleteUser_ReturnsNoContent()
        {
            //Arrange
            int id = 1;
            var user = A.Fake<User>();

            A.CallTo(() => _userRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _userRepository.GetUser(id)).Returns(user);
            A.CallTo(() => _userRepository.DeleteUser(user)).Returns(true);

            //Act
            var result = _userController.DeleteUser(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
