using System;
using System.IO;
using System.IO.Compression;

namespace NPOI.Compression.DotNet
{
    public class Compression : ICompression
    {
        public bool SupportsZip64 => false;

      
        public byte[] Inflate(byte[] bytes)
        {
            // Consuming 2 bytes for the 78 9C (Sometimes other like 78 DA)
            // .Net Deflate Stream expects the data to not have this header.
            // https://stackoverflow.com/questions/762614/how-do-you-use-a-deflatestream-on-part-of-a-file
            // http://www.faqs.org/rfcs/rfc1950.html
            // https://stackoverflow.com/questions/20850703/cant-inflate-with-c-sharp-using-deflatestream
            int offset = 0;
            if (bytes.Length > 2 && bytes[0] == 0x78)
                // Skip RFC195x header
                offset = 2;

            using (MemoryStream outStream = new MemoryStream())
            {
                using (MemoryStream inStream = new MemoryStream(bytes, offset, bytes.Length - offset))
                using (System.IO.Compression.DeflateStream ds = new System.IO.Compression.DeflateStream(inStream, CompressionMode.Decompress))
                    ds.CopyTo(outStream);
                return outStream.ToArray();
            }
                
            

        }

        public byte[] Deflate(byte[] bytes)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                // Write the RFC1950 header
                outStream.WriteByte(0x78);
                outStream.WriteByte(0x9C);
                
                using (System.IO.Compression.DeflateStream ds = new System.IO.Compression.DeflateStream(outStream, CompressionMode.Compress,true))
                    ds.Write(bytes, 0, bytes.Length);

                // Write RFC-1950 footer
                outStream.WriteByte(0x53);
                outStream.WriteByte(0xCA);
                return outStream.ToArray();
            }
        }

        public byte[] Unzip(byte[] zippedBytes)
        {
            using (MemoryStream zipStream = new MemoryStream(zippedBytes))
            using (ZipArchive zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Read, false))
            {
                if (zipArchive.Entries.Count > 1)
                    throw new Exception("Single entry expected.");

                ZipArchiveEntry ze = zipArchive.Entries[0];

                MemoryStream outputStream = new MemoryStream();
                ze.Open().CopyTo(outputStream);
                return outputStream.ToArray();
            }
        }

        public byte[] Zip(byte[] bytes)
        {
            using(MemoryStream zipStream = new MemoryStream())
            using(ZipArchive zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                var entry = zipArchive.CreateEntry("1");
                var outputStream = entry.Open();
                outputStream.Write(bytes, 0, bytes.Length);
                return zipStream.ToArray();
            }
        }

        public IZipEntry CreateZipEntry(string name) => new ZipEntry(name);
        public IZipInputStream CreateZipInputStream(Stream stream) => new ZipInputStream(stream);

        public IZipOutputStream CreateZipOutputStream(Stream stream) => new ZipOutputStream(stream);

        public IZipFile CreateZipFile(Stream stream) => new ZipFile(stream);

        public Stream CreateGzipInputStream(Stream stream) => new System.IO.Compression.GZipStream(stream, CompressionMode.Decompress);

        public Stream CreateGzipOutputStream(Stream stream) => new System.IO.Compression.GZipStream(stream, CompressionMode.Compress);
       
    }
}
