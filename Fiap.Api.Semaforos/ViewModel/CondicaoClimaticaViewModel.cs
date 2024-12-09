namespace Fiap.Api.Semaforos.ViewModel
{
    public class CondicaoClimaticaViewModel
    {
        public long CondicaoClimaticaId { get; set; }
        public string Tempo { get; set; } = string.Empty;
        public double Temperatura { get; set; }
        public DateTime AtualizadoEm { get; set; }

        public long? SemaforoId { get; set; }
        public string? SemaforoLogradouro { get; set; } 
        public string? SemaforoLuz { get; set; } 
    }
}
