using creditoauto.Domain.Interfaces;
using creditoauto.Entity.Models;
using Moq;
using creditoauto.Infraestructure.Services;
using Microsoft.Extensions.Configuration;

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
    }
}
