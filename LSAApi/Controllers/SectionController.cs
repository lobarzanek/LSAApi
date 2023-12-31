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
    public class SectionController : Controller
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;

        public SectionController(ISectionRepository sectionRepository, IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Section>))]
        [ProducesResponseType(400)]
        public IActionResult GetSections()
        {
            var sections = _mapper.Map<List<GetSectionDto>>(_sectionRepository.GetSections());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(sections);
        }
        [HttpGet("{sectionId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(Section))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetSection(int sectionId)
        {
            if (!_sectionRepository.IsExist(sectionId))
            {
                return NotFound("Section not found");
            }

            var section = _mapper.Map<GetSectionDto>(_sectionRepository.GetSection(sectionId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(section);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType((201), Type = typeof(GetSectionDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateSection([FromBody] CreateSectionDto newSection)
        {
            if (newSection == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sectionMap = _mapper.Map<Section>(newSection);

            if (!_sectionRepository.CreateSection(sectionMap))
            {
                ModelState.AddModelError("", "Something went wrong whle saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetSectionDto>(sectionMap));
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType((200), Type = typeof(GetSectionDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSection([FromBody] UpdateSectionDto updateSection)
        {

            if (updateSection == null)
            {
                return BadRequest(ModelState);
            }

            if (!_sectionRepository.IsExist(updateSection.SectionId))
            {
                return NotFound("Section not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sectionMap = _mapper.Map<Section>(updateSection);

            if (!_sectionRepository.UpdateSection(sectionMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }


            return Ok(_mapper.Map<GetSectionDto>(sectionMap));
        }

        [HttpDelete("{sectionId}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteSection(int sectionId)
        {
            if (!_sectionRepository.IsExist(sectionId))
            {
                return NotFound("Section not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var section = _sectionRepository.GetSection(sectionId);

            if (!_sectionRepository.DeleteSection(section))
            {
                ModelState.AddModelError("", "Something went wrong deleting section");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
