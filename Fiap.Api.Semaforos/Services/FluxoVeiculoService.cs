using Fiap.Api.Semaforos.Data.Repository;
using Fiap.Api.Semaforos.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Semaforos.Services
{
    public class FluxoVeiculoService : IFluxoVeiculoService
    {
        private readonly IFluxoVeiculoRepository _repository;

        public FluxoVeiculoService(IFluxoVeiculoRepository repository)
        {
            _repository = repository;
        }

        public FluxoVeiculoModel ObterFluxoVeiculoPorIdComDetalhes(int id) => _repository.GetById(id);

        public void AdicionarFluxoVeiculo(FluxoVeiculoModel fluxoVeiculo) => _repository.Add(fluxoVeiculo);

    }
}
