//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using PhotoAssistant.Controls.Wpf;
//using PhotoAssistant.Core.Model;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Media.Imaging;

//namespace PhotoAssistant.Tests {
//    [TestClass]
//    public class WatermarkImageGeneratorTests {

//        [TestMethod]
//        public void TestMethod() {
//            WatermarkImageGenerator generator = new WatermarkImageGenerator();
//            WatermarkParameters pars = new WatermarkParameters();
//            pars.ShowWatermark = true;
//            pars.Text = "Copyright (c) Arsen";
//            pars.FontSize = 20;
//            pars.Layout = WatermarkLayout.TopLeft;
//            pars.Opacity = 0.5;
//            RenderTargetBitmap bmp = generator.GenerateWatermarkImage(pars, 400, 600, 96, 96).Bitmap;

//            PngBitmapEncoder encoder = new PngBitmapEncoder();
//            encoder.Frames.Add(BitmapFrame.Create(bmp));
//            using(Stream s = File.Create("c:\\watermark.png")) {
//                encoder.Save(s);
//            }

//            bool foundColor = false;
//            System.Drawing.Bitmap img = (System.Drawing.Bitmap)System.Drawing.Image.FromFile("c:\\watermark.png");
//            for(int i = 0; i < img.Width; i++) {
//                for(int j = 0; j < img.Height; j++) {
//                    System.Drawing.Color c = img.GetPixel(i, j);
//                    if(c.A != 0 && c.A != 255) {
//                        foundColor = true;
//                        break;
//                    }
//                }
//                if(foundColor)
//                    break;
//            }
//            Assert.AreEqual(true, foundColor);
//        }
//    }
//}
