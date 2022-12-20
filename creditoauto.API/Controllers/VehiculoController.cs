using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace creditoauto.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private IVehiculoInfraestructura _vehiculoInfraestructura;

        public VehiculoController(IVehiculoInfraestructura vehiculoInfraestructura)
        {
            _vehiculoInfraestructura = vehiculoInfraestructura;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerVehiculo(int vehiculoId)
        {
            RespuestaGenerica<Vehiculo> result = await _vehiculoInfraestructura.ObtenerVehiculoAsync(vehiculoId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearVehiculo(Vehiculo vehiculo)
        {
            RespuestaGenerica<Vehiculo> result = await _vehiculoInfraestructura.CrearVehiculoAsync(vehiculo);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizaVehiculo(Vehiculo vehiculo)
        {
            RespuestaGenerica<Vehiculo> result = await _vehiculoInfraestructura.ActualizarVehiculoAsync(vehiculo);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarVehiculo(int idVehiculo)
        {
            RespuestaGenerica<string> result = await _vehiculoInfraestructura.EliminarVehiculoAsync(idVehiculo);

            return Ok(result);
        }
    }
}
