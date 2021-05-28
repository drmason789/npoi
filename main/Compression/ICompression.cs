using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPOI.Compression
{
    public enum Zip64
    {
        Off,
        On,
        Dynamic
    }
    public interface ICompression
    {

        bool SupportsZip64 { get; }
              

        byte[] Inflate(byte[] deflatedBytes);
        byte[] Deflate(byte[] bytes);
        byte[] Unzip(byte[] zippedBytes);
        byte[] Zip(byte[] bytes);

        IZipInputStream CreateZipInputStream(Stream stream);
        IZipOutputStream CreateZipOutputStream(Stream stream);

        IZipFile CreateZipFile(Stream stream);

        IZipEntry CreateZipEntry(string name);

        Stream CreateGzipInputStream(Stream stream);
        Stream CreateGzipOutputStream(Stream stream);

    }
}
