using System;
using System.IO;
using System.IO.Compression;

namespace NPOI.Compression.DotNet
{
    public class Compression : ICompression
    {
        public bool SupportsZip64 => false;

        private byte [] Flate(byte [] bytes, CompressionMode mode)
        {
            using (MemoryStream inStream = new MemoryStream(bytes))
            {
                System.IO.Compression.DeflateStream ds = new System.IO.Compression.DeflateStream(inStream, mode);
                using (MemoryStream outStream = new MemoryStream())
                {
                    ds.CopyTo(outStream);
                    return outStream.ToArray();
                }
            }
        }

        public byte[] Deflate(byte[] bytes) => Flate(bytes,CompressionMode.Compress);
        public byte[] Inflate(byte[] bytes) => Flate(bytes, CompressionMode.Decompress);

        public byte[] Unzip(byte[] zippedBytes)
        {
            using(MemoryStream zipStream = new MemoryStream(zippedBytes))
                using(ZipArchive zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Read, false))
            {

            }

            return null;            
        }

        public byte[] Zip(byte[] bytes)
        {
            throw new NotImplementedException();
        }


        public IZipEntry CreateZipEntry(string name) => new ZipEntry(name);
        public IZipInputStream CreateZipInputStream(Stream stream) => new ZipInputStream(stream);

        public IZipOutputStream CreateZipOutputStream(Stream stream) => new ZipOutputStream(stream);

        public IZipFile CreateZipFile(Stream stream) => new ZipFile(stream);

        public Stream CreateGzipInputStream(Stream stream) => new System.IO.Compression.GZipStream(stream, CompressionMode.Decompress);

        public Stream CreateGzipOutputStream(Stream stream) => new System.IO.Compression.GZipStream(stream, CompressionMode.Compress);
       
    }
}
