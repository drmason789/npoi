﻿namespace NPOI.Compression
{
    public static class Compression
    {
        public static ICompression Instance { get; set; } = new DotNet.Compression();
    }
}
