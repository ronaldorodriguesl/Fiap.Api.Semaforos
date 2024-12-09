using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Fiap.Api.Semaforos.Controllers;
using Fiap.Api.Semaforos.Services;
using Fiap.Api.Semaforos.ViewModel;
using AutoMapper;
using System.Collections.Generic;
using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Tests
{
    public class CondicaoClimaticaControllerTests
    {
        private readonly Mock<ICondicaoClimaticaService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CondicaoClimaticaController _controller;

        public CondicaoClimaticaControllerTests()
        {
            _serviceMock = new Mock<ICondicaoClimaticaService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CondicaoClimaticaController(_serviceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Get_CondicaoClimaticaExistente_DeveRetornarOkComDados()
        {
            // Arrange
            var id = 1;
            var semaforoMock = new SemaforoModel
            {
                SemaforoId = 123,
                Luz = "Verde",
                Logradouro = "Av. Central",
                AtualizadoEm = DateTime.UtcNow
            };

            var condicaoMock = new CondicaoClimaticaModel
            {
                CondicaoClimaticaId = id,
                Tempo = "Ensolarado",
                Temperatura = 22.0,
                AtualizadoEm = DateTime.UtcNow,
                SemaforoId = 123,
                Semaforo = semaforoMock
            };

            var viewModelMock = new CondicaoClimaticaViewModel
            {
                CondicaoClimaticaId = id,
                Tempo = "Ensolarado",
                Temperatura = 22.0,
                AtualizadoEm = DateTime.UtcNow,
                SemaforoId = 123
            };

            _serviceMock.Setup(s => s.ObterCondicaoClimaticaPorId(id)).Returns(condicaoMock);
            _mapperMock.Setup(m => m.Map<CondicaoClimaticaViewModel>(condicaoMock)).Returns(viewModelMock);

            // Act
            var result = _controller.Get(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(viewModelMock, okResult.Value);
        }

        [Fact]
        public void Get_CondicaoClimaticaInexistente_DeveRetornarNotFound()
        {
            // Arrange
            var id = 999; 
            _serviceMock.Setup(s => s.ObterCondicaoClimaticaPorId(id)).Returns((CondicaoClimaticaModel)null);

            // Act
            var result = _controller.Get(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }


        [Fact]
        public void Get_Paginacao_DeveRetornarOkComListaDeDados()
        {
            // Arrange
            var pagina = 1;
            var tamanho = 10;
            var condicoesMock = new List<CondicaoClimaticaModel>
            {
                new CondicaoClimaticaModel
                {
                    CondicaoClimaticaId = 1,
                    Tempo = "Ensolarado",
                    Temperatura = 25,
                    AtualizadoEm = DateTime.UtcNow,
                    SemaforoId = 123,
                    Semaforo = new SemaforoModel
                    {
                        SemaforoId = 123,
                        Luz = "Verde",
                        Logradouro = "Rua A",
                        AtualizadoEm = DateTime.UtcNow
                    }
                },
                new CondicaoClimaticaModel
                {
                    CondicaoClimaticaId = 2,
                    Tempo = "Chuvoso",
                    Temperatura = 18,
                    AtualizadoEm = DateTime.UtcNow,
                    SemaforoId = 124,
                    Semaforo = new SemaforoModel
                    {
                        SemaforoId = 124,
                        Luz = "Vermelho",
                        Logradouro = "Rua B",
                        AtualizadoEm = DateTime.UtcNow
                    }
                }
            };

            var viewModelMock = new List<CondicaoClimaticaViewModel>
            {
                new CondicaoClimaticaViewModel
                {
                    CondicaoClimaticaId = 1,
                    Tempo = "Ensolarado",
                    Temperatura = 25,
                    AtualizadoEm = DateTime.UtcNow,
                    SemaforoId = 123,
                    SemaforoLogradouro = "Rua A",
                    SemaforoLuz = "Verde"
                },
                new CondicaoClimaticaViewModel
                {
                    CondicaoClimaticaId = 2,
                    Tempo = "Chuvoso",
                    Temperatura = 18,
                    AtualizadoEm = DateTime.UtcNow,
                    SemaforoId = 124,
                    SemaforoLogradouro = "Rua B",
                    SemaforoLuz = "Vermelho"
                }
            };

            _serviceMock.Setup(s => s.ListarCondicoes(pagina, tamanho)).Returns(condicoesMock);
            _mapperMock.Setup(m => m.Map<IEnumerable<CondicaoClimaticaViewModel>>(condicoesMock)).Returns(viewModelMock);

            // Act
            var result = _controller.Get(pagina, tamanho);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedValue = Assert.IsType<CondicaoClimaticaPaginacaoViewModel>(okResult.Value);
            Assert.NotNull(returnedValue);
            Assert.Equal(2, returnedValue.CondicoesClimaticas.Count());
            Assert.Equal(pagina, returnedValue.CurrentPage);
            Assert.Equal(tamanho, returnedValue.PageSize);
        }
    }
}
