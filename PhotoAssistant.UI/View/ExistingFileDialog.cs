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
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.View {
    public partial class ExistingFileDialog : XtraForm {
        public ExistingFileDialog() {
            InitializeComponent();
            Result = ExistingFileMode.Skip;
        }

        string fileName;
        public string FileName {
            get { return fileName; }
            set {
                if(FileName == value)
                    return;
                fileName = value;
                OnFileNameChanged();
            }
        }

        public ExistingFileMode Result { get; private set; }
        public bool RememberChoise { get; set; }

        private void OnFileNameChanged() {
            this.labelControl1.Text = "File with name '" + Path.GetFileName(FileName) + "' already exists. What should be done?";
        }

        private void btSkip_Click(object sender, EventArgs e) {
            Result = ExistingFileMode.Skip;
            RememberChoise = this.ceRemember.Checked;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btGenerateNewName_Click(object sender, EventArgs e) {
            Result = ExistingFileMode.GenerateNewName;
            RememberChoise = this.ceRemember.Checked;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btOvewrite_Click(object sender, EventArgs e) {
            Result = ExistingFileMode.OverrideWithoutPrompt;
            RememberChoise = this.ceRemember.Checked;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
