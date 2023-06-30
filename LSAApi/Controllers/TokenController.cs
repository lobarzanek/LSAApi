using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LSAApi.Controllers
{
    [Route("LSAApi/token")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public TokenController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((200), Type = typeof(JwtSecurityTokenHandler))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult Login([FromBody] UserLoginDto userLoginDto)
        {
            if (string.IsNullOrEmpty(userLoginDto.UserLogin) || string.IsNullOrEmpty(userLoginDto.UserPassword))
            {
                return BadRequest();
            }

            var user = _userRepository.UserLogin(userLoginDto.UserLogin, userLoginDto.UserPassword);

            if (user == null)
            {
                return Forbid();
            }

            var claims = new[] {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("UserLogin", user.UserLogin),
                        new Claim(ClaimTypes.Role, user.RoleId.ToString())
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(20),
                        signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        
    }
}
