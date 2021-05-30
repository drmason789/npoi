using System;
using System.IO;
using System.IO.Compression;

namespace NPOI.Compression.DotNet
{
    class ZipInputStream: Stream, IZipInputStream
    {
        
        ZipArchive zipArchive;
        int currentEntryIndex = -1;
        ZipArchiveEntry openEntry = null;
        Stream openEntryStream = null;

        public ZipInputStream(Stream stream)
        {
            this.zipArchive = new ZipArchive(stream);            
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => openEntry.Length;

        public override long Position
        { 
            get => this.openEntryStream.Position;
            set => throw new NotSupportedException(); 
        }

        public void Close()
        {
            this.openEntryStream.Close();
            this.openEntry = null;
            this.openEntryStream = null;

        }

        public override void Flush()
        {
            this.openEntryStream.Flush();
        }

        public IZipEntry GetNextEntry()
        {
            if (this.currentEntryIndex >= this.zipArchive.Entries.Count-1)
                return null;

            this.currentEntryIndex++;
            this.openEntry = this.zipArchive.Entries[currentEntryIndex];
            this.openEntryStream = this.openEntry.Open();
            
                                              
            return new ZipEntry(this.openEntry);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.openEntryStream.Read(buffer, offset, count);
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
