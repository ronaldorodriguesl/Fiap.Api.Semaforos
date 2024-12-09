using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Fiap.Api.Semaforos.Controllers;
using Fiap.Api.Semaforos.Services;
using AutoMapper;
using Fiap.Api.Semaforos.ViewModel;
using Fiap.Api.Semaforos.Models;
using System;
using Microsoft.AspNetCore.Http;

namespace Fiap.Api.Semaforos.Tests
{
    public class FluxoVeiculoControllerTests
    {
        private readonly Mock<IFluxoVeiculoService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly FluxoVeiculoController _controller;

        public FluxoVeiculoControllerTests()
        {
            _serviceMock = new Mock<IFluxoVeiculoService>();
            _mapperMock = new Mock<IMapper>();

            _controller = new FluxoVeiculoController(_serviceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Post_DeveRetornarCreatedComSucesso()
        {
            // Arrange
            var fluxoVeiculoViewModel = new FluxoVeiculoViewModel
            {
                FluxoVeiculoId = 1,
                Quantidade = 5,
                SemaforoId = 123,
                SemaforoLogradouro = "Rua A",
                SemaforoLuz = "Verde",
                AtualizadoEm = DateTime.UtcNow,
            };

            var fluxoVeiculoModel = new FluxoVeiculoModel
            {
                FluxoVeiculoId = 1,
                Quantidade = 5,
                SemaforoId = 123,
                Semaforo = new SemaforoModel
                {
                    SemaforoId = 123,
                    Logradouro = "Rua A",
                    Luz = "Verde"
                },
                AtualizadoEm = DateTime.UtcNow,
            };

            _mapperMock.Setup(m => m.Map<FluxoVeiculoModel>(It.IsAny<FluxoVeiculoViewModel>())).Returns(fluxoVeiculoModel);
            _serviceMock.Setup(s => s.AdicionarFluxoVeiculo(It.IsAny<FluxoVeiculoModel>()));

            // Act
            var result = _controller.Post(fluxoVeiculoViewModel);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(FluxoVeiculoController.GetFluxoVeiculoById), createdResult.ActionName);
            Assert.Equal(fluxoVeiculoViewModel, createdResult.Value);
        }

        [Fact]
        public void Post_DeveRetornarBadRequestEmCasoDeErro()
        {
            // Arrange
            var fluxoVeiculoViewModel = new FluxoVeiculoViewModel
            {
                FluxoVeiculoId = 1,
                Quantidade = 5,
                SemaforoId = 123,
                SemaforoLogradouro = "Rua A",
                SemaforoLuz = "Verde",
                AtualizadoEm = DateTime.UtcNow,
            };

            var fluxoVeiculoModel = new FluxoVeiculoModel
            {
                FluxoVeiculoId = 1,
                Quantidade = 5,
                SemaforoId = 123,
                Semaforo = new SemaforoModel
                {
                    SemaforoId = 123,
                    Logradouro = "Rua A",
                    Luz = "Verde"
                },
                AtualizadoEm = DateTime.UtcNow,
            };

            _mapperMock.Setup(m => m.Map<FluxoVeiculoModel>(It.IsAny<FluxoVeiculoViewModel>())).Returns(fluxoVeiculoModel);
            _serviceMock.Setup(s => s.AdicionarFluxoVeiculo(It.IsAny<FluxoVeiculoModel>())).Throws(new ArgumentException("Erro"));

            // Act
            var result = _controller.Post(fluxoVeiculoViewModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Erro", badRequestResult.Value);
        }

        [Fact]
        public void GetFluxoVeiculoById_DeveRetornarOkComDadosQuandoEncontrado()
        {
            // Arrange
            var id = 1;

            var fluxoVeiculoModel = new FluxoVeiculoModel
            {
                FluxoVeiculoId = 1,
                Quantidade = 5,
                SemaforoId = 123,
                Semaforo = new SemaforoModel
                {
                    SemaforoId = 123,
                    Logradouro = "Rua A",
                    Luz = "Verde"
                },
                AtualizadoEm = DateTime.UtcNow,
            };

            var fluxoVeiculoViewModel = new FluxoVeiculoViewModel
            {
                FluxoVeiculoId = 1,
                Quantidade = 5,
                SemaforoId = 123,
                SemaforoLogradouro = "Rua A",
                SemaforoLuz = "Verde",
                AtualizadoEm = DateTime.UtcNow,
            };

            _serviceMock.Setup(s => s.ObterFluxoVeiculoPorIdComDetalhes(id)).Returns(fluxoVeiculoModel);
            _mapperMock.Setup(m => m.Map<FluxoVeiculoViewModel>(fluxoVeiculoModel)).Returns(fluxoVeiculoViewModel);

            // Act
            var result = _controller.GetFluxoVeiculoById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedValue = Assert.IsType<FluxoVeiculoViewModel>(okResult.Value);

            Assert.NotNull(returnedValue);
            Assert.Equal(5, returnedValue.Quantidade);
        }

        [Fact]
        public void GetFluxoVeiculoById_DeveRetornarNotFoundQuandoNaoEncontrado()
        {
            // Arrange
            var id = 999;
            _serviceMock.Setup(s => s.ObterFluxoVeiculoPorIdComDetalhes(id)).Returns((FluxoVeiculoModel)null);

            // Act
            var result = _controller.GetFluxoVeiculoById(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    }
}
