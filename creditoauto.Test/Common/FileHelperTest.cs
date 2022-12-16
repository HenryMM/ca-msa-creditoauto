using Castle.Core.Configuration;
using creditoauto.Common;
using creditoauto.Domain.Interfaces;
using creditoauto.Entity.Models;
using NSubstitute;

namespace creditoauto.Test.Common
{
    public class FileHelperTest
    {
        [Test]
        public void CargaInicialAsync_UbicacionArchivoEsNull_ThrowArgumentException()
        {
            #region Arrange
            string ubicacionArchivo = null;
            var _config = Substitute.For<IConfiguration>();
            var _repositoryCliente = Substitute.For<IRepository<Cliente>>();
            IFileHelper<Cliente> _fileHelper = new FileHelper<Cliente>();
            #endregion

            #region Act
            var ex = Assert.Throws<ArgumentException>( () =>_fileHelper.LeerArchivoCSV<ClienteMap>(ubicacionArchivo));
            #endregion

            #region Assert

            Assert.That(ex.Message == "La ubicación del archivo es inválida");
            #endregion
        }

        [Test]
        public void CargaInicialAsync_UbicacionArchivoEsVacio_ThrowArgumentException()
        {
            #region Arrange
            string ubicacionArchivo = string.Empty;
            var _config = Substitute.For<IConfiguration>();
            var _repositoryCliente = Substitute.For<IRepository<Cliente>>();
            IFileHelper<Cliente> _fileHelper = new FileHelper<Cliente>();
            #endregion

            #region Act
            var ex = Assert.Throws<ArgumentException>(() => _fileHelper.LeerArchivoCSV<ClienteMap>(ubicacionArchivo));
            #endregion

            #region Assert

            Assert.That(ex.Message == "La ubicación del archivo es inválida");
            #endregion
        }

        //[Test]
        //public async Task CargaInicialAsync_UbicacionArchivoEsCorrecto_GuardarClientes()
        //{
        //    #region Arrange
        //    string ubicacionArchivo = "C:\\Users\\UbicacionCorrecta";
        //    var _config = Substitute.For<IConfiguration>();
        //    var _repositoryCliente = Substitute.For<IRepository<Cliente>>();
        //    IClienteService clienteService = new ClienteInfraestructura(_repositoryCliente, _config);
        //    #endregion

        //    #region Act
        //    _config.GetSection("UbicacionArchivo").Value.Returns(ubicacionArchivo);
        //    clienteService.LeerArchivo(ubicacionArchivo).Returns(new List<Cliente>
        //    {
        //        new Cliente
        //        {
        //            Id= 1,
        //            Identificacion = "178888888"
        //        },
        //        new Cliente
        //        {
        //            Id= 12,
        //            Identificacion = "178888889"
        //        }
        //    });
        //    var clientes = await clienteService.CargaInicialAsync();
        //    #endregion

        //    #region Assert
        //    Assert.IsNotNull(clientes);
        //    #endregion
        //}
    }
}
