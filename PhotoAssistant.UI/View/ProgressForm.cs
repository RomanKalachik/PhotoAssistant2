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
    public partial class ProgressForm : XtraForm {
        public ProgressForm() {
            InitializeComponent();
            this.layoutControl1.LookAndFeel.ParentLookAndFeel = this.panelControl1.LookAndFeel;
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            this.layoutControl1.LayoutChanged();
            Size res = this.layoutControl1.Root.MinSize;
            res.Height += 5;
            Size = res;
        }
        public string Caption {
            get { return this.labelControl1.Text; }
            set { this.labelControl1.Text = value; }
        }
        public string Description {
            get { return this.labelControl2.Text; }
            set { this.labelControl2.Text = value; }
        }
        string imageFileName;
        public string ImageFileName {
            get { return imageFileName; }
            set {
                if(ImageFileName == value)
                    return;
                imageFileName = value;
                OnImageFileNameChanged();
            }
        }

        private void OnImageFileNameChanged() {
            Image img = this.pictureEdit1.Image;
            this.pictureEdit1.Image = null;
            if(img != null)
                img.Dispose();
            this.pictureEdit1.Image = Image.FromFile(ImageFileName);
        }

        public int Progress {
            get { return (int)this.progressBarControl1.EditValue; }
            set { this.progressBarControl1.EditValue = value; }
        }
        public int MaxProgressValue {
            get { return this.progressBarControl1.Properties.Maximum; }
            set { this.progressBarControl1.Properties.Maximum = value; }
        }

        protected override void OnLocationChanged(EventArgs e) {
            base.OnLocationChanged(e);
        }

        Action Action { get; set; }

        public void ShowDialog(IWin32Window owner, Action action) {
            Action = action;
            ShowDialog(owner);
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            Action.Invoke();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Hide();
        }
    }
}
