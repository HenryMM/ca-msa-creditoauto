using System.ComponentModel.DataAnnotations.Schema;

namespace creditoauto.Entity.Models
{
    [Table("ClientePatio")]
    public class ClientePatio
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int PatioId { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }
}
