using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Data.Repository
{
    public interface ICondicaoClimaticaRepository
    {
        IEnumerable<CondicaoClimaticaModel> GetAll();

        IEnumerable<CondicaoClimaticaModel> GetAll(int pagina =0, int tamanho = 10);

        CondicaoClimaticaModel GetById(int id);
    }
}
