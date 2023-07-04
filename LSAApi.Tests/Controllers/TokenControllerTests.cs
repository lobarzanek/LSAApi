using FakeItEasy;
using FluentAssertions;
using LSAApi.Controllers;
using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using LSAApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LSAApi.Tests.Controllers
{
    public class TokenControllerTests
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly TokenController _tokenController;

        public TokenControllerTests()
        {
            _configuration = A.Fake<IConfiguration>();
            _userRepository = A.Fake<IUserRepository>();
            _tokenController = new TokenController(_configuration, _userRepository);
        }

        [Fact]
        public void TokenController_Login_ReturnsOk()
        {
            //Arrange
            var userLoginDto = new UserLoginDto { UserLogin = "login", UserPassword = "password" };
            var user = new User { RoleId = 1, UserId = 1, UserLogin = "login", UserPassword = "password", UserName = "name" };
            var jwtToken = "jwttesttoken-jwttesttoken-jwttesttoken";

            A.CallTo(() => _userRepository.UserLogin(userLoginDto.UserLogin, userLoginDto.UserPassword)).Returns(user);
            A.CallTo(() => _configuration["Jwt:Key"]).Returns(jwtToken);

            //Act
            var result = _tokenController.Login(userLoginDto);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
