
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Domain.Interfaces.Infraestructure
{
    public interface IEjecutivoInfraestructura
    {
        Task<RespuestaGenerica<List<Ejecutivo>>> CargaInicialAsync();
        Task<List<Ejecutivo>> CrearEjecutivosAsync(List<Ejecutivo> ejecutivos);
    }
}
