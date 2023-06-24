using AutoMapper;
using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ConfigStatus>))]
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
        [ProducesResponseType(200, Type = typeof(ConfigStatus))]
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
        [ProducesResponseType(201)]
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
                ModelState.AddModelError("", "Something went wrong whle saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetConfigStatusDto>(configStatusMap));
        }
    }
}
