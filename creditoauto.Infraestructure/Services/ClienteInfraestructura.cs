using creditoauto.Common.ClassMaps;
using creditoauto.Domain.Interfaces;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Microsoft.Extensions.Configuration;

namespace creditoauto.Infraestructure.Services
{
    public class ClienteInfraestructura : IClienteInfraestructura
    {

        private IRepository<Cliente> _repositoryCliente;
        private IRepository<ClientePatio> _repositoryClientePatio;
        private readonly IFileHelper<Cliente> _fileHelper;
        private readonly IConfiguration _config;

        public ClienteInfraestructura(IRepository<Cliente> repositoryCliente,
            IRepository<ClientePatio> repositoryClientePatio,
            IConfiguration config, 
            IFileHelper<Cliente> fileHelper)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryClientePatio = repositoryClientePatio;
            _config = config;
            _fileHelper = fileHelper;
        }

        #region Métodos Públicos

        public async Task<RespuestaGenerica<Cliente>> ObtenerClienteAsync( int clienteId)
        {
            Cliente cliente = await _repositoryCliente.GetEntityByIdAsync(clienteId);
            return new RespuestaGenerica<Cliente>
            {
                Data = cliente,
                IsSuccessfull = true
            };
        }
        public async Task<RespuestaGenerica<Cliente>> CrearClienteAsync(Cliente cliente)
        {
            await _repositoryCliente.CreateEntityAsync(cliente);
            await _repositoryCliente.SaveAsync();
            return new RespuestaGenerica<Cliente>
            {
                Data = cliente,
                IsSuccessfull = true
            };

        }

        public async Task<RespuestaGenerica<Cliente>> ActualizarClienteAsync(Cliente cliente)
        {
            await _repositoryCliente.UpdateEntityAsync(cliente);
            await _repositoryCliente.SaveAsync();
            return new RespuestaGenerica<Cliente>
            {
                Data = cliente,
                IsSuccessfull = true
            };
        }

        public async Task<RespuestaGenerica<string>> EliminarClienteAsync(int idCliente)
        {
            await _repositoryCliente.DeleteEntityAsync(idCliente);
            await _repositoryCliente.SaveAsync();
            return new RespuestaGenerica<string>
            {
                Data = "Ok",
                IsSuccessfull = true
            };
        }

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

        public async Task<RespuestaGenerica<ClientePatio>> AsignarPatio(ClientePatio clientePatio)
        {
            await _repositoryClientePatio.CreateEntityAsync(clientePatio);
            await _repositoryClientePatio.SaveAsync();
            return new RespuestaGenerica<ClientePatio>
            {
                Data = clientePatio,
                IsSuccessfull = true
            };
        }
        #endregion

        #region Métodos Privados
        private List<Cliente> ObtenerClientes()
        {
            string ubicacionArchivo = _config.GetSection("UbicacionArchivoClientes").Value;

            List<Cliente> clientes = _fileHelper.LeerArchivoCSV<ClienteMap>(ubicacionArchivo);

            if(clientes.Count > 1)
            {
                clientes = clientes.DistinctBy(x=>x.Identificacion).ToList();
            }

            return clientes;
        }

        #endregion
    }
}
