using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;

namespace PhotoAssistant.UI.ViewHelpers {
    public static class CustomSkinHelper {
        public static void UpdateSkin() {
            Skin skin = MapSkins.GetSkin(CustomSkinProvider.Default);
            Skin sourceSkin = MapSkins.GetSkin(DefaultSkinProvider.Default);
            SkinElement elem = sourceSkin[MapSkins.SkinCustomElement];
            skin.RemoveElement(skin[MapSkins.SkinCustomElement]);
            SkinElement newElem = elem.Copy(skin);
            newElem.Info.ContentMargins = new SkinPaddingEdges(8);
            skin.AddElement(newElem);
            elem = CustomGalleryBorder;
        }

        static SkinElement customGalleryBorder;
        public static SkinElement CustomGalleryBorder {
            get {
                if(customGalleryBorder == null) {
                    Skin skin = MapSkins.GetSkin(CustomSkinProvider.Default);
                    SkinElement elem = skin[MapSkins.SkinCustomElement];
                    customGalleryBorder = elem.Copy(skin, "PhotoGalleryBorder");
                    customGalleryBorder.Image.Image = PhotoAssistant.UI.Properties.Resources.PhotoBorder;
                }
                return customGalleryBorder;
            }
        }
    }

    public class CustomSkinProvider : ISkinProvider {
        static CustomSkinProvider defaultProvider;
        public static CustomSkinProvider Default {
            get {
                if(defaultProvider == null)
                    defaultProvider = new CustomSkinProvider();
                return defaultProvider;
            }
        }
        public string SkinName {
            get { return "Visual Studio 2013 Dark"; }
        }
    }
}
