using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.View.ExportControls {
    public partial class ExportApplicationParams : XtraForm {
        public ExportApplicationParams() {
            InitializeComponent();
        }

        ApplicationInfo info;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ApplicationInfo ApplicationInfo {
            get { return info; }
            set {
                if(ApplicationInfo == value)
                    return;
                info = value;
                OnApplicationInfoChanged();
            }
        }

        protected virtual void OnApplicationInfoChanged() {
            if(ApplicationInfo == null) {
                this.beApplicationPath.Text = "";
                this.teCommandLine.Text = "";
                return;
            }
            this.beApplicationPath.Text = ApplicationInfo.Path;
            this.teCommandLine.Text = ApplicationInfo.CommandLine;
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckPathExists = true;
            dlg.CheckFileExists = true;
            dlg.Filter = "Executable Applications (*.exe)|*.exe|All Files (*.*)|*.*";
            dlg.FilterIndex = 0;
            dlg.Title = "Choose Application";
            if(dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            this.beApplicationPath.Text = dlg.FileName;
        }

        private void btnOk_Click(object sender, EventArgs e) {
            if(ApplicationInfo == null)
                return;
            if(!File.Exists(this.beApplicationPath.Text)) {
                XtraMessageBox.Show(this, "Error: Application not selected.", "Export Applications", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.dxErrorProvider1.SetError(this.beApplicationPath, "Application not selected");
                return;
            }
            this.dxErrorProvider1.ClearErrors();

            ApplicationInfo.Path = this.beApplicationPath.Text;
            ApplicationInfo.CommandLine = this.teCommandLine.Text.Trim();
            FileVersionInfo fi = FileVersionInfo.GetVersionInfo(ApplicationInfo.Path);
            ApplicationInfo.Name = fi.ProductName;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
