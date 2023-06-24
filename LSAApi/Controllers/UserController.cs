using AutoMapper;
using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using LSAApi.Repository;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType((200), Type = typeof(IEnumerable<User>))]
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
        [ProducesResponseType(200, Type = typeof(ConfigStatus))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetConfigStatus(int userId)
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
        [ProducesResponseType((200), Type = typeof(IEnumerable<User>))]
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
        [ProducesResponseType(201)]
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
    }
}
