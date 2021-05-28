using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using NPOI.Compression;

namespace NPOI.OpenXml4Net.Util
{
    /**
     * A ZipEntrySource wrapper around a ZipFile.
     * Should be as low in terms of memory as a
     *  normal ZipFile implementation is.
     */
    public class ZipFileZipEntrySource : ZipEntrySource
    {
        private IZipFile zipArchive;
        public ZipFileZipEntrySource(IZipFile zipFile)
        {
            this.zipArchive = zipFile;
        }

        public void Close()
        {
            if (zipArchive != null)
            {
                zipArchive.Close();
            }
            zipArchive = null;
        }

        public bool IsClosed
        {
            get { return zipArchive == null; }
        }

        public IEnumerator Entries
        {
            get
            {
                if (zipArchive == null)
                    throw new InvalidDataException("Zip File is closed");
                return zipArchive.GetEnumerator();

            }
        }

        public Stream GetInputStream(IZipEntry entry)
        {
            if (zipArchive == null)
                throw new InvalidDataException("Zip File is closed");
            return entry.GetInputStream();
            //Stream s = zipArchive.GetInputStream(entry);
            //return s;
        }
    }
}
