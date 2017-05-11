#if DEBUG
using NUnit.Framework;
using PhotoAssistant.Indexer.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Indexer.Tests {
    public class BaseFixture {
        static BaseFixture() {
            Indexer.InitDCRaw();
        }
        public string GetFile(string fileName) {
            return string.Format(@"{0}\{1}", @"..\..\TestData", fileName);
        }
    }
    [TestFixture]
    public class SimpleLinkTests {
        [Test]
        public void Test() {
            InitialImage ris = new InitialImage(IntPtr.Zero, false);
            ris.Dispose();
        }
        [Test]
        public void Test2() {
            RawImageSource ris = new RawImageSource();
            ris.Dispose();
        }

    }
    [TestFixture]
    public class FunctionalTests : BaseFixture {
        [Test]
        public void GetJpegThumbTest() {
            string fName = "DSC00841.JPG";
            PreviewImage image = new PreviewImage(GetFile(fName), "jpg", PreviewImage.PreviewImageMode.PIM_EmbeddedOrRaw);
            image.Dispose();
        }
        [Test]
        public void GetJpegThumbTest_NoThumb() {

        }
    }
}

#endif