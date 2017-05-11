using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using PhotoAssistant.Core;
using PhotoAssistant.UI.ViewHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoAssistant.UI.View {
    public partial class AddStorageMediaForm : XtraForm {
        public AddStorageMediaForm() {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            MediaGalleryHelper.InitializeGallery(this.galleryControl1.Gallery, OnAllowMediaStorage);
        }

        bool OnAllowMediaStorage(StorageVolumeInfo info) {
            AllowStorageEventArgs e = new AllowStorageEventArgs(info);
            RaiseAllowStorage(e);
            return e.Allow;
        }

        private static readonly object allowStorage = new object();
        public event AllowStorageEventHandler AllowStorage {
            add { Events.AddHandler(allowStorage, value); }
            remove { Events.RemoveHandler(allowStorage, value); }
        }

        public void RaiseAllowStorage(AllowStorageEventArgs e) {
            AllowStorageEventHandler handler = Events[allowStorage] as AllowStorageEventHandler;
            if(handler != null)
                handler(this, e);
        }

        public List<StorageVolumeInfo> SelectedStorage {
            get;
            set;
        }

        private void galleryControl1_Gallery_ItemDoubleClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            SelectedStorage = new List<StorageVolumeInfo>();
            SelectedStorage.Add((StorageVolumeInfo)e.Item.Tag);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void sbOk_Click(object sender, EventArgs e) {
            List<GalleryItem> items = this.galleryControl1.Gallery.GetCheckedItems();
            SelectedStorage = new List<StorageVolumeInfo>();
            items.ForEach((i) => SelectedStorage.Add((StorageVolumeInfo)i.Tag));
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
