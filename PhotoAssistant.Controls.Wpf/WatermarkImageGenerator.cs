using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace PhotoAssistant.Controls.Wpf {
    public class WatermarkImageGenerator {
        static WatermarkImageGenerator defaultGenerator;
        public static WatermarkImageGenerator Default {
            get {
                if(defaultGenerator == null) {
                    defaultGenerator = new WatermarkImageGenerator();
                }

                return defaultGenerator;
            }
        }
        public WatermarkImageGenerator() => Cache = new List<WatermarkInfo>();
        public WatermarkInfo GenerateWatermarkImage(WatermarkParameters watermark, int width, int height, int dpix, int dpiy) {
            WatermarkInfo info = Cache == null ? null : Cache.FirstOrDefault((w) => w.Width == width && w.Height == height);
            if(info != null && info.Bitmap != null) {
                return info;
            }

            if(info == null) {
                info = new WatermarkInfo() { Width = width, Height = height };
                Cache.Add(info);
            }

            double cw = width;
            double ch = height;

            WatermarkControl control = new WatermarkControl();
            control.Params = watermark;
            control.Width = cw;
            control.Height = ch;
            UserControl uc = new UserControl();
            uc.Background = new SolidColorBrush(Colors.Transparent);
            uc.Content = control;

            uc.Width = cw;
            uc.Height = ch;
            uc.Measure(new Size(cw, ch));
            uc.Arrange(new Rect(0, 0, cw, ch));
            uc.UpdateLayout();

            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96.0f, 96.0f, PixelFormats.Pbgra32);
            bmp.Render(uc);

            if(info != null) {
                info.Bitmap = bmp;
                info.Image = RenderTargetBitmap2Image(bmp);
            }

            return info;
        }
        protected System.Drawing.Image RenderTargetBitmap2Image(RenderTargetBitmap renderTarget) {
            System.Windows.Media.Imaging.BitmapEncoder encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
            MemoryStream myStream = new MemoryStream();

            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(renderTarget));
            encoder.Save(myStream);
            myStream.Seek(0, SeekOrigin.Begin);
            return System.Drawing.Image.FromStream(myStream);
        }
        protected List<WatermarkInfo> Cache {
            get; set;
        }
        public void CreateWatermarkCache(List<DmFile> files) {
            ClearWatermarkCache();
            Cache = new List<WatermarkInfo>();
            foreach(DmFile file in files) {
                WatermarkInfo info = Cache.FirstOrDefault((w) => w.Width == file.Width && w.Height == file.Height);
                if(info == null) {
                    info = new WatermarkInfo() { Width = file.Width, Height = file.Height };
                }

                info.RefCount++;
            }
            Cache.Sort(new WatermarkInfoComparer());

            if(Cache.Count > 5) {
                Cache.RemoveRange(5, Cache.Count - 5);
            }
        }
        public void ClearWatermarkCache() {
            if(Cache != null) {
                Cache.Clear();
            }

            Cache = null;
        }
    }
    public class WatermarkInfoComparer : IComparer<WatermarkInfo> {
        int IComparer<WatermarkInfo>.Compare(WatermarkInfo x, WatermarkInfo y) {
            if(x.RefCount > y.RefCount) {
                return 1;
            }

            if(x.RefCount == y.RefCount) {
                return 0;
            }

            return -1;
        }
    }
    public class WatermarkInfo {
        public int Width {
            get; set;
        }
        public int Height {
            get; set;
        }
        public int RefCount {
            get; set;
        }
        public RenderTargetBitmap Bitmap {
            get; set;
        }
        public System.Drawing.Image Image {
            get; set;
        }
    }
}
