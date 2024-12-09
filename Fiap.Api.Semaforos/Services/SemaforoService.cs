using Fiap.Api.Semaforos.Data.Repository;
using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Services
{
    public class SemaforoService: ISemaforoService
    {
        private readonly ISemaforoRepository _repository;

        public SemaforoService(ISemaforoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SemaforoModel> ListarSemaforos() => _repository.GetAll();

        public IEnumerable<SemaforoModel> ListarSemaforos(int pagina = 1, int tamanho = 10)
        {
            return _repository.GetAll(pagina, tamanho);
        }

    }
}
