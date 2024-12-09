using Fiap.Api.Semaforos.Data.Contexts;
using Fiap.Api.Semaforos.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Semaforos.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DatabaseContext _context;

        public UsuarioRepository(DatabaseContext context)
        {
            _context = context;
        }

        public UsuarioModel GetByEmail(string email)
        {
            return _context.Usuario.SingleOrDefault(u => u.Email == email);
        }
    }
}
