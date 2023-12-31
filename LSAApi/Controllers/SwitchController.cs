﻿using AutoMapper;
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
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetSwitchDto>))]
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
        [Authorize]
        [ProducesResponseType(200, Type = typeof(GetSwitchDto))]
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
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetSwitchDto>))]
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
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetSwitchDto>))]
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
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetSwitchDto>))]
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
        [Authorize(Roles = "1,2")]
        [ProducesResponseType(200, Type = typeof(GetSwitchCredentialsDto))]
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
        [Authorize]
        [ProducesResponseType((201), Type = typeof(GetSwitchDto))]
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
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetSwitchDto>(switchMap));
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType((200), Type = typeof(GetSwitchDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSwitch([FromBody] UpdateSwitchDto updateSwitch)
        {

            if (updateSwitch == null)
            {
                return BadRequest(ModelState);
            }

            if (!_switchRepository.IsExist(updateSwitch.SwitchId))
            {
                return NotFound("Switch not found");
            }
            
            if (!_modelRepository.IsExist(updateSwitch.ModelId))
            {
                return NotFound("Model not found");
            }
            
            if (!_switchStatusRepository.IsExist(updateSwitch.SwitchStatusId))
            {
                return NotFound("Status not found");
            }
            
            if (updateSwitch.SectionId != null && !_sectionRepository.IsExist(updateSwitch.SectionId.Value))
            {
                return NotFound("Section not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var switchMap = _mapper.Map<Switch>(updateSwitch);

            var ethSwitch = _switchRepository.GetSwitch(switchMap.SwitchId);

            switchMap.SwitchLogin = ethSwitch.SwitchLogin;
            switchMap.SwitchPassword = ethSwitch.SwitchPassword;

            if (!_switchRepository.UpdateSwitch(switchMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }


            return Ok(_mapper.Map<GetSwitchDto>(switchMap));
        }
        [HttpPut("credentials")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSwitchCredentials([FromBody] UpdateSwitchCredentialsDto updateSwitchCr)
        {

            if (updateSwitchCr == null)
            {
                return BadRequest(ModelState);
            }

            if (!_switchRepository.IsExist(updateSwitchCr.SwitchId))
            {
                return NotFound("Switch not found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ethSwitch = _switchRepository.GetSwitch(updateSwitchCr.SwitchId);

            ethSwitch.SwitchLogin = updateSwitchCr.SwitchLogin;
            ethSwitch.SwitchPassword = updateSwitchCr.SwitchPassword;

            if (!_switchRepository.UpdateSwitch(ethSwitch))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }


            return Ok();
        }

        [HttpDelete("{switchId}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteSwitch(int switchId)
        {
            if (!_switchRepository.IsExist(switchId))
            {
                return NotFound("Switch not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ethSwitch = _switchRepository.GetSwitch(switchId);

            if (!_switchRepository.DeleteSwitch(ethSwitch))
            {
                ModelState.AddModelError("", "Something went wrong deleting switch");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
