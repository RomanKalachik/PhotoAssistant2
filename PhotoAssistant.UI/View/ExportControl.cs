using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

using DevExpress.XtraEditors;
using DevExpress.Utils.Serializing;
using System.Collections.ObjectModel;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraSplashScreen;
using System.IO;
using System.Drawing.Imaging;
using PhotoAssistant.Core;
using PhotoAssistant.Core.Model;
using PhotoAssistant.Controls.Wpf;
using PhotoAssistant.UI.ViewHelpers;
using PhotoAssistant.UI.View.ExportControls;

namespace PhotoAssistant.UI.View {
    public partial class ExportControl : ViewControlBase {
        public ExportControl() {
            InitializeComponent();
            this.watermarkPropertyControl1.Watermark = new WatermarkParameters();
            UpdateSaveButtonsEnabledState(false);
        }

        private void UpdateSaveButtonsEnabledState(bool enabled) {
            if(ApplyingExportInfo)
                return;
            this.btnSave.Enabled = this.btnSaveAs.Enabled = enabled;
            if(SelectedExportInfo != null) {
                this.grExportGroup.AllowHtmlText = true;
                this.grExportGroup.Text = "[" + SelectedExportInfo.Name + "] - Settings" + (enabled ? " - <b>Modified</b>" : "");
            }
        }

        public MRUEdit FoldersListCombo { get { return cbeFolder; } }
        public ComboBoxEdit FileRenameMaskCombo { get { return cbeMask; } }
        public ListBoxControl KeywordsListBox { get { return lbcKeywords; } }
        public MRUEdit ExportAppCombo { get { return cbeExportApplication; } }

        private void InitializePresetsTree(ExportInfo info) {
            this.treeList1.DataSource = null;
            this.treeList1.DataSource = CreatePresetsDataSource();
        }

        private object CreatePresetsDataSource() {
            ExportNodeInfo photoAssistantPresets = new ExportNodeInfo() { Name = "PhotoAssistant Presets", ParentId = Guid.Empty };
            ExportNodeInfo userPresets = new ExportNodeInfo() { Name = "User Presets", ParentId = Guid.Empty };
            List<ExportNodeInfo> list = new List<ExportNodeInfo>();
            list.Add(photoAssistantPresets);
            list.Add(userPresets);
            foreach(ExportInfo info in SettingsStore.Default.ExportPresets) {
                ExportNodeInfo node = new ExportNodeInfo();
                node.ParentId = userPresets.Id;
                node.ExportInfo = info;
                list.Add(node);
            }
            return list;
        }

        DmFile previewFile;
        public DmFile PreviewFile {
            get { return previewFile; }
            set {
                if(PreviewFile == value)
                    return;
                previewFile = value;
                OnPreviewFileChanged();
            }
        }

        Image watermarkImage;
        public Image WatermakPreviewImage {
            get { return watermarkImage; }
            set {
                //if(WatermakPreviewImage != null) {
                //    WatermakPreviewImage.Dispose();
                //    watermarkImage = null;
                //}
                watermarkImage = value;

            }
        }
        Image previewImage;
        public Image OriginalPreviewImage {
            get { return previewImage; }
            set {
                //if(OriginalPreviewImage != null) {
                //    OriginalPreviewImage.Dispose();
                //    previewImage = null;
                //}
                previewImage = value;
                FinalPreviewImage = null;
            }
        }
        public System.Windows.Media.ImageSource OriginalPreviewImageSource {
            get;
            set;
        }
        Image finalPreviewImage;
        public Image FinalPreviewImage {
            get { return finalPreviewImage; }
            set {
                if(finalPreviewImage != null) {
                    finalPreviewImage.Dispose();
                    finalPreviewImage = null;
                }
                finalPreviewImage = value;
            }
        }

        private void OnPreviewFileChanged() {
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        protected bool SuppressUpdatePreviewImage { get; set; }

        private void UpdateOriginalPreviewImage() {
            if(PreviewFile == null || SelectedExportInfo == null || SuppressUpdatePreviewImage)
                return;
            PrepareFileResizeManager();
            ResizeInfo resizeInfo = GetFileResizeInfo(PreviewFile);

            Image inImage = ImageLoader.LoadPreviewImage(PreviewFile, () => { return false; }); //TODO BETTER FILE HANDLIG
            OriginalPreviewImage = FileResizeManager.Default.GetImage(inImage, resizeInfo, SelectedExportInfo.GetPixelFormat());
            OriginalPreviewImageSource = ((Bitmap)OriginalPreviewImage).ToWpfBitmap();
        }

        public override void OnQuickGalleryItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e) {
            PreviewFile = (DmFile)e.Item.View.GetRow(e.Item.RowHandle);
            UpdateFinalPreviewImage();
        }

        private void UpdateFinalPreviewImage() {
            if(PreviewFile == null || SelectedExportInfo == null || !SelectedExportInfo.ShowWatermark || SuppressUpdatePreviewImage)
                return;
            this.watermarkPreviewControl1.Image = OriginalPreviewImageSource;
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
            if(Files.Count == 0)
                return;
            PreviewFile = Files[0];
            UpdateFinalPreviewImage();
        }

        public override void OnShowView() {
            base.OnShowView();
            InitializePresetsTree(null);
            InitializeFolderGroup();
            InitializeTemplateCombo();
            InitializeImageResizeModeCombo();
            InitializeImageDimensionCombo();
            InitializeImageResolutionCombo();
            UpdateRenameFilesEnabledState();
            UpdateImageSizingEnabledState();
            UpdateWatermarkEnabledState();
            InitializeAfterExportCombo();
            InitializeExportApplications();
            InitializeImageFormatCombo(SelectedExportInfo);
            AccordionControlHelper.UpdateAccordionControlHeight(this.accordionControl1);
        }

        private void InitializeImageFormatCombo(ExportInfo exportInfo) {
            this.cbeImageFormat.Properties.Items.BeginUpdate();
            try {
                this.cbeImageFormat.Properties.Items.Clear();
                this.cbeImageFormat.Properties.Items.AddEnum<ExportImageFormat>();
                ExportImageFormat format = exportInfo != null ? exportInfo.ImageFormat : ExportImageFormat.JPEG;
                this.cbeImageFormat.SelectedItem = this.cbeImageFormat.Properties.Items.FirstOrDefault((i) => ((ExportImageFormat)i.Value) == format);
            } finally {
                this.cbeImageFormat.Properties.Items.EndUpdate();
            }
            InitializeImageFormatParameters(exportInfo);
        }

        protected ExportImageFormat SelectedImageFormat { get { return (ExportImageFormat)((ImageComboBoxItem)this.cbeImageFormat.SelectedItem).Value; } }

        private void InitializeImageFormatParameters(ExportInfo exportInfo) {
            switch(SelectedImageFormat) {
                case ExportImageFormat.BMP:
                    InitializeImageFormatBmp(exportInfo);
                    break;
                case ExportImageFormat.JPEG:
                    InitializeImageFormatJPEG(exportInfo);
                    break;
                case ExportImageFormat.PNG:
                    InitializeImageFormatPNG(exportInfo);
                    break;
                case ExportImageFormat.PSD:
                    InitializeImageFormatPSD(exportInfo);
                    break;
                case ExportImageFormat.TIFF:
                    InitializeImageFormatTIFF(exportInfo);
                    break;
            }
        }

        private void InitializeImageFormatTIFF(ExportInfo exportInfo) {

        }

        private void InitializeImageFormatPSD(ExportInfo exportInfo) {

        }

        private void InitializeImageFormatPNG(ExportInfo exportInfo) {
            if(exportInfo != null) {
                this.tbCompression.Value = exportInfo.CompressionLevel;
                this.ceLimitFileSize.Checked = exportInfo.IsLimitFileSize;
                this.seLimitFileSize.Value = exportInfo.LimitFileSize;
                this.cbePngBitsPerPixel.EditValue = exportInfo.PngBitsPerChannel.ToString();
            } else {
                this.tbCompression.Value = 85;
                this.ceLimitFileSize.Checked = false;
                this.seLimitFileSize.Value = 200;
                this.cbePngBitsPerPixel.EditValue = 8;
            }
        }

        private void InitializeImageFormatJPEG(ExportInfo exportInfo) {
            if(exportInfo != null) {
                this.tbCompression.Value = exportInfo.CompressionLevel;
                this.ceLimitFileSize.Checked = exportInfo.IsLimitFileSize;
                this.seLimitFileSize.Value = exportInfo.LimitFileSize;
            } else {
                this.tbCompression.Value = 85;
                this.ceLimitFileSize.Checked = false;
                this.seLimitFileSize.Value = 200;
            }
        }

        private void InitializeImageFormatBmp(ExportInfo exportInfo) {

        }

        private void InitializeAfterExportCombo() {
            this.cbeAfterExport.Properties.Items.AddEnum<AfterExportEvent>();
            this.cbeAfterExport.SelectedIndex = 0;
        }

        private void InitializeExportApplications() {
            this.cbeExportApplication.Properties.BeginInit();
            try {
                this.cbeExportApplication.Properties.Items.AddRange(SettingsStore.Default.ExportApplications.ToArray());
            } finally {
                this.cbeExportApplication.Properties.EndUpdate();
            }
            UpdateExportApplicationEnabledState();
        }

        private void InitializeImageResolutionCombo() {
            this.cbResolutionMode.Properties.Items.AddEnum<ResolutionMode>();
            this.cbResolutionMode.SelectedIndex = 0;
        }

        private void InitializeImageDimensionCombo() {
            this.cbImageDimension.Properties.Items.AddEnum<ImageDimensionMode>();
            this.cbImageDimension.SelectedIndex = 0;
        }

        private void InitializeImageResizeModeCombo() {
            this.cbeResizeMode.Properties.Items.AddEnum<FileResizeMode>();
            this.cbeResizeMode.SelectedIndex = 0;
        }

        private void InitializeTemplateCombo() {
            this.cbeMask.Properties.Items.BeginUpdate();
            try {
                this.cbeMask.Properties.Items.Clear();
                foreach(FileRenameTemplateInfo info in SettingsStore.Default.FileRenameTemplates) {
                    this.cbeMask.Properties.Items.Add(info);
                }
            } finally {
                this.cbeMask.Properties.Items.EndUpdate();
                this.cbeMask.SelectedIndex = 0;
            }
        }

        private void InitializeFolderGroup() {
            InitializeFoldersMRU();
            InitializeExistingFilesCombo();
            InitializeSubFolderText();
        }

        private void InitializeSubFolderText() {
            this.teSubFolder.Text = "ExportSubFolder";
        }

        private void InitializeFoldersMRU() {
            this.cbeFolder.Properties.Items.BeginUpdate();
            try {
                this.cbeFolder.Properties.Items.Clear();
                foreach(PathInfo folder in SettingsStore.Default.FoldersToExport)
                    this.cbeFolder.Properties.Items.Add(folder);
            } finally {
                this.cbeFolder.Properties.Items.EndUpdate();
            }
        }

        private void InitializeExistingFilesCombo() {
            this.cbeExistingFiles.Properties.Items.BeginUpdate();
            try {
                this.cbeExistingFiles.Properties.Items.Clear();
                foreach(ExistingFileMode mode in Enum.GetValues(typeof(ExistingFileMode)))
                    this.cbeExistingFiles.Properties.Items.Add(new ImageComboBoxItem(mode.ToString(), mode));
            } finally {
                this.cbeExistingFiles.Properties.Items.EndUpdate();
            }
            this.cbeExistingFiles.SelectedIndex = 0;
        }

        private void cbeFolder_ButtonClick(object sender, ButtonPressedEventArgs e) {
            if(e.Button.Kind == ButtonPredefines.Ellipsis && this.folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                PathInfo info = GetPathInfo(this.folderBrowserDialog1.SelectedPath);
                if(info != null)
                    this.cbeFolder.Properties.Items.Remove(info);
                else
                    info = new PathInfo() { Path = this.folderBrowserDialog1.SelectedPath };
                this.cbeFolder.Properties.Items.Insert(0, info);
                this.cbeFolder.EditValue = info;
                SettingsStore.Default.FoldersToExport.Remove(info);
                SettingsStore.Default.FoldersToExport.Insert(0, info);
                SettingsStore.Default.SaveToXml();
            }
        }

        bool ContainsPath(string path) {
            foreach(PathInfo info in this.cbeFolder.Properties.Items)
                if(info.Path.Equals(path))
                    return true;
            return false;
        }

        PathInfo GetPathInfo(string path) {
            foreach(PathInfo info in this.cbeFolder.Properties.Items)
                if(info.Path.Equals(path))
                    return info;
            return null;
        }

        private void cbeFolder_Properties_RemovingMRUItem(object sender, RemovingMRUItemEventArgs e) {
            SettingsStore.Default.FoldersToExport.Remove((PathInfo)e.Item);
            SettingsStore.Default.SaveToXml();
        }

        public string Folder { get { return this.cbeFolder.Text; } }
        public string SubFolder { get { return this.teSubFolder.Text; } }
        public bool CreateSubFolder { get { return this.ceCreateSubFolder.Checked; } }
        public ExistingFileMode ExistingFileMode { get { return this.cbeExistingFiles.SelectedItem != null ? (ExistingFileMode)((ImageComboBoxItem)this.cbeExistingFiles.SelectedItem).Value : ExistingFileMode.AskUser; } }

        private void meMask_ButtonClick(object sender, ButtonPressedEventArgs e) {
            if(e.Button.Kind == ButtonPredefines.Ellipsis) {
                FileRenameCustomizationDialog dlg = new FileRenameCustomizationDialog();
                dlg.Template = (FileRenameTemplateInfo)this.cbeMask.SelectedItem;
                DialogResult res = dlg.ShowDialog();
                InitializeTemplateCombo();
                if(res == DialogResult.OK)
                    this.cbeMask.SelectedItem = dlg.Template;
                SettingsStore.Default.SaveToXml();
            }
        }

        private void ceMask_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateExampleLabel();
            UpdateKeywordsListBox();
            UdateRenameMaskFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateExampleLabel() {
            FileRenameManager.Default.Template = ((FileRenameTemplateInfo)this.cbeMask.SelectedItem).Template;
            this.lbExample.Text = FileRenameManager.Default.GetExample();
        }

        private void UpdateKeywordsListBox() {
            this.lbcKeywords.Items.BeginUpdate();
            try {
                this.lbcKeywords.Items.Clear();
                foreach(FileRenameValueReference fref in FileRenameManager.Default.TemplateValues) {
                    this.lbcKeywords.Items.Add(fref);
                }
            } finally {
                this.lbcKeywords.Items.EndUpdate();
                this.lbcKeywords.SelectedIndex = 0;
            }
        }

        private void lbcKeywords_SelectedIndexChanged(object sender, EventArgs e) {
            this.propertyGridControl1.SelectedObject = this.lbcKeywords.SelectedItem;
        }

        private void propertyGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e) {
            UpdateExampleLabel();
            UpdateRenameFilesFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void ceRenameFiles_CheckedChanged(object sender, EventArgs e) {
            UpdateRenameFilesEnabledState();
            UpdateRenameFilesFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateRenameFilesEnabledState() {
            this.actRename.ContentContainer.Enabled = this.ceRenameFiles.Checked;
        }

        private void ceAllowImageSizing_CheckedChanged(object sender, EventArgs e) {
            UpdateImageSizingEnabledState();
            UpdateResizeImageFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateImageSizingEnabledState() {
            this.acnImageSizing.ContentContainer.Enabled = this.ceResizeImages.Checked;
        }

        private void ceWatermark_CheckedChanged(object sender, EventArgs e) {
            UpdateWatermarkEnabledState();
            UpdateShowWatermarkFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateWatermarkEnabledState() {
            this.acnWatermarking.ContentContainer.Enabled = this.ceWatermark.Checked;
            this.watermarkPropertyControl1.EnableWatermark = this.ceWatermark.Checked;
            this.watermarkPreviewControl1.Visible = this.ceWatermark.Checked;
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateExportApplicationEnabledState();
            UpdateAfterExportFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        protected virtual void UpdateExportApplicationEnabledState() {
            AfterExportEvent ae = (AfterExportEvent)((ImageComboBoxItem)this.cbeAfterExport.SelectedItem).Value;
            this.cbeExportApplication.Enabled = ae == AfterExportEvent.OpenInApplication;
        }

        private void cbeExportApplication_Properties_RemovingMRUItem(object sender, RemovingMRUItemEventArgs e) {
            if(XtraMessageBox.Show(this, "Remove Application from list?") != DialogResult.Yes) {
                e.Cancel = true;
                return;
            }
            SettingsStore.Default.ExportApplications.Remove((ApplicationInfo)e.Item);
            SettingsStore.Default.SaveToXml();
        }

        private void cbeExportApplication_ButtonClick(object sender, ButtonPressedEventArgs e) {
            if(e.Button.Kind == ButtonPredefines.Combo)
                return;
            ExportApplicationParams dlg = new ExportApplicationParams();
            if(e.Button.Kind == ButtonPredefines.Glyph)
                dlg.ApplicationInfo = (ApplicationInfo)this.cbeExportApplication.SelectedItem;
            else if(e.Button.Kind == ButtonPredefines.Plus)
                dlg.ApplicationInfo = new ApplicationInfo(); ;
            if(dlg.ShowDialog(this) == DialogResult.OK) {
                if(e.Button.Kind == ButtonPredefines.Plus)
                    SettingsStore.Default.ExportApplications.Add(dlg.ApplicationInfo);
                InitializeExportApplications();
                this.cbeExportApplication.SelectedItem = dlg.ApplicationInfo;
                SettingsStore.Default.SaveToXml();
            }
        }

        private void btnNew_Click(object sender, EventArgs e) {
            PresetNameEditor dlg = new PresetNameEditor();
            if(dlg.ShowDialog(this) != DialogResult.OK)
                return;
            ExportInfo info = new ExportInfo();
            info.Name = dlg.TemplateName;
            SettingsStore.Default.ExportPresets.Add(info);
            InitializePresetsTree(info);
            UpdateExportInfoFromEditors(info);
            SettingsStore.Default.SaveToXml();
        }

        protected bool ApplyingExportInfo { get; set; }

        public void ApplyExportInfo(ExportInfo exportInfo) {
            ApplyingExportInfo = true;
            try {
                this.grExportGroup.Text = "[" + exportInfo.Name + "] - Settings";
                this.cbeAfterExport.EditValue = exportInfo.AfterExportEvent;
                this.cbeExportApplication.EditValue = exportInfo.Application;
                this.ceCreateSubFolder.Checked = exportInfo.CreateSubFolder;
                this.ceDontEnlarge.Checked = exportInfo.DontEnlarge;
                this.spDpi.Value = exportInfo.Dpi;
                this.cbeExistingFiles.EditValue = exportInfo.ExistingFileMode;

                this.lbcKeywords.Items.Clear();
                FileRenameTemplateInfo info = SettingsStore.Default.GetFileRenameTemlate(exportInfo.RenameMaskName, exportInfo.RenameMask); ;
                InitializeTemplateCombo();
                this.cbeMask.SelectedItem = info;
                FileRenameManager.Default.Template = exportInfo.RenameMask;
                FileRenameManager.Default.TemplateValues.Clear();
                foreach(FileRenameValueReference fref in exportInfo.FileRenameValues) {
                    FileRenameManager.Default.TemplateValues.Add(fref);
                }

                UpdateExampleLabel();
                UpdateKeywordsListBox();
                InitializeImageFormatCombo(exportInfo);
                InitializeImageFormatParameters(exportInfo);
                UpdateImageFormatParametersGUI();

                PathInfo pInfo = SettingsStore.Default.GetExportFolderInfo(exportInfo.Folder);
                InitializeFoldersMRU();
                this.cbeFolder.EditValue = pInfo;
                this.spHeight.Value = exportInfo.Height;
                this.cbeResizeMode.EditValue = exportInfo.ResizeMode;
                if(exportInfo.ResizeMode == FileResizeMode.LongSide)
                    this.spWidth.Value = exportInfo.LongSide;
                else if(exportInfo.ResizeMode == FileResizeMode.ShortSide)
                    this.spWidth.Value = exportInfo.ShortSide;
                else
                    this.spWidth.Value = exportInfo.Width;
                this.ceRenameFiles.Checked = exportInfo.RenameFiles;
                this.ceResizeImages.Checked = exportInfo.ResizeImages;
                this.cbResolutionMode.EditValue = exportInfo.ResolutionMode;
                this.cbImageDimension.EditValue = exportInfo.ImageDimension;
                this.ceWatermark.Checked = exportInfo.ShowWatermark;
                this.teSubFolder.Text = exportInfo.SubFolder;
                this.watermarkPropertyControl1.Watermark = exportInfo.Watermark.Clone();
                if(!string.IsNullOrEmpty(exportInfo.ApplicationIdString) && !string.IsNullOrEmpty(exportInfo.Application.Name)) {
                    ApplicationInfo ainfo = SettingsStore.Default.GetExportApplication(exportInfo.ApplicationIdString);
                    if(ainfo == null)
                        SettingsStore.Default.ExportApplications.Add(exportInfo.Application);
                }
                InitializeExportApplications();
            } finally {
                ApplyingExportInfo = false;
            }
        }

        protected ExportNodeInfo GetSelectedExportInfo() {
            return (ExportNodeInfo)this.treeList1.GetDataRecordByNode(this.treeList1.FocusedNode);
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e) {
            UpdateExportPresetButtons();

        }

        private void UpdateExportPresetButtons() {
            ExportNodeInfo info = GetSelectedExportInfo();
            if(info == null) {
                this.btnLoad.Enabled = false;
            } else {
                this.btnLoad.Enabled = true;
            }
        }

        private void cbeResizeMode_SelectedIndexChanged(object sender, EventArgs e) {
            FileResizeMode mode = (FileResizeMode)((ImageComboBoxItem)this.cbeResizeMode.SelectedItem).Value;
            if(mode == FileResizeMode.LongSide) {
                this.lciHeight.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciWidth.Text = "Long Side";
            } else if(mode == FileResizeMode.ShortSide) {
                this.lciHeight.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciWidth.Text = "Short Side";
            } else {
                this.lciHeight.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciWidth.Text = "Width";
            }
            UpdateResizeModeFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void btnLoad_Click(object sender, EventArgs e) {
            ExportNodeInfo info = (ExportNodeInfo)this.treeList1.GetDataRecordByNode(this.treeList1.FocusedNode);
            if(info == null)
                return;
            SelectedExportInfo = info.ExportInfo;

        }

        ExportInfo selectedInfo;
        protected ExportInfo SelectedExportInfo {
            get { return selectedInfo; }
            set {
                selectedInfo = value;
                OnSelectedExportInfoChanged();
            }
        }

        private void OnSelectedExportInfoChanged() {
            ApplyExportInfo(SelectedExportInfo);
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            ExportInfo info = SelectedExportInfo;
            if(info == null)
                return;
            if(XtraMessageBox.Show(this, "'" + info.Name + "'" + " already exists. Overwrite?", "Save Export Preset", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            UpdateExportInfoFromEditors(info);
            SettingsStore.Default.SaveToXml();
            UpdateSaveButtonsEnabledState(false);
        }

        private void btnSaveAs_Click(object sender, EventArgs e) {
            PresetNameEditor dlg = new PresetNameEditor();
            if(dlg.ShowDialog(this) != DialogResult.OK)
                return;
            ExportInfo info = SettingsStore.Default.ExportPresets.FirstOrDefault((ee) => ee.Name == dlg.TemplateName);
            if(info != null) {
                if(XtraMessageBox.Show(this, "'" + info.Name + "'" + " already exists. Overwrite?", "Save Export Preset", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            } else {
                info = new ExportInfo();
                info.Name = dlg.TemplateName;
                SettingsStore.Default.ExportPresets.Add(info);
            }
            UpdateExportInfoFromEditors(info);
            SettingsStore.Default.SaveToXml();
            UpdateSaveButtonsEnabledState(false);
        }

        private void treeList1_DoubleClick(object sender, EventArgs e) {
            TreeListHitInfo info = this.treeList1.CalcHitInfo(this.treeList1.PointToClient(Control.MousePosition));
            if(info.Node == null)
                return;
            ExportNodeInfo node = (ExportNodeInfo)this.treeList1.GetDataRecordByNode(info.Node);
            if(node != null)
                SelectedExportInfo = node.ExportInfo;
        }

        private void tbCompression_EditValueChanged(object sender, EventArgs e) {
            this.teCompressionText.EditValue = this.tbCompression.Value;
            UpdateCompressionFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void teCompressionText_EditValueChanged(object sender, EventArgs e) {
            try {
                this.tbCompression.Value = Convert.ToInt32(this.teCompressionText.Text);
            } catch(Exception) {
            }
        }

        private void ceLimitFileSize_CheckedChanged(object sender, EventArgs e) {
            if(this.ceLimitFileSize.Checked) {
                this.lciCompressionLevel.Enabled = false;
                this.lciCompressionTrackBar.Enabled = false;
                this.lciLimitFileSize.Enabled = true;
            } else {
                this.lciCompressionLevel.Enabled = true;
                this.lciCompressionTrackBar.Enabled = true;
                this.lciLimitFileSize.Enabled = false;
            }
            UpdateIsLimitFileSizeFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void cbeImageFormat_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateImageFormatParametersGUI();
            InitializeImageFormatParameters(SelectedExportInfo);
            UpdateImageFormatFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateImageFormatParametersGUI() {
            ExportImageFormat format = (ExportImageFormat)((ImageComboBoxItem)this.cbeImageFormat.SelectedItem).Value;
            switch(format) {
                case ExportImageFormat.JPEG:
                case ExportImageFormat.PNG:
                    this.tgFormatParams.SelectedTabPage = this.lcgJpegPng;
                    this.lciPngBitsPerPixel.Visibility = format == ExportImageFormat.PNG ? LayoutVisibility.Always : LayoutVisibility.Never;
                    break;
                case ExportImageFormat.BMP:
                    this.tgFormatParams.SelectedTabPage = this.lcgBmp;
                    break;
            }
        }

        private void watermarkPropertyControl1_WatermarkChanged(object sender, EventArgs e) {
            //this.watermarkPreviewControl1.WatermarkParams = this.watermarkPropertyControl1.Watermark;
        }

        private void btExport_Click(object sender, EventArgs e) {
            ExportFiles();
        }

        ProgressForm progress;
        ProgressForm ProgressForm {
            get {
                if(progress == null) {
                    progress = new ProgressForm();
                    progress.Caption = "Exporting...";
                    progress.Description = "";
                    progress.Progress = 0;
                }
                return progress;
            }
        }

        private void ExportFiles() {

            PrepareForExport();
            ProgressForm.DialogResult = DialogResult.None;
            ProgressForm.ShowDialog(FindForm(), ProcessFiles);
            ExecuteAfterExportAction();
        }

        private void ExecuteAfterExportAction() {
            if(SelectedExportInfo.AfterExportEvent == AfterExportEvent.DoNothing)
                return;
            else if(SelectedExportInfo.AfterExportEvent == AfterExportEvent.ShowInExplorer) {
                System.Diagnostics.Process.Start(ExportFolder);
            } else if(SelectedExportInfo.AfterExportEvent == AfterExportEvent.OpenInApplication) {
                try {
                    System.Diagnostics.Process.Start(
                        SelectedExportInfo.Application.Path + " " + String.Format(SelectedExportInfo.Application.CommandLine, ExportFolder) + " " + ExportFolder);
                } catch(Exception) {
                }
            }
        }

        private void PrepareForExport() {
            PrepareExportFolder();
            PrepareFileRenameManager();
            PrepareFileResizeManager();
            PrepareWatermarkGenerator();
            PrepareCodec(null, null);
            ExistingFileManager.RememberChoise = false;
            ProgressForm.MaxProgressValue = Files.Count;
        }

        private string GetMimeType(ExportImageFormat imageFormat) {
            switch(imageFormat) {
                case ExportImageFormat.BMP:
                    return "image/bmp";
                case ExportImageFormat.JPEG:
                    return "image/jpeg";
                case ExportImageFormat.PNG:
                    return "image/png";
                case ExportImageFormat.TIFF:
                    return "image/tiff";
            }
            return "";
        }

        private ImageCodecInfo GetEncoderInfo(DmFile file, Image image, ExportImageFormat imageFormat) {
            return GetEncoderInfo(GetMimeType(imageFormat));
        }

        private ImageCodecInfo GetEncoderInfo(Guid guid) {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for(j = 0; j < encoders.Length; ++j) {
                if(encoders[j].FormatID == guid)
                    return encoders[j];
            }
            return null;
        }

        private ImageCodecInfo GetEncoderInfo(String mimeType) {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for(j = 0; j < encoders.Length; ++j) {
                if(encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void PrepareCodec(DmFile file, Image image) {
            CodecInfo = GetEncoderInfo(file, image, SelectedExportInfo.ImageFormat);
            EncoderParameters = GetEncoderParameters(file, image, SelectedExportInfo.ImageFormat);
        }

        private EncoderParameters GetEncoderParameters(DmFile file, Image image, ExportImageFormat imageFormat) {
            List<EncoderParameter> p = new List<EncoderParameter>();
            if(imageFormat == ExportImageFormat.JPEG || imageFormat == ExportImageFormat.PNG) {
                p.Add(new EncoderParameter(Encoder.Quality, (long)SelectedExportInfo.CompressionLevel));
            }
            if(imageFormat == ExportImageFormat.PNG) {
                p.Add(new EncoderParameter(Encoder.ColorDepth, (long)SelectedExportInfo.PngBitsPerChannel * 4));
            }
            EncoderParameters res = new EncoderParameters(p.Count);
            for(int i = 0; i < p.Count; i++) {
                res.Param[i] = p[i];
            }
            return res;
        }

        void ProcessFiles() {
            int index = 0;
            foreach(DmFile file in Files) {
                ExportFile(file, index);
                index++;
                if(ProgressForm.DialogResult == DialogResult.Cancel)
                    break;
            }
            ProgressForm.Hide();
        }

        private void PrepareWatermarkGenerator() {
            if(SelectedExportInfo.ShowWatermark) {
                WatermarkImageGenerator.Default.CreateWatermarkCache(Files);
            }
        }

        private void PrepareFileResizeManager() {
            if(SelectedExportInfo == null)
                return;
            FileResizeManager.Default.Dpi = SelectedExportInfo.ImageDimension == ImageDimensionMode.Pixels ? 96 : SelectedExportInfo.Dpi;
            FileResizeManager.Default.DimensionMode = SelectedExportInfo.ImageDimension;
            FileResizeManager.Default.ResolutionMode = SelectedExportInfo.ResolutionMode;
            FileResizeManager.Default.ResizeMode = SelectedExportInfo.ResizeMode;
            FileResizeManager.Default.ShortSide = SelectedExportInfo.ShortSide;
            FileResizeManager.Default.LongSide = SelectedExportInfo.LongSide;
            FileResizeManager.Default.Width = SelectedExportInfo.Width;
            FileResizeManager.Default.Height = SelectedExportInfo.Height;
        }

        private void PrepareFileRenameManager() {
            if(SelectedExportInfo.RenameFiles) {
                FileRenameManager.Default.Template = SelectedExportInfo.RenameMask;
                FileRenameManager.Default.AssignValues(SelectedExportInfo.FileRenameValues);
            }
        }

        private void PrepareExportFolder() {
            ExportFolder = SelectedExportInfo.Folder;
            if(SelectedExportInfo.CreateSubFolder)
                ExportFolder += "\\" + SelectedExportInfo.SubFolder;
            if(!Directory.Exists(ExportFolder))
                Directory.CreateDirectory(ExportFolder);
        }

        protected string ExportFolder { get; set; }
        protected System.Drawing.Imaging.EncoderParameters EncoderParameters { get; set; }
        protected System.Drawing.Imaging.ImageCodecInfo CodecInfo { get; set; }

        private Image GetExportImage(DmFile file) {
            ResizeInfo resizeInfo = GetFileResizeInfo(file);

            Image watermark = GetWatermark(file, (int)resizeInfo.ImageSize.Width, (int)resizeInfo.ImageSize.Height);
            Image inImage = ImageLoader.LoadPreviewImage(file, () => { return false; }); //TODO BETTER FILE HANDLIG
            if(inImage == null)
                return null;
            Image outImage = FileResizeManager.Default.GetImage(inImage, resizeInfo, SelectedExportInfo.GetPixelFormat());
            ApplyWatermark(outImage, watermark);
            inImage.Dispose();
            return outImage;
        }

        private void ExportFile(DmFile file, int index) {
            string fileName = GetNewFileName(file);
            bool skipSave = false;
            if(fileName != null) {
                if(File.Exists(fileName)) {
                    try {
                        File.Delete(fileName);
                    } catch(Exception) {
                        skipSave = true;
                    }
                }
                if(!skipSave) {
                    Image outImage = GetExportImage(file);
                    outImage.Save(fileName, CodecInfo, EncoderParameters);
                }
            }

            ProgressForm.Description = file.FileName;
            ProgressForm.ImageFileName = file.ThumbFileName;
            ProgressForm.Progress = index;
            Application.DoEvents();
        }

        private void ApplyWatermark(Image outImage, Image watermark) {
            if(watermark == null)
                return;
            using(Graphics g = Graphics.FromImage(outImage)) {
                g.DrawImage(watermark, new Rectangle(0, 0, outImage.Width, outImage.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel);
            }
        }

        private Image GetWatermark(DmFile file, int width, int height) {
            if(!SelectedExportInfo.ShowWatermark)
                return null;
            return WatermarkImageGenerator.Default.GenerateWatermarkImage(SelectedExportInfo.Watermark, width, height, SelectedExportInfo.Dpi, SelectedExportInfo.Dpi).Image;
        }

        private System.Windows.Media.ImageSource GetWatermarkWpf(DmFile file, int width, int height) {
            if(!SelectedExportInfo.ShowWatermark)
                return null;
            return WatermarkImageGenerator.Default.GenerateWatermarkImage(SelectedExportInfo.Watermark, width, height, SelectedExportInfo.Dpi, SelectedExportInfo.Dpi).Bitmap;
        }

        private ResizeInfo GetFileResizeInfo(DmFile file) {
            if(!SelectedExportInfo.ResizeImages)
                return new ResizeInfo(file.WidthPixels, file.HeightPixels);
            return FileResizeManager.Default.GetResizeInfo(file);
        }

        public string ExportFileExtension {
            get {
                switch(SelectedExportInfo.ImageFormat) {
                    case ExportImageFormat.BMP: return "bmp";
                    case ExportImageFormat.JPEG: return "jpg";
                    case ExportImageFormat.PNG: return "png";
                    case ExportImageFormat.PSD: return "psd";
                    case ExportImageFormat.TIFF: return "tiff";
                }
                return "png";
            }
        }

        public string GetNewFileName(DmFile file) {
            string exportFileName = string.Empty;
            FileRenameManager.Default.Extension = ExportFileExtension;
            if(!SelectedExportInfo.RenameFiles)
                exportFileName = ExportFolder + "\\" + file.FileName;
            else
                exportFileName = ExportFolder + "\\" + FileRenameManager.Default.GetFileName(Files, file);
            exportFileName = CheckFileExists(exportFileName);
            return exportFileName;
        }

        private string CheckFileExists(string exportFileName) {
            if(!File.Exists(exportFileName))
                return exportFileName;
            ExistingFileMode mode = SelectedExportInfo.ExistingFileMode;
            if(mode == ExistingFileMode.AskUser)
                mode = AskUserForExistingFile(exportFileName);
            switch(mode) {
                case ExistingFileMode.Skip:
                    return null;
                case ExistingFileMode.OverrideWithoutPrompt:
                    return exportFileName;
                case ExistingFileMode.GenerateNewName:
                    return GenerateNewName(exportFileName);
            }
            return exportFileName;
        }

        private string GenerateNewName(string exportFileName) {
            return ExistingFileManager.Default.GenerateNewName(exportFileName);
        }

        private ExistingFileMode AskUserForExistingFile(string exportFileName) {
            return ExistingFileManager.Default.AskUserForExistingFile(FindForm(), exportFileName);
        }

        private void cbImageDimension_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateImageDimensionFromEditor(SelectedExportInfo);
            UpdateResolutionEditorsVisibility();
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateResolutionEditorsVisibility() {
            if(SelectedExportInfo == null)
                return;
            if(SelectedExportInfo.ImageDimension == ImageDimensionMode.Pixels) {
                this.lciDpi.Visibility = LayoutVisibility.OnlyInCustomization;
                this.lciResolution.Visibility = LayoutVisibility.OnlyInCustomization;
            } else {
                this.lciDpi.Visibility = LayoutVisibility.Always;
                this.lciResolution.Visibility = LayoutVisibility.Always;
            }
        }

        private void UpdateImageDimensionFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.ImageDimension = (ImageDimensionMode)((ImageComboBoxItem)this.cbImageDimension.SelectedItem).Value;
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        public void UpdateExportInfoFromEditors(ExportInfo info) {
            SuppressUpdatePreviewImage = true;

            try {
                UpdateAfterExportFromEditor(info);
                UpdateExportApplicationFromEditor(info);
                UpdateCreateSubFolderFromEditor(info);
                UpdateDpiFromEditor(info);
                UpdateExistingFileModeFromEditor(info);
                UpdateRenameFilesFromEditor(info);

                UpdateFolderFromEditor(info);
                UpdateSubFolderFromEditor(info);
                UpdateHeightFromEditor(info);
                UpdateRenameFilesFromEditor(info);

                UpdateResizeImageFromEditor(info);
                UpdateResizeModeFromEditor(info);
                UpdateDontEnlargeFromEditor(info);
                UpdateResolutionModeFromEditor(info);
                UpdateImageDimensionFromEditor(info);

                UpdateShowWatermarkFromEditor(info);
                UpdateWatermarkFromEditor(info);

                UpdateWidthFromEditor(info);
                UpdateImageFormatFromEditor(info);
                UpdateCompressionFromEditor(info);
                UpdateIsLimitFileSizeFromEditor(info);
                UpdateLimitFileSizeFromEditor(info);
                UpdatePngBitsPerChannelFromEditor(info);
            } finally {
                SuppressUpdatePreviewImage = false;
            }

            OnPreviewFileChanged();
        }

        private void UpdateExportApplicationFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            ApplicationInfo ainfo = ((ApplicationInfo)this.cbeExportApplication.SelectedItem);
            info.ApplicationIdString = ainfo != null ? ainfo.IdString : "";
        }

        private void UpdateAfterExportFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.AfterExportEvent = (AfterExportEvent)((ImageComboBoxItem)this.cbeAfterExport.SelectedItem).Value;
        }

        private void UpdateWatermarkFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            if(info.Watermark == null)
                info.Watermark = new WatermarkParameters();
            info.Watermark.Assign(this.watermarkPropertyControl1.Watermark);
            UpdateFinalPreviewImage();
        }

        private void UpdateShowWatermarkFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.ShowWatermark = this.ceWatermark.Checked;
            UpdateFinalPreviewImage();
        }

        private void UpdateResolutionModeFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.ResolutionMode = (ResolutionMode)((ImageComboBoxItem)this.cbResolutionMode.SelectedItem).Value;
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void UpdateResizeModeFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.ResizeMode = (FileResizeMode)((ImageComboBoxItem)this.cbeResizeMode.SelectedItem).Value;
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void UpdateResizeImageFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.ResizeImages = this.ceResizeImages.Checked;
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void UpdatePngBitsPerChannelFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.PngBitsPerChannel = Convert.ToInt32(this.cbePngBitsPerPixel.SelectedItem);
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void UpdateLimitFileSizeFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.LimitFileSize = Convert.ToInt32(this.seLimitFileSize.Value);
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void UpdateIsLimitFileSizeFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.IsLimitFileSize = this.ceLimitFileSize.Checked;
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void UpdateCompressionFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.CompressionLevel = this.tbCompression.Value;
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void cbeFolder_SelectedValueChanged(object sender, EventArgs e) {
            UpdateFolderFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateFolderFromEditor(ExportInfo info) {
            if(info == null || this.cbeFolder.SelectedItem == null || ApplyingExportInfo)
                return;
            info.Folder = ((PathInfo)this.cbeFolder.SelectedItem).Path;
        }

        private void teSubFolder_TextChanged(object sender, EventArgs e) {
            UpdateSubFolderFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateSubFolderFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.SubFolder = this.teSubFolder.Text;
        }

        private void ceCreateSubFolder_CheckedChanged(object sender, EventArgs e) {
            UpdateCreateSubFolderFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateCreateSubFolderFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.CreateSubFolder = this.ceCreateSubFolder.Checked;
        }

        private void cbeExistingFiles_SelectedValueChanged(object sender, EventArgs e) {
            UpdateExistingFileModeFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateExistingFileModeFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.ExistingFileMode = (ExistingFileMode)((ImageComboBoxItem)this.cbeExistingFiles.SelectedItem).Value;
        }

        private void UpdateRenameFilesFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.RenameFiles = this.ceRenameFiles.Checked;
        }

        private void UdateRenameMaskFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            FileRenameTemplateInfo finfo = (FileRenameTemplateInfo)this.cbeMask.SelectedItem;
            info.RenameMaskName = finfo == null ? "" : finfo.Name;
            info.RenameMask = finfo == null ? "" : finfo.Template;
            info.FileRenameValues.Clear();
            foreach(FileRenameValueReference vref in this.lbcKeywords.Items) {
                info.FileRenameValues.Add(vref);
            }
        }

        private void UpdateImageFormatFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.ImageFormat = (ExportImageFormat)((ImageComboBoxItem)this.cbeImageFormat.SelectedItem).Value;
        }

        private void seLimitFileSize_ValueChanged(object sender, EventArgs e) {
            UpdateLimitFileSizeFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void cbePngBitsPerPixel_SelectedValueChanged(object sender, EventArgs e) {
            UpdatePngBitsPerChannelFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void ceDontEnlarge_CheckedChanged(object sender, EventArgs e) {
            UpdateDontEnlargeFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateDontEnlargeFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.DontEnlarge = this.ceDontEnlarge.Checked;
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void spWidth_ValueChanged(object sender, EventArgs e) {
            UpdateWidthFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateWidthFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.Width = Convert.ToInt32(spWidth.Value);
            info.LongSide = info.Width;
            info.ShortSide = info.Width;
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void spHeight_ValueChanged(object sender, EventArgs e) {
            UpdateHeightFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateHeightFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.Height = Convert.ToInt32(this.spHeight.Value);
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void spDpi_ValueChanged(object sender, EventArgs e) {
            UpdateDpiFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void UpdateDpiFromEditor(ExportInfo info) {
            if(info == null || ApplyingExportInfo)
                return;
            info.Dpi = Convert.ToInt32(this.spDpi.Value);
            UpdateOriginalPreviewImage();
            UpdateFinalPreviewImage();
        }

        private void cbResolutionMode_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateResolutionModeFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void watermarkPropertyControl1_WatermarkParamsChanged(object sender, EventArgs e) {
            UpdateWatermarkFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }

        private void cbeExportApplication_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateExportApplicationFromEditor(SelectedExportInfo);
            UpdateSaveButtonsEnabledState(true);
        }
    }
}
