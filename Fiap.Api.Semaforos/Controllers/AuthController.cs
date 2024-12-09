using Microsoft.AspNetCore.Mvc;
using Fiap.Api.Semaforos.ViewModel;
using Fiap.Api.Semaforos.Services;
using AutoMapper;


namespace Fiap.Api.Semaforos.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioViewModel loginRequest)
        {
            var usuario = _service.Autenticar(loginRequest.Email, loginRequest.Senha);

            if (usuario == null)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            var token = _service.GenerateJwtToken(usuario);

            return Ok(new { Token = token });
        }
    }
}
