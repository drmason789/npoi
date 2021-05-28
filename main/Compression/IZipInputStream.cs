using System.IO;

namespace NPOI.Compression
{
    public interface IZipInputStream
    {
        Stream ToStream();

        IZipEntry GetNextEntry();

        int Read(byte[] buffer, int offset, int count);

        void Close();
    }
}
