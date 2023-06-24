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
    public class SwitchStatusController : Controller
    {
        private readonly ISwitchStatusRepository _switchStatusRepository;
        private readonly IMapper _mapper;

        public SwitchStatusController(ISwitchStatusRepository switchStatusRepository, IMapper mapper)
        {
            _switchStatusRepository = switchStatusRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SwitchStatus>))]
        [ProducesResponseType(400)]
        public IActionResult GetConfigStatuses()
        {
            var switchStatuses = _mapper.Map<List<GetSwitchStatusDto>>(_switchStatusRepository.GetSwitchStatuses());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(switchStatuses);
        }

        [HttpGet("{switchStatusId}")]
        [ProducesResponseType(200, Type = typeof(SwitchStatus))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetConfigStatus(int switchStatusId)
        {
            if (!_switchStatusRepository.IsExist(switchStatusId))
            {
                return NotFound("Status not found");
            }

            var switchStatus = _mapper.Map<GetSwitchStatusDto>(_switchStatusRepository.GetSwitchStatus(switchStatusId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(switchStatus);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateSwitchStatus([FromBody] CreateSwitchStatusDto newSwitchstatus)
        {
            if (newSwitchstatus == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var switchStatusMap = _mapper.Map<SwitchStatus>(newSwitchstatus);

            if (!_switchStatusRepository.CreateSwitchStatus(switchStatusMap))
            {
                ModelState.AddModelError("", "Something went wrong whle saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetSwitchStatusDto>(switchStatusMap));
        }
    }
}
