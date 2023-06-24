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
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        [ProducesResponseType(400)]
        public IActionResult GetRoles()
        {
            var roles = _mapper.Map<List<GetRoleDto>>(_roleRepository.GetRoles());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetRole(int roleId)
        {
            if (!_roleRepository.IsExist(roleId))
            {
                return NotFound("Role not found");
            }

            var role = _mapper.Map<GetRoleDto>(_roleRepository.GetRole(roleId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(role);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateRole([FromBody] CreateRoleDto newRole)
        {
            if (newRole == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roleMap = _mapper.Map<Role>(newRole);

            if (!_roleRepository.CreateRole(roleMap))
            {
                ModelState.AddModelError("", "Something went wrong whle saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetRoleDto>(roleMap));
        }
    }
}
