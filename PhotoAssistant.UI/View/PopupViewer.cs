using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraBars.Ribbon;
using DevExpress.Utils;

using PhotoAssistant.Core.Model;
using PhotoAssistant.Controls.Wpf;
using PhotoAssistant.UI.ViewHelpers;

namespace PhotoAssistant.UI.View {
    public partial class PopupViewer : UserControl {
        public PopupViewer() {
            InitializeComponent();
            InitializePicturePreviewControl();
        }

        internal PicturePreviewControl PicturePreview { get; set; }
        private void InitializePicturePreviewControl() {
            PicturePreview = new PicturePreviewControl();
            PicturePreview.EffectType = PicturePreviewEffectType.Wipe;
            PicturePreview.PanelScaleFactor = 0.5;
            PicturePreview.IsCompactMode = true;
            PicturePreview.AllowAnimation = true;
            this.elementHost1.Child = PicturePreview;
        }

        public event EventHandler CloseClick {
            add { PicturePreview.Close += value; }
            remove { PicturePreview.Close -= value; }
        }

        List<DmFile> files;
        public List<DmFile> Files {
            get { return files; }
            set {
                if(Files == value)
                    return;
                files = value;
                OnFilesChanged();
            }
        }

        protected virtual void OnFilesChanged() {
            PicturePreview.Files = Files;
            this.galleryControl1.Gallery.BeginUpdate();
            try {
                this.galleryControl1.Gallery.Groups.Clear();
                this.galleryControl1.Gallery.Groups.Add(new GalleryItemGroup());
                if(Files != null)
                    Files.ForEach((file) => this.galleryControl1.Gallery.Groups[0].Items.Add(CreateGalleryItem(file)));
            } finally {
                this.galleryControl1.Gallery.EndUpdate();
            }
        }

        private GalleryItem CreateGalleryItem(DmFile file) {
            GalleryItem res = new GalleryItem();
            res.Tag = file;
            return res;
        }

        private void galleryControlGallery1_GetThumbnailImage(object sender, DevExpress.XtraBars.Ribbon.Gallery.GalleryThumbnailImageEventArgs e) {
            DmFile file = (DmFile)e.Item.Tag;
            ThumbHelper.GetThumbnailImage(sender, e, file);
        }

        private void galleryControlGallery1_ItemCheckedChanged(object sender, GalleryItemEventArgs e) {
            if(!e.Item.Checked)
                return;
            PicturePreview.CurrentFile = (DmFile)e.Item.Tag;
        }
    }
}
