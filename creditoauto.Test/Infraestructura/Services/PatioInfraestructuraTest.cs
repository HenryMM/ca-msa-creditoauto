
using creditoauto.Domain.Interfaces;
using creditoauto.Entity.Models;
using creditoauto.Infraestructure.Services;
using Moq;

namespace creditoauto.Test.Infraestructura.Services
{
    public class PatioInfraestructuraTest
    {
        [Test]
        public async Task ObtenerPatioAsync_PatioExiste_ReturnPatio()
        {
            //Arrange
            int expectedPatioId = 1;
            var _patioRepository = new Mock<IRepository<Patio>>();
            _patioRepository.Setup(x => x.GetEntityByIdAsync(
                It.IsAny<int>())).ReturnsAsync(new Patio
                {
                    Id = 1,
                    Nombre = "Patio test"
                });
            var target = new PatioInfraestructura(_patioRepository.Object);

            //Act
            var patio = await target.ObtenerPatioAsync(1);

            //Assert
            Assert.That(patio.Data.Id, Is.EqualTo(expectedPatioId));
        }

        [Test]
        public async Task CrearPatioAsync_PatioEsCorrecto_IsSuccessfullIgualTrue()
        {
            //Arrange
            var _patioRepository = new Mock<IRepository<Patio>>();

            _patioRepository.Setup(
                x => x.CreateEntityAsync(It.IsAny<Patio>())).ReturnsAsync(new Patio
                {
                    Id= 1,
                    Nombre= "Patio test"
                });
            _patioRepository.Setup(
                x => x.SaveAsync()).ReturnsAsync(1);

            var target = new PatioInfraestructura(_patioRepository.Object);

            Patio patio = new Patio
            {
                Id = 0,
                Nombre = "Patio test"
            };

            //Act
            var actualResult = await target.CrearPatioAsync(patio);

            //Assert
            Assert.IsTrue(actualResult.IsSuccessfull);
        }

        [Test]
        public async Task ActualizarPatioAsync_PatioEsCorrecto_IsSuccessfullIgualTrue()
        {
            //Arrange
            var _patioRepository = new Mock<IRepository<Patio>>();

            _patioRepository.Setup(
                x => x.UpdateEntityAsync(It.IsAny<Patio>())).ReturnsAsync(new Patio
                {
                    Id = 1,
                    Nombre = "Patio test"
                });
            _patioRepository.Setup(
                x => x.SaveAsync()).ReturnsAsync(1);

            var target = new PatioInfraestructura(_patioRepository.Object);

            Patio patio = new Patio
            {
                Id = 1,
                Nombre = "Patio test1"
            };

            //Act
            var actualResult = await target.ActualizarPatioAsync(patio);

            //Assert
            Assert.IsTrue(actualResult.IsSuccessfull);
        }

        [Test]
        public async Task EliminarPatioAsync_PatioExiste_IsSuccessfullIgualTrue()
        {
            //Arrange
            var _patioRepository = new Mock<IRepository<Patio>>();

            _patioRepository.Setup(
                 x => x.DeleteEntityAsync(It.IsAny<int>())).Returns(Task.FromResult(true));
            _patioRepository.Setup(
                x => x.SaveAsync()).ReturnsAsync(1);

            var target = new PatioInfraestructura(_patioRepository.Object);

            //Act
            var result = await target.EliminarPatioAsync(1);

            //Assert
            Assert.IsTrue(result.IsSuccessfull);
        }
    }
}
