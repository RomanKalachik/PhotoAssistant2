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
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Wizard;

using System.Windows.Forms.Integration;

using System.Windows.Media;
using DevExpress.XtraBars;
using System.Reflection;
using PhotoAssistant.Core.Model;
using PhotoAssistant.Controls.Wpf;
using PhotoAssistant.Core;
using PhotoAssistant.UI.View.EditingControls;
using PhotoAssistant.Controls.Win.EditingControls;

namespace PhotoAssistant.UI.View {
    public partial class EditingControl : ViewControlBase {
        public EditingControl(DmModel model) {
            InitializeComponent();
            Model = model;
            InitializePicturePreviewControl();
            InitializeRightPanel();
            InitializeLeftPanel();
            InitializeColorLabelEditor();
            InitializeToolbarsMenu();
        }

        internal void ActivateCropTool() {
            PicturePreview.ActivateCrop();
        }
        internal void DeactivateCropTool() {
            PicturePreview.DeactivateCrop();
        }

        private void InitializeToolbarsMenu() {
            this.barManager1.ForceInitialize();
            BarLinkContainerExItem[] items = new BarLinkContainerExItem[] { this.blcPreviewMode, this.blcZoom, this.blcGrid, this.blcMark, this.blcRating, this.blcColorLabels };
            this.sbToolbars.BeginUpdate();
            try {
                foreach(BarLinkContainerExItem item in items) {
                    this.sbToolbars.ItemLinks.Add(CreateCheckItem(item));
                }
            }
            finally {
                this.sbToolbars.EndUpdate();
            }
        }

        bool GetVisibilitySettings(BarLinkContainerExItem item) {
            if(item == this.blcPreviewMode)
                return SettingsStore.Default.EditingToolbarPreviewModeVisible;
            if(item == this.blcZoom)
                return SettingsStore.Default.EditingToolbarZoomVisible;
            if(item == this.blcGrid)
                return SettingsStore.Default.EditingToolbarGridVisible;
            if(item == this.blcRating)
                return SettingsStore.Default.EditingToolbarRatingVisible;
            if(item == this.blcColorLabels)
                return SettingsStore.Default.EditingToolbarColorLabelsVisible;
            if(item == this.blcMark)
                return SettingsStore.Default.EditingToolbarMarkVisible;
            return false;
        }
        void SetVisibilitySettings(BarLinkContainerExItem item, bool value) {
            if(item == this.blcPreviewMode)
                SettingsStore.Default.EditingToolbarPreviewModeVisible = value;
            else if(item == this.blcZoom)
                SettingsStore.Default.EditingToolbarZoomVisible = value;
            else if(item == this.blcGrid)
                SettingsStore.Default.EditingToolbarGridVisible = value;
            else if(item == this.blcRating)
                SettingsStore.Default.EditingToolbarRatingVisible = value;
            else if(item == this.blcColorLabels)
                SettingsStore.Default.EditingToolbarColorLabelsVisible = value;
            else if(item == this.blcMark)
                SettingsStore.Default.EditingToolbarMarkVisible = value;
        }

        private BarItem CreateCheckItem(BarLinkContainerExItem item) {
            BarCheckItem check = new BarCheckItem(this.barManager1, true);
            check.CheckedChanged += ToolbarItemCheckedChanged;
            check.Tag = item;
            check.Caption = item.Caption;
            check.Checked = GetVisibilitySettings(item);
            return check;
        }

        private void ToolbarItemCheckedChanged(object sender, ItemClickEventArgs e) {
            BarCheckItem check = (BarCheckItem)e.Item;
            BarLinkContainerExItem item = (BarLinkContainerExItem)check.Tag;
            item.Visibility = check.Checked ? BarItemVisibility.Always : BarItemVisibility.Never;
            SetVisibilitySettings(item, check.Checked);
        }

        protected virtual void InitializeColorLabelEditor() {
            IEnumerable<DmColorLabel> labels = Model.GetColorLabels();
            this.riColorLabel.Labels.Add(new ColorLabel() { Color = System.Drawing.Color.Empty, Value = null });
            foreach(DmColorLabel label in labels) {
                this.riColorLabel.Labels.Add(CreateLabel(label));
            }
        }

        protected virtual ColorLabel CreateLabel(DmColorLabel label) {
            return new ColorLabel() { Color = label.Color, Value = label };
        }

        protected EditingControlRightPanel RightPanel { get; private set; }
        private void InitializeRightPanel() {
            RightPanel = new EditingControlRightPanel();
            RightPanel.EditingControl = this;
            RightPanel.Dock = DockStyle.Fill;
            this.button1.Panel2.Controls.Add(RightPanel);
        }

        protected EditingControlLeftPanel LeftPanel { get; private set; }
        private void InitializeLeftPanel() {
            LeftPanel = new EditingControlLeftPanel();
            LeftPanel.Dock = DockStyle.Fill;
            this.button2.Panel1.Controls.Add(LeftPanel);
        }

        DmFile currentFileCore = null;
        public DmFile CurrentFile {
            get { return currentFileCore; }
            set {
                if(CurrentFile == value)
                    return;
                currentFileCore = value;
                OnCurrentFileChanged();
            }
        }

        private void OnCurrentFileChanged() {
            UpdateStorageDevices();
            PicturePreview.Source = GetBeforeSource();
            PicturePreview.ProcessedSource = GetAfterSource();
            UpdateEditors();
            UpdateLeftPanel();
        }

        private void UpdateLeftPanel() {
            LeftPanel.CurrentFile = CurrentFile;
            LeftPanel.NavigatroClient = PicturePreview;
        }

        protected bool SuppressUpdateFile { get; set; }
        protected virtual void UpdateEditors() {
            SuppressUpdateFile = true;
            try {
                RightPanel.CurrentFile = CurrentFile;
                this.beRating.EditValue = CurrentFile.Rating;
                this.beColorLabel.EditValue = CurrentFile.ColorLabel;
                this.bcMark.Checked = CurrentFile.Marked;
                this.bcReject.Checked = CurrentFile.Rejected;
            }
            finally {
                SuppressUpdateFile = false;
            }
        }

        private void UpdateStorageDevices() {
            StorageManager.Default.UpdateDevices();
            StorageManager.Default.UpdateVolumeMountNames();
        }

        private ImageSource GetAfterSource() {
            return ImageLoader.LoadWpfPreviewImage(CurrentFile, null);
        }

        private ImageSource GetBeforeSource() {
            return ImageLoader.LoadWpfPreviewImage(CurrentFile, null);
        }

        internal SplitPicturePreviewControl PicturePreview { get; set; }
        ElementHost PicturePreviewHost { get; set; }

        private void InitializePicturePreviewControl() {
            PicturePreviewHost = new ElementHost();
            PicturePreviewHost.Dock = DockStyle.Fill;
            PicturePreview = new SplitPicturePreviewControl();
            PicturePreview.PreviewMode = SplitPicturePreviewType.Editing;
            PicturePreviewHost.Child = PicturePreview;
            PicturePreview.ZoomChanged += PicturePreview_ZoomChanged;
            this.button2.Panel2.Controls.Add(PicturePreviewHost);
        }

        bool SkipUpdateZoomTrackBar { get; set; }
        private void PicturePreview_ZoomChanged(object sender, EventArgs e) {
            if(SkipUpdateZoomTrackBar)
                return;
            this.beZoom.EditValue = (int)(PicturePreview.Zoom * 100);
        }

        private void OnPreviewModeCheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcSingle.Checked)
                PicturePreview.LayoutMode = PreviewPictureLayoutMode.Single;
            else if(this.bcHorizontal.Checked)
                PicturePreview.LayoutMode = PreviewPictureLayoutMode.Horizontal;
            else if(this.bcSplitHorizontal.Checked)
                PicturePreview.LayoutMode = PreviewPictureLayoutMode.HorizontalSplit;
            else if(this.bcVertical.Checked)
                PicturePreview.LayoutMode = PreviewPictureLayoutMode.Vertical;
            else if(this.bcSplitVertical.Checked)
                PicturePreview.LayoutMode = PreviewPictureLayoutMode.VerticalSplit;
        }

        private void bcSwap_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            PicturePreview.IsSwap = this.bcSwap.Checked;
        }

        private void btShowGrid_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            PicturePreview.ShowGrid = this.btShowGrid.Checked;
        }

        private void riGridSize_ValueChanged(object sender, EventArgs e) {
            PicturePreview.GridSize = ((TrackBarControl)sender).Value;
        }

        private void riGridOpacity_ValueChanged(object sender, EventArgs e) {
            PicturePreview.GridOpacity = ((TrackBarControl)sender).Value * 0.01;
        }

        private void riZoomTrackBar_ValueChanged(object sender, EventArgs e) {
            try {
                SkipUpdateZoomTrackBar = true;
                PicturePreview.SetZoomAnimated(((ZoomTrackBarControl)sender).Value / 100.0);
            }
            finally {
                SkipUpdateZoomTrackBar = false;
            }
        }

        private void bbFit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            PicturePreview.FitToScreen(true);
        }

        private void bbUnzoom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            PicturePreview.SetZoomAnimated(1.0);
        }

        private void riRating_EditValueChanged(object sender, EventArgs e) {
            if(SuppressUpdateFile)
                return;
            Model.BeginUpdateFile(CurrentFile);
            CurrentFile.Rating = Convert.ToInt32(((RatingControl)sender).Rating);
            Model.EndUpdateFile(CurrentFile);
        }

        private void beColorLabel_EditValueChanged(object sender, EventArgs e) {
            if(SuppressUpdateFile)
                return;
            Model.BeginUpdateFile(CurrentFile);
            CurrentFile.ColorLabel = (DmColorLabel)beColorLabel.EditValue;
            Model.EndUpdateFile(CurrentFile);
        }

        private void riColorLabel_EditValueChanged(object sender, EventArgs e) {
            if(SuppressUpdateFile)
                return;
            Model.BeginUpdateFile(CurrentFile);
            CurrentFile.ColorLabel = (DmColorLabel)((ColorLabelControl)sender).EditValue;
            Model.EndUpdateFile(CurrentFile);
        }

        private void bcMark_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(SuppressUpdateFile)
                return;
            Model.BeginUpdateFile(CurrentFile);
            CurrentFile.Rejected = false;
            CurrentFile.Marked = bcMark.Checked;
            Model.EndUpdateFile(CurrentFile);
        }

        private void bcReject_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(SuppressUpdateFile)
                return;
            Model.BeginUpdateFile(CurrentFile);
            CurrentFile.Rejected = bcReject.Checked;
            CurrentFile.Marked = false;
            Model.EndUpdateFile(CurrentFile);
        }
    }
}
