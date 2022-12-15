using creditoauto.Common;
using creditoauto.Domain.Interfaces;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Microsoft.Extensions.Configuration;

namespace creditoauto.Infraestructure.Services
{
    public class ClienteInfraestructura : IClienteService
    {

        private IRepository<Cliente> _repositoryCliente;
        private readonly IConfiguration _config;

        public ClienteInfraestructura(IRepository<Cliente> repositoryCliente, IConfiguration config)
        {
            _repositoryCliente = repositoryCliente;
            _config = config;
        }

        #region Métodos Públicos
        public async Task<RespuestaGenerica<List<Cliente>>> CargaInicialAsync()
        {
            List<Cliente> clientes = ObtenerClientes();
            await CrearClientesAsync(clientes);
            return new RespuestaGenerica<List<Cliente>>
            {
                Data = clientes,
                IsSuccessfull = true
            };
        }

        public async Task<List<Cliente>> CrearClientesAsync(List<Cliente> clientes)
        {

            await _repositoryCliente.CreateEntitiesAsync(clientes);
            await _repositoryCliente.SaveAsync();
            return clientes;
        }

        public List<Cliente> LeerArchivo(string ubicacionArchivo)
        {
            List<Cliente> clientes = new List<Cliente>();
            using (StreamReader archivo = new StreamReader(ubicacionArchivo))
            {
                archivo.ReadLine();
                string fila;
                while ((fila = archivo.ReadLine()) != null)
                {
                    Console.WriteLine(fila);
                    string[] datosCliente = fila.Split(";");
                    var cliente = new Cliente
                    {
                        Identificacion = datosCliente[0],
                        Nombres = datosCliente[1],
                        Edad = int.Parse(datosCliente[2]),
                        FechaNacimiento = DateTime.Parse(datosCliente[3]),
                        Apellidos = datosCliente[4],
                        Direccion = datosCliente[5],
                        Telefono = datosCliente[6],
                        EstadoCivil = datosCliente[7],
                        IdentificacionConyuge = datosCliente[8],
                        NombreConyuge = datosCliente[9],
                        SujetoCredito = datosCliente[10]
                    };
                    clientes.Add(cliente);
                }
                clientes = clientes.DistinctBy(x => x.Identificacion).ToList();
            }
            return clientes;
        }
        #endregion

        #region Métodos Privados
        private List<Cliente> ObtenerClientes()
        {
            FileHelper<Cliente> fileHelper = new FileHelper<Cliente>();
            string ubicacionArchivo = _config.GetSection("UbicacionArchivo").Value;

            if (string.IsNullOrEmpty(ubicacionArchivo))
            {
                throw new ArgumentException("La ubicación del archivo es invalida");
            }

            List<Cliente> clientes = fileHelper.LeerArchivoCSV<ClienteMap>(ubicacionArchivo);

        }



        #endregion
    }
}
