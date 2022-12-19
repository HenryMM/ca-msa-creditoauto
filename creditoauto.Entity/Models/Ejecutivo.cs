using System.ComponentModel.DataAnnotations.Schema;

namespace creditoauto.Entity.Models
{
    [Table("Ejecutivo")]
    public partial class Ejecutivo
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string TelefonoConvencional { get; set; }
        public string Celular { get; set; }
        public string Edad { get; set; }
        public int PatioId { get; set; }
        public Patio Patio { get; set; }
    }
}
