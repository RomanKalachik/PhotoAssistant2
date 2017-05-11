using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.View.ExportControls {
    public partial class TemplateNameEditor : XtraForm {
        public TemplateNameEditor() {
            InitializeComponent();
        }

        public string TemplateName {
            get { return this.textEdit1.Text; }
            set { this.textEdit1.Text = value; }
        }

        private void btnOk_Click(object sender, EventArgs e) {
            TemplateName = TemplateName.Trim();
            if(SettingsStore.Default.GetFileRenameTemlate(TemplateName) != null) {
                DialogResult res = XtraMessageBox.Show(OverwriteText, Title, MessageBoxButtons.YesNoCancel);
                if(res == System.Windows.Forms.DialogResult.Cancel ||
                    res == System.Windows.Forms.DialogResult.No)
                    return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        protected virtual string OverwriteText { get { return "Template with specified name already exists. Overwrite?"; } }
        protected virtual string Title { get { return "Template Name"; } }
    }
}
