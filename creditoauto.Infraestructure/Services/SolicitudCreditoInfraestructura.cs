using creditoauto.Domain.Interfaces;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Infraestructure.Services
{
    public class SolicitudCreditoInfraestructura : ISolicitudCreditoInfraestructura
    {
        private IRepository<SolicitudCredito> _repositorySolicitudCredito;
        private IRepository<Ejecutivo> _repositoryEjecutivo;
        private IRepository<ClientePatio> _repositoryClientePatio;

        public SolicitudCreditoInfraestructura(IRepository<SolicitudCredito> repositorySolicitudCredito,
            IRepository<Ejecutivo> repositoryEjecutivo,
            IRepository<ClientePatio> repositoryClientePatio)
        {
            _repositorySolicitudCredito = repositorySolicitudCredito;
            _repositoryEjecutivo = repositoryEjecutivo;
            _repositoryClientePatio = repositoryClientePatio;
        }
        public async Task<RespuestaGenerica<SolicitudCredito>> CrearSolicitudCredito(SolicitudCredito solicitudCredito)
        {

            RespuestaGenerica<SolicitudCredito> result = await Validate(solicitudCredito);

            if (!result.IsSuccessfull)
            {
                return result;
            }

            await _repositorySolicitudCredito.CreateEntityAsync(solicitudCredito);


            Ejecutivo ejecutivo = await _repositoryEjecutivo.GetEntityByIdAsync(solicitudCredito.EjecutivoId);

            int patioId = ejecutivo.PatioId;

            ClientePatio clientePatio = new ClientePatio
            {
                PatioId = patioId,
                ClienteId   = solicitudCredito.ClienteId,
                FechaAsignacion = DateTime.Now
            };

            await _repositoryClientePatio.CreateEntityAsync(clientePatio);
            await _repositorySolicitudCredito.SaveAsync();

            return new RespuestaGenerica<SolicitudCredito>
            {
                Data = solicitudCredito,
                IsSuccessfull = true
            };

        }


        private async Task<RespuestaGenerica<SolicitudCredito>> Validate(SolicitudCredito solicitudCredito)
        {
            var solicitudesCliente = await _repositorySolicitudCredito.SearchByAsync(
               s => s.ClienteId == solicitudCredito.ClienteId && s.Estado == "REGISTRADO");

            int cantidadSolicitudes = solicitudesCliente.Count();

            if (cantidadSolicitudes > 0)
            {
                return new RespuestaGenerica<SolicitudCredito>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Mensaje = "El cliente puede tener una solicitud activa por día. La solicitud no fue creada."
                };
            }

            var solicitudesVehiculo = await _repositorySolicitudCredito.SearchByAsync(
                s => s.VehiculoId == solicitudCredito.VehiculoId && s.Estado == "REGISTRADO");


            int cantidadSolicitudesVehiculo = solicitudesVehiculo.Count();

            if (cantidadSolicitudesVehiculo > 0)
            {
                return new RespuestaGenerica<SolicitudCredito>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Mensaje = "El vehiculo tiene una solicitud registrada. No puede ser vendido. "
                };
            }
            return new RespuestaGenerica<SolicitudCredito> { IsSuccessfull = true };
        }
    }
}
