using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Data.Repository
{
    public interface ISemaforoRepository
    {
        IEnumerable<SemaforoModel> GetAll();

        IEnumerable<SemaforoModel> GetAll(int pagina = 0, int tamanho = 10);
    }
}
