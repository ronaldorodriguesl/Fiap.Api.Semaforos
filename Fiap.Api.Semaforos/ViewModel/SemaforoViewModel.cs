namespace Fiap.Api.Semaforos.ViewModel
{
    public class SemaforoViewModel
    {
        public long SemaforoId { get; set; }
        public string Luz { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public DateTime AtualizadoEm { get; set; }
    }
}
