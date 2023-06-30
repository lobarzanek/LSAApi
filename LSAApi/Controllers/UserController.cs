using AutoMapper;
using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using LSAApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LSAApi.Controllers
{
    [Route("LSAApi/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetUserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<GetUserDto>>(_userRepository.GetUsers());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(GetUserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(int userId)
        {
            if (!_userRepository.IsExist(userId))
            {
                return NotFound("User not found");
            }

            var user = _mapper.Map<GetUserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpGet("role/{roleId}")]
        [Authorize]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetUserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersByRole(int roleId)
        {
            if (!_roleRepository.IsExist(roleId))
            {
                return NotFound("Role not found");
            }

            var users = _mapper.Map<List<GetUserDto>>(_userRepository.GetUsersByRole(roleId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        [ProducesResponseType((201), Type = typeof(GetUserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateUser([FromBody] CreateUserDto newUser)
        {
            if (newUser == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_roleRepository.IsExist(newUser.RoleId))
            {
                return NotFound("Role not found");
            }

            var userMap = _mapper.Map<User>(newUser);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong whle saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetUserDto>(userMap));
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        [ProducesResponseType((200), Type = typeof(GetUserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateUser([FromBody] UpdateUserDto updateUser)
        {

            if (updateUser == null)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.IsExist(updateUser.UserId))
            {
                return NotFound("User not found");
            }

            if (!_roleRepository.IsExist(updateUser.RoleId))
            {
                return NotFound("Role not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(updateUser.UserId);

            user.UserName = updateUser.UserName;
            user.RoleId = updateUser.RoleId;

            if (!_userRepository.UpdateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }


            return Ok(_mapper.Map<GetUserDto>(user));
        }

        [HttpPut("password")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateUserPassword([FromBody] UpdateUserPasswordDto updateUserPassword)
        {

            if (updateUserPassword == null)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.IsExist(updateUserPassword.UserId))
            {
                return NotFound("User not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(updateUserPassword.UserId);

            if (updateUserPassword.OldUserPassword != user.UserPassword)
            {
                return Ok("Wrong password");
            }

            user.UserPassword = updateUserPassword.NewUserPassword;

            if (!_userRepository.UpdateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }


            return NoContent();
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.IsExist(userId))
            {
                return NotFound("User not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(userId);

            if (!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
