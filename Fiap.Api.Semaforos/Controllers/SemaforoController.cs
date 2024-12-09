using AutoMapper;
using Fiap.Api.Semaforos.Services;
using Fiap.Api.Semaforos.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Semaforos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemaforoController : ControllerBase
    {
        private readonly ISemaforoService _service;
        private readonly IMapper _mapper;
        public SemaforoController(ISemaforoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<SemaforoViewModel>> Get([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            var semaforos = _service.ListarSemaforos(pagina, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<SemaforoViewModel>>(semaforos);

            var viewModel = new SemaforoPaginacaoViewModel
            {
                Semaforos = viewModelList,
                CurrentPage = pagina,
                PageSize = tamanho
            };


            return Ok(viewModel);
        }
    }
}
