using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace PhotoAssistant.Core.Model {
    public enum ItemPosition {
        TopStretch,
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeftCenter,
        MiddleLeftStretch,
        MiddleRightCenter,
        MiddleRightStretch,
        BottomStretch,
        BottomLeft,
        BottomCenter,
        BottomRight
    }
    public enum WatermarkLayout {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        FillPhoto
    }
    public enum WatermarkImageToTextAlign {
        Left,
        Top,
        Right,
        Bottom
    }
    public class WatermarkParameters : DependencyObject {
        [XtraSerializableProperty]
        public bool ShowWatermark {
            get => (bool)GetValue(ShowWatermarkProperty);
            set => SetValue(ShowWatermarkProperty, value);
        }
        public static readonly DependencyProperty ShowWatermarkProperty =
            DependencyProperty.Register("ShowWatermark", typeof(bool), typeof(WatermarkParameters), new PropertyMetadata(false));
        [XtraSerializableProperty]
        public string Text {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(WatermarkParameters), new PropertyMetadata(string.Empty));
        [XtraSerializableProperty]
        public string ImageUri {
            get => (string)GetValue(ImageUriProperty);
            set => SetValue(ImageUriProperty, value);
        }
        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register("ImageUri", typeof(string), typeof(WatermarkParameters), new PropertyMetadata(string.Empty, (d, e) => ((WatermarkParameters)d).OnImageUriChanged(e)));
        void OnImageUriChanged(DependencyPropertyChangedEventArgs e) {
            if(!File.Exists(ImageUri)) {
                return;
            }

            Image = new BitmapImage(new Uri(ImageUri));
        }
        [XtraSerializableProperty]
        public WatermarkLayout Layout {
            get => (WatermarkLayout)GetValue(LayoutProperty);
            set => SetValue(LayoutProperty, value);
        }
        public static readonly DependencyProperty LayoutProperty =
            DependencyProperty.Register("Layout", typeof(WatermarkLayout), typeof(WatermarkParameters), new PropertyMetadata(WatermarkLayout.BottomRight));
        [XtraSerializableProperty]
        public WatermarkImageToTextAlign ImageToTextAlignment {
            get => (WatermarkImageToTextAlign)GetValue(ImageToTextAlignmentProperty);
            set => SetValue(ImageToTextAlignmentProperty, value);
        }
        public static readonly DependencyProperty ImageToTextAlignmentProperty =
            DependencyProperty.Register("ImageToTextAlignment", typeof(WatermarkImageToTextAlign), typeof(WatermarkParameters), new PropertyMetadata(WatermarkImageToTextAlign.Left));
        [XtraSerializableProperty]
        public double RotateAngle {
            get => (double)GetValue(RotateAngleProperty);
            set => SetValue(RotateAngleProperty, value);
        }
        public static readonly DependencyProperty RotateAngleProperty =
            DependencyProperty.Register("RotateAngle", typeof(double), typeof(WatermarkParameters), new PropertyMetadata(0.0));
        [XtraSerializableProperty]
        public double Opacity {
            get => (double)GetValue(OpacityProperty);
            set => SetValue(OpacityProperty, value);
        }
        public static readonly DependencyProperty OpacityProperty =
            DependencyProperty.Register("Opacity", typeof(double), typeof(WatermarkParameters), new PropertyMetadata(1.0));
        [XtraSerializableProperty]
        public double ImageScale {
            get => (double)GetValue(ImageScaleProperty);
            set => SetValue(ImageScaleProperty, value);
        }
        public static readonly DependencyProperty ImageScaleProperty =
            DependencyProperty.Register("ImageScale", typeof(double), typeof(WatermarkParameters), new PropertyMetadata(1.0));
        [XtraSerializableProperty]
        public Color FontColor {
            get => (Color)GetValue(FontColorProperty);
            set => SetValue(FontColorProperty, value);
        }
        public static readonly DependencyProperty FontColorProperty =
            DependencyProperty.Register("FontColor", typeof(Color), typeof(WatermarkParameters), new PropertyMetadata(Colors.White));
        [XtraSerializableProperty]
        public double FontSize {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(WatermarkParameters), new PropertyMetadata(36.0));
        [XtraSerializableProperty]
        public double WatermarkIndent {
            get => (double)GetValue(WatermarkIndentProperty);
            set => SetValue(WatermarkIndentProperty, value);
        }
        public static readonly DependencyProperty WatermarkIndentProperty =
            DependencyProperty.Register("WatermarkIndent", typeof(double), typeof(WatermarkParameters), new PropertyMetadata(0.0, new PropertyChangedCallback((d, e) => ((WatermarkParameters)d).UpdateWatermarkMargins(e))));
        void UpdateWatermarkMargins(DependencyPropertyChangedEventArgs e) => WatermarkMargins = new Thickness(WatermarkIndent);
        public Thickness WatermarkMargins {
            get => (Thickness)GetValue(WatermarkMarginsProperty);
            set => SetValue(WatermarkMarginsProperty, value);
        }
        public static readonly DependencyProperty WatermarkMarginsProperty =
            DependencyProperty.Register("WatermarkMargins", typeof(Thickness), typeof(WatermarkParameters), new PropertyMetadata(new Thickness()));
        [XtraSerializableProperty]
        public string FontFamilyName {
            get => FontFamily.Source;
            set => FontFamily = new FontFamily(value);
        }
        public FontFamily FontFamily {
            get => (FontFamily)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }
        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(WatermarkParameters), new PropertyMetadata(new FontFamily("Segoe UI")));
        [XtraSerializableProperty]
        public string FontStyleName {
            get => FontStyle.ToString();
            set => FontStyle = FontStyleName2Style(value);
        }
        System.Windows.FontStyle FontStyleName2Style(string value) {
            if(value == "Italic") {
                return FontStyles.Italic;
            }

            if(value == "Oblique") {
                return FontStyles.Oblique;
            }

            if(value == "Normal") {
                return FontStyles.Normal;
            }

            return FontStyles.Normal;
        }
        public FontStyle FontStyle {
            get => (FontStyle)GetValue(FontStyleProperty);
            set => SetValue(FontStyleProperty, value);
        }
        public static readonly DependencyProperty FontStyleProperty =
            DependencyProperty.Register("FontStyle", typeof(FontStyle), typeof(WatermarkParameters), new PropertyMetadata(FontStyles.Normal));
        [XtraSerializableProperty]
        public string FontWeightName {
            get => FontWeight.ToString();
            set => FontWeight = FontWeightName2FontWeight(value);
        }
        System.Windows.FontWeight FontWeightName2FontWeight(string value) {
            if(value == "Black") {
                return FontWeights.Black;
            }

            if(value == "Bold") {
                return FontWeights.Bold;
            }

            if(value == "DemiBold") {
                return FontWeights.DemiBold;
            }

            if(value == "ExtraBlack") {
                return FontWeights.ExtraBlack;
            }

            if(value == "ExtraBold") {
                return FontWeights.ExtraBold;
            }

            if(value == "ExtraLight") {
                return FontWeights.ExtraLight;
            }

            if(value == "Heavy") {
                return FontWeights.Heavy;
            }

            if(value == "Light") {
                return FontWeights.Light;
            }

            if(value == "Medium") {
                return FontWeights.Medium;
            }

            if(value == "Normal") {
                return FontWeights.Normal;
            }

            if(value == "Regular") {
                return FontWeights.Regular;
            }

            if(value == "SemiBold") {
                return FontWeights.SemiBold;
            }

            if(value == "Thin") {
                return FontWeights.Thin;
            }

            if(value == "UltraBlack") {
                return FontWeights.UltraBlack;
            }

            if(value == "UltraBold") {
                return FontWeights.UltraBold;
            }

            if(value == "UltraLight") {
                return FontWeights.UltraLight;
            }

            return FontWeights.Normal;
        }
        public FontWeight FontWeight {
            get => (FontWeight)GetValue(FontWeightProperty);
            set => SetValue(FontWeightProperty, value);
        }
        public static readonly DependencyProperty FontWeightProperty =
            DependencyProperty.Register("FontWeight", typeof(FontWeight), typeof(WatermarkParameters), new PropertyMetadata(FontWeights.Normal));
        public ImageSource Image {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(WatermarkParameters), new PropertyMetadata(null));
        public WatermarkParameters Clone() => (WatermarkParameters)MemberwiseClone();
        public void Assign(WatermarkParameters pars) {
            FontColor = pars.FontColor;
            FontFamily = pars.FontFamily;
            FontFamilyName = pars.FontFamilyName;
            FontSize = pars.FontSize;
            FontStyle = pars.FontStyle;
            FontStyleName = pars.FontStyleName;
            FontWeight = pars.FontWeight;
            FontWeightName = pars.FontWeightName;
            Image = pars.Image;
            ImageToTextAlignment = pars.ImageToTextAlignment;
            ImageUri = pars.ImageUri;
            Layout = pars.Layout;
            Opacity = pars.Opacity;
            RotateAngle = pars.RotateAngle;
            ShowWatermark = pars.ShowWatermark;
            Text = pars.Text;
            WatermarkIndent = pars.WatermarkIndent;
            ImageScale = pars.ImageScale;
        }
    }
}
