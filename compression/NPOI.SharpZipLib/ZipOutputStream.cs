using ICSharpCode.SharpZipLib.Zip;
using NPOI.Compression;
using System;
using System.IO;

namespace NPOI.SharpZipLib
{
    class ZipOutputStream : IZipOutputStream
    {
        private object[][] zipUsages = new object[][] { new object[] { UseZip64.On, Zip64.On }, new object [] { UseZip64.Off, Zip64.Off}, new object[]{ UseZip64.Dynamic, Zip64.Dynamic} };

        private ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipOutputStream;

        private static Zip64 ToZip64(UseZip64 useZip64) => (Zip64)useZip64;
        private static UseZip64 ToUseZip64(Zip64 zip64) => (UseZip64)zip64;

        static ZipOutputStream()
        {
            // Do a test to make sure the (Use)?Zip64 enums still match up so the conversion functions above work.
            if ((int)Zip64.On != (int)UseZip64.On || (int)Zip64.Off != (int)UseZip64.Off || (int)Zip64.Dynamic != (int)Zip64.Dynamic || Enum.GetValues(typeof(UseZip64)).Length != Enum.GetValues(typeof(Zip64)).Length)
                throw new Exception($"{typeof(UseZip64).FullName} does not match {typeof(Zip64).FullName}.");
        }

        public ZipOutputStream(Stream stream)
        {
            this.zipOutputStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(stream);
        }

        public Zip64 Zip64
        {
            get => ToZip64(this.zipOutputStream.UseZip64);
            set => this.zipOutputStream.UseZip64 = ToUseZip64(value);
        }

        public void CloseEntry()
        {
            this.zipOutputStream.CloseEntry();
        }

        public IZipEntry CreateEntry(string name)
        {
            return new ZipEntry(new ICSharpCode.SharpZipLib.Zip.ZipEntry(name));
        }

        public void PutNextEntry(IZipEntry entry)
        {
            if (!(entry is ZipEntry ze))
                throw new ArgumentException($"{typeof(ZipEntry).FullName} expected.", nameof(entry));

            this.zipOutputStream.PutNextEntry(ze.Inner);
        }

        public Stream ToStream()
        {
            return this.zipOutputStream;
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            this.zipOutputStream.Write(buffer, offset, count);
        }

        public void Finish()
        {
            this.zipOutputStream.Finish();
        }

        public void Close()
        {
            this.zipOutputStream.Close();
        }
    }
}
