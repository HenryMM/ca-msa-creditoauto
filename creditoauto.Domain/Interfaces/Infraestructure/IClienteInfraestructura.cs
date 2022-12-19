using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Domain.Interfaces.Infraestructure
{
    public interface IClienteInfraestructura
    {
        Task<RespuestaGenerica<Cliente>> CrearClienteAsync(Cliente cliente);
        Task<RespuestaGenerica<Cliente>> ObtenerClienteAsync(int idCliente);
        Task<RespuestaGenerica<string>> EliminarClienteAsync(int idCliente);
        Task<RespuestaGenerica<Cliente>> ActualizarClienteAsync(Cliente cliente);
        Task<RespuestaGenerica<List<Cliente>>> CargaInicialAsync();
        Task<List<Cliente>> CrearClientesAsync(List<Cliente> clientes);
        Task<RespuestaGenerica<ClientePatio>> AsignarPatio(ClientePatio clientePatio);
    }
}
