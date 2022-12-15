using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace creditoauto.Common
{
    /// <summary>
    /// Permite leer archivos externos.
    /// </summary>
    /// <typeparam name="T">El tipo de objeto que va a devolver un método</typeparam>
    public class FileHelper<T> where T : class
    {
        /// <summary>
        /// Permite leer un archivo archivo CSV y devolver un listado con la información proporcionada.
        /// </summary>
        /// <typeparam name="K">Class Map</typeparam>
        /// <param name="ubicacionArchivo">La ubicación fisica del archivo</param>
        /// <returns>Un listado de tipo T</returns>
        public List<T> LeerArchivoCSV<K>(string ubicacionArchivo) where K : ClassMap
        {
            List<T> records = new List<T>();
            using (var streamReader = new StreamReader(ubicacionArchivo))
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
