using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Controls;
using System.Windows.Forms;
using DevExpress.Utils.Drawing.Animation;
using DevExpress.Utils;
using PhotoAssistant.Core;

namespace PhotoAssistant.Controls.Win.EditingControls {
    public class RepositoryItemPictureNavigator : RepositoryItemPictureEdit {
        internal static string PictureNavigatorName = "PictureMap";
        public override string EditorTypeName { get { return PictureNavigatorName; } }

        static RepositoryItemPictureNavigator() {
            PictureNavigator.Register();
        }

        protected override PictureSizeMode DefaultSizeMode {
            get { return PictureSizeMode.Squeeze; }
        }

        protected override Padding DefaultPadding {
            get { return new Padding(10); }
        }

        private IPictureNavigatorClient viewer;
        [DefaultValue(null)]
        public IPictureNavigatorClient Client {
            get { return viewer; }
            set {
                if(Client == value)
                    return;
                IPictureNavigatorClient prev = Client;
                viewer = value;
                OnViewerChanged(prev);
            }
        }

        private void OnViewerChanged(IPictureNavigatorClient prev) {
            if(prev != null)
                prev.PropertiesChanged -= OnViewerPropertiesChanged;
            if(Client != null)
                Client.PropertiesChanged += OnViewerPropertiesChanged;
            OnPropertiesChanged();
        }

        private void OnViewerPropertiesChanged(object sender, EventArgs e) {
            if(OwnerEdit != null) { 
                OwnerEdit.Invalidate();
                OwnerEdit.Update();
            }
        }
    }

    public class PictureNavigatorViewInfo : PictureEditViewInfo {
        public PictureNavigatorViewInfo(RepositoryItem item) : base(item) { }

        public RepositoryItemPictureNavigator MapItem {
            get { return (RepositoryItemPictureNavigator)Item; }
        }

        public override bool AllowDrawFocusRect {
            get { return false; }
            set { }
        }

        protected override bool UseDefaultLayoutMode {
            get { return true; }
        }

        public Rectangle LastViewportBounds { get; set; }

        protected override void CalcPictureBounds() {
            base.CalcPictureBounds();
        }

        public Rectangle ViewportBounds {
            get {
                if(MapItem.Client == null)
                    return Rectangle.Empty;

                float zoomX =  (float)Math.Max(1.0f, MapItem.Client.ImageSize.Width * MapItem.Client.Zoom / MapItem.Client.ScreenSize.Width);
                float zoomY = (float)Math.Max(1.0f, MapItem.Client.ImageSize.Height * MapItem.Client.Zoom / MapItem.Client.ScreenSize.Height);

                int viewPortWidth = (int)(PictureScreenBounds.Width / zoomX + 0.5f);
                int viewPortHeight = (int)(PictureScreenBounds.Height / zoomY + 0.5f);

                int viewPortX = (int)(PictureScreenBounds.X + MapItem.Client.ScrollPosition.X / MapItem.Client.ImageSize.Width * PictureScreenBounds.Width + 0.5f);
                int viewPortY = (int)(PictureScreenBounds.Y + MapItem.Client.ScrollPosition.Y / MapItem.Client.ImageSize.Height * PictureScreenBounds.Height + 0.5f);

                LastViewportBounds = new Rectangle(viewPortX, viewPortY, viewPortWidth, viewPortHeight);
                return LastViewportBounds;
            }
        }

        protected RectangleF AnimatedStartBounds { get; set; }
        protected RectangleF AnimatedEndBounds { get; set; }
        public Rectangle AnimatedBounds {
            get {
                FloatAnimationInfo info = (FloatAnimationInfo)XtraAnimator.Current.Get(this, BoundsAnimationId);
                if(info == null)
                    return ViewportBounds;

                int x = (int)(AnimatedStartBounds.X + (AnimatedEndBounds.X - AnimatedStartBounds.X) * info.Value  + 0.5f);
                int y = (int)(AnimatedStartBounds.Y + (AnimatedEndBounds.Y - AnimatedStartBounds.Y) * info.Value + 0.5f);
                int width = (int)(AnimatedStartBounds.Width + (AnimatedEndBounds.Width - AnimatedStartBounds.Width) * info.Value + 0.5f);
                int height = (int)(AnimatedStartBounds.Height + (AnimatedEndBounds.Height - AnimatedStartBounds.Height) * info.Value + 0.5f);

                return new Rectangle(x, y, width, height);
            }
        }
        public bool IsAnimated {
            get {
                return XtraAnimator.Current.Get(this, BoundsAnimationId) != null;
            }
        }

        public bool SuppressViewportNavigation { get; internal set; }

        internal readonly object BoundsAnimationId = new object();
        internal void AnimateViewPortBounds(Rectangle start, Rectangle end) {
            if(IsAnimated) {
                AnimatedStartBounds = AnimatedBounds;
                AnimatedEndBounds = end;
                return;
            }
            AnimatedStartBounds = start;
            AnimatedEndBounds = end;
            XtraAnimator.Current.Animations.Add(new MapNavigatorBoundsAnimation(this));
        }
        internal void RemoveAnimation() {
            XtraAnimator.Current.Animations.Remove(this, BoundsAnimationId);
        }

        internal void ResetLastViewportBounds() {
            LastViewportBounds = Rectangle.Empty;
        }
    }

    public class MapNavigatorBoundsAnimation : FloatAnimationInfo {
        public MapNavigatorBoundsAnimation(PictureNavigatorViewInfo viewInfo) : base(viewInfo, viewInfo.BoundsAnimationId, 300, 0.0f, 1.0f, true) {
            ViewInfo = viewInfo;
        }
        protected PictureNavigatorViewInfo ViewInfo { get; private set; }
        protected override void Invalidate() {
            base.Invalidate();
            if(ViewInfo != null) {
                ViewInfo.OwnerEdit.Invalidate();
                ViewInfo.OwnerEdit.Update();
            }
        }
    }

    public class PictureNavigatorPainter : PictureEditPainter {
        protected override void DrawContent(ControlGraphicsInfoArgs info) {
            base.DrawContent(info);
            DrawViewportBounds(info);
        }

        protected void DrawBounds(ControlGraphicsInfoArgs info, Rectangle rect) {
            info.Graphics.DrawRectangle(info.Cache.GetPen(Color.White), rect);
            rect.Inflate(-1, -1);
            info.Graphics.DrawRectangle(info.Cache.GetPen(Color.DarkGray), rect);
        }

        private void DrawViewportBounds(ControlGraphicsInfoArgs info) {
            PictureNavigatorViewInfo vi = (PictureNavigatorViewInfo)info.ViewInfo;
            Rectangle lastBounds = vi.LastViewportBounds;
            if(vi.IsAnimated) {
                DrawBounds(info, vi.AnimatedBounds);
                return;
            }
            if(lastBounds != vi.ViewportBounds && !vi.SuppressViewportNavigation) {
                vi.AnimateViewPortBounds(lastBounds, vi.ViewportBounds);
                return;
            }
            DrawBounds(info, vi.ViewportBounds);
        }
    }

    [ToolboxItem(true)]
    public class PictureNavigator : PictureEdit {
        static PictureNavigator() {
            Register();
        }

        protected override void OnSizeChanged(EventArgs e) {
            ((PictureNavigatorViewInfo)ViewInfo).RemoveAnimation();
            base.OnSizeChanged(e);
        }

        protected override Cursor DefaultCursor {
            get { return Cursors.Cross; }
        }

        public static void Register() {
            if(EditorRegistrationInfo.Default.Editors.Contains(RepositoryItemPictureNavigator.PictureNavigatorName))
                return;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(RepositoryItemPictureNavigator.PictureNavigatorName, typeof(PictureNavigator),
                typeof(RepositoryItemPictureNavigator), typeof(PictureNavigatorViewInfo),
                new PictureNavigatorPainter(), true, EditImageIndexes.PictureEdit));
        }

        public override string EditorTypeName {
            get { return RepositoryItemPictureNavigator.PictureNavigatorName; }
        }

        public new RepositoryItemPictureNavigator Properties { get{ return (RepositoryItemPictureNavigator)base.Properties; } }

        PictureNavigatorViewInfo NavigatorInfo {
            get { return (PictureNavigatorViewInfo)ViewInfo; }
        }

        Cursor PrevCursor { get; set; }
        protected override void OnMouseDown(MouseEventArgs e) {
            DXMouseEventArgs ee = new DXMouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta);
            base.OnMouseDown(ee);
            if(ee.Handled)
                return;
            if(Properties.Client == null)
                return;
            if(e.Button == MouseButtons.Left)
                UpdateClientScrollPosition(e.Location, true);
        }

        protected override void OnMouseWheelCore(MouseEventArgs ee) {
            base.OnMouseWheelCore(ee);
            Properties.Client.Zoom += ee.Delta / 120.0 * Properties.Client.ZoomChange;
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if(e.Button == MouseButtons.Left) {
                NavigatorInfo.RemoveAnimation();
                NavigatorInfo.SuppressViewportNavigation = true;
                UpdateClientScrollPosition(e.Location, false);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            NavigatorInfo.SuppressViewportNavigation = false;
        }

        PointF ScreenPointToImagePoint(Point point) {
            PointF local = new PointF(
                (point.X - ViewInfo.PictureScreenBounds.X) / ViewInfo.PictureScreenBounds.Width * Properties.Client.ImageSize.Width,
                (point.Y - ViewInfo.PictureScreenBounds.Y) / ViewInfo.PictureScreenBounds.Height * Properties.Client.ImageSize.Height);
            return local;
        }

        private void UpdateClientScrollPosition(Point point, bool animated) {
            PointF local = ScreenPointToImagePoint(point);
            local.X -= (float)(Properties.Client.ScreenSize.Width / 2 / Properties.Client.Zoom);
            local.Y -= (float)(Properties.Client.ScreenSize.Height / 2 / Properties.Client.Zoom);
            Properties.Client.AllowScrollAnimation = animated;
            Properties.Client.ScrollPosition = local;
        }
    }
}
