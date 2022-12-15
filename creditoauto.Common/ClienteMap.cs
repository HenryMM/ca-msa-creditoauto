using creditoauto.Entity.Models;
using CsvHelper.Configuration;

namespace creditoauto.Common
{
    public class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Map(m => m.Identificacion).Name("Identificacion");
            Map(m => m.Nombres).Name("Nombres");
            Map(m => m.Edad).Name("Edad");
            Map(m => m.Identificacion).Name("Identificacion");
            Map(m => m.Identificacion).Name("Identificacion");
            Map(m => m.Identificacion).Name("Identificacion");
            Map(m => m.Identificacion).Name("Identificacion");
            Map(m => m.Identificacion).Name("Identificacion");
            Map(m => m.Identificacion).Name("Identificacion");
            Map(m => m.Identificacion).Name("Identificacion");
        }
    }
}