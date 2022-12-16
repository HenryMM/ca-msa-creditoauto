using creditoauto.Common.ClassMaps;
using creditoauto.Domain.Interfaces;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;
using Microsoft.Extensions.Configuration;

namespace creditoauto.Infraestructure.Services
{
    public class EjecutivoInfraestructura : IEjecutivoInfraestructura
    {
        private IRepository<Ejecutivo> _repositoryEjecutivo;
        private IRepository<Patio> _repositoryPatio;
        private readonly IFileHelper<Ejecutivo> _fileHelper;
        private readonly IConfiguration _config;

        public EjecutivoInfraestructura(IRepository<Ejecutivo> repositoryEjecutivo,
            IRepository<Patio> repositoryPatio,
            IConfiguration config,
            IFileHelper<Ejecutivo> fileHelper)
        {
            _repositoryEjecutivo = repositoryEjecutivo;
            _repositoryPatio = repositoryPatio;
            _config = config;
            _fileHelper = fileHelper;
        }

        #region Métodos Públicos
        public async Task<RespuestaGenerica<List<Ejecutivo>>> CargaInicialAsync()
        {
            List<Ejecutivo> ejecutivos = ObtenerEjecutivos();

            await SetPatio(ejecutivos);
            await CrearEjecutivosAsync(ejecutivos);
            return new RespuestaGenerica<List<Ejecutivo>>
            {
                Data = ejecutivos,
                IsSuccessfull = true
            };
        }

        public async Task<List<Ejecutivo>> CrearEjecutivosAsync(List<Ejecutivo> ejecutivos)
        {
            await _repositoryEjecutivo.CreateEntitiesAsync(ejecutivos);
            await _repositoryEjecutivo.SaveAsync();
            return ejecutivos;
        }
        #endregion

        #region Métodos Privados
        private async Task SetPatio(List<Ejecutivo> ejecutivos)
        {
            bool patioExiste = true;
            List<string> codigos = ejecutivos.Select(e => e.CodigoPatio).Distinct().ToList();

            var queryResult = await _repositoryPatio.SearchByAsync(p => codigos.Contains(p.Codigo));
            var codigosPatio = queryResult.Select(c => new
            {
                PatioId = c.Id,
                Codigo = c.Codigo
            }).ToList();

            ejecutivos.ForEach(ejecutivo =>
            {
                int patioId = codigosPatio.Where(
                    c => c.Codigo == ejecutivo.CodigoPatio).Select(c => c.PatioId).FirstOrDefault();
                if (patioId == 0) patioExiste = false;
                ejecutivo.PatioId = patioId;
            });

            if (!patioExiste)
            {
                ejecutivos.RemoveAll(e => e.PatioId == 0);
            }
        }
        private List<Ejecutivo> ObtenerEjecutivos()
        {
            string ubicacionArchivo = _config.GetSection("UbicacionArchivoEjecutivos").Value;

            List<Ejecutivo> ejecutivos = _fileHelper.LeerArchivoCSV<EjecutivoMap>(ubicacionArchivo);

            if (ejecutivos.Count > 1)
            {
                ejecutivos = ejecutivos.DistinctBy(x => x.Identificacion).ToList();
            }

            return ejecutivos;
        }

        #endregion
    }
}
