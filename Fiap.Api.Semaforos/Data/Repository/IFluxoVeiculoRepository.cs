using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Data.Repository
{
    public interface IFluxoVeiculoRepository
    {
        void Add(FluxoVeiculoModel fluxoVeiculo);

        FluxoVeiculoModel GetById(int id);
    }
}
