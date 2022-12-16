using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace creditoauto.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {

        private IMarcaInfraestructura _marcaService;

        public MarcaController(IMarcaInfraestructura marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpPost("CargaInicial")]
        public async Task<IActionResult> CargaInicial()
        {
            RespuestaGenerica<List<Marca>> result = await _marcaService.CargaInicialAsync();

            return Ok(result);
        }
    }
}
