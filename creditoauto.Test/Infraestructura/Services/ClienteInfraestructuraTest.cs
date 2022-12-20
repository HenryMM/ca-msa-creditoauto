using creditoauto.Domain.Interfaces;
using creditoauto.Entity.Models;
using Moq;
using creditoauto.Infraestructure.Services;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using creditoauto.Common.ClassMaps;
using creditoauto.Domain.Interfaces.Infraestructure;

namespace creditoauto.Test.Infraestructura.Services
{
    public class ClienteInfraestructuraTest
    {
        [Test]
        public async Task ObtenerClienteAsync_ClienteExiste_ReturnCliente()
        {
            //Arrange
            int expectedClienteId = 1;
            var _clienteRepository = new Mock<IRepository<Cliente>>();
            var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();
            var _fileHelper = new Mock<IFileHelper<Cliente>>();
            var _config = new Mock<IConfiguration>();
            _clienteRepository.Setup(x => x.GetEntityByIdAsync(
                It.IsAny<int>())).ReturnsAsync(new Cliente
                {
                    Id = 1,
                    Identificacion = "1598755656"
                });
            var target = new ClienteInfraestructura(_clienteRepository.Object,
                _clientePatioRepository.Object, _config.Object, _fileHelper.Object);

            //Act
            var cliente = await target.ObtenerClienteAsync(1);

            //Assert
            Assert.That(cliente.Data.Id, Is.EqualTo(expectedClienteId));
        }

        [Test]
        public async Task CrearClienteAsync_IdentificacionExiste_IsSuccessfullIgualFalso()
        {
            //Arrange
            var _clienteRepository = new Mock<IRepository<Cliente>>();
            var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();
            var _fileHelper = new Mock<IFileHelper<Cliente>>();
            var _config = new Mock<IConfiguration>();

            IQueryable<Cliente> clienteFake = new List<Cliente>
            {
                new Cliente
                {
                    Id = 1,
                    Identificacion = "1784956665"
                }
            }.AsQueryable();

            _clienteRepository.Setup(
                x => x.SearchByAsync(It.IsAny<Expression<Func<Cliente, bool>>>())).ReturnsAsync(clienteFake);

            var target = new ClienteInfraestructura(_clienteRepository.Object,
                _clientePatioRepository.Object, _config.Object, _fileHelper.Object);

            Cliente cliente = new Cliente
            {
                Id = 0,
                Identificacion = "1784956665"
            };

            //Act
            var actualResult = await target.CrearClienteAsync(cliente);

            //Assert
            Assert.IsFalse(actualResult.IsSuccessfull);
        }

        [Test]
        public async Task CrearClienteAsync_IdentificacionNoExiste_IsSuccessfullIgualTrue()
        {
            //Arrange
            var _clienteRepository = new Mock<IRepository<Cliente>>();
            var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();
            var _fileHelper = new Mock<IFileHelper<Cliente>>();
            var _config = new Mock<IConfiguration>();

            IQueryable<Cliente> clienteFake = new List<Cliente> { }.AsQueryable();

            _clienteRepository.Setup(
                x => x.SearchByAsync(It.IsAny<Expression<Func<Cliente, bool>>>())).ReturnsAsync(clienteFake);

            var target = new ClienteInfraestructura(_clienteRepository.Object,
                _clientePatioRepository.Object, _config.Object, _fileHelper.Object);

            Cliente cliente = new Cliente
            {
                Id = 0,
                Identificacion = "1784956665"
            };

            //Act
            var actualResult = await target.CrearClienteAsync(cliente);

            //Assert
            Assert.IsTrue(actualResult.IsSuccessfull);
        }

        [Test]
        public async Task ActualizarClienteAsync_IdentificacionExiste_IsSuccessfullIgualFalse()
        {
            //Arrange
            var _clienteRepository = new Mock<IRepository<Cliente>>();
            var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();
            var _fileHelper = new Mock<IFileHelper<Cliente>>();
            var _config = new Mock<IConfiguration>();

            IQueryable<Cliente> clienteFake = new List<Cliente> {
                 new Cliente
                    {
                        Id = 2,
                        Identificacion = "1784956668"
                    }
            }.AsQueryable();

            _clienteRepository.Setup(
                x => x.SearchByAsync(It.IsAny<Expression<Func<Cliente, bool>>>())).ReturnsAsync(clienteFake);

            var target = new ClienteInfraestructura(_clienteRepository.Object,
                _clientePatioRepository.Object, _config.Object, _fileHelper.Object);

            Cliente cliente = new Cliente
            {
                Id = 1,
                Identificacion = "1784956668"
            };

            //Act
            var actualResult = await target.ActualizarClienteAsync(cliente);

            //Assert
            Assert.IsFalse(actualResult.IsSuccessfull);
        }

        [Test]
        public async Task ActualizarClienteAsync_IdentificacionNoExiste_IsSuccessfullIgualTrue()
        {
            //Arrange
            var _clienteRepository = new Mock<IRepository<Cliente>>();
            var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();
            var _fileHelper = new Mock<IFileHelper<Cliente>>();
            var _config = new Mock<IConfiguration>();

            IQueryable<Cliente> clienteFake = new List<Cliente> {}.AsQueryable();

            _clienteRepository.Setup(
                x => x.SearchByAsync(It.IsAny<Expression<Func<Cliente, bool>>>())).ReturnsAsync(clienteFake);

            var target = new ClienteInfraestructura(_clienteRepository.Object,
                _clientePatioRepository.Object, _config.Object, _fileHelper.Object);

            Cliente cliente = new Cliente
            {
                Id = 1,
                Identificacion = "1784956668"
            };

            //Act
            var actualResult = await target.ActualizarClienteAsync(cliente);

            //Assert
            Assert.IsTrue(actualResult.IsSuccessfull);
        }

        [Test]
        public async Task EliminarClienteAsync_ClienteExiste_IsSuccessfullIgualTrue()
        {
            //Arrange
            var _clienteRepository = new Mock<IRepository<Cliente>>();
            var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();
            var _fileHelper = new Mock<IFileHelper<Cliente>>();
            var _config = new Mock<IConfiguration>();

            _clienteRepository.Setup(
                 x => x.DeleteEntityAsync(It.IsAny<int>())).Returns(Task.FromResult(true));
            _clienteRepository.Setup(
                x => x.SaveAsync()).ReturnsAsync(1);

            var target = new ClienteInfraestructura(_clienteRepository.Object,
                _clientePatioRepository.Object, _config.Object, _fileHelper.Object);

            //Act
            var actualResult = await target.EliminarClienteAsync(1);

            //Assert
            Assert.IsTrue(actualResult.IsSuccessfull);
        }

        [Test]
        public async Task CargaInicialAsync_IdentificacionesRepetidas_RemoverElementosDuplicados()
        {
            #region Arrange
            string ubicacionArchivo = "C:\\Users\\PC\\UbicacionDefault";
            int expectedCount = 2;
            var _clienteRepository = new Mock<IRepository<Cliente>>();
            var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();
            var _fileHelper = new Mock<IFileHelper<Cliente>>();
            var _config = new Mock<IConfiguration>();

            List<Cliente> clientesFake = new List<Cliente>
            {
                new Cliente
                {
                    Identificacion = "1",
                    Nombres = "Juan Carlos",
                },
                new Cliente
                {
                    Identificacion = "2",
                    Nombres = "Luis Paul"
                },
                new Cliente {
                    Identificacion = "2",
                    Nombres = "Luis Paul"
                }
            };

            IQueryable<Patio> patiosFake = new List<Patio>
            {
                new Patio
                {
                    Id = 1,
                    Nombre = "Patio1",
                    Codigo = "PT1"
                },
                new Patio
                {
                    Id = 2,
                    Nombre = "Patio2",
                    Codigo = "PT2"
                },
                new Patio
                {
                    Id = 3,
                    Nombre = "Patio3",
                    Codigo = "PT3"
                }
            }.AsQueryable();

            _config.Setup(p => p.GetSection(It.IsAny<string>()).Value).Returns(ubicacionArchivo);
            _fileHelper.Setup(f => f.LeerArchivoCSV<ClienteMap>(It.IsAny<string>())).Returns(clientesFake);
            

            var target = new ClienteInfraestructura(_clienteRepository.Object,
               _clientePatioRepository.Object, _config.Object, _fileHelper.Object);
            #endregion

            #region Act
            var clientesResult = await target.CargaInicialAsync();
            #endregion

            #region Assert
            Assert.That(expectedCount, Is.EqualTo(clientesResult.Data.Count));
            #endregion
        }

        [Test]
        public async Task CargaInicialAsync_CantidadClientesEsCero_ReturnListaVacia()
        {
            #region Arrange
            string ubicacionArchivo = "C:\\Users\\PC\\UbicacionDefault";
            int expectedCount = 0;
            var _clienteRepository = new Mock<IRepository<Cliente>>();
            var _clientePatioRepository = new Mock<IRepository<ClientePatio>>();
            var _fileHelper = new Mock<IFileHelper<Cliente>>();
            var _config = new Mock<IConfiguration>();

            List<Cliente> clientesFake = new List<Cliente>{};

            _config.Setup(p => p.GetSection(It.IsAny<string>()).Value).Returns(ubicacionArchivo);
            _fileHelper.Setup(f => f.LeerArchivoCSV<ClienteMap>(It.IsAny<string>())).Returns(clientesFake);


            var target = new ClienteInfraestructura(_clienteRepository.Object,
               _clientePatioRepository.Object, _config.Object, _fileHelper.Object);
            #endregion

            #region Act
            var clientesResult = await target.CargaInicialAsync();
            #endregion

            #region Assert
            Assert.That(expectedCount, Is.EqualTo(clientesResult.Data.Count));
            #endregion
        }
    }
}
