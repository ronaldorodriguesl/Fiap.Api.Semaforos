using Fiap.Api.Semaforos.Data.Contexts;
using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Data.Repository
{
    public class SemaforoRepository: ISemaforoRepository
    {
        private readonly DatabaseContext _context;

        public SemaforoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<SemaforoModel> GetAll()
        {
            return _context.Semaforo
                .ToList();
        }

        public IEnumerable<SemaforoModel> GetAll(int page, int size)
        {
            return _context.Semaforo
                            .Skip((page - 1) * page)
                            .Take(size)
                            .ToList();
        }
    }
}
