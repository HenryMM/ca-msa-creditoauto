using creditoauto.Entity.DTO;
using creditoauto.Entity.Enums;
using creditoauto.Entity.Models;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace creditoauto.IntegrationTest.Controllers
{
    public class VehiculoControllerTest : BaseControllerTests
    {
        public VehiculoControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {

        }

        [Fact]
        public async Task ObtenerVehiculo_ReturnRecord()
        {
            //Arrange
            var client = this.GetNewClient();

            //Act
            var response = await client.GetAsync("/api/v1/Vehiculo?vehiculoId=1");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RespuestaGenerica<Vehiculo>>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            //Assert
            Assert.Equal("OK", statusCode);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task CrearVehiculo_ReturnVehiculoCreado()
        {
            //Arrange
            var client = this.GetNewClient();

            var vehiculo = new Vehiculo
            {
                Id = 0,
                Avaluo = 2000,
                Modelo = "M1",
                MarcaId = 1,
                Cilindraje = "1765",
                NumeroChasis = "195",
                Placa = "PRT-8458",
                Tipo = "T1"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(vehiculo), Encoding.UTF8, "application/json");

            //Act
            var response1 = await client.PostAsync("/api/v1/Vehiculo", stringContent);
            response1.EnsureSuccessStatusCode();

            var stringResponse1 = await response1.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<RespuestaGenerica<Vehiculo>>(stringResponse1);

            //Assert
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task CrearVehiculo_InvalidData_ReturnError()
        {
            //Arrange
            var client = this.GetNewClient();

            var vehiculo = new Vehiculo
            {
                Id = 0,
                Avaluo = 2000,
                Modelo = "M1",
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(vehiculo), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("/api/v1/Vehiculo", stringContent);
            var statusCode = response.StatusCode.ToString();

            //Assert
            Assert.Equal("BadRequest", statusCode);
        }

        [Fact]
        public async Task ActualizaVehiculo_ReturnCLienteActualizado()
        {
            //Arrange
            string excpectedPlaca = "PRT-8458";
            var client = this.GetNewClient();

            var vehiculo = new Vehiculo
            {
                Id = 1,
                Avaluo = 2000,
                Modelo = "M1",
                MarcaId = 1,
                Cilindraje = "1765",
                NumeroChasis = "195",
                Placa = "PRT-8458",
                Tipo = "T1"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(vehiculo), Encoding.UTF8, "application/json");

            //Act
            var response1 = await client.PutAsync("/api/v1/Vehiculo", stringContent);
            response1.EnsureSuccessStatusCode();

            var stringResponse1 = await response1.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<RespuestaGenerica<Vehiculo>>(stringResponse1);

            //Assert
            Assert.Equal(excpectedPlaca, response.Data.Placa);
        }

        [Fact]
        public async Task EliminarVehiculo_ReturnMensaje()
        {
            //Arrange
            string excpectedMensaje = Parametro.Eliminado;
            var client = this.GetNewClient();

            //Act
            var response = await client.DeleteAsync("/api/v1/Vehiculo?idVehiculo=1");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RespuestaGenerica<string>>(stringResponse);


            //Assert
            Assert.Equal(excpectedMensaje, result.Data);
        }
    }
}
