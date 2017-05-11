using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PhotoAssistant.UI.View {
    public partial class ProjectPropertiesForm : XtraForm {
        public ProjectPropertiesForm() {
            InitializeComponent();
        }

        public string ProjectName {
            get { return this.textEdit1.Text; }
            set { this.textEdit1.Text = value; }
        }

        public string ProjectFileName {
            get { return this.buttonEdit1.Text; }
            set { this.buttonEdit1.Text = value; }
        }

        public bool EnableSelectLocation {
            get { return this.buttonEdit1.ReadOnly; }
            set {
                this.buttonEdit1.ReadOnly = value;
                this.buttonEdit1.Properties.Buttons[0].Visible = value;
            }
        }

        public string ProjectDescription {
            get { return this.memoEdit1.Text; }
            set { this.memoEdit1.Text = value; }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "PhotoAssistant projects (*.ddm)|*.ddm|All files (*.*)|*.*";
            dlg.FilterIndex = 0;
            dlg.CheckFileExists = IsOpenProject;
            if(dlg.ShowDialog() != DialogResult.OK)
                return;
            if(!IsOpenProject && File.Exists(dlg.FileName)) {
                if(XtraMessageBox.Show("File with specified name is already exists. Are you want to override specified file?", "Create new file..", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                    return;
                try {
                    File.Delete(dlg.FileName);
                } catch(Exception) {
                    XtraMessageBox.Show("Error: cannot override file...", "Create new file..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            this.buttonEdit1.Text = dlg.FileName;
        }



        private void simpleButton1_Click(object sender, EventArgs e) {
            ProjectName = ProjectName.Trim();
            if(string.IsNullOrEmpty(this.buttonEdit1.Text)) {
                XtraMessageBox.Show("Error: project file name and location not specified", "Project Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.dxErrorProvider1.SetError(this.buttonEdit1, "Project file name and location not specified");
                return;
            }
            if(!Directory.Exists(Path.GetDirectoryName(this.buttonEdit1.Text))) {
                XtraMessageBox.Show("Error: invalid path specified", "Project Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.dxErrorProvider1.SetError(this.buttonEdit1, "Invalid path specified");
                return;
            }
            if(string.IsNullOrEmpty(ProjectName)) {
                XtraMessageBox.Show("Error: Please specify project name", "Project Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.dxErrorProvider1.SetError(this.textEdit1, "Please specify project name");
                return;
            }
            this.dxErrorProvider1.ClearErrors();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        public bool IsOpenProject { get; set; }
    }
}
