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
using System.Windows.Forms.Integration;
using DevExpress.XtraBars.Ribbon;
using System.Windows.Media;

using System.Drawing.Printing;
using System.Printing;
using PhotoAssistant.Core.Model;
using PhotoAssistant.Controls.Wpf;
using PhotoAssistant.UI.ViewHelpers;

namespace PhotoAssistant.UI.View {

    public partial class PrintControl : ViewControlBase, ISupportPhotoDragDrop {
        public PrintControl() {
            InitializeComponent();
            InitializeLabelPositionCombo();
            InitializePicturePrintControl();
            InitializePrinterGallery();
            InitializeLayoutGallery();
            InitializePaperSizeGallery();
            InitializePaddings();
            InitializeFit();
            InitializeMediaTypeCombo();
            UpdateWatermarkProperties();
            UpdateZoomText();
        }

        private void InitializeMediaTypeCombo() {
            this.cbMediaType.Properties.Items.BeginUpdate();
            this.cbMediaType.Properties.Items.Clear();
            try {
                PrintCapabilities caps = PicturePrint.PrintQueue.GetPrintCapabilities();
                foreach(PageMediaType type in caps.PageMediaTypeCapability) {
                    this.cbMediaType.Properties.Items.Add(type);
                }
            } finally {
                this.cbMediaType.Properties.Items.EndUpdate();
                this.cbMediaType.EditValue = PicturePrint.PrintTicket.PageMediaType.HasValue ? (object)PicturePrint.PrintTicket.PageMediaType.Value : null;
            }
        }

        private void UpdatePrinterSettings() {
            this.spDpi.Value = PicturePrint.PrintTicket.PageResolution.X.HasValue ? PicturePrint.PrintTicket.PageResolution.X.Value : 0;
            GalleryItem item = this.gcPrinter.Gallery.GetItemByCaption(PicturePrint.PrintQueue.Name);
            if(item != null)
                item.Checked = true;
            if(PicturePrint.PrintTicket.PageMediaType.HasValue)
                this.cbMediaType.EditValue = PicturePrint.PrintTicket.PageMediaType.Value;
            else
                this.cbMediaType.EditValue = PageMediaType.Unknown;
            InitializePaperSizeGallery();
            InitializeMediaTypeCombo();
        }

        private void InitializePrinterGallery() {
            GalleryItem defaultPrinter = null;
            try {
                LocalPrintServer localPrintServer = new LocalPrintServer();
                PrintQueueCollection localPrinterCollection = localPrintServer.GetPrintQueues();
                System.Collections.IEnumerator localPrinterEnumerator = localPrinterCollection.GetEnumerator();
                while(localPrinterEnumerator.MoveNext()) {
                    GalleryItem item = new GalleryItem();
                    item.Image = PhotoAssistant.UI.Properties.Resources.print_32x32;
                    PrintQueue queue = ((PrintQueue)localPrinterEnumerator.Current);
                    item.Caption = queue.FullName;
                    item.Description = queue.Description;
                    item.Value = queue;
                    this.gcPrinter.Gallery.Groups[0].Items.Add(item);
                    if(queue == localPrintServer.DefaultPrintQueue)
                        defaultPrinter = item;
                }
            } catch { }
            this.gcPrinter.Gallery.ItemCheckedChanged += new GalleryItemEventHandler(OnPrinterGalleryItemCheckedChanged);
            if(defaultPrinter != null)
                defaultPrinter.Checked = true;
        }

        private void OnPrinterGalleryItemCheckedChanged(object sender, GalleryItemEventArgs e) {
            if(!e.Item.Checked)
                return;
            PicturePrint.PrintQueue = (PrintQueue)e.Item.Value;
            UpdatePrinterSettings();
        }

        private void UpdateWatermarkProperties() {
            this.watermarkPropertyControl1.PicturePrint = PicturePrint;
        }

        private void InitializeLabelPositionCombo() {
            this.cbPosition.Properties.Items.BeginUpdate();
            try {
                foreach(ItemPosition pos in Enum.GetValues(typeof(ItemPosition))) {
                    this.cbPosition.Properties.Items.Add(pos);
                }
            } finally {
                this.cbPosition.Properties.Items.EndUpdate();
            }
            this.cbPosition.EditValue = ItemPosition.BottomStretch;
        }

        private void InitializeFit() {
            this.rgFit.Properties.Items[0].Value = Stretch.Uniform;
            this.rgFit.Properties.Items[1].Value = Stretch.UniformToFill;
            this.rgFit.EditValue = Stretch.Uniform;
        }

        private void InitializePaddings() {
            if(PicturePrint.PageLayout == null) {
                this.sePhotoIndent.Value = new Decimal(0);
                this.sePageTop.Value = new Decimal(0);
                this.sePageLeft.Value = new Decimal(0);
                this.sePageRight.Value = new Decimal(0);
                this.sePageBottom.Value = new Decimal(0);
            } else {
                this.sePhotoIndent.EditValue = ValueToEditor(PicturePrint.PageLayout.ImageIndent);
                this.sePageTop.EditValue = ValueToEditor(PicturePrint.PageLayout.PageMargins.Left);
                this.sePageLeft.EditValue = ValueToEditor(PicturePrint.PageLayout.PageMargins.Top);
                this.sePageRight.EditValue = ValueToEditor(PicturePrint.PageLayout.PageMargins.Right);
                this.sePageBottom.EditValue = ValueToEditor(PicturePrint.PageLayout.PageMargins.Bottom);
            }
        }

        private double InchesToCentimeters(double inches) {
            return inches * 2.54;
        }

        /*
            PageMediaSizeName.BusinessCard	Business card
            PageMediaSizeName.CreditCard	Credit card
            PageMediaSizeName.ISOA0	A0
            PageMediaSizeName.ISOA1	A1
            PageMediaSizeName.ISOA10	A10
            PageMediaSizeName.ISOA2	A2
            PageMediaSizeName.ISOA3	A3
            PageMediaSizeName.ISOA3Extra	A3 Extra
            PageMediaSizeName.ISOA3Rotated	A3 Rotated
            PageMediaSizeName.ISOA4	A4
            PageMediaSizeName.ISOA4Extra	A4 Extra
            PageMediaSizeName.ISOA4Rotated	A4 Rotated
            PageMediaSizeName.ISOA5	A5
            PageMediaSizeName.ISOA5Extra	A5 Extra
            PageMediaSizeName.ISOA5Rotated	A5 Rotated
            PageMediaSizeName.ISOA6	A6
            PageMediaSizeName.ISOA6Rotated	A6 Rotated
            PageMediaSizeName.ISOA7	A7
            PageMediaSizeName.ISOA8	A8
            PageMediaSizeName.PageMediaSizeName.ISOA9	A9
            PageMediaSizeName.ISOB0	B0
            PageMediaSizeName.ISOB1	B1
            PageMediaSizeName.ISOB10	B10
            PageMediaSizeName.ISOB2	B2
            PageMediaSizeName.ISOB3	B3
            PageMediaSizeName.ISOB4	B4
            PageMediaSizeName.ISOB4Envelope	B4 Envelope
            PageMediaSizeName.ISOB5Envelope	B5 Envelope
            PageMediaSizeName.ISOB5Extra	B5 Extra
            PageMediaSizeName.ISOB7	B7
            PageMediaSizeName.ISOB8	B8
            PageMediaSizeName.ISOB9	B9
            PageMediaSizeName.ISOC0	C0
            PageMediaSizeName.ISOC1	C1
            PageMediaSizeName.ISOC10	C10
            PageMediaSizeName.ISOC2	C2
            PageMediaSizeName.ISOC3	C3
            PageMediaSizeName.ISOC3Envelope	C3 Envelope
            PageMediaSizeName.ISOC4	C4
            PageMediaSizeName.ISOC4Envelope	C4 Envelope
            PageMediaSizeName.ISOC5	C5
            PageMediaSizeName.ISOC5Envelope	C5 Envelope
            PageMediaSizeName.ISOC6	C6
            PageMediaSizeName.ISOC6C5Envelope	C6C5 Envelope
            PageMediaSizeName.ISOC6Envelope	C6 Envelope
            PageMediaSizeName.ISOC7	C7
            PageMediaSizeName.ISOC8	C8
            PageMediaSizeName.ISOC9	C9
            PageMediaSizeName.ISODLEnvelope	DL Envelope
            PageMediaSizeName.ISODLEnvelopeRotated	DL Envelope Rotated
            PageMediaSizeName.ISOSRA3	SRA 3
            PageMediaSizeName.Japan2LPhoto	2L Photo
            PageMediaSizeName.JapanChou3Envelope	Chou 3 Envelope
            PageMediaSizeName.JapanChou3EnvelopeRotated	Chou 3 Envelope Rotated
            PageMediaSizeName.JapanChou4Envelope	Chou 4 Envelope
            PageMediaSizeName.JapanChou4EnvelopeRotated	Chou 4 Envelope Rotated
            PageMediaSizeName.JapanDoubleHagakiPostcard	Double Hagaki Postcard
            PageMediaSizeName.JapanDoubleHagakiPostcardRotated	Double Hagaki Postcard Rotated
            PageMediaSizeName.JapanHagakiPostcard	Hagaki Postcard
            PageMediaSizeName.JapanHagakiPostcardRotated	Hagaki Postcard Rotated
            PageMediaSizeName.JapanKaku2Envelope	Kaku 2 Envelope
            PageMediaSizeName.JapanKaku2EnvelopeRotated	Kaku 2 Envelope Rotated
            PageMediaSizeName.JapanKaku3Envelope	Kaku 3 Envelope
            PageMediaSizeName.JapanKaku3EnvelopeRotated	Kaku 3 Envelope Rotated
            PageMediaSizeName.JapanLPhoto	L Photo
            PageMediaSizeName.JapanQuadrupleHagakiPostcard	Quadruple Hagaki Postcard
            PageMediaSizeName.JapanYou1Envelope	You 1 Envelope
            PageMediaSizeName.JapanYou2Envelope	You 2 Envelope
            PageMediaSizeName.JapanYou3Envelope	You 3 Envelope
            PageMediaSizeName.JapanYou4Envelope	You 4 Envelope
            PageMediaSizeName.JapanYou4EnvelopeRotated	You 4 Envelope Rotated
            PageMediaSizeName.JapanYou6Envelope	You 6 Envelope
            PageMediaSizeName.JapanYou6EnvelopeRotated	You 6 Envelope Rotated
            PageMediaSizeName.JISB0	Japanese Industrial Standard B0
            PageMediaSizeName.JISB1	Japanese Industrial Standard B1
            PageMediaSizeName.JISB10	Japanese Industrial Standard B10
            PageMediaSizeName.JISB2	Japanese Industrial Standard B2
            PageMediaSizeName.JISB3	Japanese Industrial Standard B3
            PageMediaSizeName.JISB4	Japanese Industrial Standard B4
            PageMediaSizeName.JISB4Rotated	Japanese Industrial Standard B4 Rotated
            PageMediaSizeName.JISB5	Japanese Industrial Standard B5
            PageMediaSizeName.JISB5Rotated	Japanese Industrial Standard B5 Rotated
            PageMediaSizeName.JISB6	Japanese Industrial Standard B6
            PageMediaSizeName.JISB6Rotated	Japanese Industrial Standard B6 Rotated
            PageMediaSizeName.JISB7	Japanese Industrial Standard B7
            PageMediaSizeName.JISB8	Japanese Industrial Standard B8
            PageMediaSizeName.JISB9	Japanese Industrial Standard B9
            PageMediaSizeName.NorthAmerica10x11	10 x 11
            PageMediaSizeName.NorthAmerica10x12	10 x 12
            PageMediaSizeName.NorthAmerica10x14	10 x 14
            PageMediaSizeName.NorthAmerica11x17	11 x 17
            PageMediaSizeName.NorthAmerica14x17	14 x 17
            PageMediaSizeName.NorthAmerica4x6	4 x 6
            PageMediaSizeName.NorthAmerica4x8	4 x 8
            PageMediaSizeName.NorthAmerica5x7	5 x 7
            PageMediaSizeName.NorthAmerica8x10	8 x 10
            PageMediaSizeName.NorthAmerica9x11	9 x 11
            PageMediaSizeName.NorthAmericaArchitectureASheet	Architecture A Sheet
            PageMediaSizeName.NorthAmericaArchitectureBSheet	Architecture B Sheet
            PageMediaSizeName.NorthAmericaArchitectureCSheet	Architecture C Sheet
            PageMediaSizeName.NorthAmericaArchitectureDSheet	Architecture D Sheet
            PageMediaSizeName.NorthAmericaArchitectureESheet	Architecture E Sheet
            PageMediaSizeName.NorthAmericaCSheet	C Sheet
            PageMediaSizeName.NorthAmericaDSheet	D Sheet
            PageMediaSizeName.NorthAmericaESheet	E Sheet
            PageMediaSizeName.NorthAmericaExecutive	Executive
            PageMediaSizeName.NorthAmericaGermanLegalFanfold	German Legal Fanfold
            PageMediaSizeName.NorthAmericaGermanStandardFanfold	German Standard Fanfold
            PageMediaSizeName.NorthAmericaLegal	Legal
            PageMediaSizeName.NorthAmericaLegalExtra	Legal Extra
            PageMediaSizeName.NorthAmericaLetter	Letter
            PageMediaSizeName.NorthAmericaLetterExtra	Letter Extra
            PageMediaSizeName.NorthAmericaLetterPlus	Letter Plus
            PageMediaSizeName.NorthAmericaLetterRotated	Letter Rotated
            PageMediaSizeName.NorthAmericaMonarchEnvelope	Monarch Envelope
            PageMediaSizeName.NorthAmericaNote	Note
            PageMediaSizeName.NorthAmericaNumber10Envelope	#10 Envelope
            PageMediaSizeName.NorthAmericaNumber10EnvelopeRotated	#10 Envelope Rotated
            PageMediaSizeName.NorthAmericaNumber11Envelope	#11 Envelope
            PageMediaSizeName.NorthAmericaNumber12Envelope	#12 Envelope
            PageMediaSizeName.NorthAmericaNumber14Envelope	#14 Envelope
            PageMediaSizeName.NorthAmericaNumber9Envelope	#9 Envelope
            PageMediaSizeName.NorthAmericaPersonalEnvelope	Personal Envelope
            PageMediaSizeName.NorthAmericaQuarto	Quarto
            PageMediaSizeName.NorthAmericaStatement	Statement
            PageMediaSizeName.NorthAmericaSuperA	Super A
            PageMediaSizeName.NorthAmericaSuperB	Super B
            PageMediaSizeName.NorthAmericaTabloid	Tabloid
            PageMediaSizeName.NorthAmericaTabloidExtra	Tabloid Extra
            PageMediaSizeName.OtherMetricA3Plus	A3 Plus
            PageMediaSizeName.OtherMetricA4Plus	A4 Plus
            PageMediaSizeName.OtherMetricFolio	Folio
            PageMediaSizeName.OtherMetricInviteEnvelope	Invite Envelope
            PageMediaSizeName.OtherMetricItalianEnvelope	Italian Envelope
            PageMediaSizeName.PRC10Envelope	People's Republic of China #10 Envelope
            PageMediaSizeName.PRC10EnvelopeRotated	People's Republic of China #10 Envelope Rotated
            PageMediaSizeName.PRC16K	People's Republic of China 16K
            PageMediaSizeName.PRC16KRotated	People's Republic of China 16K Rotated
            PageMediaSizeName.PRC1Envelope	People's Republic of China #1 Envelope
            PageMediaSizeName.PRC1EnvelopeRotated	People's Republic of China #1 Envelope Rotated
            PageMediaSizeName.PRC2Envelope	People's Republic of China #2 Envelope
            PageMediaSizeName.PRC2EnvelopeRotated	People's Republic of China #2 Envelope Rotated
            PageMediaSizeName.PRC32K	People's Republic of China 32K
            PageMediaSizeName.PRC32KBig	People's Republic of China 32K Big
            PageMediaSizeName.PRC32KRotated	People's Republic of China 32K Rotated
            PageMediaSizeName.PRC3Envelope	People's Republic of China #3 Envelope
            PageMediaSizeName.PRC3EnvelopeRotated	People's Republic of China #3 Envelope Rotated
            PageMediaSizeName.PRC4Envelope	People's Republic of China #4 Envelope
            PageMediaSizeName.PRC4EnvelopeRotated	People's Republic of China #4 Envelope Rotated
            PageMediaSizeName.PRC5Envelope	People's Republic of China #5 Envelope
            PageMediaSizeName.PRC5EnvelopeRotated	People's Republic of China #5 Envelope Rotated
            PageMediaSizeName.PRC6Envelope	People's Republic of China #6 Envelope
            PageMediaSizeName.PRC6EnvelopeRotated	People's Republic of China #6 Envelope Rotated
            PageMediaSizeName.PRC7Envelope	People's Republic of China #7 Envelope
            PageMediaSizeName.PRC7EnvelopeRotated	People's Republic of China #7 Envelope Rotated
            PageMediaSizeName.PRC8Envelope	People's Republic of China #8 Envelope
            PageMediaSizeName.PRC8EnvelopeRotated	People's Republic of China #8 Envelope Rotated
            PageMediaSizeName.PRC9Envelope	People's Republic of China #9 Envelope
            PageMediaSizeName.PRC9EnvelopeRotated	People's Republic of China #9 Envelope Rotated
            PageMediaSizeName.Roll04Inch	4-inch wide roll
            PageMediaSizeName.Roll06Inch	6-inch wide roll
            PageMediaSizeName.Roll08Inch	8-inch wide roll
            PageMediaSizeName.Roll12Inch	12-inch wide roll
            PageMediaSizeName.Roll15Inch	15-inch wide roll
            PageMediaSizeName.Roll18Inch	18-inch wide roll
            PageMediaSizeName.Roll22Inch	22-inch wide roll
            PageMediaSizeName.Roll24Inch	24-inch wide roll
            PageMediaSizeName.Roll30Inch	30-inch wide roll
            PageMediaSizeName.Roll36Inch	36-inch wide roll
            PageMediaSizeName.Roll54Inch	54-inch wide roll
        */
        private void InitializePaperSizeGallery() {
            this.paperSizeGallery.Gallery.BeginUpdate();
            try {
                GalleryItem defaultItem = this.paperSizeGallery.Gallery.GetCheckedItem();
                PageMediaSize defaultSize = defaultItem == null ? PicturePrint.PrintTicket.PageMediaSize : (PageMediaSize)defaultItem.Value;
                this.paperSizeGallery.Gallery.Groups[0].Items.Clear();
                PrintCapabilities caps = PicturePrint.PrintQueue.GetPrintCapabilities();
                foreach(PageMediaSize size in caps.PageMediaSizeCapability) {
                    GalleryItem item = CreatePageMediaSizeItem(size);
                    this.paperSizeGallery.Gallery.Groups[0].Items.Add(item);
                    if(defaultSize.PageMediaSizeName.HasValue && defaultSize.PageMediaSizeName.Value == ((PageMediaSize)item.Value).PageMediaSizeName) {
                        defaultItem = item;
                    }
                }
                if(defaultItem == null && this.paperSizeGallery.Gallery.Groups[0].Items.Count > 0)
                    defaultItem = this.paperSizeGallery.Gallery.Groups[0].Items[0];
                if(defaultItem != null)
                    defaultItem.Checked = true;
            } finally {
                this.paperSizeGallery.Gallery.EndUpdate();
            }
        }

        private GalleryItem CreatePageMediaSizeItem(PageMediaSize size) {
            GalleryItem res = new GalleryItem();
            res.Caption = Enum.GetName(typeof(PageMediaSizeName), size.PageMediaSizeName);
            res.Value = size;
            return res;
        }

        protected virtual void InitializeLayoutGallery() {
            GalleryItem defaultLayout = null;
            foreach(PageLayoutTemplate info in PicturePrint.PageLayouts) {
                int index = this.layoutGallery.Gallery.Groups[0].Items.Add(CreatePageLayout(info));
                if(info.PhotoLayouts.Count == 1 && double.IsNaN(info.PhotoLayouts[0].Width))
                    defaultLayout = this.layoutGallery.Gallery.Groups[0].Items[index];
            }
            if(defaultLayout == null)
                defaultLayout = this.layoutGallery.Gallery.Groups[0].Items[0];
            defaultLayout.Checked = true;
        }

        private DevExpress.XtraBars.Ribbon.GalleryItem CreatePageLayout(PageLayoutTemplate info) {
            return new DevExpress.XtraBars.Ribbon.GalleryItem() { Caption = info.Name, Description = info.Description, Value = info };
        }

        internal PicturePrintControl PicturePrint { get; set; }
        ElementHost PicturePreviewHost { get; set; }

        private void InitializePicturePrintControl() {
            PicturePreviewHost = new ElementHost();
            PicturePreviewHost.Dock = DockStyle.Fill;
            PicturePrint = new PicturePrintControl();
            PicturePreviewHost.Child = PicturePrint;
            this.splitContainerControl1.Panel2.Controls.Add(PicturePreviewHost);
            PicturePreviewHost.BringToFront();
            SubscribeEvents();
        }

        private void SubscribeEvents() {
            PicturePrint.SelectedPageChanged += PicturePrint_SelectedPageChanged;
            PicturePrint.SelectedPhotoLayoutChanged += PicturePrint_SelectedPhotoLayoutChanged;
            PicturePrint.PagePrinting += PicturePrint_PagePrinting;
        }

        string PrintingProgressString { get; set; }
        void PicturePrint_PagePrinting(object sender, PagePrintingEventArgs e) {
            if(e.Finished) {
                this.bsPagePrintingText.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.bePagePrintingProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                return;
            }
            PrintingProgressString = string.Format("Page {0} of {1}", e.Index, e.Count);
            this.riProgressBarPrinting.Minimum = 0;
            this.riProgressBarPrinting.Maximum = e.Count;
            this.bePagePrintingProgress.EditValue = e.Index;
            this.bsPagePrintingText.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            this.bePagePrintingProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            Application.DoEvents();
        }
        private void riProgressBarPrinting_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e) {
            e.DisplayText = PrintingProgressString;
        }


        void PicturePrint_SelectedPhotoLayoutChanged(object sender, EventArgs e) {
            this.cbePhotoLayout.SelectedIndex = PicturePrint.SelectedPhotoLayout;
        }

        void PicturePrint_SelectedPageChanged(object sender, EventArgs e) {
            InitializePhotoLayoutCombo();
            UpdatePhotoInfoParams();
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

        protected virtual void OnFilesChanged() {
            PicturePrint.Files = Files;
            UpdatePagesTrackBar();
        }

        private void UpdatePagesTrackBar() {
            this.repositoryItemTrackBar1.Minimum = 0;
            this.repositoryItemTrackBar1.Maximum = PicturePrint.ItemsSource == null ? 1 : PicturePrint.ItemsSource.Count - 1;
            this.repositoryItemTrackBar1.TickFrequency = this.repositoryItemTrackBar1.Maximum - this.repositoryItemTrackBar1.Minimum;
            this.bePagesTrackBar.Visibility = this.repositoryItemTrackBar1.Maximum < 2 ? DevExpress.XtraBars.BarItemVisibility.Never : DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void galleryControlGallery1_ItemCheckedChanged(object sender, DevExpress.XtraBars.Ribbon.GalleryItemEventArgs e) {
            if(e.Item.Checked)
                UpdatePageLayot();
        }

        SizeF dpiSize;
        protected SizeF DpiSize {
            get {
                if(dpiSize.Width == 0.0f) {
                    using(Graphics g = this.CreateGraphics()) {
                        dpiSize.Width = g.DpiX;
                        dpiSize.Height = g.DpiY;
                    }
                }
                return dpiSize;
            }
        }
        double InchesToPixel(double inches) {
            return inches * DpiSize.Height;
        }
        double CentimeterToPixel(double centimeter) {
            return centimeter * DpiSize.Height / 2.54d;
        }
        double CentimeterToInches(double centimeter) {
            return centimeter / 2.54d;
        }

        private void galleryControlGallery1_ItemCheckedChanged_1(object sender, GalleryItemEventArgs e) {
            if(!e.Item.Checked)
                return;
            PageMediaSize size = (PageMediaSize)e.Item.Value;
            PicturePrint.PrintTicket.PageMediaSize = size;
            PicturePrint.PageWidthPixels = size.Width.Value;
            PicturePrint.PageHeightPixels = size.Height.Value;
            PicturePrint.PaperText = "Paper: " + size.PageMediaSizeName.Value;
        }

        protected double Zoom {
            get { return PicturePrint.Zoom; }
            set {
                PicturePrint.Zoom = value;
                UpdateZoomText();
            }
        }

        void UpdateZoomText() {
            this.bsZoomText.Caption = ((int)(Zoom * 100)).ToString() + "%";
        }

        private void repositoryItemZoomTrackBar1_ValueChanged(object sender, EventArgs e) {
            Zoom = ((ZoomTrackBarControl)sender).Value / 100.0;
        }

        bool UseInches { get { return SettingsStore.Default.UseInches; } }
        double ValueFromEditor(object value) {
            if(value == null)
                return double.NaN;
            if(UseInches) return Convert.ToDouble(value);
            return CentimeterToInches(Convert.ToDouble(value));
        }
        object ValueToEditor(double value) {
            if(double.IsNaN(value))
                return null;
            if(UseInches) return value;
            double res = InchesToCentimeters(value);
            res = ((int)(res * 10 + 0.5)) / 10.0;
            return res;
        }

        private void sePageTop_ValueChanged(object sender, EventArgs e) {
            double value = ValueFromEditor(this.sePageTop.Value);
            foreach(PhotoAssistant.Controls.Wpf.PageInfo pageInfo in PicturePrint.ItemsSource) {
                pageInfo.PageLayout.PageMargins = new System.Windows.Thickness(
                    pageInfo.PageLayout.PageMargins.Left,
                    value,
                    pageInfo.PageLayout.PageMargins.Right,
                    pageInfo.PageLayout.PageMargins.Bottom
                    );
            }
        }

        private void sePhotoIndent_ValueChanged(object sender, EventArgs e) {
            double value = ValueFromEditor(this.sePhotoIndent.Value);
            foreach(PageInfo pageInfo in PicturePrint.ItemsSource) {
                pageInfo.PageLayout.ImageIndent = value;
            }
        }

        private void sePageLeft_ValueChanged(object sender, EventArgs e) {
            double value = ValueFromEditor(this.sePageLeft.Value);
            foreach(PageInfo pageInfo in PicturePrint.ItemsSource) {
                pageInfo.PageLayout.PageMargins = new System.Windows.Thickness(
                    value,
                    pageInfo.PageLayout.PageMargins.Top,
                    pageInfo.PageLayout.PageMargins.Right,
                    pageInfo.PageLayout.PageMargins.Bottom
                );
            }
        }

        private void sePageRight_ValueChanged(object sender, EventArgs e) {
            double value = ValueFromEditor(this.sePageRight.Value);
            foreach(PageInfo pageInfo in PicturePrint.ItemsSource) {
                pageInfo.PageLayout.PageMargins = new System.Windows.Thickness(
                    pageInfo.PageLayout.PageMargins.Left,
                    pageInfo.PageLayout.PageMargins.Top,
                    value,
                    pageInfo.PageLayout.PageMargins.Bottom
                    );
            }
        }

        private void sePageBottom_ValueChanged(object sender, EventArgs e) {
            double value = ValueFromEditor(this.sePageBottom.Value);
            foreach(PageInfo pageInfo in PicturePrint.ItemsSource) {
                pageInfo.PageLayout.PageMargins = new System.Windows.Thickness(
                    pageInfo.PageLayout.PageMargins.Left,
                    pageInfo.PageLayout.PageMargins.Top,
                    pageInfo.PageLayout.PageMargins.Right,
                    value
                    );
            }
        }

        private void rgFit_SelectedIndexChanged(object sender, EventArgs e) {
            SetPhotoParam((item) => item.ImageFit = (Stretch)this.rgFit.EditValue);
        }

        private void ceRotateToFit_CheckedChanged(object sender, EventArgs e) {
            SetPhotoParam((item) => item.RotateToFit = this.ceRotateToFit.Checked);
        }

        private void UpdatePageLayot() {
            if(SelectedLayout == null)
                return;
            PicturePrint.PageLayout = SelectedLayout.LayoutCopy;
            UpdateImageIndent();
            UpdatePagePaddings();
            InitializePhotoLayoutCombo();
        }

        private void UpdatePagePaddings() {
            InitializePaddings();
        }

        private void UpdateImageIndent() {
            if(PicturePrint.SelectedPage == null || PicturePrint.SelectedPage.PageLayout == null)
                return;
            this.sePhotoIndent.EditValue = ValueToEditor(PicturePrint.SelectedPage.PageLayout.ImageIndent);
        }

        private void InitializePhotoLayoutCombo() {
            if(PicturePrint.SelectedPage == null)
                return;
            int lastSelectedIndex = this.cbePhotoLayout.SelectedIndex;
            this.cbePhotoLayout.Properties.Items.BeginUpdate();
            try {
                this.cbePhotoLayout.Properties.Items.Clear();
                int index = 0;
                foreach(PhotoInfo info in PicturePrint.SelectedPage.Files) {
                    this.cbePhotoLayout.Properties.Items.Add(CreatePhotoLayoutItem(info, index + 1));
                    index++;
                }
            } finally {
                this.cbePhotoLayout.Properties.Items.EndUpdate();
            }

            lastSelectedIndex = Math.Max(0, lastSelectedIndex);
            lastSelectedIndex = Math.Min(lastSelectedIndex, this.cbePhotoLayout.Properties.Items.Count - 1);
            this.cbePhotoLayout.SelectedIndex = lastSelectedIndex;
        }

        private object CreatePhotoLayoutItem(PhotoInfo photoInfo, int index) {
            return new PhotoComboBoxItem() { PhotoInfo = photoInfo, PhotoLayout = photoInfo.PhotoLayout, Name = "Item " + index.ToString() };
        }

        public PageLayoutTemplate SelectedLayout {
            get {
                GalleryItem item = this.layoutGallery.Gallery.GetCheckedItem();
                if(item == null)
                    return null;
                return (PageLayoutTemplate)item.Value;
            }
        }

        private void btResetLayout_Click(object sender, EventArgs e) {
            PicturePrint.ResetParams();
            UpdatePageLayot();
        }

        PhotoInfo SelectedPhoto {
            get {
                PhotoComboBoxItem item = (PhotoComboBoxItem)this.cbePhotoLayout.SelectedItem;
                if(item == null)
                    return null;
                return item.PhotoInfo;
            }
        }

        PhotoLayoutTemplate SelectedPhotoLayout {
            get {
                if(SelectedPhoto == null)
                    return null;
                return SelectedPhoto.PhotoLayout;
            }
        }

        private void UpdatePhotoInfoParams() {
            if(SelectedPhotoLayout == null)
                return;
            this.spePhotoWidth.EditValue = ValueToEditor(SelectedPhotoLayout.Width);
            this.spePhotoHeight.EditValue = ValueToEditor(SelectedPhotoLayout.Height);
            this.ceRotateToFit.Checked = SelectedPhotoLayout.RotateToFit;
            this.rgFit.EditValue = SelectedPhotoLayout.ImageFit;
            this.ceShowLabel.Checked = SelectedPhotoLayout.ShowLabel;
            this.ceShowPhotoInfo.Checked = SelectedPhotoLayout.ShowPhotoInfo;
            this.teText.Text = SelectedPhoto.LabelText;
            this.sePadding.EditValue = ValueToEditor(SelectedPhotoLayout.LabelPadding);
            this.cpeFontColor.Color = NormalizedColorToColor(SelectedPhotoLayout.FontColor);
            this.cpeBackgroundColor.Color = NormalizedColorToColor(SelectedPhotoLayout.LabelBackgroundColor);
            this.cpeBorderColor.Color = NormalizedColorToColor(SelectedPhotoLayout.LabelBorderColor);
            this.sbChooseFont.Text = GetFontInfoText(SelectedPhotoLayout);
            this.seBorderThickness.EditValue = ValueToEditor(SelectedPhotoLayout.BorderThickness);
        }

        private string GetFontInfoText(PhotoLayoutTemplate layout) {
            return layout.FontFamily.ToString() + "," + layout.FontSize;
        }

        private System.Drawing.Color NormalizedColorToColor(System.Windows.Media.Color color) {
            return UtilsHelper.NormalizedColorToColor(color);
        }

        private void repositoryItemTrackBar1_ValueChanged(object sender, EventArgs e) {
            PicturePrint.SelectedPageIndex = ((TrackBarControl)sender).Value;
        }

        private void bbiPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            PicturePrint.SelectedPageIndex--;
            bePagesTrackBar.EditValue = PicturePrint.SelectedPageIndex;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            PicturePrint.SelectedPageIndex++;
            bePagesTrackBar.EditValue = PicturePrint.SelectedPageIndex;
        }

        private void cbePhotoLayout_SelectedIndexChanged(object sender, EventArgs e) {
            PicturePrint.SelectedPhotoLayout = this.cbePhotoLayout.SelectedIndex;
            UpdatePhotoInfoParams();
        }

        private void spePhotoWidth_ValueChanged(object sender, EventArgs e) {
            double value = ValueFromEditor(this.spePhotoWidth.Value);
            if(value == 0.0)
                value = double.NaN;
            SetPhotoParam((item) => item.Width = value);
        }

        private void spePhotoHeight_ValueChanged(object sender, EventArgs e) {
            double value = ValueFromEditor(this.spePhotoHeight.Value);
            if(value == 0.0)
                value = double.NaN;
            SetPhotoParam((item) => item.Height = value);
        }

        private void ceShowLabel_CheckedChanged(object sender, EventArgs e) {
            SetPhotoParam((item) => item.ShowLabel = this.ceShowLabel.Checked);
        }

        private void SetPhotoParam(SetPhotoParamDelegate method) {
            if(this.ceApplyToAllPhotos.SelectedIndex == 2) {
                foreach(PageInfo pageInfo in PicturePrint.ItemsSource)
                    pageInfo.Files.ForEach((item) => method(item.PhotoLayout));
            } else if(this.ceApplyToAllPhotos.SelectedIndex == 1) {
                PicturePrint.SelectedPage.Files.ForEach((item) => method(item.PhotoLayout));
            } else if(SelectedPhotoLayout != null)
                method(SelectedPhotoLayout);
        }

        private void ceShowPhotoInfo_CheckedChanged(object sender, EventArgs e) {
            SetPhotoParam((item) => item.ShowPhotoInfo = ceShowPhotoInfo.Checked);
        }

        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e) {
            SetPhotoParam((item) => item.LabelPosition = (ItemPosition)cbPosition.SelectedItem);
        }

        private void sePadding_ValueChanged(object sender, EventArgs e) {
            SetPhotoParam((item) => item.LabelPadding = ValueFromEditor(sePadding.Value));
        }

        private void cpeFontColor_ColorChanged(object sender, EventArgs e) {
            SetPhotoParam((item) => item.FontColor = ColorToNormalizedColor(cpeFontColor.Color));
        }

        private System.Windows.Media.Color ColorToNormalizedColor(System.Drawing.Color color) {
            return UtilsHelper.ColorToNormalizedColor(color);
        }

        private void cpeBackgroundColor_ColorChanged(object sender, EventArgs e) {
            SetPhotoParam((item) => item.LabelBackgroundColor = ColorToNormalizedColor(cpeBackgroundColor.Color));
        }

        private void cpeBorderColor_ColorChanged(object sender, EventArgs e) {
            SetPhotoParam((item) => item.LabelBorderColor = ColorToNormalizedColor(cpeBorderColor.Color));
        }

        private void sbChooseFont_Click(object sender, EventArgs e) {
            if(this.fontDialog1.ShowDialog() != DialogResult.OK)
                return;
            SetPhotoParam((item) => item.FontFamily = new System.Windows.Media.FontFamily(this.fontDialog1.Font.FontFamily.Name));
            SetPhotoParam((item) => item.FontSize = this.fontDialog1.Font.Size);
            SetPhotoParam((item) => item.FontStyle = FontStyleToWpfFontStyle(this.fontDialog1.Font));
            SetPhotoParam((item) => item.FontWeight = FontWeigthToWpfFontWeight(this.fontDialog1.Font));
            this.sbChooseFont.Text = GetFontInfoText(SelectedPhotoLayout);
        }

        private System.Windows.FontWeight FontWeigthToWpfFontWeight(Font font) {
            return UtilsHelper.FontWeigthToWpfFontWeight(font);
        }

        private System.Windows.FontStyle FontStyleToWpfFontStyle(Font font) {
            return UtilsHelper.FontStyleToWpfFontStyle(font);
        }

        private void seBorderThickness_ValueChanged(object sender, EventArgs e) {
            SetPhotoParam((item) => item.BorderThickness = ValueFromEditor(this.seBorderThickness.Value));
        }

        private void btPrint_Click(object sender, EventArgs e) {
            PicturePrint.Print();
        }

        private void btPrintOne_Click(object sender, EventArgs e) {
            PicturePrint.PrintSinglePage();
        }

        bool ISupportPhotoDragDrop.AllowDragDrop {
            get { return true; }
        }
    }

    public delegate void SetPhotoParamDelegate(PhotoLayoutTemplate photoLayout);

    public class PhotoComboBoxItem {
        public PhotoLayoutTemplate PhotoLayout { get; set; }
        public PhotoInfo PhotoInfo { get; set; }
        public string Name { get; set; }
        public override string ToString() {
            return Name;
        }
    }

}
