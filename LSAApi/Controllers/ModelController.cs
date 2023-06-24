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
    public class ModelController : Controller
    {
        private readonly IModelRepository _modelRepository;
        private readonly IProducentRepository _producentRepository;
        private readonly IMapper _mapper;

        public ModelController(IModelRepository modelRepository, IProducentRepository producentRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _producentRepository = producentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<Model>))]
        [ProducesResponseType(400)]
        public IActionResult GetModels()
        {
            var models = _mapper.Map<List<GetModelDto>>(_modelRepository.GetModels());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(models);
        }

        [HttpGet("{modelId}")]
        [ProducesResponseType((200), Type = typeof(Model))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetModel(int modelId)
        {
            if(!_modelRepository.IsExist(modelId))
            {
                return NotFound("Model not found");
            }

            var model = _mapper.Map<GetModelDto>(_modelRepository.GetModel(modelId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(model);
        }

        [HttpGet("producent/{producentId}")]
        [ProducesResponseType((200), Type = typeof(IEnumerable<Model>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetModelsByProducent(int producentId)
        {
            if (!_producentRepository.IsExist(producentId))
            {
                return NotFound("Producent not found");
            }

            var models = _mapper.Map<List<GetModelDto>>(_modelRepository.GetModelsByProducent(producentId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(models);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateModel([FromBody] CreateModelDto newModel)
        {
            if (newModel == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_producentRepository.IsExist(newModel.ProducentId))
            {
                return NotFound("Producent not found");
            }

            var modelMap = _mapper.Map<Model>(newModel);

            if (!_modelRepository.CreateModel(modelMap))
            {
                ModelState.AddModelError("", "Something went wrong whle saving");
                return StatusCode(500, ModelState);
            }

            return Created("", _mapper.Map<GetModelDto>(modelMap));
        }

    }
}
