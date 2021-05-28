using System.IO;

namespace NPOI.Compression
{
    public interface IZipEntry
    {
        string Name { get; }

        long Size { get; }

        Stream GetInputStream();
    }
}
