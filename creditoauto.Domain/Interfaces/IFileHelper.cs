using CsvHelper.Configuration;

namespace creditoauto.Domain.Interfaces
{
    public interface IFileHelper<T> where T : class
    {
        List<T> LeerArchivoCSV<K>(string? ubicacionArchivo) where K: ClassMap;
    }
}
