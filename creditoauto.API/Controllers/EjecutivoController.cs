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

        public EjecutivoController(IEjecutivoInfraestructura clienteService)
        {
            _ejecutivoInfraestructura = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> CargaInicial()
        {
            RespuestaGenerica<List<Ejecutivo>> result = await _ejecutivoInfraestructura.CargaInicialAsync();

            return Ok(result);
        }
    }
}
