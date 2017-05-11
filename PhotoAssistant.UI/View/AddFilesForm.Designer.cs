namespace PhotoAssistant.UI.View {
    partial class AddFilesForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.fileExplorerControl1 = new PhotoAssistant.UI.View.ImportControls.FileExplorerControl();
            this.sbUncheckAll = new DevExpress.XtraEditors.SimpleButton();
            this.sbCheckAll = new DevExpress.XtraEditors.SimpleButton();
            this.zoomTrackBarControl1 = new DevExpress.XtraEditors.ZoomTrackBarControl();
            this.filesGridControl = new DevExpress.XtraGrid.GridControl();
            this.filesExplorerView = new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView();
            this.sbImport = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesExplorerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.fileExplorerControl1);
            this.layoutControl1.Controls.Add(this.sbUncheckAll);
            this.layoutControl1.Controls.Add(this.sbCheckAll);
            this.layoutControl1.Controls.Add(this.zoomTrackBarControl1);
            this.layoutControl1.Controls.Add(this.filesGridControl);
            this.layoutControl1.Controls.Add(this.sbImport);
            this.layoutControl1.Controls.Add(this.sbCancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(773, 257, 1002, 630);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(817, 483);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // fileExplorerControl1
            // 
            this.fileExplorerControl1.Location = new System.Drawing.Point(12, 12);
            this.fileExplorerControl1.Name = "fileExplorerControl1";
            this.fileExplorerControl1.Size = new System.Drawing.Size(196, 409);
            this.fileExplorerControl1.TabIndex = 10;
            this.fileExplorerControl1.SourcePathCanged += new System.EventHandler(this.fileExplorerControl1_SourcePathCanged);
            // 
            // sbUncheckAll
            // 
            this.sbUncheckAll.Location = new System.Drawing.Point(340, 399);
            this.sbUncheckAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sbUncheckAll.Name = "sbUncheckAll";
            this.sbUncheckAll.Size = new System.Drawing.Size(124, 22);
            this.sbUncheckAll.StyleController = this.layoutControl1;
            this.sbUncheckAll.TabIndex = 9;
            this.sbUncheckAll.Text = "Uncheck All";
            this.sbUncheckAll.Click += new System.EventHandler(this.sbUncheckAll_Click);
            // 
            // sbCheckAll
            // 
            this.sbCheckAll.Location = new System.Drawing.Point(212, 399);
            this.sbCheckAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sbCheckAll.Name = "sbCheckAll";
            this.sbCheckAll.Size = new System.Drawing.Size(124, 22);
            this.sbCheckAll.StyleController = this.layoutControl1;
            this.sbCheckAll.TabIndex = 8;
            this.sbCheckAll.Text = "Check All";
            this.sbCheckAll.Click += new System.EventHandler(this.sbCheckAll_Click);
            // 
            // zoomTrackBarControl1
            // 
            this.zoomTrackBarControl1.EditValue = 128;
            this.zoomTrackBarControl1.Location = new System.Drawing.Point(589, 399);
            this.zoomTrackBarControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.zoomTrackBarControl1.Name = "zoomTrackBarControl1";
            this.zoomTrackBarControl1.Properties.Maximum = 256;
            this.zoomTrackBarControl1.Properties.Middle = 5;
            this.zoomTrackBarControl1.Properties.Minimum = 64;
            this.zoomTrackBarControl1.Properties.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.zoomTrackBarControl1.Size = new System.Drawing.Size(216, 23);
            this.zoomTrackBarControl1.StyleController = this.layoutControl1;
            this.zoomTrackBarControl1.TabIndex = 7;
            this.zoomTrackBarControl1.Value = 128;
            this.zoomTrackBarControl1.EditValueChanged += new System.EventHandler(this.zoomTrackBarControl1_EditValueChanged);
            // 
            // filesGridControl
            // 
            this.filesGridControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.filesGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filesGridControl.Location = new System.Drawing.Point(212, 12);
            this.filesGridControl.MainView = this.filesExplorerView;
            this.filesGridControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filesGridControl.Name = "filesGridControl";
            this.filesGridControl.Size = new System.Drawing.Size(593, 383);
            this.filesGridControl.TabIndex = 6;
            this.filesGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.filesExplorerView});
            // 
            // filesExplorerView
            // 
            this.filesExplorerView.GridControl = this.filesGridControl;
            this.filesExplorerView.Name = "filesExplorerView";
            this.filesExplorerView.OptionsImageLoad.AnimationType = DevExpress.Utils.ImageContentAnimationType.Expand;
            this.filesExplorerView.OptionsImageLoad.AsyncLoad = true;
            this.filesExplorerView.OptionsImageLoad.LoadThumbnailImagesFromDataSource = false;
            this.filesExplorerView.OptionsView.ImageLayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.Squeeze;
            this.filesExplorerView.OptionsView.ShowCheckBoxes = true;
            this.filesExplorerView.OptionsView.ShowExpandCollapseButtons = true;
            this.filesExplorerView.OptionsView.Style = DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewStyle.Large;
            this.filesExplorerView.ItemDoubleClick += new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemDoubleClickEventHandler(this.filesExplorerView_ItemDoubleClick);
            this.filesExplorerView.GetThumbnailImage += new DevExpress.Utils.ThumbnailImageEventHandler(this.filesExplorerView_GetThumbnailImage);
            // 
            // sbImport
            // 
            this.sbImport.AutoWidthInLayoutControl = true;
            this.sbImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sbImport.Location = new System.Drawing.Point(617, 437);
            this.sbImport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sbImport.MinimumSize = new System.Drawing.Size(86, 0);
            this.sbImport.Name = "sbImport";
            this.sbImport.Size = new System.Drawing.Size(86, 22);
            this.sbImport.StyleController = this.layoutControl1;
            this.sbImport.TabIndex = 5;
            this.sbImport.Text = "Import";
            this.sbImport.Click += new System.EventHandler(this.sbImport_Click);
            // 
            // sbCancel
            // 
            this.sbCancel.AutoWidthInLayoutControl = true;
            this.sbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbCancel.Location = new System.Drawing.Point(707, 437);
            this.sbCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sbCancel.MinimumSize = new System.Drawing.Size(86, 0);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(86, 22);
            this.sbCancel.StyleController = this.layoutControl1;
            this.sbCancel.TabIndex = 4;
            this.sbCancel.Text = "Cancel";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlGroup2,
            this.layoutControlItem6,
            this.layoutControlItem5,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(817, 483);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.filesGridControl;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(200, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(597, 387);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.zoomTrackBarControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(577, 387);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(220, 22);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(220, 22);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(220, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(456, 387);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(121, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 413);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(797, 50);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(593, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbImport;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(593, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(90, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sbCancel;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(683, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(90, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sbUncheckAll;
            this.layoutControlItem6.Location = new System.Drawing.Point(328, 387);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(128, 0);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(128, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(128, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbCheckAll;
            this.layoutControlItem5.Location = new System.Drawing.Point(200, 387);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(128, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(128, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(128, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.fileExplorerControl1;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(200, 413);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // AddFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 483);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AddFilesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose files to import";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesExplorerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl filesGridControl;
        private DevExpress.XtraEditors.SimpleButton sbImport;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView filesExplorerView;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.ZoomTrackBarControl zoomTrackBarControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sbUncheckAll;
        private DevExpress.XtraEditors.SimpleButton sbCheckAll;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private ImportControls.FileExplorerControl fileExplorerControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}