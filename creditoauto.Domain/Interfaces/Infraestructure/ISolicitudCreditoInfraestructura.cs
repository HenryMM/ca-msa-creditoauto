
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Domain.Interfaces.Infraestructure
{
    public interface ISolicitudCreditoInfraestructura
    {
        Task<RespuestaGenerica<SolicitudCredito>> CrearSolicitudCredito(SolicitudCredito solicitudCredito);
    }
   
}
