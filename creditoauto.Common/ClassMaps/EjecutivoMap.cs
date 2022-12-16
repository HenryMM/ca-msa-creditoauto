using creditoauto.Entity.Models;
using CsvHelper.Configuration;

namespace creditoauto.Common.ClassMaps
{
    public class EjecutivoMap : ClassMap<Ejecutivo>
    {
        public EjecutivoMap()
        {
            Map(m => m.Id).Ignore();
            Map(m => m.Identificacion).Name("Identificacion");
            Map(m => m.Nombres).Name("Nombres");
            Map(m => m.Apellidos).Name("Apellidos");
            Map(m => m.Direccion).Name("Direccion"); 
            Map(m => m.TelefonoConvencional).Name("TelefonoConvencional");
            Map(m => m.Celular).Name("Celular");
            Map(m => m.Edad).Name("Edad");
            Map(m => m.CodigoPatio).Name("Patio");
        }
    }
}
