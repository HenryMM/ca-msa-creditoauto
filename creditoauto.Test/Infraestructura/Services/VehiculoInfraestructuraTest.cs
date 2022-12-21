using creditoauto.Domain.Interfaces;
using creditoauto.Entity.Enums;
using creditoauto.Entity.Models;
using creditoauto.Infraestructure.Services;
using Moq;
using System.Linq.Expressions;

namespace creditoauto.Test.Infraestructura.Services
{
    public class VehiculoInfraestructuraTest
    {

        [Test]
        public async Task ObtenerVehiculoAsync_VehiculoExiste_ReturnVehiculo()
        {
            //Arrange
            int expectedVehiculoId = 1;
            var _vehiculoRepository = new Mock<IRepository<Vehiculo>>();
            _vehiculoRepository.Setup(x => x.GetEntityByIdAsync(
                It.IsAny<int>())).ReturnsAsync(new Vehiculo
                {
                    Id = 1,
                    MarcaId = 2,
                    Placa = "POS-4784"
                });
            var target = new VehiculoInfraestructura(_vehiculoRepository.Object);

            //Act
            var vehiculo = await target.ObtenerVehiculoAsync(1);

            //Assert
            Assert.AreEqual(expectedVehiculoId, vehiculo.Data.Id);
        }

        [Test]
        public async Task CrearVehiculoAsync_PlacaExiste_IsSuccessfullIgualFalso()
        {
            //Arrange
            string placa = "POS-4784";
            var _vehiculoRepository = new Mock<IRepository<Vehiculo>>();

            IQueryable<Vehiculo> vehiculoFake = new List<Vehiculo>
            {
                new Vehiculo
                {
                    Id = 1,
                    MarcaId = 2,
                    Placa = placa
                }
            }.AsQueryable();

            _vehiculoRepository.Setup(
                x => x.SearchByAsync(It.IsAny<Expression<Func<Vehiculo, bool>>>())).ReturnsAsync(vehiculoFake);

            var target = new VehiculoInfraestructura(_vehiculoRepository.Object);

            Vehiculo vehiculo = new Vehiculo
            {
                MarcaId = 2,
                Placa = placa
            };

            //Act
            var actualResult = await target.CrearVehiculoAsync(vehiculo);

            //Assert
            Assert.IsFalse(actualResult.IsSuccessfull);
        }

        [Test]
        public async Task CrearVehiculoAsync_PlacaNoExiste_IsSuccessfullIgualTrue()
        {
            //Arrange
            string placa = "POS-4784";
            var _vehiculoRepository = new Mock<IRepository<Vehiculo>>();

            IQueryable<Vehiculo> vehiculoFake = new List<Vehiculo> { }.AsQueryable();

            _vehiculoRepository.Setup(
                x => x.SearchByAsync(It.IsAny<Expression<Func<Vehiculo, bool>>>())).ReturnsAsync(vehiculoFake);

            var target = new VehiculoInfraestructura(_vehiculoRepository.Object);

            Vehiculo vehiculo = new Vehiculo
            {
                MarcaId = 2,
                Placa = placa
            };

            //Act
            var actualResult = await target.CrearVehiculoAsync(vehiculo);

            //Assert
            Assert.IsTrue(actualResult.IsSuccessfull);
        }

        [Test]
        public async Task ActualizarVehiculoAsync_PlacaExiste_IsSuccessfullIgualFalse()
        {
            //Arrange
            string placa = "POS-4784";
            var _vehiculoRepository = new Mock<IRepository<Vehiculo>>();

            IQueryable<Vehiculo> vehiculoFake = new List<Vehiculo>
            {
                new Vehiculo
                {
                    Id = 1,
                    MarcaId = 2,
                    Placa = placa
                }
            }.AsQueryable();

            _vehiculoRepository.Setup(
                x => x.SearchByAsync(It.IsAny<Expression<Func<Vehiculo, bool>>>())).ReturnsAsync(vehiculoFake);

            var target = new VehiculoInfraestructura(_vehiculoRepository.Object);

            Vehiculo vehiculo = new Vehiculo
            {
                MarcaId = 2,
                Placa = placa
            };

            //Act
            var actualResult = await target.ActualizarVehiculoAsync(vehiculo);

            //Assert
            Assert.IsFalse(actualResult.IsSuccessfull);
        }

        [Test]
        public async Task ActualizarVehiculoAsync_PlacaNoExiste_IsSuccessfullIgualTrue()
        {
            //Arrange
            string placa = "POS-4784";
            var _vehiculoRepository = new Mock<IRepository<Vehiculo>>();

            IQueryable<Vehiculo> vehiculoFake = new List<Vehiculo>{}.AsQueryable();

            _vehiculoRepository.Setup(
                x => x.SearchByAsync(It.IsAny<Expression<Func<Vehiculo, bool>>>())).ReturnsAsync(vehiculoFake);

            var target = new VehiculoInfraestructura(_vehiculoRepository.Object);

            Vehiculo vehiculo = new Vehiculo
            {
                MarcaId = 2,
                Placa = placa
            };

            //Act
            var actualResult = await target.ActualizarVehiculoAsync(vehiculo);

            //Assert
            Assert.IsTrue(actualResult.IsSuccessfull);
        }

        [Test]
        public async Task EliminarVehiculoAsync_VehiculoExiste_IsSuccessfullIgualTrue()
        {
            //Arrange
            var _vehiculoRepository = new Mock<IRepository<Vehiculo>>();

            _vehiculoRepository.Setup(
                x => x.DeleteEntityAsync(It.IsAny<int>())).Returns(Task.FromResult(true));
            _vehiculoRepository.Setup(
                x => x.SaveAsync()).ReturnsAsync(1);
            var target = new VehiculoInfraestructura(_vehiculoRepository.Object);

            //Act
            var vehiculo = await target.EliminarVehiculoAsync(1);

            //Assert
            Assert.That(vehiculo.Data, Is.EqualTo(Parametro.Eliminado));
        }
    }
}
