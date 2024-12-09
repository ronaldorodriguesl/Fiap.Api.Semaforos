using Fiap.Api.Semaforos.Models;

namespace Fiap.Api.Semaforos.Services
{
    public interface ISemaforoService
    {
        IEnumerable<SemaforoModel> ListarSemaforos();

        IEnumerable<SemaforoModel> ListarSemaforos(int pagina = 0, int tamanho = 10);
    }
}
