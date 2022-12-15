
using System.ComponentModel.DataAnnotations.Schema;

namespace creditoauto.Entity.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Identificacion { get; set; }
        public string EstadoCivil { get; set; }
        public string IdentificacionConyuge { get; set; }
        public string NombreConyuge { get; set; }
        public string SujetoCredito { get; set; }
    }
}
