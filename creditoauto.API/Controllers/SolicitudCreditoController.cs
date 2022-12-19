using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace creditoauto.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SolicitudCreditoController : ControllerBase
    {
        private ISolicitudCreditoInfraestructura _solicitudCreditoInfraestructura;

        public SolicitudCreditoController(ISolicitudCreditoInfraestructura solicitudCreditoInfraestructura)
        {
            _solicitudCreditoInfraestructura = solicitudCreditoInfraestructura;
        }

        [HttpPost]
        public async Task<IActionResult> CrearSolicitudCredito(SolicitudCredito solicitudCredito)
        {
            RespuestaGenerica<SolicitudCredito> result = await _solicitudCreditoInfraestructura.CrearSolicitudCredito(solicitudCredito);

            return Ok(result);
        }
    }
}
