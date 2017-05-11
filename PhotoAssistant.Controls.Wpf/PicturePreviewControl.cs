using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using PhotoAssistant.Core;
using System.Collections.ObjectModel;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.Controls.Wpf {
    public class PicturePreviewControl : Control, IPictureNavigatorClient, IGridManagerOwner {

        static PicturePreviewControl() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PicturePreviewControl), new FrameworkPropertyMetadata(typeof(PicturePreviewControl)));
        }

        public PicturePreviewControl() {
            LastMovePoint = EmptyMovePoint;
            ClipToBounds = true;
            ExitCommand = new PreviewExitCommand(this);
            RotateLetCommand = new PreviewRotateLeftCommand(this);
            RotateRightCommand = new PreviewRotateRightCommand(this);
            FirstCommand = new PreviewFirstCommand(this);
            PrevPageCommand = new PreviewPrevPageCommand(this);
            PrevCommand = new PreviewPrevCommand(this);
            SlideShowCommand = new PreviewSlideShowCommand(this);
            NextCommand = new PreviewNextCommand(this);
            NextPageCommand = new PreviewNextPageCommand(this);
            LastCommand = new PreviewLastCommand(this);
            IncrementTimeCommand = new IncrementTimeCommand(this);
            DecrementTimeCommand = new DecrementTimeCommand(this);
            InputBindings.Add(new KeyBinding(new StopSlideShowCommand(this), Key.Escape, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(NextCommand, Key.Space, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(PrevCommand, Key.Back, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(NextCommand, Key.Right, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(PrevCommand, Key.Left, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(FirstCommand, Key.Home, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(LastCommand, Key.End, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(PrevPageCommand, Key.PageUp, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(NextPageCommand, Key.PageDown, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(new ToggleFullScreenCommand(this), Key.Enter, ModifierKeys.None));

        }

        public bool ShowGrid {
            get { return (bool)GetValue(ShowGridProperty); }
            set { SetValue(ShowGridProperty, value); }
        }

        public static readonly DependencyProperty ShowGridProperty =
            DependencyProperty.Register("ShowGrid", typeof(bool), typeof(PicturePreviewControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public double GridOpacity {
            get { return (double)GetValue(GridOpacityProperty); }
            set { SetValue(GridOpacityProperty, value); }
        }

        public static readonly DependencyProperty GridOpacityProperty =
            DependencyProperty.Register("GridOpacity", typeof(double), typeof(PicturePreviewControl), new FrameworkPropertyMetadata(0.8, FrameworkPropertyMetadataOptions.AffectsRender));

        public double GridSize {
            get { return (double)GetValue(GridSizeProperty); }
            set { SetValue(GridSizeProperty, value); }
        }

        public static readonly DependencyProperty GridSizeProperty =
            DependencyProperty.Register("GridSize", typeof(double), typeof(PicturePreviewControl), new FrameworkPropertyMetadata(40.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public Color GridColor {
            get { return (Color)GetValue(GridColorProperty); }
            set { SetValue(GridColorProperty, value); }
        }

        public static readonly DependencyProperty GridColorProperty =
            DependencyProperty.Register("GridColor", typeof(Color), typeof(PicturePreviewControl), new PropertyMetadata(Colors.Gray));

        GridManager gridManager;
        protected virtual GridManager GridManager {
            get {
                if(gridManager == null)
                    gridManager = new GridManager(this);
                return gridManager;
            }
        }

        protected virtual bool ShowGridCore {
            get { return ShowGrid; }
        }

        protected virtual void DrawGrid(DrawingContext drawingContext) {
            if(!ShowGridCore)
                return;
            GridManager.Mode = GridMode.UseSize;
            GridManager.CellSize = GridSize;
            GridManager.Color = GridColor;
            GridManager.Opacity = GridOpacity;
            GridManager.Render(drawingContext);
        }


        CropToolManager cropManager;
        protected CropToolManager CropManager {
            get {
                if(cropManager == null)
                    cropManager = CreateCropManager();
                return cropManager;
            }
        }

        protected virtual CropToolManager CreateCropManager() {
            return new CropToolManager(this);
        }

        public void ActivateCrop() {
            CropManager.Activate();
        }

        public void DeactivateCrop() {
            CropManager.Deactivate();
        }

        public double PanelScaleFactor {
            get { return (double)GetValue(PanelScaleFactorProperty); }
            set { SetValue(PanelScaleFactorProperty, value); }
        }

        public static readonly DependencyProperty PanelScaleFactorProperty =
            DependencyProperty.Register("PanelScaleFactor", typeof(double), typeof(PicturePreviewControl), new PropertyMetadata(1.0));

        public bool IsCompactMode {
            get { return (bool)GetValue(IsCompactModeProperty); }
            set { SetValue(IsCompactModeProperty, value); }
        }

        public static readonly DependencyProperty IsCompactModeProperty =
            DependencyProperty.Register("IsCompactMode", typeof(bool), typeof(PicturePreviewControl), new PropertyMetadata(false));


        public double ZoomChange {
            get { return (double)GetValue(ZoomChangeProperty); }
            set { SetValue(ZoomChangeProperty, value); }
        }

        public static readonly DependencyProperty ZoomChangeProperty =
            DependencyProperty.Register("ZoomChange", typeof(double), typeof(PicturePreviewControl), new PropertyMetadata(0.01));

        public double AnimationProgress {
            get { return (double)GetValue(AnimationProgressProperty); }
            set { SetValue(AnimationProgressProperty, value); }
        }

        public static readonly DependencyProperty AnimationProgressProperty =
            DependencyProperty.Register("AnimationProgress", typeof(double), typeof(PicturePreviewControl), new PropertyMetadata(0.0, new PropertyChangedCallback((d, e) => ((PicturePreviewControl)d).OnAnimationProgress(e))));

        public string FileInfoText {
            get { return (string)GetValue(FileInfoTextProperty); }
            set { SetValue(FileInfoTextProperty, value); }
        }

        public static readonly DependencyProperty FileInfoTextProperty =
            DependencyProperty.Register("FileInfoText", typeof(string), typeof(PicturePreviewControl), new PropertyMetadata(""));

        public ImageSource Source {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(PicturePreviewControl), new PropertyMetadata(null, new PropertyChangedCallback((d, e) => ((PicturePreviewControl)d).OnSourceChanged(e))));

        public double Zoom {
            get { return (double)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register("Zoom", typeof(double), typeof(PicturePreviewControl), new PropertyMetadata(1.0, new PropertyChangedCallback((d, e) => ((PicturePreviewControl)d).OnZoomChanged(e)), new CoerceValueCallback((d, v) => ((PicturePreviewControl)d).CoerceZoom(v))));

        public double MinimumZoom {
            get { return (double)GetValue(MinimumZoomProperty); }
            set { SetValue(MinimumZoomProperty, value); }
        }

        public static readonly DependencyProperty MinimumZoomProperty =
            DependencyProperty.Register("MinimumZoom", typeof(double), typeof(PicturePreviewControl), new PropertyMetadata(0.01, new PropertyChangedCallback((d, e) => ((PicturePreviewControl)d).OnMinimumZoomChanged(e))));

        public double MaximumZoom {
            get { return (double)GetValue(MaximumZoomProperty); }
            set { SetValue(MaximumZoomProperty, value); }
        }

        public static readonly DependencyProperty MaximumZoomProperty =
            DependencyProperty.Register("MaximumZoom", typeof(double), typeof(PicturePreviewControl), new PropertyMetadata(10.0, new PropertyChangedCallback((d, e) => ((PicturePreviewControl)d).OnMaximumZoomChanged(e))));

        public double ContentChangeAnimationDelay {
            get { return (double)GetValue(ContentChangeAnimationDelayProperty); }
            set { SetValue(ContentChangeAnimationDelayProperty, value); }
        }

        public static readonly DependencyProperty ContentChangeAnimationDelayProperty =
            DependencyProperty.Register("ContentChangeAnimationDelay", typeof(double), typeof(PicturePreviewControl), new PropertyMetadata(3.0));

        public double ContentChangeProgress {
            get { return (double)GetValue(ContentChangeProgressProperty); }
            set { SetValue(ContentChangeProgressProperty, value); }
        }

        public static readonly DependencyProperty ContentChangeProgressProperty =
            DependencyProperty.Register("ContentChangeProgress", typeof(double), typeof(PicturePreviewControl), new PropertyMetadata(0.0));

        public PicturePreviewEffectType EffectType {
            get { return (PicturePreviewEffectType)GetValue(EffectTypeProperty); }
            set { SetValue(EffectTypeProperty, value); }
        }

        public static readonly DependencyProperty EffectTypeProperty =
            DependencyProperty.Register("EffectType", typeof(PicturePreviewEffectType), typeof(PicturePreviewControl), new PropertyMetadata(PicturePreviewEffectType.None));

        protected virtual void OnSourceChanged(DependencyPropertyChangedEventArgs e) {
            ImageInfo.PrevSource = (ImageSource)e.OldValue;
            ImageInfo.Source = Source;
            ChangePicture(ImageInfo);
            RaisePropertiesChanged();
        }
        protected void FitToScreen(bool animated, PicturePreviewFitMode fitMode) {
            if(!animated) {
                ImageInfo.UseDefaultLayout = true;
                ImageInfo.Info = new ScrollZoomInfo();
                ImageInfo.Info.FitMode = fitMode;
                CalcLayout(ImageInfo);
            }
            else {
                ImageInfo.Info = new ScrollZoomInfo();
                ImageInfo.Info.FitMode = fitMode;
                UpdateInfo(ImageInfo);
                ScrollZoomHelper.CalcSqueezeBounds(ImageInfo.Info);
                SetZoomAnimated(ImageInfo, ImageInfo.Info.Zoom);
            }
        }
        ColorPickerControl colorPicker;
        protected ColorPickerControl ColorPicker {
            get {
                if(colorPicker == null)
                    colorPicker = CreateColorPicker();
                return colorPicker;
            }
        }
        protected virtual ColorPickerControl CreateColorPicker() {
            return new ColorPickerControl();
        }

        Popup colorPickerPopup;
        protected Popup ColorPickerPopup {
            get {
                if(colorPickerPopup == null)
                    colorPickerPopup = CreateColorPickerPopup();
                return colorPickerPopup;
            }
        }

        protected virtual Popup CreateColorPickerPopup() {
            Popup popup = new Popup();
            popup.Placement = PlacementMode.RelativePoint;
            popup.PlacementTarget = this;
            popup.Child = ColorPicker;
            return popup;
        }

        public void ShowColorPicker() {
            ShowColorPicker(new Point(100000, 100000));
        }
        public void ShowColorPicker(Point mouseLocation) {
            UpdateColorPickerProperties();
            UpdateColorPickerPosition(mouseLocation);
            UpdateColorPickerColors(mouseLocation);
            ColorPickerVisible = true;
            ColorPickerPopup.IsOpen = true;
        }

        protected bool ColorPickerVisible { get; set; }
        byte[] pixel;
        protected byte[] Pixel {
            get {
                if(pixel == null)
                    pixel = new byte[4];
                return pixel;
            }
        }

        protected virtual void UpdateColorPickerColors(Point mouseLocation) {
            ImageInfo imageInfo = GetImageInfo(mouseLocation);
            Point pt = ScreenToImagePoint(ImageInfo, mouseLocation);
            int centerX = (int)pt.X;
            int centerY = (int)pt.Y;
            BitmapImage bitmapImage = (BitmapImage)imageInfo.Source;
            int height = bitmapImage.PixelHeight;
            int width = bitmapImage.PixelWidth;
            int nStride = (bitmapImage.PixelWidth * bitmapImage.Format.BitsPerPixel + 7) / 8;

            for(int y = 0; y < ColorPicker.RowCount; y++) {
                for(int x = 0; x < ColorPicker.ColumnCount; x++) {
                    int index = y * ColorPicker.ColumnCount + x;
                    int pixelY = centerY - ColorPicker.RowCount / 2 + y;
                    int pixelX = centerX - ColorPicker.ColumnCount / 2 + x;
                    if(pixelY < 0 || pixelX < 0 || pixelY >= height || pixelX >= width) {
                        ColorPicker.ColorCells[index].Color = Colors.Black;
                    }
                    else {
                        bitmapImage.CopyPixels(new Int32Rect(pixelX, pixelY, 1, 1), Pixel, 4, 0);
                        ColorPicker.ColorCells[index].Color = ColorFromArray(bitmapImage.Format, Pixel);
                    }
                    if(pixelY == centerY && pixelX == centerX) {
                        ColorPicker.Color = ColorPicker.ColorCells[index].Color;
                    }
                }
            }
        }

        private Color ColorFromArray(PixelFormat format, byte[] pixel) {
            if(format == PixelFormats.Pbgra32 || format == PixelFormats.Bgra32)
                return Color.FromArgb(pixel[3], pixel[2], pixel[1], pixel[0]);
            else if(format == PixelFormats.Rgb24)
                return Color.FromArgb(255, pixel[0], pixel[1], pixel[2]);
            else if(format == PixelFormats.Bgr24)
                return Color.FromArgb(255, pixel[2], pixel[1], pixel[0]);
            return Color.FromArgb(pixel[3], pixel[2], pixel[1], pixel[0]);
        }

        private Point ScreenToImagePoint(ImageInfo imageInfo, Point mouseLocation) {
            return new Point(
                Math.Max(0, Math.Min(Math.Round((mouseLocation.X - imageInfo.Info.ScreenBounds.X) / imageInfo.Info.ScreenBounds.Width * imageInfo.Info.ImageSize.Width), imageInfo.Info.ImageSize.Width -1)), 
                Math.Max(0, Math.Min(Math.Round((mouseLocation.Y - imageInfo.Info.ScreenBounds.Y) / imageInfo.Info.ScreenBounds.Height * imageInfo.ImageSize.Height), imageInfo.Info.ImageSize.Height - 1)));
        }

        protected virtual ImageInfo GetImageInfo(Point mouseLocation) {
            return ImageInfo;
        }

        protected virtual void UpdateColorPickerProperties() {
            ColorPicker.RowCount = 9;
            ColorPicker.ColumnCount = 9;
            ColorPickerControl.SetColumnCount(ColorPicker, 9);
            ColorPicker.CellSize = 180 / ColorPicker.RowCount;
        }

        protected virtual Size ColorPickerPopupOffset { get { return new Size(48, 48); } }

        protected virtual void UpdateColorPickerPosition(Point mouseLocation) {
            Point pt = new Point(mouseLocation.X, mouseLocation.Y + ColorPickerPopupOffset.Height);
            ColorPickerPopup.HorizontalOffset = pt.X;
            ColorPickerPopup.VerticalOffset = pt.Y;
        }

        public void FitToScreen(bool animated) {
            ImageInfo.UseDefaultLayout = true;
            ImageInfo.Info.FitMode = PicturePreviewFitMode.FitToScreen;
            UpdateInfo(ImageInfo);
            SetZoomAnimated(ScrollZoomHelper.CalcSqueezeZoom(ImageInfo.Info));
        }

        public void FillToScreen(bool animated) {
            ImageInfo.UseDefaultLayout = true;
            ImageInfo.Info.FitMode = PicturePreviewFitMode.FillToScreen;
            UpdateInfo(ImageInfo);
            SetZoomAnimated(ScrollZoomHelper.CalcZoomOutZoom(ImageInfo.Info));
        }

        protected virtual void ChangePicture(ImageInfo imageInfo) {
            if(!AllowAnimation) {
                if(imageInfo.PrevSource == null || ActualWidth == 0 || ActualHeight == 0) {
                    imageInfo.UseDefaultLayout = true;
                    imageInfo.Info = new ScrollZoomInfo();
                    CalcLayout(imageInfo);
                    return;
                }
                RunSimpleContentChangeAnimation(imageInfo);
            } else {
                RunContentChangeAnimation(imageInfo);
            }
        }

        protected internal PicturePreviewSlideDirection Direction { get; set; }

        protected virtual void RunSimpleContentChangeAnimation(ImageInfo imageInfo) {
            imageInfo.InContentChangeAnimation = true;

            imageInfo.PrevContentInfo = InitializePrevContentInfo(imageInfo, PicturePreviewEffectType.Fade, 200, 0.2);
            imageInfo.NextContentInfo = InitializeNextContentInfo(imageInfo, PicturePreviewEffectType.Fade, 200, 0.2);
            imageInfo.PrevContentInfo.AnimationBehavior = FillBehavior.Stop;
            imageInfo.NextContentInfo.AnimationBehavior = FillBehavior.Stop;

            if(imageInfo.PrevContentInfo != null)
                imageInfo.PrevContentInfo.Start();
            if(imageInfo.NextContentInfo != null)
                imageInfo.NextContentInfo.Start();
        }

        protected virtual void RunContentChangeAnimation(ImageInfo imageInfo) {
            imageInfo.InContentChangeAnimation = true;

            imageInfo.PrevContentInfo = InitializePrevContentInfo(imageInfo, EffectType, ContentChangeAnimationDelay);
            imageInfo.NextContentInfo = InitializeNextContentInfo(imageInfo, EffectType, ContentChangeAnimationDelay);

            if(imageInfo.PrevContentInfo != null)
                imageInfo.PrevContentInfo.Start();
            if(imageInfo.NextContentInfo != null)
                imageInfo.NextContentInfo.Start();
            if(IsSlideShow) {
                SlideShowTimer.Interval = TimeSpan.FromSeconds(ContentChangeAnimationDelay);
                SlideShowTimer.Start();
            }
        }
        protected ContentChangeAnimationInfo InitializeNextContentInfo(ImageInfo info, PicturePreviewEffectType effect, double duration) {
            return InitializeNextContentInfo(info, effect, duration, 1.0);
        }
        protected ContentChangeAnimationInfo InitializePrevContentInfo(ImageInfo info, PicturePreviewEffectType effect, double duration) {
            return InitializePrevContentInfo(info, effect, duration, 1.0);
        }
        protected ContentChangeAnimationInfo InitializeNextContentInfo(ImageInfo info, PicturePreviewEffectType effect, double duration, double fadeDuration) {
            ContentChangeAnimationInfo cinfo = new ContentChangeAnimationInfo(this) { FadeAnimationDuration = fadeDuration };
            cinfo.InitializeStoryboard(info, info.Source, effect, duration, false);
            return cinfo;
        }

        protected ContentChangeAnimationInfo InitializePrevContentInfo(ImageInfo info, PicturePreviewEffectType effect, double duration, double fadeDuration) {
            if(effect == PicturePreviewEffectType.PanAndZoom) {
                if(imageInfo.NextContentInfo != null) imageInfo.NextContentInfo.IsPrev = true;
                return imageInfo.NextContentInfo;
            }
            ContentChangeAnimationInfo cinfo = new ContentChangeAnimationInfo(this) { FadeAnimationDuration = fadeDuration };
            cinfo.InitializeStoryboard(info, info.PrevSource, effect, duration, true);
            return cinfo;
        }

        public event EventHandler ZoomChanged;
        protected void RaiseZoomChanged() {
            if(ZoomChanged != null)
                ZoomChanged(this, EventArgs.Empty);
        }

        protected bool InternalSetZoom { get; set; }
        protected virtual void OnZoomChanged(DependencyPropertyChangedEventArgs e) {
            OnZoomChangedCore(e, ImageInfo);
        }

        protected virtual void OnZoomChangedCore(DependencyPropertyChangedEventArgs e, ImageInfo info) {
            if(!InternalSetZoom) {
                ImageInfo.UseDefaultLayout = false;
                UpdateInfoByZoom(info, (double)e.OldValue, Zoom, new Point(info.Screen.Width / 2, info.Screen.Height / 2));
                ScrollZoomHelper.Zoom(info.Info);
            }
            RaiseZoomChanged();
            InvalidateVisual();
        }

        private void OnMinimumZoomChanged(DependencyPropertyChangedEventArgs e) {
            Zoom = CoerceZoom(Zoom);
        }

        private void OnMaximumZoomChanged(DependencyPropertyChangedEventArgs e) {
            Zoom = CoerceZoom(Zoom);
        }

        private double CoerceZoom(object value) {
            return CoerceZoom((double)value);
        }

        protected double CoerceZoom(double value) {
            return Math.Min(MaximumZoom, Math.Max(MinimumZoom, value));
        }

        private object CoerceHorizontalScrollPosition(object value) {
            return value;
            //Info.ScrollPosition = new Point((double)value, VerticalScrollPosition);
            //ScrollZoomHelper.CoerceScrollPosition(Info);
            //return Info.ScrollPosition.X;
        }

        private object CoerceVerticalScrollPosition(object value) {
            return value;
            //Info.ScrollPosition = new Point(HorizontalScrollPosition, (double)value);
            //ScrollZoomHelper.CoerceScrollPosition(Info);
            //return Info.ScrollPosition.Y;
        }

        bool ContainsPoint(MouseButtonEventArgs e, FrameworkElement elem) {
            Point pt = e.GetPosition(elem);
            return pt.X >= 0 && pt.Y >= 0 && pt.X <= elem.ActualWidth && pt.Y < elem.ActualHeight;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e) {
            base.OnMouseDown(e);
            Focus();
            if(e.ChangedButton == MouseButton.Left) {
                Mouse.Capture(this);
                if(ColorPickerVisible) {
                    HideColorPicker();
                }
                if(CropManager.IsActive && CropManager.OnMouseDown(e.LeftButton, e.GetPosition(this)))
                    return;
                if(ContainsPoint(e, ToolbarPanel))
                    return;
                LastMovePoint = e.GetPosition(this);
            }
        }

        protected virtual void HideColorPicker() {
            ColorPickerVisible = false;
            ColorPickerPopup.IsOpen = false;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e) {
            base.OnMouseUp(e);
            Mouse.Capture(null);
            if(CropManager.IsActive)
                CropManager.OnMouseUp(e.LeftButton, e.GetPosition(this));
            LastMovePoint = EmptyMovePoint;
        }

        protected Point EmptyMovePoint { get { return new Point(-1, -1); } }
        protected Point LastMovePoint { get; set; }

        protected override void OnMouseLeave(MouseEventArgs e) {
            base.OnMouseLeave(e);
            if(e.LeftButton == MouseButtonState.Pressed)
                return;
            if(ColorPickerVisible) {
                UpdateColorPicker(new Point(100000, 100000));
            }
            if(CropManager.IsActive)
                CropManager.OnMouseLeave(new Point(10000, 10000));
        }

        protected virtual void ProcessScroll(Point pt) {
            Point scrollDelta = new Point(LastMovePoint.X - pt.X, LastMovePoint.Y - pt.Y);
            LastMovePoint = pt;
            OnScroll(scrollDelta);
            InvalidateVisual();
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            Point pt = e.GetPosition(this);
            if(e.LeftButton == MouseButtonState.Released && ColorPickerVisible)
                UpdateColorPicker(pt);
            if(CropManager.IsActive) {
                if(CropManager.OnMouseMove(e.LeftButton, pt) || e.LeftButton == MouseButtonState.Pressed)
                    return;
            }
            if(e.LeftButton == MouseButtonState.Pressed && LastMovePoint != EmptyMovePoint) {
                ProcessScroll(pt);
                return;
            }
            if(ShouldShowInfoPanels) {
                ShowInfoPanels();
            }
        }

        protected virtual void UpdateColorPicker(Point pt) {
            UpdateColorPickerVisibility(pt);
            UpdateColorPickerPosition(pt);
            UpdateColorPickerColors(pt);
        }

        protected virtual void UpdateColorPickerVisibility(Point pt) {
            ColorPickerPopup.IsOpen = ImageInfo.Info.ScreenBounds.Contains(pt);
        }

        protected virtual void OnScroll(Point scrollDelta) {
            OnScroll(ImageInfo, scrollDelta);
        }

        protected void OnScroll(ImageInfo imageInfo, Point scrollDelta) {
            imageInfo.UseDefaultLayout = false;
            UpdateInfo(imageInfo);
            imageInfo.Info.ScrollDelta = scrollDelta;
            imageInfo.Info.RotateAngle = RotateAngle;
            ScrollZoomHelper.Scroll(imageInfo.Info);
            RaisePropertiesChanged();
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e) {
            base.OnMouseDoubleClick(e);
            if(ToolbarPanel.IsMouseOver || CaptionPanel.IsMouseOver)
                return;
            ToggleFullScreenCore();
        }

        protected Control LoadingIndicator { get; set; }
        protected ComboBox EffectCombo { get; private set; }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            CaptionPanel = (FrameworkElement)GetTemplateChild("CaptionPanel");
            ToolbarPanel = (FrameworkElement)GetTemplateChild("ToolbarPanel");
            CaptionPanel.DataContext = this;
            ToolbarPanel.DataContext = this;
            LoadingIndicator = (Control)GetTemplateChild("PART_LoadingIndicator");
            TimeEdit = (TextBox)GetTemplateChild("timeEdit");
            EffectCombo = (ComboBox)GetTemplateChild("animationCombo");
        }

        DispatcherTimer timer;
        DispatcherTimer Timer {
            get {
                if(timer == null) {
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(5);
                    timer.Tick += timer_Tick;
                }
                return timer;
            }
        }

        DispatcherTimer slideShowTimer;
        DispatcherTimer SlideShowTimer {
            get {
                if(slideShowTimer == null) {
                    slideShowTimer = new DispatcherTimer();
                    slideShowTimer.Interval = TimeSpan.FromSeconds(ContentChangeAnimationDelay);
                    slideShowTimer.Tick += slideShowTimer_Tick;
                }
                return slideShowTimer;
            }
        }

        void slideShowTimer_Tick(object sender, EventArgs e) {
            slideShowTimer.Stop();
            if(!CheckAnimationCompleted()) {
                StartWaitTimer();
                return;
            }
            ClearPreviousImageSource();
            SetNextSlideShowFile();
        }

        private void ClearPreviousImageSource() {
            if(ImageInfo.PrevContentInfo == null)
                return;
            ImageInfo.PrevContentInfo.Source = null;
            if(CurrentFile == null)
                return;
            int index = Files.IndexOf(CurrentFile);
            index--;
            if(index < 0) index = Files.Count - 1;
            Files[index].ImageSource = null;
        }

        void SetNextSlideShowFile() {
            if(CurrentFile == null)
                CurrentFile = Files[0];
            else {
                int index = Files.IndexOf(CurrentFile);
                index++;
                if(index >= Files.Count)
                    index = 0;
                CurrentFile = Files[index];
            }
        }

        DispatcherTimer WaitTimer { get; set; }
        private void StartWaitTimer() {
            if(WaitTimer == null) {
                WaitTimer = new DispatcherTimer();
                WaitTimer.Interval = TimeSpan.FromMilliseconds(100);
                WaitTimer.Tick += WaitTimer_Tick;
            }
            WaitTimer.Start();
        }

        void WaitTimer_Tick(object sender, EventArgs e) {
            if(!CheckAnimationCompleted())
                return;
            WaitTimer.Stop();
            ClearPreviousImageSource();
            SetNextSlideShowFile();
        }

        private bool CheckAnimationCompleted() {
            if(ImageInfo.PrevContentInfo != null && ImageInfo.PrevContentInfo.InProgress) return false;
            if(ImageInfo.NextContentInfo != null && ImageInfo.NextContentInfo.InProgress) return false;
            return true;
        }

        protected TextBox TimeEdit { get; private set; }
        void timer_Tick(object sender, EventArgs e) {
            if((TimeEdit != null && TimeEdit.IsFocused) ||
                (EffectCombo != null && EffectCombo.IsFocused))
                return;
            Dispatcher.BeginInvoke(new Action(HideInfoPanels));
            Timer.Stop();
        }

        private void ShowInfoPanels() {
            Timer.Stop();
            Timer.Start();
            VisualStateManager.GoToState(this, "ShowPanels", false);
        }
        private void HideInfoPanels() {
            VisualStateManager.GoToState(this, "HidePanels", false);
        }

        protected FrameworkElement CaptionPanel { get; set; }
        protected FrameworkElement ToolbarPanel { get; set; }

        public virtual void SetZoomAnimated(double newZoom) {
            SetZoomAnimated(ImageInfo, newZoom);
            InternalSetZoom = true;
            Zoom = newZoom;
            InternalSetZoom = false;
            MakeAnimatedZoom();
        }

        protected internal void SetZoomAnimated(ImageInfo info, double newZoom) {
            SetZoomAnimatedCore(info, newZoom, new Point(ActualWidth / 2, ActualHeight / 2));
        }

        protected virtual void SetZoomAnimatedCore(ImageInfo info, double newZoom, Point point) {
            imageInfo.UseDefaultLayout = false;
            newZoom = Math.Max(MinimumZoom, Math.Min(MaximumZoom, newZoom));
            UpdateInfoByZoom(info, Zoom, newZoom, point);
            info.Info.PrevScreenBounds = info.Info.ScreenBounds;
            ScrollZoomHelper.Zoom(info.Info);
        }

        protected virtual void SetScrollCore(ImageInfo info, Point newScroll) {
            ImageInfo.UseDefaultLayout = false;
            UpdateInfoByScroll(info, info.Info.ScrollPosition, newScroll);
            ScrollZoomHelper.Scroll(info.Info);
            RaisePropertiesChanged();
            InvalidateVisual();
        }

        protected virtual void SetScrollAnimatedCore(ImageInfo info, Point newScroll) {
            ImageInfo.UseDefaultLayout = false;
            UpdateInfoByScroll(info, info.Info.ScrollPosition, newScroll);
            info.Info.PrevScreenBounds = info.Info.ScreenBounds;
            ScrollZoomHelper.Scroll(info.Info);
            RaisePropertiesChanged();
            info.Info.Animating = true;
            RunAnimationScroll();
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e) {
            base.OnMouseWheel(e);
            OnMouseWheelCore(e);
        }

        protected virtual void OnMouseWheelCore(MouseWheelEventArgs e) {
            double newZoom = Zoom + e.Delta / 120.0 * ZoomChange;
            Point pt = e.GetPosition(this);
            SetZoomAnimatedCore(newZoom, pt);

            InternalSetZoom = true;
            Zoom = newZoom;
            InternalSetZoom = false;

            MakeAnimatedZoom();
        }
        protected virtual void SetZoomAnimatedCore(double newZoom, Point pt) {
            SetZoomAnimatedCore(ImageInfo, newZoom, ImageInfo.ScreenToLocal(pt));
        }

        protected Storyboard Storyboard { get; set; }
        protected Storyboard StoryboardScroll { get; set; }

        protected void MakeAnimatedZoom() {
            PrepareForAnimatedZoom();
            RunAnimationZoom();
        }

        void RunAnimationZoom() {
            if(Storyboard != null) {
                BeginStoryboard(Storyboard);
                return;
            }
            Storyboard = new Storyboard();
            Storyboard.Completed += OnZoomAnimationCompleted;
            DoubleAnimationUsingKeyFrames anim = new DoubleAnimationUsingKeyFrames();
            anim.KeyFrames.Add(new SplineDoubleKeyFrame(0.0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))));
            anim.KeyFrames.Add(new SplineDoubleKeyFrame(0.9, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(120))));
            anim.KeyFrames.Add(new SplineDoubleKeyFrame(1.0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150))));
            Storyboard.Children.Add(anim);
            Storyboard.SetTarget(anim, this);
            Storyboard.SetTargetProperty(anim, new PropertyPath(AnimationProgressProperty));
            BeginStoryboard(Storyboard);
        }

        void RunAnimationScroll() {
            if(StoryboardScroll != null) {
                BeginStoryboard(StoryboardScroll);
                return;
            }
            StoryboardScroll = new Storyboard();
            StoryboardScroll.Completed += OnZoomAnimationCompleted;
            DoubleAnimation anim = new DoubleAnimation() { From = 0.0, To = 1.0, Duration = new Duration(TimeSpan.FromMilliseconds(300)) };
            anim.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            //DoubleAnimationUsingKeyFrames anim = new DoubleAnimationUsingKeyFrames();
            //anim.KeyFrames.Add(new SplineDoubleKeyFrame(0.0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))));
            //anim.KeyFrames.Add(new SplineDoubleKeyFrame(0.9, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200))));
            //anim.KeyFrames.Add(new SplineDoubleKeyFrame(1.0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300))));
            StoryboardScroll.Children.Add(anim);
            Storyboard.SetTarget(anim, this);
            Storyboard.SetTargetProperty(anim, new PropertyPath(AnimationProgressProperty));
            BeginStoryboard(StoryboardScroll);
        }

        protected virtual void PrepareForAnimatedZoom() {
            PrepareForAnimatedZoom(ImageInfo);
        }

        protected virtual void PrepareForAnimatedZoom(ImageInfo imageInfo) {
            imageInfo.UseDefaultLayout = false;
            imageInfo.Info.Animating = true;
            imageInfo.Info.AnimatedScreenBounds = imageInfo.Info.PrevScreenBounds;
        }

        protected virtual void OnZoomAnimationCompleted(object sender, EventArgs e) {
            Storyboard.Completed -= OnZoomAnimationCompleted;
            Storyboard = null;
            ImageInfo.Info.Animating = false;
            InvalidateVisual();
        }

        private void OnAnimationProgress(DependencyPropertyChangedEventArgs e) {
            OnAnimationProgressCore();
            InvalidateVisual();
        }

        protected virtual void OnAnimationProgressCore() {
            OnAnimationProgress(ImageInfo);
        }

        protected virtual void OnAnimationProgress(ImageInfo imageInfo) {
            imageInfo.Info.AnimatedScreenBounds = new Rect(
                    imageInfo.Info.PrevScreenBounds.X + (imageInfo.Info.ScreenBounds.X - imageInfo.Info.PrevScreenBounds.X) * AnimationProgress,
                    imageInfo.Info.PrevScreenBounds.Y + (imageInfo.Info.ScreenBounds.Y - imageInfo.Info.PrevScreenBounds.Y) * AnimationProgress,
                    imageInfo.Info.PrevScreenBounds.Width + (imageInfo.Info.ScreenBounds.Width - imageInfo.Info.PrevScreenBounds.Width) * AnimationProgress,
                    imageInfo.Info.PrevScreenBounds.Height + (imageInfo.Info.ScreenBounds.Height - imageInfo.Info.PrevScreenBounds.Height) * AnimationProgress
                );
        }

        protected virtual ImageSource GetDrawSource() { return Source; }

        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);
            RenderImageShadow(drawingContext);
            RenderImage(drawingContext);
            DrawGrid(drawingContext);
            if(CropManager.IsActive)
                CropManager.Render(drawingContext);
        }

        protected virtual void RenderImageShadow(DrawingContext drawingContext) {
            RenderImageShadow(drawingContext, ImageInfo);
        }

        protected void RenderImageShadow(DrawingContext drawingContext, ImageInfo imageInfo) {
            if(!imageInfo.InContentChangeAnimation && !imageInfo.Info.Animating && !imageInfo.SuppressCalcLayout)
                CalcLayout(ImageInfo);
            Rect bounds = imageInfo.Info.Animating ? imageInfo.Info.AnimatedScreenBounds : imageInfo.Info.ScreenBounds;
            if(bounds.Width == 0 || bounds.Height == 0)
                return;
            double angle = imageInfo.Info.UseDefaultRotateOrigin ? CurrentRotateAngle : imageInfo.Info.RotateAngle;
            drawingContext.PushTransform(new RotateTransform(angle, imageInfo.Info.RotateOrigin.X, imageInfo.Info.RotateOrigin.Y));
            RenderImageShadow(drawingContext, bounds);
            drawingContext.Pop();
        }

        protected Color ShadowStartColor {
            get { return Color.FromArgb(80, 0, 0, 0); }
        }

        protected Color ShadowMiddleColor {
            get { return Color.FromArgb(30, 0, 0, 0); }
        }

        protected Color ShadowEndColor {
            get { return Color.FromArgb(5, 0, 0, 0); }
        }

        double contentPadding = 10;
        public double ContentPadding {
            get { return contentPadding; }
            set {
                if(ContentPadding == value)
                    return;
                contentPadding = value;
                InvalidateVisual();
            }
        }
        internal double ContentPaddingCore {
            get { return contentPadding; }
            set { contentPadding = value; }
        }

        double shadowTickness = 10;
        public double ShadowThickness {
            get { return shadowTickness; }
            set {
                if(ShadowThickness == value)
                    return;
                shadowTickness = value;
                InvalidateVisual();
            }
        }

        protected virtual void RenderImageShadow(DrawingContext drawingContext, Rect bounds) {
            double middlePos = 0.5;
            LinearGradientBrush topCenterBrush = new LinearGradientBrush(ShadowMiddleColor, ShadowStartColor, 90);
            topCenterBrush.GradientStops[0].Offset = 1.0 - middlePos;
            topCenterBrush.GradientStops.Insert(0, new GradientStop(ShadowEndColor, 0.0));

            LinearGradientBrush bottomCenterBrush = new LinearGradientBrush(ShadowStartColor, ShadowMiddleColor, 90);
            bottomCenterBrush.GradientStops[1].Offset = middlePos;
            bottomCenterBrush.GradientStops.Add(new GradientStop(ShadowEndColor, 1.0));
            LinearGradientBrush middleLeftBrush = new LinearGradientBrush(ShadowMiddleColor, ShadowStartColor, 0);
            middleLeftBrush.GradientStops[0].Offset = 1.0 - middlePos;
            middleLeftBrush.GradientStops.Insert(0, new GradientStop(ShadowEndColor, 0.0));
            LinearGradientBrush middleRightBrush = new LinearGradientBrush(ShadowStartColor, ShadowMiddleColor, 0);
            middleRightBrush.GradientStops[1].Offset = middlePos;
            middleRightBrush.GradientStops.Add(new GradientStop(ShadowEndColor, 1.0));

            RadialGradientBrush cornerBrush = new RadialGradientBrush(ShadowStartColor, ShadowMiddleColor);
            cornerBrush.GradientStops[1].Offset = middlePos;
            cornerBrush.GradientStops.Add(new GradientStop(ShadowEndColor, 1.0));
            drawingContext.DrawRectangle(topCenterBrush, null, new Rect(bounds.X + ShadowThickness / 2, bounds.Y - ShadowThickness / 2, bounds.Width - ShadowThickness / 2, ShadowThickness));
            drawingContext.DrawRectangle(bottomCenterBrush, null, new Rect(bounds.X + ShadowThickness / 2, bounds.Bottom, bounds.Width - ShadowThickness / 2, ShadowThickness));
            drawingContext.DrawRectangle(middleLeftBrush, null, new Rect(bounds.X - ShadowThickness / 2, bounds.Y + ShadowThickness / 2, ShadowThickness, bounds.Height - ShadowThickness / 2));
            drawingContext.DrawRectangle(middleRightBrush, null, new Rect(bounds.Right, bounds.Y + ShadowThickness / 2, ShadowThickness, bounds.Height - ShadowThickness / 2));

            DrawShadowCorner(drawingContext, new Point(bounds.X + ShadowThickness / 2, bounds.Y + ShadowThickness / 2), cornerBrush, -1, -1);
            DrawShadowCorner(drawingContext, new Point(bounds.Right, bounds.Y + ShadowThickness / 2), cornerBrush, 0, -1);
            DrawShadowCorner(drawingContext, new Point(bounds.X + ShadowThickness / 2, bounds.Bottom), cornerBrush, -1, 0);
            DrawShadowCorner(drawingContext, new Point(bounds.Right, bounds.Bottom), cornerBrush, 0, 0);
        }

        protected virtual void DrawShadowCorner(DrawingContext drawingContext, Point location, Brush brush, double dirX, double dirY) {
            RectangleGeometry clip = new RectangleGeometry(new Rect(location.X + ShadowThickness * dirX, location.Y + ShadowThickness * dirY, ShadowThickness, ShadowThickness));
            drawingContext.PushClip(clip);
            drawingContext.DrawEllipse(brush, null, location, ShadowThickness, ShadowThickness);
            drawingContext.Pop();
        }

        protected virtual void RenderImage(DrawingContext drawingContext, ImageInfo imageInfo) {
            RectangleGeometry g = new RectangleGeometry(imageInfo.ClipRect);
            drawingContext.PushClip(g);
            try {
                if(imageInfo.InContentChangeAnimation) {
                    if(imageInfo.PrevSource != null)
                        DrawContent(drawingContext, imageInfo.PrevContentInfo);
                    if(Source != null)
                        DrawContent(drawingContext, imageInfo.NextContentInfo);
                    return;
                }
                if(Source != null) {
                    if(!imageInfo.Info.Animating && !ImageInfo.SuppressCalcLayout) {
                        CalcLayout(imageInfo);
                    }
                    Rect rect = imageInfo.Info.Animating ? imageInfo.Info.AnimatedScreenBounds : imageInfo.Info.ScreenBounds;
                    double angle = imageInfo.Info.UseDefaultRotateOrigin ? CurrentRotateAngle : imageInfo.Info.RotateAngle;
                    drawingContext.PushTransform(new RotateTransform(angle, imageInfo.Info.RotateOrigin.X, imageInfo.Info.RotateOrigin.Y));
                    drawingContext.DrawImage(imageInfo.Source, rect);
                    drawingContext.Pop();
                }
            }
            finally {
                drawingContext.Pop();
            }
        }

        protected virtual void RenderImage(DrawingContext drawingContext) {
            RenderImage(drawingContext, ImageInfo);
        }

        double CalcValue(double start, double end, double progress) {
            return start + (end - start) * progress;
        }

        protected virtual void DrawContent(DrawingContext drawingContext, ContentChangeAnimationInfo info) {
            info.Draw(drawingContext);
        }

        ImageInfo imageInfo;
        protected internal ImageInfo ImageInfo {
            get {
                if(imageInfo == null)
                    imageInfo = CreateImageInfo();
                return imageInfo;
            }
        }

        protected virtual ImageInfo CreateImageInfo() {
            return new ImageInfo(this);
        }

        protected void UpdateInfoByScroll(ImageInfo imageInfo, Point prevScroll, Point newScroll) {
            UpdateInfo(imageInfo);
            imageInfo.Info.ScrollPosition = prevScroll;
            imageInfo.Info.ScrollDelta = new Point((newScroll.X - prevScroll.X) * Zoom, (newScroll.Y - prevScroll.Y) * Zoom);
        }

        protected void UpdateInfoByZoom(ImageInfo imageInfo, double prevZoom, double newZoom, Point zoomPotin) {
            UpdateInfo(imageInfo);
            imageInfo.Info.ZoomPoint = zoomPotin;
            imageInfo.Info.Zoom = prevZoom;
            imageInfo.Info.NewZoom = newZoom;
        }
        protected virtual void UpdateInfo(ImageInfo imageInfo) {
            imageInfo.Info.ImageSize = imageInfo.ImageSize;
            imageInfo.Info.Screen = imageInfo.Screen;
            imageInfo.Info.Zoom = Zoom;
        }
        protected virtual void CalcLayout(ImageInfo imageInfo) {
            UpdateInfo(imageInfo);
            if(imageInfo.UseDefaultLayout) {
                InternalSetZoom = true;
                if(imageInfo.Info.FitMode == PicturePreviewFitMode.FitToScreen)
                    ScrollZoomHelper.CalcSqueezeBounds(imageInfo.Info);
                else
                    ScrollZoomHelper.CalcZoomOutsideBounds(imageInfo.Info);
                if(imageInfo == ImageInfo) {
                    Zoom = ImageInfo.Info.Zoom;
                }
                InternalSetZoom = false;
            }
            else {
                ScrollZoomHelper.CalcBounds(imageInfo.Info);
            }
            RaisePropertiesChanged();
        }

        public DmFile CurrentFile {
            get { return (DmFile)GetValue(CurrentFileProperty); }
            set { SetValue(CurrentFileProperty, value); }
        }

        public static readonly DependencyProperty CurrentFileProperty =
            DependencyProperty.Register("CurrentFile", typeof(DmFile), typeof(PicturePreviewControl), new PropertyMetadata(null, (d, e) => ((PicturePreviewControl)d).OnFileChanged(e)));

        private void OnFileChanged(DependencyPropertyChangedEventArgs e) {
            ClearValue(CurrentRotateAngleProperty);
            ClearValue(RotateAngleProperty);
            if(!IsSlideShow && e.OldValue != null && CurrentFile != null) {
                ShowLoadingIndicator();
                BackgroundImageLoader.Default.LoadFileImageInBackground(CurrentFile, (d, s) => {
                    CurrentFile.Worker = null;
                    HideLoadingIndicator();
                    Source = (ImageSource)s.Result;
                    OnFileChangedCore(e);
                });
            } else {
                Source = GetImageSource(CurrentFile);
                OnFileChangedCore(e);
            }
        }

        protected virtual void HideLoadingIndicator() {
            if(LoadingIndicator != null)
                LoadingIndicator.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected virtual void ShowLoadingIndicator() {
            if(LoadingIndicator != null)
                LoadingIndicator.Visibility = System.Windows.Visibility.Visible;
        }

        protected virtual void OnFileChangedCore(DependencyPropertyChangedEventArgs e) {
            if(IsSlideShow)
                LoadNextFileInBackground(CurrentFile);
            else {
                ClearPrevFile((DmFile)e.OldValue);
            }
            UpdateFileInfoText();
        }

        protected virtual void ClearPrevFile(DmFile file) {
            if(file != null) {
                file.ImageSource = null;
            }
        }

        private void LoadNextFileInBackground(DmFile file) {
            int index = CurrentFile == null ? 0 : Files.IndexOf(file);
            index++;
            if(index >= Files.Count)
                index = 0;
            if(Files[index].ImageSource == null && !Files[index].LoadingImageSource) {
                LoadFileImageInBackground(index);
            }
        }

        private void UpdateFileInfoText() {
            if(CurrentFile == null) {
                FileInfoText = string.Empty;
                return;
            }
            if(Files == null) {
                FileInfoText = CurrentFile.FileName;
                return;
            }
            FileInfoText = string.Format("{0} - {1} / {2}", CurrentFile.FileName, Files.IndexOf(CurrentFile) + 1, Files.Count);
        }

        protected virtual ImageSource GetImageSource(DmFile file) {
            if(file == null)
                return null;
            if(file.ImageSource != null)
                return (ImageSource)file.ImageSource;
            file.ImageSource = new BitmapImage(GetFileUri(file)) { CacheOption = BitmapCacheOption.OnLoad };
            return (ImageSource)file.ImageSource;
        }

        private Uri GetFileUri(DmFile file) {
            if(System.IO.File.Exists(file.Path))
                return new Uri(file.Path, UriKind.Absolute);
            else
                return new Uri(file.ThumbFileName, UriKind.Absolute);
        }
        public virtual bool ShouldShowInfoPanels { get { return CurrentFile != null; } }

        public List<DmFile> Files {
            get { return (List<DmFile>)GetValue(FilesProperty); }
            set { SetValue(FilesProperty, value); }
        }

        public static readonly DependencyProperty FilesProperty =
            DependencyProperty.Register("Files", typeof(List<DmFile>), typeof(PicturePreviewControl), new PropertyMetadata(null, (d, e) => ((PicturePreviewControl)d).OnFilesChanged(e)));

        private void OnFilesChanged(DependencyPropertyChangedEventArgs e) {
            ImageInfo.PrevSource = null;
            CurrentFile = Files == null || Files.Count == 0 ? null : Files[0];
            RaiseExecuteChangedCore();
        }

        public PreviewExitCommand ExitCommand {
            get { return (PreviewExitCommand)GetValue(ExitCommandProperty); }
            set { SetValue(ExitCommandProperty, value); }
        }

        public static readonly DependencyProperty ExitCommandProperty =
            DependencyProperty.Register("ExitCommand", typeof(PreviewExitCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        public PreviewRotateLeftCommand RotateLetCommand {
            get { return (PreviewRotateLeftCommand)GetValue(RotateLetCommandProperty); }
            set { SetValue(RotateLetCommandProperty, value); }
        }

        public static readonly DependencyProperty RotateLetCommandProperty =
            DependencyProperty.Register("RotateLetCommand", typeof(PreviewRotateLeftCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        public PreviewRotateRightCommand RotateRightCommand {
            get { return (PreviewRotateRightCommand)GetValue(RotateRightCommandProperty); }
            set { SetValue(RotateRightCommandProperty, value); }
        }

        public static readonly DependencyProperty RotateRightCommandProperty =
            DependencyProperty.Register("RotateRightCommand", typeof(PreviewRotateRightCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        public PreviewPrevCommand PrevCommand {
            get { return (PreviewPrevCommand)GetValue(PrevCommandProperty); }
            set { SetValue(PrevCommandProperty, value); }
        }

        public static readonly DependencyProperty PrevCommandProperty =
            DependencyProperty.Register("PrevCommand", typeof(PreviewPrevCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        public PreviewPrevPageCommand PrevPageCommand {
            get { return (PreviewPrevPageCommand)GetValue(PrevPageCommandProperty); }
            set { SetValue(PrevPageCommandProperty, value); }
        }

        public static readonly DependencyProperty PrevPageCommandProperty =
            DependencyProperty.Register("PrevPageCommand", typeof(PreviewPrevPageCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        public PreviewNextCommand NextCommand {
            get { return (PreviewNextCommand)GetValue(NextCommandProperty); }
            set { SetValue(NextCommandProperty, value); }
        }

        public static readonly DependencyProperty NextCommandProperty =
            DependencyProperty.Register("NextCommand", typeof(PreviewNextCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        public PreviewNextPageCommand NextPageCommand {
            get { return (PreviewNextPageCommand)GetValue(NextPageCommandProperty); }
            set { SetValue(NextPageCommandProperty, value); }
        }

        public static readonly DependencyProperty NextPageCommandProperty =
            DependencyProperty.Register("NextPageCommand", typeof(PreviewNextPageCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        public PreviewFirstCommand FirstCommand {
            get { return (PreviewFirstCommand)GetValue(FirstCommandProperty); }
            set { SetValue(FirstCommandProperty, value); }
        }

        public static readonly DependencyProperty FirstCommandProperty =
            DependencyProperty.Register("FirstCommand", typeof(PreviewFirstCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        public PreviewLastCommand LastCommand {
            get { return (PreviewLastCommand)GetValue(LastCommandProperty); }
            set { SetValue(LastCommandProperty, value); }
        }

        public static readonly DependencyProperty LastCommandProperty =
            DependencyProperty.Register("LastCommand", typeof(PreviewLastCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        public PreviewSlideShowCommand SlideShowCommand {
            get { return (PreviewSlideShowCommand)GetValue(SlideShowCommandProperty); }
            set { SetValue(SlideShowCommandProperty, value); }
        }

        public static readonly DependencyProperty SlideShowCommandProperty =
            DependencyProperty.Register("SlideShowCommand", typeof(PreviewSlideShowCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        public DecrementTimeCommand DecrementTimeCommand {
            get { return (DecrementTimeCommand)GetValue(DecrementTimeCommandProperty); }
            set { SetValue(DecrementTimeCommandProperty, value); }
        }

        public static readonly DependencyProperty DecrementTimeCommandProperty =
            DependencyProperty.Register("DecrementTimeCommand", typeof(DecrementTimeCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        public IncrementTimeCommand IncrementTimeCommand {
            get { return (IncrementTimeCommand)GetValue(IncrementTimeCommandProperty); }
            set { SetValue(IncrementTimeCommandProperty, value); }
        }

        public static readonly DependencyProperty IncrementTimeCommandProperty =
            DependencyProperty.Register("IncrementTimeCommand", typeof(IncrementTimeCommand), typeof(PicturePreviewControl), new PropertyMetadata(null));

        event EventHandler close;
        public event EventHandler Close {
            add { close += value; }
            remove { close -= value; }
        }

        internal void RaiseClose() {
            if(close != null)
                close(this, EventArgs.Empty);
        }

        public double RotateAngle {
            get { return (double)GetValue(RotateAngleProperty); }
            set { SetValue(RotateAngleProperty, value); }
        }

        public static readonly DependencyProperty RotateAngleProperty =
            DependencyProperty.Register("RotateAngle", typeof(double), typeof(PicturePreviewControl), new PropertyMetadata(0.0, (d, e) => ((PicturePreviewControl)d).OnRotateAngleChanged(e)));

        public double CurrentRotateAngle {
            get { return (double)GetValue(CurrentRotateAngleProperty); }
            set { SetValue(CurrentRotateAngleProperty, value); }
        }

        public static readonly DependencyProperty CurrentRotateAngleProperty =
            DependencyProperty.Register("CurrentRotateAngle", typeof(double), typeof(PicturePreviewControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        protected bool SuppressRotateAnimation { get; set; }
        private void OnRotateAngleChanged(DependencyPropertyChangedEventArgs e) {
            if(CurrentRotateAngle == RotateAngle || SuppressRotateAnimation)
                return;
            RunRotateAnimation();
        }

        Storyboard RotateStoryboard { get; set; }
        private void RunRotateAnimation() {
            RotateStoryboard = new Storyboard();
            RotateStoryboard.Completed += RotateStoryboard_Completed;
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = CurrentRotateAngle;
            anim.To = RotateAngle;
            anim.EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut };
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            Storyboard.SetTarget(anim, this);
            Storyboard.SetTargetProperty(anim, new PropertyPath(CurrentRotateAngleProperty));
            RotateStoryboard.Children.Add(anim);
            RotateStoryboard.Begin();
        }

        private void RotateStoryboard_Completed(object sender, EventArgs e) {
            //TransformGroup group = new TransformGroup();
            //group.Children.Add(new RotateTransform(RotateAngle, 0, 0));
            //TransformedBitmap bmp = new TransformedBitmap((BitmapSource)Source, group);
            //Source = bmp;
            //try {
            //    SuppressRotateAnimation = true;
            //    CurrentRotateAngle = 0;
            //    RotateAngle = 0;
            //}
            //finally {
            //    SuppressRotateAnimation = false;
            //}
        }

        public bool AllowAnimation {
            get { return (bool)GetValue(AllowAnimationProperty); }
            set { SetValue(AllowAnimationProperty, value); }
        }

        public static readonly DependencyProperty AllowAnimationProperty =
            DependencyProperty.Register("AllowAnimation", typeof(bool), typeof(PicturePreviewControl), new PropertyMetadata(false));

        public bool IsSlideShow {
            get { return (bool)GetValue(IsSlideShowProperty); }
            set { SetValue(IsSlideShowProperty, value); }
        }

        double IPictureNavigatorClient.Zoom {
            get { return Zoom; }
            set { Zoom = value; }
        }

        void IPictureNavigatorClient.ZoomFit() {
            FitToScreen(true, PicturePreviewFitMode.FitToScreen);
        }

        void IPictureNavigatorClient.ZoomFill() {
            FitToScreen(true, PicturePreviewFitMode.FillToScreen);
        }

        bool IPictureNavigatorClient.AllowScrollAnimation { get; set; }

        System.Drawing.PointF IPictureNavigatorClient.ScrollPosition {
            get { return new System.Drawing.PointF((float)ImageInfo.Info.ScrollPosition.X, (float)ImageInfo.Info.ScrollPosition.Y); }
            set {
                if(((IPictureNavigatorClient)this).AllowScrollAnimation)
                    SetScrollAnimatedCore(ImageInfo, new Point(value.X, value.Y));
                else
                    SetScrollCore(ImageInfo, new Point(value.X, value.Y));
            }
        }

        System.Drawing.SizeF IPictureNavigatorClient.ScreenSize {
            get { return new System.Drawing.SizeF((float)ImageInfo.Screen.Width, (float)ImageInfo.Screen.Height); }
        }

        System.Drawing.SizeF IPictureNavigatorClient.ImageSize {
            get { return new System.Drawing.SizeF((float)ImageInfo.ImageSize.Width, (float)ImageInfo.ImageSize.Height); }
        }

        Rect IGridManagerOwner.Bounds {
            get {
                return ImageInfo.Info.ScreenBounds;
            }
        }

        Rect IGridManagerOwner.Screen {
            get {
                return ImageInfo.Screen;
            }
        }

        public static readonly DependencyProperty IsSlideShowProperty =
            DependencyProperty.Register("IsSlideShow", typeof(bool), typeof(PicturePreviewControl), new PropertyMetadata(false, (d, e) => ((PicturePreviewControl)d).OnIsSlideShowChanged(e)));

        private void OnIsSlideShowChanged(DependencyPropertyChangedEventArgs e) {
            if(IsSlideShow)
                StartSlideShow();
            else
                StopSlideShow();
        }

        private void StopSlideShow() {
            AllowAnimation = false;
            SlideShowTimer.Stop();
            ImageInfo.InContentChangeAnimation = false;
            InvalidateVisual();
        }

        private void StartSlideShow() {
            int index = CurrentFile == null ? 0 : Files.IndexOf(CurrentFile);
            ImageInfo.PrevSource = null;
            ImageInfo.PrevContentInfo = null;
            CurrentFile = null;
            AllowAnimation = true;
            GetImageSource(Files[index]);
            CurrentFile = Files[index];
            SlideShowTimer.Start();
        }

        int GetNextFileIndex(int index) {
            index++;
            if(index >= Files.Count)
                index = 0;
            return index;
        }
        private void LoadFilesImages(int index, int count) {
            for(int i = 0; i < count; i++) {
                GetImageSource(Files[index]);
                index = GetNextFileIndex(index);
            }
        }
        private void LoadFileImageInBackground(int index) {
            BackgroundImageLoader.Default.LoadFileImageInBackground(Files[index],
                (s, args) => {
                    if(args.Error == null) {
                        Files[index].ImageSource = args.Result as BitmapImage;
                    }
                });
        }

        event EventHandler toggleFullScreen;
        public event EventHandler ToggleFullScreen {
            add { toggleFullScreen += value; }
            remove { toggleFullScreen -= value; }
        }

        internal void RaiseToggleFullScreen() {
            if(toggleFullScreen != null)
                toggleFullScreen.Invoke(this, EventArgs.Empty);
        }

        event EventHandler exitFullScreen;
        public event EventHandler ExitFullScreen {
            add { exitFullScreen += value; }
            remove { exitFullScreen -= value; }
        }

        event EventHandler propertiesChanged;
        event EventHandler IPictureNavigatorClient.PropertiesChanged {
            add {
                propertiesChanged += value;
            }

            remove {
                propertiesChanged -= value;
            }
        }

        protected void RaisePropertiesChanged() {
            if(propertiesChanged != null)
                propertiesChanged(this, EventArgs.Empty);
        }

        internal void RaiseExitFullScreen() {
            if(exitFullScreen != null)
                exitFullScreen.Invoke(this, EventArgs.Empty);
        }

        internal void ToggleFullScreenCore() {
            RaiseToggleFullScreen();
        }

        internal void ExitFullScreenCore() {
            RaiseExitFullScreen();
        }

        internal void RaiseExecuteChangedCore() {
            if(FirstCommand != null)
                FirstCommand.RaiseExecuteChangedCore();
            if(PrevPageCommand != null)
                PrevPageCommand.RaiseExecuteChangedCore();
            if(PrevCommand != null)
                PrevCommand.RaiseExecuteChangedCore();
            if(NextCommand != null)
                NextCommand.RaiseExecuteChangedCore();
            if(NextPageCommand != null)
                NextPageCommand.RaiseExecuteChangedCore();
            if(LastCommand != null)
                LastCommand.RaiseExecuteChangedCore();
        }

        void IGridManagerOwner.InvalidateVisual() {
            InvalidateVisual();
        }
    }

    public class ScrollZoomInfo {
        public ScrollZoomInfo() {
            Zoom = 1.0;
            ClearLastMovePoint();
        }

        public PicturePreviewFitMode FitMode { get; set; }
        public Point RotateOrigin { get; set; }
        public bool UseDefaultRotateOrigin { get; set; }
        public Point LastMovePoint { get; set; }
        public Point ScrollDelta { get; set; }
        public double Zoom { get; set; }
        public double NewZoom { get; set; }
        public Rect PrevScreenBounds { get; set; }
        public bool Animating { get; set; }
        public Rect AnimatedScreenBounds { get; set; }
        public Rect ScreenBounds { get; set; }
        public Rect Screen { get; set; }
        public Size ImageSize { get; set; }
        public Point ScrollPosition { get; set; }
        public Point NewScrollPosition { get; set; }
        public Point PrevScrollPosition { get; set; }
        public Point ZoomPoint { get; set; }
        public double RotateAngle { get; set; }

        public void ClearLastMovePoint() {
            LastMovePoint = new Point(-1, -1);
        }
        public bool IsLastMovePointEmpty {
            get { return LastMovePoint.X == -1 && LastMovePoint.Y == -1; }
        }
    }

    public class ContentChangeAnimationInfo : DependencyObject {

        public ContentChangeAnimationInfo(PicturePreviewControl preview) {
            Preview = preview;
            FadeAnimationDuration = 1.0;
            AnimationBehavior = FillBehavior.HoldEnd;
        }

        public PicturePreviewControl Preview { get; set; }
        public Rect StartRect { get; set; }
        public Rect EndRect { get; set; }
        public double StartOpacity { get; set; }
        public double EndOpacity { get; set; }
        public ImageSource Source { get; set; }
        public Double Duration { get; set; }
        public bool IsPrev { get; set; }
        public PicturePreviewEffectType Effect { get; set; }

        public Rect CurrentRect {
            get { return (Rect)GetValue(CurrentRectProperty); }
            set { SetValue(CurrentRectProperty, value); }
        }

        public static readonly DependencyProperty CurrentRectProperty =
            DependencyProperty.Register("CurrentRect", typeof(Rect), typeof(ContentChangeAnimationInfo), new PropertyMetadata(Rect.Empty, (d, e) => ((ContentChangeAnimationInfo)d).OnRectChanged(e)));

        private void OnRectChanged(DependencyPropertyChangedEventArgs e) {
            if(Preview != null)
                Preview.InvalidateVisual();
        }

        public double CurrentOpacity {
            get { return (double)GetValue(CurrentOpacityProperty); }
            set { SetValue(CurrentOpacityProperty, value); }
        }

        public ImageInfo ImageInfo { get; set; }

        public static readonly DependencyProperty CurrentOpacityProperty =
            DependencyProperty.Register("CurrentOpacity", typeof(double), typeof(ContentChangeAnimationInfo), new PropertyMetadata(0.0, (d, e) => ((ContentChangeAnimationInfo)d).OnOpacityChanged(e)));

        private void OnOpacityChanged(DependencyPropertyChangedEventArgs e) {
            if(Preview != null)
                Preview.InvalidateVisual();
        }

        public void InitializeStoryboard(ImageInfo info, ImageSource source, PicturePreviewEffectType effect, double duration, bool prev) {
            ImageInfo = info;
            Source = source;
            Duration = duration;
            IsPrev = prev;
            Effect = effect;

            InitializeStoryboardCore();

            if(effect == PicturePreviewEffectType.None)
                InitializeStoryboard_None();
            if(effect == PicturePreviewEffectType.PanAndZoom)
                InitializeStoryboard_PanAndZoom();
            if(effect == PicturePreviewEffectType.Fade)
                InitializeStoryboard_Fade();
            if(effect == PicturePreviewEffectType.Wipe)
                InitializeStoryboard_Wipe();
            if(effect == PicturePreviewEffectType.Circle)
                InitializeStoryboard_Wipe();
            if(effect == PicturePreviewEffectType.Slide)
                InitializeStoryboard_Slide();

            CurrentRect = StartRect;
            CurrentOpacity = StartOpacity;
        }

        private void InitializeStoryboard_Slide() {
            if(Source == null)
                return;
            StartOpacity = IsPrev ? 1.0 : 0.0;
            EndOpacity = IsPrev ? 0.0 : 1.0;

            Rect rect = CenterSizeInRect(ClientRect, FeatSizeInRect(ClientRect, ImageSize));
            Rect scaledRect = ScaleRect(rect, 0.7);
            scaledRect.Y = (ClientRect.Height - scaledRect.Height) / 2;
            Rect right = scaledRect;
            Rect left = scaledRect;
            right.X = rect.Right;
            left.X = rect.X - scaledRect.Width;

            if(Preview.Direction == PicturePreviewSlideDirection.Forward) {
                StartRect = IsPrev ? rect : right;
                EndRect = IsPrev ? left : rect;
            } else {
                StartRect = IsPrev ? rect : left;
                EndRect = IsPrev ? right : rect;
            }

            OpacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            RectAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            OpacityAnimation.From = StartOpacity;
            OpacityAnimation.To = EndOpacity;
            RectAnimation.From = StartRect;
            RectAnimation.To = EndRect;
            OpacityAnimation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
            RectAnimation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
        }

        private void InitializeStoryboard_Wipe() {
            if(Source == null)
                return;
            Rect rect = CenterSizeInRect(ClientRect, FeatSizeInRect(ClientRect, ImageSize));
            StartRect = rect;
            EndRect = rect;

            Duration dur = new Duration(TimeSpan.FromSeconds(0.5));
            ProgressAnimationSpline.Duration = dur;
            OpacityAnimationSpline.Duration = dur;
            RectAnimationSpline.Duration = dur;
            OpacityAnimationSpline.KeyFrames.Add(new SplineDoubleKeyFrame(StartOpacity, KeyTime.FromPercent(0)));
            OpacityAnimationSpline.KeyFrames.Add(new SplineDoubleKeyFrame(EndOpacity, KeyTime.FromPercent(1.0)));

            RectAnimationSpline.KeyFrames.Add(new SplineRectKeyFrame(StartRect, KeyTime.FromPercent(0)));
            RectAnimationSpline.KeyFrames.Add(new SplineRectKeyFrame(EndRect, KeyTime.FromPercent(1)));

            ProgressAnimationSpline.KeyFrames.Add(new SplineDoubleKeyFrame(0.0, KeyTime.FromPercent(0)));
            ProgressAnimationSpline.KeyFrames.Add(new SplineDoubleKeyFrame(1.0, KeyTime.FromPercent(1.0)));
        }

        public double FadeAnimationDuration {
            get;
            set;
        }

        private void InitializeStoryboard_Fade() {
            if(Source == null)
                return;
            StartOpacity = IsPrev ? 1.0 : 0.0;
            EndOpacity = IsPrev ? 0.0 : 1.0;
            Rect rect = CenterSizeInRect(ClientRect, FeatSizeInRect(ClientRect, ImageSize));
            StartRect = rect;
            EndRect = rect;

            OpacityAnimationSpline.Duration = new Duration(TimeSpan.FromSeconds(FadeAnimationDuration));
            RectAnimationSpline.Duration = new Duration(TimeSpan.FromSeconds(FadeAnimationDuration));
            OpacityAnimationSpline.KeyFrames.Add(new SplineDoubleKeyFrame(StartOpacity, KeyTime.FromPercent(0)));
            OpacityAnimationSpline.KeyFrames.Add(new SplineDoubleKeyFrame(EndOpacity, KeyTime.FromPercent(1)));

            RectAnimationSpline.KeyFrames.Add(new SplineRectKeyFrame(StartRect, KeyTime.FromPercent(0)));
            RectAnimationSpline.KeyFrames.Add(new SplineRectKeyFrame(EndRect, KeyTime.FromPercent(1)));
        }

        double ZoomDelta {
            get {
                if(Source == null) return 0.05;
                return 20 / Math.Max(Source.Width, Source.Height);
            }
        }

        private void InitializeStoryboard_PanAndZoom() {
            if(IsPrev)
                return;
            StartOpacity = 0.0;
            EndOpacity = 1.0;
            Rect rect = CenterSizeInRect(ClientRect, FeatSizeInRect(ClientRect, ImageSize));
            double startScale = 1.0 + (Random.NextDouble() * 2 * ZoomDelta - ZoomDelta);
            double endScale = 1.0 + (Random.NextDouble() * 2 * ZoomDelta - ZoomDelta);

            StartRect = ScaleRect(rect, startScale);
            EndRect = ScaleRect(rect, endScale);
            Vector dir = CreateDirection();
            StartRect = MoveRect(StartRect, dir, GetShift(-10, -10));
            EndRect = MoveRect(EndRect, dir, GetShift(10, 10));

            OpacityAnimationSpline.Duration = new Duration(TimeSpan.FromSeconds(Duration + 0.0));
            RectAnimationSpline.Duration = new Duration(TimeSpan.FromSeconds(Duration + 0.0));
            OpacityAnimationSpline.KeyFrames.Add(new SplineDoubleKeyFrame(0.0, KeyTime.FromPercent(0)));
            OpacityAnimationSpline.KeyFrames.Add(new SplineDoubleKeyFrame(1.0, KeyTime.FromPercent(0.2)));
            OpacityAnimationSpline.KeyFrames.Add(new SplineDoubleKeyFrame(1.0, KeyTime.FromPercent(0.8)));
            OpacityAnimationSpline.KeyFrames.Add(new SplineDoubleKeyFrame(0.0, KeyTime.FromPercent(1.0)));

            RectAnimationSpline.KeyFrames.Add(new SplineRectKeyFrame(StartRect, KeyTime.FromPercent(0)));
            RectAnimationSpline.KeyFrames.Add(new SplineRectKeyFrame(EndRect, KeyTime.FromPercent(1)));
        }

        protected double GetShift(double start, double delta) {
            return start + delta * Random.NextDouble();
        }

        private Rect MoveRect(Rect rect, Vector dir, double scale) {
            rect.X += dir.X * scale;
            rect.Y += dir.Y * scale;
            return rect;
        }

        static Random random;
        public static Random Random {
            get {
                if(random == null)
                    random = new Random();
                return random;
            }
        }
        private Vector CreateDirection() {
            double angle = Random.NextDouble() * 2.0 * Math.PI;
            Vector res = new Vector(Math.Cos(angle), Math.Sin(angle));
            return res;
        }

        private Rect ScaleRect(Rect rect, double scale) {
            return new Rect(rect.X, rect.Y, rect.Width * scale, rect.Height * scale);
        }



        public double Progress {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(ContentChangeAnimationInfo), new PropertyMetadata(0.0, (d, e) => ((ContentChangeAnimationInfo)d).OnProgressChanged(e)));

        private void OnProgressChanged(DependencyPropertyChangedEventArgs e) {
            if(Preview != null)
                Preview.InvalidateVisual();
        }

        DoubleAnimationUsingKeyFrames OpacityAnimationSpline { get { return (DoubleAnimationUsingKeyFrames)Storyboard.Children[0]; } }
        RectAnimationUsingKeyFrames RectAnimationSpline { get { return (RectAnimationUsingKeyFrames)Storyboard.Children[1]; } }
        DoubleAnimationUsingKeyFrames ProgressAnimationSpline { get { return (DoubleAnimationUsingKeyFrames)Storyboard.Children[2]; } }

        DoubleAnimation OpacityAnimation { get { return (DoubleAnimation)Storyboard.Children[0]; } }
        RectAnimation RectAnimation { get { return (RectAnimation)Storyboard.Children[1]; } }
        DoubleAnimation ProgressAnimation { get { return (DoubleAnimation)Storyboard.Children[2]; } }

        private void InitializeStoryboardCore() {
            if(Storyboard != null) {
                Storyboard.Stop();
                Storyboard.Completed -= Storyboard_Completed;
            }
            Storyboard = new Storyboard();
            Storyboard.Completed += Storyboard_Completed;

            DoubleAnimationBase opacity = CreateOpacityAnimation();
            RectAnimationBase rect = CreateRectAnimation();
            DoubleAnimationBase progress = CreateProgressAnimation();

            opacity.FillBehavior = FillBehavior.HoldEnd;
            rect.FillBehavior = FillBehavior.HoldEnd;
            progress.FillBehavior = FillBehavior.HoldEnd;

            Storyboard.SetTarget(opacity, this);
            Storyboard.SetTarget(rect, this);
            Storyboard.SetTarget(progress, this);

            Storyboard.SetTargetProperty(opacity, new PropertyPath(CurrentOpacityProperty));
            Storyboard.SetTargetProperty(rect, new PropertyPath(CurrentRectProperty));
            Storyboard.SetTargetProperty(progress, new PropertyPath(ProgressProperty));

            Storyboard.Children.Add(opacity);
            Storyboard.Children.Add(rect);
            Storyboard.Children.Add(progress);
        }

        private DoubleAnimationBase CreateProgressAnimation() {
            return new DoubleAnimationUsingKeyFrames();
        }

        private RectAnimationBase CreateRectAnimation() {
            if(Effect == PicturePreviewEffectType.Slide)
                return new RectAnimation();
            return new RectAnimationUsingKeyFrames();
        }

        private DoubleAnimationBase CreateOpacityAnimation() {
            if(Effect == PicturePreviewEffectType.Slide)
                return new DoubleAnimation();
            return new DoubleAnimationUsingKeyFrames();
        }

        event EventHandler completed;
        public event EventHandler Completed {
            add { completed += value; }
            remove { completed -= value; }
        }

        void RaiseCompleted() {
            if(completed != null)
                completed(this, EventArgs.Empty);
        }
        public FillBehavior AnimationBehavior { get; set; }
        void Storyboard_Completed(object sender, EventArgs e) {
            InProgress = false;
            if(AnimationBehavior == FillBehavior.Stop)
                ImageInfo.InContentChangeAnimation = false;
            RaiseCompleted();
            Preview.InvalidateVisual();
        }

        Rect ClientRect { get { return ImageInfo.Screen; } }

        protected Size ImageSize {
            get {
                BitmapImage img = Source as BitmapImage;
                if(img != null)
                    return new Size(img.PixelWidth, img.PixelHeight);
                return new Size(Source.Width, Source.Height);
            }
        }

        private void InitializeStoryboard_None() {
            if(Source == null)
                return;

            StartOpacity = 1.0;
            EndOpacity = IsPrev ? 0.0 : 1.0;
            StartRect = CenterSizeInRect(ClientRect, FeatSizeInRect(ClientRect, ImageSize));
            EndRect = StartRect;

            OpacityAnimationSpline.Duration = new Duration(TimeSpan.FromMilliseconds(0));
            OpacityAnimationSpline.KeyFrames.Add(new DiscreteDoubleKeyFrame(StartOpacity, KeyTime.FromPercent(0)));
            OpacityAnimationSpline.KeyFrames.Add(new DiscreteDoubleKeyFrame(EndOpacity, KeyTime.FromPercent(1)));

            RectAnimationSpline.Duration = new Duration(TimeSpan.FromSeconds(0));
            RectAnimationSpline.KeyFrames.Add(new DiscreteRectKeyFrame(StartRect, KeyTime.FromPercent(0)));
            RectAnimationSpline.KeyFrames.Add(new DiscreteRectKeyFrame(EndRect, KeyTime.FromPercent(1)));
        }

        public Storyboard Storyboard { get; private set; }

        Size FeatSizeInRect(Rect rect, Size size) {
            double kx = size.Width / rect.Width;
            double ky = size.Height / rect.Height;

            double k = Math.Max(kx, ky);
            if(k < 1) k = 1;

            return new Size(size.Width / k, size.Height / k);
        }

        Rect CenterSizeInRect(Rect rect, Size size) {
            return new Rect(rect.X + (rect.Width - size.Width) / 2, rect.Y + (rect.Height - size.Height) / 2, size.Width, size.Height);
        }

        public void Start() {
            if(IsPrev && Effect == PicturePreviewEffectType.PanAndZoom)
                return;
            if(!InProgress) {
                InProgress = true;
                Storyboard.Begin();
            }
        }

        bool inProgress;
        public bool InProgress {
            get { return inProgress; }
            set { inProgress = value; }
        }

        GradientBrush opacityMask;
        protected GradientBrush OpacityMask {
            get {
                if(opacityMask == null) {
                    if(Effect == PicturePreviewEffectType.Wipe) {
                        opacityMask = new LinearGradientBrush() { EndPoint = new Point(1, 0), StartPoint = new Point(0, 0) };
                    } else if(Effect == PicturePreviewEffectType.Circle) {
                        opacityMask = new RadialGradientBrush() { GradientOrigin = new Point(0.5, 0.5) };
                    }
                    opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(0xff, 0, 0, 0), 0.0));
                    opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(0xff, 0, 0, 0), 0.1));
                    opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(0xff, 0, 0, 0), 0.2));
                    opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(0xff, 0, 0, 0), 1.0));
                }
                return opacityMask;
            }
        }
        double OpacityMaskWidth {
            get { return 25 / ClientRect.Width; }
        }
        public void Draw(DrawingContext drawingContext) {
            if(Source == null)
                return;
            if(IsPrev && Effect == PicturePreviewEffectType.PanAndZoom)
                return;
            if(Effect == PicturePreviewEffectType.Wipe ||
                Effect == PicturePreviewEffectType.Circle) {
                if(IsPrev) {
                    OpacityMask.GradientStops[0].Color = Color.FromArgb(0xff, 0, 0, 0);
                    OpacityMask.GradientStops[0].Offset = 0;
                    OpacityMask.GradientStops[1].Color = Color.FromArgb(0xff, 0, 0, 0);
                    OpacityMask.GradientStops[1].Offset = 1.0 - Progress - OpacityMaskWidth;
                    OpacityMask.GradientStops[2].Color = Color.FromArgb(0, 0, 0, 0);
                    OpacityMask.GradientStops[2].Offset = 1.0 - Progress;
                    OpacityMask.GradientStops[3].Color = Color.FromArgb(0, 0, 0, 0);
                    OpacityMask.GradientStops[3].Offset = 1.0;
                } else {
                    OpacityMask.GradientStops[0].Color = Color.FromArgb(0, 0, 0, 0);
                    OpacityMask.GradientStops[0].Offset = 0;
                    OpacityMask.GradientStops[1].Color = Color.FromArgb(0, 0, 0, 0);
                    OpacityMask.GradientStops[1].Offset = 1.0 - Progress - OpacityMaskWidth;
                    OpacityMask.GradientStops[2].Color = Color.FromArgb(0xff, 0, 0, 0);
                    OpacityMask.GradientStops[2].Offset = 1.0 - Progress;
                    OpacityMask.GradientStops[3].Color = Color.FromArgb(0xff, 0, 0, 0);
                    OpacityMask.GradientStops[3].Offset = 1.0;
                }
                drawingContext.PushOpacityMask(OpacityMask);
                drawingContext.DrawImage(Source, CurrentRect);
                drawingContext.Pop();
            } else {
                drawingContext.PushOpacity(CurrentOpacity);
                drawingContext.DrawImage(Source, CurrentRect);
                drawingContext.Pop();
            }
        }
    }

    public enum PicturePreviewFitMode { FitToScreen, FillToScreen }
    public class ImageInfo {
        public ImageInfo(PicturePreviewControl preview) {
            Preview = preview;
            Info = new ScrollZoomInfo();
        }

        public PicturePreviewControl Preview { get; private set; }
        public ImageSource Source { get; set; }
        public ImageSource PrevSource { get; set; }
        public bool InContentChangeAnimation { get; set; }
        public ScrollZoomInfo Info { get; set; }
        public ScrollZoomInfo PrevInfo { get; set; }
        public ContentChangeAnimationInfo PrevContentInfo { get; set; }
        public ContentChangeAnimationInfo NextContentInfo { get; set; }
        public bool UseDefaultLayout { get; set; }
        public Size ImageSize {
            get {
                if(Source == null)
                    return new Size(0, 0);
                BitmapImage img = Source as BitmapImage;
                if(img != null)
                    return new Size(img.PixelWidth, img.PixelHeight);
                return new Size(Source.Width, Source.Height);
            }
        }
        public virtual Rect Screen {
            get { return new Rect(Preview.ContentPadding, Preview.ContentPadding, Math.Max(0, Preview.ActualWidth - Preview.ContentPadding * 2), Math.Max(0, Preview.ActualHeight - Preview.ContentPadding * 2)); }
        }
        public virtual Rect ClipRect {
            get { return new Rect(0, 0, Preview.ActualWidth, Preview.ActualHeight); }
        }

        public bool SuppressCalcLayout { get; internal set; }

        public Point ScreenToLocal(Point pt) {
            return new Point(pt.X - Screen.X, pt.Y - Screen.Y);
        }
    }

    public static class ScrollZoomHelper {
        public static void CalcZoomOutsideBounds(ScrollZoomInfo info) {
            double kx = info.Screen.Width / info.ImageSize.Width;
            double ky = info.Screen.Height / info.ImageSize.Height;

            double k = Math.Max(kx, ky);
            double width = k * info.ImageSize.Width;
            double height = k * info.ImageSize.Height;

            info.ScreenBounds = new Rect(info.Screen.X + (info.Screen.Width - width) / 2, info.Screen.Y + (info.Screen.Height - height) / 2, width, height);
            info.Zoom = k;
            info.ScrollPosition = new Point(0, 0);
            if(info.UseDefaultRotateOrigin)
                info.RotateOrigin = new Point(info.Screen.X + info.Screen.Width / 2, info.Screen.Y + info.Screen.Height / 2);
        }
        public static double CalcZoomOutZoom(ScrollZoomInfo info) {
            double kx = info.Screen.Width / info.ImageSize.Width;
            double ky = info.Screen.Height / info.ImageSize.Height;

            return Math.Max(kx, ky);
        }
        public static double CalcSqueezeZoom(ScrollZoomInfo info) {
            double kx = info.Screen.Width / info.ImageSize.Width;
            double ky = info.Screen.Height / info.ImageSize.Height;

            return Math.Min(kx, ky);
        }
        public static void CalcSqueezeBounds(ScrollZoomInfo info) {
            double kx = info.Screen.Width / info.ImageSize.Width;
            double ky = info.Screen.Height / info.ImageSize.Height;

            double k = Math.Min(kx, ky);
            double width = k * info.ImageSize.Width;
            double height = k * info.ImageSize.Height;

            info.ScreenBounds = new Rect(info.Screen.X + (info.Screen.Width - width) / 2, info.Screen.Y + (info.Screen.Height - height) / 2, width, height);
            info.Zoom = k;
            info.ScrollPosition = new Point(0, 0);
            if(info.UseDefaultRotateOrigin)
                info.RotateOrigin = new Point(info.Screen.X + info.Screen.Width / 2, info.Screen.Y + info.Screen.Height / 2);
        }

        public static void CalcBounds(ScrollZoomInfo info) {
            double viewPortWidth = info.Screen.Width / info.Zoom;
            double viewPortHeight = info.Screen.Height / info.Zoom;

            if(viewPortWidth >= info.ImageSize.Width && viewPortHeight >= info.ImageSize.Height) {
                info.ScrollPosition = new Point(0.0, 0.0);
            }
            else {
                info.ScrollPosition = new Point(
                Math.Max(-viewPortWidth / 2, Math.Min(info.ScrollPosition.X, info.ImageSize.Width - viewPortWidth / 2)),
                Math.Max(-viewPortHeight / 2, Math.Min(info.ScrollPosition.Y, info.ImageSize.Height - viewPortHeight / 2)));
            }

            Rect rect = new Rect(info.Screen.X - info.ScrollPosition.X * info.Zoom, info.Screen.Y - info.ScrollPosition.Y * info.Zoom, info.ImageSize.Width * info.Zoom, info.ImageSize.Height * info.Zoom);
            if(rect.Width < info.Screen.Width)
                rect.X = info.Screen.X + (info.Screen.Width - rect.Width) / 2;
            if(rect.Height < info.Screen.Height)
                rect.Y = info.Screen.Y + (info.Screen.Height - rect.Height) / 2;
            info.ScreenBounds = rect;
            if(info.UseDefaultRotateOrigin)
                info.RotateOrigin = new Point(info.Screen.X + info.Screen.Width / 2, info.Screen.Y + info.Screen.Height / 2);
        }

        public static void Zoom(ScrollZoomInfo info) {
            double x = info.ZoomPoint.X / info.Zoom + info.ScrollPosition.X;
            double y = info.ZoomPoint.Y / info.Zoom + info.ScrollPosition.Y;

            info.ScrollPosition = new Point(x - info.ZoomPoint.X / info.NewZoom, y - info.ZoomPoint.Y / info.NewZoom);
            info.Zoom = info.NewZoom;
            CalcBounds(info);
        }

        public static void Scroll(ScrollZoomInfo info) {
            double viewPortWidth = info.Screen.Width / info.Zoom;
            double viewPortHeight = info.Screen.Height / info.Zoom;

            Point localScroll = CalcLocalScrollDelta(info.ScrollDelta, info.RotateAngle);

            if(viewPortWidth >= info.ImageSize.Width && viewPortHeight >= info.ImageSize.Height) {
                info.ScrollPosition = new Point(0.0, 0.0);
            }
            else {
                info.ScrollPosition = new Point(
                    Math.Max(-viewPortWidth / 2, Math.Min(info.ScrollPosition.X + localScroll.X / info.Zoom, info.ImageSize.Width - viewPortWidth / 2)),
                    Math.Max(-viewPortHeight / 2, Math.Min(info.ScrollPosition.Y + localScroll.Y / info.Zoom, info.ImageSize.Height - viewPortHeight / 2)));
            }
            CalcBounds(info);
        }

        private static Point CalcLocalScrollDelta(Point scrollDelta, double rotateAngle) {
            int angle = (int)rotateAngle;
            angle = angle % 360;

            if(angle == 0 || angle == 360)
                return scrollDelta;
            if(angle == 90 || angle == -270)
                return new Point(scrollDelta.Y, -scrollDelta.X);
            if(angle == -90 || angle == 270)
                return new Point(-scrollDelta.Y, scrollDelta.X);
            if(angle == 180 || angle == -180)
                return new Point(-scrollDelta.X, -scrollDelta.Y);
            return scrollDelta;
        }

        public static void CoerceScrollPosition(ScrollZoomInfo info) {
            double viewPortWidth = info.Screen.Width / info.Zoom;
            double viewPortHeight = info.Screen.Height / info.Zoom;

            info.ScrollPosition = new Point(
                Math.Max(0.0, Math.Min(info.ScrollPosition.X, info.ImageSize.Width - viewPortWidth)),
                Math.Max(0.0, Math.Min(info.ScrollPosition.Y, info.ImageSize.Height - viewPortHeight)));
        }
    }

    public class PicturePreviewCommand : ICommand {
        public PicturePreviewCommand(PicturePreviewControl preview) {
            Preview = preview;
        }

        public PicturePreviewControl Preview { get; private set; }

        bool ICommand.CanExecute(object parameter) {
            return CanExecureCore();
        }

        protected virtual bool CanExecureCore() {
            if(Preview.ImageInfo.InContentChangeAnimation)
                return false;
            return true;
        }

        event EventHandler canExecutedChanged;
        event EventHandler ICommand.CanExecuteChanged {
            add { canExecutedChanged += value; }
            remove { canExecutedChanged -= value; }
        }

        void ICommand.Execute(object parameter) {
            ExecuteCore();
        }

        protected virtual void ExecuteCore() {

        }

        public bool AllowCommand {
            get { return Preview.CurrentFile != null && Preview.Files != null && Preview.Files.Count > 0 && !Preview.IsSlideShow; }
        }

        protected void RaiseExecuteChanged() {
            Preview.RaiseExecuteChangedCore();
        }

        protected internal void RaiseExecuteChangedCore() {
            if(canExecutedChanged != null)
                canExecutedChanged(this, EventArgs.Empty);
        }
    }

    public class PreviewExitCommand : PicturePreviewCommand {
        public PreviewExitCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            return true;
        }
        protected override void ExecuteCore() {
            Preview.RaiseClose();
        }
    }

    public class PreviewRotateLeftCommand : PicturePreviewCommand {
        public PreviewRotateLeftCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            return AllowCommand;
        }
        protected override void ExecuteCore() {
            Preview.RotateAngle -= 90;
        }
    }

    public class PreviewRotateRightCommand : PicturePreviewCommand {
        public PreviewRotateRightCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            return AllowCommand;
        }
        protected override void ExecuteCore() {
            Preview.RotateAngle += 90;
        }
    }

    public class PreviewPrevCommand : PicturePreviewCommand {
        public PreviewPrevCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            if(!AllowCommand)
                return false;
            return Preview.Files.IndexOf(Preview.CurrentFile) > 0;
        }
        protected override void ExecuteCore() {
            if(!AllowCommand)
                return;
            Preview.Direction = PicturePreviewSlideDirection.Backward;
            int index = Preview.Files.IndexOf(Preview.CurrentFile);
            Preview.CurrentFile = Preview.Files[index - 1];
            RaiseExecuteChanged();
        }
    }

    public class PreviewPrevPageCommand : PicturePreviewCommand {
        public PreviewPrevPageCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            if(!AllowCommand)
                return false;
            return Preview.Files.IndexOf(Preview.CurrentFile) > 0;
        }
        protected override void ExecuteCore() {
            if(!AllowCommand)
                return;
            Preview.Direction = PicturePreviewSlideDirection.Backward;
            int index = Preview.Files.IndexOf(Preview.CurrentFile);
            int delta = Math.Max(2, (int)(Preview.Files.Count * 0.05));
            index = Math.Max(0, index - delta);
            Preview.CurrentFile = Preview.Files[index];
            RaiseExecuteChanged();
        }
    }

    public class PreviewNextCommand : PicturePreviewCommand {
        public PreviewNextCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            if(!AllowCommand)
                return false;
            return Preview.Files.IndexOf(Preview.CurrentFile) < Preview.Files.Count - 1;
        }
        protected override void ExecuteCore() {
            if(!AllowCommand)
                return;
            Preview.Direction = PicturePreviewSlideDirection.Forward;
            int index = Preview.Files.IndexOf(Preview.CurrentFile);
            Preview.CurrentFile = Preview.Files[index + 1];
            RaiseExecuteChanged();
        }
    }

    public class PreviewNextPageCommand : PicturePreviewCommand {
        public PreviewNextPageCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            if(!AllowCommand)
                return false;
            return Preview.Files.IndexOf(Preview.CurrentFile) < Preview.Files.Count - 1;
        }
        protected override void ExecuteCore() {
            if(!AllowCommand)
                return;
            Preview.Direction = PicturePreviewSlideDirection.Forward;
            int index = Preview.Files.IndexOf(Preview.CurrentFile);
            int delta = Math.Max(2, (int)(Preview.Files.Count * 0.05));
            index = Math.Min(Preview.Files.Count - 1, index + delta);
            Preview.CurrentFile = Preview.Files[index];
            RaiseExecuteChanged();
        }
    }

    public class PreviewFirstCommand : PicturePreviewCommand {
        public PreviewFirstCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            if(!AllowCommand)
                return false;
            return Preview.Files.IndexOf(Preview.CurrentFile) > 0;
        }
        protected override void ExecuteCore() {
            if(!AllowCommand)
                return;
            Preview.Direction = PicturePreviewSlideDirection.Backward;
            Preview.CurrentFile = Preview.Files[0];
            RaiseExecuteChanged();
        }
    }

    public class PreviewLastCommand : PicturePreviewCommand {
        public PreviewLastCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            if(!AllowCommand)
                return false;
            return Preview.Files.IndexOf(Preview.CurrentFile) < Preview.Files.Count - 1;
        }
        protected override void ExecuteCore() {
            if(!AllowCommand)
                return;
            Preview.Direction = PicturePreviewSlideDirection.Forward;
            Preview.CurrentFile = Preview.Files[Preview.Files.Count - 1];
            RaiseExecuteChanged();
        }
    }

    public class PreviewSlideShowCommand : PicturePreviewCommand {
        public PreviewSlideShowCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            if(Preview.Files == null || Preview.Files.Count < 2)
                return false;
            return true;
        }
        protected override void ExecuteCore() {
            Preview.Direction = PicturePreviewSlideDirection.Forward;
            Preview.IsSlideShow = !Preview.IsSlideShow;
            VisualStateManager.GoToState(Preview, Preview.IsSlideShow ? "SlideShow" : "Normal", false);
        }
    }

    public class DecrementTimeCommand : PicturePreviewCommand {
        public DecrementTimeCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            return Preview.ContentChangeAnimationDelay > 1;
        }
        protected override void ExecuteCore() {
            Preview.ContentChangeAnimationDelay--;
            RaiseExecuteChanged();
        }
    }

    public class IncrementTimeCommand : PicturePreviewCommand {
        public IncrementTimeCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            return true;
        }
        protected override void ExecuteCore() {
            Preview.ContentChangeAnimationDelay++;
            RaiseExecuteChanged();
        }
    }

    public class StopSlideShowCommand : PicturePreviewCommand {
        public StopSlideShowCommand(PicturePreviewControl preview)
            : base(preview) {

        }
        protected override bool CanExecureCore() {
            return true;
        }
        protected override void ExecuteCore() {
            base.ExecuteCore();
            if(Preview.IsSlideShow == false) {
                Preview.RaiseExitFullScreen();
            } else {
                Preview.IsSlideShow = false;
                VisualStateManager.GoToState(Preview, "Normal", false);
            }
        }
    }

    public class ToggleFullScreenCommand : PicturePreviewCommand {
        public ToggleFullScreenCommand(PicturePreviewControl preview) : base(preview) { }
        protected override bool CanExecureCore() {
            return true;
        }
        protected override void ExecuteCore() {
            base.ExecuteCore();
            Preview.ToggleFullScreenCore();
        }
    }

    

    public enum PicturePreviewEffectType {
        None = 0,
        PanAndZoom = 1,
        Fade = 2,
        Wipe = 3,
        Circle = 4,
        Slide = 5
    }

    public enum PicturePreviewSlideDirection {
        Forward,
        Backward
    }
}
