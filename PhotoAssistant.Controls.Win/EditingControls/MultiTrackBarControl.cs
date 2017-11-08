using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace PhotoAssistant.Controls.Win.EditingControls {
    [UserRepositoryItem("Register")]
    public class RepositoryItemMultiTrackBarControl : RepositoryItemTrackBar {
        static readonly object drawTrackArea = new object();
        internal const string EditorName = "MultiTrackBarControl";
        public static void Register() {
            if(EditorRegistrationInfo.Default.Editors.Contains(EditorName)) {
                return;
            }

            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(MultiTrackBarControl), typeof(RepositoryItemMultiTrackBarControl), typeof(MultiTrackBarControlViewInfo), new MultiTrackBarPainter(), true, EditImageIndexes.ProgressBarControl));
        }
        static RepositoryItemMultiTrackBarControl() => Register();
        public override string EditorTypeName => EditorName;
        public RepositoryItemMultiTrackBarControl() => AutoSize = true;
        public event TrackBarPaintEventHandler DrawTrackLine {
            add {
                Events.AddHandler(drawTrackArea, value);
            }
            remove {
                Events.RemoveHandler(drawTrackArea, value);
            }
        }
        protected internal void RaiseDrawTrackLine(TrackBarPaintEventArgs e) {
            TrackBarPaintEventHandler handler = Events[drawTrackArea] as TrackBarPaintEventHandler;
            if(handler != null) {
                handler(this, e);
            }
        }
        protected internal virtual MultiTrackBarValues ConvertValueCore(object editValue) {
            if(editValue is MultiTrackBarValues) {
                return (MultiTrackBarValues)editValue;
            }

            return null;
        }
        protected internal virtual object CheckValueCore(MultiTrackBarValues value) => value;
        int trackLineHeight = -1;
        [DefaultValue(-1)]
        public int TrackLineHeight {
            get => trackLineHeight;
            set {
                if(TrackLineHeight == value) {
                    return;
                }

                trackLineHeight = value;
                OnPropertiesChanged();
            }
        }
        int thumbHeight;
        [DefaultValue(0)]
        public int ThumbHeight {
            get => thumbHeight;
            set {
                if(ThumbHeight == value) {
                    return;
                }

                thumbHeight = value;
                OnPropertiesChanged();
            }
        }
        bool minSize = false;
        [DefaultValue(false)]
        public bool MinSize {
            get => minSize;
            set {
                if(MinSize == value) {
                    return;
                }

                minSize = value;
                OnMinSizeChanged();
            }
        }
        protected virtual void OnMinSizeChanged() {
            if(OwnerEdit != null) {
                OwnerEdit.Height = OwnerEdit.GetPreferredSize(new Size(int.MaxValue, int.MaxValue)).Height;
            }

            OnPropertiesChanged();
        }
        public override void Assign(RepositoryItem item) {
            base.Assign(item);
            RepositoryItemMultiTrackBarControl mitem = item as RepositoryItemMultiTrackBarControl;
            trackLineHeight = mitem.TrackLineHeight;
        }
    }
    public delegate void TrackBarPaintEventHandler(object sender, TrackBarPaintEventArgs e);
    public class TrackBarPaintEventArgs : EventArgs {
        public MultiTrackBarControlViewInfo ViewInfo {
            get; internal set;
        }
        public TrackBarObjectPainter Painter {
            get; internal set;
        }
        public GraphicsCache Cache {
            get; internal set;
        }
        public bool Habdled {
            get; set;
        }
    }
    public class MultiTrackBarControlViewInfo : TrackBarViewInfo, IMultiTrackBarValuesOwner {
        public MultiTrackBarControlViewInfo(RepositoryItem item) : base(item) {
        }
        public override int ValueFromPoint(Point p) => base.ValueFromPoint(p);
        public override EditHitInfo CalcHitInfo(Point p) {
            MultiTrackBarHitInfo mhi = new MultiTrackBarHitInfo();
            int index = 0;
            foreach(Rectangle thumb in ThumbsBounds) {
                if(thumb.Contains(p)) {
                    mhi.SetHitTestCore(EditHitTest.Button);
                    mhi.SetHitObjectCore(EditHitTest.Button);
                    mhi.ThumbIndex = index;
                    break;
                }
                index++;
            }
            return mhi;
        }
        protected override void UpdateFromEditor() {
            base.UpdateFromEditor();
            AllowDrawFocusRect = Item.AllowFocused;
        }
        public override TrackBarObjectPainter GetTrackPainter() {
            if(LookAndFeel.ActiveStyle == ActiveLookAndFeelStyle.Skin) {
                return new SkinMultiTrackBarObjectPainter(LookAndFeel.ActiveLookAndFeel);
            }

            return new MultiTrackBarObjectPainter();
        }
        public MultiTrackBarInfoCalculator MultiTrackCalculator => (MultiTrackBarInfoCalculator)TrackCalculator;
        public MultiTrackBarValues Values {
            get {
                if(EditValue == null) {
                    return new MultiTrackBarValues(this);
                }

                return (MultiTrackBarValues)EditValue;
            }
        }
        public Rectangle[] ThumbsBounds {
            get; private set;
        }
        public Point[] ThumbsPosition {
            get; private set;
        }
        protected MultiTrackBarControl MultiTrackBar => (MultiTrackBarControl)OwnerEdit;
        protected override void CalcThumbPos() {
            base.CalcThumbPos();
            ThumbsPosition = new Point[Values.Count];
            for(int i = 0; i < Values.Count; i++) {
                ThumbsPosition[i] = CalcThumbPosCore(Values[i]);
            }
            ThumbsBounds = MultiTrackCalculator.GetThumbsBounds();
        }
        public virtual Point[] GetArrowThumbRegion(Point thumbPos) {
            TrackBarObjectPainter pt = TrackPainter;
            int[,] offsetP1 = { { 0, 11 }, { -pt.GetThumbBestWidth(this) / 2, 6 }, { -pt.GetThumbBestWidth(this) / 2, -9 }, { pt.GetThumbBestWidth(this) / 2, -9 }, { pt.GetThumbBestWidth(this) / 2, 6 }, { 0, 11 } };
            Point[] polygon = new Point[6];
            TransformPoints(offsetP1, polygon, thumbPos);
            return polygon;
        }
        public virtual Point[] GetRectThumbRegion(Point thumbPos) {
            TrackBarObjectPainter pt = TrackPainter;
            int top = -pt.GetThumbBestHeight(this) / 2;
            int left = -pt.GetThumbBestWidth(this) / 2;
            int width = pt.GetThumbBestWidth(this);
            int height = pt.GetThumbBestHeight(this);
            int[,] offsetP1 = { { left, top + height }, { left, top }, { left + width, top }, { left + width, top + height }, { left, top + height } };
            Point[] polygon = new Point[5];
            TransformPoints(offsetP1, polygon, thumbPos);
            return polygon;
        }
        public override int ThumbCriticalHeight => TrackBarHelper.ClientHeight;
        public override int TrackLineCriticalHeight => TrackBarHelper.ClientHeight;
        internal void SetThumbPosCore(Point pt) => ThumbPos = pt;
        protected internal virtual Rectangle GetThumbContentBounds(int index) => MultiTrackCalculator.GetThumbContentBounds(index);
        internal int GetNearestThumb(Point pt) {
            int minIndex = -1;
            int minDistance = int.MaxValue;
            int index = 0;
            foreach(Point thumb in ThumbsPosition) {
                int distance = Item.Orientation == Orientation.Horizontal ? pt.X - thumb.X : pt.Y - thumb.Y;
                distance = Math.Abs(distance);
                if(minDistance > distance) {
                    minIndex = index;
                    minDistance = distance;
                }
                index++;
            }
            return minIndex;
        }
        protected int StateThumbIndex {
            get; set;
        }
        protected override bool UpdateObjectState() {
            MultiTrackBarHitInfo hitInfo = (MultiTrackBarHitInfo)CalcHitInfo(MousePosition);
            ObjectState prevState = State;
            State = CalcObjectState();
            int prevThumbIndex = StateThumbIndex;
            StateThumbIndex = hitInfo.ThumbIndex;
            return prevState != State;
        }
        void IMultiTrackBarValuesOwner.OnValuesChanged() {
        }
        protected override int PreferredDimension {
            get {
                if(((RepositoryItemMultiTrackBarControl)Item).MinSize) {
                    return Math.Max(TrackCalculator.RealTrackLineHeight, TrackCalculator.ThumbHeight);
                }
                return TrackCalculator.ThumbHeight + TrackCalculator.SummaryRealDistances + 2 * TrackCalculator.TickHeight;
            }
        }
    }
    public class MultiTrackBarInfoCalculator : TrackBarInfoCalculator {
        public MultiTrackBarInfoCalculator(TrackBarViewInfo viewInfo, TrackBarObjectPainter painter) : base(viewInfo, painter) {
        }
        protected override int GetTrackLineRectInflateWidth() {
            if(((RepositoryItemMultiTrackBarControl)ViewInfo.Item).MinSize) {
                return 0;
            }

            return base.GetTrackLineRectInflateWidth();
        }
        protected override int PointsRectOffsetX {
            get {
                if(((RepositoryItemMultiTrackBarControl)ViewInfo.Item).MinSize) {
                    return 0;
                }

                return base.PointsRectOffsetX;
            }
        }
        public override int DistanceFromThumbToTicks => 0;
        public override int DistanceFromTicksToBottom => 0;
        public override int DistanceFromTopToThumb => 0;
        public override int DistanceFromLeftToThumb => 0;
        public override int TickHeight {
            get {
                if(ViewInfo.Item.TickStyle == TickStyle.None) {
                    return 0;
                }

                return base.TickHeight;
            }
        }
        public override int RealTrackLineHeight {
            get {
                if(((RepositoryItemMultiTrackBarControl)ViewInfo.Item).TrackLineHeight > 0) {
                    return ((RepositoryItemMultiTrackBarControl)ViewInfo.Item).TrackLineHeight;
                }

                if(((RepositoryItemMultiTrackBarControl)ViewInfo.Item).MinSize) {
                    return RealThumbHeight;
                }

                return base.RealTrackLineHeight;
            }
        }
        protected override Rectangle CalcTrackLineRect() {
            Rectangle rect = base.CalcTrackLineRect();
            return rect;
        }
        public MultiTrackBarControlViewInfo MultiViewInfo => (MultiTrackBarControlViewInfo)ViewInfo;
        protected virtual Rectangle GetThumbBoundsCore(Point thumbPos) {
            Point[] p = MultiViewInfo.GetRectThumbRegion(thumbPos);
            Rectangle r = Rectangle.FromLTRB(p[1].X, p[1].Y, p[3].X, p[3].Y);
            if(r.Height < 0) {
                r.Y += r.Height; r.Height = Math.Abs(r.Height);
            }
            if(r.Width < 0) {
                r.X += r.Width; r.Width = Math.Abs(r.Width);
            }
            return r;
        }
        public Rectangle[] GetThumbsBounds() {
            Rectangle[] res = new Rectangle[MultiViewInfo.Values.Count];
            for(int i = 0; i < MultiViewInfo.ThumbsPosition.Length; i++) {
                res[i] = GetThumbBoundsCore(MultiViewInfo.ThumbsPosition[i]);
            }
            return res;
        }
        public virtual Rectangle GetThumbContentBounds(int index) => GetThumbBoundsCore(MultiViewInfo.ThumbsPosition[index]);
    }
    public class SkinMultiTrackBarInfoCalculator : MultiTrackBarInfoCalculator {
        public SkinMultiTrackBarInfoCalculator(TrackBarViewInfo viewInfo, SkinTrackBarObjectPainter painter) : base(viewInfo, painter) {
        }
        public virtual Rectangle GetSkinThumbBounds(Point thumbPos) {
            Point pt = GetSkinThumbElementOffset();
            Rectangle rect = Rectangle.Empty;
            if(ViewInfo.TickStyle == TickStyle.TopLeft) {
                rect = new Rectangle(new Point(thumbPos.X - pt.X, GetThumbY()), GetSkinThumbElementSize());
            } else {
                rect = new Rectangle(new Point(thumbPos.X - pt.X, GetThumbY()), GetSkinThumbElementSize());
            }

            return ViewInfo.TrackBarHelper.Rotate(rect);
        }
        protected override int ThumbUpperPartHeight => ThumbHeight / 2;
        protected override int ThumbLowerPartHeight => ThumbHeight - ThumbUpperPartHeight;
        public override int ThumbHeight {
            get {
                if(((RepositoryItemMultiTrackBarControl)ViewInfo.Item).ThumbHeight > 0) {
                    return ((RepositoryItemMultiTrackBarControl)ViewInfo.Item).ThumbHeight;
                }

                return GetSkinThumbElementOriginSize().Height;
            }
        }
        public override SkinElementInfo GetThumbElementInfo() {
            if(ViewInfo.TickStyle == TickStyle.Both) {
                return new SkinElementInfo(EditorsSkins.GetSkin(ViewInfo.LookAndFeel.ActiveLookAndFeel)[EditorsSkins.SkinTrackBarThumbBoth], Rectangle.Empty);
            }

            return new SkinElementInfo(EditorsSkins.GetSkin(ViewInfo.LookAndFeel.ActiveLookAndFeel)[EditorsSkins.SkinTrackBarThumb], Rectangle.Empty);
        }
    }
    public class MultiTrackBarObjectPainter : TrackBarObjectPainter {
        public override void DrawObject(ObjectInfoArgs e) {
            TrackBarObjectInfoArgs tbe = e as TrackBarObjectInfoArgs;
            RepositoryItemTrackBar ri = tbe.ViewInfo.Item;
            DrawBackground(tbe);
            DrawTrackLine(tbe);
            if(AllowTick(tbe.ViewInfo)) {
                DrawPoints(tbe);
            }

            DrawThumbs(tbe);
            if(ri.ShowLabels) {
                DrawLabels(tbe);
            }
        }
        public override void DrawTrackLine(TrackBarObjectInfoArgs e) {
            MultiTrackBarControlViewInfo viewInfo = (MultiTrackBarControlViewInfo)e.ViewInfo;
            TrackBarPaintEventArgs pe = new TrackBarPaintEventArgs() { ViewInfo = viewInfo, Painter = this, Cache = e.Cache };
            ((RepositoryItemMultiTrackBarControl)viewInfo.Item).RaiseDrawTrackLine(pe);
            if(!pe.Habdled) {
                base.DrawTrackLine(e);
            }
        }
        protected override TrackBarInfoCalculator GetCalculator(TrackBarViewInfo viewInfo) => new MultiTrackBarInfoCalculator((MultiTrackBarControlViewInfo)viewInfo, this);
        protected virtual void DrawThumbs(TrackBarObjectInfoArgs tbe) {
            MultiTrackBarControlViewInfo viewInfo = (MultiTrackBarControlViewInfo)tbe.ViewInfo;
            foreach(Point pt in viewInfo.ThumbsPosition) {
                viewInfo.SetThumbPosCore(pt);
                DrawThumb(tbe);
            }
        }
    }
    public class SkinMultiTrackBarObjectPainter : SkinTrackBarObjectPainter {
        public SkinMultiTrackBarObjectPainter(ISkinProvider provider) : base(provider) {
        }
        public override void DrawObject(ObjectInfoArgs e) {
            TrackBarObjectInfoArgs tbe = e as TrackBarObjectInfoArgs;
            RepositoryItemTrackBar ri = tbe.ViewInfo.Item;
            DrawBackground(tbe);
            DrawTrackLine(tbe);
            if(AllowTick(tbe.ViewInfo)) {
                DrawPoints(tbe);
            }

            DrawThumbs(tbe);
            if(ri.ShowLabels) {
                DrawLabels(tbe);
            }
        }
        protected override TrackBarInfoCalculator GetCalculator(TrackBarViewInfo viewInfo) => new SkinMultiTrackBarInfoCalculator(viewInfo, this);
        protected virtual void UpdateSkinThumbState(SkinElementInfo info, TrackBarObjectInfoArgs e, int index) {
            info.State = e.State;
            info.ImageIndex = -1;
        }
        public override void DrawTrackLine(TrackBarObjectInfoArgs e) {
            MultiTrackBarControlViewInfo viewInfo = (MultiTrackBarControlViewInfo)e.ViewInfo;
            TrackBarPaintEventArgs pe = new TrackBarPaintEventArgs() { ViewInfo = viewInfo, Painter = this, Cache = e.Cache };
            ((RepositoryItemMultiTrackBarControl)viewInfo.Item).RaiseDrawTrackLine(pe);
            if(!pe.Habdled) {
                base.DrawTrackLine(e);
            }
        }
        protected virtual void DrawSkinThumb(TrackBarObjectInfoArgs e, int index) {
            MultiTrackBarControlViewInfo viewInfo = (MultiTrackBarControlViewInfo)e.ViewInfo;
            SkinElementInfo info = new SkinElementInfo(GetTrackThumb(e.ViewInfo), GetVerticalThumbRectangle(e.ViewInfo, viewInfo.GetThumbContentBounds(index)));
            UpdateSkinThumbState(info, e, index);
            new RotateObjectPaintHelper().DrawRotated(e.Cache, info, SkinElementPainter.Default, GetRotateAngle(e.ViewInfo), true);
        }
        protected virtual void DrawThumbs(TrackBarObjectInfoArgs tbe) {
            MultiTrackBarControlViewInfo viewInfo = (MultiTrackBarControlViewInfo)tbe.ViewInfo;
            for(int i = 0; i < viewInfo.ThumbsPosition.Length; i++) {
                DrawSkinThumb(tbe, i);
            }
        }
    }
    public class MultiTrackBarHitInfo : EditHitInfo {
        public MultiTrackBarHitInfo() => ThumbIndex = -1;
        public int ThumbIndex {
            get; set;
        }
        internal void SetHitObjectCore(EditHitTest button) => SetHitObject(button);
        internal void SetHitTestCore(EditHitTest button) => SetHitTest(button);
    }
    public class MultiTrackBarPainter : TrackBarPainter {
    }
    [ToolboxItem(true)]
    public class MultiTrackBarControl : TrackBarControl, IMultiTrackBarValuesOwner {
        static MultiTrackBarControl() => RepositoryItemMultiTrackBarControl.Register();
        public MultiTrackBarHitInfo CalcHitInfo(Point location) => (MultiTrackBarHitInfo)ViewInfo.CalcHitInfo(location);
        public override string EditorTypeName => RepositoryItemMultiTrackBarControl.EditorName;
        public new RepositoryItemMultiTrackBarControl Properties => base.Properties as RepositoryItemMultiTrackBarControl;
        protected virtual MultiTrackBarValues CreateValues(int count) {
            MultiTrackBarValues res = new MultiTrackBarValues(this);
            int delta = (Properties.Maximum - Properties.Minimum) / count;
            res.BeginUpdate();
            for(int i = 0; i < count; i++) {
                res.Add(Properties.Minimum + delta);
            }
            res.CancelUpdate();
            return res;
        }
        void IMultiTrackBarValuesOwner.OnValuesChanged() {
            CorrectValues();
            OnEditValueChanged();
        }
        protected override void OnKeyDown(KeyEventArgs e) {
        }
        protected virtual void CorrectValues() {
            Values.BeginUpdate();
            try {
                for(int index = 0; index < Values.Count; index++) {
                    int minValue = index == 0 ? Properties.Minimum : Values[index - 1];
                    int maxValue = index == Values.Count - 1 ? Properties.Maximum : Values[index + 1];

                    minValue = Math.Min(Properties.Maximum, Math.Max(Properties.Minimum, minValue));
                    maxValue = Math.Min(Properties.Maximum, Math.Max(Properties.Minimum, maxValue));

                    Values[index] = Math.Max(minValue, Math.Min(maxValue, Values[index]));
                }
            } finally {
                Values.CancelUpdate();
            }
        }
        protected override object ConvertCheckValue(object val) {
            if((val == null) || (val is DBNull)) {
                return val;
            }
            return Properties.ConvertValueCore(val);
        }
        protected MultiTrackBarControlViewInfo MultiViewInfo => (MultiTrackBarControlViewInfo)ViewInfo;
        protected override void UpdateValueFromPoint(Point pt) {
            int thumbIndex = MultiViewInfo.GetNearestThumb(pt);
            if(thumbIndex == -1) {
                return;
            }

            Values[thumbIndex] = MultiViewInfo.ValueFromPoint(pt);
        }
        protected override void OnProcessThumb(Point p) {
            MultiTrackBarHitInfo info = (MultiTrackBarHitInfo)ViewInfo.PressedInfo;
            if(info.ThumbIndex == -1) {
                return;
            }

            int value = ViewInfo.ValueFromPoint(ViewInfo.ControlToClient(p));

            int minValue = info.ThumbIndex == 0 ? Properties.Minimum : Values[info.ThumbIndex - 1];
            int maxValue = info.ThumbIndex == Values.Count - 1 ? Properties.Maximum : Values[info.ThumbIndex + 1];

            Values[info.ThumbIndex] = Math.Max(minValue, Math.Min(value, maxValue));
            ShowValue();
        }
        public MultiTrackBarValues Values {
            get {
                MultiTrackBarValues values = Properties.ConvertValueCore(EditValue);
                if(values == null) {
                    values = CreateValues(1);
                    fEditValue = values;
                }
                return values;
            }
            set {
                EditValue = Properties.CheckValueCore(value);
                if(IsHandleCreated) {
                    Update();
                }
            }
        }
    }
    public interface IMultiTrackBarValuesOwner {
        void OnValuesChanged();
    }
    public class MultiTrackBarValues : Collection<int> {
        public MultiTrackBarValues(IMultiTrackBarValuesOwner owner) => Owner = owner;
        public IMultiTrackBarValuesOwner Owner {
            get; private set;
        }
        protected virtual void OnValuesChanged() {
            if(UpdateCount > 0) {
                return;
            }

            if(Owner != null) {
                Owner.OnValuesChanged();
            }
        }
        protected override void InsertItem(int index, int item) {
            base.InsertItem(index, item);
            OnValuesChanged();
        }
        protected override void RemoveItem(int index) {
            base.RemoveItem(index);
            OnValuesChanged();
        }
        protected override void SetItem(int index, int item) {
            base.SetItem(index, item);
            OnValuesChanged();
        }
        protected override void ClearItems() {
            base.ClearItems();
            OnValuesChanged();
        }
        public int UpdateCount {
            get; private set;
        }
        public void BeginUpdate() => UpdateCount++;
        public void EndUpdate() {
            if(UpdateCount == 0) {
                return;
            }

            UpdateCount--;
            if(UpdateCount == 0) {
                Owner.OnValuesChanged();
            }
        }
        public void CancelUpdate() {
            if(UpdateCount == 0) {
                return;
            }

            UpdateCount--;
        }
    }
}
