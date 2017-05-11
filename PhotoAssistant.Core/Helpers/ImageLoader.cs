using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
namespace PhotoAssistant.Core {
  
   public static class ImageLoader {
        public static Image LoadPreviewImage(DmFile file, NoFileFoundDelegate noFileFoundDelegate) {
            while(!File.Exists(file.FullPreviewPath)) {
                bool res = noFileFoundDelegate == null ? false : noFileFoundDelegate.Invoke();
                if(!res)
                    break;
            }
            if(!File.Exists(file.FullPreviewPath))
                return null;
            return Image.FromFile(file.FullPreviewPath);
        }
        public static BitmapImage LoadWpfPreviewImage(DmFile file, NoFileFoundDelegate noFileFoundDelegate) {
            while(!File.Exists(file.FullPreviewPath)) {
                bool res = noFileFoundDelegate == null ? false : noFileFoundDelegate.Invoke();
                if(!res)
                    break;
            }
            if(!File.Exists(file.FullPreviewPath))
                return null;
            return new BitmapImage(new Uri(file.FullPreviewPath));
        }
    }
    public delegate bool NoFileFoundDelegate();

    public static class WinformWpfConverter {
        public static Bitmap ToWinFormsBitmap(this System.Windows.Media.Imaging.BitmapSource bitmapsource) {
            using(MemoryStream stream = new MemoryStream()) {
                System.Windows.Media.Imaging.BitmapEncoder enc = new System.Windows.Media.Imaging.BmpBitmapEncoder();
                enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bitmapsource));
                enc.Save(stream);

                using(var tempBitmap = new Bitmap(stream)) {
                    return new Bitmap(tempBitmap);
                }
            }
        }

        public static System.Windows.Media.Imaging.BitmapSource ToWpfBitmap(this Bitmap bitmap) {
            using(MemoryStream stream = new MemoryStream()) {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

                stream.Position = 0;
                System.Windows.Media.Imaging.BitmapImage result = new System.Windows.Media.Imaging.BitmapImage();
                result.BeginInit();
                result.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
    }
}
