using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DevExpress.Utils.Helpers;
using PhotoAssistant.Core;

namespace PhotoAssistant.UI.ViewHelpers {
    public static class MediaGalleryHelper {
        public static void InitializeGallery(GalleryControlGallery gallery, AllowMedia allowMediaDelegate) {
            StorageManager.Default.UpdateDevices();
            StorageVolumeInfoCollection volumes = new StorageVolumeInfoCollection();
            foreach(StorageVolumeInfo v in StorageManager.Default.PresentVolumes) {
                volumes.Add(v);
            }
            InitializeGallery(gallery, volumes, allowMediaDelegate);
        }
        public static void InitializeGallery(GalleryControlGallery gallery, StorageVolumeInfoCollection volumes, AllowMedia allowMediaDelegate) {
            gallery.Groups.Clear();
            gallery.ShowGroupCaption = false;
            gallery.ShowItemText = true;
            gallery.ItemImageLocation = DevExpress.Utils.Locations.Left;
            gallery.FixedImageSize = false;
            gallery.AllowHoverImages = false;
            gallery.StretchItems = true;

            GalleryItemGroup group = new GalleryItemGroup();
            gallery.Groups.Add(group);

            foreach(StorageVolumeInfo volume in volumes) {
                if(allowMediaDelegate != null && !allowMediaDelegate(volume))
                    continue;
                GalleryItem item = CreateItem(volume);
                item.Enabled = allowMediaDelegate == null ? true : allowMediaDelegate(volume);
                item.Image = FileSystemImageCache.Cache.GetImage(volume.HintName, IconSizeType.Medium, new Size(32, 32));
                group.Items.Add(item);
            }
        }

        public static GalleryItem CreateItem(StorageVolumeInfo info) {
            GalleryItem item = new GalleryItem();
            item.Caption = info.VolumeLabel + " - " + info.ActualName + " - " + info.ProjectFolder;
            item.Description = FileSizeHelper.Size2String(info.AvailableFreeSpace) + "/" + FileSizeHelper.Size2String(info.TotalFreeSpace) + "  " + info.Device.ProductId;
            item.Image = GetImage(info);
            item.Tag = info;
            return item;
        }

        private static Image GetImage(StorageVolumeInfo info) {
            return null;
        }
    }

    public delegate bool AllowMedia(StorageVolumeInfo info);
}
