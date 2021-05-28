using System;
using System.IO;
using System.IO.Compression;

namespace NPOI.Compression.DotNet
{
    class ZipEntry : IZipEntry
    {
        internal System.IO.Compression.ZipArchiveEntry Inner { get; private set; }

        public string Name { get; private set; }

        public long Size { get; }

        public ZipEntry(ZipArchiveEntry entry)
        {
            this.Inner = entry;
            this.Name = this.Inner.Name;
        }

        public ZipEntry(string name)
        {
            //this.zipArchiveEntry = entry;
            this.Name = name;
        }

        public void AddToStream(IZipOutputStream stream)
        {
            if (!(stream is ZipOutputStream zos))
                throw new ArgumentException($"{typeof(ZipOutputStream).FullName} expected.", nameof(stream));

            this.Inner = zos.Inner.CreateEntry(this.Name);
        }

        public Stream GetInputStream()
        {
            throw new NotImplementedException();
        }
    }
}
