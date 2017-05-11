using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PhotoAssistant.Controls.Wpf {
    public class GridManager : DependencyObject {
        public GridManager(IGridManagerOwner owner) {
            Owner = owner;
            Opacity = 1.0;
            CellSize = 40;
            Color = Colors.Gray;
            HighlightColor = Colors.LightGray;
            RowCount = 3;
            ColumnCount = 3;
        }

        public IGridManagerOwner Owner { get; private set; }

        public void Render(DrawingContext drawingContext) {
            Render(drawingContext, Owner.Screen, Owner.Bounds);
        }
        protected void Render(DrawingContext drawingContext, Rect screen, Rect bounds) {
            ApplyTransform(drawingContext, screen);
            DrawGridLines(drawingContext, screen, bounds);
            if(ShowThumbs)
                DrawThumbs(drawingContext, screen, bounds);
            RestoreTransform(drawingContext);
        }

        protected double ThumbTickness {
            get { return 5; }
        }

        protected double ThumbSize {
            get { return 32; }
        }

        public bool IsHitTestVisible { get; set; }

        GridManagerHitInfo hoverInfo = new GridManagerHitInfo();
        public GridManagerHitInfo HoverInfo {
            get { return hoverInfo; }
            set {
                if(HoverInfo.IsEquals(value))
                    return;
                hoverInfo = value;
                OnHoverInfoChanged();
            }
        }

        private void OnHoverInfoChanged() {
            Owner.InvalidateVisual();
        }

        protected GridManagerHitInfo CalcHitInfo(Point location) {
            GridManagerHitInfo res = new GridManagerHitInfo();
            res.HitPoint = location;
            if(res.ContainsSet(GetTopLeftThumbBounds(Owner.Bounds), GridManagerHitTest.TopLeft))
                return res;
            if(res.ContainsSet(GetTopRightThumbBounds(Owner.Bounds), GridManagerHitTest.TopRight))
                return res;
            if(res.ContainsSet(GetBottomLeftThumbBounds(Owner.Bounds), GridManagerHitTest.BottomLeft))
                return res;
            if(res.ContainsSet(GetBottomRightThumbBounds(Owner.Bounds), GridManagerHitTest.BottomRight))
                return res;
            if(res.ContainsSet(GetTopThumbBounds(Owner.Bounds), GridManagerHitTest.TopCenter))
                return res;
            if(res.ContainsSet(GetBottomThumbBounds(Owner.Bounds), GridManagerHitTest.BottomCenter))
                return res;
            if(res.ContainsSet(GetLeftThumbBounds(Owner.Bounds), GridManagerHitTest.MiddleLeft))
                return res;
            if(res.ContainsSet(GetRightThumbBounds(Owner.Bounds), GridManagerHitTest.MiddleRight))
                return res;
            return res;
        }

        public virtual bool OnMouseMove(Point location) {
            if(!IsHitTestVisible)
                return false;
            HoverInfo = CalcHitInfo(location);
            return HoverInfo.HitTest != GridManagerHitTest.None;
        }

        protected internal void UpdateCursor() {
            switch(HoverInfo.HitTest) {
                case GridManagerHitTest.BottomCenter:
                case GridManagerHitTest.TopCenter:
                    Mouse.OverrideCursor = Cursors.SizeNS;
                    break;
                case GridManagerHitTest.MiddleLeft:
                case GridManagerHitTest.MiddleRight:
                    Mouse.OverrideCursor = Cursors.SizeWE;
                    break;
                case GridManagerHitTest.TopLeft:
                case GridManagerHitTest.BottomRight:
                    Mouse.OverrideCursor = Cursors.SizeNWSE;
                    break;
                case GridManagerHitTest.TopRight:
                case GridManagerHitTest.BottomLeft:
                    Mouse.OverrideCursor = Cursors.SizeNESW;
                    break;
                case GridManagerHitTest.None:
                    Mouse.OverrideCursor = Cursors.Arrow;
                    break;
            }
        }

        public virtual bool OnMouseLeave(Point location) {
            if(!IsHitTestVisible)
                return false;
            HoverInfo = CalcHitInfo(location);
            return HoverInfo.HitTest != GridManagerHitTest.None;
        }

        public bool OnMouseDown(MouseButtonState leftButton, Point location) {
            if(leftButton == MouseButtonState.Released)
                return false;
            HoverInfo = CalcHitInfo(location);
            return HoverInfo.HitTest != GridManagerHitTest.None;
        }

        protected ThumbBounds GetTopLeftThumbBounds(Rect bounds) {
            ThumbBounds res = new ThumbBounds();
            res.Bounds1 = new Rect(bounds.X - ThumbTickness, bounds.Y - ThumbTickness, ThumbSize + ThumbTickness, ThumbTickness);
            res.Bounds2 = new Rect(bounds.X - ThumbTickness, bounds.Y, ThumbTickness, ThumbSize);
            return res;
        }

        protected ThumbBounds GetTopRightThumbBounds(Rect bounds) {
            ThumbBounds res = new ThumbBounds();
            res.Bounds1 = new Rect(bounds.Right - ThumbSize, bounds.Y - ThumbTickness, ThumbSize + ThumbTickness, ThumbTickness);
            res.Bounds2 = new Rect(bounds.Right, bounds.Y, ThumbTickness, ThumbSize);
            return res;
        }

        protected ThumbBounds GetBottomLeftThumbBounds(Rect bounds) {
            ThumbBounds res = new ThumbBounds();
            res.Bounds1 = new Rect(bounds.X - ThumbTickness, bounds.Bottom, ThumbSize + ThumbTickness, ThumbTickness);
            res.Bounds2 = new Rect(bounds.X - ThumbTickness, bounds.Bottom - ThumbSize, ThumbTickness, ThumbSize);
            return res;
        }

        protected ThumbBounds GetBottomRightThumbBounds(Rect bounds) {
            ThumbBounds res = new ThumbBounds();
            res.Bounds1 = new Rect(bounds.Right - ThumbSize, bounds.Bottom, ThumbSize + ThumbTickness, ThumbTickness);
            res.Bounds2 = new Rect(bounds.Right, bounds.Bottom - ThumbSize, ThumbTickness, ThumbSize);
            return res;
        }

        protected ThumbBounds GetTopThumbBounds(Rect bounds) {
            ThumbBounds res = new ThumbBounds();
            res.Bounds1 = new Rect((bounds.X + bounds.Right - ThumbSize) / 2, bounds.Y - ThumbTickness, ThumbSize, ThumbTickness);
            res.Bounds2 = Rect.Empty;
            return res;
        }

        protected ThumbBounds GetLeftThumbBounds(Rect bounds) {
            ThumbBounds res = new ThumbBounds();
            res.Bounds1 = new Rect(bounds.X - ThumbTickness, (bounds.Y + bounds.Bottom - ThumbSize) / 2, ThumbTickness, ThumbSize);
            res.Bounds2 = Rect.Empty;
            return res;
        }

        protected ThumbBounds GetRightThumbBounds(Rect bounds) {
            ThumbBounds res = new ThumbBounds();
            res.Bounds1 = new Rect(bounds.Right, (bounds.Y + bounds.Bottom - ThumbSize) / 2, ThumbTickness, ThumbSize);
            res.Bounds2 = Rect.Empty;
            return res;
        }

        protected ThumbBounds GetBottomThumbBounds(Rect bounds) {
            ThumbBounds res = new ThumbBounds();
            res.Bounds1 = new Rect((bounds.X + bounds.Right - ThumbSize) / 2, bounds.Bottom, ThumbSize, ThumbTickness);
            res.Bounds2 = Rect.Empty;
            return res;
        }

        protected SolidColorBrush GetThumbBrush(GridManagerHitTest hitTest) {
            if(HoverInfo.HitTest == hitTest)
                return new SolidColorBrush(Color.FromArgb((byte)(Opacity * 255), HighlightColor.R, HighlightColor.G, HighlightColor.B));
            return new SolidColorBrush(Color.FromArgb((byte)(Opacity * 255), Color.R, Color.G, Color.B));
        }

        private void DrawThumbs(DrawingContext drawingContext, Rect screen, Rect bounds) {

            DrawThumb(drawingContext, GetThumbBrush(GridManagerHitTest.TopLeft), GetTopLeftThumbBounds(bounds));
            DrawThumb(drawingContext, GetThumbBrush(GridManagerHitTest.TopRight), GetTopRightThumbBounds(bounds));
            DrawThumb(drawingContext, GetThumbBrush(GridManagerHitTest.BottomLeft), GetBottomLeftThumbBounds(bounds));
            DrawThumb(drawingContext, GetThumbBrush(GridManagerHitTest.BottomRight), GetBottomRightThumbBounds(bounds));
            DrawThumb(drawingContext, GetThumbBrush(GridManagerHitTest.TopCenter), GetTopThumbBounds(bounds));
            DrawThumb(drawingContext, GetThumbBrush(GridManagerHitTest.BottomCenter), GetBottomThumbBounds(bounds));
            DrawThumb(drawingContext, GetThumbBrush(GridManagerHitTest.MiddleLeft), GetLeftThumbBounds(bounds));
            DrawThumb(drawingContext, GetThumbBrush(GridManagerHitTest.MiddleRight), GetRightThumbBounds(bounds));
        }

        private void DrawThumb(DrawingContext drawingContext, Brush brush, ThumbBounds thumbBounds) {
            drawingContext.DrawRectangle(brush, null, thumbBounds.Bounds1);
            if(!thumbBounds.Bounds2.IsEmpty)
                drawingContext.DrawRectangle(brush, null, thumbBounds.Bounds2);
        }

        private void DrawGridLines(DrawingContext drawingContext, Rect screen, Rect bounds) {
            Pen pen = new Pen(new SolidColorBrush(Color.FromArgb((byte)(Opacity * 255), Color.R, Color.G, Color.B)), 1.0);
            if(Mode == GridMode.UseSize) {
                Point center = new Point((screen.X + screen.Right) / 2, (screen.Y + screen.Bottom) / 2);
                for(double x = 0; center.X - x >= bounds.X; x += CellSize) {
                    drawingContext.DrawLine(pen, new Point(center.X - x, bounds.Y), new Point(center.X - x, bounds.Bottom));
                }
                for(double x = CellSize; center.X + x <= bounds.Right; x += CellSize) {
                    drawingContext.DrawLine(pen, new Point(center.X + x, bounds.Y), new Point(center.X + x, bounds.Bottom));
                }
                for(double y = 0; center.Y - y >= bounds.Top; y += CellSize) {
                    drawingContext.DrawLine(pen, new Point(bounds.X, center.Y - y), new Point(bounds.Right, center.Y - y));
                }
                for(double y = 0; center.Y + y <= bounds.Bottom; y += CellSize) {
                    drawingContext.DrawLine(pen, new Point(bounds.X, center.Y + y), new Point(bounds.Right, center.Y + y));
                }
            }
            else if(Mode == GridMode.UseCount) {
                double width = bounds.Width / ColumnCount;
                double height = bounds.Height / RowCount;

                for(int i = 0; i < ColumnCount + 1; i++)
                    drawingContext.DrawLine(pen, new Point(bounds.X + width * i, bounds.Y), new Point(bounds.X + width * i, bounds.Bottom));
                for(int i = 0; i < RowCount + 1; i++)
                    drawingContext.DrawLine(pen, new Point(bounds.X, bounds.Y + i * height), new Point(bounds.Right, bounds.Y + i * height));
            }
        }

        private void RestoreTransform(DrawingContext drawingContext) {
            drawingContext.Pop();
        }

        private void ApplyTransform(DrawingContext drawingContext, Rect screen) {
            TransformGroup group = new TransformGroup();
            group.Children.Add(new RotateTransform(Angle, (screen.X + screen.Right) / 2, (screen.Y + screen.Bottom) / 2));
            drawingContext.PushTransform(group);
        }

        public GridMode Mode { get; set; }
        public double Opacity { get; set; }
        public Color Color { get; set; }
        public Color HighlightColor { get; set; }
        public double CellSize { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public double Angle { get; set; }
        public bool ShowThumbs { get; set; }
    }

    public enum GridMode { UseSize, UseCount }
    public class ThumbBounds {
        public Rect Bounds1 { get; set; }
        public Rect Bounds2 { get; set; }
        public bool Contains(Point pt) { return Bounds1.Contains(pt) || Bounds2.Contains(pt); }
    }

    public interface IGridManagerOwner {
        Rect Bounds { get; }
        Rect Screen { get; }
        void InvalidateVisual();
    }

    public enum GridManagerHitTest { None, TopLeft, TopCenter, TopRight, MiddleLeft, MiddleRight, BottomLeft, BottomCenter, BottomRight }
    public class GridManagerHitInfo {
        public Point HitPoint { get; set; }
        public GridManagerHitTest HitTest { get; set; }

        public bool IsEquals(GridManagerHitInfo info) {
            return HitTest == info.HitTest;
        }

        public bool ContainsSet(ThumbBounds bounds, GridManagerHitTest hitTest) {
            if(bounds.Contains(HitPoint)) {
                HitTest = hitTest;
                return true;
            }
            return false;
        }
    }
}
