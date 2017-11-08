using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.Utils;
using System.ComponentModel;
using System.Drawing;
using DevExpress.LookAndFeel;
using DevExpress.Utils.Drawing;
using System.Windows.Forms;
using DevExpress.Utils.Drawing.Animation;
using DevExpress.Images;
using DevExpress.Utils.Text;
using System.Collections.ObjectModel;
using DevExpress.Skins;

namespace PhotoAssistant.UI.View.EditingControls {
    public class HistogrammControl : BaseStyleControl, IContextItemCollectionOptionsOwner, IContextItemCollectionOwner, IXtraAnimationListener, IMouseWheelSupport {
        private static readonly object contextButtonClick = new object();
        private static readonly object customContextButtonToolTip = new object();
        private static readonly object contextButtonValueChanged = new object();

        public HistogrammControl() {
            this.redChannelColor = DefaultRedChannelColor;
            this.greenChannelColor = DefaultGreenChannelColor;
            this.blueChannelColor = DefaultBlueChannelColor;
            this.redGreenChannelColor = DefaultRedGreenChannelColor;
            this.redBlueChannelColor = DefaultRedBlueChannelColor;
            this.greenBlueChannelColor = DefaultGreenBlueChannelColor;
            this.redGreenBlueChannelColor = DefaultRedGreenBlueChannelColor;
            this.gridThickLineColor = DefaultGridThickLineColor;
            this.gridThinLineColor = DefaultGridThinLineColor;
            this.exposureAreaColor = DefaultExposureAreaColor;
            this.verticalLineColor = DefaultVerticalLineColor;
            ShowExposureArea = true;
            ClearChannels(false);
            CreateDefaultButtons();

            BlacksFormatString = DefaultBlacksFormatString;
            ShadowsFormatString = DefaultShadowsFormatString;
            ExposureFormatString = DefaultExposureFormatString;
            HighlightsFormatString = DefaultHighlightsFormatString;
            WhitesFormatString = DefaultWhitesFormatString;
        }

        protected virtual void CreateDefaultButtons() {
            if(ShowClippingButtons) {
                ContextButtons.Add(CreateShowShadowClippingButton());
                ContextButtons.Add(CreateShowHighlightClippingButton());
            }
            if(ShowChannelButtons) {
                ContextButtons.Add(CreateRedChannelButton());
                ContextButtons.Add(CreateGreenChannelButton());
                ContextButtons.Add(CreateBlueChannelButton());
                ContextButtons.Add(CreateAllChannelButton());
            }
            if(ShowChartModeButton) {
                ContextButtons.Add(CreateChartModeButton());
            }
        }

        public virtual void SetLabel(string label) {
            Labels.BeginUpdate();
            Labels.Clear();
            Labels.Add(label);
            Labels.EndUpdate();
        }

        public virtual void SetLabels(string label, string label2) {
            Labels.BeginUpdate();
            Labels.Clear();
            Labels.Add(label);
            Labels.Add(label2);
            Labels.EndUpdate();
        }

        public virtual void SetLabels(string label, string label2, string label3, string label4) {
            Labels.BeginUpdate();
            Labels.Clear();
            Labels.Add(label);
            Labels.Add(label2);
            Labels.Add(label3);
            Labels.Add(label4);
            Labels.EndUpdate();
        }

        float blacksMinValue = -100.0f;
        [DefaultValue(-100.0f)]
        public float BlacksMinValue {
            get { return blacksMinValue; }
            set {
                value = ConstrainMinValue(value, BlacksMaxValue);
                if(BlacksMinValue == value)
                    return;
                blacksMinValue = value;
                OnValueChanged(HistogrammValue.BlacksMin, value);
            }
        }

        float blacksMaxValue = 100.0f;
        [DefaultValue(100.0f)]
        public float BlacksMaxValue {
            get { return blacksMaxValue; }
            set {
                value = ConstrainMaxValue(value, BlacksMinValue);
                if(BlacksMaxValue == value)
                    return;
                blacksMaxValue = value;
                OnValueChanged(HistogrammValue.BlacksMax, value);
            }
        }

        float blacksValue = 0.0f;
        [DefaultValue(0.0f)]
        public float BlacksValue {
            get { return blacksValue; }
            set {
                value = ConstrainValue(value, BlacksMinValue, BlacksMaxValue);
                if(BlacksValue == value)
                    return;
                blacksValue = value;
                OnValueChanged(HistogrammValue.Blacks, value);
            }
        }

        float shadowsMinValue = -100.0f;
        [DefaultValue(-100.0f)]
        public float ShadowsMinValue {
            get { return shadowsMinValue; }
            set {
                value = ConstrainMinValue(value, ShadowsMaxValue);
                if(ShadowsMinValue == value)
                    return;
                shadowsMinValue = value;
                OnValueChanged(HistogrammValue.ShadowsMin, value);
            }
        }

        float shadowsMaxValue = 100.0f;
        [DefaultValue(100.0f)]
        public float ShadowsMaxValue {
            get { return shadowsMaxValue; }
            set {
                value = ConstrainMaxValue(value, ShadowsMinValue);
                if(ShadowsMaxValue == value)
                    return;
                shadowsMaxValue = value;
                OnValueChanged(HistogrammValue.ShadowsMax, value);
            }
        }

        float shadowsValue = 0.0f;
        [DefaultValue(0.0f)]
        public float ShadowsValue {
            get { return shadowsValue; }
            set {
                value = ConstrainValue(value, ShadowsMinValue, ShadowsMaxValue);
                if(ShadowsValue == value)
                    return;
                shadowsValue = value;
                OnValueChanged(HistogrammValue.Shadows, value);
            }
        }

        float exposureMinValue = -5.0f;
        [DefaultValue(-5.0f)]
        public float ExposureMinValue {
            get { return exposureMinValue; }
            set {
                value = ConstrainMinValue(value, ExposureMaxValue);
                if(ExposureMinValue == value)
                    return;
                exposureMinValue = value;
                OnValueChanged(HistogrammValue.ExposureMin, value);
            }
        }

        float exposureMaxValue = 5.0f;
        [DefaultValue(5.0f)]
        public float ExposureMaxValue {
            get { return exposureMaxValue; }
            set {
                value = ConstrainMaxValue(value, ExposureMinValue);
                if(ExposureMaxValue == value)
                    return;
                exposureMaxValue = value;
                OnValueChanged(HistogrammValue.ExposureMax, value);
            }
        }

        float exposureValue = 0.0f;
        [DefaultValue(0.0f)]
        public float ExposureValue {
            get { return exposureValue; }
            set {
                value = ConstrainValue(value, ExposureMinValue, ExposureMaxValue);
                if(ExposureValue == value)
                    return;
                exposureValue = value;
                OnValueChanged(HistogrammValue.Exposure, value);
            }
        }

        float highlightsMinValue = -100.0f;
        [DefaultValue(-100.0f)]
        public float HighlightsMinValue {
            get { return highlightsMinValue; }
            set {
                value = ConstrainMinValue(value, HighlightsMaxValue);
                if(HighlightsMinValue == value)
                    return;
                highlightsMinValue = value;
                OnValueChanged(HistogrammValue.HighlightsMin, value);
            }
        }

        float highlightsMaxValue = 100.0f;
        [DefaultValue(100.0f)]
        public float HighlightsMaxValue {
            get { return highlightsMaxValue; }
            set {
                value = ConstrainMaxValue(value, HighlightsMinValue);
                if(HighlightsMaxValue == value)
                    return;
                highlightsMaxValue = value;
                OnValueChanged(HistogrammValue.HighlightsMax, value);
            }
        }

        float highlightsValue = 0.0f;
        [DefaultValue(0.0f)]
        public float HighlightsValue {
            get { return highlightsValue; }
            set {
                value = ConstrainValue(value, HighlightsMinValue, HighlightsMaxValue);
                if(HighlightsValue == value)
                    return;
                highlightsValue = value;
                OnValueChanged(HistogrammValue.Highlights, value);
            }
        }

        float whitesMinValue = -100.0f;
        [DefaultValue(-100.0f)]
        public float WhitesMinValue {
            get { return whitesMinValue; }
            set {
                value = ConstrainMinValue(value, WhitesMaxValue);
                if(WhitesMinValue == value)
                    return;
                whitesMinValue = value;
                OnValueChanged(HistogrammValue.WhitesMin, value);
            }
        }

        float whitesMaxValue = 100.0f;
        [DefaultValue(100.0f)]
        public float WhitesMaxValue {
            get { return whitesMaxValue; }
            set {
                value = ConstrainMaxValue(value, WhitesMinValue);
                if(WhitesMaxValue == value)
                    return;
                whitesMaxValue = value;
                OnValueChanged(HistogrammValue.WhitesMax, value);
            }
        }

        float whitesValue = 0.0f;
        [DefaultValue(0.0f)]
        public float WhitesValue {
            get { return whitesValue; }
            set {
                value = ConstrainValue(value, WhitesMinValue, WhitesMaxValue);
                if(WhitesValue == value)
                    return;
                whitesValue = value;
                OnValueChanged(HistogrammValue.Whites, value);
            }
        }

        protected virtual float ConstrainValue(float value, float min, float max) {
            return Math.Min(Math.Max(value, min), max);
        }

        protected virtual float ConstrainMinValue(float value, float max) {
            return Math.Min(value, max);
        }

        protected virtual float ConstrainMaxValue(float value, float min) {
            return Math.Max(value, min);
        }

        protected virtual void OnValueChanged(HistogrammValue valueType, float value) {
            RaiseValueChanged(new HistogrammValueChangedEventArgs(valueType, value));
        }

        protected void RaiseValueChanged(HistogrammValueChangedEventArgs e) {
            HistogrammValueChangedEventHandler handler = Events[valueChanged] as HistogrammValueChangedEventHandler;
            if(handler != null)
                handler(this, e);
        }

        private static readonly object valueChanged = new object();
        public event HistogrammValueChangedEventHandler ValueChanged {
            add { Events.AddHandler(valueChanged, value); }
            remove { Events.RemoveHandler(valueChanged, value); }
        }

        HistogrammLabelCollection labels;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual HistogrammLabelCollection Labels {
            get {
                if(labels == null)
                    labels = CreateLabels();
                return labels;
            }
        }

        private HistogrammLabelCollection CreateLabels() {
            return new HistogrammLabelCollection(this);
        }

        HistogrammControlHandler handler;
        protected HistogrammControlHandler Handler {
            get {
                if(handler == null)
                    handler = CreateHandler();
                return handler;
            }
        }

        float blacksWidth = 0.05f;
        [DefaultValue(0.05f)]
        public float BlacksWidth {
            get { return blacksWidth; }
            set {
                if(BlacksWidth == value)
                    return;
                blacksWidth = value;
                OnColorChanged();
            }
        }

        float shadowsWidth = 0.3f;
        [DefaultValue(0.3f)]
        public float ShadowsWidth {
            get { return shadowsWidth; }
            set {
                if(ShadowsWidth == value)
                    return;
                shadowsWidth = value;
                OnColorChanged();
            }
        }

        float highlightsWidth = 0.3f;
        [DefaultValue(0.3f)]
        public float HighlightsWidth {
            get { return highlightsWidth; }
            set {
                if(HighlightsWidth == value)
                    return;
                highlightsWidth = value;
                OnColorChanged();
            }
        }

        float whitesWidth = 0.05f;
        [DefaultValue(0.05f)]
        public float WhitesWidth {
            get { return whitesWidth; }
            set {
                if(WhitesWidth == value)
                    return;
                whitesWidth = value;
                OnColorChanged();
            }
        }

        public float ExposureWidth { get { return 1.0f - BlacksWidth - ShadowsWidth - HighlightsWidth - WhitesWidth; } }

        protected Color DefaultExposureAreaColor {
            get { return Color.FromArgb(20, 255, 255, 255); }
        }

        Color exposureAreaColor;
        public Color ExposureAreaColor {
            get { return exposureAreaColor; }
            set {
                if(ExposureAreaColor == value)
                    return;
                exposureAreaColor = value;
                OnColorChanged();
            }
        }

        [DefaultValue(true)]
        public bool ShowExposureArea {
            get; set;
        }

        private ContextItem CreateChartModeButton() {
            CheckContextButton res = new CheckContextButton();
            res.ToolTip = "Chart Prevew Mode (Line/Area)";
            res.CheckedGlyph = res.HoverCheckedGlyph = res.HoverGlyph = res.Glyph = ImageResourceCache.Default.GetImage("grayscaleimages/other/diagonal_up_border_16x16.png");
            res.Name = ChartPrevewModeButtonName;
            res.CheckedGlyph = res.Glyph;
            res.AppearanceNormal.ForeColor = Color.White;
            res.AppearanceHover.ForeColor = Color.White;
            res.AllowGlyphSkinning = DefaultBoolean.True;
            res.Alignment = ContextItemAlignment.MiddleTop;
            return res;
        }

        protected override void OnResize(EventArgs e) {
            ((HistogrammControlViewInfo)ViewInfo).ResetContextButtons();
            base.OnResize(e);
        }

        protected string ChartPrevewModeButtonName {
            get { return "ChartPreviewMode"; }
        }

        protected string ShowShadowClippingButtonName {
            get { return "ShowShadowClipping"; }
        }

        protected string ShowHighlightClippingButtonName {
            get { return "ShowHighlightClipping"; }
        }

        protected string ShowRedChannelButtonName {
            get { return "ShowRedChannel"; }
        }

        protected string ShowGreenChannelButtonName {
            get { return "ShowGreenChannel"; }
        }

        protected string ShowBlueChannelButtonName {
            get { return "ShowBlueChannel"; }
        }

        protected string ShowAllChannelButtonName {
            get { return "ShowAllChannel"; }
        }

        public void ClearLabels() {
            Labels.BeginUpdate();
            Labels.Clear();
            Labels.EndUpdate();
        }

        protected virtual ContextItem CreateAllChannelButton() {
            return CreateChannelButton(Color.White, ShowAllChannelButtonName, "Show All Channels");
        }

        protected virtual ContextItem CreateGreenChannelButton() {
            return CreateChannelButton(Color.Green, ShowGreenChannelButtonName, "Show Green Channel");
        }

        protected virtual ContextItem CreateBlueChannelButton() {
            return CreateChannelButton(Color.Blue, ShowBlueChannelButtonName, "Show Blue Channel");
        }

        protected virtual ContextItem CreateRedChannelButton() {
            return CreateChannelButton(Color.Red, ShowRedChannelButtonName, "Show Red Channel");
        }

        protected virtual ContextItem CreateChannelButton(Color channelColor, string name, string tooltip) {
            CheckContextButton res = new CheckContextButton();
            res.ToolTip = tooltip;
            res.CheckedGlyph = res.HoverCheckedGlyph = res.HoverGlyph = res.Glyph = GetChannelGlyph();
            res.Name = name;
            res.CheckedGlyph = res.Glyph;
            res.AppearanceNormal.ForeColor = channelColor;
            res.AppearanceHover.ForeColor = channelColor;
            res.AllowGlyphSkinning = DefaultBoolean.True;
            res.Alignment = ContextItemAlignment.MiddleTop;
            return res;
        }

        protected virtual Image GetChannelGlyph() {
            return ImageResourceCache.Default.GetImage("grayscaleimages/chart/chart_16x16.png");
        }

        protected virtual ContextButtonBase CreateShowHighlightClippingButton() {
            CheckContextButton res = new CheckContextButton();
            res.ToolTip = "Show Highlight Clipping";
            res.CheckedGlyph = res.HoverCheckedGlyph = res.HoverGlyph = res.Glyph = ImageResourceCache.Default.GetImage("grayscaleimages/actions/show_16x16.png");
            res.Name = ShowHighlightClippingButtonName;
            res.CheckedGlyph = res.Glyph;
            res.AppearanceNormal.ForeColor = Color.White;
            res.AppearanceHover.ForeColor = Color.White;
            res.AllowGlyphSkinning = DefaultBoolean.True;
            res.Alignment = ContextItemAlignment.TopFar;
            return res;
        }

        protected virtual ContextButtonBase CreateShowShadowClippingButton() {
            CheckContextButton res = new CheckContextButton();
            res.ToolTip = "Show Shadow Clipping";
            res.CheckedGlyph = res.HoverCheckedGlyph = res.HoverGlyph = res.Glyph = ImageResourceCache.Default.GetImage("grayscaleimages/actions/show_16x16.png");
            res.Name = ShowShadowClippingButtonName;
            res.CheckedGlyph = res.Glyph;
            res.AppearanceNormal.ForeColor = Color.White;
            res.AppearanceHover.ForeColor = Color.White;
            res.AllowGlyphSkinning = DefaultBoolean.True;
            res.Alignment = ContextItemAlignment.TopNear;
            return res;
        }

        public void ClearChannels(bool animated) {
            SetChannels(new int[256], new int[256], new int[256], animated);
        }

        protected override BaseControlPainter CreatePainter() {
            return new HistogrammControlPainter();
        }

        protected override BaseStyleControlViewInfo CreateViewInfo() {
            return new HistogrammControlViewInfo(this);
        }

        HistogrammChartPreviewMode chartMode = HistogrammChartPreviewMode.Area;
        [DefaultValue(HistogrammChartPreviewMode.Area)]
        public HistogrammChartPreviewMode ChartPreviewMode {
            get { return chartMode; }
            set {
                if(ChartPreviewMode == value)
                    return;
                chartMode = value;
                OnColorChanged();
            }
        }

        protected virtual byte HistogrammOpacity {
            get { return 140; }
        }

        protected virtual byte GridOpacity {
            get { return 40; }
        }

        Color DefaultRedChannelColor {
            get { return Color.FromArgb(HistogrammOpacity, 255, 0, 0); }
        }

        Color DefaultGreenChannelColor {
            get { return Color.FromArgb(HistogrammOpacity, 0, 255, 0); }
        }

        Color DefaultBlueChannelColor {
            get { return Color.FromArgb(HistogrammOpacity, 0, 0, 255); }
        }

        Color DefaultRedGreenChannelColor {
            get { return Color.FromArgb(HistogrammOpacity, 255, 255, 0); }
        }

        Color DefaultRedBlueChannelColor {
            get { return Color.FromArgb(HistogrammOpacity, 255, 0, 255); }
        }

        Color DefaultGreenBlueChannelColor {
            get { return Color.FromArgb(HistogrammOpacity, 0, 255, 255); }
        }

        Color DefaultRedGreenBlueChannelColor {
            get { return Color.FromArgb(HistogrammOpacity, 255, 255, 255); }
        }

        protected Color DefaultGridThinLineColor {
            get { return Color.FromArgb(10, Color.Gray); }
        }

        HistogrammChannelPreviewMode mode = HistogrammChannelPreviewMode.All;
        [DefaultValue(HistogrammChannelPreviewMode.All)]
        public HistogrammChannelPreviewMode Mode {
            get { return mode; }
            set {
                if(Mode == value)
                    return;
                mode = value;
                OnColorChanged();
            }
        }

        Color gridThinLineColor;
        bool ShouldSerializeGridThinLineColor() { return GridThinLineColor != DefaultGridThinLineColor; }
        public Color GridThinLineColor {
            get { return gridThinLineColor; }
            set {
                if(GridThinLineColor == value)
                    return;
                gridThinLineColor = value;
                OnColorChanged();
            }
        }

        protected Color DefaultGridThickLineColor {
            get { return Color.FromArgb(GridOpacity, Color.LightGray); }
        }

        protected Color DefaultVerticalLineColor {
            get { return Color.FromArgb(80, Color.White); }
        }

        private Color verticalLineColor;
        bool ShouldSerializeVerticalLineColor() { return VerticalLineColor != DefaultVerticalLineColor; }
        public Color VerticalLineColor {
            get { return verticalLineColor; }
            set {
                if(VerticalLineColor == value)
                    return;
                verticalLineColor = value;
                OnPropertiesChanged();
            }
        }


        Color gridThickLineColor;
        bool ShouldSerializeGridThickLineColor() { return GridThickLineColor != DefaultGridThickLineColor; }
        public Color GridThickLineColor {
            get { return gridThickLineColor; }
            set {
                if(GridThickLineColor == value)
                    return;
                gridThickLineColor = value;
                OnColorChanged();
            }
        }

        bool ShouldSerializeRedChannelColor() { return RedChannelColor != DefaultRedChannelColor; }
        Color redChannelColor;
        public Color RedChannelColor {
            get { return redChannelColor; }
            set {
                if(RedChannelColor == value)
                    return;
                redChannelColor = value;
                OnColorChanged();
            }
        }

        private void OnColorChanged() {
            Invalidate();
            Update();
        }

        bool ShouldSerializeGreenChannelColor() { return GreenChannelColor != DefaultGreenChannelColor; }
        Color greenChannelColor;
        public Color GreenChannelColor {
            get { return greenChannelColor; }
            set {
                if(GreenChannelColor == value)
                    return;
                greenChannelColor = value;
                OnColorChanged();
            }
        }

        bool ShouldSerializeBlueChannelColor() { return BlueChannelColor != DefaultBlueChannelColor; }
        Color blueChannelColor;
        public Color BlueChannelColor {
            get { return blueChannelColor; }
            set {
                if(BlueChannelColor == value)
                    return;
                blueChannelColor = value;
                OnColorChanged();
            }
        }

        bool ShouldSerializeRedGreenChannelColor() { return RedGreenChannelColor != DefaultRedGreenChannelColor; }
        Color redGreenChannelColor;
        public Color RedGreenChannelColor {
            get { return redGreenChannelColor; }
            set {
                if(RedGreenChannelColor == value)
                    return;
                redGreenChannelColor = value;
                OnColorChanged();
            }
        }

        bool ShouldSerializeRedBlueChannelColor() { return RedBlueChannelColor != DefaultRedBlueChannelColor; }
        Color redBlueChannelColor;
        public Color RedBlueChannelColor {
            get { return redBlueChannelColor; }
            set {
                if(RedBlueChannelColor == value)
                    return;
                redBlueChannelColor = value;
                OnColorChanged();
            }
        }

        bool ShouldSerializeGreenBlueChannelColor() { return GreenBlueChannelColor != DefaultGreenBlueChannelColor; }
        Color greenBlueChannelColor;
        public Color GreenBlueChannelColor {
            get { return greenBlueChannelColor; }
            set {
                if(GreenBlueChannelColor == value)
                    return;
                greenBlueChannelColor = value;
                OnColorChanged();
            }
        }

        bool ShouldSerializeRedGreenBlueChannelColor() { return RedGreenBlueChannelColor != DefaultRedGreenBlueChannelColor; }
        Color redGreenBlueChannelColor;
        public Color RedGreenBlueChannelColor {
            get { return redGreenBlueChannelColor; }
            set {
                if(RedGreenBlueChannelColor == value)
                    return;
                redGreenBlueChannelColor = value;
                OnColorChanged();
            }
        }

        ContextItemCollection contextButtons;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ContextItemCollection ContextButtons {
            get {
                if(contextButtons == null) {
                    contextButtons = CreateContextButtonsCollection();
                    contextButtons.Options = ContextButtonOptions;
                }
                return contextButtons;
            }
        }

        bool showClippingButtons = true;
        [DefaultValue(true)]
        public virtual bool ShowClippingButtons {
            get { return showClippingButtons; }
            set {
                if(ShowClippingButtons == value)
                    return;
                showClippingButtons = value;
                OnShowContextButtonsChanged();
            }
        }

        bool showChannelButtons = true;
        [DefaultValue(true)]
        public virtual bool ShowChannelButtons {
            get { return showChannelButtons; }
            set {
                if(ShowChannelButtons == value)
                    return;
                showChannelButtons = value;
                OnShowContextButtonsChanged();
            }
        }

        bool showChartModeButton = true;
        [DefaultValue(true)]
        public virtual bool ShowChartModeButton {
            get { return showChartModeButton; }
            set {
                if(ShowChartModeButton == value)
                    return;
                showChartModeButton = value;
                OnShowContextButtonsChanged();
            }
        }

        private void OnShowContextButtonsChanged() {
            ContextButtons.Clear();
            CreateDefaultButtons();
        }

        ContextItemCollectionOptions contextButtonOptions;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), TypeConverter(typeof(ExpandableObjectConverter))]
        public ContextItemCollectionOptions ContextButtonOptions {
            get {
                if(contextButtonOptions == null) {
                    contextButtonOptions = CreateContextButtonOptions();
                }
                return contextButtonOptions;
            }
        }

        protected virtual ContextItemCollectionOptions CreateContextButtonOptions() {
            return new ContextItemCollectionOptions(this);
        }

        protected virtual ContextItemCollection CreateContextButtonsCollection() {
            return new ContextItemCollection(this);
        }

        void IContextItemCollectionOwner.OnCollectionChanged() {
            ((HistogrammControlViewInfo)ViewInfo).IsContextButtonsReady = false;
            OnPropertiesChanged();
        }

        protected internal int[] RedChannel { get; set; }
        protected internal int[] PrevRedChannel { get; set; }

        protected internal int[] GreenChannel { get; set; }
        protected internal int[] PrevGreenChannel { get; set; }

        protected internal int[] BlueChannel { get; set; }
        protected internal int[] PrevBlueChannel { get; set; }

        protected internal int MaxRedChannel { get; set; }
        protected internal int MaxGreenChannel { get; set; }
        protected internal int MaxBlueChannel { get; set; }

        protected internal int PrevMaxRedChannel { get; set; }
        protected internal int PrevMaxGreenChannel { get; set; }
        protected internal int PrevMaxBlueChannel { get; set; }

        public void SetChannels(int[] redChannel, int[] greenChannel, int[] blueChannel, bool animated) {
            PrevRedChannel = RedChannel;
            PrevGreenChannel = GreenChannel;
            PrevBlueChannel = BlueChannel;

            PrevMaxRedChannel = MaxRedChannel;
            PrevMaxGreenChannel = MaxGreenChannel;
            PrevMaxBlueChannel = MaxBlueChannel;
            PrevMaxChannel = MaxChannel;

            RedChannel = redChannel;
            BlueChannel = blueChannel;
            GreenChannel = greenChannel;

            MaxRedChannel = CalcMaxRedChannel();
            MaxGreenChannel = CalcMaxGreenChannel();
            MaxBlueChannel = CalcMaxBlueChannel();

            MaxChannel = CalcMaxChannel();

            if(animated) {
                ShouldMakeTransition = true;
                OnPropertiesChanged();
            }
            else {
                Invalidate();
                Update();
            }
        }

        private int CalcMaxChannel() {
            return Math.Max(1, Math.Max(MaxBlueChannel, Math.Max(MaxGreenChannel, MaxBlueChannel)));
        }

        public void CreateHistogramm(Bitmap image) {
            int[] rc = new int[256];
            int[] bc = new int[256];
            int[] gc = new int[256];
            for(int i = 0; i < image.Width; i++) {
                for(int j = 0; j < image.Height; j++) {
                    Color c = image.GetPixel(i, j);
                    rc[c.R]++;
                    gc[c.G]++;
                    bc[c.B]++;
                }
            }
            SetChannels(rc, gc, bc, true);
        }

        protected internal object HistogrammAnimationId = new object();
        static int HistogrammAnimationLength { get { return 400; } }
        protected internal virtual void MakeTransition() {
            ShouldMakeTransition = false;
            XtraAnimator.Current.Animations.Add(new FloatAnimationInfo(this, HistogrammAnimationId, HistogrammAnimationLength, 0.0f, 1.0f, true));
        }

        protected virtual int CalcMaxBlueChannel() {
            return CalcMaxChannelCore(RedChannel);
        }

        protected virtual int CalcMaxChannelCore(int[] channel) {
            return channel.Max();
        }

        protected virtual int CalcMaxGreenChannel() {
            return CalcMaxChannelCore(GreenChannel);
        }

        protected virtual int CalcMaxRedChannel() {
            return CalcMaxChannelCore(BlueChannel);
        }

        void IContextItemCollectionOwner.OnItemChanged(ContextItem item, string propertyName, object oldValue, object newValue) {
            if(propertyName == "Checked" || propertyName == "Value" || propertyName == "Rating") {
                if(propertyName == "Checked") {
                    OnContextItemCheckedChanged(item);
                }
                RaiseContextButtonValueChanged(new ContextButtonValueEventArgs(item, newValue));
            }
            if(propertyName == "Visibility" || propertyName == "Value") {
                Invalidate();
                Update();
                return;
            }
            OnPropertiesChanged();
        }

        private void OnContextItemCheckedChanged(ContextItem item) {
            CheckContextButton check = item as CheckContextButton;
            if(check == null)
                return;
            if(check.Name == ShowShadowClippingButtonName) {
                OnShowShadowClipping(check.Checked);
            }
            else if(check.Name == ShowHighlightClippingButtonName) {
                OnShowHighlightClipping(check.Checked);
            }
            else if(check.Name == ShowRedChannelButtonName) {
                OnShowRedChannel(check.Checked);
            }
            else if(check.Name == ShowGreenChannelButtonName) {
                OnShowGreenChannel(check.Checked);
            }
            else if(check.Name == ShowBlueChannelButtonName) {
                OnShowBlueChannel(check.Checked);
            }
            else if(check.Name == ShowAllChannelButtonName) {
                OnShowAllChannels(check.Checked);
            }
            else if(check.Name == ChartPrevewModeButtonName) {
                OnChartMode(check.Checked);
            }
        }

        protected virtual void OnChartMode(bool isChecked) {
            ChartPreviewMode = isChecked ? HistogrammChartPreviewMode.Line : HistogrammChartPreviewMode.Area;
        }

        protected virtual void OnShowAllChannels(bool show) {
            if(!show) return;
            ((CheckContextButton)ContextButtons[ShowRedChannelButtonName]).Checked = false;
            ((CheckContextButton)ContextButtons[ShowGreenChannelButtonName]).Checked = false;
            ((CheckContextButton)ContextButtons[ShowBlueChannelButtonName]).Checked = false;
            Mode = HistogrammChannelPreviewMode.All;
        }

        protected virtual void OnShowBlueChannel(bool show) {
            if(!show) return;
            ((CheckContextButton)ContextButtons[ShowRedChannelButtonName]).Checked = false;
            ((CheckContextButton)ContextButtons[ShowGreenChannelButtonName]).Checked = false;
            ((CheckContextButton)ContextButtons[ShowAllChannelButtonName]).Checked = false;
            Mode = HistogrammChannelPreviewMode.Blue;
        }

        protected virtual void OnShowGreenChannel(bool show) {
            if(!show) return;
            ((CheckContextButton)ContextButtons[ShowRedChannelButtonName]).Checked = false;
            ((CheckContextButton)ContextButtons[ShowBlueChannelButtonName]).Checked = false;
            ((CheckContextButton)ContextButtons[ShowAllChannelButtonName]).Checked = false;
            Mode = HistogrammChannelPreviewMode.Green;
        }

        protected virtual void OnShowRedChannel(bool show) {
            if(!show) return;
            ((CheckContextButton)ContextButtons[ShowAllChannelButtonName]).Checked = false;
            ((CheckContextButton)ContextButtons[ShowBlueChannelButtonName]).Checked = false;
            ((CheckContextButton)ContextButtons[ShowGreenChannelButtonName]).Checked = false;
            Mode = HistogrammChannelPreviewMode.Red;
        }

        protected virtual void OnShowHighlightClipping(bool show) {

        }

        protected virtual void OnShowShadowClipping(bool show) {

        }

        bool IContextItemCollectionOwner.IsDesignMode { get { return IsDesignMode; } }
        bool IContextItemCollectionOwner.IsRightToLeft {
            get { return false; }
        }
        void IContextItemCollectionOptionsOwner.OnOptionsChanged(string propertyName, object oldValue, object newValue) {
            OnPropertiesChanged();
        }

        ContextAnimationType IContextItemCollectionOptionsOwner.AnimationType { get { return ContextAnimationType.OpacityAnimation; } }

        public event ContextItemClickEventHandler ContextButtonClick {
            add { Events.AddHandler(contextButtonClick, value); }
            remove { Events.RemoveHandler(contextButtonClick, value); }
        }

        protected internal void RaiseContextButtonClick(ContextItemClickEventArgs e) {
            ContextItemClickEventHandler handler = Events[contextButtonClick] as ContextItemClickEventHandler;
            if(handler != null)
                handler(this, e);
        }

        public event ContextButtonToolTipEventHandler CustomContextButtonToolTip {
            add { Events.AddHandler(customContextButtonToolTip, value); }
            remove { Events.RemoveHandler(customContextButtonToolTip, value); }
        }
        protected internal void RaiseCustomContextButtonToolTip(ContextButtonToolTipEventArgs e) {
            ContextButtonToolTipEventHandler handler = Events[customContextButtonToolTip] as ContextButtonToolTipEventHandler;
            if(handler != null)
                handler(this, e);
        }
        public event ContextButtonValueChangedEventHandler ContextButtonValueChanged {
            add { Events.AddHandler(contextButtonValueChanged, value); }
            remove { Events.RemoveHandler(contextButtonValueChanged, value); }
        }
        protected internal void RaiseContextButtonValueChanged(ContextButtonValueEventArgs e) {
            ContextButtonValueChangedEventHandler handler = Events[contextButtonValueChanged] as ContextButtonValueChangedEventHandler;
            if(handler != null)
                handler(this, e);
        }
        protected internal HistogrammControlViewInfo HistogrammViewInfo {
            get { return (HistogrammControlViewInfo)ViewInfo; }
        }

        void IMouseWheelSupport.OnMouseWheel(MouseEventArgs e) {
            OnMouseWheelCore(e);
        }

        protected virtual void OnMouseWheelCore(MouseEventArgs e) {
            DXMouseEventArgs ee = DXMouseEventArgs.GetMouseArgs(e);
            try {
                base.OnMouseWheel(ee);
                if(Handler.OnMouseWheel(ee))
                    return;
            }
            finally {
                ee.Sync();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            if(XtraForm.ProcessSmartMouseWheel(this, e)) return;
            
        }
        protected override void OnMouseDown(MouseEventArgs e) {
            DXMouseEventArgs ee = DXMouseEventArgs.GetMouseArgs(e);
            base.OnMouseDown(ee);
            if(ee.Handled)
                return;
            ContextButtonsHandler.ViewInfo = HistogrammViewInfo.ContextButtonsViewInfo;
            if(ContextButtonsHandler.OnMouseDown(e))
                return;
            if(Handler.OnMouseDown(e))
                return;
        }
        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            ContextButtonsHandler.ViewInfo = HistogrammViewInfo.ContextButtonsViewInfo;
            ContextButtonsHandler.OnMouseMove(e);
            Handler.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            DXMouseEventArgs ee = DXMouseEventArgs.GetMouseArgs(e);
            base.OnMouseUp(ee);
            if(ee.Handled)
                return;
            if(ContextButtonsHandler.OnMouseUp(e))
                return;
            if(Handler.OnMouseUp(e))
                return;
        }
        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);

            ContextButtonsHandler.ViewInfo = HistogrammViewInfo.ContextButtonsViewInfo;
            ContextButtonsHandler.OnMouseEnter(e);
            Handler.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
            ContextButtonsHandler.ViewInfo = HistogrammViewInfo.ContextButtonsViewInfo;
            ContextButtonsHandler.OnMouseLeave(e);
            Handler.OnMouseLeave(e);
        }

        ContextItemCollectionHandler contextButtonsHandler;
        protected ContextItemCollectionHandler ContextButtonsHandler {
            get {
                if(contextButtonsHandler == null)
                    contextButtonsHandler = CreateContextButtonsHandler();
                return contextButtonsHandler;
            }
        }

        public int MaxChannel { get; private set; }
        public int PrevMaxChannel { get; private set; }
        bool drawGrid = true;
        [DefaultValue(true)]
        public bool DrawGrid {
            get { return drawGrid; }
            set {
                if(DrawGrid == value)
                    return;
                drawGrid = value;
                OnColorChanged();
            }
        }

        public bool ShouldMakeTransition { get; internal set; }

        protected virtual HistogrammControlHandler CreateHandler() {
            return new HistogrammControlHandler(this);
        }

        protected virtual ContextItemCollectionHandler CreateContextButtonsHandler() {
            return new ContextItemCollectionHandler();
        }

        void IXtraAnimationListener.OnAnimation(BaseAnimationInfo info) {
            Invalidate();
            Update();
        }

        void IXtraAnimationListener.OnEndAnimation(BaseAnimationInfo info) {
            Invalidate();
            Update();
        }

        protected internal virtual void OnLabelsChanged() {
            OnPropertiesChanged();
        }

        public virtual void SetLabels(IEnumerable<string> labels) {
            Labels.BeginUpdate();
            Labels.Clear();
            foreach(string label in labels) {
                Labels.Add(label);
            }
            Labels.EndUpdate();
        }

        protected internal void OnHoverInfoChanged(HistogrammHitInfo prevInfo, HistogrammHitInfo newInfo) {
            if(HistogrammViewInfo.DownInfo.HitTest != HistogrammHitTest.None)
                return;
            SetValueLabel(newInfo.HitTest);
        }

        protected string DefaultBlacksFormatString {
            get { return "F0"; }
        }

        protected string DefaultShadowsFormatString {
            get { return "F0"; }
        }

        protected string DefaultExposureFormatString {
            get { return "F2"; }
        }

        protected string DefaultHighlightsFormatString {
            get { return "F0"; }
        }

        protected string DefaultWhitesFormatString {
            get { return "F0"; }
        }

        bool ShouldSerializeBlacksFormatString() { return BlacksFormatString != DefaultBlacksFormatString; }
        void ResetBlacksFormatString() { BlacksFormatString = DefaultBlacksFormatString; }
        public string BlacksFormatString { get; set; }
        bool ShouldSerializeShadowsFormatString() { return ShadowsFormatString != DefaultShadowsFormatString; }
        void ResetShadowsFormatString() { ShadowsFormatString = DefaultShadowsFormatString; }
        public string ShadowsFormatString { get; set; }
        bool ShouldSerializeExposureFormatString() { return ExposureFormatString != DefaultExposureFormatString; }
        void ResetExposureFormatString() { ExposureFormatString = DefaultExposureFormatString; }
        public string ExposureFormatString { get; set; }
        bool ShouldSerializeHighlightsFormatString() { return HighlightsFormatString != DefaultHighlightsFormatString; }
        void ResetHighlightsFormatString() { HighlightsFormatString = DefaultHighlightsFormatString; }
        public string HighlightsFormatString { get; set; }
        bool ShouldSerializeWhitesFormatString() { return WhitesFormatString != DefaultWhitesFormatString; }
        void ResetWhitesFormatString() { WhitesFormatString = DefaultWhitesFormatString; }
        public string WhitesFormatString { get; set; }

        protected internal void SetValueLabel(HistogrammHitTest hitTest) {
            switch(hitTest) {
                case HistogrammHitTest.Blacks:
                    SetLabels("Blacks", BlacksValue.ToString(BlacksFormatString));
                    break;
                case HistogrammHitTest.Shadows:
                    SetLabels("Shadows", ShadowsValue.ToString(ShadowsFormatString));
                    break;
                case HistogrammHitTest.Exposure:
                    SetLabels("Exposure", ExposureValue.ToString(ExposureFormatString));
                    break;
                case HistogrammHitTest.Highlights:
                    SetLabels("Highlights", HighlightsValue.ToString(HighlightsFormatString));
                    break;
                case HistogrammHitTest.Whites:
                    SetLabels("Whites", WhitesValue.ToString(WhitesFormatString));
                    break;
                case HistogrammHitTest.None:
                case HistogrammHitTest.ContextButtons:
                    RestoreLabels();
                    break;
            }
        }

        protected List<string> SavedLabels { get; set; }
        protected virtual void SetDrawMonochromeCore(bool value) { this.drawMonochrome = value; }
        bool drawMonochrome = false;
        [DefaultValue(false)]
        public virtual bool DrawMonochrome {
            get { return drawMonochrome; }
            set {
                if(DrawMonochrome == value)
                    return;
                drawMonochrome = value;
                OnColorChanged();
            }
        }

        private bool showLabels = true;
        [DefaultValue(true)]
        public bool ShowLabels {
            get { return showLabels; }
            set {
                if(ShowLabels == value)
                    return;
                showLabels = value;
                OnPropertiesChanged();
            }
        }

        int IContextItemCollectionOptionsOwner.ChangeIndex { get; set; }
        int IContextItemCollectionOwner.ChangeIndex { get; set; }

        protected internal void SaveLabels() {
            SavedLabels = new List<string>();
            foreach(string label in Labels) {
                SavedLabels.Add(label);
            }
        }

        protected internal void RestoreLabels() {
            if(SavedLabels != null)
                SetLabels(SavedLabels);
        }

        protected override void OnDoubleClick(EventArgs e) {
            base.OnDoubleClick(e);
            Handler.OnDoubleClick(e);
        }
    }

    public class HistogrammControlViewInfo : BaseStyleControlViewInfo, ISupportContextItems {
        public HistogrammControlViewInfo(BaseStyleControl owner) : base(owner) { }

        public bool IsContextButtonsReady { get; internal set; }

        protected internal int[] PrevRedChannel {
            get { return HistogrammControl.PrevRedChannel; }
        }

        protected internal int[] PrevGreenChannel {
            get { return HistogrammControl.PrevGreenChannel; }
        }

        protected internal int[] PrevBlueChannel {
            get { return HistogrammControl.PrevBlueChannel; }
        }

        protected internal int[] RedChannel {
            get { return HistogrammControl.RedChannel; }
        }

        protected internal int[] GreenChannel {
            get { return HistogrammControl.GreenChannel; }
        }

        protected internal int[] BlueChannel {
            get { return HistogrammControl.BlueChannel; }
        }

        protected internal int MaxChannel {
            get { return HistogrammControl.MaxChannel; }
        }

        protected internal int PrevMaxChannel {
            get { return HistogrammControl.PrevMaxChannel; }
        }

        public virtual float GetNormalizedValue(int index, int[] channel, int maxValue) {
            return channel[index] / (float)maxValue;
        }

        HistogrammHitInfo hoverInfo;
        public HistogrammHitInfo HoverInfo {
            get {
                if(hoverInfo == null)
                    hoverInfo = new HistogrammHitInfo();
                return hoverInfo; }
            set {
                HistogrammHitInfo prevInfo = HoverInfo;
                hoverInfo = value;
                if(HoverInfo.HitTest != prevInfo.HitTest)
                    OnHoverInfoChanged(prevInfo, HoverInfo);
            }
        }

        protected virtual void OnHoverInfoChanged(HistogrammHitInfo prevInfo, HistogrammHitInfo newInfo) {
            HistogrammControl.OnHoverInfoChanged(prevInfo, newInfo);
            HistogrammControl.Invalidate();
            HistogrammControl.Update();
        }
        
        public virtual HistogrammHitInfo CalcHitInfo(Point hitPoint) {
            HistogrammHitInfo info = new HistogrammHitInfo();
            info.HitPoint = hitPoint;
            info.LastPoint = hitPoint;
            ContextItemHitInfo cinfo = ContextButtonsViewInfo.CalcHitInfo(hitPoint);
            if(cinfo != null && cinfo.HitTest != ContextItemHitTest.None)
                info.HitTest = HistogrammHitTest.ContextButtons;
            else if(BlacksAreaBounds.Contains(info.HitPoint))
                info.HitTest = HistogrammHitTest.Blacks;
            else if(ShadowsAreaBounds.Contains(info.HitPoint))
                info.HitTest = HistogrammHitTest.Shadows;
            else if(ExposureAreaBounds.Contains(info.HitPoint))
                info.HitTest = HistogrammHitTest.Exposure;
            else if(HighlightsAreaBounds.Contains(info.HitPoint))
                info.HitTest = HistogrammHitTest.Highlights;
            else if(WhitesAreaBounds.Contains(info.HitPoint))
                info.HitTest = HistogrammHitTest.Whites;
            return info;
        }

        public virtual RectangleF BlacksAreaBounds {
            get {
                return new RectangleF(ChartBounds.X, ChartBounds.Y, HistogrammControl.BlacksWidth * ChartBounds.Width, ChartBounds.Height);
            }
        }

        public virtual RectangleF ShadowsAreaBounds {
            get {
                float width = HistogrammControl.BlacksWidth;
                return new RectangleF(ChartBounds.X + ChartBounds.Width * width, ChartBounds.Y, HistogrammControl.ShadowsWidth * ChartBounds.Width, ChartBounds.Height);
            }
        }

        public virtual RectangleF ExposureAreaBounds {
            get {
                float width = HistogrammControl.BlacksWidth + HistogrammControl.ShadowsWidth;
                return new RectangleF(ChartBounds.X + ChartBounds.Width * width, ChartBounds.Y, HistogrammControl.ExposureWidth * ChartBounds.Width, ChartBounds.Height);
            }
        }

        public virtual RectangleF HighlightsAreaBounds {
            get {
                float width = HistogrammControl.BlacksWidth + HistogrammControl.ShadowsWidth + HistogrammControl.ExposureWidth;
                return new RectangleF(ChartBounds.X + ChartBounds.Width * width, ChartBounds.Y, HistogrammControl.HighlightsWidth * ChartBounds.Width, ChartBounds.Height);
            }
        }

        public virtual RectangleF WhitesAreaBounds {
            get {
                float width = HistogrammControl.BlacksWidth + HistogrammControl.ShadowsWidth + HistogrammControl.ExposureWidth + HistogrammControl.HighlightsWidth;
                return new RectangleF(ChartBounds.X + ChartBounds.Width * width, ChartBounds.Y, ChartBounds.Right - HighlightsAreaBounds.Right, ChartBounds.Height);
            }
        }

        HistogrammHitInfo downInfo;
        public HistogrammHitInfo DownInfo {
            get {
                if(downInfo == null)
                    downInfo = new HistogrammHitInfo();
                return downInfo;
            }
            set {
                HistogrammHitInfo prevInfo = DownInfo;
                downInfo = value;
                if(prevInfo.HitTest != DownInfo.HitTest)
                    OnDownInfoChanged(prevInfo, DownInfo);
            }
        }

        protected virtual void OnDownInfoChanged(HistogrammHitInfo prevInfo, HistogrammHitInfo downInfo) {
            
        }

        Pen[,,] penPallete;
        protected Pen[,,] PenPallete {
            get {
                if(penPallete == null)
                    penPallete = CreatePenPalette();
                return penPallete;
            }
        }

        Brush[,,] brushPallete;
        protected Brush[,,] BrushPallete {
            get {
                if(brushPallete == null)
                    brushPallete = CreateBrushPallete();
                return brushPallete;
            }
        }

        protected virtual Brush[,,] CreateBrushPallete() {
            Brush[,,] res = new SolidBrush[2, 2, 2];

            res[0, 0, 0] = new SolidBrush(Color.Transparent);
            res[1, 0, 0] = new SolidBrush(HistogrammControl.RedChannelColor);
            res[1, 1, 0] = new SolidBrush(HistogrammControl.RedGreenChannelColor);
            res[1, 0, 1] = new SolidBrush(HistogrammControl.RedBlueChannelColor);
            res[1, 1, 1] = new SolidBrush(HistogrammControl.RedGreenBlueChannelColor);
            res[0, 1, 0] = new SolidBrush(HistogrammControl.GreenChannelColor);
            res[0, 1, 1] = new SolidBrush(HistogrammControl.GreenBlueChannelColor);
            res[1, 1, 0] = new SolidBrush(HistogrammControl.RedGreenChannelColor);
            res[0, 0, 1] = new SolidBrush(HistogrammControl.BlueChannelColor);
            res[1, 0, 1] = new SolidBrush(HistogrammControl.RedBlueChannelColor);
            res[0, 1, 1] = new SolidBrush(HistogrammControl.GreenBlueChannelColor);

            return res;
        }

        public override void CalcViewInfo(Graphics g) {
            ResetPalette();
            base.CalcViewInfo(g);
        }

        protected override void CalcRects() {
            base.CalcRects();
            CreateLabels();
            InfoAreaBounds = CalcInfoAreaBounds();
            InfoAreaContentBounds = CalcInfoAreaContentBounds();
            CalcLabels();
            ChartBounds = CalcChartBounds();
            CalcContextButtons();
            if(HistogrammControl.ShouldMakeTransition) {
                HistogrammControl.MakeTransition();
            }
        }

        protected virtual void CalcLabels() {
            if(Labels.Count == 0)
                return;
            if(Labels.Count == 1) {
                CenterLabel(Labels[0], InfoAreaContentBounds);
                return;
            }
            LayoutLabelLeft(Labels[0], InfoAreaContentBounds);
            LayoutLabelRight(Labels[Labels.Count - 1], InfoAreaContentBounds);
            //int width = CalcLabelsMiddleWidth();
            int availWidth = InfoAreaContentBounds.Width - Labels[0].Bounds.Width - Labels[1].Bounds.Width;

            int startX = Labels[0].Bounds.Right;
            float delta = availWidth / (float)(Labels.Count - 1);
            for(int i = 1; i < Labels.Count - 1; i++) {
                int x = (int)(startX + i * delta - Labels[i].Bounds.Width / 2);
                Labels[i].SetLocation(x, InfoAreaContentBounds.Y + (InfoAreaContentBounds.Height - Labels[i].Bounds.Height) / 2);
            }
        }

        //private int CalcLabelsMiddleWidth() {
        //    return Labels.Sum((lb) => lb.Bounds.Width);
        //}

        private void LayoutLabelRight(StringInfo stringInfo, Rectangle bounds) {
            stringInfo.SetLocation(bounds.Right - stringInfo.Bounds.Width, bounds.Y + (bounds.Height - stringInfo.Bounds.Height) / 2);
        }

        private void LayoutLabelLeft(StringInfo stringInfo, Rectangle bounds) {
            stringInfo.SetLocation(bounds.X, bounds.Y + (bounds.Height - stringInfo.Bounds.Height) / 2);
        }

        private void CenterLabel(StringInfo stringInfo, Rectangle bounds) {
            CenterLabel(stringInfo, bounds.X, bounds.Width, bounds.Y, bounds.Height);
        }
        private void CenterLabel(StringInfo stringInfo, int x, int width, int y, int height) {
            stringInfo.SetLocation(x + (width - Labels[0].Bounds.Width) / 2, y + (height - Labels[0].Bounds.Height) / 2);
        }

        HistogrammLabelInfoCollection labels;
        public HistogrammLabelInfoCollection Labels {
            get {
                if(labels == null)
                    labels = new HistogrammLabelInfoCollection();
                return labels;
            }
        }

        protected virtual void CreateLabels() {
            StringInfo[] cache = new StringInfo[Labels.Count];
            Labels.CopyTo(cache, 0);
            Labels.Clear();
            foreach(string label in HistogrammControl.Labels) {
                Labels.Add(GetLabel(cache, label));
            }
        }

        public override AppearanceDefault DefaultAppearance {
            get {
                AppearanceDefault app = base.DefaultAppearance;
                app.ForeColor = CommonSkins.GetSkin(LookAndFeel.ActiveLookAndFeel).GetSystemColor(SystemColors.ControlText);
                return app;
            }
        }

        protected virtual StringInfo GetLabel(StringInfo[] cache, string label) {
            StringInfo info = cache.FirstOrDefault((si) => si.SourceString == label);
            if(info != null)
                return info;
            return StringPainter.Default.Calculate(GInfo.Graphics, PaintAppearance, label, 10000);
        }

        protected virtual Rectangle CalcInfoAreaContentBounds() {
            Rectangle res = InfoAreaBounds;
            res.Inflate(-LabelsTextIndent, -LabelsTextIndent);
            return res;
        }

        protected virtual int CalcLabelsMaxTextHeight() {
            return StringPainter.Default.Calculate(GInfo.Graphics, PaintAppearance, "Wg", 0).Bounds.Height;
        }

        protected virtual int LabelsTextIndent {
            get { return 3; }
        }

        protected virtual Rectangle CalcInfoAreaBounds() {
            if(!ShowLabels)
                return Rectangle.Empty;
            Rectangle rect = ContentRect;
            rect.Height = CalcLabelsMaxTextHeight() + LabelsTextIndent * 2;
            rect.Y = ContentRect.Bottom - rect.Height;
            return rect;
        }

        protected virtual Rectangle CalcChartBounds() {
            Rectangle rect = ContentRect;
            rect.Height -= InfoAreaBounds.Height;
            return rect;
        }

        public Rectangle ChartBounds { get; private set; }
        public Rectangle InfoAreaBounds { get; private set; }

        private void ResetPalette() {
            if(this.penPallete != null) {
                for(int i = 0; i < 2; i++) {
                    for(int j = 0; j < 2; j++) {
                        for(int k = 0; k < 2; k++) {
                            this.penPallete[i, j, k].Dispose();
                        }
                    }
                }
            }
            this.penPallete = null;

            if(this.brushPallete != null) {
                for(int i = 0; i < 2; i++) {
                    for(int j = 0; j < 2; j++) {
                        for(int k = 0; k < 2; k++) {
                            this.brushPallete[i, j, k].Dispose();
                        }
                    }
                }
            }
            this.brushPallete = null;
        }

        protected virtual Pen[,,] CreatePenPalette() {
            Pen[,,] res = new Pen[2, 2, 2];

            res[0, 0, 0] = new Pen(Color.Transparent);
            res[1, 0, 0] = new Pen(HistogrammControl.RedChannelColor);
            res[1, 1, 0] = new Pen(HistogrammControl.RedGreenChannelColor);
            res[1, 0, 1] = new Pen(HistogrammControl.RedBlueChannelColor);
            res[1, 1, 1] = new Pen(HistogrammControl.RedGreenBlueChannelColor);
            res[0, 1, 0] = new Pen(HistogrammControl.GreenChannelColor);
            res[0, 1, 1] = new Pen(HistogrammControl.GreenBlueChannelColor);
            res[1, 1, 0] = new Pen(HistogrammControl.RedGreenChannelColor);
            res[0, 0, 1] = new Pen(HistogrammControl.BlueChannelColor);
            res[1, 0, 1] = new Pen(HistogrammControl.RedBlueChannelColor);
            res[0, 1, 1] = new Pen(HistogrammControl.GreenBlueChannelColor);

            return res;
        }
        protected virtual bool DrawOnlyMaxChannel { get { return false; } }
        protected internal virtual Pen GetPenColor(float redValue, float greenValue, float blueValue, float v) {
            int hasRedValue = redValue >= v ? 1 : 0;
            int hasGreenValue = greenValue >= v ? 1 : 0;
            int hasBlueValue = blueValue >= v ? 1 : 0;
            return PenPallete[hasRedValue,hasGreenValue,hasBlueValue];
        }
        Pen grayPen;
        public Pen MonochromePen {
            get {
                if(grayPen == null)
                    grayPen = new Pen(Color.FromArgb(40, 80, 80, 80));
                return grayPen;
            }
        }
        protected internal Brush GetBrushColor(float redValue, float greenValue, float blueValue, float v) {
            int hasRedValue = redValue >= v ? 1 : 0;
            int hasGreenValue = greenValue >= v ? 1 : 0;
            int hasBlueValue = blueValue >= v ? 1 : 0;
            return BrushPallete[hasRedValue, hasGreenValue, hasBlueValue];
        }

        protected internal void SortValues(float redValue, float greenValue, float blueValue, out float v1, out float v2, out float v3) {
            float v;
            v1 = redValue; v2 = greenValue; v3 = blueValue;
            if(v1 > v2) {
                v = v2;
                v2 = v1;
                v1 = v;
            }
            if(v1 > v3) {
                v = v3;
                v3 = v1;
                v1 = v;
            }
            if(v2 > v3) {
                v = v3;
                v3 = v2;
                v2 = v;
            }
        }

        protected Rectangle LastContextButtonsDisplayRectangle { get; set; }
        private void CalcContextButtons() {
            if(IsContextButtonsReady) return;
            if(LastContextButtonsDisplayRectangle != ((ISupportContextItems)this).DisplayBounds) {
                ContextButtonsViewInfo.Refresh();
            }
            ContextButtonsViewInfo.CalcItems();
            IsContextButtonsReady = true;
        }

        ContextItemCollectionOptions ISupportContextItems.Options {
            get { return HistogrammControl.ContextButtonOptions; }
        }

        Rectangle ISupportContextItems.DisplayBounds {
            get { return ContentRect; }
        }

        Rectangle ISupportContextItems.DrawBounds {
            get { return ContentRect; }
        }

        Rectangle ISupportContextItems.ActivationBounds {
            get { return ContentRect; }
        }

        ContextItemCollection ISupportContextItems.ContextItems {
            get {
                return HistogrammControl.ContextButtons;
            }
        }

        Control ISupportContextItems.Control {
            get {
                return HistogrammControl;
            }
        }

        public HistogrammControl HistogrammControl {
            get { return (HistogrammControl)OwnerControl; }
        }

        bool ISupportContextItems.DesignMode { get { return HistogrammControl.IsDesignMode; } }

        bool ISupportContextItems.CloneItems { get { return false; } }
        void ISupportContextItems.RaiseCustomizeContextItem(ContextItem item) { }
        void ISupportContextItems.RaiseContextItemClick(ContextItemClickEventArgs e) {
            HistogrammControl.RaiseContextButtonClick(e);
        }
        void ISupportContextItems.RaiseCustomContextButtonToolTip(ContextButtonToolTipEventArgs e) {
            HistogrammControl.RaiseCustomContextButtonToolTip(e);
        }

        bool ISupportContextItems.ShowOutsideDisplayBounds { get { return false; } }

        ItemHorizontalAlignment ISupportContextItems.GetCaptionHorizontalAlignment(ContextButton btn) {
            return ItemHorizontalAlignment.Left;
        }

        ItemVerticalAlignment ISupportContextItems.GetCaptionVerticalAlignment(ContextButton btn) {
            return ItemVerticalAlignment.Center;
        }

        ItemHorizontalAlignment ISupportContextItems.GetGlyphHorizontalAlignment(ContextButton btn) {
            return ItemHorizontalAlignment.Left;
        }

        ItemLocation ISupportContextItems.GetGlyphLocation(ContextButton btn) {
            return ItemLocation.Default;
        }

        protected int ContextButtonGlyphToCaptionIndent { get { return 3; } }
        int ISupportContextItems.GetGlyphToCaptionIndent(ContextButton btn) {
            return ContextButtonGlyphToCaptionIndent;
        }

        ItemVerticalAlignment ISupportContextItems.GetGlyphVerticalAlignment(ContextButton btn) {
            return ItemVerticalAlignment.Center;
        }

        UserLookAndFeel ISupportContextItems.LookAndFeel {
            get { return LookAndFeel.ActiveLookAndFeel; }
        }

        void ISupportContextItems.Redraw() {
            OwnerControl.Invalidate();
        }
        void ISupportContextItems.Update() {
            OwnerControl.Update();
        }
        void ISupportContextItems.Redraw(Rectangle rect) {
            OwnerControl.Invalidate(rect);
        }
        ContextItemCollectionViewInfo contextButtonsViewInfo;
        protected internal ContextItemCollectionViewInfo ContextButtonsViewInfo {
            get {
                if(contextButtonsViewInfo == null)
                    contextButtonsViewInfo = CreateContextButtonsViewInfo();

                return contextButtonsViewInfo;
            }
        }

        public bool AllowRedChannel { get { return HistogrammControl.Mode == HistogrammChannelPreviewMode.Red || HistogrammControl.Mode == HistogrammChannelPreviewMode.All; } }
        public bool AllowGreenChannel { get { return HistogrammControl.Mode == HistogrammChannelPreviewMode.Green || HistogrammControl.Mode == HistogrammChannelPreviewMode.All; } }
        public bool AllowBlueChannel { get { return HistogrammControl.Mode == HistogrammChannelPreviewMode.Blue || HistogrammControl.Mode == HistogrammChannelPreviewMode.All; } }

        public Rectangle InfoAreaContentBounds { get; private set; }
        public virtual bool DrawMonochrome { get { return HistogrammControl.DrawMonochrome; } }

        protected virtual ContextItemCollectionViewInfo CreateContextButtonsViewInfo() {
            return new ContextItemCollectionViewInfo(((ISupportContextItems)this).ContextItems, ((ISupportContextItems)this).Options, this);
        }
        public virtual bool ShowLabels {
            get { return HistogrammControl.ShowLabels; }
        }

        public virtual bool AllowDrawVerticalTickLine { get { return false; } }

        //public override ToolTipControlInfo GetToolTipInfo(Point point) {
        //    ToolTipControlInfo info = base.GetToolTipInfo(point);
        //    ToolTipControlInfo contextBtnInfo = ContextButtonsViewInfo.GetToolTipInfo(point);
        //    return contextBtnInfo == null ? info : contextBtnInfo;
        //}
        public virtual ContextItemHitInfo CalcContextButtonHitInfo(Point point) {
            return contextButtonsViewInfo.CalcHitInfo(point);
        }

        public float GetRedValue(int index1) {
            return AllowRedChannel ? GetNormalizedValue(index1, RedChannel, MaxChannel) : 0.0f;
        }

        public float GetGreenValue(int index1) {
            return AllowGreenChannel ? GetNormalizedValue(index1, GreenChannel, MaxChannel) : 0.0f;
        }

        public float GetBlueValue(int index1) {
            return AllowBlueChannel ? GetNormalizedValue(index1, BlueChannel, MaxChannel) : 0.0f;
        }

        public float GetPrevRedValue(int index1) {
            return AllowRedChannel ? GetNormalizedValue(index1, PrevRedChannel, PrevMaxChannel) : 0.0f;
        }

        public float GetPrevGreenValue(int index1) {
            return AllowGreenChannel ? GetNormalizedValue(index1, PrevGreenChannel, PrevMaxChannel) : 0.0f;
        }

        public float GetPrevBlueValue(int index1) {
            return AllowBlueChannel ? GetNormalizedValue(index1, PrevBlueChannel, PrevMaxChannel) : 0.0f;
        }

        internal void ResetContextButtons() {
            IsContextButtonsReady = false;
        }
    }

    public class HistogrammControlPainter : BaseControlPainter {
        public HistogrammControlPainter() { }

        protected override void DrawContent(ControlGraphicsInfoArgs info) {
            base.DrawContent(info);
            HistogrammControlViewInfo viewInfo = (HistogrammControlViewInfo)info.ViewInfo;
            if(viewInfo.HistogrammControl.DrawGrid)
                DrawGrid(info);
            DrawHistogramm(info);
            DrawContextItems(info);
            DrawSelectedExposureArea(info);
            DrawLabels(info);
        }

        protected virtual void DrawLabels(ControlGraphicsInfoArgs info) {
            HistogrammControlViewInfo viewInfo = (HistogrammControlViewInfo)info.ViewInfo;
            foreach(StringInfo label in viewInfo.Labels)
                StringPainter.Default.DrawString(info.Cache, label);
        }

        protected virtual void DrawSelectedExposureArea(ControlGraphicsInfoArgs info) {
            HistogrammControlViewInfo viewInfo = (HistogrammControlViewInfo)info.ViewInfo;
            if(viewInfo.HoverInfo.HitTest == HistogrammHitTest.Blacks)
                DrawAreaCore(info, viewInfo.BlacksAreaBounds);
            else if(viewInfo.HoverInfo.HitTest == HistogrammHitTest.Shadows)
                DrawAreaCore(info, viewInfo.ShadowsAreaBounds);
            else if(viewInfo.HoverInfo.HitTest == HistogrammHitTest.Exposure)
                DrawAreaCore(info, viewInfo.ExposureAreaBounds);
            else if(viewInfo.HoverInfo.HitTest == HistogrammHitTest.Highlights)
                DrawAreaCore(info, viewInfo.HighlightsAreaBounds);
            else if(viewInfo.HoverInfo.HitTest == HistogrammHitTest.Whites)
                DrawAreaCore(info, viewInfo.WhitesAreaBounds);
        }

        private void DrawAreaCore(ControlGraphicsInfoArgs info, RectangleF bounds) {
            HistogrammControlViewInfo viewInfo = (HistogrammControlViewInfo)info.ViewInfo;
            info.Cache.FillRectangle(info.Cache.GetSolidBrush(viewInfo.HistogrammControl.ExposureAreaColor), bounds);
        }

        private void DrawContextItems(ControlGraphicsInfoArgs info) {
            HistogrammControlViewInfo viewInfo = (HistogrammControlViewInfo)info.ViewInfo;
            new ContextItemCollectionPainter().Draw(new ContextItemCollectionInfoArgs(viewInfo.ContextButtonsViewInfo, info.Cache, info.Bounds));
        }

        private void DrawGrid(ControlGraphicsInfoArgs info) {
            HistogrammControlViewInfo viewInfo = (HistogrammControlViewInfo)info.ViewInfo;
            if(!viewInfo.HistogrammControl.DrawGrid)
                return;

            float deltaSmall = viewInfo.ChartBounds.Width / 20.0f;
            Pen thinLine = info.Cache.GetPen(viewInfo.HistogrammControl.GridThinLineColor);
            Pen thickLine = info.Cache.GetPen(viewInfo.HistogrammControl.GridThickLineColor);
            int index = 1;
            for(float x = viewInfo.ChartBounds.X + deltaSmall; x <= viewInfo.ChartBounds.Right - 1; x += deltaSmall) {
                bool isThickLine = index % 5 == 0;
                info.Graphics.DrawLine(isThickLine? thickLine: thinLine, x, viewInfo.ChartBounds.Bottom, x, viewInfo.ChartBounds.Top);
                index++;
            }
            index = 1;
            for(float y = viewInfo.ChartBounds.Bottom - deltaSmall; y >= viewInfo.ChartBounds.Top - 1; y -= deltaSmall) {
                bool isThickLine = viewInfo.AllowDrawVerticalTickLine && (index % 5 == 0);
                info.Graphics.DrawLine(isThickLine? thickLine : thinLine, viewInfo.ChartBounds.Left, y, viewInfo.ChartBounds.Right, y);
                index++;
            }
        }

        protected virtual void DrawHistogramm(ControlGraphicsInfoArgs info) {
            HistogrammControlViewInfo viewInfo = (HistogrammControlViewInfo)info.ViewInfo;
            if(viewInfo.RedChannel == null)
                return;

            float height = viewInfo.ChartBounds.Height;
            float width = viewInfo.ChartBounds.Width;
            PointF pt0 = PointF.Empty, pt1 = PointF.Empty, pt2 = PointF.Empty, pt3 = PointF.Empty;
            float v1, v2, v3;
            Pen cp1, cp2, cp3;
            //Brush cb1, cb2, cb3;

            FloatAnimationInfo animInfo = (FloatAnimationInfo)XtraAnimator.Current.Get(viewInfo.HistogrammControl, viewInfo.HistogrammControl.HistogrammAnimationId);
            float koeff = 1.0f;
            if(animInfo != null)
                koeff = animInfo.Value;

            float arrayKoeff = (viewInfo.RedChannel.Length - 1) / width;
            PointF prevPt0 = PointF.Empty, prevPt1 = PointF.Empty, prevPt2 = PointF.Empty, prevPt3 = PointF.Empty;
            bool hasPrevPoint = false;

            Pen redPen = info.Cache.GetPen(viewInfo.HistogrammControl.RedChannelColor);
            Pen greenPen = info.Cache.GetPen(viewInfo.HistogrammControl.GreenChannelColor);
            Pen bluePen = info.Cache.GetPen(viewInfo.HistogrammControl.BlueChannelColor);

            for(float x = 0; x < viewInfo.ChartBounds.Width - 1; x+= 1.0f) {
                int index1 = (int)(x * arrayKoeff + 0.5f);
                int index2 = (int)((x + 1.0f) * arrayKoeff + 0.5f);

                float redValue = viewInfo.GetRedValue(index1);
                float greenValue = viewInfo.GetGreenValue(index1);
                float blueValue = viewInfo.GetBlueValue(index1);

                if(animInfo != null) {
                    float prevRedValue = viewInfo.GetPrevRedValue(index1);
                    float prevGreenValue = viewInfo.GetPrevGreenValue(index1);
                    float prevBlueValue = viewInfo.GetPrevBlueValue(index1);

                    redValue = prevRedValue + (redValue - prevRedValue) * koeff;
                    greenValue = prevGreenValue + (greenValue - prevGreenValue) * koeff;
                    blueValue = prevBlueValue + (blueValue - prevBlueValue) * koeff;
                }

                if(viewInfo.HistogrammControl.ChartPreviewMode == HistogrammChartPreviewMode.Area) {
                    viewInfo.SortValues(redValue, greenValue, blueValue, out v1, out v2, out v3);

                    pt0.X = pt1.X = pt2.X = pt3.X = viewInfo.ChartBounds.X + x;
                    pt0.Y = viewInfo.ChartBounds.Bottom;
                    pt1.Y = viewInfo.ChartBounds.Bottom - v1 * height;
                    pt2.Y = viewInfo.ChartBounds.Bottom - v2 * height;
                    pt3.Y = viewInfo.ChartBounds.Bottom - v3 * height;

                    if(hasPrevPoint) {
                        //if(index1 == index2) {
                        if(viewInfo.DrawMonochrome) {
                            info.Graphics.DrawLine(viewInfo.MonochromePen, pt0, pt3);
                        }
                        else {
                            cp1 = viewInfo.GetPenColor(redValue, greenValue, blueValue, v1);
                            cp2 = viewInfo.GetPenColor(redValue, greenValue, blueValue, v2);
                            cp3 = viewInfo.GetPenColor(redValue, greenValue, blueValue, v3);

                            info.Graphics.DrawLine(cp1, pt0, pt1);
                            info.Graphics.DrawLine(cp2, pt1, pt2);
                            info.Graphics.DrawLine(cp3, pt2, pt3);
                        }
                        
                        //}
                        //else {
                        //cb1 = viewInfo.GetBrushColor(redValue, greenValue, blueValue, v1);
                        //cb2 = viewInfo.GetBrushColor(redValue, greenValue, blueValue, v2);
                        //cb3 = viewInfo.GetBrushColor(redValue, greenValue, blueValue, v3);

                        //info.Graphics.FillPolygon(cb1, new Point[] { prevPt0, prevPt1, pt1, pt0 });
                        //info.Graphics.DrawLine(cp1, pt0, pt1);
                        //info.Graphics.DrawLine(cp2, pt1, pt2);
                        //info.Graphics.DrawLine(cp3, pt2, pt3);
                        //}
                    }
                }
                else {
                    pt0.X = pt1.X = pt2.X = pt3.X = x;
                    pt0.Y = viewInfo.ChartBounds.Bottom;
                    pt1.Y = viewInfo.ChartBounds.Bottom - redValue * height;
                    pt2.Y = viewInfo.ChartBounds.Bottom - greenValue * height;
                    pt3.Y = viewInfo.ChartBounds.Bottom - blueValue * height;

                    if(hasPrevPoint) {
                        if(viewInfo.AllowRedChannel)
                            info.Graphics.DrawLine(viewInfo.DrawMonochrome? viewInfo.MonochromePen: redPen, prevPt1, pt1);
                        if(viewInfo.AllowGreenChannel)
                            info.Graphics.DrawLine(viewInfo.DrawMonochrome ? viewInfo.MonochromePen : greenPen, prevPt2, pt2);
                        if(viewInfo.AllowBlueChannel)
                            info.Graphics.DrawLine(viewInfo.DrawMonochrome ? viewInfo.MonochromePen : bluePen, prevPt3, pt3);
                    }
                }

                prevPt0 = pt0;
                prevPt1 = pt1;
                prevPt2 = pt2;
                prevPt3 = pt3;

                hasPrevPoint = true;
            }
        }
    }

    public class HistogrammControlHandler {
        public HistogrammControlHandler(HistogrammControl owner) {
            Owner = owner;
        }

        protected HistogrammControl Owner { get; private set; }
        protected HistogrammControlViewInfo ViewInfo {
            get { return Owner.HistogrammViewInfo; }
        }
        public virtual bool OnMouseDown(MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                ViewInfo.DownInfo = ViewInfo.CalcHitInfo(e.Location);
                if(ViewInfo.DownInfo.IsInExposureAreas)
                    Cursor.Hide();
            }
            return false;
        }

        public virtual bool OnMouseUp(MouseEventArgs e) {
            ViewInfo.DownInfo = new HistogrammHitInfo();
            ViewInfo.HoverInfo = ViewInfo.CalcHitInfo(e.Location);
            Cursor.Show();
            UpdateCursor();
            return false;
        }

        public virtual bool OnMouseMove(MouseEventArgs e) {
            if(e.Button == MouseButtons.None) {
                ViewInfo.HoverInfo = ViewInfo.CalcHitInfo(e.Location);
            }
            else {
                if(ViewInfo.DownInfo.IsInExposureAreas) {
                    DragThumb(e);
                }
            }
            UpdateCursor();
            return false;
        }

        float GetValueDelta(float delta, float min, float max) {
            float valueRange = max - min;
            return delta * valueRange / 200.0f;
        }

        public virtual bool OnMouseWheel(MouseEventArgs e) {
            if(!ViewInfo.HoverInfo.IsInExposureAreas)
                return false;
            UpdateValues(ViewInfo.HoverInfo.HitTest, e.Delta / 120);
            return true;
        }

        protected virtual void DragThumb(MouseEventArgs e) {
            float delta = e.Location.X - ViewInfo.DownInfo.LastPoint.X;
            if(delta == 0.0f)
                return;
            UpdateValues(ViewInfo.DownInfo.HitTest, delta);
            Cursor.Position = Owner.PointToScreen(ViewInfo.DownInfo.HitPoint);
        }

        protected virtual void UpdateValues(HistogrammHitTest hitTest, float delta) {
            switch(hitTest) {
                case HistogrammHitTest.Blacks:
                    Owner.BlacksValue += GetValueDelta(delta, Owner.BlacksMinValue, Owner.BlacksMaxValue);
                    break;
                case HistogrammHitTest.Shadows:
                    Owner.ShadowsValue += GetValueDelta(delta, Owner.ShadowsMinValue, Owner.ShadowsMaxValue);
                    break;
                case HistogrammHitTest.Exposure:
                    Owner.ExposureValue += GetValueDelta(delta, Owner.ExposureMinValue, Owner.ExposureMaxValue);
                    break;
                case HistogrammHitTest.Highlights:
                    Owner.HighlightsValue += GetValueDelta(delta, Owner.HighlightsMinValue, Owner.HighlightsMaxValue);
                    break;
                case HistogrammHitTest.Whites:
                    Owner.WhitesValue += GetValueDelta(delta, Owner.WhitesMinValue, Owner.WhitesMaxValue);
                    break;
            }
            Owner.SetValueLabel(hitTest);
        }
        protected Cursor PrevCursor { get; set; }
        protected Cursor DragCursor { get { return Cursors.SizeWE; } }
        protected void UpdateCursor() {
            if(ViewInfo.DownInfo.IsInExposureAreas) {
                //Cursor.Hide();
                return;
            }
            //Cursor.Show();
            if(ViewInfo.HoverInfo.IsInExposureAreas)
                Owner.Cursor = DragCursor;
            else
                Owner.Cursor = PrevCursor;
        }
        
        public virtual void OnMouseEnter(EventArgs e) {
            PrevCursor = Owner.Cursor;
            Owner.SaveLabels();
        }

        public virtual bool OnMouseLeave(EventArgs e) {
            ViewInfo.HoverInfo = new HistogrammHitInfo();
            Owner.Cursor = PrevCursor;
            Owner.RestoreLabels();
            return false;
        }

        public virtual void OnDoubleClick(EventArgs e) {
        }
    }

    public class HistogrammLabelInfoCollection : Collection<StringInfo> { }
    public class HistogrammLabelCollection : Collection<string> {
        public HistogrammLabelCollection(HistogrammControl owner) {
            Owner = owner;
        }
        public HistogrammControl Owner { get; private set; }
        public int UpdateCount { get; private set; }

        public void BeginUpdate() {
            UpdateCount++;
        }
        public void EndUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
            if(UpdateCount == 0)
                Owner.OnLabelsChanged();
        }
        public void CancelUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
        }
        protected override void InsertItem(int index, string item) {
            base.InsertItem(index, item);
            OnCollectionChanged();
        }

        private void OnCollectionChanged() {
            if(UpdateCount > 0)
                return;
            Owner.OnLabelsChanged();
        }

        protected override void RemoveItem(int index) {
            base.RemoveItem(index);
            OnCollectionChanged();
        }
        protected override void SetItem(int index, string item) {
            base.SetItem(index, item);
            OnCollectionChanged();
        }
    }

    public enum HistogrammChannelPreviewMode { Red, Green, Blue, All }
    public enum HistogrammChartPreviewMode { Line, Area }

    public enum HistogrammHitTest { None, Blacks, Shadows, Exposure, Highlights, Whites, ContextButtons }
    public enum HistogrammValue { BlacksMin, BlacksMax, Blacks, ShadowsMin, ShadowsMax, Shadows, ExposureMin, ExposureMax, Exposure, HighlightsMin, HighlightsMax, Highlights, WhitesMin, WhitesMax, Whites }
    public class HistogrammHitInfo {
        public Point HitPoint { get; set; }
        public Point LastPoint { get; set; }
        public HistogrammHitTest HitTest { get; set; }
        public bool IsInExposureAreas {
            get { return HitTest != HistogrammHitTest.None && HitTest != HistogrammHitTest.ContextButtons; } }
    }

    public delegate void HistogrammValueChangedEventHandler(object sender, HistogrammValueChangedEventArgs e);
    public class HistogrammValueChangedEventArgs : EventArgs {
        public HistogrammValueChangedEventArgs(HistogrammValue valueType, float value) {
            ValueType = valueType;
            Value = value;
        }
        public HistogrammValue ValueType { get; private set; }
        public float Value { get; private set; }
    }
}
