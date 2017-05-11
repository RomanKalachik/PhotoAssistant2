using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.Controls.Wpf {
    public class WatermarkControl : Control {
        static WatermarkControl() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkControl), new FrameworkPropertyMetadata(typeof(WatermarkControl)));
        }

        public WatermarkControl() {
            OnImageAlignmentChanged(new DependencyPropertyChangedEventArgs());
        }

        public WatermarkParameters Params {
            get { return (WatermarkParameters)GetValue(ParamsProperty); }
            set { SetValue(ParamsProperty, value); }
        }

        public static readonly DependencyProperty ParamsProperty =
            DependencyProperty.Register("Params", typeof(WatermarkParameters), typeof(WatermarkControl), new PropertyMetadata(null, (d, e) => ((WatermarkControl)d).OnParamsChanged(e)));

        public ImageSource Image {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(WatermarkControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((WatermarkControl)d).OnParamsChanged(e)));

        private void OnParamsChanged(DependencyPropertyChangedEventArgs e) {
            OnImageAlignmentChanged(new DependencyPropertyChangedEventArgs());
            OnTileModeChanged(e);
            OnParamsChangedCore();
        }

        private void OnParamsChangedCore() {
        }

        public bool ShowWatermark {
            get { return (bool)GetValue(ShowWatermarkProperty); }
            set { SetValue(ShowWatermarkProperty, value); }
        }

        public static readonly DependencyProperty ShowWatermarkProperty =
            DependencyProperty.Register("ShowWatermark", typeof(bool), typeof(WatermarkControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((WatermarkControl)d).OnParamsChanged(e)));

        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(WatermarkControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((WatermarkControl)d).OnParamsChanged(e)));

        public string ImageUri {
            get { return (string)GetValue(ImageUriProperty); }
            set { SetValue(ImageUriProperty, value); }
        }

        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register("ImageUri", typeof(string), typeof(WatermarkControl), new FrameworkPropertyMetadata("", (d, e) => ((WatermarkControl)d).OnImageUriChanged(e)));

        private void OnImageUriChanged(DependencyPropertyChangedEventArgs e) {
            Image = new BitmapImage(new Uri(ImageUri));
        }

        public WatermarkLayout Layout {
            get { return (WatermarkLayout)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }

        public static readonly DependencyProperty LayoutProperty =
            DependencyProperty.Register("Layout", typeof(WatermarkLayout), typeof(WatermarkControl), new FrameworkPropertyMetadata(WatermarkLayout.BottomRight, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((WatermarkControl)d).OnLayoutChanged(e)));

        private void OnLayoutChanged(DependencyPropertyChangedEventArgs e) {
            switch(Layout) {
                case WatermarkLayout.TopLeft:
                case WatermarkLayout.TopRight:
                case WatermarkLayout.TopCenter:
                    AlignmentY = System.Windows.Media.AlignmentY.Top;
                    break;
                case WatermarkLayout.BottomLeft:
                case WatermarkLayout.BottomRight:
                case WatermarkLayout.BottomCenter:
                    AlignmentY = System.Windows.Media.AlignmentY.Bottom;
                    break;
                default:
                    AlignmentY = System.Windows.Media.AlignmentY.Center;
                    break;
            }
            switch(Layout) {
                case WatermarkLayout.TopLeft:
                case WatermarkLayout.MiddleLeft:
                case WatermarkLayout.BottomLeft:
                    AlignmentX = System.Windows.Media.AlignmentX.Left;
                    break;
                case WatermarkLayout.TopRight:
                case WatermarkLayout.MiddleRight:
                case WatermarkLayout.BottomRight:
                    AlignmentX = System.Windows.Media.AlignmentX.Right;
                    break;
                default:
                    AlignmentX = System.Windows.Media.AlignmentX.Center;
                    break;
            }
            if(Layout == WatermarkLayout.FillPhoto)
                TileMode = System.Windows.Media.TileMode.Tile;
            else
                TileMode = System.Windows.Media.TileMode.None;
            OnParamsChanged(e);
        }

        public AlignmentX AlignmentX {
            get { return (AlignmentX)GetValue(AlignmentXProperty); }
            set { SetValue(AlignmentXProperty, value); }
        }

        public static readonly DependencyProperty AlignmentXProperty =
            DependencyProperty.Register("AlignmentX", typeof(AlignmentX), typeof(WatermarkControl), new PropertyMetadata(AlignmentX.Right));

        public AlignmentY AlignmentY {
            get { return (AlignmentY)GetValue(AlignmentYProperty); }
            set { SetValue(AlignmentYProperty, value); }
        }

        public static readonly DependencyProperty AlignmentYProperty =
            DependencyProperty.Register("AlignmentY", typeof(AlignmentY), typeof(WatermarkControl), new PropertyMetadata(AlignmentY.Bottom));

        public WatermarkImageToTextAlign ImageToTextAlignment {
            get { return (WatermarkImageToTextAlign)GetValue(ImageToTextAlignmentProperty); }
            set { SetValue(ImageToTextAlignmentProperty, value); }
        }

        public static readonly DependencyProperty ImageToTextAlignmentProperty =
            DependencyProperty.Register("ImageToTextAlignment", typeof(WatermarkImageToTextAlign), typeof(WatermarkControl), new FrameworkPropertyMetadata(WatermarkImageToTextAlign.Left, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((WatermarkControl)d).OnImageAlignmentChanged(e)));

        public DataTemplate ActualContentTemplate {
            get { return (DataTemplate)GetValue(ActualContentTemplateProperty); }
            set { SetValue(ActualContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty ActualContentTemplateProperty =
            DependencyProperty.Register("ActualContentTemplate", typeof(DataTemplate), typeof(WatermarkControl), new PropertyMetadata(null));

        private void OnImageAlignmentChanged(DependencyPropertyChangedEventArgs e) {
            if(ImageToTextAlignment == WatermarkImageToTextAlign.Top)
                ActualContentTemplate = TopImageTemplate;
            else if(ImageToTextAlignment == WatermarkImageToTextAlign.Bottom)
                ActualContentTemplate = BottomImageTemplate;
            else if(ImageToTextAlignment == WatermarkImageToTextAlign.Left)
                ActualContentTemplate = LeftImageTemplate;
            else
                ActualContentTemplate = RightImageTemplate;
            OnParamsChangedCore();
        }

        public DataTemplate TopImageTemplate {
            get { return (DataTemplate)GetValue(TopImageTemplateProperty); }
            set { SetValue(TopImageTemplateProperty, value); }
        }

        public static readonly DependencyProperty TopImageTemplateProperty =
            DependencyProperty.Register("TopImageTemplate", typeof(DataTemplate), typeof(WatermarkControl), new PropertyMetadata(null, (d, e) => ((WatermarkControl)d).OnParamsChanged(e)));

        public DataTemplate BottomImageTemplate {
            get { return (DataTemplate)GetValue(BottomImageTemplateProperty); }
            set { SetValue(BottomImageTemplateProperty, value); }
        }

        public static readonly DependencyProperty BottomImageTemplateProperty =
            DependencyProperty.Register("BottomImageTemplate", typeof(DataTemplate), typeof(WatermarkControl), new PropertyMetadata(null, (d, e) => ((WatermarkControl)d).OnParamsChanged(e)));

        public DataTemplate LeftImageTemplate {
            get { return (DataTemplate)GetValue(LeftImageTemplateProperty); }
            set { SetValue(LeftImageTemplateProperty, value); }
        }

        public static readonly DependencyProperty LeftImageTemplateProperty =
            DependencyProperty.Register("LeftImageTemplate", typeof(DataTemplate), typeof(WatermarkControl), new PropertyMetadata(null, (d, e) => ((WatermarkControl)d).OnParamsChanged(e)));

        public DataTemplate RightImageTemplate {
            get { return (DataTemplate)GetValue(RightImageTemplateProperty); }
            set { SetValue(RightImageTemplateProperty, value); }
        }

        public static readonly DependencyProperty RightImageTemplateProperty =
            DependencyProperty.Register("RightImageTemplate", typeof(DataTemplate), typeof(WatermarkControl), new PropertyMetadata(null, (d, e) => ((WatermarkControl)d).OnParamsChanged(e)));

        public double RotateAngle {
            get { return (double)GetValue(RotateAngleProperty); }
            set { SetValue(RotateAngleProperty, value); }
        }

        public static readonly DependencyProperty RotateAngleProperty =
            DependencyProperty.Register("RotateAngle", typeof(double), typeof(WatermarkControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((WatermarkControl)d).OnParamsChanged(e)));

        public Color FontColor {
            get { return (Color)GetValue(FontColorProperty); }
            set { SetValue(FontColorProperty, value); }
        }

        public static readonly DependencyProperty FontColorProperty =
            DependencyProperty.Register("FontColor", typeof(Color), typeof(WatermarkControl), new FrameworkPropertyMetadata(Colors.White, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((WatermarkControl)d).OnParamsChanged(e)));

        public double WatermarkIndent {
            get { return (double)GetValue(WatermarkIndentProperty); }
            set { SetValue(WatermarkIndentProperty, value); }
        }

        public static readonly DependencyProperty WatermarkIndentProperty =
            DependencyProperty.Register("WatermarkIndent", typeof(double), typeof(WatermarkControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((WatermarkControl)d).OnParamsChanged(e)));

        public double WatermarkFontSize {
            get { return (double)GetValue(WatermarkFontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly DependencyProperty WatermarkFontSizeProperty =
            DependencyProperty.Register("WatermarkFontSize", typeof(double), typeof(WatermarkControl), new PropertyMetadata(0.0, (d, e) => ((WatermarkControl)d).OnWatermarkFontSizeChanged(e)));

        private void OnWatermarkFontSizeChanged(DependencyPropertyChangedEventArgs e) {
            if(ContentControl != null)
                ContentControl.UpdateLayout();
            OnParamsChanged(e);
        }

        public Rect Viewport {
            get { return (Rect)GetValue(ViewportProperty); }
            set { SetValue(ViewportProperty, value); }
        }

        public static readonly DependencyProperty ViewportProperty =
            DependencyProperty.Register("Viewport", typeof(Rect), typeof(WatermarkControl), new PropertyMetadata(new Rect(0, 0, 1, 1)));

        public TileMode TileMode {
            get { return (TileMode)GetValue(TileModeProperty); }
            set { SetValue(TileModeProperty, value); }
        }

        public static readonly DependencyProperty TileModeProperty =
            DependencyProperty.Register("TileMode", typeof(TileMode), typeof(WatermarkControl), new PropertyMetadata(TileMode.None, (d, e) => ((WatermarkControl)d).OnTileModeChanged(e)));

        ContentControl ContentControl { get; set; }
        VisualBrush Brush { get; set; }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            ContentControl = (ContentControl)GetTemplateChild("PART_ContentControl");
            Brush = (VisualBrush)GetTemplateChild("PART_Brush");
            ContentControl.UpdateLayout();
            OnTileModeChanged(new DependencyPropertyChangedEventArgs());
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            base.OnRenderSizeChanged(sizeInfo);
            OnTileModeChanged(new DependencyPropertyChangedEventArgs());
        }

        private void OnTileModeChanged(DependencyPropertyChangedEventArgs e) {
            if(TileMode == System.Windows.Media.TileMode.Tile) {
                if(ContentControl == null)
                    return;
                if(!ContentControl.IsLoaded) {
                    ContentControl.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    ContentControl.Arrange(new Rect(0, 0, ContentControl.DesiredSize.Width, ContentControl.DesiredSize.Height));
                }
                Viewport = new Rect(0, 0, ContentControl.DesiredSize.Width / ActualWidth, ContentControl.DesiredSize.Height / ActualHeight);
                Padding = new Thickness(0);
            } else {
                double wi = ActualWidth == 0.0 ? 0.0 : WatermarkIndent / ActualWidth;
                double hi = ActualHeight == 0.0 ? 0.0 : WatermarkIndent / ActualHeight;
                Viewport = new Rect(wi, hi, Math.Max(0, 1 - wi * 2), Math.Max(0, 1 - hi * 2));
            }
        }
        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);
        }
    }
}
