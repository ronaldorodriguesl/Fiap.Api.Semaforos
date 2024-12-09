namespace Fiap.Api.Semaforos.ViewModel
{
    public class FluxoVeiculoViewModel
    {
        public long FluxoVeiculoId { get; set; }
        public int Quantidade { get; set; }
        public DateTime AtualizadoEm { get; set; }
        public long? SemaforoId { get; set; }
        public string? SemaforoLogradouro { get; set; }
        public string? SemaforoLuz { get; set; }
    }
}
