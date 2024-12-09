using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Fiap.Api.Semaforos.Controllers;
using Fiap.Api.Semaforos.Services;
using Fiap.Api.Semaforos.ViewModel;
using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public void Login_UsuarioValido_DeveRetornarOkComToken()
        {
            // Arrange
            var loginRequest = new UsuarioViewModel { Email = "usuario@exemplo.com", Senha = "senha123" };
            var usuarioMock = new UsuarioModel
            {
                Email = "user@example.com",
                Senha = "senha123",
                Role = "user"
            };
            _authServiceMock.Setup(x => x.Autenticar(loginRequest.Email, loginRequest.Senha)).Returns(usuarioMock);
            _authServiceMock.Setup(x => x.GenerateJwtToken(usuarioMock)).Returns("token_mock");

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
            Assert.Contains("token_mock", okResult.Value.ToString());
        }

        [Fact]
        public void Login_UsuarioInvalido_DeveRetornarUnauthorized()
        {
            // Arrange
            var loginRequest = new UsuarioViewModel { Email = "usuario@exemplo.com", Senha = "senha123" };
            _authServiceMock.Setup(x => x.Autenticar(loginRequest.Email, loginRequest.Senha)).Returns((UsuarioModel)null);

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
            Assert.Equal("Usuário ou senha inválidos.", unauthorizedResult.Value.ToString());
        }
    }
}
