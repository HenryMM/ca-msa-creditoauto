using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Domain.Interfaces.Infraestructure
{
    public interface IMarcaInfraestructura
    {
        Task<RespuestaGenerica<List<Marca>>> CargaInicialAsync();
        Task<List<Marca>> CrearMarcasAsync(List<Marca> clientes);
    }
}
