using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Data.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel GetByEmail(string email);
    }
}
