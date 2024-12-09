using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Services
{
    public interface IAuthService
    {
        UsuarioModel Autenticar(string email, string senha);
        string GenerateJwtToken(UsuarioModel user);
    }
}
