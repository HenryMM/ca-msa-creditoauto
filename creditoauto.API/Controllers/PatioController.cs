using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace creditoauto.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private IPatioInfraestructura _patioInfraestrucrure;

        public PatioController(IPatioInfraestructura patioInfraestructure)
        {
            _patioInfraestrucrure = patioInfraestructure;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPatio(int patioId)
        {
            RespuestaGenerica<Patio> result = await _patioInfraestrucrure.ObtenerPatioAsync(patioId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearPatio(Patio patio)
        {
            RespuestaGenerica<Patio> result = await _patioInfraestrucrure.CrearPatioAsync(patio);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizaPatio(Patio patio)
        {
            RespuestaGenerica<Patio> result = await _patioInfraestrucrure.ActualizarPatioAsync(patio);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarPatio(int idPatio)
        {
            RespuestaGenerica<string> result = await _patioInfraestrucrure.EliminarPatioAsync(idPatio);

            return Ok(result);
        }
    }
}
