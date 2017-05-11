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
    public partial class SplashScreenForm : DevExpress.XtraSplashForm.SplashFormBase {
        public SplashScreenForm() {
            InitializeComponent();
            this.marqueeProgressBarControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.marqueeProgressBarControl1.LookAndFeel.SkinName = "Visual Studio 2013 Dark";

            var imgSize = this.pictureEdit1.Image.Size;
            int w = 512;
            int h = (int)(imgSize.Height * ((float)w / imgSize.Width));
            this.Size = new Size(w, h);
        }
    }
}
