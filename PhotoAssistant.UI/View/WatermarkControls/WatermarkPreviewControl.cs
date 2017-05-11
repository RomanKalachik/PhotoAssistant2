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
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.View.WatermarkControls {
    public partial class WatermarkPreviewControl : XtraUserControl {
        public WatermarkPreviewControl() {
            InitializeComponent();
            InitializePreview();
        }

        protected PhotoAssistant.Controls.Wpf.WatermarkPreviewControl PreviewControl {
            get;
            set;
        }
        private void InitializePreview() {
            PreviewControl = new PhotoAssistant.Controls.Wpf.WatermarkPreviewControl();
            this.elementHost1.Child = PreviewControl;
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        //public WatermarkParameters WatermarkParams {
        //    get { return PreviewControl.WatermarkParameters; }
        //    set { PreviewControl.WatermarkParameters = value; }
        //}

        public Uri ImageSource {
            get { return PreviewControl.ImageSource; }
            set { PreviewControl.ImageSource = value; }
        }

        public System.Windows.Media.ImageSource Image {
            get { return PreviewControl.Image; }
            set { PreviewControl.Image = value; }
        }
    }
}
