using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
namespace PhotoAssistant.Controls.Wpf {
    public class CropToolManager : IGridManagerOwner {
        public CropToolManager(PicturePreviewControl preview) {
        Preview = preview;
            MinWidth = 10;
            MinHeight = 10;
        }
        public PicturePreviewControl Preview {
            get; private set;
        }
        public double ContentPadding => 30;
        protected double SavedContentPadding {
            get; set;
        }
        public bool IsActive {
            get; set;
        }
        public double Angle {
            get; set;
        }
        public double Width {
            get; set;
        }
        public double Height {
            get; set;
        }
        public double MinWidth {
            get; set;
        }
        public double MinHeight {
            get; set;
        }
        public Point Origin {
            get; set;
        }
        public void Activate() {
            if(IsActive) {
                return;
            }

            IsActive = true;
            SavedContentPadding = Preview.ContentPadding;
            Width = Preview.ImageInfo.ImageSize.Width;
            Height = Preview.ImageInfo.ImageSize.Height;
            Origin = new Point(Width / 2, Height / 2);
            Angle = 0.0;
            Preview.ContentPaddingCore = ContentPadding;
            Preview.FitToScreen(true);
            UpdatePreview();
            LastDragPoint = InvalidDragPoint;
            SavedWidth = double.PositiveInfinity;
            SavedHeight = double.PositiveInfinity;
        }
        Point InvalidDragPoint = new Point(100000, 100000);
        public void Deactivate() {
            if(!IsActive) {
                return;
            }

            IsActive = false;
            Preview.ContentPaddingCore = SavedContentPadding;
            Preview.FitToScreen(true);
            Preview.InvalidateVisual();
            Preview.ImageInfo.SuppressCalcLayout = false;
        }
        GridManager gridManager;
        protected GridManager GridManager {
            get {
                if(gridManager == null) {
                    gridManager = CreateGridManager();
                }

                return gridManager;
            }
        }
        protected Rect Screen => Preview.ImageInfo.Screen;
        protected double Zoom => Preview.ImageInfo.Info.Zoom;
        protected Rect BoundsCore {
            get {
                Point screenPoint = new Point(Screen.X + Screen.Width / 2, Screen.Y + Screen.Height / 2);
                return new Rect(screenPoint.X - Width / 2 * Zoom, screenPoint.Y - Height / 2 * Zoom, Width * Zoom, Height * Zoom);
            }
        }
        Rect IGridManagerOwner.Bounds => BoundsCore;
        Rect IGridManagerOwner.Screen => Preview.ImageInfo.Screen;
        protected virtual GridManager CreateGridManager() => new GridManager(this) { IsHitTestVisible = true };
        public void Render(DrawingContext drawingContext) {
            RenderShadowArea(drawingContext);
            RenderGrid(drawingContext);
        }
        protected virtual void RenderShadowArea(DrawingContext drawingContext) {
            RectangleGeometry outerBounds = new RectangleGeometry(new Rect(0, 0, Preview.ActualWidth, Preview.ActualHeight));
            RectangleGeometry innerBounds = new RectangleGeometry(BoundsCore);
            CombinedGeometry finalClip = new CombinedGeometry(GeometryCombineMode.Exclude, outerBounds, innerBounds);
            drawingContext.PushClip(finalClip);
            drawingContext.PushTransform(new RotateTransform(Preview.ImageInfo.Info.RotateAngle, Preview.ImageInfo.Info.RotateOrigin.X, Preview.ImageInfo.Info.RotateOrigin.Y));
            drawingContext.DrawRectangle(new SolidColorBrush(Color.FromArgb(100, 0, 0, 0)), null, Preview.ImageInfo.Info.ScreenBounds);
            drawingContext.Pop();
            drawingContext.Pop();
        }
        void RenderGrid(DrawingContext drawingContext) {
            GridManager.Angle = 0;
            GridManager.Mode = GridMode.UseCount;
            GridManager.ShowThumbs = true;
            GridManager.Render(drawingContext);
        }
        void IGridManagerOwner.InvalidateVisual() => Preview.InvalidateVisual();
        protected CropArea SelectedArea {
            get; set;
        }
        Point LastDragPoint {
            get; set;
        }
        public virtual void OnMouseUp(MouseButtonState leftButton, Point location) {
            LastDragPoint = InvalidDragPoint;
            SavedWidth = double.PositiveInfinity;
            SavedHeight = double.PositiveInfinity;
        }
        public virtual bool OnMouseDown(MouseButtonState leftButton, Point location) {
            if(leftButton == MouseButtonState.Released) {
                return false;
            }

            GridManager.OnMouseDown(leftButton, location);
            LastDragPoint = location;
            if(GridManager.HoverInfo.HitTest != GridManagerHitTest.None) {
                return true;
            }

            SelectedArea = CalcHitInfo(location);
            return SelectedArea != CropArea.None;
        }
        protected virtual CropArea CalcHitInfo(Point location) {
            if(BoundsCore.Contains(location)) {
                return CropArea.DragArea;
            }

            if(TopLeftArea.Contains(location)) {
                return CropArea.TopLeft;
            }

            if(TopCenterArea.Contains(location)) {
                return CropArea.TopCenter;
            }

            if(TopRightArea.Contains(location)) {
                return CropArea.TopRight;
            }

            if(BottomLeftArea.Contains(location)) {
                return CropArea.BottomLeft;
            }

            if(BottomRightArea.Contains(location)) {
                return CropArea.BottomRight;
            }

            if(BottomCenterArea.Contains(location)) {
                return CropArea.BottomCenter;
            }

            if(MiddleLeftArea.Contains(location)) {
                return CropArea.MiddleLeft;
            }

            if(MiddleRightArea.Contains(location)) {
                return CropArea.MiddleRight;
            }

            return CropArea.None;
        }
        protected Rect TopLeftArea => new Rect(BoundsCore.X - 10000, BoundsCore.Y - 10000, 10000 + BoundsCore.Width / 3, 10000 + BoundsCore.Height / 3);
        protected Rect TopRightArea => new Rect(BoundsCore.Right - BoundsCore.Width / 3, BoundsCore.Y - 10000, 10000, 10000 + BoundsCore.Height / 3);
        protected Rect TopCenterArea => new Rect(BoundsCore.X + BoundsCore.Width / 3, BoundsCore.Y - 10000, BoundsCore.Width / 3, 10000);
        protected Rect BottomLeftArea => new Rect(BoundsCore.X - 10000, BoundsCore.Bottom - BoundsCore.Height / 3, 10000 + BoundsCore.Width / 3, 10000);
        protected Rect BottomCenterArea => new Rect(BoundsCore.X + BoundsCore.Width / 3, BoundsCore.Bottom, BoundsCore.Width / 3, 10000);
        protected Rect BottomRightArea => new Rect(BoundsCore.Right - BoundsCore.Width / 3, BoundsCore.Bottom - BoundsCore.Height / 3, 10000, 10000);
        protected Rect MiddleLeftArea => new Rect(BoundsCore.X - 10000, BoundsCore.Y + BoundsCore.Height / 3, 10000, BoundsCore.Height / 3);
        protected Rect MiddleRightArea => new Rect(BoundsCore.Right, BoundsCore.Y + BoundsCore.Height / 3, 10000, BoundsCore.Height / 3);
        public bool OnMouseMove(MouseButtonState leftButton, Point location) {
            if(leftButton == MouseButtonState.Pressed) {
                if(LastDragPoint == InvalidDragPoint) {
                    LastDragPoint = location;
                }

                if(GridManager.HoverInfo.HitTest != GridManagerHitTest.None) {
                    DoSizing(location);
                } else if(SelectedArea == CropArea.DragArea) {
                    DoDrag(location);
                } else if(SelectedArea != CropArea.None) {
                    MakeRotation(location);
                }
                return true;
            } else if(GridManager.OnMouseMove(location)) {
                SelectedArea = CropArea.None;
                UpdateCursor();
                return true;
            }
            SelectedArea = CalcHitInfo(location);
            UpdateCursor();
            return true;
        }
        protected void UpdateCursor() {
            if(GridManager.HoverInfo.HitTest != GridManagerHitTest.None) {
                GridManager.UpdateCursor();
                return;
            }
            switch(SelectedArea) {
                case CropArea.DragArea:
                    Mouse.OverrideCursor = Cursors.SizeAll;
                    break;
                case CropArea.None:
                    Mouse.OverrideCursor = Cursors.Arrow;
                    break;
                case CropArea.TopLeft:
                    Mouse.OverrideCursor = CursorRotateTopLeft;
                    break;
                case CropArea.TopCenter:
                    Mouse.OverrideCursor = CursorRotateTopCenter;
                    break;
                case CropArea.TopRight:
                    Mouse.OverrideCursor = CursorRotateTopRight;
                    break;
                case CropArea.BottomLeft:
                    Mouse.OverrideCursor = CursorRotateBottomLeft;
                    break;
                case CropArea.BottomCenter:
                    Mouse.OverrideCursor = CursorRotateBottomCenter;
                    break;
                case CropArea.BottomRight:
                    Mouse.OverrideCursor = CursorRotateBottomRight;
                    break;
                case CropArea.MiddleLeft:
                    Mouse.OverrideCursor = CursorRotateLeft;
                    break;
                case CropArea.MiddleRight:
                    Mouse.OverrideCursor = CursorRotateRight;
                    break;
            }
        }
        Cursor cursorRotateTopLeft;
        protected Cursor CursorRotateTopLeft {
            get {
                if(cursorRotateTopLeft == null) {
                    cursorRotateTopLeft = new Cursor(new MemoryStream(Properties.Resources.CursorRotateTopLeft));
                }

                return cursorRotateTopLeft;
            }
        }
        Cursor cursorRotateTopCenter;
        protected Cursor CursorRotateTopCenter {
            get {
                if(cursorRotateTopCenter == null) {
                    cursorRotateTopCenter = new Cursor(new MemoryStream(Properties.Resources.CursorRotateTop));
                }

                return cursorRotateTopCenter;
            }
        }
        Cursor cursorRotateTopRight;
        protected Cursor CursorRotateTopRight {
            get {
                if(cursorRotateTopRight == null) {
                    cursorRotateTopRight = new Cursor(new MemoryStream(Properties.Resources.CursorRotateTopRight));
                }

                return cursorRotateTopRight;
            }
        }
        Cursor cursorRotateBottomLeft;
        protected Cursor CursorRotateBottomLeft {
            get {
                if(cursorRotateBottomLeft == null) {
                    cursorRotateBottomLeft = new Cursor(new MemoryStream(Properties.Resources.CursorRotateBottomLeft));
                }

                return cursorRotateBottomLeft;
            }
        }
        Cursor cursorRotateBottomCenter;
        protected Cursor CursorRotateBottomCenter {
            get {
                if(cursorRotateBottomCenter == null) {
                    cursorRotateBottomCenter = new Cursor(new MemoryStream(Properties.Resources.CursorRotateBottom));
                }

                return cursorRotateBottomCenter;
            }
        }
        Cursor cursorRotateBottomRight;
        protected Cursor CursorRotateBottomRight {
            get {
                if(cursorRotateBottomRight == null) {
                    cursorRotateBottomRight = new Cursor(new MemoryStream(Properties.Resources.CursorRotateBottomRight));
                }

                return cursorRotateBottomRight;
            }
        }
        Cursor cursorRotateLeft;
        protected Cursor CursorRotateLeft {
            get {
                if(cursorRotateLeft == null) {
                    cursorRotateLeft = new Cursor(new MemoryStream(Properties.Resources.CursorRotateLeft));
                }

                return cursorRotateLeft;
            }
        }
        Cursor cursorRotateRight;
        protected Cursor CursorRotateRight {
            get {
                if(cursorRotateRight == null) {
                    cursorRotateRight = new Cursor(new MemoryStream(Properties.Resources.CursorRotateRight));
                }

                return cursorRotateRight;
            }
        }
        protected Point ScreenOrigin => new Point(Screen.X + Screen.Width / 2, Screen.Y + Screen.Height / 2);
        protected double SavedWidth {
            get; set;
        }
        protected double SavedHeight {
            get; set;
        }
        protected virtual void MakeRotation(Point location) {
            if(double.IsPositiveInfinity(SavedWidth)) {
                SavedWidth = Width;
            }

            if(double.IsPositiveInfinity(SavedHeight)) {
                SavedHeight = Height;
            }

            Width = SavedWidth;
            Height = SavedHeight;
            Vector rotateVector = new Vector(location.X - LastDragPoint.X, location.Y - LastDragPoint.Y);
            Vector v1 = new Vector(location.X - ScreenOrigin.X, location.Y - ScreenOrigin.Y);
            Vector v2 = new Vector(LastDragPoint.X - ScreenOrigin.X, LastDragPoint.Y - ScreenOrigin.Y);
            LastDragPoint = location;
            Angle -= Vector.AngleBetween(v2, v1);
            ConstrainBounds();
            UpdatePreview();
        }
        protected Point ConstrainPoint(Point pt) {
            if(pt.X < 0) {
                pt.X = 0;
            }

            if(pt.X > ImageSize.Width) {
                pt.X = ImageSize.Width;
            }

            if(pt.Y < 0) {
                pt.Y = 0;
            }

            if(pt.Y > ImageSize.Height) {
                pt.Y = ImageSize.Height;
            }

            return pt;
        }
        protected virtual void ConstrainBounds() {
            Point topLeft = CalcTopLeftPoint();
            Point topRight = CalcTopRightPoint();
            Point bottomLeft = CalcBottomLeftPoint();
            Point bottomRight = CalcBottomRightPoint();

            topLeft = ConstrainPoint(topLeft);
            topRight = ConstrainPoint(topRight);
            bottomLeft = ConstrainPoint(bottomLeft);
            bottomRight = ConstrainPoint(bottomRight);

            double width = GetXLen(topLeft, topRight);
            double width2 = GetXLen(bottomLeft, bottomRight);
            double width3 = GetXLen(topLeft, bottomRight);
            double width4 = GetXLen(bottomLeft, topRight);

            double height = GetYLen(topLeft, bottomLeft);
            double height2 = GetYLen(topRight, bottomRight);
            double height3 = GetYLen(topLeft, bottomRight);
            double height4 = GetYLen(bottomLeft, topRight);

            Width = Math.Min(Math.Min(width3, width4), Math.Min(width, width2));
            Height = Math.Min(Math.Min(height3, height4), Math.Min(height, height2));
        }
        protected virtual void DoDrag(Point location) {
            Vector delta = GetConvertDelta(location);
            LastDragPoint = location;
            Point topLeft = CalcTopLeftPoint();
            Point topRight = CalcTopRightPoint();
            Point bottomLeft = CalcBottomLeftPoint();
            Point bottomRight = CalcBottomRightPoint();

            delta = ConstrainDelta(delta, topLeft, true, true);
            delta = ConstrainDelta(delta, topRight, true, true);
            delta = ConstrainDelta(delta, bottomLeft, true, true);
            delta = ConstrainDelta(delta, bottomRight, true, true);

            Origin = AddVector(Origin, delta);
            UpdatePreview();
        }
        protected Vector GetBasisI() {
            double angle = Angle / 180 * Math.PI;
            return new Vector(Math.Cos(angle), Math.Sin(angle));
        }
        protected Vector GetBasisJ() {
            double angle = Angle / 180 * Math.PI;
            return new Vector(-Math.Sin(angle), Math.Cos(angle));
        }
        Point AddVector(Point pt, Point delta) {
            Vector i = GetBasisI();
            Vector j = GetBasisJ();

            pt.X += i.X * delta.X + j.X * delta.Y;
            pt.Y += i.Y * delta.X + j.Y * delta.Y;
            return pt;
        }
        protected Size ImageSize => Preview.ImageInfo.ImageSize;
        void ConstrainPositionWhileDrag() {
            double leftDelta = (Origin.X - Width / 2);
            double rightDelta = ImageSize.Width - (Origin.X + Width / 2);
            if(leftDelta < 0 && rightDelta < 0) {
                Width -= Math.Min(leftDelta, rightDelta);
            } else if(leftDelta < 0) {
                Origin = new Point(Origin.X - leftDelta, Origin.Y);
            } else if(rightDelta < 0) {
                Origin = new Point(Origin.X + rightDelta, Origin.Y);
            }

            double topDelta = (Origin.Y - Height / 2);
            double bottomDelta = ImageSize.Height - (Origin.Y + Height / 2);
            if(topDelta < 0 && bottomDelta < 0) {
                Height -= Math.Min(topDelta, bottomDelta);
            } else if(topDelta < 0) {
                Origin = new Point(Origin.X, Origin.Y - topDelta);
            } else if(bottomDelta < 0) {
                Origin = new Point(Origin.X, Origin.Y + bottomDelta);
            }
        }
        protected Point GetDelta(Point location) {
            Point delta = new Point((location.X - LastDragPoint.X) / Zoom, (location.Y - LastDragPoint.Y) / Zoom);
            delta = ConstrainDelta(delta);
            return delta;
        }
        protected Vector GetConvertDelta(Point location) {
            Point delta = GetDelta(location);
            return new Vector(delta.X, delta.Y);
        }
        protected double GetXLen(Point pt1, Point pt2) {
            Vector v = new Vector(pt2.X - pt1.X, pt2.Y - pt1.Y);
            Vector i = GetBasisI();
            return Math.Abs(v.X * i.X + v.Y * i.Y);
        }
        protected double GetYLen(Point pt1, Point pt2) {
            Vector v = new Vector(pt2.X - pt1.X, pt2.Y - pt1.Y);
            Vector j = GetBasisJ();
            return Math.Abs(v.X * j.X + v.Y * j.Y);
        }
        Point CheckBoundsX(Point newPoint, Point point, ref Vector d, double boundValue, bool shouldBeAfter, out double koefficient) {
            koefficient = 1.0;
            if((shouldBeAfter && newPoint.X < boundValue) || (!shouldBeAfter && newPoint.X > boundValue)) {
                double k = Math.Abs(boundValue - point.X) / Math.Abs(d.X);
                d = new Vector(d.X * k, d.Y * k);
                newPoint = new Point(point.X + d.X, point.Y + d.Y);
                koefficient = k;
            }
            return newPoint;
        }
        Point CheckBoundsY(Point newPoint, Point point, ref Vector d, double boundValue, bool shouldBeAfter, out double koefficient) {
            koefficient = 1.0;
            if((shouldBeAfter && newPoint.Y < boundValue) || (!shouldBeAfter && newPoint.Y > boundValue)) {
                double k = Math.Abs(boundValue - point.Y) / Math.Abs(d.Y);
                d = new Vector(d.X * k, d.Y * k);
                newPoint = new Point(point.X + d.X, point.Y + d.Y);
                koefficient = k;
            }
            return newPoint;
        }
        Point ConstrainDeltaByBounds(Point delta) {
            Point topLeft = CalcTopLeftPoint();
            Point bottomRight = CalcBottomRightPoint();
            Point topRight = CalcTopRightPoint();
            Point bottomLeft = CalcBottomLeftPoint();
            Point middleLeft = CalcMiddleLeftPoint();
            Point middleRight = CalcMiddleRightPoint();
            Point topCenter = CalcTopCenterPoint();
            Point bottomCenter = CalcBottomCenterPoint();

            switch(GridManager.HoverInfo.HitTest) {
                case GridManagerHitTest.TopLeft:
                    break;
                case GridManagerHitTest.TopCenter:
                    break;
                case GridManagerHitTest.TopRight:
                    break;
                case GridManagerHitTest.MiddleLeft:
                    break;
                case GridManagerHitTest.MiddleRight:
                    break;
                case GridManagerHitTest.BottomLeft:
                    break;
                case GridManagerHitTest.BottomRight:
                    break;
                case GridManagerHitTest.BottomCenter:
                    break;
            }
            return delta;
        }
        double ConstrainDeltaScale(Point point, Vector delta) {
            Point newPoint = new Point(point.X + delta.X, point.Y + delta.Y);

            double koefficient = 1.0;
            double k = 1.0;

            newPoint = CheckBoundsX(newPoint, point, ref delta, 0, true, out k);
            koefficient = Math.Min(koefficient, k);
            newPoint = CheckBoundsX(newPoint, point, ref delta, ImageSize.Width, false, out k);
            koefficient = Math.Min(koefficient, k);
            newPoint = CheckBoundsY(newPoint, point, ref delta, 0, true, out k);
            koefficient = Math.Min(koefficient, k);
            newPoint = CheckBoundsY(newPoint, point, ref delta, ImageSize.Height, false, out k);
            koefficient = Math.Min(koefficient, k);

            return koefficient;
        }
        protected Vector ConstrainDelta(Vector delta, Point point, bool useI, bool useJ) {
            Vector i = GetBasisI();
            Vector j = GetBasisJ();

            Vector d = delta;
            if(!useI) {
                d.X = 0;
            }

            if(!useJ) {
                d.Y = 0;
            }

            d = new Vector(i.X * d.X + j.X * d.Y, i.Y * d.X + j.Y * d.Y);
            double koefficient = ConstrainDeltaScale(point, d);
            return new Vector(delta.X * koefficient, delta.Y * koefficient);
        }
        protected Point AddVector(Point pt, Vector delta) {
            Vector i = GetBasisI();
            Vector j = GetBasisJ();

            Vector d = new Vector(i.X * delta.X + j.X * delta.Y, i.Y * delta.X + j.Y * delta.Y);
            return new Point(pt.X + d.X, pt.Y + d.Y);
        }
        void DoSizing(Point location) {
            if(GridManager.HoverInfo.HitTest == GridManagerHitTest.None) {
                return;
            }

            Vector delta = GetConvertDelta(location);
            LastDragPoint = location;

            Point topLeft = CalcTopLeftPoint();
            Point bottomRight = CalcBottomRightPoint();
            Point topRight = CalcTopRightPoint();
            Point bottomLeft = CalcBottomLeftPoint();
            Point middleLeft = CalcMiddleLeftPoint();
            Point middleRight = CalcMiddleRightPoint();
            Point topCenter = CalcTopCenterPoint();
            Point bottomCenter = CalcBottomCenterPoint();

            switch(GridManager.HoverInfo.HitTest) {
                case GridManagerHitTest.TopLeft:
                    delta = ConstrainDelta(delta, topLeft, true, true);
                    delta = ConstrainDelta(delta, topRight, false, true);
                    delta = ConstrainDelta(delta, bottomLeft, true, false);
                    topLeft = AddVector(topLeft, delta);
                    Origin = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);
                    Width = GetXLen(Origin, topLeft) * 2;
                    Height = GetYLen(Origin, topLeft) * 2;
                    break;
                case GridManagerHitTest.TopCenter:
                    delta = ConstrainDelta(delta, topLeft, false, true);
                    delta = ConstrainDelta(delta, topRight, false, true);
                    topCenter = AddVector(topCenter, delta);
                    Origin = new Point((topCenter.X + bottomCenter.X) / 2, (topCenter.Y + bottomCenter.Y) / 2);
                    Height = GetYLen(Origin, topCenter) * 2;
                    break;
                case GridManagerHitTest.TopRight:
                    delta = ConstrainDelta(delta, topRight, true, true);
                    delta = ConstrainDelta(delta, topLeft, false, true);
                    delta = ConstrainDelta(delta, bottomRight, true, false);
                    topRight = AddVector(topRight, delta);
                    Origin = new Point((topRight.X + bottomLeft.X) / 2, (topRight.Y + bottomLeft.Y) / 2);
                    Width = GetXLen(Origin, topRight) * 2;
                    Height = GetYLen(Origin, topRight) * 2;
                    break;
                case GridManagerHitTest.MiddleLeft:
                    delta = ConstrainDelta(delta, topLeft, true, false);
                    delta = ConstrainDelta(delta, bottomLeft, true, false);
                    middleLeft = AddVector(middleLeft, delta);
                    Origin = new Point((middleLeft.X + middleRight.X) / 2, (middleLeft.Y + middleRight.Y) / 2);
                    Width = GetXLen(Origin, middleLeft) * 2;
                    break;
                case GridManagerHitTest.MiddleRight:
                    delta = ConstrainDelta(delta, topRight, true, false);
                    delta = ConstrainDelta(delta, bottomRight, true, false);
                    middleRight = AddVector(middleRight, delta);
                    Origin = new Point((middleLeft.X + middleRight.X) / 2, (middleLeft.Y + middleRight.Y) / 2);
                    Width = GetXLen(Origin, middleRight) * 2;
                    break;
                case GridManagerHitTest.BottomLeft:
                    delta = ConstrainDelta(delta, bottomLeft, true, true);
                    delta = ConstrainDelta(delta, bottomRight, false, true);
                    delta = ConstrainDelta(delta, topLeft, true, false);
                    bottomLeft = AddVector(bottomLeft, delta);
                    Origin = new Point((topRight.X + bottomLeft.X) / 2, (topRight.Y + bottomLeft.Y) / 2);
                    Width = GetXLen(Origin, bottomLeft) * 2;
                    Height = GetYLen(Origin, bottomLeft) * 2;
                    break;
                case GridManagerHitTest.BottomRight:
                    delta = ConstrainDelta(delta, bottomRight, true, true);
                    delta = ConstrainDelta(delta, bottomLeft, false, true);
                    delta = ConstrainDelta(delta, topRight, true, false);
                    bottomRight = AddVector(bottomRight, delta);
                    Origin = new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2);
                    Width = GetXLen(Origin, bottomRight) * 2;
                    Height = GetYLen(Origin, bottomRight) * 2;
                    break;
                case GridManagerHitTest.BottomCenter:
                    delta = ConstrainDelta(delta, bottomRight, false, true);
                    delta = ConstrainDelta(delta, bottomLeft, false, true);
                    bottomCenter = AddVector(bottomCenter, delta);
                    Origin = new Point((bottomCenter.X + topCenter.X) / 2, (bottomCenter.Y + topCenter.Y) / 2);
                    Height = GetYLen(Origin, bottomCenter) * 2;
                    break;
            }
            UpdatePreview();
        }
        Point ConstrainDelta(Point delta) {
            if(GridManager.HoverInfo.HitTest == GridManagerHitTest.TopCenter || GridManager.HoverInfo.HitTest == GridManagerHitTest.BottomCenter) {
                return new Point(0, delta.Y);
            }

            if(GridManager.HoverInfo.HitTest == GridManagerHitTest.MiddleLeft || GridManager.HoverInfo.HitTest == GridManagerHitTest.MiddleRight) {
                return new Point(delta.X, 0);
            }

            return delta;
        }
        protected virtual Point ScreenToLocal(Point pt) {
            Vector v = new Vector(pt.X, pt.Y);
            Vector i = GetBasisI();
            Vector j = GetBasisJ();

            return new Point(v.X * i.X + v.Y * i.Y, v.X * j.X + v.Y * j.Y);
        }
        Point LocalToImage(Point pt) => new Point(Origin.X + pt.X, Origin.Y + pt.Y);
        double CalcMaxBottom() => ImageSize.Height - Origin.Y;
        double CalcMaxRight() => ImageSize.Width - Origin.X;
        double CalcMinTop() => -Origin.Y;
        double CalcMinLeft() => -Origin.X;
        Point CalcBottomRightPoint() => AddVector(Origin, new Point(Width / 2, Height / 2));
        Point CalcTopLeftPoint() => AddVector(Origin, new Point(-Width / 2, -Height / 2));
        Point CalcBottomLeftPoint() => AddVector(Origin, new Point(-Width / 2, Height / 2));
        Point CalcTopRightPoint() => AddVector(Origin, new Point(Width / 2, -Height / 2));
        Point CalcMiddleLeftPoint() => AddVector(Origin, new Point(-Width / 2, 0));
        Point CalcMiddleRightPoint() => AddVector(Origin, new Point(Width / 2, 0));
        Point CalcTopCenterPoint() => AddVector(Origin, new Point(0, -Height / 2));
        Point CalcBottomCenterPoint() => AddVector(Origin, new Point(0, Height / 2));
        protected virtual void UpdatePreview() {
            Point screenPoint = new Point(Screen.X + Screen.Width / 2, Screen.Y + Screen.Height / 2);
            Preview.ImageInfo.Info.ScreenBounds = new Rect(screenPoint.X - Origin.X * Zoom, screenPoint.Y - Origin.Y * Zoom, Preview.ImageInfo.Info.ScreenBounds.Width, Preview.ImageInfo.Info.ScreenBounds.Height);
            Preview.ImageInfo.Info.UseDefaultRotateOrigin = false;
            Preview.ImageInfo.Info.RotateOrigin = ScreenOrigin;
            Preview.ImageInfo.Info.RotateAngle = -Angle;
            Preview.ImageInfo.UseDefaultLayout = false;
            Preview.ImageInfo.SuppressCalcLayout = true;
            Preview.InvalidateVisual();
        }
        Point AddScaleVector(Point origin, Point delta, double scale) {
            Point i = new Point(Math.Cos(Angle), Math.Sin(Angle));
            Point j = new Point(-Math.Sin(Angle), Math.Cos(Angle));
            origin.X += i.X * delta.X * scale + j.X * delta.X * scale;
            origin.Y += i.Y * delta.Y * scale + j.Y * delta.Y * scale;
            return origin;
        }
        public bool OnMouseLeave(Point location) {
            if(GridManager.OnMouseLeave(location)) {
                return true;
            }

            return false;
        }
    }
    public enum CropArea {
        None, TopLeft, TopCenter, TopRight, BottomLeft, BottomCenter, BottomRight, MiddleLeft, MiddleRight, DragArea
    }
}
