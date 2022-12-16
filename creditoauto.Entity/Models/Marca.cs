using System.ComponentModel.DataAnnotations.Schema;

namespace creditoauto.Entity.Models
{
    [Table("Marca")]
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; } 
    }
}
