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
    public class SwitchController : Controller
    {
        private readonly ISwitchRepository _switchRepository;
        private readonly IModelRepository _modelRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISwitchStatusRepository _switchStatusRepository;
        private readonly IMapper _mapper;

        public SwitchController(ISwitchRepository switchRepository, IModelRepository modelRepository, 
            ISectionRepository sectionRepository, ISwitchStatusRepository switchStatusRepository, IMapper mapper)
        {
            _switchRepository = switchRepository;
            _modelRepository = modelRepository;
            _sectionRepository = sectionRepository;
            _switchStatusRepository = switchStatusRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Switch>))]
        [ProducesResponseType(400)]
        public IActionResult GetSwitches()
        {
            var switches = _mapper.Map<List<GetSwitchDto>>(_switchRepository.GetSwitches());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(switches);
        }

        [HttpGet("{switchId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Switch>))]
        [ProducesResponseType(400)]
        public IActionResult GetSwitch(int switchId)
        {
            if(!_switchRepository.IsExist(switchId))
            {
                return NotFound("Switch not found");
            }

            var ethSwitch = _mapper.Map<GetSwitchDto>(_switchRepository.GetSwitch(switchId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ethSwitch);
        }

        [HttpGet("model/{modelId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Switch>))]
        [ProducesResponseType(400)]
        public IActionResult GetSwitchesByModel(int modelId)
        {
            if (!_modelRepository.IsExist(modelId))
            {
                return NotFound("Model not found");
            }

            var switches = _mapper.Map<List<GetSwitchDto>>(_switchRepository.GetSwitchesByModel(modelId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(switches);
        }

        [HttpGet("status/{statusId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Switch>))]
        [ProducesResponseType(400)]
        public IActionResult GetSwitchesByStatus(int statusId)
        {
            if (!_switchStatusRepository.IsExist(statusId))
            {
                return NotFound("Status not found");
            }

            var switches = _mapper.Map<List<GetSwitchDto>>(_switchRepository.GetSwitchesByStatus(statusId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(switches);
        }

        [HttpGet("section/{sectionId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Switch>))]
        [ProducesResponseType(400)]
        public IActionResult GetSwitchesBySection(int sectionId)
        {
            if (!_sectionRepository.IsExist(sectionId))
            {
                return NotFound("Section not found");
            }

            var switches = _mapper.Map<List<GetSwitchDto>>(_switchRepository.GetSwitchesBySection(sectionId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(switches);
        }

        [HttpGet("credentials/{switchId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Switch>))]
        [ProducesResponseType(400)]
        public IActionResult GetSwitchCredentials(int switchId)
        {
            if (!_switchRepository.IsExist(switchId))
            {
                return NotFound("Switch not found");
            }

            var ethSwitch = _mapper.Map<GetSwitchCredentialsDto>(_switchRepository.GetSwitch(switchId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ethSwitch);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateSwitch([FromBody] CreateSwitchDto newSwitch)
        {
            if (newSwitch == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_modelRepository.IsExist(newSwitch.ModelId))
            {
                return NotFound("Model not found");
            }

            if (newSwitch.SectionId != null && !_sectionRepository.IsExist(newSwitch.SectionId.Value))
            {
                return NotFound("Section not found");
            }

            var switchMap = _mapper.Map<Switch>(newSwitch);

            if (!_switchRepository.CreateSwitch(switchMap))
            {
                ModelState.AddModelError("", "Something went wrong whle saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetSwitchDto>(switchMap));
        }

    }
}
