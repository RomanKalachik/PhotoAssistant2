using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PhotoAssistant.Core.Model;
using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Core.Native;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.Controls.Wpf {
    public class PicturePrintControl : Control {
        static PicturePrintControl() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PicturePrintControl), new FrameworkPropertyMetadata(typeof(PicturePrintControl)));
        }

        public PicturePrintControl() {
            Watermark = new WatermarkParameters();
            MovePrevCommand = new PicturePrintMovePrevCommand(this);
            MoveNextCommand = new PicturePrintMoveNextCommand(this);
            Loaded += PicturePrintControl_Loaded;
            PrintTicket = GetPrintTicketFromPrinter();
            PrintQueue = LocalPrintServer.GetDefaultPrintQueue();
        }

        public PrintQueue PrintQueue {
            get { return (PrintQueue)GetValue(PrintQueueProperty); }
            set { SetValue(PrintQueueProperty, value); }
        }

        public static readonly DependencyProperty PrintQueueProperty =
            DependencyProperty.Register("PrintQueue", typeof(PrintQueue), typeof(PicturePrintControl), new PropertyMetadata((d, e) => ((PicturePrintControl)d).OnPrintQueueChanged(e)));

        private void OnPrintQueueChanged(DependencyPropertyChangedEventArgs e) {
            PrintTicket = GetPrintTicketFromPrinter();
            PrinterText = "Printer: " + PrintQueue.FullName;
        }

        public PrintTicket PrintTicket {
            get { return (PrintTicket)GetValue(PrintTicketProperty); }
            set { SetValue(PrintTicketProperty, value); }
        }

        public static readonly DependencyProperty PrintTicketProperty =
            DependencyProperty.Register("PrintTicket", typeof(PrintTicket), typeof(PicturePrintControl), new PropertyMetadata(null));

        public string PageNumberText {
            get { return (string)GetValue(PageNumberTextProperty); }
            set { SetValue(PageNumberTextProperty, value); }
        }

        public static readonly DependencyProperty PageNumberTextProperty =
            DependencyProperty.Register("PageNumberText", typeof(string), typeof(PicturePrintControl), new PropertyMetadata(""));

        public string PaperText {
            get { return (string)GetValue(PaperTextProperty); }
            set { SetValue(PaperTextProperty, value); }
        }

        public static readonly DependencyProperty PaperTextProperty =
            DependencyProperty.Register("PaperText", typeof(string), typeof(PicturePrintControl), new PropertyMetadata(""));

        public string PrinterText {
            get { return (string)GetValue(PrinterTextProperty); }
            set { SetValue(PrinterTextProperty, value); }
        }

        public static readonly DependencyProperty PrinterTextProperty =
            DependencyProperty.Register("PrinterText", typeof(string), typeof(PicturePrintControl), new PropertyMetadata(""));

        public WatermarkParameters Watermark {
            get { return (WatermarkParameters)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(WatermarkParameters), typeof(PicturePrintControl), new PropertyMetadata(null));

        void PicturePrintControl_Loaded(object sender, RoutedEventArgs e) {
            OnParamsChanged(new DependencyPropertyChangedEventArgs());
        }

        public int SelectedPageIndex {
            get {
                if(ItemsSource == null || SelectedPage == null)
                    return -1;
                return ItemsSource.IndexOf(SelectedPage);
            }
            set {
                if(ItemsSource == null)
                    return;
                value = Math.Min(value, ItemsSource.Count - 1);
                value = Math.Max(value, 0);
                SelectedPage = ItemsSource[value];
            }
        }

        internal void RaiseCommandExecuteChanged() {
            if(MovePrevCommand != null)
                MovePrevCommand.RaiseCanExecuteChanged();
            if(MoveNextCommand != null)
                MoveNextCommand.RaiseCanExecuteChanged();
        }

        public PicturePrintMovePrevCommand MovePrevCommand {
            get { return (PicturePrintMovePrevCommand)GetValue(MovePrevCommandProperty); }
            set { SetValue(MovePrevCommandProperty, value); }
        }

        public static readonly DependencyProperty MovePrevCommandProperty =
            DependencyProperty.Register("MovePrevCommand", typeof(PicturePrintMovePrevCommand), typeof(PicturePrintControl), new PropertyMetadata(null));

        public PicturePrintMoveNextCommand MoveNextCommand {
            get { return (PicturePrintMoveNextCommand)GetValue(MoveNextCommandProperty); }
            set { SetValue(MoveNextCommandProperty, value); }
        }

        public static readonly DependencyProperty MoveNextCommandProperty =
            DependencyProperty.Register("MoveNextCommand", typeof(PicturePrintMoveNextCommand), typeof(PicturePrintControl), new PropertyMetadata(null));

        public double Zoom {
            get { return (double)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register("Zoom", typeof(double), typeof(PicturePrintControl), new PropertyMetadata(1.0, (d, e) => ((PicturePrintControl)d).OnParamsChanged(e)));

        private void OnParamsChanged(DependencyPropertyChangedEventArgs e) {
            //PageWidthPixels = PageWidth * Dpi.Width;
            //PageHeightPixels = PageHeight * Dpi.Height;
            //if(PageWidthPixels > 0 && PageHeightPixels > 0)
            //    PrintTicket.PageMediaSize = new PageMediaSize(PageWidthPixels, PageHeightPixels);
            UpdatePagesParams();
        }

        public List<PageInfo> ItemsSource {
            get { return (List<PageInfo>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(List<PageInfo>), typeof(PicturePrintControl), new PropertyMetadata(null));

        private void UpdatePagesParams() {
            if(ItemsSource == null)
                return;
            foreach(PageInfo page in ItemsSource) {
                AssignParams(page);
            }
        }

        public PageLayoutCollection PageLayouts {
            get { return (PageLayoutCollection)GetValue(PageLayoutsProperty); }
            set { SetValue(PageLayoutsProperty, value); }
        }

        public static readonly DependencyProperty PageLayoutsProperty =
            DependencyProperty.Register("PageLayouts", typeof(PageLayoutCollection), typeof(PicturePrintControl), new PropertyMetadata(new PageLayoutCollection()));

        Border Border { get; set; }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            Border = (Border)GetTemplateChild("PART_Border");
        }

        public double ScaleTransform {
            get { return ((ScaleTransform)Border.RenderTransform).ScaleX; }
        }

        public PageLayoutTemplate PageLayout {
            get { return (PageLayoutTemplate)GetValue(PageLayoutProperty); }
            set { SetValue(PageLayoutProperty, value); }
        }

        public static readonly DependencyProperty PageLayoutProperty =
            DependencyProperty.Register("PageLayout", typeof(PageLayoutTemplate), typeof(PicturePrintControl), new PropertyMetadata(null, (d, e) => ((PicturePrintControl)d).OnPageLayoutChanged(e)));

        private void OnPageLayoutChanged(DependencyPropertyChangedEventArgs e) {
            UpdatePages();
        }

        public Style PictureBorderStyle {
            get { return (Style)GetValue(PictureBorderStyleProperty); }
            set { SetValue(PictureBorderStyleProperty, value); }
        }

        public static readonly DependencyProperty PictureBorderStyleProperty =
            DependencyProperty.Register("PictureBorderStyle", typeof(Style), typeof(PicturePrintControl), new PropertyMetadata(null, (d, e) => ((PicturePrintControl)d).OnParamsChanged(e)));

        public double PageWidth {
            get { return (double)GetValue(PageWidthProperty); }
            set { SetValue(PageWidthProperty, value); }
        }

        public static readonly DependencyProperty PageWidthProperty =
            DependencyProperty.Register("PageWidth", typeof(double), typeof(PicturePrintControl), new PropertyMetadata(0.0, (d, e) => ((PicturePrintControl)d).OnParamsChanged(e)));

        public double PageHeight {
            get { return (double)GetValue(PageHeightProperty); }
            set { SetValue(PageHeightProperty, value); }
        }

        public static readonly DependencyProperty PageHeightProperty =
            DependencyProperty.Register("PageHeight", typeof(double), typeof(PicturePrintControl), new PropertyMetadata(0.0, (d, e) => ((PicturePrintControl)d).OnParamsChanged(e)));

        public double PageWidthPixels {
            get { return (double)GetValue(PageWidthPixelsProperty); }
            set { SetValue(PageWidthPixelsProperty, value); }
        }

        public static readonly DependencyProperty PageWidthPixelsProperty =
            DependencyProperty.Register("PageWidthPixels", typeof(double), typeof(PicturePrintControl), new PropertyMetadata(0.0));

        public double PageHeightPixels {
            get { return (double)GetValue(PageHeightPixelsProperty); }
            set { SetValue(PageHeightPixelsProperty, value); }
        }

        public static readonly DependencyProperty PageHeightPixelsProperty =
            DependencyProperty.Register("PageHeightPixels", typeof(double), typeof(PicturePrintControl), new PropertyMetadata(0.0));

        public Style BackgroundStyle {
            get { return (Style)GetValue(BackgroundStyleProperty); }
            set { SetValue(BackgroundStyleProperty, value); }
        }

        public static readonly DependencyProperty BackgroundStyleProperty =
            DependencyProperty.Register("BackgroundStyle", typeof(Style), typeof(PicturePrintControl), new PropertyMetadata(null, (d, e) => ((PicturePrintControl)d).OnParamsChanged(e)));

        public PageInfo SelectedPage {
            get { return (PageInfo)GetValue(SelectedPageProperty); }
            set { SetValue(SelectedPageProperty, value); }
        }

        public static readonly DependencyProperty SelectedPageProperty =
            DependencyProperty.Register("SelectedPage", typeof(PageInfo), typeof(PicturePrintControl), new PropertyMetadata(null, (d, e) => ((PicturePrintControl)d).OnSelectedPageChanged(e)));

        private void OnSelectedPageChanged(DependencyPropertyChangedEventArgs e) {
            if(selectedPageChanged != null)
                selectedPageChanged(this, EventArgs.Empty);
            SelectedPhoto = SelectedPage.Files[SelectedPhotoLayout];
            PageNumberText = string.Format("Page {0} of {1}", SelectedPageIndex + 1, ItemsSource.Count);
            LoadNearPhotos();
        }

        private void LoadNearPhotos() {
            if(SelectedPageIndex > 0)
                LoadNearPhotos(ItemsSource[SelectedPageIndex - 1]);
            if(SelectedPageIndex < ItemsSource.Count - 1)
                LoadNearPhotos(ItemsSource[SelectedPageIndex + 1]);
        }

        private void LoadNearPhotos(PageInfo pageInfo) {
            List<DmFile> files = new List<DmFile>();
            pageInfo.Files.ForEach((photo) => files.Add(photo.File));
            BackgroundImageLoader.Default.LoadFileImageInBackground(files, (d, s) => {
                foreach(PhotoInfo photo in pageInfo.Files)
                    photo.ImageSource = (ImageSource)photo.File.ImageSource;
            });
        }

        event PagePrintingEventHandler pagePrinting;
        public event PagePrintingEventHandler PagePrinting {
            add { pagePrinting += value; }
            remove { pagePrinting -= value; }
        }

        internal void RaisePagePrinting(PagePrintingEventArgs e) {
            if(pagePrinting != null)
                pagePrinting.Invoke(this, e);
        }
        public void Print() {
            PrintDialog pd = new PrintDialog();
            PrintTicket.PageMediaSize = new PageMediaSize(PageWidthPixels, PageHeightPixels);
            pd.PrintTicket = PrintTicket;
            pd.PrintQueue = PrintQueue;
            if(pd.ShowDialog() != true)
                return;
            try {
                var xpsDocWriter = PrintQueue.CreateXpsDocumentWriter(pd.PrintQueue);
                xpsDocWriter.WritingPrintTicketRequired += xpsDocWriter_WritingPrintTicketRequired;
                xpsDocWriter.Write(new PhotoPaginator(this));
                RaisePagePrinting(new PagePrintingEventArgs() { Finished = true });
                xpsDocWriter.WritingPrintTicketRequired -= xpsDocWriter_WritingPrintTicketRequired;
            } catch(Exception) { }
        }

        void xpsDocWriter_WritingPrintTicketRequired(object sender, System.Windows.Documents.Serialization.WritingPrintTicketRequiredEventArgs e) {
            e.CurrentPrintTicket = PrintTicket;
        }
        private DocumentReference toDocumentReference(UIElement uiElement) {
            if(uiElement == null)
                throw new NullReferenceException("the UIElement has to be not null");

            FixedPage fixedPage = new FixedPage();
            PageContent pageContent = new PageContent();
            FixedDocument fixedDoc = new FixedDocument();
            DocumentReference docRef = new DocumentReference();

            #region Step1

            // add the UIElement object the FixedPage
            fixedPage.Children.Add(uiElement);

            #endregion

            #region Step2

            // add the FixedPage to the PageContent collection
            pageContent.BeginInit();
            ((IAddChild)pageContent).AddChild(fixedPage);
            pageContent.EndInit();

            #endregion

            #region Step 3

            // add the PageContent to the FixedDocument collection
            ((IAddChild)fixedDoc).AddChild(pageContent);

            #endregion

            #region Step 4

            // add the FixedDocument to the document reference collection
            docRef.BeginInit();
            docRef.SetDocument(fixedDoc);
            docRef.EndInit();

            #endregion

            return docRef;
        }

        public void PrintSinglePage() {
            if(SelectedPage == null)
                return;
            PrintDialog pd = new PrintDialog();
            pd.PrintTicket = GetPrintTicketFromPrinter();
            pd.PrintQueue = PrintQueue;
            if(pd.ShowDialog() != true)
                return;
            try {
                PrintablePageControl pg = new PrintablePageControl() { Width = PageWidthPixels, Height = PageHeightPixels };
                pg.PageInfo = SelectedPage;
                pd.PrintVisual(pg, "");
            } catch(Exception) { }
        }

        private PrintTicket GetPrintTicketFromPrinter() {
            PrintQueue printQueue = PrintQueue;
            if(printQueue == null)
                return null;

            PrintTicket printTicket = (PrintTicket)printQueue.DefaultPrintTicket.Clone();
            printTicket.PageBorderless = PageBorderless.Borderless;

            PrintCapabilities printCapabilites = printQueue.GetPrintCapabilities();

            if(printCapabilites.CollationCapability.Contains(Collation.Collated)) {
                printTicket.Collation = Collation.Collated;
            }

            if(printCapabilites.DuplexingCapability.Contains(
                    Duplexing.TwoSidedLongEdge)) {
                printTicket.Duplexing = Duplexing.TwoSidedLongEdge;
            }

            if(printCapabilites.StaplingCapability.Contains(Stapling.StapleDualLeft)) {
                printTicket.Stapling = Stapling.StapleDualLeft;
            }

            return printTicket;
        }

        event EventHandler selectedPageChanged;
        public event EventHandler SelectedPageChanged {
            add { selectedPageChanged += value; }
            remove { selectedPageChanged -= value; }
        }

        List<DmFile> files;
        public List<DmFile> Files {
            get { return files; }
            set {
                if(Files == value)
                    return;
                files = value;
                OnFilesChanged();
            }
        }

        private void OnFilesChanged() {
            ItemsSource = null;
            if(Files != null)
                UpdatePages();
        }

        internal Uri GetFileUri(DmFile file) {
            if(System.IO.File.Exists(file.Path))
                return new Uri(file.Path, UriKind.Absolute);
            else
                return new Uri(file.ThumbFileName, UriKind.Absolute);
        }

        private void UpdatePages() {
            ItemsSource = CreatePages();
            SelectedPage = ItemsSource.Count > 0 ? ItemsSource[0] : null;
            if(SelectedPage != null) {
                foreach(PhotoInfo info in SelectedPage.Files) {
                    info.File.ImageSource = new BitmapImage(GetFileUri(info.File));
                    info.ImageSource = (ImageSource)info.File.ImageSource;
                }
            }
        }

        Size dpi = new Size(0, 0);
        public Size Dpi {
            get {
                PresentationSource source = PresentationSource.FromVisual(this);
                if(source != null)
                    dpi = new Size(96.0 * source.CompositionTarget.TransformToDevice.M11, 96.0 * source.CompositionTarget.TransformToDevice.M22);
                return dpi;
            }
        }

        protected virtual void AssignParams(PageInfo page) {
            PageLayout.PrintControl = this;
            page.PageLayout = PageLayout.Clone();
        }

        protected virtual PageInfo CreatePage() {
            PageInfo page = new PageInfo();
            AssignParams(page);
            return page;
        }

        protected virtual List<PageInfo> CreatePages() {
            List<PageInfo> pages = new List<PageInfo>();
            if(Files == null)
                return pages;
            int filesPerPage = GetFilesPerPage();
            for(int i = 0; i < Files.Count; i += filesPerPage) {
                PageInfo page = CreatePage();
                List<PhotoInfo> files = new List<PhotoInfo>();
                for(int j = i; j < i + filesPerPage && j < Files.Count; j++) {
                    int index = files.Count;
                    files.Add(CreatePhotoInfo(page, page.PageLayout.PhotoLayouts[index], Files[j]));
                }
                page.Files = files;
                pages.Add(page);
            }
            return pages;
        }

        private PhotoInfo CreatePhotoInfo(PageInfo pageInfo, PhotoLayoutTemplate photoLayout, DmFile dmFile) {
            return new PhotoInfo() { PageInfo = pageInfo, File = dmFile, PhotoLayout = photoLayout };
        }

        private int GetFilesPerPage() {
            if(PageLayout.PhotoLayouts.Count == 0)
                return 1;
            return PageLayout.PhotoLayouts.Count;
        }

        public void ResetParams() {
            PageLayout.ResetParams();
            foreach(PageInfo pageInfo in ItemsSource) {
                pageInfo.PageLayout = PageLayout.Clone();
            }
        }

        public int SelectedPhotoLayout {
            get { return (int)GetValue(SelectedPhotoLayoutProperty); }
            set { SetValue(SelectedPhotoLayoutProperty, value); }
        }

        public static readonly DependencyProperty SelectedPhotoLayoutProperty =
            DependencyProperty.Register("SelectedPhotoLayout", typeof(int), typeof(PicturePrintControl), new PropertyMetadata(-1, (d, e) => ((PicturePrintControl)d).OnSelectedPhotoLayoutChanged(e)));

        private void OnSelectedPhotoLayoutChanged(DependencyPropertyChangedEventArgs e) {
            if(PageControl != null)
                SelectedPhoto = SelectedPage.Files[SelectedPhotoLayout];
            if(selectedPhotoLayoutChanged != null)
                selectedPhotoLayoutChanged(this, EventArgs.Empty);
        }

        event EventHandler selectedPhotoLayoutChanged;
        public event EventHandler SelectedPhotoLayoutChanged {
            add { selectedPhotoLayoutChanged += value; }
            remove { selectedPhotoLayoutChanged -= value; }
        }

        PhotoInfo selectedPhoto;
        public PhotoInfo SelectedPhoto {
            get { return selectedPhoto; }
            set {
                if(SelectedPhoto == value)
                    return;
                PhotoInfo prev = SelectedPhoto;
                selectedPhoto = value;
                OnSelectedPhotoChanged(prev);
            }
        }

        private void OnSelectedPhotoChanged(PhotoInfo prev) {
            if(prev != null)
                prev.Selected = false;
            if(SelectedPhoto != null)
                SelectedPhoto.Selected = true;
        }

        public PageControl PageControl {
            get { return (PageControl)GetValue(PageControlProperty); }
            set { SetValue(PageControlProperty, value); }
        }

        public static readonly DependencyProperty PageControlProperty =
            DependencyProperty.Register("PageControl", typeof(PageControl), typeof(PicturePrintControl), new PropertyMetadata(null));
    }

    public class PrintablePageControl : PageControl {
        static PrintablePageControl() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PrintablePageControl), new FrameworkPropertyMetadata(typeof(PrintablePageControl)));
        }
    }

    public class PageControl : ItemsControl {
        static PageControl() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PageControl), new FrameworkPropertyMetadata(typeof(PageControl)));
        }

        public PageControl() {
            Loaded += PageControl_Loaded;
        }

        public PicturePrintControl PrintControl {
            get { return (PicturePrintControl)GetValue(PrintControlProperty); }
            set { SetValue(PrintControlProperty, value); }
        }

        public static readonly DependencyProperty PrintControlProperty =
            DependencyProperty.Register("PrintControl", typeof(PicturePrintControl), typeof(PageControl), new PropertyMetadata(null));

        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);
        }

        void PageControl_Loaded(object sender, RoutedEventArgs e) {
            PrintControl = LayoutHelper.FindParentObject<PicturePrintControl>(this);
            if(PrintControl != null)
                PrintControl.PageControl = this;
        }

        public PageInfo PageInfo {
            get { return (PageInfo)GetValue(PageInfoProperty); }
            set { SetValue(PageInfoProperty, value); }
        }

        public static readonly DependencyProperty PageInfoProperty =
            DependencyProperty.Register("PageInfo", typeof(PageInfo), typeof(PageControl), new PropertyMetadata(null, (d, e) => ((PageControl)d).OnPageInfoChanged(e)));

        private void OnPageInfoChanged(DependencyPropertyChangedEventArgs e) {
            if(PageInfo == null) {
                ItemsSource = null;
                return;
            }
            ItemsSource = PageInfo.Files;
            PageInfo.PageControl = this;
        }

        protected override Size MeasureOverride(Size constraint) {
            Size res = base.MeasureOverride(constraint);
            return res;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            PhotoInfo info = (PhotoInfo)item;
            ContentPresenter presenter = (ContentPresenter)element;
            Grid.SetRow(presenter, info.PhotoLayout.Row);
            Grid.SetRowSpan(presenter, info.PhotoLayout.RowSpan);
            Grid.SetColumn(presenter, info.PhotoLayout.Column);
            Grid.SetColumnSpan(presenter, info.PhotoLayout.ColumnSpan);
        }
    }

    public class PhotoInfo : DependencyObject {

        public ImageSource ImageSource {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(PhotoInfo), new PropertyMetadata(null));

        public string LabelText {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public bool Selected {
            get { return (bool)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }

        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.Register("Selected", typeof(bool), typeof(PhotoInfo), new PropertyMetadata(false));

        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(PhotoInfo), new PropertyMetadata(""));

        public string PhotoInfoText {
            get { return (string)GetValue(PhotoInfoTextProperty); }
            set { SetValue(PhotoInfoTextProperty, value); }
        }

        public static readonly DependencyProperty PhotoInfoTextProperty =
            DependencyProperty.Register("PhotoInfoText", typeof(string), typeof(PhotoInfo), new PropertyMetadata(""));

        public DmFile File {
            get { return (DmFile)GetValue(FileProperty); }
            set { SetValue(FileProperty, value); }
        }

        public static readonly DependencyProperty FileProperty =
            DependencyProperty.Register("File", typeof(DmFile), typeof(PhotoInfo), new FrameworkPropertyMetadata(null, (d, e) => ((PhotoInfo)d).OnFileChanged(e)));

        private void OnFileChanged(DependencyPropertyChangedEventArgs e) {
            UpdateLabelText();
            UpdatePhotoInfoText();
        }

        private void UpdatePhotoInfoText() {
            if(File != null)
                PhotoInfoText = "  " + File.CameraProducer + " - " + File.CameraModel + " ISO = " + File.ISO + " Aperture = " + File.Aperture;
        }

        public void UpdateLabelText() {
            if(File == null)
                return;
            LabelText = File.Caption;
        }

        public PhotoLayoutTemplate PhotoLayout {
            get { return (PhotoLayoutTemplate)GetValue(PhotoLayoutProperty); }
            set { SetValue(PhotoLayoutProperty, value); }
        }

        public static readonly DependencyProperty PhotoLayoutProperty =
            DependencyProperty.Register("PhotoLayout", typeof(PhotoLayoutTemplate), typeof(PhotoInfo), new PropertyMetadata(null));


        public PageInfo PageInfo {
            get { return (PageInfo)GetValue(PageInfoProperty); }
            set { SetValue(PageInfoProperty, value); }
        }

        public static readonly DependencyProperty PageInfoProperty =
            DependencyProperty.Register("PageInfo", typeof(PageInfo), typeof(PhotoInfo), new PropertyMetadata(null));
    }

    public class PageInfo : DependencyObject {

        public List<PhotoInfo> Files {
            get { return (List<PhotoInfo>)GetValue(FilesProperty); }
            set { SetValue(FilesProperty, value); }
        }

        public static readonly DependencyProperty FilesProperty =
           DependencyProperty.Register("Files", typeof(List<PhotoInfo>), typeof(PageInfo), new PropertyMetadata(null));

        public double Zoom {
            get { return (double)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register("Zoom", typeof(double), typeof(PageInfo), new PropertyMetadata(1.0));


        public PageLayoutTemplate PageLayout {
            get { return (PageLayoutTemplate)GetValue(PageLayoutProperty); }
            set { SetValue(PageLayoutProperty, value); }
        }

        public static readonly DependencyProperty PageLayoutProperty =
            DependencyProperty.Register("PageLayout", typeof(PageLayoutTemplate), typeof(PageInfo), new PropertyMetadata(null));

        public PageControl PageControl { get; set; }
    }

    public class PhotoLayoutTemplate : DependencyObject {

        public double LabelPadding {
            get { return (double)GetValue(LabelPaddingProperty); }
            set { SetValue(LabelPaddingProperty, value); }
        }

        public static readonly DependencyProperty LabelPaddingProperty =
            DependencyProperty.Register("LabelPadding", typeof(double), typeof(PhotoLayoutTemplate), new PropertyMetadata(0.03937, (d, e) => ((PhotoLayoutTemplate)d).OnLabelPaddingChanged(e)));

        private void OnLabelPaddingChanged(DependencyPropertyChangedEventArgs e) {
            if(PageLayout == null || PageLayout.PrintControl == null)
                return;
            LabelPaddingPixels = PageLayout.PrintControl.Dpi.Width * LabelPadding;
        }

        public double LabelPaddingPixels {
            get { return (double)GetValue(LabelPaddingPixelsProperty); }
            set { SetValue(LabelPaddingPixelsProperty, value); }
        }

        public static readonly DependencyProperty LabelPaddingPixelsProperty =
            DependencyProperty.Register("LabelPaddingPixels", typeof(double), typeof(PhotoLayoutTemplate), new PropertyMetadata(0.0));

        public ItemPosition LabelPosition {
            get { return (ItemPosition)GetValue(LabelPositionProperty); }
            set { SetValue(LabelPositionProperty, value); }
        }

        public static readonly DependencyProperty LabelPositionProperty =
            DependencyProperty.Register("LabelPosition", typeof(ItemPosition), typeof(PhotoLayoutTemplate), new PropertyMetadata(ItemPosition.BottomCenter, (d, e) => ((PhotoLayoutTemplate)d).OnLabelPositionChanged(e)));

        private void OnLabelPositionChanged(DependencyPropertyChangedEventArgs e) {
            switch(LabelPosition) {
                case ItemPosition.TopLeft:
                case ItemPosition.TopRight:
                case ItemPosition.TopCenter:
                case ItemPosition.TopStretch:
                    LabelVerticalAlignment = VerticalAlignment.Top;
                    break;
                case ItemPosition.MiddleLeftCenter:
                case ItemPosition.MiddleRightCenter:
                    LabelVerticalAlignment = VerticalAlignment.Center;
                    break;
                case ItemPosition.MiddleLeftStretch:
                case ItemPosition.MiddleRightStretch:
                    LabelVerticalAlignment = VerticalAlignment.Stretch;
                    break;
                default:
                    LabelVerticalAlignment = VerticalAlignment.Bottom;
                    break;
            }
            switch(LabelPosition) {
                case ItemPosition.TopLeft:
                case ItemPosition.MiddleLeftCenter:
                case ItemPosition.MiddleLeftStretch:
                    LabelHorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case ItemPosition.TopCenter:
                case ItemPosition.BottomCenter:
                    LabelHorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case ItemPosition.TopStretch:
                case ItemPosition.BottomStretch:
                    LabelHorizontalAlignment = HorizontalAlignment.Stretch;
                    break;
                case ItemPosition.TopRight:
                case ItemPosition.MiddleRightCenter:
                case ItemPosition.MiddleRightStretch:
                case ItemPosition.BottomRight:
                    LabelHorizontalAlignment = HorizontalAlignment.Right;
                    break;
            }
        }

        public bool ShowPhotoInfo {
            get { return (bool)GetValue(ShowPhotoInfoProperty); }
            set { SetValue(ShowPhotoInfoProperty, value); }
        }

        public static readonly DependencyProperty ShowPhotoInfoProperty =
            DependencyProperty.Register("ShowPhotoInfo", typeof(bool), typeof(PhotoLayoutTemplate), new PropertyMetadata(false));

        public bool ShowLabel {
            get { return (bool)GetValue(ShowLabelProperty); }
            set { SetValue(ShowLabelProperty, value); }
        }

        public static readonly DependencyProperty ShowLabelProperty =
            DependencyProperty.Register("ShowLabel", typeof(bool), typeof(PhotoLayoutTemplate), new PropertyMetadata(false));

        public Color FontColor {
            get { return (Color)GetValue(FontColorProperty); }
            set { SetValue(FontColorProperty, value); }
        }

        public static readonly DependencyProperty FontColorProperty =
            DependencyProperty.Register("FontColor", typeof(Color), typeof(PhotoLayoutTemplate), new PropertyMetadata(Colors.Black));

        public Color LabelBackgroundColor {
            get { return (Color)GetValue(LabelBackgroundColorProperty); }
            set { SetValue(LabelBackgroundColorProperty, value); }
        }

        public Color LabelBorderColor {
            get { return (Color)GetValue(LabelBorderColorProperty); }
            set { SetValue(LabelBorderColorProperty, value); }
        }

        public static readonly DependencyProperty LabelBorderColorProperty =
            DependencyProperty.Register("LabelBorderColor", typeof(Color), typeof(PhotoLayoutTemplate), new PropertyMetadata(Colors.Gray));

        public double BorderThickness {
            get { return (double)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(double), typeof(PhotoLayoutTemplate), new PropertyMetadata(1.0));

        public static readonly DependencyProperty LabelBackgroundColorProperty =
            DependencyProperty.Register("LabelBackgroundColor", typeof(Color), typeof(PhotoLayoutTemplate), new PropertyMetadata(Colors.White));

        public FontFamily FontFamily {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(PhotoLayoutTemplate), new PropertyMetadata(new FontFamily("Segoe UI")));

        public FontWeight FontWeight {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        public static readonly DependencyProperty FontWeightProperty =
            DependencyProperty.Register("FontWeight", typeof(FontWeight), typeof(PhotoLayoutTemplate), new PropertyMetadata(FontWeights.Normal));

        public double FontSize {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(PhotoLayoutTemplate), new PropertyMetadata(10.0));

        public FontStyle FontStyle {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        public static readonly DependencyProperty FontStyleProperty =
            DependencyProperty.Register("FontStyle", typeof(FontStyle), typeof(PhotoLayoutTemplate), new PropertyMetadata(FontStyles.Normal));

        public HorizontalAlignment LabelHorizontalAlignment {
            get { return (HorizontalAlignment)GetValue(LabelHorizontalAlignmentProperty); }
            set { SetValue(LabelHorizontalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty LabelHorizontalAlignmentProperty =
            DependencyProperty.Register("LabelHorizontalAlignment", typeof(HorizontalAlignment), typeof(PhotoLayoutTemplate), new PropertyMetadata(HorizontalAlignment.Stretch));

        public VerticalAlignment LabelVerticalAlignment {
            get { return (VerticalAlignment)GetValue(LabelVerticalAlignmentProperty); }
            set { SetValue(LabelVerticalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty LabelVerticalAlignmentProperty =
            DependencyProperty.Register("LabelVerticalAlignment", typeof(VerticalAlignment), typeof(PhotoLayoutTemplate), new PropertyMetadata(VerticalAlignment.Bottom, (d, e) => ((PhotoLayoutTemplate)d).OnLabelVerticalAlignmentChanged(e)));

        private void OnLabelVerticalAlignmentChanged(DependencyPropertyChangedEventArgs e) {
            if(LabelVerticalAlignment == VerticalAlignment.Center ||
                LabelVerticalAlignment == VerticalAlignment.Stretch) {
                RotateAngle = LabelHorizontalAlignment == HorizontalAlignment.Left ? -90 : 90.0;
            } else {
                RotateAngle = 0.0;
            }
        }

        public double RotateAngle {
            get { return (double)GetValue(RotateAngleProperty); }
            set { SetValue(RotateAngleProperty, value); }
        }

        public static readonly DependencyProperty RotateAngleProperty =
            DependencyProperty.Register("RotateAngle", typeof(double), typeof(PhotoLayoutTemplate), new PropertyMetadata(0.0));

        public Stretch ImageFit {
            get { return (Stretch)GetValue(ImageFitProperty); }
            set { SetValue(ImageFitProperty, value); }
        }

        public static readonly DependencyProperty ImageFitProperty =
            DependencyProperty.Register("ImageFit", typeof(Stretch), typeof(PhotoLayoutTemplate), new PropertyMetadata(Stretch.Uniform));

        public bool RotateToFit {
            get { return (bool)GetValue(RotateToFitProperty); }
            set { SetValue(RotateToFitProperty, value); }
        }

        public static readonly DependencyProperty RotateToFitProperty =
            DependencyProperty.Register("RotateToFit", typeof(bool), typeof(PhotoLayoutTemplate), new PropertyMetadata(false));

        public int ColumnSpan {
            get { return (int)GetValue(ColumnSpanProperty); }
            set { SetValue(ColumnSpanProperty, value); }
        }

        public static readonly DependencyProperty ColumnSpanProperty =
            DependencyProperty.Register("ColumnSpan", typeof(int), typeof(PhotoLayoutTemplate), new PropertyMetadata(1));

        public int RowSpan {
            get { return (int)GetValue(RowSpanProperty); }
            set { SetValue(RowSpanProperty, value); }
        }

        public static readonly DependencyProperty RowSpanProperty =
            DependencyProperty.Register("RowSpan", typeof(int), typeof(PhotoLayoutTemplate), new PropertyMetadata(1));

        public int Row {
            get { return (int)GetValue(RowProperty); }
            set { SetValue(RowProperty, value); }
        }

        public static readonly DependencyProperty RowProperty =
            DependencyProperty.Register("Row", typeof(int), typeof(PhotoLayoutTemplate), new PropertyMetadata(0));

        public int Column {
            get { return (int)GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }

        public static readonly DependencyProperty ColumnProperty =
            DependencyProperty.Register("Column", typeof(int), typeof(PhotoLayoutTemplate), new PropertyMetadata(0));

        public double Width {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(PhotoLayoutTemplate), new PropertyMetadata(double.NaN));

        public double Height {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(PhotoLayoutTemplate), new PropertyMetadata(double.NaN));

        public PageLayoutTemplate PageLayout {
            get { return (PageLayoutTemplate)GetValue(PageLayoutProperty); }
            set { SetValue(PageLayoutProperty, value); }
        }

        public static readonly DependencyProperty PageLayoutProperty =
            DependencyProperty.Register("PageLayout", typeof(PageLayoutTemplate), typeof(PhotoLayoutTemplate), new PropertyMetadata(null));

        public PhotoLayoutTemplate Copy() {
            PhotoLayoutTemplate res = new PhotoLayoutTemplate();
            res.Column = Column;
            res.ColumnSpan = ColumnSpan;
            res.Height = Height;
            res.Width = Width;
            res.ImageFit = ImageFit;
            res.RotateToFit = RotateToFit;
            res.Row = Row;
            res.RowSpan = RowSpan;
            return res;
        }
    }

    public class PhotoInfoCollection : List<PhotoLayoutTemplate> { }

    public class PageLayoutTemplate : DependencyObject {

        public PageLayoutTemplate() {
            PhotoLayouts = new PhotoInfoCollection();
        }


        public PicturePrintControl PrintControl {
            get { return (PicturePrintControl)GetValue(PrintControlProperty); }
            set { SetValue(PrintControlProperty, value); }
        }

        public static readonly DependencyProperty PrintControlProperty =
            DependencyProperty.Register("PrintControl", typeof(PicturePrintControl), typeof(PageLayoutTemplate), new PropertyMetadata(null, (d, e) => ((PageLayoutTemplate)d).OnPrintCotrolChanged(e)));

        private void OnPrintCotrolChanged(DependencyPropertyChangedEventArgs e) {
            UpdateParamsByDpi();
        }

        public Style PictureBorderStyle {
            get { return (Style)GetValue(PictureBorderStyleProperty); }
            set { SetValue(PictureBorderStyleProperty, value); }
        }

        public static readonly DependencyProperty PictureBorderStyleProperty =
            DependencyProperty.Register("PictureBorderStyle", typeof(Style), typeof(PageLayoutTemplate), new PropertyMetadata(null));

        public Style PageBackgroundStyle {
            get { return (Style)GetValue(PageBackgroundStyleProperty); }
            set { SetValue(PageBackgroundStyleProperty, value); }
        }

        public static readonly DependencyProperty PageBackgroundStyleProperty =
            DependencyProperty.Register("PageBackgroundStyle", typeof(Style), typeof(PageLayoutTemplate), new PropertyMetadata(null));

        public Thickness PageMargins {
            get { return (Thickness)GetValue(PageMarginsProperty); }
            set { SetValue(PageMarginsProperty, value); }
        }

        public static readonly DependencyProperty PageMarginsProperty =
            DependencyProperty.Register("PageMargins", typeof(Thickness), typeof(PageLayoutTemplate), new PropertyMetadata(new Thickness(), (d, e) => ((PageLayoutTemplate)d).OnPageMarginsChanged(e)));

        void UpdateParamsByDpi() {
            if(PrintControl == null)
                return;
            PageMarginsPixels = new Thickness(
                PageMargins.Left * PrintControl.Dpi.Width,
                PageMargins.Top * PrintControl.Dpi.Height,
                PageMargins.Right * PrintControl.Dpi.Width,
                PageMargins.Bottom * PrintControl.Dpi.Height
            );
            ImageIndentPixels = ImageIndent * PrintControl.Dpi.Width;
        }

        private void OnPageMarginsChanged(DependencyPropertyChangedEventArgs e) {
            UpdateParamsByDpi();
        }

        public Thickness PageMarginsPixels {
            get { return (Thickness)GetValue(PageMarginsPixelsProperty); }
            set { SetValue(PageMarginsPixelsProperty, value); }
        }

        public static readonly DependencyProperty PageMarginsPixelsProperty =
            DependencyProperty.Register("PageMarginsPixels", typeof(Thickness), typeof(PageLayoutTemplate), new PropertyMetadata(new Thickness()));

        public double ImageIndent {
            get { return (double)GetValue(ImageIndentProperty); }
            set { SetValue(ImageIndentProperty, value); }
        }

        public static readonly DependencyProperty ImageIndentProperty =
            DependencyProperty.Register("ImageIndent", typeof(double), typeof(PageLayoutTemplate), new PropertyMetadata(0.0, (d, e) => ((PageLayoutTemplate)d).OnImageIndentChanged(e)));

        private void OnImageIndentChanged(DependencyPropertyChangedEventArgs e) {
            UpdateParamsByDpi();
        }

        public double ImageIndentPixels {
            get { return (double)GetValue(ImageIndentPixelsProperty); }
            set { SetValue(ImageIndentPixelsProperty, value); }
        }

        public static readonly DependencyProperty ImageIndentPixelsProperty =
            DependencyProperty.Register("ImageIndentPixels", typeof(double), typeof(PageLayoutTemplate), new PropertyMetadata(0.0));

        public string Name {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(PageLayoutTemplate), new PropertyMetadata(""));

        public string Description {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(PageLayoutTemplate), new PropertyMetadata(""));

        public ItemsPanelTemplate PanelTemplate {
            get { return (ItemsPanelTemplate)GetValue(PanelTemplateProperty); }
            set { SetValue(PanelTemplateProperty, value); }
        }

        public static readonly DependencyProperty PanelTemplateProperty =
            DependencyProperty.Register("PanelTemplate", typeof(ItemsPanelTemplate), typeof(PageLayoutTemplate), new PropertyMetadata(null));

        public PhotoInfoCollection PhotoLayouts {
            get { return (PhotoInfoCollection)GetValue(PhotoLayoutsProperty); }
            set { SetValue(PhotoLayoutsProperty, value); }
        }

        public static readonly DependencyProperty PhotoLayoutsProperty =
            DependencyProperty.Register("PhotoLayouts", typeof(PhotoInfoCollection), typeof(PageLayoutTemplate), new PropertyMetadata(null));

        PageLayoutTemplate copyLayout;
        public PageLayoutTemplate LayoutCopy {
            get {
                if(copyLayout == null)
                    copyLayout = Clone();
                return copyLayout;
            }
        }

        public void ResetParams() {
            this.copyLayout = null;
        }

        public PageLayoutTemplate Clone() {
            PageLayoutTemplate res = new PageLayoutTemplate();
            res.Description = Description;
            res.ImageIndent = ImageIndent;
            res.Name = Name;
            res.PageBackgroundStyle = PageBackgroundStyle;
            res.PageMargins = PageMargins;
            res.PanelTemplate = PanelTemplate;
            res.PictureBorderStyle = PictureBorderStyle;
            res.PrintControl = PrintControl;
            res.PhotoLayouts = new PhotoInfoCollection();
            foreach(PhotoLayoutTemplate item in PhotoLayouts) {
                PhotoLayoutTemplate item2 = item.Copy();
                item2.PageLayout = res;
                res.PhotoLayouts.Add(item2);
            }
            return res;
        }
    }

    public class PageLayoutCollection : List<PageLayoutTemplate> { }

    public class PhotoGrid : Grid {
        protected override Size MeasureOverride(Size constraint) {
            Size res = base.MeasureOverride(constraint);
            return res;
        }
        protected override Size ArrangeOverride(Size arrangeSize) {
            Size res = base.ArrangeOverride(arrangeSize);
            return res;
        }
    }

    public class PhotoBorder : Border {
        protected override Size MeasureOverride(Size constraint) {
            Size res = base.MeasureOverride(constraint);
            return res;
        }
        protected override Size ArrangeOverride(Size finalSize) {
            Size res = base.ArrangeOverride(finalSize);
            return res;
        }
    }

    public class PicturePrintCommandBase : ICommand {
        public PicturePrintCommandBase(PicturePrintControl owner) {
            Owner = owner;
        }
        protected PicturePrintControl Owner { get; private set; }

        bool ICommand.CanExecute(object parameter) {
            return CanExecuteCore(parameter);
        }

        protected virtual bool CanExecuteCore(object parameter) {
            return false;
        }

        event EventHandler canExecuteChanged;
        event EventHandler ICommand.CanExecuteChanged {
            add { canExecuteChanged += value; }
            remove { canExecuteChanged -= value; }
        }

        protected internal void RaiseCanExecuteChanged() {
            if(canExecuteChanged != null)
                canExecuteChanged(this, EventArgs.Empty);
        }

        void ICommand.Execute(object parameter) {
            ExecuteCore(parameter);
        }

        protected virtual void ExecuteCore(object parameter) {
        }

        protected void RaiseExecuteChangedCore() {
            Owner.RaiseCommandExecuteChanged();
        }
    }

    public class PicturePrintMovePrevCommand : PicturePrintCommandBase {
        public PicturePrintMovePrevCommand(PicturePrintControl owner) : base(owner) { }
        protected override bool CanExecuteCore(object parameter) {
            if(Owner.ItemsSource == null)
                return false;
            return Owner.SelectedPageIndex > 0;
        }
        protected override void ExecuteCore(object parameter) {
            if(Owner.SelectedPageIndex == -1)
                Owner.SelectedPageIndex = 0;
            else if(Owner.SelectedPageIndex > 0)
                Owner.SelectedPageIndex--;
            RaiseExecuteChangedCore();
        }
    }

    public class PicturePrintMoveNextCommand : PicturePrintCommandBase {
        public PicturePrintMoveNextCommand(PicturePrintControl owner) : base(owner) { }
        protected override bool CanExecuteCore(object parameter) {
            if(Owner.ItemsSource == null)
                return false;
            return Owner.SelectedPageIndex < Owner.ItemsSource.Count;
        }
        protected override void ExecuteCore(object parameter) {
            if(Owner.SelectedPageIndex == -1)
                Owner.SelectedPageIndex = 0;
            else if(Owner.SelectedPageIndex < Owner.ItemsSource.Count)
                Owner.SelectedPageIndex++;
            RaiseExecuteChangedCore();
        }
    }



    public class PagePrintingEventArgs {
        public int Index { get; set; }
        public int Count { get; set; }

        public bool Finished { get; set; }
    }

    public delegate void PagePrintingEventHandler(object sender, PagePrintingEventArgs e);

    public class PhotoPaginator : DocumentPaginator, IDocumentPaginatorSource {
        public PhotoPaginator(PicturePrintControl control) {
            Control = control;
        }

        public PicturePrintControl Control { get; private set; }
        public override Size PageSize {
            get { return new Size(Control.PrintTicket.PageMediaSize.Width.Value, Control.PrintTicket.PageMediaSize.Height.Value); }
            set { }
        }
        public override bool IsPageCountValid {
            get { return true; }
        }
        public override IDocumentPaginatorSource Source {
            get { return this; }
        }
        public override int PageCount {
            get { return Control.ItemsSource.Count; }
        }
        public override DocumentPage GetPage(int pageNumber) {
            PagePrintingEventArgs e = new PagePrintingEventArgs() { Index = pageNumber + 1, Count = Control.ItemsSource.Count };
            Control.RaisePagePrinting(e);
            foreach(PhotoInfo photo in Control.ItemsSource[pageNumber].Files) {
                BitmapImage image = new BitmapImage(Control.GetFileUri(photo.File));
                image.CacheOption = BitmapCacheOption.OnLoad;
                photo.File.ImageSource = photo.ImageSource = image;
            }
            PrintablePageControl pg = new PrintablePageControl();
            pg.PageInfo = Control.ItemsSource[pageNumber];
            UserControl control = new UserControl() { Width = PageSize.Width, Height = PageSize.Height };
            control.Content = pg;
            control.Measure(PageSize);
            control.Arrange(new Rect(0, 0, PageSize.Width, PageSize.Height));

            DocumentPage page = new DocumentPage(control);
            return page;
        }

        DocumentPaginator IDocumentPaginatorSource.DocumentPaginator {
            get { return this; }
        }
    }
}
