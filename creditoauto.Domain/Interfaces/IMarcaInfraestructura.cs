
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Domain.Interfaces
{
    public interface IMarcaInfraestructura
    {
        Task<RespuestaGenerica<List<Marca>>> CargaInicialAsync();
        Task<List<Marca>> CrearMarcasAsync(List<Marca> clientes);
    }
}
