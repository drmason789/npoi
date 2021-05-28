using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.Compression.DotNet
{

    public class ZipFile : IZipFile
    {
        public ZipFile(Stream stream)
        {

        }

        public void Close()
        {

        }

        public IEnumerator<IZipEntry> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
