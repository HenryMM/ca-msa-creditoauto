using creditoauto.Domain.Interfaces;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Infraestructure.Services
{
    public class VehiculoInfraestructura : IVehiculoInfraestructura
    {
        private IRepository<Vehiculo> _repositoryVehiculo;

        public VehiculoInfraestructura(IRepository<Vehiculo> repositoryVehiculo)
        {
            _repositoryVehiculo = repositoryVehiculo;
        }
        public async Task<RespuestaGenerica<Vehiculo>> ActualizarVehiculoAsync(Vehiculo vehiculo)
        {
            var queryResult = await _repositoryVehiculo.SearchByAsync(v => v.Placa == vehiculo.Placa);

            if (queryResult.Count() > 0)
            {
                return new RespuestaGenerica<Vehiculo>
                {
                    Mensaje = "El vehiculo no fue modificado. Ya hay un vehiculo con la misma placa.",
                    IsSuccessfull = false
                };
            }

            await _repositoryVehiculo.UpdateEntityAsync(vehiculo);
            await _repositoryVehiculo.SaveAsync();
            return new RespuestaGenerica<Vehiculo>
            {
                Data = vehiculo,
                IsSuccessfull = true
            };
        }

        public async Task<RespuestaGenerica<Vehiculo>> CrearVehiculoAsync(Vehiculo vehiculo)
        {
            var queryResult = await _repositoryVehiculo.SearchByAsync(v => v.Placa == vehiculo.Placa);

            if (queryResult.Count() > 0)
            {
                return new RespuestaGenerica<Vehiculo>
                {
                    Mensaje = "El vehiculo no fue creado. Ya hay un vehiculo con la misma placa.",
                    IsSuccessfull = false
                };
            }

            await _repositoryVehiculo.CreateEntityAsync(vehiculo);
            await _repositoryVehiculo.SaveAsync();
            return new RespuestaGenerica<Vehiculo>
            {
                Data = vehiculo,
                IsSuccessfull = true
            };
        }

        public async Task<RespuestaGenerica<string>> EliminarVehiculoAsync(int idVehiculo)
        {
            await _repositoryVehiculo.DeleteEntityAsync(idVehiculo);
            await _repositoryVehiculo.SaveAsync();
            return new RespuestaGenerica<string>
            {
                Data = "Ok",
                IsSuccessfull = true
            };
        }

        public async Task<RespuestaGenerica<Vehiculo>> ObtenerVehiculoAsync(int idVehiculo)
        {
            Vehiculo vehiculo = await _repositoryVehiculo.GetEntityByIdAsync(idVehiculo);
            return new RespuestaGenerica<Vehiculo>
            {
                Data = vehiculo,
                IsSuccessfull = true
            };
        }
    }
}
