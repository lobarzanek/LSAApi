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
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetSwitchStatusDto>))]
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
        [Authorize]
        [ProducesResponseType(200, Type = typeof(GetSwitchStatusDto))]
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
        [Authorize(Roles = "1")]
        [ProducesResponseType((201), Type = typeof(GetSwitchStatusDto))]
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
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetSwitchStatusDto>(switchStatusMap));
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        [ProducesResponseType((200), Type = typeof(GetConfigStatusDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSwitchStatus([FromBody] UpdateSwitchStatusDto updateSwitchStatus)
        {

            if (updateSwitchStatus == null)
            {
                return BadRequest(ModelState);
            }

            if (!_switchStatusRepository.IsExist(updateSwitchStatus.SwitchStatusId))
            {
                return NotFound("Status not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var switchStatusMap = _mapper.Map<SwitchStatus>(updateSwitchStatus);

            if (!_switchStatusRepository.UpdateSwitchStatus(switchStatusMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }


            return Ok(_mapper.Map<GetSwitchStatusDto>(switchStatusMap));
        }

        [HttpDelete("{switchStatusId}")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteSwitchStatus(int switchStatusId)
        {
            if (!_switchStatusRepository.IsExist(switchStatusId))
            {
                return NotFound("Status not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var switchstatus = _switchStatusRepository.GetSwitchStatus(switchStatusId);

            if (!_switchStatusRepository.DeleteSwitchStatus(switchstatus))
            {
                ModelState.AddModelError("", "Something went wrong deleting status");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
