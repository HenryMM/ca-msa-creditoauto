using creditoauto.Domain.Interfaces;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace creditoauto.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private IClienteInfraestructura _clienteService;

        public ClienteController(IClienteInfraestructura clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost("CargaInicial")]
        public async Task<IActionResult> CargaInicial()
        {
            RespuestaGenerica<List<Cliente>> result = await _clienteService.CargaInicialAsync();

            return Ok(result);
        }

        [HttpPost("AsignarPatio")]
        public async Task<IActionResult> AsignarPatio(ClientePatio clientePatio)
        {
            RespuestaGenerica<ClientePatio> result = await _clienteService.AsignarPatio(clientePatio);

            return Ok(result);
        }
    }
}