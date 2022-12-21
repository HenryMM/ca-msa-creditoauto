using creditoauto.Entity.DTO;
using creditoauto.Entity.Enums;
using creditoauto.Entity.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace creditoauto.IntegrationTest.Controllers
{
    public class PatioControllerTest : BaseControllerTests
    {
        public PatioControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {

        }

        [Fact]
        public async Task ObtenerPatio_ReturnRecord()
        {
            //Arrange
            var client = this.GetNewClient();

            //Act
            var response = await client.GetAsync("/api/v1/Patio?patioId=1");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RespuestaGenerica<Patio>>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            //Assert
            Assert.Equal("OK", statusCode);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task CrearPatio_ReturnPatioCreado()
        {
            //Arrange
            var client = this.GetNewClient();

            var patio = new Patio
            {
                Id = 0,
                Nombre = "Patio1",
                Descripcion = "Test",
                Codigo = "PT1"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(patio), Encoding.UTF8, "application/json");

            //Act
            var response1 = await client.PostAsync("/api/v1/Patio", stringContent);
            response1.EnsureSuccessStatusCode();

            var stringResponse1 = await response1.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<RespuestaGenerica<Cliente>>(stringResponse1);

            //Assert
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task CrearPatio_InvalidData_ReturnError()
        {
            //Arrange
            var client = this.GetNewClient();

            var patio = new Patio
            {
                Id = 0,
                Nombre = "Patio1"
            };


            var stringContent = new StringContent(JsonConvert.SerializeObject(patio), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("/api/v1/Patio", stringContent);
            var statusCode = response.StatusCode.ToString();

            //Assert
            Assert.Equal("BadRequest", statusCode);
        }

        [Fact]
        public async Task ActualizaPatio_ReturnPatioActualizado()
        {
            //Arrange
            string excpectedNombre = "Patio1";
            var client = this.GetNewClient();

            var patio = new Patio
            {
                Id = 1,
                Nombre = "Patio1",
                Descripcion = "Test",
                Codigo = "PT1"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(patio), Encoding.UTF8, "application/json");

            //Act
            var response1 = await client.PutAsync("/api/v1/Patio", stringContent);
            response1.EnsureSuccessStatusCode();

            var stringResponse1 = await response1.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<RespuestaGenerica<Patio>>(stringResponse1);

            //Assert
            Assert.Equal(excpectedNombre, response.Data.Nombre);
        }

        [Fact]
        public async Task EliminarPatio_ReturnMensaje()
        {
            //Arrange
            string excpectedMensaje = Parametro.Eliminado;
            var client = this.GetNewClient();

            //Act
            var response = await client.DeleteAsync("/api/v1/Patio?idPatio=1");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RespuestaGenerica<string>>(stringResponse);


            //Assert
            Assert.Equal(excpectedMensaje, result.Data);
        }

    }
}
