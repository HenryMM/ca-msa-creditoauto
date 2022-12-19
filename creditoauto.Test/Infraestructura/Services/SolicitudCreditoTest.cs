using creditoauto.Domain.Interfaces;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.Models;
using creditoauto.Infraestructure.Services;
using Moq;
using System.Linq.Expressions;

namespace creditoauto.Test.Infraestructura.Services
{
    public class SolicitudCreditoTest
    {
        [Test]
        public async Task CrearSolicitudCredito_ClienteTieneSolicitud_MensajeError()
        {
            #region Arrange
            string excpectedMessage = "El cliente puede tener una solicitud activa por día. La solicitud no fue creada.";
            int clienteId = 1;
            var _solicitudCreditoRepository = new Mock<IRepository<SolicitudCredito>>();
            var _ejecutivoRepository = new Mock<IRepository<Ejecutivo>>();
            var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();
            ISolicitudCreditoInfraestructura _target = new SolicitudCreditoInfraestructura(_solicitudCreditoRepository.Object,
                _ejecutivoRepository.Object, _clientePatioRepository.Object);


            IQueryable<SolicitudCredito> solicitudCreditosFake = new List<SolicitudCredito>
            {
                new SolicitudCredito
                {
                     ClienteId = clienteId,
                    Cuotas = 2,
                    EjecutivoId = 2,
                    Entrada = 3,
                    Estado = "REGISTRADO"
                }
            }.AsQueryable();

            _solicitudCreditoRepository.Setup(
                m => m.SearchByAsync(It.IsAny<Expression<Func<SolicitudCredito, bool>>>())).ReturnsAsync(solicitudCreditosFake);

            SolicitudCredito solicitudCredito = new SolicitudCredito
            {
                ClienteId = clienteId,
                Cuotas = 2,
                EjecutivoId = 2,
                Entrada = 3,
                Estado = "REGISTRADO"
            };
            #endregion

            #region Act
            var actualResult = await _target.CrearSolicitudCredito(solicitudCredito);
            #endregion

            #region Assert
            Assert.That(actualResult.Mensaje, Is.EqualTo(excpectedMessage));
            #endregion
        }

        //[Test]
        //public async Task CrearSolicitudCredito_VehiculoTieneSolicitud_MensajeError()
        //{
        //    #region Arrange
        //    string excpectedMessage = "El vehiculo tiene una solicitud registrada. No puede ser vendido.";
        //    int clienteId = 1;
        //    int vehiculoId = 1;
        //    var _solicitudCreditoRepository = new Mock<IRepository<SolicitudCredito>>();
        //    var _ejecutivoRepository = new Mock<IRepository<Ejecutivo>>();
        //    var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();
        //    ISolicitudCreditoInfraestructura _target = new SolicitudCreditoInfraestructura(_solicitudCreditoRepository.Object,
        //        _ejecutivoRepository.Object, _clientePatioRepository.Object);


        //    IQueryable<SolicitudCredito> solicitudCreditosFake = new List<SolicitudCredito>
        //    {
        //        new SolicitudCredito
        //        {
        //             ClienteId = clienteId,
        //            Cuotas = 2,
        //            EjecutivoId = 2,
        //            Entrada = 3,
        //            Estado = "REGISTRADO"
        //        }
        //    }.AsQueryable();

        //    _solicitudCreditoRepository.Setup(
        //        m => m.SearchByAsync(x => x.VehiculoId == vehiculoId && x.Estado == "REGISTRADO")).ReturnsAsync(solicitudCreditosFake);

        //    SolicitudCredito solicitudCredito = new SolicitudCredito
        //    {
        //        ClienteId = clienteId,
        //        Cuotas = 2,
        //        EjecutivoId = 2,
        //        Entrada = 3,
        //        Estado = "REGISTRADO",
        //        VehiculoId = vehiculoId
        //    };
        //    #endregion

        //    #region Act
        //    var actualResult = await _target.CrearSolicitudCredito(solicitudCredito);
        //    #endregion

        //    #region Assert
        //    Assert.That(actualResult.Mensaje, Is.EqualTo(excpectedMessage));
        //    #endregion
        //}
    }
}
