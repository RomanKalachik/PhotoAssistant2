using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoAssistant.Controls.Wpf {
    public class WatermarkPreviewControl : Control {

        static WatermarkPreviewControl() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkPreviewControl), new FrameworkPropertyMetadata(typeof(WatermarkPreviewControl)));
        }

        public Uri ImageSource {
            get { return (Uri)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(Uri), typeof(WatermarkPreviewControl), new PropertyMetadata(null, (d, e) => ((WatermarkPreviewControl)d).OnImageSourceChanged(e)));

        public ImageSource Image {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(WatermarkPreviewControl), new PropertyMetadata(null, (d, e) => ((WatermarkPreviewControl)d).OnImageChanged(e)));

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
        }

        private void OnImageChanged(DependencyPropertyChangedEventArgs e) {
            ImageWidth = ((BitmapImage)Image).PixelWidth;
            ImageHeight = ((BitmapImage)Image).PixelHeight;
        }

        private void OnImageSourceChanged(DependencyPropertyChangedEventArgs e) {
            Uri uri = new Uri(ImageSource.AbsoluteUri);
            if(File.Exists(uri.LocalPath))
                Image = new BitmapImage(ImageSource);
            else
                Image = null;
        }

        public WatermarkParameters WatermarkParameters {
            get { return (WatermarkParameters)GetValue(WatermarkParametersProperty); }
            set { SetValue(WatermarkParametersProperty, value); }
        }

        public static readonly DependencyProperty WatermarkParametersProperty =
            DependencyProperty.Register("WatermarkParameters", typeof(WatermarkParameters), typeof(WatermarkPreviewControl), new PropertyMetadata(null));

        public double ImageWidth {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(WatermarkPreviewControl), new PropertyMetadata(0.0));

        public double ImageHeight {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(WatermarkPreviewControl), new PropertyMetadata(0.0));

        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);
        }
    }
}
