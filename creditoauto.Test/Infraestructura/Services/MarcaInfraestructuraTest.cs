using creditoauto.Common.ClassMaps;
using creditoauto.Domain.Interfaces;
using creditoauto.Entity.Models;
using creditoauto.Infraestructure.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditoauto.Test.Infraestructura.Services
{
    public class MarcaInfraestructuraTest
    {
        [Test]
        public async Task CargaInicialAsync_MarcasRepetidas_RemoverElementosDuplicados()
        {
            #region Arrange
            string ubicacionArchivo = "C:\\Users\\PC\\UbicacionDefault";
            int expectedCount = 2;
            var _marcaRepository = new Mock<IRepository<Marca>>();
            var _fileHelper = new Mock<IFileHelper<Marca>>();
            var _config = new Mock<IConfiguration>();

            List<Marca> marcasFake = new List<Marca>
            {
                new Marca
                {
                    Nombre = "Marca1"
                },
                new Marca
                {
                    Nombre = "Marca2"
                },
                new Marca {
                    Nombre = "Marca2"
                }
            };

            _config.Setup(p => p.GetSection(It.IsAny<string>()).Value).Returns(ubicacionArchivo);
            _fileHelper.Setup(f => f.LeerArchivoCSV<MarcaMap>(It.IsAny<string>())).Returns(marcasFake);


            var target = new MarcaInfraestructura(_marcaRepository.Object,
               _config.Object, _fileHelper.Object);
            #endregion

            #region Act
            var clientesResult = await target.CargaInicialAsync();
            #endregion

            #region Assert
            Assert.That(expectedCount, Is.EqualTo(clientesResult.Data.Count));
            #endregion
        }

        [Test]
        public async Task CargaInicialAsync_CantidadMarcasEsCero_ReturnListaVacia()
        {
            #region Arrange
            string ubicacionArchivo = "C:\\Users\\PC\\UbicacionDefault";
            int expectedCount = 0;
            var _marcaRepository = new Mock<IRepository<Marca>>();
            var _fileHelper = new Mock<IFileHelper<Marca>>();
            var _config = new Mock<IConfiguration>();

            List<Marca> marcasFake = new List<Marca>{};

            _config.Setup(p => p.GetSection(It.IsAny<string>()).Value).Returns(ubicacionArchivo);
            _fileHelper.Setup(f => f.LeerArchivoCSV<MarcaMap>(It.IsAny<string>())).Returns(marcasFake);


            var target = new MarcaInfraestructura(_marcaRepository.Object,
               _config.Object, _fileHelper.Object);
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
