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
    public class VlanController : Controller
    {
        private readonly IVlanRepository _vlanRepository;
        private readonly IMapper _mapper;

        public VlanController(IVlanRepository vlanRepository, IMapper mapper)
        {
            _vlanRepository = vlanRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Vlan>))]
        [ProducesResponseType(400)]
        public IActionResult GetVlans()
        {
            var configStatuses = _mapper.Map<List<GetVlanDto>>(_vlanRepository.GetVlans());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(configStatuses);
        }

        [HttpGet("{vlanId}")]
        [ProducesResponseType(200, Type = typeof(Vlan))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetVlan(int vlanId)
        {
            if (!_vlanRepository.IsExist(vlanId))
            {
                return NotFound("Vlan not found");
            }

            var vlan = _mapper.Map<GetVlanDto>(_vlanRepository.GetVlan(vlanId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(vlan);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateVlan([FromBody] CreateVlanDto newVlan)
        {
            if (newVlan == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vlanMap = _mapper.Map<Vlan>(newVlan);

            if (!_vlanRepository.CreateVlan(vlanMap))
            {
                ModelState.AddModelError("", "Something went wrong whle saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetVlanDto>(vlanMap));
        }
    }
}
