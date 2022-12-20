using creditoauto.Common.ClassMaps;
using creditoauto.Common;
using creditoauto.Domain.Interfaces;
using creditoauto.Entity.Models;
using Moq;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Infraestructure.Services;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace creditoauto.Test.Infraestructura.Services
{
    public class EjectivoInfraestructuraTest
    {
        [Test]
        public async Task CargaInicialAsync_IdentificacionesRepetidas_RemoverElementosDuplicados()
        {
            #region Arrange
            string ubicacionArchivo = "C:\\Users\\PC\\UbicacionDefault";
            int expectedCount = 2;
            var _repositoryEjecutivo = new Mock<IRepository<Ejecutivo>>();
            var _repositoryPatio = new Mock<IRepository<Patio>>();
            var _configuracion = new Mock<IConfiguration>();
            var _fileHelper = new Mock<IFileHelper<Ejecutivo>>();
            IEjecutivoInfraestructura _ejecutivoInfraestructura = new EjecutivoInfraestructura(_repositoryEjecutivo.Object,
                _repositoryPatio.Object, _configuracion.Object, _fileHelper.Object);

            List<Ejecutivo> ejecutivosFake = new List<Ejecutivo>
            {
                new Ejecutivo
                {
                    Identificacion = "1",
                    Nombres = "Juan Carlos",
                    CodigoPatio = "PT1"
                },
                new Ejecutivo
                {
                    Identificacion = "2",
                    Nombres = "Luis Paul",
                    CodigoPatio = "PT2"
                },
                new Ejecutivo {
                    Identificacion = "2",
                    Nombres = "Luis Paul",
                    CodigoPatio = "PT2"
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

            _configuracion.Setup(p => p.GetSection(It.IsAny<string>()).Value).Returns(ubicacionArchivo);
            _fileHelper.Setup(f => f.LeerArchivoCSV<EjecutivoMap>(It.IsAny<string>())).Returns(ejecutivosFake);
            _repositoryPatio.Setup(p => p.SearchByAsync(It.IsAny<Expression<Func<Patio, bool>>>())).ReturnsAsync(patiosFake);

            #endregion

            #region Act
            var ejecutivosResult = await _ejecutivoInfraestructura.CargaInicialAsync();
            #endregion

            #region Assert
            Assert.That(expectedCount, Is.EqualTo(ejecutivosResult.Data.Count));
            #endregion
        }

        [Test]
        public async Task CargaInicialAsync_CantidadEjecutivosEsCero_ReturnListaVacia()
        {
            #region Arrange
            string ubicacionArchivo = "C:\\Users\\PC\\UbicacionDefault";
            int expectedCount = 0;
            var _repositoryEjecutivo = new Mock<IRepository<Ejecutivo>>();
            var _repositoryPatio = new Mock<IRepository<Patio>>();
            var _configuracion = new Mock<IConfiguration>();
            var _fileHelper = new Mock<IFileHelper<Ejecutivo>>();
            IEjecutivoInfraestructura _ejecutivoInfraestructura = new EjecutivoInfraestructura(_repositoryEjecutivo.Object,
                _repositoryPatio.Object, _configuracion.Object, _fileHelper.Object);

            List<Ejecutivo> ejecutivosFake = new List<Ejecutivo>{};

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

            _configuracion.Setup(p => p.GetSection(It.IsAny<string>()).Value).Returns(ubicacionArchivo);
            _fileHelper.Setup(f => f.LeerArchivoCSV<EjecutivoMap>(It.IsAny<string>())).Returns(ejecutivosFake);
            _repositoryPatio.Setup(p => p.SearchByAsync(It.IsAny<Expression<Func<Patio, bool>>>())).ReturnsAsync(patiosFake);

            #endregion

            #region Act
            var ejecutivosResult = await _ejecutivoInfraestructura.CargaInicialAsync();
            #endregion

            #region Assert
            Assert.That(expectedCount, Is.EqualTo(ejecutivosResult.Data.Count));
            #endregion
        }
    }
}
