
using System.ComponentModel.DataAnnotations.Schema;

namespace creditoauto.Entity.Models
{
    [Table("Vehiculo")]
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string NumeroChasis { get; set; }
        public int MarcaId { get; set; }
        public string Tipo { get; set; }
        public string Cilindraje { get; set; }
        public int Avaluo { get; set; }
    }
}
