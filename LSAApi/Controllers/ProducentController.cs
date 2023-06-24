﻿using AutoMapper;
using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using LSAApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LSAApi.Controllers
{
    [Route("LSAApi/[controller]")]
    [ApiController]
    public class ProducentController : Controller
    {
        private readonly IProducentRepository _producentRepository;
        private readonly IMapper _mapper;

        public ProducentController(IProducentRepository producentRepository, IMapper mapper)
        {
            _producentRepository = producentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Producent>))]
        [ProducesResponseType(400)]
        public IActionResult GetProducents()
        {
            var producents = _mapper.Map<List<GetProducentDto>>(_producentRepository.GetProducents());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(producents);
        }
        [HttpGet("{producentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Producent>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetProducent(int producentId)
        {
            if(!_producentRepository.IsExist(producentId))
            {
                return NotFound("Producent not found");
            }

            var producent = _mapper.Map<GetProducentDto>(_producentRepository.GetProducent(producentId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(producent);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateProducent([FromBody] CreateProducentDto newProducent)
        {
            if (newProducent == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producentMap = _mapper.Map<Producent>(newProducent);

            if (!_producentRepository.CreateProducent(producentMap))
            {
                ModelState.AddModelError("", "Something went wrong whle saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetProducentDto>(producentMap));
        }
    }
}