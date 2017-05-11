
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.ColorWheel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.WinExplorer;

using DevExpress.XtraEditors;
using System.IO;
using DevExpress.XtraGrid;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils.Drawing;
using DevExpress.Utils;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars.Alerter;
using System.Windows.Forms.Integration;

using System.Windows.Media.Imaging;
using DevExpress.Data.Filtering;
using System.Diagnostics;
using DevExpress.XtraGrid.WinExplorer;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;
using PhotoAssistant.Core.Model;
using PhotoAssistant.Core;
using PhotoAssistant.UI.View;
using PhotoAssistant.UI.ViewHelpers;

namespace PhotoAssistant.UI{
    public partial class MainForm : XtraForm {
        public MainForm(DmModel model) {
            //DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this, typeof(SplashScreenForm));
            StorageManager.Default.DialogsProvider = StorageManagerDialogsProvider.Default;
            Model = model;
            Application.AddMessageFilter(new MessageFilter(this));
            InitializeComponent();
            ActivateControl(tpLibrary, LibraryControl);
            this.quickGalleryView.OptionsImageLoad.AnimationType = LibraryControl.ConvertAnimationType(SettingsStore.Default.ImageAnimationType);
            //DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
            this.quickGalleryGridControl.Load += quickGalleryGridControl_Load;
        }

        protected override bool SupportAdvancedTitlePainting {
            get { return false; }
        }

        void quickGalleryView_GetThumbnailImage(object sender, DevExpress.Utils.ThumbnailImageEventArgs e) {
            int rowHandle = this.quickGalleryView.GetRowHandle(e.DataSourceIndex);
            DmFile model = (DmFile)this.quickGalleryView.GetRow(rowHandle);
            ThumbHelper.GetThumbnailImage(sender, e, model);
        }

        protected DmModel Model { get; set; }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            GenerateThumbsFormActiveProject();
            SaveSettings();
        }

        private void GenerateThumbsFormActiveProject() {
            if(SettingsStore.Default.ActiveProject == null)
                return;

            SettingsStore.Default.ActiveProject.ThumbFileName =
                SettingsStore.Default.ActiveProject.ThumbFileName2 =
                    SettingsStore.Default.ActiveProject.ThumbFileName3 =
                        SettingsStore.Default.ActiveProject.ThumbFileName4 = "";

            int count = Model.Context.Files.Local.Count;
            if(count == 0)
                return;

            int index2 = Model.Context.Files.Local.Count / 4;
            int index3 = Model.Context.Files.Local.Count / 2;
            int index4 = Model.Context.Files.Local.Count * 3 / 4;
            if(index2 == 0)
                index2++;
            if(index3 <= index2)
                index3++;
            if(index4 <= index3)
                index4++;
            if(index2 >= count)
                index2 = count - 1;
            if(index3 >= count)
                index3 = count - 1;
            if(index4 >= count)
                index4 = count - 1;
            SettingsStore.Default.ActiveProject.ThumbFileName = Model.Context.Files.Local[0].ThumbFileName;
            SettingsStore.Default.ActiveProject.ThumbFileName2 = Model.Context.Files.Local[index2].ThumbFileName;
            SettingsStore.Default.ActiveProject.ThumbFileName3 = Model.Context.Files.Local[index3].ThumbFileName;
            SettingsStore.Default.ActiveProject.ThumbFileName4 = Model.Context.Files.Local[index4].ThumbFileName;
            SettingsStore.Default.ActiveProject.FileCount = Model.Context.Files.Local.Count;
        }

        private void SaveSettings() {
            SettingsStore.Default.SelectedThemeName = LibraryControl.rgbThemes.Gallery.GetCheckedItem().Tag.ToString();
            SettingsStore.Default.SaveToXml();
        }

        ProjectsControl projectsControl;
        protected internal ProjectsControl ProjectsControl {
            get {
                if(projectsControl == null)
                    projectsControl = CreateProjectsControl();
                return projectsControl;
            }
        }

        protected virtual ProjectsControl CreateProjectsControl() {
            return new ProjectsControl(Model, this);
        }

        LibraryControl libraryControl;
        protected internal LibraryControl LibraryControl {
            get {
                if(libraryControl == null)
                    libraryControl = CreateLibraryControl();
                return libraryControl;
            }
        }

        ViewerControl viewerControl;
        protected internal ViewerControl ViewerControl {
            get {
                if(viewerControl == null)
                    viewerControl = CreateViewerControl();
                return viewerControl;
            }
        }

        PrintControl printControl;
        protected internal PrintControl PrintControl {
            get {
                if(printControl == null)
                    printControl = CreatePrintControl();
                return printControl;
            }
        }

        EditingControl editingControl;
        protected internal EditingControl EditingControl {
            get {
                if(editingControl == null)
                    editingControl = CreateEditingControl();
                return editingControl;
            }
        }

        WebControl webControl;
        protected internal WebControl WebControl {
            get {
                if(webControl == null)
                    webControl = CreateWebControl();
                return webControl;
            }
        }

        MapView mapControl;
        protected internal MapView MapControl {
            get {
                if(mapControl == null)
                    mapControl = CreateMapControl();
                return mapControl;
            }
        }

        ExportControl exportControl;
        protected internal ExportControl ExportControl {
            get {
                if(exportControl == null)
                    exportControl = CreateExportControl();
                return exportControl;
            }
        }

        private ExportControl CreateExportControl() {
            return new ExportControl() { MainForm = this, Model = Model };
        }

        private MapView CreateMapControl() {
            return new MapView(Model);
        }

        private WebControl CreateWebControl() {
            return new WebControl(Model);
        }

        private EditingControl CreateEditingControl() {
            return new EditingControl(Model);
        }

        private PrintControl CreatePrintControl() {
            return new PrintControl();
        }

        private ViewerControl CreateViewerControl() {
            return new ViewerControl() { MainForm = this, Model = Model };
        }

        private LibraryControl CreateLibraryControl() {
            return new LibraryControl(Model, this);
        }

        public void HideQuickGalleryPanel() {
            this.splitContainerControl1.Collapsed = true;
        }

        public void ShowQuickGalleryPanel() {
            this.splitContainerControl1.Collapsed = false;
        }

        private void officeNavigationBar1_SelectedItemChanged(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e) {

            if(e.Item == this.niLibrary) {
                ActivateControl(this.tpLibrary, LibraryControl);
            } else if(e.Item == this.niView) {
                ViewerControl.Files = LibraryControl.GetFilesToView();
                ActivateControl(this.tpViewer, ViewerControl);
            }
            if(e.Item == this.niPrint) {
                PrintControl.Files = LibraryControl.GetFilesToView();
                ActivateControl(this.tpPrinting, PrintControl);
            }
            if(e.Item == this.niEdit) {
                EditingControl.CurrentFile = LibraryControl.CurrentFile;
                ActivateControl(this.tpEditing, EditingControl);
            }
            if(e.Item == this.niWeb) {
                WebControl.Files = LibraryControl.GetFilesToView();
                ActivateControl(this.tpWeb, WebControl);
            }
            if(e.Item == this.niMap) {
                MapControl.Files = SortFilesByDate(LibraryControl.GetFilesToView());
                ActivateControl(this.tpMap, MapControl);
            }
            if(e.Item == this.niProject) {
                ActivateControl(this.tpProjects, ProjectsControl);
            }
            if(e.Item == this.niExport) {
                ExportControl.Files = LibraryControl.GetFilesToView();
                ActivateControl(this.tpExport, ExportControl);
            }
        }

        protected List<DmFile> SortFilesByDate(List<DmFile> files) {
            var res = from file in files
                      orderby file.CreationDate ascending
                      select file;
            return res.ToList();
        }

        internal void ActivateLibraryControl() {
            officeNavigationBar1.SelectedItem = null;
            officeNavigationBar1.SelectedItem = this.niLibrary;
        }

        internal void ActivateViewerControl(List<DmFile> files, DmFile currentFile) {
            ViewerControl.Files = files;
            ViewerControl.CurrentFile = currentFile;
            ActivateControl(this.tpViewer, ViewerControl);
        }

        private void SubscribeEvents(ViewControlBase control) {
            this.quickGalleryView.ItemClick += control.OnQuickGalleryItemClick;
        }

        private void UnsubscribeEvents(ViewControlBase control) {
            this.quickGalleryView.ItemClick -= control.OnQuickGalleryItemClick;
        }

        private void ActivateControl(DevExpress.XtraTab.XtraTabPage page, ViewControlBase control) {
            if(page.Controls.Count == 0) {
                control.Dock = DockStyle.Fill;
                page.Controls.Add(control);
            }
            if(this.tcMain.SelectedTabPage != null) {
                UnsubscribeEvents((ViewControlBase)this.tcMain.SelectedTabPage.Controls[0]);
                ((ViewControlBase)this.tcMain.SelectedTabPage.Controls[0]).OnHideView();
            }
            SubscribeEvents(control);
            control.OnShowView();
            this.tcMain.SelectedTabPage = page;
            control.Focus();
        }

        bool fullScreen;
        protected internal bool FullScreen {
            get { return fullScreen; }
            set {
                if(FullScreen == value)
                    return;
                fullScreen = value;
                OnFullScreenChanged();
            }
        }

        protected bool IsFormMaximized { get; set; }

        private void OnFullScreenChanged() {
            if(FullScreen) {
                IsFormMaximized = WindowState == FormWindowState.Maximized;
                WindowState = FormWindowState.Maximized;
                FormBorderStyle = FormBorderStyle.None;
                TopMost = true;
                this.officeNavigationBar1.Visible = false;
                this.splitContainerControl1.CollapsePanel = SplitCollapsePanel.Panel2;
                this.splitContainerControl1.Collapsed = true;
            } else {
                FormBorderStyle = FormBorderStyle.Sizable;
                if(!IsFormMaximized)
                    WindowState = FormWindowState.Normal;
                TopMost = false;
                this.officeNavigationBar1.Visible = true;
                this.splitContainerControl1.Collapsed = false;
            }
        }

        internal void ToggleFullScreen() {
            FullScreen = !FullScreen;
        }

        protected bool AllowPhotoDragDrop {
            get {
                ISupportPhotoDragDrop dragDrop = this.tcMain.SelectedTabPage.Controls[0] as ISupportPhotoDragDrop;
                return dragDrop != null && dragDrop.AllowDragDrop;
            }
        }

        private void quickGalleryView_MouseDown(object sender, MouseEventArgs e) {
            if(!AllowPhotoDragDrop)
                return;
            TileViewHitInfo info = (TileViewHitInfo)this.quickGalleryView.CalcHitInfo(e.Location);
            if(info.Item == null)
                return;
            Rectangle screenRect = this.quickGalleryGridControl.RectangleToScreen(info.ItemInfo.BackgroundImageContentBounds);
            PhotoDragDpopHelper.Default.OnMouseDown(this, this.quickGalleryGridControl, e, (DmFile)this.quickGalleryView.GetRow(info.RowHandle), screenRect);
        }

        private void quickGalleryView_MouseMove(object sender, MouseEventArgs e) {
            if(!AllowPhotoDragDrop)
                return;
            PhotoDragDpopHelper.Default.OnMouseMove(e);
        }

        private void quickGalleryView_MouseUp(object sender, MouseEventArgs e) {
            if(!AllowPhotoDragDrop)
                return;
            PhotoDragDpopHelper.Default.OnMouseUp();
        }

        private void quickGalleryView_DoubleClick(object sender, EventArgs e) {
            if(!AllowPhotoDragDrop)
                return;
            PhotoDragDpopHelper.Default.OnDoubleClick();
        }

        protected int QuickGalleryScrollBarHeight { get { return 22; } }
        protected int QuickGalleryVerticalPadding { get { return this.quickGalleryView.OptionsTiles.Padding.Vertical; } }

        protected internal void UpdateQuickGalleryItemSize() {
            int height = this.quickGalleryGridControl.ClientRectangle.Height - QuickGalleryScrollBarHeight - QuickGalleryVerticalPadding;
            int width = (int)(height * Model.Properties.AspectRatio);
            if(width > SettingsStore.Default.ThumbSize.Width) {
                width = SettingsStore.Default.ThumbSize.Width;
                height = (int)(width / Model.Properties.AspectRatio);
            }
            if(height > SettingsStore.Default.ThumbSize.Height) {
                height = SettingsStore.Default.ThumbSize.Height;
                width = (int)(height * Model.Properties.AspectRatio);
            }
            height = Math.Max(height, 22);
            width = Math.Max(width, 22);
            this.quickGalleryView.OptionsTiles.ItemSize = new Size(width, height);
        }

        private void quickGalleryGridControl_Resize(object sender, EventArgs e) {
            UpdateQuickGalleryItemSize();
        }

        void quickGalleryGridControl_Load(object sender, EventArgs e) {
            UpdateQuickGalleryItemSize();
        }
    }
}
