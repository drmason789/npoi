using NPOI.Compression;
using System.IO;

namespace NPOI.SharpZipLib
{
    class ZipInputStream : IZipInputStream
    {
        ICSharpCode.SharpZipLib.Zip.ZipInputStream zipInputStream;
        public ZipInputStream(Stream stream)
        {
            zipInputStream = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(stream);

        }

        public Stream ToStream() => this.zipInputStream;

        public void Close()
        {
            zipInputStream.Close();
        }

        public IZipEntry GetNextEntry()
        {
            var nextEntry = zipInputStream.GetNextEntry();

            if (nextEntry != null)
                return new ZipEntry(nextEntry);

            return null;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return this.zipInputStream.Read(buffer, offset, count);
        }
    }
    
}
