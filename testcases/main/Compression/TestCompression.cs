using NPOI.Compression;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestCases.Compression
{
    
    [TestFixture(typeof(NPOI.Compression.DotNet.Compression))]
    [TestFixture(typeof(NPOI.SharpZipLib.Compression))]
    public class TestCompression
    {
        protected ICompression Compression { get;  }

        public TestCompression(Type compressionType)
        {
            if (!compressionType.GetInterfaces().Contains(typeof(ICompression)))
                throw new ArgumentException($"Type argument must implement {nameof(ICompression)}", nameof(compressionType));

            this.Compression = (ICompression)Activator.CreateInstance(compressionType);
        }

        [Test]
        public void TestInflateBytes()
        {
            byte[] inflated = this.Compression.Inflate(Resources.Deflated);
            CollectionAssert.AreEqual(Resources.Inflated, inflated);
        }

        [Test]
        public void TestDeflateBytes()
        {
            byte[] deflated = this.Compression.Deflate(Resources.Inflated);
                        
            CollectionAssert.AreEqual(Resources.Deflated, deflated);
        }
        
        [Explicit]
        [Test]
        public void UpdateInflateTestData()
        {
            const string filename = "Deflated.bin";

            byte[] deflated = this.Compression.Deflate(Resources.Inflated);


            // Dump the file on the user's desktop. They can copy the
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        

            File.WriteAllBytes(Path.Combine(desktopPath, filename), deflated);

            Assert.Pass($"Update the file in the Compression\\Resources folder with the new {filename} and rebuild.");

        }
              

        [Test]
        public void TestUnzipBytes()
        {
            byte[] unzipped = this.Compression.Unzip(Resources.Zipped);
            CollectionAssert.AreEqual(Resources.Inflated, unzipped);
        }

        [Test]
        public void TestZipBytes()
        {
            byte[] zipped = this.Compression.Zip(Resources.Inflated);

            CollectionAssert.AreEqual(Resources.Zipped, zipped);
        }

        [Explicit]
        [Test]
        public void UpdateZipTestData()
        {
            const string filename = "Zipped.bin";

            byte[] zipped = this.Compression.Zip(Resources.Inflated);


            // Dump the file on the user's desktop. They can copy the
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            File.WriteAllBytes(Path.Combine(desktopPath, filename), zipped);

            Assert.Pass($"Update the file in the Compression\\Resources folder with the new {filename} and rebuild.");

        }
    }
}
