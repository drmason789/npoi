using System;
using System.IO;
using System.IO.Compression;

namespace NPOI.Compression.DotNet
{
    class ZipInputStream : Stream, IZipInputStream
    {
        Stream innerStream;
        ZipArchive zipArchive;
        int currentEntryIndex = -1;

        public ZipInputStream(Stream stream)
        {
            this.innerStream = stream;
            this.zipArchive = new ZipArchive(stream);            
        }

        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public IZipEntry GetNextEntry()
        {
            if (this.zipArchive.Entries.Count <= this.currentEntryIndex)
                return null;

            var entry = this.zipArchive.Entries[this.currentEntryIndex++];
                       
            return new ZipEntry(entry);

        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public Stream ToStream() => this;

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }


    }
}
