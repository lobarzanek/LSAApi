using AutoMapper;
using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType((200), Type = typeof(IEnumerable<ConfigurationVlan>))]
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
        [ProducesResponseType((200), Type = typeof(ConfigurationVlan))]
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
        [ProducesResponseType((200), Type = typeof(IEnumerable<ConfigurationVlan>))]
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
        [ProducesResponseType(201)]
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
                ModelState.AddModelError("", "Something went wrong whle saving");
                return StatusCode(500, ModelState);
            }


            return Created("", _mapper.Map<GetConfigurationVlanDto>(configurationVlanMap));
        }
    }
}
