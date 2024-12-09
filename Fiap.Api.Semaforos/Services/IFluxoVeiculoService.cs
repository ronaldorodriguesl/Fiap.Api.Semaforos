using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Services
{
    public interface IFluxoVeiculoService
    {
        void AdicionarFluxoVeiculo(FluxoVeiculoModel fluxoVeiculo);
        FluxoVeiculoModel ObterFluxoVeiculoPorIdComDetalhes(int id);
    }
}
