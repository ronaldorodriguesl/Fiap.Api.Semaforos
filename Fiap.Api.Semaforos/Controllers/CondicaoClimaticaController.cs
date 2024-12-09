using Microsoft.AspNetCore.Mvc;
using Fiap.Api.Semaforos.ViewModel;
using Fiap.Api.Semaforos.Services;
using AutoMapper;

namespace Fiap.Api.Semaforos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondicaoClimaticaController : ControllerBase
    {
        private readonly ICondicaoClimaticaService _service;
        private readonly IMapper _mapper;
        public CondicaoClimaticaController(ICondicaoClimaticaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<CondicaoClimaticaViewModel> Get(int id)
        {
            var condicao = _service.ObterCondicaoClimaticaPorId(id);
            if (condicao == null)
                return NotFound();
            var viewModel = _mapper.Map<CondicaoClimaticaViewModel>(condicao);
            return Ok(viewModel);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CondicaoClimaticaViewModel>> Get([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            var condicoesClimaticas = _service.ListarCondicoes(pagina, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<CondicaoClimaticaViewModel>>(condicoesClimaticas);

            var viewModel = new CondicaoClimaticaPaginacaoViewModel
            {
                CondicoesClimaticas = viewModelList,
                CurrentPage = pagina,
                PageSize = tamanho
            };


            return Ok(viewModel);
        }
    }
}