using NPOI.Compression;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.SharpZipLib
{
    class ZipFile : IZipFile
    {
        internal ICSharpCode.SharpZipLib.Zip.ZipFile Inner { get; }

        public ZipFile(Stream stream)
        {
            this.Inner = new ICSharpCode.SharpZipLib.Zip.ZipFile(stream);
        }
              
        public IEnumerator<IZipEntry> GetEnumerator()
        {
            foreach(ICSharpCode.SharpZipLib.Zip.ZipEntry entry in this.Inner)
                yield return new ZipEntry(entry);
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public void Close()
        {
            this.Inner.Close();
        }
    }
    
}
