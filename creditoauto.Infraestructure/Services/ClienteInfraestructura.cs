using creditoauto.Common.ClassMaps;
using creditoauto.Domain.Interfaces;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Microsoft.Extensions.Configuration;

namespace creditoauto.Infraestructure.Services
{
    public class ClienteInfraestructura : IClienteInfraestructura
    {

        private IRepository<Cliente> _repositoryCliente;
        private readonly IFileHelper<Cliente> _fileHelper;
        private readonly IConfiguration _config;

        public ClienteInfraestructura(IRepository<Cliente> repositoryCliente, IConfiguration config, IFileHelper<Cliente> fileHelper)
        {
            _repositoryCliente = repositoryCliente;
            _config = config;
            _fileHelper = fileHelper;
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
