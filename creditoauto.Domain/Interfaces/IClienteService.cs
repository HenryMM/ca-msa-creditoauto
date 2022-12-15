using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Domain.Interfaces
{
    public interface IClienteService
    {
        Task<RespuestaGenerica<List<Cliente>>> CargaInicialAsync();
        Task<List<Cliente>> CrearClientesAsync(List<Cliente> clientes);
        List<Cliente> LeerArchivo(string ubicacionArchivo);
    }
}
