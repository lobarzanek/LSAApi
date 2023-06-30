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
    public class ConfigurationVlanController : Controller
    {
        private readonly IConfigurationVlanRepository _configurationVlanRepository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IVlanRepository _vlanRepository;
        private readonly IMapper _mapper;

        public ConfigurationVlanController(IConfigurationVlanRepository configurationVlanRepository, IConfigurationRepository configurationRepository,
            IVlanRepository vlanRepository, IMapper mapper)
        {
            _configurationVlanRepository = configurationVlanRepository;
            _configurationRepository = configurationRepository;
            _vlanRepository = vlanRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetConfigurationVlanDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetConfigurationVlans()
        {
            var configurationVlans = _mapper.Map<List<GetConfigurationVlanDto>>(_configurationVlanRepository.GetConfigurationVlans());
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(configurationVlans);
        }

        [HttpGet("{configVlanId}")]
        [Authorize]
        [ProducesResponseType((200), Type = typeof(GetConfigurationVlanDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetConfigurationVlan(int configVlanId)
        {
            if (!_configurationVlanRepository.IsExist(configVlanId))
            {
                return NotFound("ConfigVlan not found");
            }

            var configurationVlan = _mapper.Map<GetConfigurationVlanDto>(_configurationVlanRepository.GetConfigurationVlan(configVlanId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(configurationVlan);
        }

        [HttpGet("configuration/{configurationId}")]
        [Authorize]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetConfigurationVlanDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetConfigurationVlansByConfig(int configurationId)
        {
            if (!_configurationRepository.IsExist(configurationId))
            {
                return NotFound("Configuration not found");
            }

            var configurationVlans = _mapper.Map<List<GetConfigurationVlanDto>>(_configurationVlanRepository.GetConfigurationVlansByConfig(configurationId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(configurationVlans);
        }

        [HttpPost]
        [Authorize(Roles = "1,2")]
        [ProducesResponseType((201), Type = typeof(GetConfigurationVlanDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateConfigurationVlan([FromBody] CreateConfigurationVlanDto newConfigurationVlan)
        {
            if(newConfigurationVlan == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_configurationRepository.IsExist(newConfigurationVlan.ConfigurationId))
            {
                return NotFound("Configuration not found");
            }

            if (!_vlanRepository.IsExist(newConfigurationVlan.VlanId))
            {
                return NotFound("Vlan not found");
            }

            var configurationVlanMap = _mapper.Map<ConfigurationVlan>(newConfigurationVlan);

            if (!_configurationVlanRepository.CreateConfigurationVlan(configurationVlanMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }


            return Created("", _mapper.Map<GetConfigurationVlanDto>(configurationVlanMap));
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        [ProducesResponseType((200), Type = typeof(GetConfigurationVlanDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateConfigurationVlan([FromBody] UpdateConfigurationVlanDto updateConfigurationVlan)
        {

            if (updateConfigurationVlan == null)
            {
                return BadRequest(ModelState);
            }

            if (!_configurationVlanRepository.IsExist(updateConfigurationVlan.ConfigurationVlanId))
            {
                return NotFound("Configuration Vlan not found");
            }
            
            if (!_configurationRepository.IsExist(updateConfigurationVlan.ConfigurationId))
            {
                return NotFound("Configuration not found");
            }
            
            if (!_vlanRepository.IsExist(updateConfigurationVlan.VlanId))
            {
                return NotFound("Vlan not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configurationVlanMap = _mapper.Map<ConfigurationVlan>(updateConfigurationVlan);

            if (!_configurationVlanRepository.UpdateConfigurationVlan(configurationVlanMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }


            return Ok(_mapper.Map<GetConfigurationVlanDto>(configurationVlanMap));
        }

        [HttpDelete("{configurationVlanId}")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteConfigurationVlan(int configurationVlanId)
        {
            if (!_configurationVlanRepository.IsExist(configurationVlanId))
            {
                return NotFound("Configuration Vlan not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configurationVlan = _configurationVlanRepository.GetConfigurationVlan(configurationVlanId);

            if (!_configurationVlanRepository.DeleteConfigurationVlan(configurationVlan))
            {
                ModelState.AddModelError("", "Something went wrong deleting configuration vlan");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
