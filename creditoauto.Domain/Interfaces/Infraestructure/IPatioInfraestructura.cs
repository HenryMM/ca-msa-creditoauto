
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Domain.Interfaces.Infraestructure
{
    public interface IPatioInfraestructura
    {
        Task<RespuestaGenerica<Patio>> CrearPatioAsync(Patio patio);
        Task<RespuestaGenerica<Patio>> ObtenerPatioAsync(int idPatio);
        Task<RespuestaGenerica<string>> EliminarPatioAsync(int idPatio);
        Task<RespuestaGenerica<Patio>> ActualizarPatioAsync(Patio patio);
    }
}
