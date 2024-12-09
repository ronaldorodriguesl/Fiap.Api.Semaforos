using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Services
{
    public interface ICondicaoClimaticaService
    {
        IEnumerable<CondicaoClimaticaModel> ListarCondicoes();

        IEnumerable<CondicaoClimaticaModel> ListarCondicoes(int pagina = 0, int tamanho = 10);

        CondicaoClimaticaModel ObterCondicaoClimaticaPorId(int id);
    }
}

