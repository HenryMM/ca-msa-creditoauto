using creditoauto.Domain.Interfaces;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.DTO;
using creditoauto.Entity.Models;

namespace creditoauto.Infraestructure.Services
{
    public class PatioInfraestructura : IPatioInfraestructura
    {

        private IRepository<Patio> _repositoryPatio;

        public PatioInfraestructura(IRepository<Patio> repositoryPatio)
        {
            _repositoryPatio = repositoryPatio;
        }

        public async Task<RespuestaGenerica<Patio>> ActualizarPatioAsync(Patio patio)
        {
            await _repositoryPatio.UpdateEntityAsync(patio);
            await _repositoryPatio.SaveAsync();
            return new RespuestaGenerica<Patio>
            {
                Data = patio,
                IsSuccessfull = true
            };
        }

        public async Task<RespuestaGenerica<Patio>> CrearPatioAsync(Patio patio)
        {
            await _repositoryPatio.CreateEntityAsync(patio);
            await _repositoryPatio.SaveAsync();
            return new RespuestaGenerica<Patio>
            {
                Data = patio,
                IsSuccessfull = true
            };
        }

        public async Task<RespuestaGenerica<string>> EliminarPatioAsync(int idPatio)
        {
            await _repositoryPatio.DeleteEntityAsync(idPatio);
            await _repositoryPatio.SaveAsync();
            return new RespuestaGenerica<string>
            {
                Data = "Ok",
                IsSuccessfull = true
            };
        }

        public async Task<RespuestaGenerica<Patio>> ObtenerPatioAsync(int idPatio)
        {
            Patio patio = await _repositoryPatio.GetEntityByIdAsync(idPatio);
            return new RespuestaGenerica<Patio>
            {
                Data = patio,
                IsSuccessfull = true
            };
        }
    }
}
