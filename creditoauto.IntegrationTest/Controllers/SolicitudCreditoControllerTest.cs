using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace creditoauto.IntegrationTest.Controllers
{
    public class SolicitudCreditoControllerTest : BaseControllerTests
    {
        public SolicitudCreditoControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task CrearSolicitudCredito_ReturnSolicitudCreditoCreado()
        {
            //Arrange
            var client = this.GetNewClient();

            var solicitudCredito = new SolicitudCredito
            {
                Id = 0,
                ClienteId = 1,
                Cuotas = 4,
                EjecutivoId = 5,
                Entrada = 20,
                Estado = "Activo",
                FechaElaboracion = DateTime.Now,
                MesesPlazo = 12,
                Observacion = "Observacion test",
                VehiculoId = 1
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(solicitudCredito), Encoding.UTF8, "application/json");

            //Act
            var response1 = await client.PostAsync("/api/v1/SolicitudCredito", stringContent);
            response1.EnsureSuccessStatusCode();

            var stringResponse1 = await response1.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<RespuestaGenerica<SolicitudCredito>>(stringResponse1);

            //Assert
            Assert.NotNull(response.Data);
        }

    }
}
