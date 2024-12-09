namespace Fiap.Api.Semaforos.Models
{
    public class SemaforoModel
    {
        public long SemaforoId { get; set; }
        public required string Luz { get; set; }
        public required string Logradouro { get; set; }
        public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
    }
}
