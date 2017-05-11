using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Utils;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.ViewHelpers {
    public static class ColorLabelImagesCreator {
        public static object CreateColorLabelsImageCollection(DmModel model) {
            ImageCollection coll = new ImageCollection();
            coll.ImageSize = SettingsStore.Default.ColorLabelImageSize;
            coll.Images.Add(new Bitmap(coll.ImageSize.Width, coll.ImageSize.Height));
            foreach(DmColorLabel label in model.GetColorLabels()) {
                Image img = new Bitmap(coll.ImageSize.Width, coll.ImageSize.Height);
                using(Graphics g = Graphics.FromImage(img)) {
                    g.Clear(label.Color);
                }
                coll.Images.Add(img);
            }
            return coll;
        }
    }
}
