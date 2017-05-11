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
using DevExpress.XtraMap;
using DevExpress.XtraBars.Ribbon;
using System.Xml.Linq;
using System.Globalization;

using DevExpress.Utils;

using DevExpress.XtraMap.Native;
using DevExpress.XtraGrid.Views.WinExplorer.ViewInfo;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.Map;
using DevExpress.XtraSplashScreen;
using PhotoAssistant.Core.Model;
using PhotoAssistant.UI.ViewHelpers;

namespace PhotoAssistant.UI.View {
    public partial class MapView : ViewControlBase {
        public MapControl MapControl { get { return mapControl1; } }
        public MiniMapVectorItemsLayer MiniMapItemsLayer { get { return (MiniMapVectorItemsLayer)MiniMap.Layers["DataLayer"]; } }
        public MapItemStorage MinimapItemsStorage { get { return (MapItemStorage)MiniMapItemsLayer.Data; } }
        public MiniMap MiniMap { get { return mapControl1.MiniMap; } }
        public BingGeocodeDataProvider GeoCode { get { return (BingGeocodeDataProvider)((InformationLayer)MapControl.Layers["InformationLayer"]).DataProvider; } }

        public MapView(DmModel model) {
            Model = model;
            InitializeComponent();
            InitializeGeoCodeProvider();
            InitializeGeoLocationControl();
        }

        private void InitializeGeoCodeProvider() {
            GeoCode.BingKey = DevexpressBingKey;
            GeoCode.LocationInformationReceived += GeoCode_LocationInformationReceived;
        }

        void GeoCode_LocationInformationReceived(object sender, LocationInformationReceivedEventArgs e) {
            if(e.UserState == null ||
                !string.IsNullOrEmpty(e.Result.FaultReason) ||
                e.Result.ResultCode != RequestResultCode.Success) {
                SplashScreenManager.CloseForm(false);
                return;
            }

            List<DmFile> files = (List<DmFile>)e.UserState;
            BingAddress adress = e.Result.Locations.Length > 0 ? (BingAddress)e.Result.Locations[0].Address : null;

            SplashScreenManager.Default.SendCommand(DmWaitFormCommand.SetCaption, "Map");
            SplashScreenManager.Default.SendCommand(DmWaitFormCommand.SetDescription, "Updating Data...");
            SplashScreenManager.Default.SendCommand(DmWaitFormCommand.SetUndefined, true);

            Model.BeginUpdate();
            foreach(DmFile file in files) {
                Model.BeginUpdateFile(file);
                file.Latitude = (float)HitPoint.Latitude;
                file.Longitude = (float)HitPoint.Longitude;
                if(adress != null) {
                    file.Country = adress.CountryRegion;
                    file.State = adress.AdminDistrict;
                    file.Location = adress.AddressLine;
                    file.City = adress.PostalTown;
                }
                Model.EndUpdateFile(file);
            }
            Model.EndUpdate();
            UpdateLayers();
            RefreshDataSource();
            SplashScreenManager.CloseForm();
        }

        private void RefreshDataSource() {
            this.galleryGridControl.RefreshDataSource();
        }

        private void UpdateLayers() {
            InitializeMap();
            InitializeMiniMap();
        }

        public override void OnShowView() {
            base.OnShowView();
            UpdatePopupViewerSize();
            PhotoDragDpopHelper.Default.DragDrop += Default_DragDrop;
        }

        private void UpdatePopupViewerSize() {
            Screen sc = Screen.FromControl(this);
            int height = (sc.WorkingArea.Height - 100) / 2;
            int widht = (int)(height * Model.Properties.AspectRatio);
            this.flyoutPanel1.Size = new Size(widht, height);
        }

        NotificationForm notificationForm;
        public NotificationForm NotificationForm {
            get {
                if(notificationForm == null)
                    notificationForm = new NotificationForm();
                return notificationForm;
            }
        }

        GeoPoint HitPoint { get; set; }

        void Default_DragDrop(object sender, PhotoDragDropEventArgs e) {
            if(e.Control != MapControl)
                return;
            MapHitInfo hitInfo = MapControl.CalcHitInfo(e.Location);
            GeoPoint point = (GeoPoint)MapControl.ScreenPointToCoordPoint(hitInfo.HitPoint);
            HitPoint = point;
            SplashScreenManager.ShowForm(FindForm(), typeof(WaitForm), true, true);
            SplashScreenManager.Default.SendCommand(DmWaitFormCommand.SetCaption, "Map");
            SplashScreenManager.Default.SendCommand(DmWaitFormCommand.SetDescription, "Retreiving geo location info ...");
            SplashScreenManager.Default.SendCommand(DmWaitFormCommand.SetUndefined, true);
            GeoCode.RequestLocationInformation(point, PhotoDragDpopHelper.Default.Photos);
        }
        public override void OnHideView() {
            base.OnHideView();
            PhotoDragDpopHelper.Default.DragDrop -= Default_DragDrop;
        }

        protected GeoLocationControl GeoLocationControl { get { return this.geoLocationControl1; } }

        private void InitializeGeoLocationControl() {
            Size sz = GeoLocationControl.CalcBestSize();
            sz.Height += 30;
            sz.Width += 30;
            this.flyoutPanel2.Size = sz;
        }

        protected override bool AllowQuickGalleryPanel {
            get { return false; }
        }

        private void InitializeGallery() {
            this.galleryGridControl.DataSource = Files;
            this.galleryView.PopulateColumns();
            this.galleryView.OptionsImageLoad.AnimationType = LibraryControl.ConvertAnimationType( SettingsStore.Default.ImageAnimationType);
            this.galleryGridControl.Resize -= galleryControl1_Resize;
            this.galleryGridControl.Resize += galleryControl1_Resize;
            this.galleryView.ActiveFilterString = "[VisibleOnMap] = true";
            this.galleryView.ColumnSet.ExtraLargeImageColumn = this.galleryView.Columns["ThumbImage"];
            UpdateGalleryImageSize();
        }

        void galleryControl1_Resize(object sender, EventArgs e) {
            UpdateGalleryImageSize();
        }

        int CalcGalleryItemMargins() {
            SkinWinExplorerViewInfo viewInfo = this.galleryView.GetViewInfo() as SkinWinExplorerViewInfo;
            if(viewInfo == null)
                return 0;
            SkinElementInfo info = viewInfo.GetItemBackgroundInfo();
            Rectangle bounds = ObjectPainter.CalcBoundsByClientRectangle(null, SkinElementPainter.Default, info, new Rectangle(0, 0, 100, 100));
            return bounds.Width - 100;
        }

        protected void UpdateGalleryImageSize() {
            if(this.galleryGridControl.Width == 0)
                return;
            int totalWidth = this.galleryGridControl.ClientRectangle.Width - 40;
            const int minWidth = 160;
            int count = 1;
            WinExplorerViewInfo viewInfo = (WinExplorerViewInfo)this.galleryView.GetViewInfo();
            for(int i = 1; i < 20; i++) {
                int newWidth = (totalWidth - (i - 1) * viewInfo.ItemHorizontalIndent) / i;
                if(newWidth < minWidth)
                    break;
                count = i;
            }
            int width = (totalWidth - (count - 1) * viewInfo.ItemHorizontalIndent) / count;
            width -= CalcGalleryItemMargins();
            if(width <= 0)
                return;
            this.galleryView.OptionsViewStyles.ExtraLarge.ImageSize = new Size(width, (int)(width / Model.Properties.AspectRatio));
            this.galleryGridControl.Refresh();
        }

        private void galleryView_GetThumbnailImage(object sender, ThumbnailImageEventArgs e) {
            int rowHandle = this.galleryView.GetRowHandle(e.DataSourceIndex);
            DmFile file = (DmFile)this.galleryView.GetRow(rowHandle);
            ThumbHelper.GetThumbnailImage(sender, e, file);
        }

        private void InitializeLayers() {
            InitializeMap();
            InitializeMiniMap();
        }

        private void InitializeMap() {
            while(MapControl.Layers.Count > 2)
                MapControl.Layers.RemoveAt(MapControl.Layers.Count - 1);
            MapControl.Layers.Add(CreateLayer(20000, 1, 10));
            MapControl.Layers.Add(CreateLayer(1000, 10, 15));
            MapControl.Layers.Add(CreateLayer(100, 15, 20));
        }

        private LayerBase CreateLayer(int distanceRadius, int minZoom, int maxZoom) {
            VectorItemsLayer layer = new VectorItemsLayer();
            layer.Name = "Layer" + distanceRadius;
            layer.MinZoomLevel = minZoom;
            layer.MaxZoomLevel = maxZoom;
            layer.Data = CreateMapItemStorage(distanceRadius);
            layer.ViewportChanged += layer_ViewportChanged;
            return layer;
        }

        void layer_ViewportChanged(object sender, ViewportChangedEventArgs e) {
            if(e.IsAnimated)
                return;
            VectorItemsLayer layer = (VectorItemsLayer)sender;
            double minX = Math.Min(e.TopLeft.GetX(), e.BottomRight.GetX());
            double maxX = Math.Max(e.TopLeft.GetX(), e.BottomRight.GetX());
            double minY = Math.Min(e.TopLeft.GetY(), e.BottomRight.GetY());
            double maxY = Math.Max(e.TopLeft.GetY(), e.BottomRight.GetY());
            foreach(PhotoMapItem item in ((MapItemStorage)layer.Data).Items) {
                double lattitude = item.Location.GetY();
                double longitude = item.Location.GetX();
                bool isVisible = lattitude > minY && lattitude < maxY && longitude > minX && longitude < maxX;
                item.Files.ForEach((file) => { file.VisibleOnMap = isVisible; });
            }
            this.galleryView.RefreshData();
        }

        private IMapDataAdapter CreateMapItemStorage(int distanceRadius) {
            MapItemStorage mapStorage = new MapItemStorage();
            List<PhotoMapItem> items = MapClasterizationHelper.Default.GetClasteredItems(MapControl, Files, distanceRadius);
            mapStorage.Items.BeginUpdate();
            try {
                mapStorage.Items.AddRange(items);
            } finally {
                mapStorage.Items.EndUpdate();
            }
            return mapStorage;
        }

        private void InitializeMiniMap() {
            MiniMapImageTilesLayer miniMapTiles = (MiniMapImageTilesLayer)MiniMap.Layers["ImageTilesLayer"];
            SetBingMapDataProviderKey(miniMapTiles.DataProvider as BingMapDataProvider);
            MiniMapVectorItemsLayer layer = new MiniMapVectorItemsLayer();
            MapItemStorage storage = new MapItemStorage();
            layer.Data = storage;
            foreach(PhotoMapItem item in ((MapItemStorage)((VectorItemsLayer)MapControl.Layers[2]).Data).Items) {
                storage.Items.Add(new MapDot() { Location = item.Location, Size = 4, Fill = Color.Red });
            }
            MapControl.MiniMap.Layers.Add(layer);
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
            UpdateGalleryImageSize();
            TestInitFiles();
            InitializeLayers();
            InitializeGallery();
            UpdateGalleryImageSize();
        }

        private void TestInitFiles() {
            float[] lattitude = new float[] { 51.5072f, 34.033333333333333334f, 43f, 48.8567f, DmFile.InvalidGeoLocation };
            float[] longitude = new float[] { -0.1275f, -118.2666666666666667f, -75f, 2.3508f, DmFile.InvalidGeoLocation };
            string[] city = new string[] { "London", "Los Angeles", "New York", "Paris" };
            int index = 0;
            foreach(DmFile file in Files) {
                //file.Latitude = lattitude[index % lattitude.Length];
                //file.Longitude = longitude[index % longitude.Length];
                file.Latitude = lattitude[0] + (index % 20) * 0.2f;
                file.Longitude = longitude[0] + (index / 20) * 0.2f;
                file.City = city[index % city.Length];
                index++;
            }
        }

        static string key = "AmSNFwVzMvaqFlCYQx9RRUfcAwSQCzi_Vcesric6JFQuBO9wZFXEsqzili-INaUA";
        public static string DevexpressBingKey { get { return key; } }
        protected void SetBingMapDataProviderKey(BingMapDataProvider provider) {
            if(provider != null) provider.BingKey = DevexpressBingKey;
        }
        protected BingMapDataProvider CreateBingDataProvider(BingMapKind kind) {
            return new BingMapDataProvider() { BingKey = DevexpressBingKey, Kind = kind };
        }
        protected BingGeocodeDataProvider CreateGeoCodeDataProvider() {
            return new BingGeocodeDataProvider() { BingKey = DevexpressBingKey, MaxVisibleResultCount = 1 };
        }
        protected BingRouteDataProvider CreateRouteDataProvider() {
            return new BingRouteDataProvider() { BingKey = DevexpressBingKey };
        }
        protected BingSearchDataProvider CreateSearchDataProvider() {
            return new BingSearchDataProvider() { BingKey = DevexpressBingKey };
        }
        void timer1_Tick(object sender, EventArgs e) {
        }
        protected PopupViewer PopupViewer { get { return this.popupViewer1; } }

        private void mapControl1_SelectionChanged(object sender, MapSelectionChangedEventArgs e) { }

        private void HidePopupViewer(bool immediate) {
            this.flyoutPanel1.HideBeakForm(immediate);
            PopupViewer.Files = null;
        }

        private void popupViewer1_CloseClick(object sender, EventArgs e) {
            HidePopupViewer(false);
        }

        private void galleryView_ItemClick(object sender, DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemClickEventArgs e) {
            DmFile file = (DmFile)this.galleryView.GetRow(e.ItemInfo.Row.RowHandle);
            if(file.HasGeoLocation) {
                MapControl.SetCenterPoint(new GeoPoint(file.Latitude, file.Longitude), true);
                MapControl.ZoomLevel = MapControl.Layers[MapControl.Layers.Count - 1].MinZoomLevel;
            }
        }

        bool IsInGeoLocationEditingMode { get { return this.flyoutPanel2.IsPopupOpen; } }

        private void mapControl1_Click(object sender, EventArgs e) {
            MapHitInfo hitInfo = MapControl.CalcHitInfo(PointToClient(Control.MousePosition));

            if(hitInfo.HitObjects == null || hitInfo.HitObjects.Length == 0 || !(hitInfo.HitObjects[0] is PhotoMapItem)) {
                HidePopupViewer(false);
            } else {
                HidePopupViewer(true);
                PhotoMapItem item = ((PhotoMapItem)hitInfo.HitObjects[0]);
                MapPoint point = MapControl.CoordPointToScreenPoint(item.Location);
                PopupViewer.Files = item.Files;
                this.flyoutPanel1.ShowBeakForm(MapControl.PointToScreen(new Point((int)point.X, (int)point.Y)), false, this, new Point(0, 40));
                PopupViewer.Focus();
            }
        }

        private void bcVisibleInViewPort_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcVisibleInViewPort.Checked)
                this.galleryView.ActiveFilterString = "[VisibleOnMap] = true";
        }

        private void bcNotGeotagged_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcNotGeotagged.Checked)
                this.galleryView.ActiveFilterString = "[HasGeoLocation] = false";
        }

        private void bcAllItems_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcAllItems.Checked)
                this.galleryView.ActiveFilterString = null;
        }

        private void geoLocationControl1_OkClick(object sender, EventArgs e) {
            this.flyoutPanel2.HideBeakForm();
        }

        private void geoLocationControl1_CancelClick(object sender, EventArgs e) {
            this.flyoutPanel2.HideBeakForm();
        }

        private void mapControl1_DoubleClick(object sender, EventArgs e) {
            if(!IsInGeoLocationEditingMode)
                return;
        }

        private void galleryView_ContextButtonClick(object sender, ContextItemClickEventArgs e) {
            DmFile file = (DmFile)this.galleryView.GetRow((int)e.DataItem);
            Point pt = this.galleryGridControl.PointToScreen(new Point(e.ItemInfo.Bounds.X + e.ItemInfo.Bounds.Width / 2, e.ItemInfo.Bounds.Y + e.ItemInfo.Bounds.Height / 2));
            this.flyoutPanel2.HideBeakForm(true);
            GeoLocationControl.File = file;
            this.flyoutPanel2.ShowBeakForm(pt, false, this.galleryGridControl, new Point(0, e.ItemInfo.Bounds.Height / 2));
        }

        private void galleryView_MouseDown(object sender, MouseEventArgs e) {
            WinExplorerViewHitInfo hitInfo = this.galleryView.CalcHitInfo(e.Location);
            if(!hitInfo.InItem)
                return;
            int[] rows = this.galleryView.GetSelectedRows();
            List<DmFile> files = new List<DmFile>();
            for(int i = 0; i < rows.Length; i++) {
                files.Add((DmFile)this.galleryView.GetRow(rows[i]));
            }
            if(files.Count == 0)
                return;

            Point loc = new Point(Control.MousePosition.X + 16, Control.MousePosition.Y + 16);
            PhotoDragDpopHelper.Default.OnMouseDown(FindForm(), this.galleryGridControl, e, files, new Rectangle(loc, this.galleryView.OptionsViewStyles.ExtraLarge.ImageSize));
        }

        private void galleryView_MouseMove(object sender, MouseEventArgs e) {
            PhotoDragDpopHelper.Default.OnMouseMove(e);
        }

        private void galleryView_MouseUp(object sender, MouseEventArgs e) {
            PhotoDragDpopHelper.Default.OnMouseUp();
        }

        private void galleryView_DoubleClick(object sender, EventArgs e) {
            PhotoDragDpopHelper.Default.OnDoubleClick();
        }

        private void galleryView_CustomDrawItem(object sender, DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewCustomDrawItemEventArgs e) {
            if(!e.IsAnimated) {
                SkinElementInfo info = new SkinElementInfo(CustomSkinHelper.CustomGalleryBorder, e.Bounds);
                info.ImageIndex = 0;
                if(e.ItemInfo.IsHovered)
                    info.ImageIndex = 1;
                else if(e.ItemInfo.IsPressed || e.ItemInfo.IsSelected)
                    info.ImageIndex = 2;
                ObjectPainter.DrawObject(e.Cache, SkinElementPainter.Default, info);
            }
            e.DrawItemImage();
            e.DrawContextButtons();
            e.Handled = true;
        }
    }
}
