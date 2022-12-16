
using creditoauto.Domain.Interfaces;

namespace creditoauto.Common
{
    public class FileManager : IFileManager
    {
        public StreamReader StreamReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
