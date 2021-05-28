using System.Collections.Generic;

namespace NPOI.Compression
{
    public interface IZipFile: IEnumerable<IZipEntry>
    {
        void Close();
    }
}
