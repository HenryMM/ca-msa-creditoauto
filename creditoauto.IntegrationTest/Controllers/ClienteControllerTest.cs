using Azure;
using Azure.Core;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Enums;
using creditoauto.Entity.Models;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Text;
using Xunit;
using Assert = Xunit.Assert;

namespace creditoauto.IntegrationTest.Controllers
{
    public class ClienteControllerTest : BaseControllerTests
    {
        public ClienteControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task ObtenerCliente_ReturnRecord()
        {
            //Arrange
            var client = this.GetNewClient();

            //Act
            var response = await client.GetAsync("/api/v1/Cliente?clienteId=1");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RespuestaGenerica<Cliente>>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            //Assert
            Assert.Equal("OK", statusCode);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task CrearCliente_ReturnCLienteCreado()
        {
            //Arrange
            var client = this.GetNewClient();

            var cliente = new Cliente { 
                Id = 0,
                Apellidos = "Apellidos test",
                Direccion = "Direccion test",
                Identificacion = "184848848",
                Nombres = "Nombres test",
                Edad = 15,
                EstadoCivil = "Casado",
                FechaNacimiento = DateTime.Now.AddYears(-20),
                SujetoCredito = "Sujeto credito",
                Telefono = "0985475554",
                IdentificacionConyuge = "1722451253",
                NombreConyuge = "Nombre conyuge"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

            //Act
            var response1 = await client.PostAsync("/api/v1/Cliente", stringContent);
            response1.EnsureSuccessStatusCode();

            var stringResponse1 = await response1.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<RespuestaGenerica<Cliente>>(stringResponse1);

            //Assert
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task CrearCliente_InvalidData_ReturnError()
        {
            //Arrange
            var client = this.GetNewClient();

            var cliente = new Cliente
            {
                Id = 0,
                Apellidos = "Apellidos test",
                Direccion = "Direccion test"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("/api/v1/Cliente", stringContent);
            var statusCode = response.StatusCode.ToString();

            //Assert
            Assert.Equal("BadRequest", statusCode);
        }

        [Fact]
        public async Task ActualizaCliente_ReturnCLienteActualizado()
        {
            //Arrange
            string excpectedNombre = "Nombres test";
            var client = this.GetNewClient();

            var cliente = new Cliente
            {
                Id = 1,
                Apellidos = "Apellidos test",
                Direccion = "Direccion test",
                Identificacion = "184848848",
                Nombres = "Nombres test",
                Edad = 15,
                EstadoCivil = "Casado",
                FechaNacimiento = DateTime.Now.AddYears(-20),
                SujetoCredito = "Sujeto credito",
                Telefono = "0985475554",
                IdentificacionConyuge = "1722451253",
                NombreConyuge = "Nombre conyuge"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

            //Act
            var response1 = await client.PutAsync("/api/v1/Cliente", stringContent);
            response1.EnsureSuccessStatusCode();

            var stringResponse1 = await response1.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<RespuestaGenerica<Cliente>>(stringResponse1);

            //Assert
            Assert.Equal(excpectedNombre, response.Data.Nombres);
        }

        [Fact]
        public async Task EliminarCliente_ReturnMensaje()
        {
            //Arrange
            string excpectedMensaje = Parametro.Eliminado;
            var client = this.GetNewClient();

            //Act
            var response = await client.DeleteAsync("/api/v1/Cliente?idCliente=1");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RespuestaGenerica<string>>(stringResponse);
           

            //Assert
            Assert.Equal(excpectedMensaje, result.Data);
        }
    }
}
