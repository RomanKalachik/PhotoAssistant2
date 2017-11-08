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
using DevExpress.Utils;
using DevExpress.Data.Filtering;
using System.Windows.Forms.Integration;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using System.Diagnostics;
using DevExpress.XtraTreeList;
using DevExpress.XtraGrid.Views.WinExplorer;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors.ColorWheel;
using DevExpress.LookAndFeel;

using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Views.WinExplorer.ViewInfo;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraSplashScreen;
using DevExpress.Utils.Drawing.Animation;
using PhotoAssistant.Core.Model;
using PhotoAssistant.Controls.Wpf;
using PhotoAssistant.Core;
using PhotoAssistant.UI.ViewHelpers;
using PhotoAssistant.Indexer;

namespace PhotoAssistant.UI.View {
    public partial class LibraryControl : ViewControlBase {
        public LibraryControl(DmModel model, MainForm form) {
            Model = model;
            MainForm = form;
            InitializeComponent();
            InitializePicturePreviewControl();
            InitializeGalleryOptionsImageLoad();
            InitializeGalleryZoomTrackBar();
            SubscribeModelEvents();
            AllowApplySettings = true;
            InitializeThemes();
            RestoreGUISettings();
            InitializeStorageManager();
            ConnectToDataSource();
            this.filePropertiesControl1.UpdateAccordionContainerHeight();
        }

        private void InitializeStorageManager() {
            StorageManager.Default.Model = Model;
        }

        protected override void OnParentChanged(EventArgs e) {
            base.OnParentChanged(e);
            InitializeTaskBarAssistant();
        }

        protected RepositoryItemImageComboBox FillWithComboBoxItem { get { return MainForm.riFillWithComboBoxItem; } }
        protected RepositoryItemImageComboBox ColorLabelComboBoxItem { get { return MainForm.riColorLabelComboBoxItem; } }
        protected virtual void InitializeQuickGalleryEditors() {
            FillWithComboBoxItem.BeginUpdate();
            FillWithComboBoxItem.Items.AddEnum<QuickGalleryMode>();
            FillWithComboBoxItem.EndUpdate();
            FillWithComboBoxItem.EditValueChanged += riFillWithComboBoxItem_EditValueChanged;
            ColorLabelComboBoxItem.EditValueChanged += riColorLabelComboBoxItem_EditValueChanged;
            ColorLabelComboBoxItem.BeginUpdate();
            ColorLabelComboBoxItem.Items.Clear();
            ColorLabelComboBoxItem.SmallImages = ColorLabelImagesCreator.CreateColorLabelsImageCollection(Model);
            IEnumerable<DmColorLabel> labels = Model.GetColorLabels();
            int index = 1;
            ColorLabelComboBoxItem.Items.Add(new ImageComboBoxItem(DmColorLabel.NoneString, null, 0));
            DmColorLabel selectedLabel = null;
            foreach(DmColorLabel label in labels) {
                ColorLabelComboBoxItem.Items.Add(new ImageComboBoxItem(label.Text, label, index));
                if(string.Equals(label.Text, SettingsStore.Default.QuickGalleryColorLabel))
                    selectedLabel = label;
                index++;
            }
            ColorLabelComboBoxItem.EndUpdate();
            SkipUpdateQuickGalleryDataSource = true;
            ColorLabelItem.EditValue = selectedLabel;
            FillWithItem.EditValue = SettingsStore.Default.QuickGalleryMode;
            SkipUpdateQuickGalleryDataSource = false;
            UpdateQuickGalleryDataSource();
        }

        protected BarEditItem ColorLabelItem { get { return MainForm.beColorLabel; } }
        protected BarEditItem FillWithItem { get { return MainForm.beFillWith; } }
        protected TileView QuickGalleryView { get { return MainForm.quickGalleryView; } }
        protected GridControl QuickGalleryGridControl { get { return MainForm.quickGalleryGridControl; } }

        void riColorLabelComboBoxItem_EditValueChanged(object sender, EventArgs e) {
            DmColorLabel label = (DmColorLabel)((ImageComboBoxItem)((ImageComboBoxEdit)sender).SelectedItem).Value;
            SettingsStore.Default.QuickGalleryColorLabel = label == null ? DmColorLabel.NoneString : label.Text;
            UpdateQuickGalleryDataSource();
        }

        void riFillWithComboBoxItem_EditValueChanged(object sender, EventArgs e) {
            SettingsStore.Default.QuickGalleryMode = (QuickGalleryMode)((ImageComboBoxItem)((ImageComboBoxEdit)sender).SelectedItem).Value;
            UpdateQuickGalleryDataSource();
        }

        bool SkipUpdateQuickGalleryDataSource { get; set; }
        void UpdateQuickGalleryDataSource() {
            if(SkipUpdateQuickGalleryDataSource)
                return;
            this.galleryExplorerView.DisableCurrencyManager = true;
            QuickGalleryGridControl.DataSource = this.galleryGridControl.DataSource;
            ColorLabelItem.Visibility = SettingsStore.Default.QuickGalleryMode == QuickGalleryMode.LabeledItems ? BarItemVisibility.Always : BarItemVisibility.Never;
            switch(SettingsStore.Default.QuickGalleryMode) {
                case QuickGalleryMode.SelectedItems:
                    QuickGalleryGridControl.DataSource = GetSelectedGalleryItems();
                    QuickGalleryView.ActiveFilterCriteria = null;
                    break;
                case QuickGalleryMode.MarkedItems:
                    QuickGalleryGridControl.DataSource = Model.GetMarkedItems();
                    //this.galleryGridControl.DataSource;
                    //QuickGalleryView.ActiveFilterCriteria = null;
                    //QuickGalleryView.ActiveFilterCriteria = CreateMarkedItemsCriteria();
                    break;
                case QuickGalleryMode.LabeledItems:
                    DmColorLabel label = Model.GetColorLabel(SettingsStore.Default.QuickGalleryColorLabel);
                    QuickGalleryGridControl.DataSource = Model.GetLabeledItems(label);
                    //    this.galleryGridControl.DataSource;
                    //ColorLabel label = Model.GetColorLabel(SettingsStore.Default.QuickGalleryColorLabel);
                    //QuickGalleryView.ActiveFilterCriteria = null;
                    //QuickGalleryView.ActiveFilterCriteria = CreateColorLabelCriteria(label);
                    break;
            }
        }

        private CriteriaOperator CreateColorLabelCriteria(DmColorLabel label) {
            BinaryOperator op = new BinaryOperator("ColorLabel", label);
            return op;
        }

        private CriteriaOperator CreateMarkedItemsCriteria() {
            BinaryOperator op = new BinaryOperator("Marked", true);
            return op;
        }

        private CriteriaOperator CreateSelectedItemsCriteria() {
            BinaryOperator op = new BinaryOperator("IsSelected", true);
            return op;
        }
        public static DevExpress.Utils.ImageContentAnimationType ConvertAnimationType(PhotoAssistant.Core.Model.ImageContentAnimationType type) {
            return (DevExpress.Utils.ImageContentAnimationType)((int)type);
        }
        private void InitializeGalleryOptionsImageLoad() {
            this.galleryExplorerView.OptionsImageLoad.AnimationType = ConvertAnimationType(SettingsStore.Default.ImageAnimationType);
            this.galleryExplorerView.OptionsImageLoad.DesiredThumbnailSize = SettingsStore.Default.ThumbSize;
            QuickGalleryView.OptionsImageLoad.AnimationType = ConvertAnimationType(SettingsStore.Default.ImageAnimationType);
            QuickGalleryView.OptionsImageLoad.DesiredThumbnailSize = SettingsStore.Default.ThumbSize;
        }

        private void InitializeGalleryZoomTrackBar() {
            ((RepositoryItemZoomTrackBar)this.beGalleryZoom.Edit).Minimum = 128;
            ((RepositoryItemZoomTrackBar)this.beGalleryZoom.Edit).Maximum = SettingsStore.Default.ThumbSize.Width;
            ((RepositoryItemZoomTrackBar)this.beGalleryZoom.Edit).Middle = (((SettingsStore.Default.ThumbSize.Width + 128) / 2) / 64) * 64;
        }

        private DevExpress.Utils.Taskbar.TaskbarAssistant taskbarAssistant1;
        private void InitializeTaskBarAssistant() {
            this.taskbarAssistant1 = new DevExpress.Utils.Taskbar.TaskbarAssistant();
            this.taskbarAssistant1.ParentControl = FindForm();
        }

        internal SplitPicturePreviewControl PicturePreview { get; set; }
        ElementHost PicturePreviewHost { get; set; }

        private void InitializePicturePreviewControl() {
            PicturePreviewHost = new ElementHost();
            PicturePreviewHost.Dock = DockStyle.Fill;
            PicturePreview = new SplitPicturePreviewControl();
            PicturePreviewHost.Child = PicturePreview;
            this.splitContainerControl1.Panel1.Controls.Add(PicturePreviewHost);
        }

        int Count { get; set; }
        private void SubscribeModelEvents() {
            Model.FilesRowDataChanged += Model_FilesRowDataChanged;
            Model.FilterValuesChanged += Model_FilterValuesChanged;
            Model.FilterStateChanged += Model_FilterStateChanged;
        }

        void Model_FilterStateChanged(object sender, EventArgs e) {
            UpdateGalleryGridData();
        }

        void Model_FilterValuesChanged(object sender, EventArgs e) {
            if(ProcessingFiles)
                BeginInvoke(new MethodInvoker(OnModelFilterValuesChangedCore));
            else
                OnModelFilterValuesChangedCore();
            UpdateQuickGalleryDataSource();
        }

        private void OnModelFilterValuesChangedCore() {
            InitializeFiltersTree();
            UpdateGalleryGridData();
            UpdateQuickGalleryDataSource();
        }

        void Model_FilesRowDataChanged(object sender, EventArgs e) {
            InitializeFiltersTree();
            this.filePropertiesControl1.FileInfo = GetFocusedGalleryItem();
            UpdateQuickGalleryDataSource();
        }

        void UpdateGalleryGridData() {
            this.galleryGridControl.DataSource = null;
            this.galleryGridControl.DataSource = Model.GetActualGalleryDataSource();
            this.galleryGridControl.Refresh();
            this.filePropertiesControl1.FileInfo = GetFocusedGalleryItem();
            this.bsStatus.Caption = "Update Database " + Count;
            Count++;
        }

        private void InitializeFilePropertiesControl() {
            this.filePropertiesControl1.Model = Model;
        }

        public bool AllowApplySettings { get; set; }

        private void RestoreGUISettings() {
            this.beGalleryZoom.EditValue = SettingsStore.Default.Zoom;
            UpdateGridByViewStyle();
            UpdatePreviewLocationSettings();
            UpdatePreviewModeSettings();
            UpdatePreviewSettings();
        }

        private void UpdatePreviewSettings() {
            OnShowPreviewChanged();
            this.splitContainerControl1.SplitterPosition = SettingsStore.Default.PreviewPanelSize;
        }

        private void UpdatePreviewLocationSettings() {
            UpdatePreviewLocationMenuGlyph();
            OnPreviewLocationChanged();
        }

        private void UpdatePreviewModeSettings() {
            OnPreviewModeChanged();
        }

        protected virtual void UpdatePreviewLocationMenuGlyph() {
            switch(PreviewLocation) {
                case PreviewLocation.Default:
                    this.bsPreviewLocation.Glyph = this.bcPreviewAlignDefault.Glyph;
                    this.bsPreviewLocation.LargeGlyph = this.bcPreviewAlignDefault.LargeGlyph;
                    break;
                case PreviewLocation.Left:
                    this.bsPreviewLocation.Glyph = this.bcPreviewAlignLeft.Glyph;
                    this.bsPreviewLocation.LargeGlyph = this.bcPreviewAlignLeft.LargeGlyph;
                    break;
                case PreviewLocation.Right:
                    this.bsPreviewLocation.Glyph = this.bcPreviewAlignRight.Glyph;
                    this.bsPreviewLocation.LargeGlyph = this.bcPreviewAlignRight.LargeGlyph;
                    break;
                case PreviewLocation.Top:
                    this.bsPreviewLocation.Glyph = this.bcPreviewAlignTop.Glyph;
                    this.bsPreviewLocation.LargeGlyph = this.bcPreviewAlignTop.LargeGlyph;
                    break;
            }
        }

        protected virtual void UpdateGridByViewStyle() {
            OnViewStyleChanged();
        }

        private void UpdateGridByZoom() {
            if(Model.Properties == null)
                return;
            this.galleryExplorerView.OptionsViewStyles.Large.ImageSize = new Size(SettingsStore.Default.Zoom, (int)(SettingsStore.Default.Zoom / Model.Properties.AspectRatio));
            this.galleryGridView.RowHeight = SettingsStore.Default.Zoom / 2;
        }

        protected internal virtual void UpdateFormCaption() {
            if(SettingsStore.Default.ActiveProject != null && !string.IsNullOrEmpty(SettingsStore.Default.ActiveProject.Name)) {
                MainForm.Text = SettingsStore.Default.ActiveProject.Name + " - " + SettingsStore.ApplicationName;
            } else {
                MainForm.Text = Path.GetFileName(SettingsStore.Default.CurrentDataSource) + " - " + SettingsStore.ApplicationName;
            }
        }

        private void CheckUpgradeDataSourceVesrion() {
            Model.CheckUpgradeDataSourceVersion();
        }

        public void OpenProject(ProjectInfo info) {
            if(SettingsStore.Default.Projects.Contains(info))
                SettingsStore.Default.Projects.Remove(info);
            SettingsStore.Default.Projects.Insert(0, info);
            if(SettingsStore.Default.ActiveProject == info)
                return;
            SettingsStore.Default.ActiveProject = info;
            SettingsStore.Default.SaveToXml();
            SplashScreenManager.ShowDefaultWaitForm("Opening project. Please wait.");
            ConnectToDataSource();
            SplashScreenManager.CloseDefaultWaitForm();
        }

        private void ConnectToDataSource() {
            string res = Model.ConnectToDataSource();
            if(!string.IsNullOrEmpty(res)) {
                ShowError(res);
                return;
            }
            if(SettingsStore.Default.SaveProjectParameters)
                SaveProjectParameters();
            StorageManager.Default.UpdateVolumeMountNames();
            UpdateFormCaption();
            CheckUpgradeDataSourceVesrion();
            InitializeGalleryGrid();
            InitializeFiltersTree();
            InitializeFilePropertiesControl();
            UpdateGridByZoom();
            InitializeQuickGalleryEditors();
            InitializeColorLabelPopupMenu();

        }

        private void SaveProjectParameters() {
            SettingsStore.Default.SaveProjectParameters = false;
            Model.SaveProjectParameters(SettingsStore.Default.ActiveProject);
        }

        private void InitializeFiltersTree() {
            this.filterTree.DataSource = Model.GetVisibleFilters();
            this.filterTree.RootValue = Model.GetFilterRootId();
            this.filterTree.ExpandAll();
        }

        private void ConnectToDataSource(string dataSource) {
            SettingsStore.Default.CurrentDataSource = dataSource;
            ConnectToDataSource();
        }

        private bool OpenDataSource(string dataSource) {
            return Model.OpenDataSource(dataSource, false);
        }

        private void ShowError(string errorText) {
            XtraMessageBox.Show(errorText);
        }

        private bool CreateDefaultDataSource() {
            return Model.CreateDefaultDataSource();
        }

        private void InitializeGalleryGrid() {
            this.galleryGridControl.DataSource = null;
            this.galleryExplorerView.Columns.Clear();
            this.galleryExplorerView.Columns.Add(new GridColumn() { FieldName = "Path" });
            this.galleryExplorerView.Columns.Add(new GridColumn() { FieldName = "Caption" });
            this.galleryExplorerView.Columns.Add(new GridColumn() { FieldName = "ThumbImage" });
            this.galleryExplorerView.Columns.Add(new GridColumn() { FieldName = "Description" });
            //this.galleryExplorerView.ColumnSet.TextColumn = this.galleryExplorerView.Columns[1];
            this.galleryExplorerView.ColumnSet.DescriptionColumn = this.galleryExplorerView.Columns[3];
            this.galleryExplorerView.ColumnSet.LargeImageColumn = this.galleryExplorerView.Columns[2];
            this.galleryExplorerView.ColumnSet.MediumImageColumn = this.galleryExplorerView.Columns[2];

            this.galleryGridControl.DataSource = Model.GetActualGalleryDataSource();
        }

        private void InitializeThemes() {
            SkinHelper.InitSkinGallery(this.rgbThemes);
            UserLookAndFeel.Default.SkinName = SettingsStore.Default.SelectedThemeName;
            UserLookAndFeel.Default.SkinMaskColor = SettingsStore.Default.MaskColor;
            UserLookAndFeel.Default.SkinMaskColor2 = SettingsStore.Default.MaskColor2;
        }

        private void SaveSettings() {
            SettingsStore.Default.SelectedThemeName = this.rgbThemes.Gallery.GetCheckedItem().Tag.ToString();
            SettingsStore.Default.SaveToXml();
        }

        private void bbColorWheel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowColorWheelForm();
        }

        private void ShowColorWheelForm() {
            using(ColorWheelForm form = new DevExpress.XtraEditors.ColorWheel.ColorWheelForm()) {
                form.SkinMaskColor = SettingsStore.Default.MaskColor;
                form.SkinMaskColor2 = SettingsStore.Default.MaskColor2;
                if(form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
                    SettingsStore.Default.MaskColor = UserLookAndFeel.Default.SkinMaskColor;
                    SettingsStore.Default.MaskColor2 = UserLookAndFeel.Default.SkinMaskColor2;
                }
            }
        }

        Stopwatch Stopwatch { get; set; }
        private void AddFiles() {
            using(AddFilesForm form = new AddFilesForm(Model)) {
                if(form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                CommitFilesToModel(form);
            }
        }
        void CommitFilesToModel(AddFilesForm importForm) {
            if(importForm == null) return;
            //Indexer.Indexer indexer = new Indexer.Indexer(OnIndexerProcessFile, OnIndexerProcessStarted, OnIndexerProcessCompleted);
            Indexer.Indexer indexer = new Indexer.Indexer();
            indexer.Model = Model;
            indexer.ThumbSize = SettingsStore.Default.ThumbSize;
            indexer.PreviewSize = SettingsStore.Default.PreviewSize;
            //indexer.StorageManager = StorageManager.Default;
            //indexer.StorageManagerDialogsProvider = StorageManagerDialogsProvider.Default;
            Stopwatch = new System.Diagnostics.Stopwatch();
            Stopwatch.Start();
            indexer.ProcessFiles(importForm.GetFilesToAdd().Select(file => file.ImportPath).ToArray());
        }

        NotificationForm addFileInfoForm;
        protected NotificationForm AddFileInfoForm {
            get {
                if(addFileInfoForm == null)
                    addFileInfoForm = new NotificationForm();
                return addFileInfoForm;
            }
        }
        protected bool ProcessingFiles { get; set; }
        private void OnIndexerProcessStarted(int count) {
            ProcessingFiles = true;
            this.beProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ((RepositoryItemProgressBar)beProgress.Edit).Maximum = count;
            this.beProgress.EditValue = 0;
            this.taskbarAssistant1.ProgressMaximumValue = count;
            this.taskbarAssistant1.ProgressCurrentValue = 0;
            this.taskbarAssistant1.ProgressMode = DevExpress.Utils.Taskbar.Core.TaskbarButtonProgressMode.Normal;
        }

        private void OnIndexerProcessCompleted() {
            Stopwatch.Stop();
            this.beProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.taskbarAssistant1.ProgressMode = DevExpress.Utils.Taskbar.Core.TaskbarButtonProgressMode.NoProgress;
            InitializeFiltersTree();
            UpdateGalleryGridData();
            ProcessingFiles = false;
            AddFileInfoForm.HideInfo();
            AddFileInfoForm.IsClosed = false;
            this.bsStatus.Caption = "ProcessTime = " + Stopwatch.ElapsedMilliseconds;
            Stopwatch = null;
            Model.CalcAspectRatio();
            UpdateGridByZoom();
        }

        public void OnIndexerProcessFile(DmFile file, int progress, int count) {
            this.beProgress.EditValue = progress;
            this.bsStatus.Caption = "Processing file: " + file.ImportPath;
            this.taskbarAssistant1.ProgressCurrentValue = progress;
            //if(IsApplicationInactive) {
            if(!AddFileInfoForm.IsClosed)
                AddFileInfoForm.ShowInfo(MainForm, file.ThumbImage, string.Format(MessageStrings.ProcessingFileText, progress, count), file.ImportPath);
            //}
        }

        public bool IsApplicationInactive {
            get {
                return MainForm.WindowState == FormWindowState.Minimized;
            }
        }

        protected virtual void OnGalleryZoomChanged(int value) {
            SettingsStore.Default.Zoom = value;
            this.galleryExplorerView.OptionsBehavior.EnableSmoothScrolling = SettingsStore.Default.Zoom < 192 ? false : true;
            UpdateGridByZoom();
        }

        private void riGalleryZoomTrackBar_EditValueChanged(object sender, EventArgs e) {
            OnGalleryZoomChanged(((ZoomTrackBarControl)sender).Value);
        }

        private void beGalleryZoom_EditValueChanged(object sender, EventArgs e) {
            OnGalleryZoomChanged((int)this.beGalleryZoom.EditValue);
        }

        private void UpdateButtonsState() {

        }

        private void bbDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(XtraMessageBox.Show(this, MessageStrings.DeleteImagesConfirmation, SettingsStore.ApplicationName, MessageBoxButtons.YesNoCancel) != System.Windows.Forms.DialogResult.Yes)
                return;
            this.galleryGridControl.MainView.BeginUpdate();
            try {
                Model.RemoveFiles(GetSelectedGalleryItems());
            } finally {
                this.galleryGridControl.MainView.EndUpdate();
            }
        }

        protected int[] GetSelectedRows() {
            if(this.galleryGridControl.MainView == this.galleryExplorerView)
                return this.galleryExplorerView.GetSelectedRows();
            return this.galleryGridView.GetSelectedRows();
        }

        protected object GetRow(int rowHandle) {
            if(this.galleryGridControl.MainView == this.galleryExplorerView)
                return this.galleryExplorerView.GetRow(rowHandle);
            return this.galleryGridView.GetRow(rowHandle);
        }

        protected virtual List<DmFile> GetSelectedGalleryItems() {
            int[] selRows = GetSelectedRows();
            List<DmFile> items = new List<DmFile>(selRows.Length);
            for(int i = 0; i < selRows.Length; i++)
                items.Add((DmFile)GetRow(selRows[i]));
            return items;
        }

        //private void bbNewProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
        //    ProjectsControl.CreateNewProjectCore(this);
        //this.openProjectDialog.CheckPathExists = true;
        //this.openProjectDialog.CheckFileExists = false;
        //if(this.openProjectDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
        //    ConnectToDataSource(this.openProjectDialog.FileName);
        //    ProjectsControl.CreateNewProjectCore(this);
        //    //LoadProjectFromDataSource();
        //}
        //}

        private void LoadProjectFromDataSource() {
            ProjectInfo prj = SettingsStore.Default.Projects.FirstOrDefault((p) => p.Id == Model.Properties.ProjectId);
            if(prj == null) {
                prj = new ProjectInfo();
                prj.Id = Model.Properties.ProjectId;
                prj.FileName = SettingsStore.Default.CurrentDataSource;
                prj.Name = Model.Properties.ProjectName;
                prj.Description = Model.Properties.ProjectDescription;
                prj.FileCount = Model.Properties.ProjectFileCount;
                prj.OpenCount = Model.Properties.ProjectOpenCount;
                SettingsStore.Default.Projects.Insert(0, prj);
            }
            SettingsStore.Default.ActiveProject = prj;
            SettingsStore.Default.CurrentDataSource = null;
        }

        //private void bbOpenProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
        //    MainForm.ProjectsControl.OpenProject();
        //this.openProjectDialog.CheckPathExists = true;
        //this.openProjectDialog.CheckFileExists = true;
        //if(this.openProjectDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
        //    ConnectToDataSource(this.openProjectDialog.FileName);
        //    LoadProjectFromDataSource();
        //}
        //}

        private void bbCloseProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SettingsStore.Default.ActiveProject = null;
            DisconnectFromDataSource();
        }

        private void DisconnectFromDataSource() {
            Model.DisconnectDataSource();
            InitializeGalleryGrid();
        }

        void SelectAll() {
            if(this.galleryGridControl.MainView == this.galleryExplorerView)
                this.galleryExplorerView.SelectAll();
            else
                this.galleryGridView.SelectAll();
        }

        private void bbSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SelectAll();
        }

        void DeselectAll() {
            if(this.galleryGridControl.MainView == this.galleryExplorerView)
                this.galleryExplorerView.ClearSelection();
            else
                this.galleryGridView.ClearSelection();
        }

        private void bbDeselectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            DeselectAll();
        }

        private void bbInvertSelection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.galleryGridControl.MainView.BeginUpdate();
            try {
                for(int i = 0; i < this.galleryGridControl.MainView.DataRowCount; i++) {
                    InvertSelection(i);
                }
            } finally {
                this.galleryGridControl.MainView.EndUpdate();
            }
        }

        private void InvertSelection(int rowHandle) {
            if(this.galleryGridControl.MainView == this.galleryExplorerView)
                this.galleryExplorerView.InvertRowSelection(rowHandle);
            else
                this.galleryGridView.InvertRowSelection(rowHandle);
        }

        private void OnViewItemCheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcThumbnailsView.Checked)
                ViewStyle = GalleryViewStyle.Thumbnails;
            else if(this.bcDetailsView.Checked)
                ViewStyle = GalleryViewStyle.Detail;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected GalleryViewStyle ViewStyle {
            get { return SettingsStore.Default.ViewStyle; }
            set {
                if(ViewStyle == value)
                    return;
                SettingsStore.Default.ViewStyle = value;
                OnViewStyleChanged();
            }
        }
        FilterOperationType filterOperation = FilterOperationType.And;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected FilterOperationType FilterOperation {
            get { return filterOperation; }
            set {
                if(FilterOperation == value)
                    return;
                filterOperation = value;
                OnFilterOperationChanged(true);
            }
        }

        protected virtual void OnFilterOperationChanged(bool updateCheckItems) {
            if(FilterOperation == FilterOperationType.Or)
                if(updateCheckItems) this.bcMatchAny.Checked = true;
                else
                    if(updateCheckItems) this.bcMatchAll.Checked = true;
            this.filterTree.BeginUpdate();
            this.filterTree.EndUpdate();
        }

        private void OnViewStyleChanged() {
            switch(ViewStyle) {
                case GalleryViewStyle.Thumbnails:
                    this.galleryGridControl.MainView = this.galleryExplorerView;
                    this.bcThumbnailsView.Checked = true;
                    break;
                case GalleryViewStyle.Detail:
                    this.galleryGridControl.MainView = this.galleryGridView;
                    this.bcDetailsView.Checked = true;
                    break;
            }
            OnPreviewLocationChanged();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected PreviewLocation PreviewLocation {
            get { return SettingsStore.Default.PreviewLocation; }
            set {
                if(PreviewLocation == value)
                    return;
                SettingsStore.Default.PreviewLocation = value;
                OnPreviewLocationChanged();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected bool ShowPreview {
            get { return SettingsStore.Default.ShowPreview; }
            set {
                if(ShowPreview == value)
                    return;
                SettingsStore.Default.ShowPreview = value;
                OnShowPreviewChanged();
            }
        }

        private void OnShowPreviewChanged() {
            this.bcShowPreview.Checked = SettingsStore.Default.ShowPreview;
            this.splitContainerControl1.Collapsed = !SettingsStore.Default.ShowPreview;
        }
        protected virtual PreviewLocation GetActualPreviewLocation() {
            if(PreviewLocation != PreviewLocation.Default)
                return PreviewLocation;
            if(ViewStyle == GalleryViewStyle.Detail)
                return PreviewLocation.Top;
            return PreviewLocation.Left;
        }
        private void OnPreviewLocationChanged() {
            UpdatePreviewLocationMenuGlyph();
            if(GetActualPreviewLocation() == PreviewLocation.Top) {
                this.splitContainerControl1.Horizontal = false;
                this.splitContainerControl1.Panel1.Controls.Add(PicturePreviewHost);
                this.splitContainerControl1.Panel2.Controls.Add(this.galleryGridControl);
            } else {
                this.splitContainerControl1.Horizontal = true;
                if(GetActualPreviewLocation() == PreviewLocation.Left) {
                    this.splitContainerControl1.Panel1.Controls.Add(PicturePreviewHost);
                    this.splitContainerControl1.Panel2.Controls.Add(this.galleryGridControl);
                } else {
                    this.splitContainerControl1.Panel2.Controls.Add(PicturePreviewHost);
                    this.splitContainerControl1.Panel1.Controls.Add(this.galleryGridControl);
                }
            }
        }

        private void bcShowPreview_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowPreview = this.bcShowPreview.Checked;
        }

        private void splitContainerControl1_SplitGroupPanelCollapsed(object sender, SplitGroupPanelCollapsedEventArgs e) {
            this.bcShowPreview.Checked = !e.Collapsed;
        }
        protected DmFile GetFocusedGalleryItem() {
            return (DmFile)this.galleryExplorerView.GetFocusedRow();
        }
        protected DmFile GetNextToFocusedGalleryItem() {
            return (DmFile)this.galleryExplorerView.GetRow(this.galleryExplorerView.FocusedRowHandle + 1);
        }
        protected DmFile GetPrevToFocusedGalleryItem() {
            if(this.galleryExplorerView.FocusedRowHandle == 0)
                return null;
            return (DmFile)this.galleryExplorerView.GetRow(this.galleryExplorerView.FocusedRowHandle - 1);
        }
        public DmFile LeftCandidate { get; set; }
        public DmFile RightCandidate { get; set; }
        public DmFile CurrentFile { get; set; }
        protected virtual void UpdateProperties() {
            CurrentFile = GetFocusedGalleryItem();
            if(SettingsStore.Default.PreviewMode == SplitPicturePreviewType.Two ||  SettingsStore.Default.PreviewMode == SplitPicturePreviewType.Three)
                RightCandidate = GetNextToFocusedGalleryItem();
            else
                RightCandidate = null;
            if(SettingsStore.Default.PreviewMode == SplitPicturePreviewType.Three)
                LeftCandidate = GetPrevToFocusedGalleryItem();
            else
                LeftCandidate = null;
            UpdatePicturePreview();
            this.filePropertiesControl1.FileInfo = CurrentFile;
        }
        private void UpdatePicturePreview() {
            if(!IsPicturePreviewVisible) {
                PicturePreview.SetFiles(null, null, null);
                return;
            }
            PicturePreview.LayoutMode = CalcPicturePreviewLayoutMode();
            if(SettingsStore.Default.PreviewMode == SplitPicturePreviewType.Single)
                PicturePreview.SetFiles(CurrentFile, LeftCandidate, RightCandidate);
            else if(SettingsStore.Default.PreviewMode == SplitPicturePreviewType.Two)
                PicturePreview.SetFiles(CurrentFile, RightCandidate, null);
            else if(SettingsStore.Default.PreviewMode == SplitPicturePreviewType.Three)
                PicturePreview.SetFiles(CurrentFile, LeftCandidate, RightCandidate);
        }

        private PreviewPictureLayoutMode CalcPicturePreviewLayoutMode() {
            if(SettingsStore.Default.PreviewMode == SplitPicturePreviewType.Single)
                return PreviewPictureLayoutMode.Single;
            if(PreviewLocation == PreviewLocation.Default || PreviewLocation == PreviewLocation.Left || PreviewLocation == PreviewLocation.Right)
                return PreviewPictureLayoutMode.Vertical;
            return PreviewPictureLayoutMode.Horizontal;
        }

        private BitmapImage GetBitmapImage(DmFile file) {
            if(file == null)
                return null;
            if(File.Exists(file.FullPreviewPath))
                return new BitmapImage(new Uri(file.FullPreviewPath));
            if(File.Exists(file.PreviewFileName))
                return new BitmapImage(new Uri(file.PreviewFileName));
            if(File.Exists(file.ThumbFileName))
                return new BitmapImage(new Uri(file.ThumbFileName));
            return null;
        }

        private void galleryView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            if(IsHandleCreated)
                BeginInvoke(new MethodInvoker(UpdateProperties));
            else
                UpdateProperties();
        }

        private void pePreview_PopupMenuShowing(object sender, DevExpress.XtraEditors.Events.PopupMenuShowingEventArgs e) {
            foreach(DXMenuItem item in e.PopupMenu.Items) {
                if((StringId)item.Tag == StringId.PictureEditMenuCut ||
                    (StringId)item.Tag == StringId.PictureEditMenuDelete ||
                    (StringId)item.Tag == StringId.PictureEditMenuPaste ||
                    (StringId)item.Tag == StringId.PictureEditMenuLoad)
                    item.Visible = false;
            }
        }

        private void bcPreviewAlign_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(e.Item == this.bcPreviewAlignDefault)
                PreviewLocation = PreviewLocation.Default;
            else if(e.Item == this.bcPreviewAlignLeft)
                PreviewLocation = PreviewLocation.Left;
            else if(e.Item == this.bcPreviewAlignRight)
                PreviewLocation = PreviewLocation.Right;
            else if(e.Item == this.bcPreviewAlignTop)
                PreviewLocation = PreviewLocation.Top;
        }

        private void splitContainerControl1_SplitterPositionChanged(object sender, EventArgs e) {
            if(!AllowApplySettings)
                return;
            SettingsStore.Default.PreviewPanelSize = this.splitContainerControl1.SplitterPosition;
        }

        private void bbExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            MainForm.Close();
        }

        private void filterTree_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e) {
            if(e.Column == this.tlColumnIsActive && (bool)e.Node.GetValue(this.tlIsHeader) == true) {
                e.RepositoryItem = this.riFilterTreeEmptySpace;
            }
            if(e.Column == this.tlColumnIsActive) {
                Filter filter = (Filter)this.filterTree.GetDataRecordByNode(e.Node);
                Filter header = Model.GetHeaderForFilter(filter);
                if(filter != null && !filter.IsHeader && header != null) {
                    e.RepositoryItem = header.ViewOperationType == FilterOperationType.Or ? this.ceFilterIsActiveCheck : this.ceFilterIsActiveRadio;
                }
            }
            if(e.Column == this.tlColumnText) {
                Filter filter = (Filter)this.filterTree.GetDataRecordByNode(e.Node);
                if(filter.Type == FilterType.Rating) {
                    e.RepositoryItem = this.riRating;
                }
            }
        }

        private void filterTree_GetNodeDisplayValue(object sender, DevExpress.XtraTreeList.GetNodeDisplayValueEventArgs e) {
            if((e.Column == this.tlColumnIsActive || e.Column == this.tlColumnMatchedCount) && (bool)e.Node.GetValue(this.tlIsHeader) == true) {
                e.Value = "";
            }
            if(e.Column == this.tlOpMode) {
                Filter filter = (Filter)this.filterTree.GetDataRecordByNode(e.Node);
                if(!filter.IsHeader)
                    e.Value = "";
                else if(filter.ViewOperationType == FilterOperationType.And)
                    e.Value = "";
                else if(filter.ViewOperationType == FilterOperationType.Or)
                    e.Value = "OR";
                else
                    e.Value = "";
            }
        }

        private void galleryExplorerView_CustomDrawItem(object sender, WinExplorerViewCustomDrawItemEventArgs e) {
            DmFile info = (DmFile)this.galleryExplorerView.GetRow(e.RowHandle);
            if(info != null && info.ColorLabel != null/* && !e.IsAnimated */) {
                DrawColorFrame(e.Cache, info.ColorLabel, e.Bounds);
            }
            RectangleRotateAnimationInfo animInfo = XtraAnimator.Current.Get(AnimationOwner, info) as RectangleRotateAnimationInfo;
            if(animInfo != null) {
                e.DrawItemBackground();

                PointF[] pt = new PointF[] {
                    animInfo.ActualUpperLeft,
                    animInfo.ActualUpperRight,
                    animInfo.ActualLowerLeft
                };
                e.Graphics.DrawImage(e.ItemInfo.ImageInfo.ThumbImage, pt);

                if(e.ItemInfo.AllowDrawCheckBox)
                    e.DrawItemCheckBox();
                e.DrawItemText();
                e.DrawItemSeparator();
                e.DrawContextButtons();
            } else {
                e.Draw();
            }
            e.Handled = true;
        }

        protected void DrawColorFrame(GraphicsCache cache, DmColorLabel colorLabel, Rectangle bounds) {
            Color color = Color.FromArgb(120, GetColorByLabel(colorLabel));
            cache.FillRectangle(cache.GetSolidBrush(color), bounds);
        }

        protected virtual Color GetColorByLabel(DmColorLabel colorLabel) {
            return colorLabel.Color;
        }

        void galleryExplorerView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e) {
            if(this.galleryExplorerView.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            DmFile info = (DmFile)this.galleryExplorerView.GetRow(this.galleryExplorerView.FocusedRowHandle);
            if(info == null)
                return;
            Model.BeginUpdateFile(info);
        }

        private void galleryExplorerView_HiddenEditor(object sender, EventArgs e) {
            if(this.galleryExplorerView.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            DmFile info = (DmFile)this.galleryExplorerView.GetRow(this.galleryExplorerView.FocusedRowHandle);
            if(info == null)
                return;
            Model.EndUpdateFile(info);
            this.filePropertiesControl1.UpdateProperties();
        }

        private void filterTree_ShowingEditor(object sender, CancelEventArgs e) {
            Filter model = (Filter)this.filterTree.GetDataRecordByNode(this.filterTree.FocusedNode);
            if(this.filterTree.FocusedColumn == this.tlColumnIsActive && model.IsHeader)
                e.Cancel = true;
        }

        private void galleryExplorerView_GetThumbnailImage(object sender, ThumbnailImageEventArgs e) {
            int rowHandle = this.galleryExplorerView.GetRowHandle(e.DataSourceIndex);
            DmFile model = (DmFile)this.galleryExplorerView.GetRow(rowHandle);
            ThumbHelper.GetThumbnailImage(sender, e, model);
        }

        bool FilterCheckdWithControl { get; set; }
        private void filterTree_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e) {
            if(e.Column != this.tlColumnIsActive)
                return;
            Filter filter = (Filter)this.filterTree.GetDataRecordByNode(this.filterTree.FocusedNode);
            if(filter == null)
                return;
            Model.SetHeaderFilterTypeForFilter(filter, FilterCheckdWithControl);
            UpdateFilterOperationMode(filter, (bool)e.Value);
            this.filterTree.LayoutChanged();
            ApplyFiltration();
        }

        protected virtual void ApplyFiltration() {
            UpdateGalleryGridData();
            this.filterControl1.FilterCriteria = Model.ApplyFiltration();
        }

        CheckEdit ActiveFilterEdit { get; set; }
        private void filterTree_ShownEditor(object sender, EventArgs e) {
            if(this.filterTree.ActiveEditor is CheckEdit) {
                ActiveFilterEdit = (CheckEdit)this.filterTree.ActiveEditor;
                ActiveFilterEdit.CheckedChanged += ActiveFilterEdit_CheckedChanged;
            }
        }

        void ActiveFilterEdit_CheckedChanged(object sender, EventArgs e) {
            FilterCheckdWithControl = IsShouldInverseFilter;
            this.filterTree.CloseEditor();
        }

        private void UpdateFilterOperationMode(Filter selectedFilter, bool isActive) {
            Model.UpdateFiltersState(selectedFilter, isActive);
        }

        private void filterTree_HiddenEditor(object sender, EventArgs e) {
            if(ActiveFilterEdit != null)
                ActiveFilterEdit.CheckedChanged -= ActiveFilterEdit_CheckedChanged;
            ActiveFilterEdit = null;
        }

        private void bcMatchAll_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcMatchAll.Checked) {
                FilterOperation = FilterOperationType.And;
                this.bsFilterOperation.Glyph = this.bcMatchAll.Glyph;
                this.bsFilterOperation.LargeGlyph = this.bcMatchAll.LargeGlyph;
            }
        }

        private void bcMatchAny_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcMatchAny.Checked) {
                FilterOperation = FilterOperationType.Or;
                this.bsFilterOperation.Glyph = this.bcMatchAny.Glyph;
                this.bsFilterOperation.LargeGlyph = this.bcMatchAny.LargeGlyph;
            }
        }

        bool IsShouldInverseFilter { get { return (Control.ModifierKeys & Keys.Control) != 0; } }
        protected internal void TemporaryUpdateFilterOperation(bool checkControlKey) {
            if(this.filterTree.ActiveEditor != null)
                return;
            bool shouldShowChecks = checkControlKey && IsShouldInverseFilter;
            Model.ClearForcedFilterValues();
            TreeListHitInfo ht = this.filterTree.CalcHitInfo(this.filterTree.PointToClient(Control.MousePosition));
            if(ht == null || ht.Node == null) {
                this.filterTree.LayoutChanged();
                return;
            }
            Filter filter = Model.GetHeaderForFilter((Filter)this.filterTree.GetDataRecordByNode(ht.Node));
            if(filter == null) {
                this.filterTree.LayoutChanged();
                return;
            }
            FilterOperationType prevOp = filter.ViewOperationType;
            if(!shouldShowChecks) {
                filter.ForcedOperationType = FilterOperationType.And;
            } else {
                filter.ForcedOperationType = FilterOperationType.Or;
            }
            if(prevOp != filter.ViewOperationType)
                this.filterTree.LayoutChanged();
        }

        private void filterTree_MouseMove(object sender, MouseEventArgs e) {
            TemporaryUpdateFilterOperation(true);
        }

        private void filterTree_MouseLeave(object sender, EventArgs e) {
            TemporaryUpdateFilterOperation(false);
        }

        private void filterTree_MouseHover(object sender, EventArgs e) {
            TemporaryUpdateFilterOperation(true);
        }

        private void bbGroupItems_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            List<DmFile> files = GetSelectedGalleryItems();
            Model.GroupFiles(files);
            UpdateGalleryGridData();
        }

        private void bbUngroupItems_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            List<DmFile> files = GetSelectedGalleryItems();
            Model.UngroupFiles(files);
            UpdateGalleryGridData();
        }

        private void bbToggleGroupExpand_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            List<DmFile> files = GetSelectedGalleryItems();
            if(files.Count == 0)
                return;
            Model.ToggleGroupExpand(files);
            UpdateGalleryGridData();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if(!ProcessingFiles)
                return;
            if(XtraMessageBox.Show(this, MessageStrings.ProcessingFilesWhenExit, SettingsStore.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, DefaultBoolean.False) != System.Windows.Forms.DialogResult.Yes) {
                e.Cancel = true;
            }
        }

        private void biUpdateFilters_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Model.UpdateFilters();
        }

        private void filterTree_CustomUnboundColumnData(object sender, TreeListCustomColumnDataEventArgs e) {
            Filter filter = (Filter)this.filterTree.GetDataRecordByNode(e.Node);
            if(filter.Type == FilterType.Rating)
                e.Value = filter.Value;
            else
                e.Value = filter.Text;
        }

        private void galleryExplorerView_ContextButtonCustomize(object sender, WinExplorerViewContextButtonCustomizeEventArgs e) {
            DmFile file = (DmFile)this.galleryExplorerView.GetRow(e.RowHandle);
            if(e.Item.Name == "Text") {
                ((ContextButton)e.Item).Caption = file.Caption;
            }
            else if(e.Item.Name == "Rating") {
                ((RatingContextButton)e.Item).Rating = file.Rating;
            }
            else if(e.Item.Name == "Mark") {
                ((CheckContextButton)e.Item).Checked = file.Marked;
            }
            else if(e.Item.Name == "Reject") {
                ((CheckContextButton)e.Item).Checked = file.Rejected;
            }
            else if(e.Item.Name == "ColorLabel") {
                ContextButton button = (ContextButton)e.Item;
                //button.AllowGlyphSkinning = DefaultBoolean.True;
                //button.AppearanceHover.ForeColor = button.AppearanceNormal.ForeColor = file.ColorLabel == null ? Color.LightGray : file.ColorLabel.Color;
            }
        }

        private void galleryExplorerView_ContextItemClick(object sender, ContextItemClickEventArgs e) {
            SelectedContextFile = (DmFile)this.galleryExplorerView.GetRow((int)e.DataItem);
            if(e.Item is RatingContextButton) {
                int prevRating = SelectedContextFile.Rating;
                SelectedContextFile.Rating = (int)((RatingContextButton)e.Item).Rating;
                Model.OnFileRatingChanged(SelectedContextFile, prevRating);
                SelectedContextFile = null;
            }
            else if(e.Item.Name == "Mark") {
                bool marked = ((CheckContextButton)e.Item).Checked;
                if(marked && SelectedContextFile.Rejected == true) {
                    Model.OnFileRejectedChanged(SelectedContextFile, false);
                }
                Model.OnFileMarkChanged(SelectedContextFile, marked);
                SelectedContextFile = null;
            }
            else if(e.Item.Name == "Reject") {
                bool rejected = ((CheckContextButton)e.Item).Checked;
                if(rejected && SelectedContextFile.Marked == true) {
                    Model.OnFileMarkChanged(SelectedContextFile, false);
                }
                Model.OnFileRejectedChanged(SelectedContextFile, rejected);
                SelectedContextFile = null;
            }
            else if(e.Item.Name == "ColorLabel") {
                this.flyoutPanel1.Tag = e.Item;
                this.flyoutPanel1.ShowBeakForm(new Point(e.ScreenBounds.X + e.ScreenBounds.Width / 2, e.ScreenBounds.Bottom), false, this, new Point(0, 20));
            }
            else if(e.Item.Name == "RotateClockwise") {
                Model.RotateFile(SelectedContextFile, 90);
                RotateFileThumbAnimated(SelectedContextFile, (int)e.DataItem, 90);
            }
            else if(e.Item.Name == "RotateCounterClockwise") {
                Model.RotateFile(SelectedContextFile, -90);
                RotateFileThumbAnimated(SelectedContextFile, (int)e.DataItem, -90);
            }
        }

        WinExplorerViewItemAnimationOwner animationOwner;
        protected internal WinExplorerViewItemAnimationOwner AnimationOwner {
            get {
                if(animationOwner == null) {
                    animationOwner = new WinExplorerViewItemAnimationOwner(this.galleryGridControl);
                    animationOwner.Completed += RotateThumbImageAnimationCompleted;
                }
                return animationOwner;
            }
        }

        protected int ThumbRotateTime {
            get { return 350; }
        }

        private void RotateFileThumbAnimated(DmFile file, int rowHandle, int angle) {
            WinExplorerItemViewInfo itemInfo = GetItemInfo(rowHandle);
            SelectedFileViewInfo = itemInfo;
            RectangleRotateAnimationInfo info = new RectangleRotateAnimationInfo(AnimationOwner, file, itemInfo.ImageContentBounds, itemInfo.ImageBounds, angle, ThumbRotateTime);
            info.Tag = file;
            XtraAnimator.Current.AddAnimation(info);
        }

        private void RotateThumbImageAnimationCompleted(object sender, EventArgs e) {
            ImageUtils.RotateImage(SelectedContextFile.ThumbImage, SelectedContextFile.DeltaAngle);
            SelectedContextFile.DeltaAngle = 0;
            SelectedFileViewInfo.ImageInfo.Image = SelectedContextFile.ThumbImage;
            SelectedFileViewInfo.ImageInfo.ThumbImage = SelectedContextFile.ThumbImage;
            SelectedFileViewInfo.ImageInfo.ThumbSize = SelectedContextFile.ThumbImage.Size;
            SelectedFileViewInfo.ImageInfo.InitRenderImageInfo();
            SelectedFileViewInfo.ForceUpdateImageContentBounds();
        }

        protected WinExplorerItemViewInfo GetItemInfo(int rowHandle) {
            return ((WinExplorerViewInfo)this.galleryExplorerView.GetViewInfo()).GetItemInfo(rowHandle);
        }

        protected DmFile SelectedContextFile {
            get;
            set;
        }

        protected WinExplorerItemViewInfo SelectedFileViewInfo { get; set; }

        private void InitializeColorLabelPopupMenu() {
            this.colorLabelGallery.Gallery.Groups[0].Items.Clear();
            this.colorLabelGallery.Gallery.AllowGlyphSkinning = true;
            GalleryItem none = new GalleryItem() { Caption = "None", Image = this.sharedImageCollection1.ImageSource.Images["delete_32x32.png"] };
            none.AppearanceCaption.Normal.ForeColor = none.AppearanceCaption.Hovered.ForeColor = none.AppearanceCaption.Pressed.ForeColor = Color.White;
            this.colorLabelGallery.Gallery.Groups[0].Items.Add(none);
            foreach(DmColorLabel colorLabel in Model.GetColorLabels()) {
                GalleryItem gi = new GalleryItem() { Caption = colorLabel.Text, Tag = colorLabel, Image = this.sharedImageCollection1.ImageSource.Images["fillbackground_32x32.png"] };
                gi.AppearanceCaption.Normal.ForeColor = gi.AppearanceCaption.Hovered.ForeColor = gi.AppearanceCaption.Pressed.ForeColor = colorLabel.Color;
                this.colorLabelGallery.Gallery.Groups[0].Items.Add(gi);
            }
            this.colorLabelGallery.Gallery.ColumnCount = this.colorLabelGallery.Gallery.Groups[0].Items.Count;
            Size sz = this.colorLabelGallery.CalcBestSize();
            int width = this.flyoutPanel1.Width - this.flyoutPanel1.DisplayRectangle.Width;
            int height = this.flyoutPanel1.Height - this.flyoutPanel1.DisplayRectangle.Height;
            sz.Width += width;
            sz.Height += height;
            this.flyoutPanel1.Size = sz;
        }

        private void colorLabelPopupMenu_BeforePopup(object sender, CancelEventArgs e) {
            InitializeColorLabelPopupMenu();
        }

        private void colorLabelPopupMenu_CloseUp(object sender, EventArgs e) {
            SelectedContextFile = null;
        }

        private void bcMark_CheckedChanged(object sender, ItemClickEventArgs e) {
            Model.OnFileMarkChanged(GetSelectedGalleryItems(), ((BarCheckItem)e.Item).Checked);
            this.galleryExplorerView.LayoutChanged();
        }

        private void repositoryItemRatingControl1_EditValueChanged(object sender, EventArgs e) {
            Model.OnFileRatingChanged(GetSelectedGalleryItems(), (int)((RatingControl)sender).Rating);
            this.galleryExplorerView.LayoutChanged();
        }

        private void galleryExplorerView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e) {
            if(SettingsStore.Default.QuickGalleryMode != QuickGalleryMode.SelectedItems || IsInMarqueeSelection)
                return;
            UpdateQuickGalleryDataSource();
        }

        private void galleryExplorerView_MarqueeSelectionCompleted(object sender, EventArgs e) {
            IsInMarqueeSelection = false;
            UpdateQuickGalleryDataSource();
        }

        bool IsInMarqueeSelection { get; set; }
        public bool IsPicturePreviewVisible {
            get { return !this.splitContainerControl1.IsPanelCollapsed; }
        }

        private void galleryExplorerView_MarqueeSelectionStarted(object sender, EventArgs e) {
            IsInMarqueeSelection = true;
        }

        public List<DmFile> GetFilesToView() {
            List<DmFile> files = (List<DmFile>)QuickGalleryGridControl.DataSource;
            if(files == null || files.Count == 0)
                return (List<DmFile>)this.galleryGridControl.DataSource;
            return files;
        }

        private void quickGalleryView_GetThumbnailImage(object sender, ThumbnailImageEventArgs e) {
            int rowHandle = QuickGalleryView.GetRowHandle(e.DataSourceIndex);
            DmFile model = (DmFile)QuickGalleryView.GetRow(rowHandle);
            ThumbHelper.GetThumbnailImage(sender, e, model);
        }

        private void galleryExplorerView_DoubleClick(object sender, EventArgs e) {
            Point point = this.galleryGridControl.PointToClient(Control.MousePosition);
            WinExplorerViewHitInfo hitInfo = (WinExplorerViewHitInfo)this.galleryExplorerView.CalcHitInfo(point);
            if(!hitInfo.InItem)
                return;
            MainForm.ActivateViewerControl((List<DmFile>)this.galleryGridControl.DataSource, (DmFile)this.galleryExplorerView.GetRow(hitInfo.ItemInfo.Row.RowHandle));
        }

        private void galleryControlGallery1_ItemCheckedChanged(object sender, GalleryItemEventArgs e) {
            GalleryItem item = e.Item;
            if(!item.Checked)
                return;
            if(SelectedContextFile != null) {
                DmColorLabel prevLabel = SelectedContextFile.ColorLabel;
                SelectedContextFile.ColorLabel = (DmColorLabel)e.Item.Tag;
                Model.OnFileColorLabelChanged(SelectedContextFile, prevLabel);
                ContextItem contextItem = this.flyoutPanel1.Tag as ContextItem;
                if(contextItem != null)
                    contextItem.AppearanceHover.ForeColor = contextItem.AppearanceNormal.ForeColor = SelectedContextFile.ColorLabel == null ? Color.LightGray : SelectedContextFile.ColorLabel.Color;
            } else {
                Model.OnFileColorLabelChanged(GetSelectedGalleryItems(), (DmColorLabel)e.Item.Tag);
            }
            this.galleryExplorerView.LayoutChanged();
        }

        private void bbiColorLabel_ItemClick(object sender, ItemClickEventArgs e) {
            this.flyoutPanel1.ShowBeakForm(new Point(e.Link.ScreenBounds.X + e.Link.ScreenBounds.Width / 2, e.Link.ScreenBounds.Bottom), false, this, new Point(0, 5));
        }

        private void bbRemoveSelectedItems_ItemClick(object sender, ItemClickEventArgs e) {
            if(XtraMessageBox.Show(this, "Do you really want to remove selected items?", SettingsStore.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            List<DmFile> files = GetSelectedGalleryItems();
            foreach(DmFile file in files) {
                if(file.IsGroupOwner) {
                    if(XtraMessageBox.Show(this, "Deleteing some files will delete entire group of files. Do you really want to delete grouped files?", SettingsStore.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                    break;
                }
            }
            files = GetSelectedGalleryItemsWithGroupChildren();
            Model.BeginUpdate();
            try {
                Model.RemoveFiles(files);
            } finally {
                Model.EndUpdate();
            }
            UpdateGalleryGridData();
            UpdateQuickGalleryDataSource();
        }

        private List<DmFile> GetSelectedGalleryItemsWithGroupChildren() {
            List<DmFile> files = GetSelectedGalleryItems();
            List<DmFile> res = new List<DmFile>();
            foreach(DmFile file in files) {
                res.Add(file);
                if(file.IsGroupOwner) {
                    res.AddRange(file.GroupedFiles);
                }
            }
            return res;
        }

        private void bbManageDrives_ItemClick(object sender, ItemClickEventArgs e) {
            using(StorageForm form = new StorageForm()) {
                form.Manager = StorageManager.Default;
                form.ShowDialog(this);
            }
        }

        private void bcReject_CheckedChanged(object sender, ItemClickEventArgs e) {
            Model.OnFileRejectedChanged(GetSelectedGalleryItems(), ((BarCheckItem)e.Item).Checked);
            this.galleryExplorerView.LayoutChanged();
        }

        private void bciPreviewMode_CheckedChanged(object sender, ItemClickEventArgs e) {
            OnPreviewModeChanged();
        }

        private void OnPreviewModeChanged() {
            if(this.bciSinglePreview.Checked) {
                SettingsStore.Default.PreviewMode = SplitPicturePreviewType.Single;
            }
            else if(this.bciTwoImagePreview.Checked) {
                SettingsStore.Default.PreviewMode = SplitPicturePreviewType.Two;
            }
            else {
                SettingsStore.Default.PreviewMode = SplitPicturePreviewType.Three;
            }
            PicturePreview.PreviewMode = SettingsStore.Default.PreviewMode;
            UpdateProperties();
        }

        private void bbAddImages_ItemClick(object sender, ItemClickEventArgs e) {
            AddFiles();
        }
    }

    public class WinExplorerViewItemAnimationOwner : ISupportXtraAnimationEx, IXtraAnimationListener {
        public WinExplorerViewItemAnimationOwner(Control owner) {
            OwnerControl = owner;
        }

        public Control OwnerControl { get; private set; }
        public bool CanAnimate {
            get { return true; }
        }

        public void OnFrameStep(BaseAnimationInfo info) {

        }

        public void OnEndAnimation(BaseAnimationInfo info) {

        }

        void IXtraAnimationListener.OnAnimation(BaseAnimationInfo info) {
            if(OwnerControl != null) {
                RectangleRotateAnimationInfo ainfo = (RectangleRotateAnimationInfo)info;
                OwnerControl.Invalidate(ainfo.BoundRect);
                OwnerControl.Update();
            }
        }

        void IXtraAnimationListener.OnEndAnimation(BaseAnimationInfo info) {
            if(Completed != null)
                Completed(this, EventArgs.Empty);
        }

        public event EventHandler Completed;
    }
}
