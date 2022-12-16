using creditoauto.Entity.Models;
using CsvHelper.Configuration;

namespace creditoauto.Common.ClassMaps
{
    public class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Map(m => m.Id).Ignore();
            Map(m => m.Identificacion).Name("Identificacion");
            Map(m => m.Nombres).Name("Nombres");
            Map(m => m.Edad).Name("Edad");
            Map(m => m.FechaNacimiento).Name("FechaNacimiento").TypeConverterOption.Format("dd/MM/yyyy"); ;
            Map(m => m.Apellidos).Name("Apellidos");
            Map(m => m.Direccion).Name("Direccion");
            Map(m => m.Telefono).Name("Telefono");
            Map(m => m.EstadoCivil).Name("EstadoCivil");
            Map(m => m.IdentificacionConyuge).Name("IdentificacionConyuge");
            Map(m => m.NombreConyuge).Name("NombreConyuge");
            Map(m => m.SujetoCredito).Name("SujetoCredito");
        }
    }
}
