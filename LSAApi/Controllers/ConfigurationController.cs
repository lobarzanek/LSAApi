using AutoMapper;
using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LSAApi.Controllers
{
    [Route("LSAApi/[controller]")]
    [ApiController]
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ISwitchRepository _switchRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISwitchStatusRepository _switchStatusRepository;
        private readonly IMapper _mapper;

        public ConfigurationController(IConfigurationRepository configurationRepository, ISwitchRepository switchRepository, 
            IUserRepository userRepository, ISwitchStatusRepository switchStatusRepository, IMapper mapper)
        {
            _configurationRepository = configurationRepository;
            _switchRepository = switchRepository;
            _userRepository = userRepository;
            _switchStatusRepository = switchStatusRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Configuration>))]
        [ProducesResponseType(400)]
        public IActionResult GetConfigurations()
        {
            var configurations = _mapper.Map<List<GetConfigurationDto>>(_configurationRepository.GetAllConfigurations());
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(configurations);
        }
        [HttpGet("{configurationId}")]
        [ProducesResponseType((200), Type = typeof(Configuration))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetConfiguration(int configurationId) 
        {
            if (!_configurationRepository.IsExist(configurationId))
            {
                return NotFound("Configuration not found");
            }

            var configuration = _mapper.Map<GetConfigurationDto>(_configurationRepository.GetConfigurationById(configurationId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(configuration);
        }

        [HttpGet("switch/{switchId}")]
        [ProducesResponseType((200), Type = typeof(IEnumerable<Configuration>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetConfigurationsBySwitch(int switchId)
        {
            
            if (!_switchRepository.IsExist(switchId))
            {
                return NotFound("Switch not found");
            }

            var configurations = _mapper.Map<List<GetConfigurationDto>>(_configurationRepository.GetConfigurationsBySwitch(switchId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(configurations);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType((200), Type = typeof(IEnumerable<Configuration>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetConfigurationsByUser(int userId)
        {
            if (!_userRepository.IsExist(userId))
            {
                return NotFound("User not found");
            }

            var configurations = _mapper.Map<List<GetConfigurationDto>>(_configurationRepository.GetConfigurationsByUser(userId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(configurations);
        }

        [HttpGet("status/{statusId}")]
        [ProducesResponseType((200), Type = typeof(IEnumerable<Configuration>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetConfigurationsByStatus(int statusId)
        {
            
            if (!_switchStatusRepository.IsExist(statusId))
            {
                return NotFound("Status not found");
            }

            var configurations = _mapper.Map<List<GetConfigurationDto>>(_configurationRepository.GetConfigurationsByStatus(statusId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(configurations);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateConfiguration([FromBody] CreateConfigurationDto newConfiguration)
        {
            if (newConfiguration == null)
            {
                return BadRequest(ModelState);
            }

            if (!_switchRepository.IsExist(newConfiguration.SwitchId))
            {
                return NotFound("Switch not found");
            }

            if (!_userRepository.IsExist(newConfiguration.UserId))
            {
                return NotFound("User not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configurationMap = _mapper.Map<Configuration>(newConfiguration);
            configurationMap.CreateDate = DateTime.Now;
            configurationMap.ConfigStatusId = 1; //Pending

            if (!_configurationRepository.CreateConfiguration(configurationMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetConfigurationDto>(configurationMap));
        }


    }
}
