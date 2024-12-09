namespace Fiap.Api.Semaforos.Models
{
    public class FluxoVeiculoModel
    {
        public long FluxoVeiculoId { get; set; }

        public int Quantidade { get; set; }

        public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

        public long SemaforoId { get; set; }

        public SemaforoModel? Semaforo { get; set; }

    }
}
