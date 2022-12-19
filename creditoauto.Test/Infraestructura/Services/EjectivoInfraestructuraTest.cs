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
        public async Task CargaInicialAsync_CantidadEjecutivosMayorAUno_RemoverElementosDuplicados()
        {
            #region Arrange
            int expectedCount = 2;
            string ubicacionArchivo = "C:\\Users\\PC\\UbicacionDefault";
            var _repositoryEjecutivo = new Mock<IRepository<Ejecutivo>>();
            var _repositoryPatio = new Mock<IRepository<Patio>>();
            var _configuracion = new Mock<IConfiguration>();
            var _fileHelper = new Mock<IFileHelper<Ejecutivo>>();
            IEjecutivoInfraestructura _ejecutivoInfraestructura = new EjecutivoInfraestructura(_repositoryEjecutivo.Object,
                _repositoryPatio.Object, _configuracion.Object, _fileHelper.Object);

            List<Ejecutivo> ejecutivos = new List<Ejecutivo>
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
            _configuracion.Setup(p => p.GetSection(It.IsAny<string>()).Value).Returns(ubicacionArchivo);
            _fileHelper.Setup(f => f.LeerArchivoCSV<EjecutivoMap>(It.IsAny<string>())).Returns(ejecutivos);
            _repositoryPatio.Setup(p => p.SearchByAsync(It.IsAny<Expression<Func<Patio, bool>>>()));

            #endregion

            #region Act
            var ejecutivosResult = await _ejecutivoInfraestructura.CargaInicialAsync();
            #endregion

            #region Assert
            Assert.Equals(ejecutivosResult.Data.Count, expectedCount);
            #endregion
        }
    }
}
