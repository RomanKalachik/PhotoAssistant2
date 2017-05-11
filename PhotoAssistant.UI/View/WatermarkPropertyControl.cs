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
using DevExpress.Utils.Controls;

using System.IO;

using System.Drawing.Imaging;
using DevExpress.Utils;
using PhotoAssistant.Controls.Wpf;
using PhotoAssistant.Core.Model;
using PhotoAssistant.UI.ViewHelpers;

namespace PhotoAssistant.UI.View {
    public partial class WatermarkPropertyControl : XtraUserControl {
        public WatermarkPropertyControl() {
            InitializeComponent();
            InitializeWatermarkLayout();
            InitializeImageToTextAlignment();
        }

        public bool ShowEnableWatermark {
            get { return this.lciShowWatermark.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always; }
            set { this.lciShowWatermark.Visibility = value ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never; }
        }

        public bool EnableWatermark {
            get { return this.ceShowWatermark.Checked; }
            set { this.ceShowWatermark.Checked = value; }
        }

        private void UpdateFontButtonText() {
            this.simpleButton1.Text = Watermark.FontFamily.ToString() + "," + Watermark.FontSize; ;
        }

        private void InitializeImageToTextAlignment() {
            this.ceImageToTextAlign.Properties.Items.BeginUpdate();
            foreach(WatermarkImageToTextAlign align in Enum.GetValues(typeof(WatermarkImageToTextAlign))) {
                this.ceImageToTextAlign.Properties.Items.Add(align);
            }
            this.ceImageToTextAlign.Properties.Items.EndUpdate();
            this.ceImageToTextAlign.SelectedIndex = 0;
        }

        private void InitializeWatermarkLayout() {
            this.ceLayout.Properties.Items.BeginUpdate();
            foreach(WatermarkLayout layout in Enum.GetValues(typeof(WatermarkLayout))) {
                this.ceLayout.Properties.Items.Add(layout);
            }
            this.ceLayout.Properties.Items.EndUpdate();
            this.ceLayout.SelectedIndex = 0;
        }

        PicturePrintControl picturePrint;
        public PicturePrintControl PicturePrint {
            get { return picturePrint; }
            set {
                if(PicturePrint == value)
                    return;
                picturePrint = value;
                OnPicturePrintChanged();
            }
        }

        WatermarkParameters watermark;
        public WatermarkParameters Watermark {
            get { return PicturePrint == null ? watermark : PicturePrint.Watermark; }
            set {
                watermark = value;
                UpdateWatermarkProperties();
                RaiseWatermarkChanged();
            }
        }

        private void RaiseWatermarkChanged() {
            EventHandler handler = Events[watermarkChanged] as EventHandler;
            if(handler != null)
                handler(this, EventArgs.Empty);
        }

        private static readonly object watermarkChanged = new object();
        public event EventHandler WatermarkChanged {
            add { Events.AddHandler(watermarkChanged, value); }
            remove { Events.RemoveHandler(watermarkChanged, value); }
        }

        private void OnPicturePrintChanged() {
            UpdateWatermarkProperties();
        }

        bool SuppressRaiseEvent { get; set; }

        void UpdateWatermarkProperties() {
            if(Watermark == null)
                return;
            SuppressRaiseEvent = true;
            try {
                this.ceShowWatermark.Checked = Watermark.ShowWatermark;
                this.teText.EditValue = Watermark.Text;
                if(this.peImage.Image != null) {
                    Image prev = this.peImage.Image;
                    this.peImage.Image = null;
                    prev.Dispose();
                }
                if(File.Exists(Watermark.ImageUri))
                    this.peImage.EditValue = Image.FromFile(Watermark.ImageUri);
                this.ceLayout.EditValue = Watermark.Layout;
                this.ceImageToTextAlign.EditValue = Watermark.ImageToTextAlignment;
                this.tcRotate.Value = (int)Watermark.RotateAngle;
                this.tcOpacity.Value = (int)(100 * Watermark.Opacity);
                this.cpeFontColor.Color = UtilsHelper.NormalizedColorToColor(Watermark.FontColor);
                this.tcFontSize.Value = (int)Watermark.FontSize;
                this.tcIndent.Value = (int)Watermark.WatermarkIndent;
                this.tbImageSize.Value = (int)(Watermark.ImageScale * 100);
                UpdateFontButtonText();
                UpdateRotateEditor();
            } finally {
                SuppressRaiseEvent = false;
            }
        }

        private static readonly object parametersChanged = new object();
        public event EventHandler WatermarkParamsChanged {
            add { Events.AddHandler(parametersChanged, value); }
            remove { Events.RemoveHandler(parametersChanged, value); }
        }

        void RaiseWatermarkParamsChanged() {
            EventHandler handler = Events[parametersChanged] as EventHandler;
            if(handler != null)
                handler(this, EventArgs.Empty);
        }

        private void ceShowWatermark_CheckedChanged(object sender, EventArgs e) {
            Watermark.ShowWatermark = this.ceShowWatermark.Checked;
            RaiseWatermarkParamsChanged();
        }

        private void teText_EditValueChanged(object sender, EventArgs e) {
            Watermark.Text = this.teText.Text;
            RaiseWatermarkParamsChanged();
        }

        private void ceLayout_SelectedIndexChanged(object sender, EventArgs e) {
            if(Watermark == null)
                return;
            Watermark.Layout = (WatermarkLayout)this.ceLayout.EditValue;
            UpdateRotateEditor();
            RaiseWatermarkParamsChanged();
        }

        void UpdateRotateEditor() {
            if(Watermark.Layout != WatermarkLayout.FillPhoto) {
                this.tcRotate.EditValue = 0;
                this.tcRotate.Enabled = false;
            } else {
                this.tcRotate.Enabled = true;
            }
        }

        private void ceImageToTextAlign_SelectedIndexChanged(object sender, EventArgs e) {
            if(Watermark == null)
                return;
            Watermark.ImageToTextAlignment = (WatermarkImageToTextAlign)this.ceImageToTextAlign.EditValue;
            RaiseWatermarkParamsChanged();
        }

        private void tcRotate_EditValueChanged(object sender, EventArgs e) {
            Watermark.RotateAngle = this.tcRotate.Value;
            RaiseWatermarkParamsChanged();
        }

        private void tcOpacity_EditValueChanged(object sender, EventArgs e) {
            Watermark.Opacity = 0.01 * this.tcOpacity.Value;
            RaiseWatermarkParamsChanged();
        }

        private void cpeFontColor_EditValueChanged(object sender, EventArgs e) {
            Watermark.FontColor = UtilsHelper.ColorToNormalizedColor(this.cpeFontColor.Color);
            RaiseWatermarkParamsChanged();
        }

        private void tcFontSize_EditValueChanged(object sender, EventArgs e) {
            Watermark.FontSize = this.tcFontSize.Value;
            RaiseWatermarkParamsChanged();
        }

        private void tcRepeat_EditValueChanged(object sender, EventArgs e) {
            Watermark.WatermarkIndent = this.tcIndent.Value;
            RaiseWatermarkParamsChanged();
        }

        private void peImage_MouseClick(object sender, MouseEventArgs e) {
            if(e.Button == System.Windows.Forms.MouseButtons.Right) {

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            if(this.fontDialog1.ShowDialog() != DialogResult.OK)
                return;
            Watermark.FontFamily = new System.Windows.Media.FontFamily(this.fontDialog1.Font.FontFamily.Name);
            Watermark.FontWeight = UtilsHelper.FontWeigthToWpfFontWeight(this.fontDialog1.Font);
            Watermark.FontStyle = UtilsHelper.FontStyleToWpfFontStyle(this.fontDialog1.Font);
            Watermark.FontSize = this.fontDialog1.Font.Size;
            UpdateFontButtonText();
            RaiseWatermarkParamsChanged();
        }

        private void peImage_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e) {
            if(e.Item.Name == "cbLoad") {
                if(this.openFileDialog1.ShowDialog() != DialogResult.OK)
                    return;
                Watermark.ImageUri = this.openFileDialog1.FileName;
                this.peImage.EditValue = Image.FromFile(this.openFileDialog1.FileName);
                RaiseWatermarkParamsChanged();
            } else if(e.Item.Name == "cbDelete") {
                Watermark.ImageUri = null;
                Image img = this.peImage.Image;
                this.peImage.EditValue = null;
                if(img != null)
                    img.Dispose();
            }
        }

        private void tbImageSize_ValueChanged(object sender, EventArgs e) {
            Watermark.ImageScale = 0.01 * this.tbImageSize.Value;
            RaiseWatermarkParamsChanged();
        }
    }
}
