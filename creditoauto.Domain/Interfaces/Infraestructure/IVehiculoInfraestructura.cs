
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Domain.Interfaces.Infraestructure
{
    public interface IVehiculoInfraestructura
    {
        Task<RespuestaGenerica<Vehiculo>> CrearVehiculoAsync(Vehiculo vehiculo);
        Task<RespuestaGenerica<Vehiculo>> ObtenerVehiculoAsync(int idVehiculo);
        Task<RespuestaGenerica<string>> EliminarVehiculoAsync(int idVehiculo);
        Task<RespuestaGenerica<Vehiculo>> ActualizarVehiculoAsync(Vehiculo vehiculo);
    }
}
