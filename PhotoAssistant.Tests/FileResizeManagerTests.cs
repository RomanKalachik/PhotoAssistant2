//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using PhotoAssistant.Core.Model;

//namespace PhotoAssistant.Tests {
//    [TestClass]
//    public class FileResizeManagerTests {
//        [TestMethod]
//        public void TestZoomInside1() {
//            DmFile file = new DmFile();
//            file.Width = 3000;
//            file.Height = 3000;
//            file.Dpi = 300;

//            FileResizeManager.Default.Dpi = 600;
//            FileResizeManager.Default.Width = 6000;
//            FileResizeManager.Default.Height = 4000;
//            FileResizeManager.Default.DimensionMode = ImageDimensionMode.Pixels;
//            FileResizeManager.Default.ResizeMode = FileResizeMode.ZoomInside;
//            ResizeInfo info = FileResizeManager.Default.GetResizeInfo(file);
//            Assert.AreEqual(4000.0f, info.ImageSize.Width);
//            Assert.AreEqual(4000.0f, info.ImageSize.Height);
//            Assert.AreEqual(0.0f, info.SourceRect.X);
//            Assert.AreEqual(0.0f, info.SourceRect.Y);
//            Assert.AreEqual(3000.0f, info.SourceRect.Width);
//            Assert.AreEqual(3000.0f, info.SourceRect.Height);
//        }

//        [TestMethod]
//        public void TestZoomInside2() {
//            DmFile file = new DmFile();
//            file.Width = 3000;
//            file.Height = 3000;
//            file.Dpi = 300;

//            FileResizeManager.Default.Dpi = 600;
//            FileResizeManager.Default.Width = 4000;
//            FileResizeManager.Default.Height = 6000;
//            FileResizeManager.Default.DimensionMode = ImageDimensionMode.Pixels;
//            FileResizeManager.Default.ResizeMode = FileResizeMode.ZoomInside;
//            ResizeInfo info = FileResizeManager.Default.GetResizeInfo(file);
//            Assert.AreEqual(4000.0f, info.ImageSize.Width);
//            Assert.AreEqual(4000.0f, info.ImageSize.Height);
//            Assert.AreEqual(0.0f, info.SourceRect.X);
//            Assert.AreEqual(0.0f, info.SourceRect.Y);
//            Assert.AreEqual(3000.0f, info.SourceRect.Width);
//            Assert.AreEqual(3000.0f, info.SourceRect.Height);
//        }

//        [TestMethod]
//        public void TestZoomOutside1() {
//            DmFile file = new DmFile();
//            file.Width = 3000;
//            file.Height = 3000;
//            file.Dpi = 300;

//            FileResizeManager.Default.Dpi = 600;
//            FileResizeManager.Default.Width = 6000;
//            FileResizeManager.Default.Height = 4000;
//            FileResizeManager.Default.DimensionMode = ImageDimensionMode.Pixels;
//            FileResizeManager.Default.ResizeMode = FileResizeMode.ZoomOutside;
//            ResizeInfo info = FileResizeManager.Default.GetResizeInfo(file);
//            Assert.AreEqual(6000.0f, info.ImageSize.Width);
//            Assert.AreEqual(4000.0f, info.ImageSize.Height);
//            Assert.AreEqual(0.0f, info.SourceRect.X);
//            Assert.AreEqual(500.0f, info.SourceRect.Y);
//            Assert.AreEqual(3000.0f, info.SourceRect.Width);
//            Assert.AreEqual(2000.0f, info.SourceRect.Height);
//        }

//        [TestMethod]
//        public void TestZoomOutside2() {
//            DmFile file = new DmFile();
//            file.Width = 3000;
//            file.Height = 3000;
//            file.Dpi = 300;

//            FileResizeManager.Default.Dpi = 600;
//            FileResizeManager.Default.Width = 4000;
//            FileResizeManager.Default.Height = 6000;
//            FileResizeManager.Default.DimensionMode = ImageDimensionMode.Pixels;
//            FileResizeManager.Default.ResizeMode = FileResizeMode.ZoomOutside;
//            ResizeInfo info = FileResizeManager.Default.GetResizeInfo(file);
//            Assert.AreEqual(4000.0f, info.ImageSize.Width);
//            Assert.AreEqual(6000.0f, info.ImageSize.Height);
//            Assert.AreEqual(500.0f, info.SourceRect.X);
//            Assert.AreEqual(0.0f, info.SourceRect.Y);
//            Assert.AreEqual(2000.0f, info.SourceRect.Width);
//            Assert.AreEqual(3000.0f, info.SourceRect.Height);
//        }

//        [TestMethod]
//        public void TestLongSide() {
//            DmFile file = new DmFile();
//            file.Width = 4000;
//            file.Height = 2000;
//            file.Dpi = 300;

//            FileResizeManager.Default.Dpi = 600;
//            FileResizeManager.Default.Width = 4000;
//            FileResizeManager.Default.Height = 6000;
//            FileResizeManager.Default.LongSide = 8000;
//            FileResizeManager.Default.DimensionMode = ImageDimensionMode.Pixels;
//            FileResizeManager.Default.ResizeMode = FileResizeMode.LongSide;
//            ResizeInfo info = FileResizeManager.Default.GetResizeInfo(file);

//            Assert.AreEqual(8000, info.ImageSize.Width);
//            Assert.AreEqual(4000.0f, info.ImageSize.Height);

//            Assert.AreEqual(0.0f, info.SourceRect.X);
//            Assert.AreEqual(0.0f, info.SourceRect.Y);
//            Assert.AreEqual(4000.0f, info.SourceRect.Width);
//            Assert.AreEqual(2000.0f, info.SourceRect.Height);
//        }

//        [TestMethod]
//        public void TestShortSide() {
//            DmFile file = new DmFile();
//            file.Width = 4000;
//            file.Height = 2000;
//            file.Dpi = 300;

//            FileResizeManager.Default.Dpi = 600;
//            FileResizeManager.Default.Width = 4000;
//            FileResizeManager.Default.Height = 6000;
//            FileResizeManager.Default.ShortSide = 3000;
//            FileResizeManager.Default.DimensionMode = ImageDimensionMode.Pixels;
//            FileResizeManager.Default.ResizeMode = FileResizeMode.ShortSide;
//            ResizeInfo info = FileResizeManager.Default.GetResizeInfo(file);

//            Assert.AreEqual(6000.0f, info.ImageSize.Width);
//            Assert.AreEqual(3000.0f, info.ImageSize.Height);

//            Assert.AreEqual(0.0f, info.SourceRect.X);
//            Assert.AreEqual(0.0f, info.SourceRect.Y);
//            Assert.AreEqual(4000.0f, info.SourceRect.Width);
//            Assert.AreEqual(2000.0f, info.SourceRect.Height);
//        }

//        [TestMethod]
//        public void TestComplex() {
//            DmFile file = new DmFile();
//            file.Width = 4000;
//            file.Height = 2000;
//            file.Dpi = 100;

//            FileResizeManager.Default.Dpi = 600;
//            FileResizeManager.Default.Width = 60;
//            FileResizeManager.Default.Height = 60;
//            FileResizeManager.Default.DimensionMode = ImageDimensionMode.Centimeters;
//            FileResizeManager.Default.ResolutionMode = ResolutionMode.PixelsPerCm;
//            FileResizeManager.Default.ResizeMode = FileResizeMode.ZoomInside;

//            float fileWidth = 4000 / 100;
//            float fileHeight = 2000 / 100;

//            float k = 6000f / 4000f;

//            float newFileWidth = fileWidth * k;
//            float newFileHeight = fileHeight * k;

//            float newWidthPixels = newFileWidth * FileResizeManager.Default.Dpi;
//            float newHeightPixels = newFileHeight * FileResizeManager.Default.Dpi;

//            ResizeInfo info = FileResizeManager.Default.GetResizeInfo(file);
//            Assert.AreEqual(newWidthPixels, info.ImageSize.Width);
//            Assert.AreEqual(newHeightPixels, info.ImageSize.Height);
//            Assert.AreEqual(0.0f, info.SourceRect.X);
//            Assert.AreEqual(0.0f, info.SourceRect.Y);
//            Assert.AreEqual(4000, info.SourceRect.Width);
//            Assert.AreEqual(2000, info.SourceRect.Height);
//        }
//    }
//}
