using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Domain.Interfaces.Infraestructure
{
    public interface IClienteInfraestructura
    {
        Task<RespuestaGenerica<List<Cliente>>> CargaInicialAsync();
        Task<List<Cliente>> CrearClientesAsync(List<Cliente> clientes);
    }
}
