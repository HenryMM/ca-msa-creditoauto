
namespace creditoauto.Entity.DTO
{
    public class ClienteDTO
    {
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Apellidos { get; set; }
        public string Direccion {get;set; }
        public string Telefono { get; set; }
        public string EstadoCivil { get; set; }
        public string IdentificacionConyuge { get; set; }   
        public string NombreConyuge { get; set; }
        public string SujetoCredito { get; set; }
    }
}
