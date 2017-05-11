//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace PhotoAssistant.Core {
//    public static class ImageUtils {
//        public static void RotateImage(Image img, int degrees) {
//            RotateFlipType rf = RotateFlipType.RotateNoneFlipNone;
//            if(degrees == 90 || degrees == -270)
//                rf = RotateFlipType.Rotate90FlipNone;
//            if(degrees == -90 || degrees == 270)
//                rf = RotateFlipType.Rotate270FlipNone;
//            if(degrees == 180 || degrees == -180)
//                rf = RotateFlipType.Rotate180FlipNone;
//            img.RotateFlip(rf);
//        }
//    }

//    public class RectangleRotateAnimationInfo : FloatAnimationInfo {
//        public RectangleRotateAnimationInfo(ISupportXtraAnimationEx obj, object animationId, Rectangle startRect, Rectangle boundRect, float angle, int ms)
//            : base(obj, animationId, ms, 0.0f, 1.0f, true) {
//            StartRect = startRect;
//            BoundRect = boundRect;
//            Angle = (float)(angle / 180.0f * Math.PI);
//            UpperLeft = GetVector(GetOrigin(BoundRect), GetUpperLeft(StartRect));
//            UpperRight = GetVector(GetOrigin(BoundRect), GetUpperRight(StartRect));
//            LowerLeft = GetVector(GetOrigin(BoundRect), GetLowerLeft(StartRect));
//            LowerRight = GetVector(GetOrigin(BoundRect), GetLowerRight(StartRect));
//        }
//        public Control OwnerControl { get; set; }
//        protected float GetLength(PointF pt1, PointF pt2) {
//            return (float)Math.Sqrt((pt2.X - pt1.X) * (pt2.X - pt1.X) + (pt2.Y - pt1.Y) * (pt2.Y - pt1.Y));
//        }
//        protected PointF GetOrigin(Rectangle rect) {
//            return new PointF(rect.X + rect.Width * 0.5f, rect.Y + rect.Height * 0.5f);
//        }
//        protected override void FrameStepCore(float k) {
//            base.FrameStepCore(k);
//            ActualAngle = Angle * Value;
//            ActualUpperLeft = RotateVector(UpperLeft, ActualAngle);
//            ActualUpperRight = RotateVector(UpperRight, ActualAngle);
//            ActualLowerLeft = RotateVector(LowerLeft, ActualAngle);
//            ActualLowerRight = RotateVector(LowerRight, ActualAngle);

//            PointF v1 = ScaleVector(ActualUpperLeft, 100.0f);
//            PointF v2 = ScaleVector(ActualUpperRight, 100.0f);

//            PointF origin = GetOrigin(BoundRect);
//            PointF pt1 = new PointF(origin.X + v1.X, origin.Y + v1.Y);
//            PointF pt2 = new PointF(origin.X + v2.X, origin.Y + v2.Y);
//            PointF ul = Crossing(origin, pt1, BoundRect);
//            PointF ur = Crossing(origin, pt2, BoundRect);

//            float len1 = ul.X == float.PositiveInfinity ? float.PositiveInfinity : GetLength(origin, ul);
//            float len2 = ur.X == float.PositiveInfinity ? float.PositiveInfinity : GetLength(origin, ur);
//            float olen = GetLength(origin, GetUpperLeft(StartRect));

//            float len = Math.Min(len1, len2);
//            if(len != float.PositiveInfinity) {
//                float koeff = len / olen;
//                ActualUpperLeft = ScaleVector(ActualUpperLeft, koeff);
//                ActualUpperRight = ScaleVector(ActualUpperRight, koeff);
//                ActualLowerLeft = ScaleVector(ActualLowerLeft, koeff);
//                ActualLowerRight = ScaleVector(ActualLowerRight, koeff);
//            }
//            ActualUpperLeft = AddVector(ActualUpperLeft, origin);
//            ActualUpperRight = AddVector(ActualUpperRight, origin);
//            ActualLowerLeft = AddVector(ActualLowerLeft, origin);
//            ActualLowerRight = AddVector(ActualLowerRight, origin);
//        }

//        public object Tag { get; set; }

//        private PointF AddVector(PointF pt1, PointF pt2) {
//            return new PointF(pt1.X + pt2.X, pt1.Y + pt2.Y);
//        }

//        private PointF ScaleVector(PointF v, float k) {
//            return new PointF(v.X * k, v.Y * k);
//        }

//        public PointF Crossing(PointF p1, PointF p2, Rectangle rect) {
//            PointF pt = Crossing(p1, p2, GetUpperLeft(BoundRect), GetUpperRight(BoundRect));
//            if(pt.X != float.PositiveInfinity)
//                return pt;
//            pt = Crossing(p1, p2, GetUpperRight(BoundRect), GetLowerRight(BoundRect));
//            if(pt.X != float.PositiveInfinity)
//                return pt;
//            pt = Crossing(p1, p2, GetLowerLeft(BoundRect), GetLowerRight(BoundRect));
//            if(pt.X != float.PositiveInfinity)
//                return pt;
//            return Crossing(p1, p2, GetUpperLeft(BoundRect), GetLowerLeft(BoundRect));
//        }
//        public PointF Crossing(PointF p1, PointF p2, PointF p3, PointF p4) {
//            if(p3.X == p4.X) {
//                double y = p1.Y + ((p2.Y - p1.Y) * (p3.X - p1.X)) / (p2.X - p1.X);
//                if(y > Math.Max(p3.Y, p4.Y) || y < Math.Min(p3.Y, p4.Y) || y > Math.Max(p1.Y, p2.Y) || y < Math.Min(p1.Y, p2.Y))
//                    return new PointF(float.PositiveInfinity, float.PositiveInfinity);
//                else
//                    return new PointF(p3.X, (float)y);
//            } else {
//                double x = p1.X + ((p2.X - p1.X) * (p3.Y - p1.Y)) / (p2.Y - p1.Y);
//                if(x > Math.Max(p3.X, p4.X) || x < Math.Min(p3.X, p4.X) || x > Math.Max(p1.X, p2.X) || x < Math.Min(p1.X, p2.X))
//                    return new PointF(float.PositiveInfinity, float.PositiveInfinity);
//                else
//                    return new PointF((float)x, p3.Y);
//            }
//        }
//        protected PointF GetVector(PointF origin, PointF pt) { return new PointF(pt.X - origin.X, pt.Y - origin.Y); }
//        protected PointF GetUpperLeft(Rectangle rect) { return new PointF(rect.X, rect.Y); }
//        protected PointF GetUpperRight(Rectangle rect) { return new PointF(rect.Right, rect.Y); }
//        protected PointF GetLowerLeft(Rectangle rect) { return new PointF(rect.X, rect.Bottom); }
//        protected PointF GetLowerRight(Rectangle rect) { return new PointF(rect.Right, rect.Bottom); }
//        protected PointF UpperLeft { get; set; }
//        protected PointF UpperRight { get; set; }
//        protected PointF LowerLeft { get; set; }
//        protected PointF LowerRight { get; set; }
//        public PointF ActualUpperLeft { get; private set; }
//        public PointF ActualUpperRight { get; private set; }
//        public PointF ActualLowerLeft { get; private set; }
//        public PointF ActualLowerRight { get; private set; }
//        PointF RotateVector(PointF vector, float angle) {
//            return new PointF((float)(vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle)), (float)(vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle)));
//        }

//        public float Angle { get; private set; }
//        public float ActualAngle { get; private set; }
//        public Rectangle StartRect { get; private set; }
//        public Rectangle BoundRect { get; private set; }
//    }
//}
