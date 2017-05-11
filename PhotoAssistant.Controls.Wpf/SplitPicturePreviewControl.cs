using PhotoAssistant.Core.Model;
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

namespace PhotoAssistant.Controls.Wpf {
    public class SplitPicturePreviewControl : PicturePreviewControl {
        
        public SplitPicturePreviewType PreviewMode {
            get { return (SplitPicturePreviewType)GetValue(PreviewModeProperty); }
            set { SetValue(PreviewModeProperty, value); }
        }
        
        public static readonly DependencyProperty PreviewModeProperty =
            DependencyProperty.Register("PreviewMode", typeof(SplitPicturePreviewType), typeof(SplitPicturePreviewControl), new FrameworkPropertyMetadata(SplitPicturePreviewType.Single, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((SplitPicturePreviewControl)d).OnPreviewModeChanged(e)));

        protected override ImageInfo GetImageInfo(Point mouseLocation) {
            if(ImageInfo.ClipRect.Contains(mouseLocation))
                return ImageInfo;
            if(ImageInfo2 != null && ImageInfo2.ClipRect.Contains(mouseLocation))
                return ImageInfo2;
            if(ImageInfo3 != null && ImageInfo3.ClipRect.Contains(mouseLocation))
                return ImageInfo3;
            return ImageInfo;
        }

        protected override void OnZoomChanged(DependencyPropertyChangedEventArgs e) {
            base.OnZoomChanged(e);
            OnZoomChangedCore(e, ImageInfo2);
            OnZoomChangedCore(e, ImageInfo3);
        }
        protected override void OnAnimationProgressCore() {
            base.OnAnimationProgressCore();
            OnAnimationProgress(ImageInfo2);
            OnAnimationProgress(ImageInfo3);
        }
        protected override void SetZoomAnimatedCore(double newZoom, Point pt) {
            Rect screen = GetScreenByPoint(pt);
            Point delta = new Point(pt.X - screen.X, pt.Y - screen.Y);

            SetZoomAnimatedCore(ImageInfo, newZoom, delta);
            SetZoomAnimatedCore(ImageInfo2, newZoom, delta);
            SetZoomAnimatedCore(ImageInfo3, newZoom, delta);

            if(ImageInfo.Info.ScrollPosition.X != ImageInfo2.Info.ScrollPosition.X ||
                ImageInfo.Info.ScrollPosition.Y != ImageInfo2.Info.ScrollPosition.Y) {

            }
        }

        protected override void OnScroll(Point scrollDelta) {
            base.OnScroll(scrollDelta);
            OnScroll(ImageInfo2, scrollDelta);
            OnScroll(ImageInfo3, scrollDelta);
        }

        private Rect GetScreenByPoint(Point pt) {
            if(FirstScreen.Contains(pt))
                return FirstScreen;
            if(SecondScreen.Contains(pt))
                return SecondScreen;
            if(ThirdScreen.Contains(pt))
                return ThirdScreen;
            return FirstScreen;
        }

        protected override void PrepareForAnimatedZoom() {
            base.PrepareForAnimatedZoom();
            PrepareForAnimatedZoom(ImageInfo2);
            PrepareForAnimatedZoom(ImageInfo3);
        }

        public DmFile CandidateFile2 {
            get { return (DmFile)GetValue(CandidateFile2Property); }
            set { SetValue(CandidateFile2Property, value); }
        }

        public static readonly DependencyProperty CandidateFile2Property =
            DependencyProperty.Register("CandidateFile2", typeof(DmFile), typeof(SplitPicturePreviewControl), new PropertyMetadata(null, (d,e) => ((SplitPicturePreviewControl)d).OnCandidate2Changed(e)));
        
        public DmFile CandidateFile {
            get { return (DmFile)GetValue(CandidateFileProperty); }
            set { SetValue(CandidateFileProperty, value); }
        }

        public static readonly DependencyProperty CandidateFileProperty =
            DependencyProperty.Register("CandidateFile", typeof(DmFile), typeof(SplitPicturePreviewControl), new PropertyMetadata(null, (d, e) => ((SplitPicturePreviewControl)d).OnCandidateChanged(e)));

        protected override void ClearPrevFile(DmFile file) {
            
        }

        protected virtual void OnCandidateChanged(DependencyPropertyChangedEventArgs e) {
            ClearValue(CurrentRotateAngleProperty);
            ClearValue(RotateAngleProperty);
            if(!IsSlideShow && e.OldValue != null && CandidateFile != null) {
                ShowLoadingIndicator();
                BackgroundImageLoader.Default.LoadFileImageInBackground(CandidateFile, (d, s) => {
                    CandidateFile.Worker = null;
                    HideLoadingIndicator();
                    Source2 = (ImageSource)s.Result;
                    OnFileChangedCore(e);
                });
            }
            else {
                Source2 = GetImageSource(CandidateFile);
                OnFileChangedCore(e);
            }
        }

        protected virtual void OnCandidate2Changed(DependencyPropertyChangedEventArgs e) {
            ClearValue(CurrentRotateAngleProperty);
            ClearValue(RotateAngleProperty);
            if(!IsSlideShow && e.OldValue != null && CandidateFile2 != null) {
                ShowLoadingIndicator();
                BackgroundImageLoader.Default.LoadFileImageInBackground(CandidateFile2, (d, s) => {
                    CandidateFile2.Worker = null;
                    HideLoadingIndicator();
                    Source3 = (ImageSource)s.Result;
                    OnFileChangedCore(e);
                });
            }
            else {
                Source3 = GetImageSource(CandidateFile);
                OnFileChangedCore(e);
            }
        }

        public override bool ShouldShowInfoPanels {
            get { return false; }
        }

        public void SetFiles(DmFile current, DmFile candidate1, DmFile candidate2) {
            DmFile prevCurrent = CurrentFile;
            DmFile prevCandidate = CandidateFile;
            DmFile prevCandidate2 = CandidateFile2;

            CurrentFile = current;
            CandidateFile = candidate1;
            CandidateFile2 = candidate2;

            ClearUnusedImageSource(prevCurrent);
            ClearUnusedImageSource(prevCandidate);
            ClearUnusedImageSource(prevCandidate2);
        }

        protected void ClearUnusedImageSource(DmFile file) {
            if(file != null && file != CurrentFile && file != CandidateFile && file != CandidateFile2) {
                if(file.Worker != null) {
                    file.Worker.CancelAsync();
                    file.Worker = null;
                }
                file.ImageSource = null;
            }
        }

        private void OnPreviewModeChanged(DependencyPropertyChangedEventArgs e) {
            if(PreviewMode == SplitPicturePreviewType.Editing) {
                Label1 = "Before";
                Label2 = "After";
                ((SplitImageInfo)ImageInfo).ScreenIndex = 0;
                ((SplitImageInfo)ImageInfo2).ScreenIndex = 1;
                ProcessedSource = GenerateProcessedSource();
            }
            else if(PreviewMode == SplitPicturePreviewType.Two) {
                Label1 = "Current";
                Label2 = "RightCandidate";
                ((SplitImageInfo)ImageInfo).ScreenIndex = 0;
                ((SplitImageInfo)ImageInfo2).ScreenIndex = 1;
            }
            else if(PreviewMode == SplitPicturePreviewType.Three) {
                Label1 = "Left Candidate";
                Label2 = "Current";
                Label3 = "Right Candidate";
                ((SplitImageInfo)ImageInfo).ScreenIndex = 1;
                ((SplitImageInfo)ImageInfo2).ScreenIndex = 0;
                ((SplitImageInfo)ImageInfo3).ScreenIndex = 2;
            }
            ResetInfo();
        }

        public void ResetInfo() {
            ImageInfo.Source = Source;
            if(PreviewMode == SplitPicturePreviewType.Editing)
                ImageInfo2.Source = ProcessedSource;
            else
                ImageInfo2.Source = Source2;
            ImageInfo3.Source = Source3;
            CalcLayout(ImageInfo);
            CalcLayout(ImageInfo2);
            CalcLayout(ImageInfo3);
        }

        protected override void OnZoomAnimationCompleted(object sender, EventArgs e) {
            ImageInfo.Info.Animating = false;
            ImageInfo2.Info.Animating = false;
            ImageInfo3.Info.Animating = false;
            InvalidateVisual();
        }

        public ImageSource ProcessedSource {
            get { return (ImageSource)GetValue(ProcessedSourceProperty); }
            set { SetValue(ProcessedSourceProperty, value); }
        }

        public static readonly DependencyProperty ProcessedSourceProperty =
            DependencyProperty.Register("ProcessedSource", typeof(ImageSource), typeof(SplitPicturePreviewControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((SplitPicturePreviewControl)d).OnProcessedSourceChanged(e)));

        protected virtual void OnProcessedSourceChanged(DependencyPropertyChangedEventArgs e) {
            ImageInfo2.PrevSource = (ImageSource)e.OldValue;
            ImageInfo2.Source = ProcessedSource;
            ChangePicture(ImageInfo2);
        }

        protected override void OnSourceChanged(DependencyPropertyChangedEventArgs e) {
            base.OnSourceChanged(e);
            if(PreviewMode == SplitPicturePreviewType.Editing)
                ProcessedSource = GenerateProcessedSource();
        }

        private double CoerceZoom2(object value) {
            return CoerceZoom((double)value);
        }

        private double CoerceZoom3(object value) {
            return CoerceZoom((double)value);
        }

        private ImageSource GenerateProcessedSource() {
            if(Source == null)
                return null;
            WriteableBitmap img = new WriteableBitmap((BitmapSource)Source);
            return img;
        }

        protected ScrollZoomInfo Info2 { get; set; }
        protected ScrollZoomInfo Info3 { get; set; }
        ScrollZoomInfo PrevInfo2 { get; set; }
        ScrollZoomInfo PrevInfo3 { get; set; }

        protected Size Image2Size {
            get {
                BitmapImage img = Source2 as BitmapImage;
                if(img != null)
                    return new Size(img.PixelWidth, img.PixelHeight);
                return new Size(Source2.Width, Source2.Height);
            }
        }

        protected Size Image3Size {
            get {
                BitmapImage img = Source3 as BitmapImage;
                if(img != null)
                    return new Size(img.PixelWidth, img.PixelHeight);
                return new Size(Source3.Width, Source3.Height);
            }
        }

        protected bool AllowItem2 {
            get { return PreviewMode == SplitPicturePreviewType.Editing || PreviewMode == SplitPicturePreviewType.Two; }
        }
        protected bool AllowItem3 {
            get { return PreviewMode == SplitPicturePreviewType.Three; }
        }
        
        public ImageSource Source2 {
            get { return (ImageSource)GetValue(Source2Property); }
            set { SetValue(Source2Property, value); }
        }

        public static readonly DependencyProperty Source2Property =
            DependencyProperty.Register("Source2", typeof(ImageSource), typeof(SplitPicturePreviewControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((SplitPicturePreviewControl)d).OnSource2Changed(e)));

        protected virtual void OnSource2Changed(DependencyPropertyChangedEventArgs e) {
            ImageInfo2.PrevSource = (ImageSource)e.OldValue;
            ImageInfo2.Source = Source2;
            ChangePicture(ImageInfo2);
        }
        
        public ImageSource Source3 {
            get { return (ImageSource)GetValue(Source3Property); }
            set { SetValue(Source3Property, value); }
        }

        public static readonly DependencyProperty Source3Property =
            DependencyProperty.Register("Source3", typeof(ImageSource), typeof(SplitPicturePreviewControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) => ((SplitPicturePreviewControl)d).OnSource3Changed(e)));

        protected virtual void OnSource3Changed(DependencyPropertyChangedEventArgs e) {
            ImageInfo3.PrevSource = (ImageSource)e.OldValue;
            ImageInfo3.Source = Source3;
            ChangePicture(ImageInfo3);
        }

        public PreviewPictureLayoutMode LayoutMode {
            get { return (PreviewPictureLayoutMode)GetValue(LayoutModeProperty); }
            set { SetValue(LayoutModeProperty, value); }
        }

        public static readonly DependencyProperty LayoutModeProperty =
            DependencyProperty.Register("LayoutwMode", typeof(PreviewPictureLayoutMode), typeof(SplitPicturePreviewControl), new FrameworkPropertyMetadata(PreviewPictureLayoutMode.Single, FrameworkPropertyMetadataOptions.AffectsRender, (d,e) => ((SplitPicturePreviewControl)d).OnLayoutModeChanged(e)));

        protected virtual void OnLayoutModeChanged(DependencyPropertyChangedEventArgs e) {
            ResetInfo();
        }

        public bool IsSwap {
            get { return (bool)GetValue(IsSwapProperty); }
            set { SetValue(IsSwapProperty, value); }
        }

        public static readonly DependencyProperty IsSwapProperty =
            DependencyProperty.Register("IsSwap", typeof(bool), typeof(SplitPicturePreviewControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        
        public override void SetZoomAnimated(double newZoom) {
            SetZoomAnimated(ImageInfo, newZoom);
            SetZoomAnimated(ImageInfo2, newZoom);
            SetZoomAnimated(ImageInfo3, newZoom);
            InternalSetZoom = true;
            Zoom = newZoom;
            InternalSetZoom = false;
            MakeAnimatedZoom();
        }

        protected override void ShowLoadingIndicator() {
        }

        protected override void RenderImageShadow(DrawingContext drawingContext) {
            base.RenderImageShadow(drawingContext);
            if(LayoutMode == PreviewPictureLayoutMode.Horizontal || 
                LayoutMode == PreviewPictureLayoutMode.Vertical) {
                if(ScreenCount >= 2 && ImageInfo2.Source != null)
                    RenderImageShadow(drawingContext, ImageInfo2);
                if(ScreenCount >= 3 && ImageInfo3.Source != null)
                    RenderImageShadow(drawingContext, ImageInfo3);
            }
        }

        protected override void RenderImage(DrawingContext drawingContext) {
            if(ScreenCount == 1) {
                RenderImage(drawingContext, ImageInfo);
            }
            else if(ScreenCount == 2) {
                RenderImage(drawingContext, ImageInfo);
                RenderImage(drawingContext, ImageInfo2);
            }
            else if(ScreenCount == 3) {
                RenderImage(drawingContext, ImageInfo);
                RenderImage(drawingContext, ImageInfo2);
                RenderImage(drawingContext, ImageInfo3);
            }
            DrawGrid(drawingContext);
            DrawLabels(drawingContext);
            DrawSplitter(drawingContext);
        }

        ImageInfo imageInfo2;
        protected internal ImageInfo ImageInfo2 {
            get {
                if(imageInfo2 == null)
                    imageInfo2 = new SplitImageInfo(this) { ScreenIndex = 1 };
                return imageInfo2;
            }
        }

        ImageInfo imageInfo3;
        protected internal ImageInfo ImageInfo3 {
            get {
                if(imageInfo3 == null)
                    imageInfo3 = new SplitImageInfo(this) { ScreenIndex = 2 };
                return imageInfo3;
            }
        }

        protected override ImageInfo CreateImageInfo() {
            return new SplitImageInfo(this) { ScreenIndex = 0 };
        }

        private void DrawVeticalSplitPreviewThree(DrawingContext drawingContext, Rect rect) {
            DrawImages(drawingContext, Source2, Source, Source3, rect, rect, rect);
        }

        private void DrawVerticalPreviewThree(DrawingContext drawingContext, Rect rect) {
            Rect imgFirstScreen = rect;
            Rect imgSecondScreen = new Rect(rect.X, rect.Y + ActualHeight / ScreenCount, rect.Width, rect.Height); 
            Rect imgThirdScreen = new Rect(rect.X, rect.Y + 2 * ActualHeight / ScreenCount, rect.Width, rect.Height);

            DrawImages(drawingContext, Source2, Source, Source3, imgFirstScreen, imgSecondScreen, imgThirdScreen);
        }

        private void DrawHorizontalSplitPreviewThree(DrawingContext drawingContext, Rect rect) {
            DrawImages(drawingContext, Source2, Source, Source3, rect, rect, rect);
        }

        private void DrawHorizontalPreviewThree(DrawingContext drawingContext, Rect rect) {
            Rect imgFirstScreen = rect;
            Rect imgSecondScreen = new Rect(rect.X + ActualWidth / ScreenCount, rect.Y, rect.Width, rect.Height); 
            Rect imgThirdScreen = new Rect(rect.X + 2 * ActualWidth / ScreenCount, rect.Y, rect.Width, rect.Height);

            DrawImages(drawingContext, Source2, Source, Source3, imgFirstScreen, imgSecondScreen, imgThirdScreen);
        }

        protected override bool ShowGridCore {
            get { return ShowGrid && LayoutMode != PreviewPictureLayoutMode.Horizontal && LayoutMode != PreviewPictureLayoutMode.Vertical; }
        }

        public string Label1 {
            get { return (string)GetValue(Label1Property); }
            set { SetValue(Label1Property, value); }
        }

        public static readonly DependencyProperty Label1Property =
            DependencyProperty.Register("Label1", typeof(string), typeof(SplitPicturePreviewControl), new FrameworkPropertyMetadata("Before", FrameworkPropertyMetadataOptions.AffectsRender));

        public string Label2 {
            get { return (string)GetValue(Label2Property); }
            set { SetValue(Label2Property, value); }
        }

        public static readonly DependencyProperty Label2Property =
            DependencyProperty.Register("Label2", typeof(string), typeof(SplitPicturePreviewControl), new FrameworkPropertyMetadata("After", FrameworkPropertyMetadataOptions.AffectsRender));

        public string Label3 {
            get { return (string)GetValue(Label3Property); }
            set { SetValue(Label3Property, value); }
        }

        public static readonly DependencyProperty Label3Property =
            DependencyProperty.Register("Label3", typeof(string), typeof(SplitPicturePreviewControl), new FrameworkPropertyMetadata("Right Candidate", FrameworkPropertyMetadataOptions.AffectsRender));

        private void DrawLabels2(DrawingContext drawingContext) {
            if(ScreenCount != 2)
                return;

            Rect beforeScreenRect = IsSwap ? SecondScreen : FirstScreen;
            Rect afterScreenRect = IsSwap ? FirstScreen : SecondScreen;

            bool isHorizontal = LayoutMode == PreviewPictureLayoutMode.Horizontal || LayoutMode == PreviewPictureLayoutMode.HorizontalSplit;

            HorizontalAlignment bh = HorizontalAlignment.Left;
            HorizontalAlignment ah = HorizontalAlignment.Left;
            VerticalAlignment bv = VerticalAlignment.Top;
            VerticalAlignment av = VerticalAlignment.Top;
            if(IsSwap) {
                if(isHorizontal) {
                    ah = HorizontalAlignment.Right;
                }
                else {
                    bh = ah = HorizontalAlignment.Right;
                    av = VerticalAlignment.Bottom;
                }
            }
            else {
                if(isHorizontal) {
                    bh = HorizontalAlignment.Right;
                }
                else {
                    bh = ah = HorizontalAlignment.Right;
                    bv = VerticalAlignment.Bottom;
                }
            }

            DrawLabel(drawingContext, Label1, beforeScreenRect, bh, bv);
            DrawLabel(drawingContext, Label2, afterScreenRect, ah, av);
        }

        private void DrawLabels3(DrawingContext drawingContext) {
            if(ScreenCount != 3)
                return;

            if(LayoutMode == PreviewPictureLayoutMode.Horizontal || LayoutMode == PreviewPictureLayoutMode.HorizontalSplit) {
                DrawLabel(drawingContext, Label1, FirstScreen, HorizontalAlignment.Right, VerticalAlignment.Top);
                DrawLabel(drawingContext, Label2, SecondScreen, HorizontalAlignment.Center, VerticalAlignment.Top);
                DrawLabel(drawingContext, Label3, ThirdScreen, HorizontalAlignment.Left, VerticalAlignment.Top);
            }
            else {
                DrawLabel(drawingContext, Label1, FirstScreen, HorizontalAlignment.Right, VerticalAlignment.Bottom);
                DrawLabel(drawingContext, Label2, SecondScreen, HorizontalAlignment.Right, VerticalAlignment.Center);
                DrawLabel(drawingContext, Label3, ThirdScreen, HorizontalAlignment.Right, VerticalAlignment.Top);
            }
        }

        private void DrawLabels(DrawingContext drawingContext) {
            if(ScreenCount == 2)
                DrawLabels2(drawingContext);
            else if(ScreenCount == 3)
                DrawLabels3(drawingContext);
        }

        TextBlock textBlock;
        TextBlock TextBlock {
            get {
                if(textBlock == null)
                    textBlock = CreateTextBlock();
                return textBlock;
            }
        }

        private TextBlock CreateTextBlock() {
            return new TextBlock() { FontSize = 16, Foreground = new SolidColorBrush(Color.FromArgb(0x80, 255, 255, 255)) };
        }

        private Size MeasureString(string text) {
            var formattedText = GetFormattedText(text);
            return new Size(formattedText.Width, formattedText.Height);
        }

        FormattedText GetFormattedText(string text) {
            return new FormattedText(
                text,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(TextBlock.FontFamily, TextBlock.FontStyle, TextBlock.FontWeight, TextBlock.FontStretch),
                TextBlock.FontSize,
                TextBlock.Foreground);
        }

        protected double LabelIndent { get { return 20; } }

        protected Thickness LabelPadding { get { return new Thickness(15, 5, 15, 5); } }
        protected double LabelRadius { get { return 3; } }

        private void DrawLabel(DrawingContext drawingContext, string text, Rect screenBounds, HorizontalAlignment halign, VerticalAlignment valign) {
            Size sz = MeasureString(text);
            sz.Width += LabelPadding.Left + LabelPadding.Right;
            sz.Height += LabelPadding.Top + LabelPadding.Bottom;

            double x = 0, y = 0;
            switch(halign) {
                case HorizontalAlignment.Left:
                    x = screenBounds.X + LabelIndent;
                    break;
                case HorizontalAlignment.Center:
                    x = screenBounds.X + (screenBounds.Width - sz.Width) / 2;
                    break;
                case HorizontalAlignment.Right:
                    x = screenBounds.Right - sz.Width - LabelIndent;
                    break;
            }
            switch(valign) {
                case VerticalAlignment.Top:
                    y = screenBounds.Y + LabelIndent;
                    break;
                case VerticalAlignment.Center:
                    y = screenBounds.Y + (screenBounds.Height - sz.Height) / 2;
                    break;
                case VerticalAlignment.Bottom:
                    y = screenBounds.Bottom - LabelIndent - sz.Height;
                    break;
            }

            Rect rect = new Rect(x, y, sz.Width, sz.Height);

            drawingContext.DrawRoundedRectangle(new SolidColorBrush(Color.FromArgb(0x80, 0, 0, 0)), null, rect, LabelRadius, LabelRadius);
            drawingContext.DrawText(GetFormattedText(text), new Point(rect.X + LabelPadding.Left, rect.Y + LabelPadding.Top));
        }

        protected Rect SplitterBounds {
            get {
                if(LayoutMode == PreviewPictureLayoutMode.Horizontal || LayoutMode == PreviewPictureLayoutMode.HorizontalSplit) {
                    return new Rect(Math.Floor(ImageInfo.ClipRect.Right - 1), ImageInfo.ClipRect.Y, 2, ImageInfo.ClipRect.Height);
                }
                return new Rect(ImageInfo.ClipRect.X, Math.Floor(ImageInfo.ClipRect.Bottom - 1), ImageInfo.ClipRect.Width, 2);
            }
        }

        protected Rect Splitter2Bounds {
            get {
                if(LayoutMode == PreviewPictureLayoutMode.Horizontal || LayoutMode == PreviewPictureLayoutMode.HorizontalSplit)
                    return new Rect(Math.Floor(ImageInfo2.ClipRect.Right - 1), ImageInfo2.ClipRect.Y, 2, ImageInfo2.ClipRect.Height);
                return new Rect(ImageInfo2.ClipRect.X, Math.Floor(ImageInfo2.ClipRect.Bottom - 1), ImageInfo2.ClipRect.Width, 2);
            }
        }

        private void DrawSplitter(DrawingContext drawingContext) {
            if(ScreenCount == 1)
                return;
            drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Gray, 0.5), SplitterBounds);
            if(ScreenCount == 3)
                drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Gray, 0.5), Splitter2Bounds);
        }

        private void DrawVeticalSplitPreviewTwo(DrawingContext drawingContext, Rect rect) {
            DrawSplitCoreTwo(drawingContext, rect);
        }

        protected void DrawImages(DrawingContext drawingContext, ImageSource source1, ImageSource source2, ImageSource source3, Rect rect1, Rect rect2, Rect rect3) {
            RectangleGeometry g1 = new RectangleGeometry(FirstScreen);
            RectangleGeometry g2 = new RectangleGeometry(SecondScreen);
            RectangleGeometry g3 = new RectangleGeometry(ThirdScreen);
            drawingContext.PushClip(g1);
            drawingContext.DrawImage(source1, rect1);
            drawingContext.Pop();
            drawingContext.PushClip(g2);
            drawingContext.DrawImage(source2, rect2);
            drawingContext.Pop();
            drawingContext.PushClip(g3);
            drawingContext.DrawImage(source3, rect3);
            drawingContext.Pop();
        }

        protected void DrawImages(DrawingContext drawingContext, ImageSource source1, ImageSource source2, Rect rect1, Rect rect2) {
            RectangleGeometry g1 = new RectangleGeometry(FirstScreen);
            RectangleGeometry g2 = new RectangleGeometry(SecondScreen);
            drawingContext.PushClip(g1);
            drawingContext.DrawImage(IsSwap ? source2 : source1, rect1);
            drawingContext.Pop();
            drawingContext.PushClip(g2);
            drawingContext.DrawImage(IsSwap ? source1 : source2, rect2);
            drawingContext.Pop();
        }

        private void DrawVerticalPreviewTwo(DrawingContext drawingContext, Rect rect) {
            Rect imgFirstScreen = rect;
            Rect imgSecondScreen = rect;
            imgSecondScreen.Y += ActualHeight / 2;
            DrawImages(drawingContext, Source, ProcessedSource, imgFirstScreen, imgSecondScreen);
        }

        private void DrawHorizontalSplitPreviewTwo(DrawingContext drawingContext, Rect rect) {
            DrawSplitCoreTwo(drawingContext, rect);
        }

        private void DrawSplitCoreTwo(DrawingContext drawingContext, Rect rect) {
            ImageSource secondImage = PreviewMode == SplitPicturePreviewType.Editing ? ProcessedSource : Source2;
            DrawImages(drawingContext, Source, secondImage, rect, rect);
        }

        protected internal int ScreenCount {
            get {
                switch(PreviewMode) {
                    case SplitPicturePreviewType.Three:
                        return 3;
                    case SplitPicturePreviewType.Editing:
                        return LayoutMode == PreviewPictureLayoutMode.Single ? 1 : 2;
                    case SplitPicturePreviewType.Two:
                        return 2;
                    default:
                        return 1;
                }
            }
        }

        protected internal Rect FirstScreen {
            get {
                if(LayoutMode == PreviewPictureLayoutMode.HorizontalSplit || LayoutMode == PreviewPictureLayoutMode.VerticalSplit)
                    return new Rect(0, 0, ActualWidth, ActualHeight);
                if(LayoutMode == PreviewPictureLayoutMode.Horizontal)
                    return new Rect(0, 0, ActualWidth / ScreenCount, ActualHeight);
                return new Rect(0, 0, ActualWidth, ActualHeight / ScreenCount);
            }
        }

        protected internal Rect SecondScreen {
            get {
                if(LayoutMode == PreviewPictureLayoutMode.HorizontalSplit || LayoutMode == PreviewPictureLayoutMode.VerticalSplit)
                    return new Rect(0, 0, ActualWidth, ActualHeight);
                if(LayoutMode == PreviewPictureLayoutMode.Horizontal)
                    return new Rect(ActualWidth / ScreenCount, 0, ActualWidth / ScreenCount, ActualHeight);
                return new Rect(0, ActualHeight / ScreenCount, ActualWidth, ActualHeight / ScreenCount);
            }
        }

        protected internal Rect ThirdScreen {
            get {
                if(LayoutMode == PreviewPictureLayoutMode.HorizontalSplit || LayoutMode == PreviewPictureLayoutMode.VerticalSplit)
                    return new Rect(0, 0, ActualWidth, ActualHeight);
                if(LayoutMode == PreviewPictureLayoutMode.Horizontal)
                    return new Rect(ActualWidth / ScreenCount * (ScreenCount - 1), 0, ActualWidth / ScreenCount, ActualHeight);
                return new Rect(0, ActualHeight / ScreenCount * (ScreenCount - 1), ActualWidth, ActualHeight / ScreenCount);
            }
        }

        private void DrawHorizontalPreviewTwo(DrawingContext drawingContext, Rect rect) {
            Rect imgFirstScreen = rect;
            rect.X += ActualWidth / 2;
            Rect imgSecondScreen = rect;

            ImageSource secondSource = PreviewMode == SplitPicturePreviewType.Editing ? ProcessedSource : Source2;
            DrawImages(drawingContext, Source, secondSource, imgFirstScreen, imgSecondScreen);
        }

        private void DrawSimplePreview(DrawingContext drawingContext, Rect rect) {
            drawingContext.DrawImage(ProcessedSource, rect);
        }
    }

    public enum PreviewPictureLayoutMode {
        Single,
        Horizontal,
        Vertical,
        HorizontalSplit,
        VerticalSplit
    }

    public class SplitImageInfo : ImageInfo {
        public SplitImageInfo(SplitPicturePreviewControl preview) : base(preview) {
            ScreenIndex = 0;
        }
        public int ScreenIndex { get; set; }
        protected Rect GetImageScreen(Rect rect) {
            rect.Width = Math.Max(0, rect.Width - Preview.ContentPadding * 2);
            rect.Height -= Math.Max(0, rect.Height - Preview.ContentPadding * 2);
            return rect;
        }
        public override Rect Screen {
            get {
                SplitPicturePreviewControl sp = (SplitPicturePreviewControl)Preview;
                if(sp.ScreenCount == 1)
                    return base.Screen;
                switch(ScreenIndex) {
                    case 0: return GetImageScreen(sp.FirstScreen);
                    case 1: return GetImageScreen(sp.SecondScreen);
                    case 2: return GetImageScreen(sp.ThirdScreen);
                }
                return base.Screen;
            }
        }
        public override Rect ClipRect {
            get {
                SplitPicturePreviewControl sp = (SplitPicturePreviewControl)Preview;
                if(sp.ScreenCount == 1)
                    return base.ClipRect;

                if(sp.LayoutMode == PreviewPictureLayoutMode.Horizontal || sp.LayoutMode == PreviewPictureLayoutMode.HorizontalSplit)
                    return new Rect(Preview.ActualWidth / sp.ScreenCount * ScreenIndex, 0, sp.ActualWidth / sp.ScreenCount, sp.ActualHeight);
                return new Rect(0, Preview.ActualHeight / sp.ScreenCount * ScreenIndex, sp.ActualWidth, sp.ActualHeight / sp.ScreenCount);
            }
        }
    }
}
