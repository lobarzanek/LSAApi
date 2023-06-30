using AutoMapper;
using LSAApi.Authorization;
using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LSAApi.Controllers
{
    [Route("LSAApi/[controller]")]
    [ApiController]
    public class ConfigStatusController : Controller
    {
        private readonly IConfigStatusRepository _configStatusRepository;
        private readonly IMapper _mapper;
        

        public ConfigStatusController(IConfigStatusRepository configStatusRepository, IMapper mapper)
        {
            _configStatusRepository = configStatusRepository;
            _mapper = mapper;
        }
        

        [HttpGet]
        [Authorize]
        [ProducesResponseType((200), Type = typeof(GetConfigStatusDto))]
        [ProducesResponseType(400)]        
        public IActionResult GetConfigStatuses()
        {
            var configStatuses = _mapper.Map<List<GetConfigStatusDto>>(_configStatusRepository.GetConfigStatuses());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(configStatuses);
        }

        [HttpGet("{configStatusId}")]
        [Authorize]
        [ProducesResponseType((200), Type = typeof(GetConfigStatusDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetConfigStatus(int configStatusId)
        {
            if (!_configStatusRepository.IsExist(configStatusId))
            {
                return NotFound("Status not found");
            }

            var configStatus = _mapper.Map<GetConfigStatusDto>(_configStatusRepository.GetConfigStatus(configStatusId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(configStatus);
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        [ProducesResponseType((201), Type = typeof(GetConfigStatusDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateConfigStatus([FromBody] CreateConfigStatusDto newConfigStatus)
        {

            if (newConfigStatus == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configStatusMap = _mapper.Map<ConfigStatus>(newConfigStatus);

            if (!_configStatusRepository.CreateConfigStatus(configStatusMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetConfigStatusDto>(configStatusMap));
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        [ProducesResponseType((200), Type = typeof(GetConfigStatusDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateConfigStatus([FromBody] UpdateConfigStatusDto updateConfigStatus)
        {

            if (updateConfigStatus == null)
            {
                return BadRequest(ModelState);
            }

            if (!_configStatusRepository.IsExist(updateConfigStatus.ConfigStatusId))
            {
                return NotFound("Status not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configStatusMap = _mapper.Map<ConfigStatus>(updateConfigStatus);

            if (!_configStatusRepository.UpdateConfigStatus(configStatusMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            
            return Ok(_mapper.Map<GetConfigStatusDto>(configStatusMap));
        }

        [HttpDelete("{configStatusId}")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteConfigStatus(int configStatusId)
        {
            if (!_configStatusRepository.IsExist(configStatusId))
            {
                return NotFound("Status not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configStatus = _configStatusRepository.GetConfigStatus(configStatusId);

            if (!_configStatusRepository.DeleteConfigStatus(configStatus))
            {
                ModelState.AddModelError("", "Something went wrong deleting status");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
