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

using PhotoAssistant.Core;

namespace PhotoAssistant.UI.View {
    public partial class StorageControl : XtraUserControl {
        public StorageControl() {
            InitializeComponent();
        }

        StorageManager manager;
        public StorageManager Manager {
            get { return manager; }
            set {
                if(Manager == value)
                    return;
                StorageManager prev = Manager;
                manager = value;
                OnManagerChanged(prev, Manager);
            }
        }

        private void OnManagerChanged(StorageManager prev, StorageManager next) {
            this.storageMediaControl.DataSource = Manager.Storage;
            this.backupMediaControl.DataSource = Manager.Backup;
            if(prev != null) {
                this.storageMediaControl.AllowStorage -= prev.AllowStorage;
                this.backupMediaControl.AllowStorage -= prev.AllowStorage;
            }
            if(next != null) {
                this.storageMediaControl.AllowStorage += next.AllowStorage;
                this.backupMediaControl.AllowStorage += next.AllowStorage;
            }
        }
    }
}
