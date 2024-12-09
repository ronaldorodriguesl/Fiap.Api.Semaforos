using Fiap.Api.Semaforos.Data.Contexts;
using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Data.Repository
{
    public class FluxoVeiculoRepository: IFluxoVeiculoRepository
    {
        private readonly DatabaseContext _context;

        public FluxoVeiculoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public FluxoVeiculoModel GetById(int id)
        {
            return _context.FluxoVeiculo.Find(id);
        }

        public void Add(FluxoVeiculoModel representante)
        {
            _context.FluxoVeiculo.Add(representante);
            _context.SaveChanges();
        }
    }
}
