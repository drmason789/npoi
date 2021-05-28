using System.IO;

namespace NPOI.Compression
{
    public interface IZipOutputStream
    {
        Zip64 Zip64 { get; set; }

        void PutNextEntry(IZipEntry entry);

        void Write(byte[] buffer, int offset, int count);

        void CloseEntry();

        Stream ToStream();


        void Finish();
        void Close();
    }

}
