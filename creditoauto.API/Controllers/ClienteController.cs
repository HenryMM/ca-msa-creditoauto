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

        [HttpGet]
        public async Task<IActionResult> ObtenerCliente(int clienteId)
        {
            RespuestaGenerica<Cliente> result = await _clienteService.ObtenerClienteAsync(clienteId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente(Cliente cliente)
        {
            RespuestaGenerica<Cliente> result = await _clienteService.CrearClienteAsync(cliente);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizaCliente(Cliente cliente)
        {
            RespuestaGenerica<Cliente> result = await _clienteService.ActualizarClienteAsync(cliente);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCliente(int idCliente)
        {
            RespuestaGenerica<string> result = await _clienteService.EliminarClienteAsync(idCliente);

            return Ok(result);
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