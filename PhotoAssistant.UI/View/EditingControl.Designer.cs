
using DevExpress.XtraEditors;
using PhotoAssistant.Controls.Win.EditingControls;
using PhotoAssistant.UI.View.EditingControls;

namespace PhotoAssistant.UI.View {
    partial class EditingControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditingControl));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.toolbar = new DevExpress.XtraBars.Bar();
            this.blcPreviewMode = new DevExpress.XtraBars.BarLinkContainerExItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.pmView = new DevExpress.XtraBars.PopupMenu(this.components);
            this.bcSingle = new DevExpress.XtraBars.BarCheckItem();
            this.bcHorizontal = new DevExpress.XtraBars.BarCheckItem();
            this.bcSplitHorizontal = new DevExpress.XtraBars.BarCheckItem();
            this.bcVertical = new DevExpress.XtraBars.BarCheckItem();
            this.bcSplitVertical = new DevExpress.XtraBars.BarCheckItem();
            this.bcSwap = new DevExpress.XtraBars.BarCheckItem();
            this.blcGrid = new DevExpress.XtraBars.BarLinkContainerExItem();
            this.btShowGrid = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.beGridSize = new DevExpress.XtraBars.BarEditItem();
            this.riGridSize = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.beGridOpacity = new DevExpress.XtraBars.BarEditItem();
            this.riGridOpacity = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.blcZoom = new DevExpress.XtraBars.BarLinkContainerExItem();
            this.beZoom = new DevExpress.XtraBars.BarEditItem();
            this.riZoomTrackBar = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.bbFit = new DevExpress.XtraBars.BarButtonItem();
            this.bbUnzoom = new DevExpress.XtraBars.BarButtonItem();
            this.blcRating = new DevExpress.XtraBars.BarLinkContainerExItem();
            this.beRating = new DevExpress.XtraBars.BarEditItem();
            this.riRating = new DevExpress.XtraEditors.Repository.RepositoryItemRatingControl();
            this.blcColorLabels = new DevExpress.XtraBars.BarLinkContainerExItem();
            this.beColorLabel = new DevExpress.XtraBars.BarEditItem();
            this.riColorLabel = new RepositoryItemColorLabelControl();
            this.blcMark = new DevExpress.XtraBars.BarLinkContainerExItem();
            this.bcMark = new DevExpress.XtraBars.BarCheckItem();
            this.bcReject = new DevExpress.XtraBars.BarCheckItem();
            this.sbToolbars = new DevExpress.XtraBars.BarSubItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.button1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.button2 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGridSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGridOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riZoomTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riColorLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.button1)).BeginInit();
            this.button1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.button2)).BeginInit();
            this.button2.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowGlyphSkinning = true;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.toolbar});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bcSingle,
            this.bcHorizontal,
            this.bcSplitHorizontal,
            this.bcVertical,
            this.bcSplitVertical,
            this.bcSwap,
            this.beGridSize,
            this.btShowGrid,
            this.beGridOpacity,
            this.beZoom,
            this.barButtonItem1,
            this.bbFit,
            this.bbUnzoom,
            this.beRating,
            this.beColorLabel,
            this.bcMark,
            this.bcReject,
            this.blcPreviewMode,
            this.blcGrid,
            this.blcZoom,
            this.blcRating,
            this.blcColorLabels,
            this.blcMark,
            this.sbToolbars});
            this.barManager1.MaxItemId = 30;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riGridSize,
            this.riGridOpacity,
            this.repositoryItemTrackBar1,
            this.riZoomTrackBar,
            this.riRating,
            this.riColorLabel});
            this.barManager1.TransparentEditors = true;
            // 
            // toolbar
            // 
            this.toolbar.BarName = "Toolbar";
            this.toolbar.DockCol = 0;
            this.toolbar.DockRow = 0;
            this.toolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.toolbar.FloatLocation = new System.Drawing.Point(75, 685);
            this.toolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.blcPreviewMode, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.blcGrid, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.blcZoom, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.blcRating, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.blcColorLabels, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.blcMark, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.sbToolbars)});
            this.toolbar.OptionsBar.DrawBorder = false;
            this.toolbar.OptionsBar.MinHeight = 40;
            this.toolbar.OptionsBar.MultiLine = true;
            this.toolbar.OptionsBar.UseWholeRow = true;
            this.toolbar.Text = "Custom 8";
            // 
            // blcPreviewMode
            // 
            this.blcPreviewMode.Caption = "PreviewMode";
            this.blcPreviewMode.Id = 23;
            this.blcPreviewMode.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcSwap)});
            this.blcPreviewMode.Name = "blcPreviewMode";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.ActAsDropDown = true;
            this.barButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItem1.Caption = "View";
            this.barButtonItem1.DropDownControl = this.pmView;
            this.barButtonItem1.Id = 16;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.RememberLastCommand = true;
            // 
            // pmView
            // 
            this.pmView.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bcSingle),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcHorizontal),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcSplitHorizontal),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcVertical),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcSplitVertical)});
            this.pmView.Manager = this.barManager1;
            this.pmView.Name = "pmView";
            // 
            // bcSingle
            // 
            this.bcSingle.Caption = "Single";
            this.bcSingle.GroupIndex = 2;
            this.bcSingle.Id = 5;
            this.bcSingle.Name = "bcSingle";
            this.bcSingle.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnPreviewModeCheckedChanged);
            // 
            // bcHorizontal
            // 
            this.bcHorizontal.Caption = "Horizontal";
            this.bcHorizontal.GroupIndex = 2;
            this.bcHorizontal.Id = 6;
            this.bcHorizontal.Name = "bcHorizontal";
            this.bcHorizontal.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnPreviewModeCheckedChanged);
            // 
            // bcSplitHorizontal
            // 
            this.bcSplitHorizontal.Caption = "Split Horizontal";
            this.bcSplitHorizontal.GroupIndex = 2;
            this.bcSplitHorizontal.Id = 7;
            this.bcSplitHorizontal.Name = "bcSplitHorizontal";
            this.bcSplitHorizontal.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnPreviewModeCheckedChanged);
            // 
            // bcVertical
            // 
            this.bcVertical.Caption = "Vertical";
            this.bcVertical.GroupIndex = 2;
            this.bcVertical.Id = 8;
            this.bcVertical.Name = "bcVertical";
            this.bcVertical.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnPreviewModeCheckedChanged);
            // 
            // bcSplitVertical
            // 
            this.bcSplitVertical.Caption = "Split Vertical";
            this.bcSplitVertical.GroupIndex = 2;
            this.bcSplitVertical.Id = 9;
            this.bcSplitVertical.Name = "bcSplitVertical";
            this.bcSplitVertical.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.OnPreviewModeCheckedChanged);
            // 
            // bcSwap
            // 
            this.bcSwap.Caption = "Swap Panels";
            this.bcSwap.Id = 10;
            this.bcSwap.Name = "bcSwap";
            this.bcSwap.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcSwap_CheckedChanged);
            // 
            // blcGrid
            // 
            this.blcGrid.Caption = "Grid";
            this.blcGrid.Id = 24;
            this.blcGrid.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btShowGrid),
            new DevExpress.XtraBars.LinkPersistInfo(this.beGridSize),
            new DevExpress.XtraBars.LinkPersistInfo(this.beGridOpacity)});
            this.blcGrid.Name = "blcGrid";
            // 
            // btShowGrid
            // 
            this.btShowGrid.Caption = "ShowGrid";
            this.btShowGrid.Id = 13;
            this.btShowGrid.Name = "btShowGrid";
            this.btShowGrid.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.btShowGrid_CheckedChanged);
            // 
            // beGridSize
            // 
            this.beGridSize.Caption = "Size";
            this.beGridSize.Edit = this.riGridSize;
            this.beGridSize.EditHeight = 40;
            this.beGridSize.EditValue = 40;
            this.beGridSize.EditWidth = 129;
            this.beGridSize.Id = 11;
            this.beGridSize.Name = "beGridSize";
            this.beGridSize.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // riGridSize
            // 
            this.riGridSize.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.riGridSize.LabelAppearance.Options.UseTextOptions = true;
            this.riGridSize.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riGridSize.Maximum = 1010;
            this.riGridSize.Minimum = 10;
            this.riGridSize.Name = "riGridSize";
            this.riGridSize.TickFrequency = 50;
            this.riGridSize.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.riGridSize.ValueChanged += new System.EventHandler(this.riGridSize_ValueChanged);
            // 
            // beGridOpacity
            // 
            this.beGridOpacity.Caption = "Opacity";
            this.beGridOpacity.Edit = this.riGridOpacity;
            this.beGridOpacity.EditHeight = 40;
            this.beGridOpacity.EditValue = 80;
            this.beGridOpacity.EditWidth = 129;
            this.beGridOpacity.Id = 14;
            this.beGridOpacity.Name = "beGridOpacity";
            this.beGridOpacity.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // riGridOpacity
            // 
            this.riGridOpacity.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.riGridOpacity.LabelAppearance.Options.UseTextOptions = true;
            this.riGridOpacity.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.riGridOpacity.Maximum = 100;
            this.riGridOpacity.Minimum = 10;
            this.riGridOpacity.Name = "riGridOpacity";
            this.riGridOpacity.TickFrequency = 10;
            this.riGridOpacity.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.riGridOpacity.ValueChanged += new System.EventHandler(this.riGridOpacity_ValueChanged);
            // 
            // blcZoom
            // 
            this.blcZoom.Caption = "Zoom";
            this.blcZoom.Id = 25;
            this.blcZoom.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.beZoom),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbFit),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbUnzoom)});
            this.blcZoom.Name = "blcZoom";
            // 
            // beZoom
            // 
            this.beZoom.Caption = "Zoom";
            this.beZoom.Edit = this.riZoomTrackBar;
            this.beZoom.EditHeight = 40;
            this.beZoom.EditWidth = 129;
            this.beZoom.Id = 15;
            this.beZoom.Name = "beZoom";
            this.beZoom.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // riZoomTrackBar
            // 
            this.riZoomTrackBar.AllowUseMiddleValue = true;
            this.riZoomTrackBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.riZoomTrackBar.Maximum = 1000;
            this.riZoomTrackBar.Middle = 100;
            this.riZoomTrackBar.Minimum = 1;
            this.riZoomTrackBar.Name = "riZoomTrackBar";
            this.riZoomTrackBar.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.riZoomTrackBar.ValueChanged += new System.EventHandler(this.riZoomTrackBar_ValueChanged);
            // 
            // bbFit
            // 
            this.bbFit.Caption = "Fit";
            this.bbFit.Glyph = ((System.Drawing.Image)(resources.GetObject("bbFit.Glyph")));
            this.bbFit.Id = 17;
            this.bbFit.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbFit.LargeGlyph")));
            this.bbFit.Name = "bbFit";
            this.bbFit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbFit_ItemClick);
            // 
            // bbUnzoom
            // 
            this.bbUnzoom.Caption = "Original Size";
            this.bbUnzoom.Glyph = ((System.Drawing.Image)(resources.GetObject("bbUnzoom.Glyph")));
            this.bbUnzoom.Id = 18;
            this.bbUnzoom.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbUnzoom.LargeGlyph")));
            this.bbUnzoom.Name = "bbUnzoom";
            this.bbUnzoom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbUnzoom_ItemClick);
            // 
            // blcRating
            // 
            this.blcRating.Caption = "Rating";
            this.blcRating.Id = 26;
            this.blcRating.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.beRating)});
            this.blcRating.Name = "blcRating";
            // 
            // beRating
            // 
            this.beRating.Caption = "Rating";
            this.beRating.Edit = this.riRating;
            this.beRating.EditWidth = 108;
            this.beRating.Id = 19;
            this.beRating.Name = "beRating";
            // 
            // riRating
            // 
            this.riRating.AutoHeight = false;
            this.riRating.Name = "riRating";
            this.riRating.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.riRating.EditValueChanged += new System.EventHandler(this.riRating_EditValueChanged);
            // 
            // blcColorLabels
            // 
            this.blcColorLabels.Caption = "ColorLabels";
            this.blcColorLabels.Id = 27;
            this.blcColorLabels.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.beColorLabel)});
            this.blcColorLabels.Name = "blcColorLabels";
            // 
            // beColorLabel
            // 
            this.beColorLabel.Caption = "Color Labels";
            this.beColorLabel.Edit = this.riColorLabel;
            this.beColorLabel.EditHeight = 40;
            this.beColorLabel.EditWidth = 201;
            this.beColorLabel.Id = 20;
            this.beColorLabel.Name = "beColorLabel";
            this.beColorLabel.EditValueChanged += new System.EventHandler(this.beColorLabel_EditValueChanged);
            // 
            // riColorLabel
            // 
            this.riColorLabel.AutoHeight = false;
            this.riColorLabel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.riColorLabel.Name = "riColorLabel";
            this.riColorLabel.EditValueChanged += new System.EventHandler(this.riColorLabel_EditValueChanged);
            // 
            // blcMark
            // 
            this.blcMark.Caption = "Mark";
            this.blcMark.Id = 28;
            this.blcMark.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bcMark),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcReject)});
            this.blcMark.Name = "blcMark";
            // 
            // bcMark
            // 
            this.bcMark.Caption = "Mark";
            this.bcMark.Id = 21;
            this.bcMark.Name = "bcMark";
            this.bcMark.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcMark_CheckedChanged);
            // 
            // bcReject
            // 
            this.bcReject.Caption = "Reject";
            this.bcReject.Id = 22;
            this.bcReject.Name = "bcReject";
            this.bcReject.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcReject_CheckedChanged);
            // 
            // sbToolbars
            // 
            this.sbToolbars.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.sbToolbars.Caption = "Toolbars";
            this.sbToolbars.Id = 29;
            this.sbToolbars.Name = "sbToolbars";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1371, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 545);
            this.barDockControlBottom.Size = new System.Drawing.Size(1371, 40);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 545);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1371, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 545);
            // 
            // repositoryItemTrackBar1
            // 
            this.repositoryItemTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemTrackBar1.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemTrackBar1.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTrackBar1.Name = "repositoryItemTrackBar1";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Panel1.Controls.Add(this.button2);
            this.button1.Size = new System.Drawing.Size(1371, 545);
            this.button1.SplitterPosition = 383;
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(913, 545);
            this.button2.SplitterPosition = 259;
            this.button2.TabIndex = 0;
            this.button2.Text = "button2";
            // 
            // EditingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Name = "EditingControl";
            this.Size = new System.Drawing.Size(1371, 585);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGridSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGridOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riZoomTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riColorLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.button1)).EndInit();
            this.button1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.button2)).EndInit();
            this.button2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarCheckItem bcSingle;
        private DevExpress.XtraBars.BarCheckItem bcHorizontal;
        private DevExpress.XtraBars.BarCheckItem bcSplitHorizontal;
        private DevExpress.XtraBars.BarCheckItem bcVertical;
        private DevExpress.XtraBars.BarCheckItem bcSplitVertical;
        private DevExpress.XtraBars.BarCheckItem bcSwap;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private SplitContainerControl button1;
        private DevExpress.XtraBars.BarToggleSwitchItem btShowGrid;
        private DevExpress.XtraBars.BarEditItem beGridSize;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar riGridSize;
        private DevExpress.XtraBars.BarEditItem beGridOpacity;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar riGridOpacity;
        private DevExpress.XtraBars.BarEditItem beZoom;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar riZoomTrackBar;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.PopupMenu pmView;
        private DevExpress.XtraBars.BarButtonItem bbFit;
        private DevExpress.XtraBars.BarButtonItem bbUnzoom;
        private DevExpress.XtraBars.BarEditItem beRating;
        private DevExpress.XtraEditors.Repository.RepositoryItemRatingControl riRating;
        private DevExpress.XtraBars.BarEditItem beColorLabel;
        private RepositoryItemColorLabelControl riColorLabel;
        private DevExpress.XtraBars.BarCheckItem bcMark;
        private DevExpress.XtraBars.BarCheckItem bcReject;
        private DevExpress.XtraBars.BarLinkContainerExItem blcPreviewMode;
        private DevExpress.XtraBars.BarLinkContainerExItem blcGrid;
        private DevExpress.XtraBars.BarLinkContainerExItem blcZoom;
        private DevExpress.XtraBars.BarLinkContainerExItem blcRating;
        private DevExpress.XtraBars.BarLinkContainerExItem blcColorLabels;
        private DevExpress.XtraBars.BarLinkContainerExItem blcMark;
        private DevExpress.XtraBars.Bar toolbar;
        private DevExpress.XtraBars.BarSubItem sbToolbars;
        private SplitContainerControl button2;
    }
}
