namespace Fiap.Api.Semaforos.ViewModel
{
    public class CondicaoClimaticaPaginacaoViewModel
    {
        public IEnumerable<CondicaoClimaticaViewModel>? CondicoesClimaticas { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CondicoesClimaticas != null && CondicoesClimaticas.Count() == PageSize;
        public string PreviousPageUrl => HasPreviousPage ? $"/CondicaoClimatica?pagina={CurrentPage - 1}&tamanho={PageSize}" : "";
        public string NextPageUrl => HasNextPage ? $"/CondicaoClimatica?pagina={CurrentPage + 1}&tamanho={PageSize}" : "";

    }
}
