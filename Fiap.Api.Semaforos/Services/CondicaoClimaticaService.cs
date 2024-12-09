using Fiap.Api.Semaforos.Data.Repository;
using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Services
{
    public class CondicaoClimaticaService: ICondicaoClimaticaService
    {
        private readonly ICondicaoClimaticaRepository _repository;

        public CondicaoClimaticaService(ICondicaoClimaticaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CondicaoClimaticaModel> ListarCondicoes() => _repository.GetAll();

        public IEnumerable<CondicaoClimaticaModel> ListarCondicoes(int pagina = 1, int tamanho = 10)
        {
            return _repository.GetAll(pagina, tamanho);
        }


        public CondicaoClimaticaModel ObterCondicaoClimaticaPorId(int id) => _repository.GetById(id);
    }
}
