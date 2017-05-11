namespace PhotoAssistant.UI.View {
    partial class MapView {
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
            DevExpress.XtraMap.ImageTilesLayer imageTilesLayer1 = new DevExpress.XtraMap.ImageTilesLayer();
            DevExpress.XtraMap.BingMapDataProvider bingMapDataProvider1 = new DevExpress.XtraMap.BingMapDataProvider();
            DevExpress.XtraMap.InformationLayer informationLayer1 = new DevExpress.XtraMap.InformationLayer();
            DevExpress.XtraMap.BingGeocodeDataProvider bingGeocodeDataProvider1 = new DevExpress.XtraMap.BingGeocodeDataProvider();
            DevExpress.XtraMap.VectorItemsLayer vectorItemsLayer1 = new DevExpress.XtraMap.VectorItemsLayer();
            DevExpress.XtraMap.MapItemStorage mapItemStorage1 = new DevExpress.XtraMap.MapItemStorage();
            DevExpress.XtraMap.VectorItemsLayer vectorItemsLayer2 = new DevExpress.XtraMap.VectorItemsLayer();
            DevExpress.XtraMap.MapItemStorage mapItemStorage2 = new DevExpress.XtraMap.MapItemStorage();
            DevExpress.XtraMap.VectorItemsLayer vectorItemsLayer3 = new DevExpress.XtraMap.VectorItemsLayer();
            DevExpress.XtraMap.MapItemStorage mapItemStorage3 = new DevExpress.XtraMap.MapItemStorage();
            DevExpress.XtraMap.MapPushpin mapPushpin1 = new DevExpress.XtraMap.MapPushpin();
            DevExpress.XtraMap.MiniMap miniMap1 = new DevExpress.XtraMap.MiniMap();
            DevExpress.XtraMap.FixedMiniMapBehavior fixedMiniMapBehavior1 = new DevExpress.XtraMap.FixedMiniMapBehavior();
            DevExpress.XtraMap.MiniMapImageTilesLayer miniMapImageTilesLayer1 = new DevExpress.XtraMap.MiniMapImageTilesLayer();
            DevExpress.XtraMap.BingMapDataProvider bingMapDataProvider2 = new DevExpress.XtraMap.BingMapDataProvider();
            DevExpress.XtraMap.MiniMapVectorItemsLayer miniMapVectorItemsLayer1 = new DevExpress.XtraMap.MiniMapVectorItemsLayer();
            DevExpress.XtraMap.MapItemStorage mapItemStorage4 = new DevExpress.XtraMap.MapItemStorage();
            DevExpress.Utils.ContextButton contextButton1 = new DevExpress.Utils.ContextButton();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapView));
            this.mapControl1 = new DevExpress.XtraMap.MapControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.flyoutPanel1 = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl1 = new DevExpress.Utils.FlyoutPanelControl();
            this.flyoutPanel2 = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl2 = new DevExpress.Utils.FlyoutPanelControl();
            this.geoLocationControl1 = new GeoLocationControl();
            this.popupViewer1 = new PopupViewer();
            this.galleryGridControl = new DevExpress.XtraGrid.GridControl();
            this.galleryView = new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bcVisibleInViewPort = new DevExpress.XtraBars.BarCheckItem();
            this.bcNotGeotagged = new DevExpress.XtraBars.BarCheckItem();
            this.bcAllItems = new DevExpress.XtraBars.BarCheckItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel1)).BeginInit();
            this.flyoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).BeginInit();
            this.flyoutPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel2)).BeginInit();
            this.flyoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).BeginInit();
            this.flyoutPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.galleryGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // mapControl1
            // 
            this.mapControl1.CenterPoint = new DevExpress.XtraMap.GeoPoint(47.5D, 2D);
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            bingMapDataProvider1.BingKey = "AmSNFwVzMvaqFlCYQx9RRUfcAwSQCzi_Vcesric6JFQuBO9wZFXEsqzili-INaUA";
            bingMapDataProvider1.Kind = DevExpress.XtraMap.BingMapKind.Area;
            imageTilesLayer1.DataProvider = bingMapDataProvider1;
            imageTilesLayer1.Name = "TilesLayer";
            informationLayer1.DataProvider = bingGeocodeDataProvider1;
            informationLayer1.Name = "InformationLayer";
            informationLayer1.Visible = false;
            vectorItemsLayer1.Data = mapItemStorage1;
            vectorItemsLayer1.ItemStyle.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            vectorItemsLayer1.Name = "ItemsLayer";
            vectorItemsLayer2.Data = mapItemStorage2;
            vectorItemsLayer2.Name = "ItemsDetailLayer";
            mapItemStorage3.Items.Add(mapPushpin1);
            vectorItemsLayer3.Data = mapItemStorage3;
            vectorItemsLayer3.Name = "PushpinLayer";
            vectorItemsLayer3.Visible = false;
            this.mapControl1.Layers.Add(imageTilesLayer1);
            this.mapControl1.Layers.Add(informationLayer1);
            this.mapControl1.Layers.Add(vectorItemsLayer1);
            this.mapControl1.Layers.Add(vectorItemsLayer2);
            this.mapControl1.Layers.Add(vectorItemsLayer3);
            this.mapControl1.Location = new System.Drawing.Point(0, 0);
            miniMap1.Alignment = DevExpress.XtraMap.MiniMapAlignment.TopLeft;
            fixedMiniMapBehavior1.ZoomLevel = 0.5D;
            miniMap1.Behavior = fixedMiniMapBehavior1;
            miniMap1.Height = 256;
            miniMapImageTilesLayer1.DataProvider = bingMapDataProvider2;
            miniMapImageTilesLayer1.Name = "ImageTilesLayer";
            miniMapVectorItemsLayer1.Data = mapItemStorage4;
            miniMapVectorItemsLayer1.ItemStyle.Fill = System.Drawing.Color.Red;
            miniMapVectorItemsLayer1.ItemStyle.Stroke = System.Drawing.Color.White;
            miniMapVectorItemsLayer1.ItemStyle.StrokeWidth = 2;
            miniMapVectorItemsLayer1.Name = "DataLayer";
            miniMap1.Layers.Add(miniMapImageTilesLayer1);
            miniMap1.Layers.Add(miniMapVectorItemsLayer1);
            miniMap1.Width = 256;
            this.mapControl1.MiniMap = miniMap1;
            this.mapControl1.MinZoomLevel = 2D;
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.SelectionMode = DevExpress.XtraMap.ElementSelectionMode.Single;
            this.mapControl1.Size = new System.Drawing.Size(768, 634);
            this.mapControl1.TabIndex = 1;
            this.mapControl1.ZoomLevel = 3D;
            this.mapControl1.SelectionChanged += new DevExpress.XtraMap.MapSelectionChangedEventHandler(this.mapControl1_SelectionChanged);
            this.mapControl1.Click += new System.EventHandler(this.mapControl1_Click);
            this.mapControl1.DoubleClick += new System.EventHandler(this.mapControl1_DoubleClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.flyoutPanel1);
            this.splitContainerControl1.Panel1.Controls.Add(this.mapControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.galleryGridControl);
            this.splitContainerControl1.Panel2.Controls.Add(this.standaloneBarDockControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1230, 634);
            this.splitContainerControl1.SplitterPosition = 457;
            this.splitContainerControl1.TabIndex = 16;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // flyoutPanel1
            // 
            this.flyoutPanel1.Controls.Add(this.flyoutPanelControl1);
            this.flyoutPanel1.Location = new System.Drawing.Point(36, 46);
            this.flyoutPanel1.Name = "flyoutPanel1";
            this.flyoutPanel1.Size = new System.Drawing.Size(689, 519);
            this.flyoutPanel1.TabIndex = 2;
            // 
            // flyoutPanelControl1
            // 
            this.flyoutPanelControl1.Controls.Add(this.flyoutPanel2);
            this.flyoutPanelControl1.Controls.Add(this.popupViewer1);
            this.flyoutPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl1.FlyoutPanel = this.flyoutPanel1;
            this.flyoutPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.flyoutPanelControl1.Name = "flyoutPanelControl1";
            this.flyoutPanelControl1.Size = new System.Drawing.Size(689, 519);
            this.flyoutPanelControl1.TabIndex = 0;
            // 
            // flyoutPanel2
            // 
            this.flyoutPanel2.Controls.Add(this.flyoutPanelControl2);
            this.flyoutPanel2.Location = new System.Drawing.Point(22, 16);
            this.flyoutPanel2.Name = "flyoutPanel2";
            this.flyoutPanel2.Size = new System.Drawing.Size(321, 212);
            this.flyoutPanel2.TabIndex = 3;
            // 
            // flyoutPanelControl2
            // 
            this.flyoutPanelControl2.Controls.Add(this.geoLocationControl1);
            this.flyoutPanelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl2.FlyoutPanel = this.flyoutPanel2;
            this.flyoutPanelControl2.Location = new System.Drawing.Point(0, 0);
            this.flyoutPanelControl2.Name = "flyoutPanelControl2";
            this.flyoutPanelControl2.Size = new System.Drawing.Size(321, 212);
            this.flyoutPanelControl2.TabIndex = 0;
            // 
            // geoLocationControl1
            // 
            this.geoLocationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geoLocationControl1.File = null;
            this.geoLocationControl1.GeoPoint = null;
            this.geoLocationControl1.Location = new System.Drawing.Point(2, 2);
            this.geoLocationControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.geoLocationControl1.Name = "geoLocationControl1";
            this.geoLocationControl1.Size = new System.Drawing.Size(317, 208);
            this.geoLocationControl1.TabIndex = 0;
            this.geoLocationControl1.CancelClick += new System.EventHandler(this.geoLocationControl1_CancelClick);
            this.geoLocationControl1.OkClick += new System.EventHandler(this.geoLocationControl1_OkClick);
            // 
            // popupViewer1
            // 
            this.popupViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popupViewer1.Files = null;
            this.popupViewer1.Location = new System.Drawing.Point(2, 2);
            this.popupViewer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.popupViewer1.Name = "popupViewer1";
            this.popupViewer1.Size = new System.Drawing.Size(685, 515);
            this.popupViewer1.TabIndex = 0;
            this.popupViewer1.CloseClick += new System.EventHandler(this.popupViewer1_CloseClick);
            // 
            // galleryGridControl
            // 
            this.galleryGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.galleryGridControl.Location = new System.Drawing.Point(0, 31);
            this.galleryGridControl.MainView = this.galleryView;
            this.galleryGridControl.Name = "galleryGridControl";
            this.galleryGridControl.Size = new System.Drawing.Size(457, 603);
            this.galleryGridControl.TabIndex = 0;
            this.galleryGridControl.ToolTipController = this.toolTipController1;
            this.galleryGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.galleryView});
            // 
            // galleryView
            // 
            this.galleryView.ContextButtonOptions.TopPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.galleryView.ContextButtonOptions.TopPanelPadding = new System.Windows.Forms.Padding(2);
            contextButton1.Alignment = DevExpress.Utils.ContextItemAlignment.TopFar;
            contextButton1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            contextButton1.AppearanceHover.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            contextButton1.AppearanceHover.ForeColor = System.Drawing.Color.White;
            contextButton1.AppearanceHover.Options.UseBackColor = true;
            contextButton1.AppearanceHover.Options.UseForeColor = true;
            contextButton1.AppearanceNormal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            contextButton1.AppearanceNormal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            contextButton1.AppearanceNormal.Options.UseBackColor = true;
            contextButton1.AppearanceNormal.Options.UseForeColor = true;
            contextButton1.Glyph = ((System.Drawing.Image)(resources.GetObject("contextButton1.Glyph")));
            contextButton1.Id = new System.Guid("6e91aeed-9ae6-403f-bc67-28e3c343e593");
            contextButton1.Name = "GeoEditingButton";
            contextButton1.Visibility = DevExpress.Utils.ContextItemVisibility.Hidden;
            this.galleryView.ContextButtons.Add(contextButton1);
            this.galleryView.GridControl = this.galleryGridControl;
            this.galleryView.Name = "galleryView";
            this.galleryView.OptionsImageLoad.AsyncLoad = true;
            this.galleryView.OptionsImageLoad.CacheThumbnails = false;
            this.galleryView.OptionsSelection.AllowMarqueeSelection = true;
            this.galleryView.OptionsSelection.ItemSelectionMode = DevExpress.XtraGrid.Views.WinExplorer.IconItemSelectionMode.Click;
            this.galleryView.OptionsSelection.MultiSelect = true;
            this.galleryView.OptionsView.ContentHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.galleryView.OptionsView.ImageLayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomOutside;
            this.galleryView.OptionsView.ItemHoverBordersShowMode = DevExpress.XtraGrid.WinExplorer.ItemHoverBordersShowMode.Always;
            this.galleryView.OptionsView.Style = DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewStyle.ExtraLarge;
            this.galleryView.OptionsViewStyles.ExtraLarge.HorizontalIndent = 5;
            this.galleryView.OptionsViewStyles.ExtraLarge.VerticalIndent = 5;
            this.galleryView.ItemClick += new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemClickEventHandler(this.galleryView_ItemClick);
            this.galleryView.ContextButtonClick += new DevExpress.Utils.ContextItemClickEventHandler(this.galleryView_ContextButtonClick);
            this.galleryView.GetThumbnailImage += new DevExpress.Utils.ThumbnailImageEventHandler(this.galleryView_GetThumbnailImage);
            this.galleryView.CustomDrawItem += new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewCustomDrawItemEventHandler(this.galleryView_CustomDrawItem);
            this.galleryView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.galleryView_MouseDown);
            this.galleryView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.galleryView_MouseUp);
            this.galleryView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.galleryView_MouseMove);
            this.galleryView.DoubleClick += new System.EventHandler(this.galleryView_DoubleClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.AutoSize = true;
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(457, 31);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bcVisibleInViewPort,
            this.bcNotGeotagged,
            this.bcAllItems});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(1117, 127);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bcVisibleInViewPort),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcNotGeotagged),
            new DevExpress.XtraBars.LinkPersistInfo(this.bcAllItems)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Custom 2";
            // 
            // bcVisibleInViewPort
            // 
            this.bcVisibleInViewPort.BindableChecked = true;
            this.bcVisibleInViewPort.Caption = "Visible In Viewport";
            this.bcVisibleInViewPort.Checked = true;
            this.bcVisibleInViewPort.GroupIndex = 1;
            this.bcVisibleInViewPort.Id = 0;
            this.bcVisibleInViewPort.Name = "bcVisibleInViewPort";
            this.bcVisibleInViewPort.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcVisibleInViewPort_CheckedChanged);
            // 
            // bcNotGeotagged
            // 
            this.bcNotGeotagged.Caption = "Not Geotagged";
            this.bcNotGeotagged.GroupIndex = 1;
            this.bcNotGeotagged.Id = 1;
            this.bcNotGeotagged.Name = "bcNotGeotagged";
            this.bcNotGeotagged.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcNotGeotagged_CheckedChanged);
            // 
            // bcAllItems
            // 
            this.bcAllItems.Caption = "All Photos";
            this.bcAllItems.GroupIndex = 1;
            this.bcAllItems.Id = 2;
            this.bcAllItems.Name = "bcAllItems";
            this.bcAllItems.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcAllItems_CheckedChanged);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1230, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 634);
            this.barDockControlBottom.Size = new System.Drawing.Size(1230, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 634);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1230, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 634);
            // 
            // MapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "MapView";
            this.Size = new System.Drawing.Size(1230, 634);
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel1)).EndInit();
            this.flyoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).EndInit();
            this.flyoutPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel2)).EndInit();
            this.flyoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).EndInit();
            this.flyoutPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.galleryGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraMap.MapControl mapControl1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl galleryGridControl;
        private DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView galleryView;
        private DevExpress.Utils.FlyoutPanel flyoutPanel1;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl1;
        private PopupViewer popupViewer1;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarCheckItem bcVisibleInViewPort;
        private DevExpress.XtraBars.BarCheckItem bcNotGeotagged;
        private DevExpress.Utils.FlyoutPanel flyoutPanel2;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl2;
        private GeoLocationControl geoLocationControl1;
        private DevExpress.XtraBars.BarCheckItem bcAllItems;
        private DevExpress.Utils.ToolTipController toolTipController1;

    }
}
