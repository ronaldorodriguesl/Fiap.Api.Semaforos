namespace Fiap.Api.Semaforos.ViewModel
{
    public class SemaforoPaginacaoViewModel
    {
        public IEnumerable<SemaforoViewModel>? Semaforos { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => Semaforos != null && Semaforos.Count() == PageSize;

        public string PreviousPageUrl => HasPreviousPage ? $"/Semaforo?pagina={CurrentPage - 1}&tamanho={PageSize}" : "";
        public string NextPageUrl => HasNextPage ? $"/Semaforo?pagina={CurrentPage + 1}&tamanho={PageSize}" : "";
    }

}
