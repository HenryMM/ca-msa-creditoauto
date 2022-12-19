
using System.ComponentModel.DataAnnotations.Schema;

namespace creditoauto.Entity.Models
{
    [Table("SolicitudCredito")]
    public class SolicitudCredito
    {
        public int Id { get; set; }
        public int MesesPlazo { get; set; }
        public int Cuotas { get; set; }
        public float Entrada { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
        public int ClienteId { get; set; }
        public int EjecutivoId { get; set; }
        public int VehiculoId { get; set; }
        public DateTime FechaElaboracion { get; set; }
    }
}
