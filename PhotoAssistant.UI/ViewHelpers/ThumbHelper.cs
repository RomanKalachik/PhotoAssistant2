using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.ViewHelpers {
    public static class ThumbHelper {
        static string GetThumbPath(DmFile info) {
            if(string.IsNullOrEmpty(info.ThumbFileName))
                info.ThumbFileName = SettingsStore.Default.ThumbPath + "\\" + info.Id.ToString() + ".dat";
            return info.ThumbFileName;
        }

        static Image GetImageNotFoundThumb() {
            return PhotoAssistant.UI.Properties.Resources.ImageNotFound;
        }

        public static void GetThumbnailImage(object sender, ThumbnailImageEventArgs e, DmFile model) {
            if(model == null)
                return;
            if(model.ThumbImage != null) {
                e.ThumbnailImage = model.ThumbImage;
                return;
            }
            string thumbPath = GetThumbPath(model);
            if(File.Exists(thumbPath)) {
                e.ThumbnailImage = Image.FromFile(thumbPath);
            } else {
                if(File.Exists(model.Path)) {
                    Image img = Image.FromFile(model.Path);
                    e.ThumbnailImage = e.CreateThumbnailImage(img);
                    img.Dispose();
                } else {
                    e.ThumbnailImage = GetImageNotFoundThumb();
                }
            }
            model.ThumbImage = e.ThumbnailImage;
            model.ThumbFileName = thumbPath;
        }
        public static Image GetThumbnailImage(DmFile file) {
            if(file.ThumbImage != null)
                return file.ThumbImage;
            if(File.Exists(file.ThumbFileName))
                file.ThumbImage = Image.FromFile(file.ThumbFileName);
            return file.ThumbImage;
        }
        public static Image GetIconImage(DmFile file) {
            if(file.IconImage != null)
                return file.IconImage;
            Image sourceImage = GetThumbnailImage(file);
            if(sourceImage == null)
                return null;
            Rectangle rect = ImageLayoutHelper.GetImageBounds(new Rectangle(0, 0, 64, 64), sourceImage.Size, ImageLayoutMode.ZoomOutside);
            Bitmap bmp = new Bitmap(64, 64);
            using(Graphics g = Graphics.FromImage(bmp)) {
                g.DrawImage(sourceImage, rect);
            }
            file.IconImage = bmp;
            return file.IconImage;
        }
    }
}
