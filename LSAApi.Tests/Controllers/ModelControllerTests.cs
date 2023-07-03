using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LSAApi.Controllers;
using LSAApi.Dto;
using LSAApi.Interfaces;
using LSAApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LSAApi.Tests.Controllers
{
    public class ModelControllerTests
    {
        private readonly IModelRepository _modelRepository;
        private readonly IProducentRepository _producentRepository;
        private readonly IMapper _mapper;
        private readonly ModelController _modelController;

        public ModelControllerTests()
        {
            _modelRepository = A.Fake<IModelRepository>();
            _producentRepository = A.Fake<IProducentRepository>();
            _mapper = A.Fake<IMapper>();
            _modelController = new ModelController(_modelRepository, _producentRepository, _mapper);
        }

        [Fact]
        public void ModelController_GetModels_ReturnsOK()
        {
            //Arrange
            var models = A.Fake<List<GetModelDto>>();

            A.CallTo(() => _mapper.Map<List<GetModelDto>>(_modelRepository.GetModels())).Returns(models);

            //Act
            var result = _modelController.GetModels();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ModelController_GetModel_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var model = A.Fake<GetModelDto>();

            A.CallTo(() => _modelRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<GetModelDto>(_modelRepository.GetModel(id))).Returns(model);

            //Act
            var result = _modelController.GetModel(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ModelController_GetModelsByProducent_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var models = A.Fake<List<GetModelDto>>();

            A.CallTo(() => _producentRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _mapper.Map<List<GetModelDto>>(_modelRepository.GetModelsByProducent(id))).Returns(models);

            //Act
            var result = _modelController.GetModelsByProducent(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ModelController_CreateModel_ReturnsOK()
        {
            //Arrange
            var newModel = A.Fake<CreateModelDto>();
            var model = A.Fake<Model>();
            var modelDto = A.Fake<GetModelDto>();

            A.CallTo(() => _producentRepository.IsExist(newModel.ProducentId)).Returns(true);
            A.CallTo(() => _mapper.Map<Model>(newModel)).Returns(model);
            A.CallTo(() => _modelRepository.CreateModel(model)).Returns(true);

            //Act
            var result = _modelController.CreateModel(newModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public void ModelController_UpdateModel_ReturnsOK()
        {
            //Arrange
            var updateModel = A.Fake<UpdateModelDto>();
            var model = A.Fake<Model>();
            var modelDto = A.Fake<GetModelDto>();

            A.CallTo(() => _modelRepository.IsExist(updateModel.ModelId)).Returns(true);
            A.CallTo(() => _producentRepository.IsExist(updateModel.ProducentId)).Returns(true);
            A.CallTo(() => _mapper.Map<Model>(updateModel)).Returns(model);
            A.CallTo(() => _modelRepository.UpdateModel(model)).Returns(true);

            //Act
            var result = _modelController.UpdateModel(updateModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ModelController_DeleteModel_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var model = A.Fake<Model>();

            A.CallTo(() => _modelRepository.IsExist(id)).Returns(true);
            A.CallTo(() => _modelRepository.GetModel(id)).Returns(model);
            A.CallTo(() => _modelRepository.DeleteModel(model)).Returns(true);

            //Act
            var result = _modelController.DeleteModel(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
