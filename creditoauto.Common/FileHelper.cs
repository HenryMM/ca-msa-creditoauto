using creditoauto.Domain.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace creditoauto.Common
{
    /// <summary>
    /// Permite leer archivos externos.
    /// </summary>
    /// <typeparam name="T">El tipo de objeto que va a devolver un método</typeparam>
    public class FileHelper<T> : IFileHelper<T> where T : class
    {
        IFileManager _fileManager;
        public FileHelper(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        /// <summary>
        /// Permite leer un archivo archivo CSV y devolver un listado de tipo T con la información obtenida.
        /// </summary>
        /// <typeparam name="K">Class Map</typeparam>
        /// <param name="ubicacionArchivo">La ubicación fisica del archivo</param>
        /// <returns>Un listado de tipo T</returns>
        public List<T> LeerArchivoCSV<K>(string? ubicacionArchivo) where K : ClassMap
        {
            if (string.IsNullOrEmpty(ubicacionArchivo))
            {
                throw new ArgumentException("La ubicación del archivo es inválida");
            }
            List<T> records = new List<T>();
            using (var streamReader = _fileManager.StreamReader(ubicacionArchivo))
            {
                using(var scvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    scvReader.Context.RegisterClassMap<K>();
                    records = scvReader.GetRecords<T>().ToList();
                }
            }
            return records;
        }

       

    }
}
