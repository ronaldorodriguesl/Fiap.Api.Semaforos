using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Fiap.Api.Semaforos.ViewModel;
using Fiap.Api.Semaforos.Models;
using Fiap.Api.Semaforos.Services;
using AutoMapper;

namespace Fiap.Api.Semaforos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FluxoVeiculoController : ControllerBase
    {
        private readonly IFluxoVeiculoService _service;
        private readonly IMapper _mapper;
        public FluxoVeiculoController(IFluxoVeiculoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult Post([FromBody] FluxoVeiculoViewModel fluxoVeiculoViewModel)
        {
            var fluxoVeiculo = _mapper.Map<FluxoVeiculoModel>(fluxoVeiculoViewModel);

            try
            {
                _service.AdicionarFluxoVeiculo(fluxoVeiculo);
                return CreatedAtAction(nameof(GetFluxoVeiculoById), new { id = fluxoVeiculo.FluxoVeiculoId }, fluxoVeiculoViewModel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<FluxoVeiculoViewModel> GetFluxoVeiculoById(int id)
        {
            var fluxoVeiculo = _service.ObterFluxoVeiculoPorIdComDetalhes(id);
            if (fluxoVeiculo == null)
            {
                return NotFound();
            }

            var fluxoVeiculoViewModel = _mapper.Map<FluxoVeiculoViewModel>(fluxoVeiculo);
 
            return Ok(fluxoVeiculoViewModel);
        }

    }
}