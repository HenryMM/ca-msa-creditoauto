using creditoauto.Domain.Interfaces;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.Models;
using creditoauto.Infraestructure.Services;
using Moq;

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

            SolicitudCredito solicitudCredito = new SolicitudCredito
            {
                ClienteId = clienteId,
                Cuotas = 2,
                EjecutivoId = 2,
                Entrada = 3,
                Estado = "REGISTRADO"
            };

            _solicitudCreditoRepository.Setup(
                m => m.SearchByAsync(x => x.ClienteId == solicitudCredito.ClienteId && x.Estado == "REGISTRADO")).ReturnsAsync(solicitudCreditosFake);

            ISolicitudCreditoInfraestructura _target = new SolicitudCreditoInfraestructura(_solicitudCreditoRepository.Object,
               _ejecutivoRepository.Object, _clientePatioRepository.Object);


            #endregion

            #region Act
            var actualResult = await _target.CrearSolicitudCredito(solicitudCredito);
            #endregion

            #region Assert
            Assert.That(actualResult.Mensaje, Is.EqualTo(excpectedMessage));
            #endregion
        }

        [Test]
        public async Task CrearSolicitudCredito_VehiculoTieneSolicitud_MensajeError()
        {
            #region Arrange
            string excpectedMessage = "El vehiculo tiene una solicitud registrada. No puede ser vendido.";
            int vehiculoId = 1;
            var _solicitudCreditoRepository = new Mock<IRepository<SolicitudCredito>>();
            var _ejecutivoRepository = new Mock<IRepository<Ejecutivo>>();
            var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();

            IQueryable<SolicitudCredito> solicitudCreditosClienteFake = new List<SolicitudCredito> { }.AsQueryable();

            IQueryable<SolicitudCredito> solicitudCreditosVehiculosFake = new List<SolicitudCredito> {
                 new SolicitudCredito
                    {
                        ClienteId = 1,
                        Cuotas = 2,
                        EjecutivoId = 2,
                        Entrada = 3,
                        Estado = "REGISTRADO",
                        VehiculoId = vehiculoId
                    }
            }.AsQueryable();

            SolicitudCredito solicitudCredito = new SolicitudCredito
            {
                ClienteId = 1,
                Cuotas = 2,
                EjecutivoId = 2,
                Entrada = 3,
                Estado = "REGISTRADO",
                VehiculoId = vehiculoId
            };

            _solicitudCreditoRepository.Setup(
                m => m.SearchByAsync(x => x.ClienteId == solicitudCredito.ClienteId && x.Estado == "REGISTRADO")).ReturnsAsync(solicitudCreditosClienteFake);
            _solicitudCreditoRepository.Setup(
                m => m.SearchByAsync(x => x.VehiculoId == solicitudCredito.VehiculoId && x.Estado == "REGISTRADO")).ReturnsAsync(solicitudCreditosVehiculosFake);

            ISolicitudCreditoInfraestructura _target = new SolicitudCreditoInfraestructura(_solicitudCreditoRepository.Object,
               _ejecutivoRepository.Object, _clientePatioRepository.Object);


            #endregion

            #region Act
            var actualResult = await _target.CrearSolicitudCredito(solicitudCredito);
            #endregion

            #region Assert
            Assert.That(actualResult.Mensaje, Is.EqualTo(excpectedMessage));
            #endregion
        }

        [Test]
        public async Task CrearSolicitudCredito_VehiculoYClienteNoTieneSolicitud_IsSuccessfullIgualTrue()
        {
            #region Arrange
            int vehiculoId = 1;
            var _solicitudCreditoRepository = new Mock<IRepository<SolicitudCredito>>();
            var _ejecutivoRepository = new Mock<IRepository<Ejecutivo>>();
            var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();

            IQueryable<SolicitudCredito> solicitudCreditosClienteFake = new List<SolicitudCredito> { }.AsQueryable();

            IQueryable<SolicitudCredito> solicitudCreditosVehiculosFake = new List<SolicitudCredito> {}.AsQueryable();

            SolicitudCredito solicitudCredito = new SolicitudCredito
            {
                ClienteId = 1,
                Cuotas = 2,
                EjecutivoId = 2,
                Entrada = 3,
                Estado = "REGISTRADO",
                VehiculoId = vehiculoId
            };

            _solicitudCreditoRepository.Setup(
                m => m.SearchByAsync(x => x.ClienteId == solicitudCredito.ClienteId && x.Estado == "REGISTRADO")).ReturnsAsync(solicitudCreditosClienteFake);
            _solicitudCreditoRepository.Setup(
                m => m.SearchByAsync(x => x.VehiculoId == solicitudCredito.VehiculoId && x.Estado == "REGISTRADO")).ReturnsAsync(solicitudCreditosVehiculosFake);
            _solicitudCreditoRepository.Setup(
                m => m.CreateEntityAsync(It.IsAny<SolicitudCredito>())).ReturnsAsync(new SolicitudCredito
                {
                    Id = 1,
                    ClienteId = 1,
                    Cuotas = 2,
                    EjecutivoId = 2,
                    Entrada = 3,
                    Estado = "REGISTRADO",
                    VehiculoId = vehiculoId
                });

            _ejecutivoRepository.Setup(
                m => m.GetEntityByIdAsync(It.IsAny<int>())).ReturnsAsync(new Ejecutivo
                {
                    Id = 1,
                    PatioId = 2 
                });

            _clientePatioRepository.Setup(
               x => x.CreateEntityAsync(It.IsAny<ClientePatio>())).ReturnsAsync(new ClientePatio { });
            _solicitudCreditoRepository.Setup(
                x => x.SaveAsync()).ReturnsAsync(1);

            ISolicitudCreditoInfraestructura _target = new SolicitudCreditoInfraestructura(_solicitudCreditoRepository.Object,
               _ejecutivoRepository.Object, _clientePatioRepository.Object);


            #endregion

            #region Act
            var actualResult = await _target.CrearSolicitudCredito(solicitudCredito);
            #endregion

            #region Assert
            Assert.IsTrue(actualResult.IsSuccessfull);
            #endregion
        }
    }
}
