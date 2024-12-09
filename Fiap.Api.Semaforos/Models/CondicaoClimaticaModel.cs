namespace Fiap.Api.Semaforos.Models
{
    public class CondicaoClimaticaModel
    {
        public long CondicaoClimaticaId { get; set; }

        public string Tempo { get; set; } = string.Empty;
        public double Temperatura { get; set; }
        public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

        public long SemaforoId { get; set; }
        public SemaforoModel? Semaforo { get; set; }
    }
}
