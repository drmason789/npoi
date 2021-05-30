using System;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using NPOI.Compression;

namespace NPOI.SharpZipLib
{
    public class Compression : ICompression
    {
        public bool SupportsZip64 => throw new NotImplementedException();

        public Stream CreateGzipInputStream(Stream stream) => new GZipInputStream(stream);

        public Stream CreateGzipOutputStream(Stream stream) => new GZipOutputStream(stream);
        public IZipEntry CreateZipEntry(string name) => new ZipEntry(name);

        public IZipFile CreateZipFile(Stream stream) => new ZipFile(stream);

        public IZipInputStream CreateZipInputStream(Stream stream) => new NPOI.SharpZipLib.ZipInputStream(stream);

        public IZipOutputStream CreateZipOutputStream(Stream stream) => new NPOI.SharpZipLib.ZipOutputStream(stream);

        public byte[] Deflate(byte[] bytes)
        {
            // info of chicago project:
            // "... LZ compression algorithm in the format used by GNU Zip deflate/inflate with a 32k window ..."
            // not sure what to do, when lookup tables exceed 32k ...

            MemoryStream bos = new MemoryStream();
            DeflaterOutputStream dos = new DeflaterOutputStream(bos);
            
            dos.Write(bytes, 0, bytes.Length);
            dos.Close();
            return bos.ToArray();
        }

        public byte[] Inflate(byte[] deflatedBytes)
        {
            MemoryStream bis = new MemoryStream(deflatedBytes);
            InflaterInputStream iis = new InflaterInputStream(bis);
            MemoryStream os = new MemoryStream();
            iis.CopyTo(os);
            return os.ToArray();
        }

        public byte[] Unzip(byte[] zippedBytes)
        {
            MemoryStream bis = new MemoryStream(zippedBytes);
            ZipInputStream zis = new ZipInputStream(bis);
            MemoryStream os = new MemoryStream();
            zis.ToStream().CopyTo(os);
            return os.ToArray();
        }

        public byte[] Zip(byte[] bytes)
        {
            MemoryStream bos = new MemoryStream(bytes);
            ZipOutputStream zos = new ZipOutputStream(bos);
            zos.Write(bytes, 0, bytes.Length);
            return bos.ToArray();
        }
    }
}
