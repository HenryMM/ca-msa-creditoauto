using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace creditoauto.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EjecutivoController : ControllerBase
    {

        private IEjecutivoInfraestructura _ejecutivoInfraestructura;
        private readonly ILogger<ClienteController> _logger;

        public EjecutivoController(IEjecutivoInfraestructura clienteService, ILogger<ClienteController> logger)
        {
            _ejecutivoInfraestructura = clienteService;
            _logger = logger;
        }

        [HttpPost("CargaInicial")]
        public async Task<IActionResult> CargaInicial()
        {
            _logger.LogInformation("Carga inicial de ejecutivos");
            RespuestaGenerica<List<Ejecutivo>> result = await _ejecutivoInfraestructura.CargaInicialAsync();

            return Ok(result);
        }

    }
}
