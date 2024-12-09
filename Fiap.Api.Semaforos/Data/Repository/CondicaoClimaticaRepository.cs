using Fiap.Api.Semaforos.Data.Contexts;
using Fiap.Api.Semaforos.Models;
using Microsoft.EntityFrameworkCore;


namespace Fiap.Api.Semaforos.Data.Repository
{
    public class CondicaoClimaticaRepository : ICondicaoClimaticaRepository
    {
        private readonly DatabaseContext _context;

        public CondicaoClimaticaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<CondicaoClimaticaModel> GetAll()
        {
            return _context.CondicaoClimatica
                .ToList();
        }


        public CondicaoClimaticaModel GetById(int id)
        {
            return _context.CondicaoClimatica.Find(id);
        }

        public IEnumerable<CondicaoClimaticaModel> GetAll(int page, int size)
        {
            return _context.CondicaoClimatica.Include(c => c.Semaforo)
                            .Skip((page - 1) * page)
                            .Take(size)
                            .AsNoTracking()
                            .ToList();
        }

    }
}
