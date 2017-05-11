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
using System.Windows.Forms.Integration;
using PhotoAssistant.Core.Model;
using PhotoAssistant.Controls.Wpf;

namespace PhotoAssistant.UI.View {
    public partial class ViewerControl : ViewControlBase {
        public ViewerControl() {
            InitializeComponent();
            InitializePicturePreviewControl();
        }

        internal PicturePreviewControl PicturePreview { get; set; }
        ElementHost PicturePreviewHost { get; set; }

        private void InitializePicturePreviewControl() {
            PicturePreviewHost = new ElementHost();
            PicturePreviewHost.Dock = DockStyle.Fill;
            PicturePreview = new PicturePreviewControl();
            PicturePreview.Close += PicturePreview_Close;
            PicturePreview.ToggleFullScreen += PicturePreview_ToggleFullScreen;
            PicturePreview.ExitFullScreen += PicturePreview_ExitFullScreen;
            PicturePreviewHost.Child = PicturePreview;
            Controls.Add(PicturePreviewHost);
        }

        void PicturePreview_ExitFullScreen(object sender, EventArgs e) {
            MainForm.FullScreen = false;
        }

        void PicturePreview_ToggleFullScreen(object sender, EventArgs e) {
            MainForm.ToggleFullScreen();
        }

        void PicturePreview_Close(object sender, EventArgs e) {
            MainForm.FullScreen = false;
            MainForm.ActivateLibraryControl();
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            return base.ProcessCmdKey(ref msg, keyData);
        }

        DateTime? LastKeyTime { get; set; }
        internal void ProcessKeyDown(Keys keyData) {
            if(LastKeyTime == null)
                LastKeyTime = DateTime.Now;
            else if(((DateTime.Now.Ticks - LastKeyTime.Value.Ticks) / TimeSpan.TicksPerMillisecond) < 300)
                return;
            if(keyData == Keys.Enter) {
                MainForm.ToggleFullScreen();
            } else if(keyData == Keys.Escape) {
                MainForm.FullScreen = false;
            }
            LastKeyTime = DateTime.Now;
        }

        public DmFile CurrentFile {
            get { return PicturePreview.CurrentFile; }
            set { PicturePreview.CurrentFile = value; }
        }

        public List<DmFile> Files {
            get { return PicturePreview.Files; }
            set { PicturePreview.Files = value; }
        }
    }
}
