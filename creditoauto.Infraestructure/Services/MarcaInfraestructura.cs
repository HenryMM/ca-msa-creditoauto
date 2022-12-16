using creditoauto.Common;
using creditoauto.Common.ClassMaps;
using creditoauto.Domain.Interfaces;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Microsoft.Extensions.Configuration;

namespace creditoauto.Infraestructure.Services
{
    public class MarcaInfraestructura: IMarcaInfraestructura
    {
        private IRepository<Marca> _repositoryMarca;
        private readonly IFileHelper<Marca> _fileHelper;
        private readonly IConfiguration _config;

        public MarcaInfraestructura(IRepository<Marca> repositoryMarca, IConfiguration config, IFileHelper<Marca> fileHelper)
        {
            _repositoryMarca = repositoryMarca;
            _config = config;
            _fileHelper = fileHelper;
        }

        #region Métodos Públicos
        public async Task<RespuestaGenerica<List<Marca>>> CargaInicialAsync()
        {
            List<Marca> clientes = ObtenerMarcas();
            await CrearMarcasAsync(clientes);
            return new RespuestaGenerica<List<Marca>>
            {
                Data = clientes,
                IsSuccessfull = true
            };
        }

        public async Task<List<Marca>> CrearMarcasAsync(List<Marca> clientes)
        {

            await _repositoryMarca.CreateEntitiesAsync(clientes);
            await _repositoryMarca.SaveAsync();
            return clientes;
        }
        #endregion

        #region Métodos Privados
        private List<Marca> ObtenerMarcas()
        {
            string ubicacionArchivo = _config.GetSection("UbicacionArchivoMarcas").Value;

            List<Marca> marcas = _fileHelper.LeerArchivoCSV<MarcaMap>(ubicacionArchivo);

            if (marcas.Count > 1)
            {
                marcas = marcas.DistinctBy(x => x.Nombre).ToList();
            }

            return marcas;
        }

        #endregion
    }
}
