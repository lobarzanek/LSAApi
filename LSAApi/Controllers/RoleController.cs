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
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetRoleDto>))]
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
        [Authorize]
        [ProducesResponseType(200, Type = typeof(GetRoleDto))]
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
        [Authorize(Roles = "1")]
        [ProducesResponseType((201), Type = typeof(GetRoleDto))]
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
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetRoleDto>(roleMap));
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        [ProducesResponseType((200), Type = typeof(GetRoleDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateRole([FromBody] UpdateRoleDto updateRole)
        {

            if (updateRole == null)
            {
                return BadRequest(ModelState);
            }

            if (!_roleRepository.IsExist(updateRole.RoleId))
            {
                return NotFound("Role not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roleMap = _mapper.Map<Role>(updateRole);

            if (!_roleRepository.UpdateRole(roleMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }


            return Ok(_mapper.Map<GetRoleDto>(roleMap));
        }

        [HttpDelete("{roleId}")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteRole(int roleId)
        {
            if (!_roleRepository.IsExist(roleId))
            {
                return NotFound("Role not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = _roleRepository.GetRole(roleId);

            if (!_roleRepository.DeleteRole(role))
            {
                ModelState.AddModelError("", "Something went wrong deleting role");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
