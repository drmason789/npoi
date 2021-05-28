using System;
using System.IO;

namespace NPOI.Compression.DotNet
{
    class ZipOutputStream : Stream, IZipOutputStream
    {
        internal System.IO.Compression.ZipArchive Inner { get; private set; }

        public ZipOutputStream(Stream stream)
        {
            this.Inner = new System.IO.Compression.ZipArchive(stream);
        }

        #region IZipOutputStream

        public Zip64 Zip64
        {
            get => Zip64.Off;
            set
            {
                if (value != Zip64.Off)
                    throw new NotSupportedException();
            }
        }

        public void CloseEntry()
        {
            throw new System.NotImplementedException();
        }

        public IZipEntry CreateEntry(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Finish() 
        { 
        }

        public void PutNextEntry(IZipEntry entry)
        {
            if (!(entry is ZipEntry ze))
                throw new ArgumentException($"{typeof(ZipEntry).FullName} expected.");

            ze.AddToStream(this);
        }
        #endregion

        public override void Flush()
        {
            throw new System.NotImplementedException();
        }

        public override bool CanRead => throw new System.NotImplementedException();

        public override bool CanSeek => throw new System.NotImplementedException();

        public override bool CanWrite => throw new System.NotImplementedException();

        public override long Length => throw new System.NotImplementedException();

        public override long Position { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
             

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new System.NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new System.NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new System.NotImplementedException();
        }

        public Stream ToStream()
        {
            throw new System.NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new System.NotImplementedException();
        }
    }
}
