using creditoauto.Common;
using creditoauto.Common.ClassMaps;
using creditoauto.Domain.Interfaces;
using creditoauto.Entity.Models;
using Moq;
using NSubstitute;
using System.Text;

namespace creditoauto.Test.Common
{
    public class FileHelperTest
    {
        [Test]
        public void CargaInicialAsync_UbicacionArchivoEsNull_ThrowArgumentException()
        {
            #region Arrange
            string ubicacionArchivo = null;
            var _fileManager = new Mock<IFileManager>();
            IFileHelper<Cliente> _fileHelper = new FileHelper<Cliente>(_fileManager.Object);
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
            var _fileManager = new Mock<IFileManager>();
            IFileHelper<Cliente> _fileHelper = new FileHelper<Cliente>(_fileManager.Object);
            #endregion

            #region Act
            var ex = Assert.Throws<ArgumentException>(() => _fileHelper.LeerArchivoCSV<ClienteMap>(ubicacionArchivo));
            #endregion

            #region Assert

            Assert.That(ex.Message == "La ubicación del archivo es inválida");
            #endregion
        }

        [Test]
        public void CargaInicialAsync_UbicacionArchivoEsInvalido_ThrowArgumentNullException()
        {
            #region Arrange
            string ubicacionArchivo = "C:\\Users\\PC\\UbicacionIncorrecta";
            var _fileManager = new Mock<IFileManager>();
            IFileHelper<Cliente> _fileHelper = new FileHelper<Cliente>(_fileManager.Object);
            #endregion

            #region Act
            var ex = Assert.Throws<ArgumentNullException>(() => _fileHelper.LeerArchivoCSV<ClienteMap>(ubicacionArchivo));
            #endregion

            #region Assert
            Assert.That(ex.Message == "Value cannot be null. (Parameter 'reader')");
            #endregion
        }

        [Test]
        public void CargaInicialAsync_UbicacionArchivoValido_ThrowArgumentException()
        {
            #region Arrange
            string ubicacionArchivo = "C:\\Users\\PC\\UbicacionCorrecta";
            var _fileManager = new Mock<IFileManager>();
            IFileHelper<Cliente> _fileHelper = new FileHelper<Cliente>(_fileManager.Object);
            #endregion

            #region Act
            StringBuilder stringBuilder = new StringBuilder("Test");
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
            _fileManager.Object.StreamReader(ubicacionArchivo).Returns(new StreamReader(memoryStream));
            var actual = _fileHelper.LeerArchivoCSV<ClienteMap>(ubicacionArchivo);
            #endregion

            #region Assert

            //Assert.That(ex.Message == "La ubicación del archivo es inválida");
            #endregion
        }
    }
}
