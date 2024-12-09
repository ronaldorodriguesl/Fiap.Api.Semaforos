using Xunit;
using Moq;
using AutoMapper;
using Fiap.Api.Semaforos.Controllers;
using Fiap.Api.Semaforos.Models;
using Fiap.Api.Semaforos.Services;
using Fiap.Api.Semaforos.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Semaforos.Tests
{
    public class SemaforoControllerTests
    {
        private readonly Mock<ISemaforoService> _serviceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SemaforoController _controller;

        public SemaforoControllerTests()
        {
            _serviceMock = new Mock<ISemaforoService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new SemaforoController(_serviceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Get_DeveRetornarOkComListaDeDados()
        {
            // Arrange
            var semaforos = new List<SemaforoModel>
            {
                new SemaforoModel { SemaforoId = 1, Luz = "Verde", Logradouro = "Rua A", AtualizadoEm = DateTime.UtcNow },
                new SemaforoModel { SemaforoId = 2, Luz = "Vermelho", Logradouro = "Rua B", AtualizadoEm = DateTime.UtcNow }
            };

            var semaforoViewModels = semaforos.Select(s => new SemaforoViewModel
            {
                SemaforoId = s.SemaforoId,
                Luz = s.Luz,
                Logradouro = s.Logradouro,
                AtualizadoEm = s.AtualizadoEm
            });

            _serviceMock.Setup(service => service.ListarSemaforos(It.IsAny<int>(), It.IsAny<int>()))
                        .Returns(semaforos);

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<SemaforoViewModel>>(semaforos))
                       .Returns(semaforoViewModels);

            // Act
            var result = _controller.Get(1, 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<SemaforoPaginacaoViewModel>(okResult.Value);

            Assert.Equal(2, returnValue.Semaforos.Count());
            Assert.Equal(1, returnValue.CurrentPage);
            Assert.Equal(10, returnValue.PageSize);
        }

        [Fact]
        public void Get_DeveRetornarOkComListaVazia()
        {
            // Arrange
            var semaforos = new List<SemaforoModel>();

            _serviceMock.Setup(service => service.ListarSemaforos(It.IsAny<int>(), It.IsAny<int>()))
                        .Returns(semaforos);

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<SemaforoViewModel>>(semaforos))
                       .Returns(new List<SemaforoViewModel>());

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<SemaforoPaginacaoViewModel>(okResult.Value);

            Assert.Empty(returnValue.Semaforos);
            Assert.Equal(1, returnValue.CurrentPage);
            Assert.Equal(10, returnValue.PageSize);
        }
    }
}
