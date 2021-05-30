using NPOI.Compression;
using System;
using System.IO;

namespace NPOI.SharpZipLib
{
    class ZipEntry : IZipEntry
    {
        internal ICSharpCode.SharpZipLib.Zip.ZipEntry Inner { get; }

        public string Name => this.Inner.Name;

        public long Size => throw new NotImplementedException();

        public ZipEntry(ICSharpCode.SharpZipLib.Zip.ZipEntry zipEntry)
        {
            this.Inner = zipEntry;
        }

        public ZipEntry(string name) : this(new ICSharpCode.SharpZipLib.Zip.ZipEntry(name))
        {
        }

        public Stream GetInputStream()
        {
            throw new NotImplementedException();
        }
    }
}
