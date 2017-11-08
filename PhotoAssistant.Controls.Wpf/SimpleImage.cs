using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace PhotoAssistant.Controls.Wpf {
    public class SimpleImage : Control {
        static SimpleImage() => DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleImage), new FrameworkPropertyMetadata(typeof(SimpleImage)));
        public SimpleImage() => ClipToBounds = true;
        Control LoadingIndicator {
            get; set;
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            LoadingIndicator = (Control)GetTemplateChild("PART_LoadingIndicator");
        }
        public PageInfo PageInfo {
            get => (PageInfo)GetValue(PageInfoProperty);
            set => SetValue(PageInfoProperty, value);
        }
        public static readonly DependencyProperty PageInfoProperty =
            DependencyProperty.Register("PageInfo", typeof(PageInfo), typeof(SimpleImage), new PropertyMetadata(null));
        public PhotoInfo PhotoInfo {
            get => (PhotoInfo)GetValue(PhotoInfoProperty);
            set => SetValue(PhotoInfoProperty, value);
        }
        public static readonly DependencyProperty PhotoInfoProperty =
            DependencyProperty.Register("PhotoInfo", typeof(PhotoInfo), typeof(SimpleImage), new PropertyMetadata(null));
        public PhotoLayoutTemplate PhotoLayout {
            get => (PhotoLayoutTemplate)GetValue(PhotoLayoutProperty);
            set => SetValue(PhotoLayoutProperty, value);
        }
        public static readonly DependencyProperty PhotoLayoutProperty =
            DependencyProperty.Register("PhotoLayout", typeof(PhotoLayoutTemplate), typeof(SimpleImage), new PropertyMetadata(null));
        public bool RotateToFit {
            get => (bool)GetValue(RotateToFitProperty);
            set => SetValue(RotateToFitProperty, value);
        }
        public static readonly DependencyProperty RotateToFitProperty =
            DependencyProperty.Register("RotateToFit", typeof(bool), typeof(SimpleImage), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
        public Stretch Stretch {
            get => (Stretch)GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof(Stretch), typeof(SimpleImage), new FrameworkPropertyMetadata(Stretch.Uniform, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
        public ImageSource Source {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(SimpleImage), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((SimpleImage)d).OnSourceChanged(e)));
        void OnSourceChanged(DependencyPropertyChangedEventArgs e) {
        }
        protected override void OnRender(DrawingContext drawingContext) {
            if(Source != null) {
                Rect imageRect = GetDisplayRect(RenderSize);

                if(ShouldRotate(RenderSize)) {
                    drawingContext.PushTransform(new RotateTransform() { CenterX = RenderSize.Width / 2, CenterY = RenderSize.Height / 2, Angle = 90 });
                }

                drawingContext.DrawImage(Source, imageRect);
                if(ShouldRotate(RenderSize)) {
                    drawingContext.Pop();
                }
            }
        }
        protected Size ImageSize {
            get {
                if(Source == null) {
                    return Size.Empty;
                }

                BitmapImage img = Source as BitmapImage;
                if(img != null) {
                    return new Size(img.PixelWidth, img.PixelHeight);
                }

                return new Size(Source.Width, Source.Height);
            }
        }
        bool ShouldRotate(Size screenSize) {
            if(!RotateToFit) {
                return false;
            }

            Orientation screen = GetOrientation(screenSize);
            Orientation image = GetOrientation(ImageSize);
            return screen != image;
        }
        Rect GetDisplayRect(Size screen) {
            Size screenOriginal = screen;
            if(ShouldRotate(screen)) {
                screen = new Size(screen.Height, screen.Width);
            }

            double kx = screen.Width / ImageSize.Width;
            double ky = screen.Height / ImageSize.Height;

            double k = 1.0;
            if(Stretch == System.Windows.Media.Stretch.Uniform) {
                k = Math.Min(kx, ky);
            } else {
                k = Math.Max(kx, ky);
            }

            Size imageScreenSize = new Size(ImageSize.Width * k, ImageSize.Height * k);
            return new Rect((screenOriginal.Width - imageScreenSize.Width) / 2, (screenOriginal.Height - imageScreenSize.Height) / 2, imageScreenSize.Width, imageScreenSize.Height);
        }
        Rect GetMeasureRect(Size screen, bool allowRotate) {
            if(ImageSize.IsEmpty) {
                return new Rect();
            }

            if(Stretch == System.Windows.Media.Stretch.UniformToFill) {
                return new Rect(0, 0, screen.Width, screen.Height);
            }

            Size imageSize = ShouldRotate(screen) && allowRotate ? new Size(ImageSize.Height, ImageSize.Width) : ImageSize;

            double kx = screen.Width / imageSize.Width;
            double ky = screen.Height / imageSize.Height;
            double k = Math.Min(kx, ky);
            Size imageScreenSize = new Size(imageSize.Width * k, imageSize.Height * k);
            return new Rect((screen.Width - imageScreenSize.Width) / 2, (screen.Height - imageScreenSize.Height) / 2, imageScreenSize.Width, imageScreenSize.Height);
        }
        Orientation GetOrientation(Size size) => size.Width > size.Height ? Orientation.Horizontal : Orientation.Vertical;
        Size dpi = new Size(0, 0);
        public Size Dpi {
            get {
                PresentationSource source = PresentationSource.FromVisual(this);
                if(source != null) {
                    dpi = new Size(96.0 * source.CompositionTarget.TransformToDevice.M11, 96.0 * source.CompositionTarget.TransformToDevice.M22);
                }

                return dpi;
            }
        }
        protected Size GetPictureActualSize(Size size) {
            size.Width *= Dpi.Width * PageInfo.Zoom;
            size.Height *= Dpi.Height * PageInfo.Zoom;
            return size;
        }
        protected bool IsFixedSizePhoto => !double.IsNaN(PhotoLayout.Width);
        protected override Size MeasureOverride(Size constraint) {
            Size size = Size.Empty;
            if(IsFixedSizePhoto) {
                constraint = GetPictureActualSize(new Size(PhotoLayout.Width, PhotoLayout.Height));
                size = GetMeasureRect(constraint, RotateToFit).Size;
                base.MeasureOverride(size);
                return constraint;
            }
            size = GetMeasureRect(constraint, RotateToFit).Size;
            base.MeasureOverride(size);
            return size;
        }
        protected override Size ArrangeOverride(Size arrangeBounds) {
            Size size = Size.Empty;
            if(IsFixedSizePhoto) {
                arrangeBounds = GetPictureActualSize(new Size(PhotoLayout.Width, PhotoLayout.Height));
                size = base.ArrangeOverride(GetMeasureRect(arrangeBounds, RotateToFit).Size);
                return arrangeBounds;
            }
            size = base.ArrangeOverride(GetMeasureRect(arrangeBounds, RotateToFit).Size);
            return size;
        }
        protected override void OnMouseDown(MouseButtonEventArgs e) {
            base.OnMouseDown(e);
            PhotoLayout.PageLayout.PrintControl.SelectedPhotoLayout = PhotoLayout.PageLayout.PhotoLayouts.IndexOf(PhotoLayout);
        }
        protected override void OnMouseUp(MouseButtonEventArgs e) {
            base.OnMouseUp(e);
            DmFile file = WpfDragDropHelper.Default.DragObject as DmFile;
            if(file == null) {
                return;
            }

            PhotoInfo.File = file;
            if(LoadingIndicator != null) {
                LoadingIndicator.Visibility = System.Windows.Visibility.Visible;
            }

            BackgroundImageLoader.Default.LoadFileImageInBackground(file, (d, s) => {
                PhotoInfo.ImageSource = (ImageSource)file.ImageSource;
                if(LoadingIndicator != null) {
        LoadingIndicator.Visibility = System.Windows.Visibility.Collapsed;
    }
});
            WpfDragDropHelper.Default.DragObject = null;
            UpdateLayout();
        }
        public bool Selected {
            get => (bool)GetValue(SelectedProperty);
            set => SetValue(SelectedProperty, value);
        }
        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.Register("Selected", typeof(bool), typeof(SimpleImage), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
    }
}
