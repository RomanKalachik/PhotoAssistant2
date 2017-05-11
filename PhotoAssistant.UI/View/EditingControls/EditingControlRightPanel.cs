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
using PhotoAssistant.UI.ViewHelpers;

namespace PhotoAssistant.UI.View.EditingControls {
    public partial class EditingControlRightPanel : BaseEditingUserControl {
        public EditingControlRightPanel() {
            InitializeComponent();
            InitializeTempColorTrackBar();
            InitializeTintColorEditor();
            InitializeToneCurveControls();
        }

        private void InitializeToneCurveControls() {
            InitializeToneCurveHighlightsControl();
            InitializeToneCurveLightsControl();
            InitializeToneCurveDarksControl();
            InitializeToneCurveShadowsControl();
        }

        protected override void OnEditingControlChanged()
        {
            base.OnEditingControlChanged();
            this.basicAdjustmentControl1.EditingControl = EditingControl;
        }

        DmFile currentFile;
        public DmFile CurrentFile {
            get { return currentFile; }
            set {
                if(CurrentFile == value)
                    return;
                currentFile = value;
                OnCurrentFileChanged();
            }
        }

        private void OnCurrentFileChanged() {
            UpdateHistogramm();
            UpdateToneCurve();
        }

        private void UpdateToneCurve() {
            if(CurrentFile == null)
                this.toneCurveControl1.ClearChannels(false);
            else 
                this.toneCurveControl1.CreateHistogramm((Bitmap)CurrentFile.ThumbImage);
        }

        private void UpdateHistogramm() {
            if(CurrentFile == null)
                this.histogramControl1.ClearChannels(false);
            else 
                this.histogramControl1.CreateHistogramm((Bitmap)CurrentFile.ThumbImage);
        }

        protected TrackBarWithSpinHelper TempColorEditor { get; private set; }
        protected TrackBarWithSpinHelper TintColorEditor { get; private set; }
        protected TrackBarWithSpinHelper ToneHighlightsEditor { get; private set; }
        protected TrackBarWithSpinHelper ToneLightsEditor { get; private set; }
        protected TrackBarWithSpinHelper ToneDarksEditor { get; private set; }
        protected TrackBarWithSpinHelper ToneShadowsEditor { get; private set; }

        private void InitializeTintColorEditor() {
            TintColorEditor = InitializeTrackBarEditor(this.tbTintColor, this.speTintColor, Color.Green, Color.Gray, Color.Purple, -100.0f, 100.0f, 1.0f);
            TintColorEditor.ValueChanged += TintColorEditor_ValueChanged;
        }
        
        private void InitializeTempColorTrackBar() {
            TempColorEditor = InitializeTrackBarEditor(this.tbTempColor, this.speTempColor, Color.Blue, Color.Gray, Color.Yellow, -100.0f, 100.0f, 1.0f);
            TempColorEditor.ValueChanged += TempColorEditor_ValueChanged;
        }

        private void InitializeToneCurveShadowsControl() {
            ToneShadowsEditor = InitializeTrackBarEditor(this.tbShadowsTone, this.speToneShadows, Color.FromArgb(255, 40, 40, 40), Color.Gray, Color.LightGray, -100.0f, 100.0f, 1.0f);
            ToneShadowsEditor.ValueChanged += ToneShadowsEditor_ValueChanged;
        }

        private void InitializeToneCurveDarksControl() {
            ToneDarksEditor = InitializeTrackBarEditor(this.tbDarksTone, this.speToneDarks, Color.FromArgb(255, 40, 40, 40), Color.Gray, Color.LightGray, -100.0f, 100.0f, 1.0f);
            ToneDarksEditor.ValueChanged += ToneDarksEditor_ValueChanged;
        }

        private void InitializeToneCurveLightsControl() {
            ToneLightsEditor = InitializeTrackBarEditor(this.tbLightsTone, this.speToneLights, Color.FromArgb(255, 40, 40, 40), Color.Gray, Color.LightGray, -100.0f, 100.0f, 1.0f);
            ToneLightsEditor.ValueChanged += ToneLightsEditor_ValueChanged;
        }

        private void InitializeToneCurveHighlightsControl() {
            ToneHighlightsEditor = InitializeTrackBarEditor(this.tbHighlightsTone, this.speToneHighlights, Color.FromArgb(255, 40, 40, 40), Color.Gray, Color.LightGray, -100.0f, 100.0f, 1.0f);
            ToneHighlightsEditor.ValueChanged += ToneHighlightsEditor_ValueChanged;
        }

        private void TempColorEditor_ValueChanged(object sender, EventArgs e) {
            
        }

        private void TintColorEditor_ValueChanged(object sender, EventArgs e) {

        }

        private void ToneShadowsEditor_ValueChanged(object sender, EventArgs e) {
            this.toneCurveControl1.ToneCurveGraph.ShadowsValue = ToneShadowsEditor.Value / 100;
        }

        private void ToneDarksEditor_ValueChanged(object sender, EventArgs e) {
            this.toneCurveControl1.ToneCurveGraph.DarksValue = ToneDarksEditor.Value / 100;
        }

        private void ToneLightsEditor_ValueChanged(object sender, EventArgs e) {
            this.toneCurveControl1.ToneCurveGraph.LightsValue = ToneLightsEditor.Value / 100;
        }

        private void ToneHighlightsEditor_ValueChanged(object sender, EventArgs e) {
            this.toneCurveControl1.ToneCurveGraph.HighlightsValue = ToneHighlightsEditor.Value / 100;
        }

        private TrackBarWithSpinHelper InitializeTrackBarEditor(ColorTrackBarControl trackBar, ScrollableSpinEdit spin, Color color1, Color color2, Color color3, float min, float max, float delta) {
            TrackBarWithSpinHelper helper = new TrackBarWithSpinHelper(trackBar, spin);
            helper.Minimum = min;
            helper.Maximum = max;
            helper.Delta = delta;
            trackBar.Properties.TickFrequency = (int)(10 / delta);
            trackBar.Properties.TickStyle = TickStyle.Both;
            trackBar.Values.BeginUpdate();
            trackBar.Values.Clear();
            trackBar.Values.Add(0);
            trackBar.Values.EndUpdate();

            trackBar.Properties.Colors.BeginUpdate();
            trackBar.Properties.Colors.Add(new ColorGradientStop() { Color = color1, Position = 0.0f });
            trackBar.Properties.Colors.Add(new ColorGradientStop() { Color = color2, Position = 0.5f });
            trackBar.Properties.Colors.Add(new ColorGradientStop() { Color = color3, Position = 1.0f });
            trackBar.Properties.Colors.EndUpdate();

            helper.Value = 0.0f;
            return helper;
        }

        private void accordionContentContainer2_SizeChanged(object sender, EventArgs e) {

        }

        private void FileEditPropertiesControl_Load(object sender, EventArgs e) {

        }

        private void colorPickerLabel_Click(object sender, EventArgs e) {
            if(EditingControl != null)
                EditingControl.PicturePreview.ShowColorPicker();
        }
    }
}
