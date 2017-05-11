using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;


using DevExpress.XtraBars.Ribbon;
using System.IO;
using PhotoAssistant.Core;
using PhotoAssistant.UI.ViewHelpers;

namespace PhotoAssistant.UI.View {
    public partial class StorageMediaControl : XtraUserControl {
        public StorageMediaControl() {
            InitializeComponent();
        }
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text {
            get { return this.layoutGroup.Text; }
            set { this.layoutGroup.Text = value; }
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

        private void sbAddStorageMedia_Click(object sender, EventArgs e) {
            using(FolderBrowserDialog dlg = new FolderBrowserDialog()) {
                if(dlg.ShowDialog() != DialogResult.OK)
                    return;
                string root = Path.GetPathRoot(dlg.SelectedPath);
                string relativePath = dlg.SelectedPath.Substring(root.Length, dlg.SelectedPath.Length - root.Length);
                if(relativePath.Length > 0 && relativePath.StartsWith("\\"))
                    relativePath.Substring(1, relativePath.Length - 1);
                CheckAddStorageDevice(root, relativePath);
            }
        }

        private void CheckAddStorageDevice(string root, string relativePath) {
            StorageVolumeInfo volume = StorageManager.Default.GetStorageVolumeInfo(root);
            volume.ProjectFolder = relativePath;
            if(!CheckIsVolumeCorrect(volume, root))
                return;
            if(CheckIsDeviceAlreadyAdded(volume))
                return;
            DataSource.Add(volume);
        }

        private bool CheckIsVolumeCorrect(StorageVolumeInfo volume, string root) {
            if(volume == null) {
                StorageManager.Default.DialogsProvider.DisplayVolumeIsNotCorrect(root);
                return false;
            }
            return true;
        }

        private bool CheckIsDeviceAlreadyAdded(StorageVolumeInfo volume) {
            foreach(StorageVolumeInfo v in StorageManager.Default.Storage) {
                if(v.Device.SerialNumber == volume.Device.SerialNumber) {
                    if(StorageManager.Default.DialogsProvider.DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsed() == DialogResult.OK)
                        return false;
                    return true;
                }
            }
            foreach(StorageVolumeInfo v in StorageManager.Default.Backup) {
                if(v.Device.SerialNumber == volume.Device.SerialNumber) {
                    if(StorageManager.Default.DialogsProvider.DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsed() == DialogResult.OK)
                        return false;
                    return true;
                }
            }
            return false;
        }

        StorageVolumeInfoCollection dataSource;
        public StorageVolumeInfoCollection DataSource {
            get { return dataSource; }
            set {
                if(DataSource == value)
                    return;
                StorageVolumeInfoCollection prev = DataSource;
                dataSource = value;
                OnDataSourceChanged(prev, DataSource);
            }
        }

        private void OnDataSourceChanged(StorageVolumeInfoCollection prev, StorageVolumeInfoCollection next) {
            if(prev != null)
                prev.CollectionChanged -= OnDataSourceCollectionChanged;
            if(next != null)
                next.CollectionChanged += OnDataSourceCollectionChanged;
            UpdateGallerySource();
        }

        private void UpdateGallerySource() {
            MediaGalleryHelper.InitializeGallery(this.galleryControl1.Gallery, DataSource, null);
        }

        private void OnDataSourceCollectionChanged(object sender, EventArgs e) {
            MediaGalleryHelper.InitializeGallery(this.galleryControl1.Gallery, DataSource, null);
        }

        private void Form_AllowStorage(object sender, AllowStorageEventArgs e) {
            RaiseAllowStorage(e);
        }

        private void sbRemoveMedia_Click(object sender, EventArgs e) {
            if(XtraMessageBox.Show(this, "Do you really want to remove selected media?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            if(DataSource == null)
                return;
            List<GalleryItem> items = this.galleryControl1.Gallery.GetCheckedItems();
            DataSource.BeginUpdate();
            try {
                foreach(GalleryItem item in items) {
                    DataSource.Remove((StorageVolumeInfo)item.Tag);
                }
            } finally {
                DataSource.EndUpdate();
            }
        }

        private void galleryControl1_Gallery_ItemCheckedChanged(object sender, DevExpress.XtraBars.Ribbon.GalleryItemEventArgs e) {
            UpdateRemoveButton();
        }

        private void UpdateRemoveButton() {
            List<GalleryItem> items = this.galleryControl1.Gallery.GetCheckedItems();
            this.sbRemoveMedia.Enabled = items.Count > 0;
        }

        private void galleryControl1_Gallery_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e) {

        }
    }
}


