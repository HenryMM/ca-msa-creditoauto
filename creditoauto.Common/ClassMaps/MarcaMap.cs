
using creditoauto.Entity.Models;
using CsvHelper.Configuration;

namespace creditoauto.Common.ClassMaps
{
    public class MarcaMap : ClassMap<Marca>
    {
        public MarcaMap()
        {
            Map(m => m.Id).Ignore();
            Map(m => m.Nombre).Name("Nombre");
            Map(m => m.Descripcion).Name("Descripcion");
            
        }
    }
}
