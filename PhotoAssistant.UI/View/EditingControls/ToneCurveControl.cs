using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Skins;
using System.Collections.ObjectModel;
using DevExpress.Utils.Drawing;
using DevExpress.Utils;
using PhotoAssistant.Controls.Win.EditingControls;

namespace PhotoAssistant.UI.View.EditingControls {
    [ToolboxItem(true)]
    public class ToneCurveControl : HistogrammControl, IGraphViewerOwner {
        public ToneCurveControl() {
            AutoSize = true;
            SetAutoSizeMode(System.Windows.Forms.AutoSizeMode.GrowAndShrink);
        }

        protected virtual int Side {
            get { return 258; }
        }

        protected override Size CalcSizeableMaxSize() {
            return new Size(Side, Side);
        }
        protected override Size CalcSizeableMinSize() {
            return new Size(Side, Side);
        }

        public override Size GetPreferredSize(Size proposedSize) {
            Size res = base.GetPreferredSize(proposedSize);
            int side = Math.Min(res.Width, res.Height);
            return new Size(side, side + TrackBar.GetPreferredSize(proposedSize).Height);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) {
            base.SetBoundsCore(x, y, width, height, specified);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override HistogrammLabelCollection Labels {
            get { return base.Labels; }
        }
        public override void SetLabel(string label) {
        }
        public override void SetLabels(IEnumerable<string> labels) {
        }
        public override void SetLabels(string label, string label2) {
        }
        public override void SetLabels(string label, string label2, string label3, string label4) {
        }
        AppearanceObject labelAppearance;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AppearanceObject LabelAppearance {
            get {
                if(labelAppearance == null)
                    labelAppearance = CreateAppearanceObject();
                return labelAppearance;
            }
        }

        protected virtual AppearanceObject CreateAppearanceObject() {
            AppearanceObject res = new AppearanceObject();
            res.SizeChanged += AppearanceObjectChanged;
            return res;
        }

        private void AppearanceObjectChanged(object sender, EventArgs e) {
            OnPropertiesChanged();
        }

        protected override void CreateDefaultButtons() {
            ContextButton dragText = new ContextButton();
            dragText.Alignment = ContextItemAlignment.TopNear;
            dragText.Name = "DragText";
            ContextButtons.Add(dragText);
        }

        GraphViewer viewer;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GraphViewer GraphViewer {
            get {
                if(viewer == null) {
                    viewer = CreateViewer();
                    UpdateGraphsVisibility();
                }
                return viewer;
            }
        }

        private bool editCurvePoints;
        [DefaultValue(false)]
        public bool EditCurvePoints {
            get { return editCurvePoints; }
            set {
                if(EditCurvePoints == value)
                    return;
                editCurvePoints = value;
                OnEditCurvePointsChanged();
            }
        }

        private void OnEditCurvePointsChanged() {
            UpdateTrackBarVisibility();
            UpdateGraphsVisibility();
            OnPropertiesChanged();
        }

        ColorTrackBarControl trackBar;
        protected internal ColorTrackBarControl TrackBar {
            get {
                if(trackBar == null) {
                    trackBar = CreateTrackBar();
                    UpdateTrackBarVisibility();
                }
                return trackBar;
            }
        }

        protected virtual ColorTrackBarControl CreateTrackBar() {
            ColorTrackBarControl trackBar = new ColorTrackBarControl();
            trackBar.Properties.Minimum = 0;
            trackBar.Properties.Maximum = 500;
            trackBar.Properties.TickStyle = TickStyle.None;
            trackBar.Properties.AllowFocused = false;
            trackBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            trackBar.Properties.Colors.Add(new ColorGradientStop() { Color = Color.Black, Position = 0.0f });
            trackBar.Properties.Colors.Add(new ColorGradientStop() { Color = Color.LightGray, Position = 1.0f });
            trackBar.BackColor = Color.Transparent;
            trackBar.MouseDown += TrackBar_MouseDown;
            trackBar.MouseUp += TrackBar_MouseUp;
            trackBar.MouseLeave += TrackBar_MouseLeave;
            trackBar.ValueChanged += TrackBar_ValueChanged;
            trackBar.MouseMove += TrackBar_MouseMove;
            trackBar.Values.BeginUpdate();
            try {
                trackBar.Values.Clear();
                trackBar.Values.Add(trackBar.Properties.Maximum / 4);
                trackBar.Values.Add(trackBar.Properties.Maximum / 2);
                trackBar.Values.Add(trackBar.Properties.Maximum * 3 / 4);
            }
            finally {
                trackBar.Values.CancelUpdate();
            }
            Controls.Add(trackBar);
            return trackBar;
            
        }

        private void TrackBar_MouseLeave(object sender, EventArgs e) {
            ToneCurveGraph.ShowShadowsLine =
               ToneCurveGraph.ShowDarksLine =
               ToneCurveGraph.ShowLightsLine = false;
        }

        protected virtual void UpdateToneGraphLineVisibility(MouseEventArgs e) {
            //MultiTrackBarHitInfo hitInfo = TrackBar.CalcHitInfo(e.Location);
            //if(hitInfo.ThumbIndex == 0)
            //    ToneCurveGraph.ShowShadowsLine = true;
            //else if(hitInfo.ThumbIndex == 1)
            //    ToneCurveGraph.ShowDarksLine = true;
            //else if(hitInfo.ThumbIndex == 2)
            //    ToneCurveGraph.ShowLightsLine = true;
            //else {
            //    ToneCurveGraph.ShowShadowsLine =
            //    ToneCurveGraph.ShowDarksLine =
            //    ToneCurveGraph.ShowLightsLine = false;
            //}
        }

        private void TrackBar_MouseMove(object sender, MouseEventArgs e) {
            if(e.Button != MouseButtons.None)
                return;
            UpdateToneGraphLineVisibility(e);
        }

        private void TrackBar_MouseUp(object sender, MouseEventArgs e) {
            ToneCurveGraph.ShowShadowsLine =
            ToneCurveGraph.ShowDarksLine =
            ToneCurveGraph.ShowLightsLine = false;
        }

        private void TrackBar_MouseDown(object sender, MouseEventArgs e) {
            UpdateToneGraphLineVisibility(e);
        }

        [DefaultValue(0.25)]
        public double ShadowsArgument {
            get { return ToneCurveGraph.ShadowsArgument; }
            set {
                if(ToneCurveGraph.ShadowsArgument == value)
                    return;
                ToneCurveGraph.ShadowsArgument = value;
                TrackBar.Values[0] = (int)(value * 500);
            }
        }

        [DefaultValue(0.5)]
        public double DarksArgument {
            get { return ToneCurveGraph.DarksArgument; }
            set {
                if(ToneCurveGraph.DarksArgument == value)
                    return;
                ToneCurveGraph.DarksArgument = value;
                TrackBar.Values[1] = (int)(value * 500);
            }
        }

        [DefaultValue(0.75)]
        public double LightsArgument {
            get { return ToneCurveGraph.LightsArgument; }
            set {
                if(ToneCurveGraph.LightsArgument == value)
                    return;
                ToneCurveGraph.LightsArgument = value;
                TrackBar.Values[2] = (int)(value * 500);
            }
        }
        
        private void TrackBar_ValueChanged(object sender, EventArgs e) {
            ToneCurveGraph.BeginUpdate();
            try {
                ToneCurveGraph.ShadowsArgument = TrackBar.Values[0] / 500.0;
                ToneCurveGraph.DarksArgument = TrackBar.Values[1] / 500.0;
                ToneCurveGraph.LightsArgument = TrackBar.Values[2] / 500.0;
            }
            finally {
                ToneCurveGraph.EndUpdate();
            }
        }

        protected virtual void UpdateTrackBarVisibility() {
            TrackBar.Visible = !EditCurvePoints;
            OnPropertiesChanged();
        }

        ToneCurveGraph toneCurveGraph;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ToneCurveGraph ToneCurveGraph {
            get {
                if(toneCurveGraph == null)
                    toneCurveGraph = CreateToneCurveGraph();
                return toneCurveGraph;
            }
        }

        private ToneCurveGraph CreateToneCurveGraph() {
            ToneCurveGraph res = new ToneCurveGraph();
            res.ShadowsMinValue = -0.2;
            res.ShadowsMaxValue = 0.2;
            res.DarksMinValue = -0.2;
            res.DarksMaxValue = 0.2;
            res.LightsMinValue = -0.2;
            res.LightsMaxValue = 0.2;
            res.HighlightsMinValue = -0.2;
            res.HighlightsMaxValue = 0.2;

            return res;
        }

        Graph curvePointsGraph;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        protected internal Graph ValueGraph {
            get {
                if(curvePointsGraph == null)
                    curvePointsGraph = CreateCurvePointsGraph();
                return curvePointsGraph;
            }
        }

        protected virtual Graph CreateCurvePointsGraph() {
            Graph res = new Graph();
            res.Points.Add(new GraphPoint(0, 0));
            res.Points.Add(new GraphPoint(1.0, 1.0));
            res.AdditiveModificatror = ToneCurveGraph;
            return res;
        }

        protected virtual GraphViewer CreateViewer() {
            GraphViewer viewer = new GraphViewer(this);
            viewer.BeginUpdate();
            try {
                viewer.Graphs.Add(ValueGraph);
                viewer.Graphs.Add(ToneCurveGraph);
                ValueGraph.Visible = true;
                ToneCurveGraph.Visible = false;
                return viewer;
            }
            finally {
                viewer.CancelUpdate();
            }
        }

        protected virtual void UpdateGraphsVisibility() {
            if(EditCurvePoints) {
                ValueGraph.DrawPoints = true;
                ValueGraph.AdditiveModificatror = null;
                ValueGraph.HitTestVisible = true;
            }
            else {
                ValueGraph.DrawPoints = false;
                ValueGraph.AdditiveModificatror = ToneCurveGraph;
                ValueGraph.HitTestVisible = false;
            }
        }

        [DefaultValue(true)]
        public override bool DrawMonochrome {
            get {
                return true;
            }
            set {
            }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool ShowClippingButtons {
            get { return false; }
            set { base.ShowClippingButtons = value; }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool ShowChannelButtons {
            get { return false; }
            set { base.ShowChannelButtons = value; }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool ShowChartModeButton {
            get { return false; }
            set { base.ShowChartModeButton = value; }
        }

        Rectangle IGraphViewerOwner.ViewportBounds {
            get {
                return ((ToneCurveControlViewInfo)ViewInfo).ChartBounds;
            }
        }

        public ToneCurveControlViewInfo ToneCurveViewInfo {
            get { return (ToneCurveControlViewInfo)ViewInfo; } }

        protected override BaseStyleControlViewInfo CreateViewInfo() {
            return new ToneCurveControlViewInfo(this);
        }
        protected override BaseControlPainter CreatePainter() {
            return new ToneCurveControlPainter();
        }
        protected override HistogrammControlHandler CreateHandler() {
            return new ToneCurveControlHandler(this);
        }

        public void OnChanged() {
            OnPropertiesChanged();
        }

        void IGraphViewerOwner.Invalidate() {
            Invalidate();
        }

        void IGraphViewerOwner.Update() {
            Update();
        }

        void IGraphViewerOwner.OnSelectedPointChanged(GraphPoint point) {
            if(point == null)
                ((ContextButton)ContextButtons[0]).Caption = "";
            else
                ((ContextButton)ContextButtons[0]).Caption = string.Format("{0:00.0}/{1:00.0}%", point.X * 100, point.Y * 100);
            ((ToneCurveControlViewInfo)ViewInfo).ContextButtonsViewInfo.InvalidateViewInfo();
            ((ToneCurveControlViewInfo)ViewInfo).ContextButtonsViewInfo.CalcItems();
        }
    }

    public class ToneCurveControlViewInfo : HistogrammControlViewInfo {
        public ToneCurveControlViewInfo(BaseStyleControl owner) : base(owner) { }
        protected override int CalcLabelsMaxTextHeight() {
            return 0;
        }
        protected override void CalcLabels() { }
        public override bool ShowLabels {
            get { return false; }
        }
        public override bool AllowDrawVerticalTickLine {
            get { return true; }
        }
        protected Padding ChartPaddings {
            get { return new Padding(0); }
        }
        protected ToneCurveControl ToneCurve {
            get { return (ToneCurveControl)OwnerControl; }
        }
        protected int TrackBarHeight {
            get { return ToneCurve.TrackBar.Height; }
        }
        protected override Rectangle CalcChartBounds() {
            Rectangle res = base.CalcChartBounds();
            res.X += ChartPaddings.Left;
            res.Y += ChartPaddings.Top;
            res.Width -= ChartPaddings.Left + ChartPaddings.Right;
            res.Height -= ChartPaddings.Top + ChartPaddings.Bottom;
            res.Height -= TrackBarHeight;
            return res;
        }
        protected override void CalcRects() {
            base.CalcRects();
            if(ToneCurve.TrackBar.Visible)
                LayoutTrackBar();
        }

        AppearanceDefault labelDefaultAppearance;
        public AppearanceDefault LabelDefaultAppearance {
            get {
                if(labelDefaultAppearance == null)
                    labelDefaultAppearance = new AppearanceDefault(ToneCurve.VerticalLineColor, Color.Transparent, Color.Transparent, Color.Transparent, new Font(AppearanceObject.DefaultFont.FontFamily, 7.0f));
                return labelDefaultAppearance;
            }
        }

        AppearanceObject labelAppearance;
        public AppearanceObject LabelAppearance {
            get {
                if(labelAppearance == null) {
                    labelAppearance = new AppearanceObject();
                    AppearanceHelper.Combine(labelAppearance, new AppearanceObject[] { ToneCurve.LabelAppearance }, LabelDefaultAppearance);
                }
                return labelAppearance;
            }
        }

        public override void UpdatePaintAppearance() {
            labelDefaultAppearance = null;
            labelAppearance = null;
            base.UpdatePaintAppearance();
        }

        private void LayoutTrackBar() {
            Rectangle rect = new Rectangle(ChartBounds.X, ChartBounds.Bottom, ChartBounds.Width, ToneCurve.TrackBar.Height);
            if(ToneCurve.Bounds.Equals(rect))
                return;
            ToneCurve.TrackBar.Bounds = rect;
        }

        public Point ThumbLocation {
            get;
            protected internal set;
        }
        public override HistogrammHitInfo CalcHitInfo(Point hitPoint) {
            HistogrammHitInfo res = base.CalcHitInfo(hitPoint);
            if(res.HitTest != HistogrammHitTest.ContextButtons)
                res.HitTest = HistogrammHitTest.None;
            return res;
        }
    }

    public class ToneCurveControlPainter : HistogrammControlPainter {
        protected override void DrawLabels(ControlGraphicsInfoArgs info) { }
        protected override void DrawSelectedExposureArea(ControlGraphicsInfoArgs info) { }
        public override void Draw(ControlGraphicsInfoArgs info) {
            base.Draw(info);
            DrawCurveDiagonal(info);
            DrawCurveLine(info);
            DrawLines(info);
        }

        protected virtual void DrawLines(ControlGraphicsInfoArgs info) {
            ToneCurveControlViewInfo viewInfo = (ToneCurveControlViewInfo)info.ViewInfo;
            ToneCurveControl tc = (ToneCurveControl)viewInfo.HistogrammControl;
            if(tc.ToneCurveGraph.ShowShadowsLine)
                DrawVerticalLine(info, tc.ToneCurveGraph.ShadowsArgument);
            if(tc.ToneCurveGraph.ShowDarksLine)
                DrawVerticalLine(info, tc.ToneCurveGraph.DarksArgument);
            if(tc.ToneCurveGraph.ShowLightsLine)
                DrawVerticalLine(info, tc.ToneCurveGraph.LightsArgument);
        }

        private void DrawVerticalLine(ControlGraphicsInfoArgs info, double x) {
            ToneCurveControlViewInfo viewInfo = (ToneCurveControlViewInfo)info.ViewInfo;
            ToneCurveControl tc = (ToneCurveControl)viewInfo.HistogrammControl;
            PointF bottomPoint = new PointF((float)(viewInfo.ChartBounds.X + (viewInfo.ChartBounds.Width) * x), viewInfo.ChartBounds.Bottom);
            info.Graphics.DrawLine(info.Cache.GetPen(tc.VerticalLineColor),
                new PointF((float)(viewInfo.ChartBounds.X + (viewInfo.ChartBounds.Width) * x), viewInfo.ChartBounds.Y),
                bottomPoint);
            viewInfo.LabelAppearance.DrawString(info.Cache, ((int)(x * 100)).ToString(), new Rectangle((int)bottomPoint.X + 5, (int)bottomPoint.Y - viewInfo.LabelAppearance.FontHeight, 100, viewInfo.LabelAppearance.FontHeight));
        }

        protected virtual void DrawCurveLine(ControlGraphicsInfoArgs info) {
            ToneCurveControlViewInfo viewInfo = (ToneCurveControlViewInfo)info.ViewInfo;
            ToneCurveControl tc = (ToneCurveControl)viewInfo.HistogrammControl;
            tc.GraphViewer.OnPaint(info.Cache);
        }

        private void DrawCurveDiagonal(ControlGraphicsInfoArgs info) {
            ToneCurveControlViewInfo viewInfo = (ToneCurveControlViewInfo)info.ViewInfo;
            System.Drawing.Drawing2D.SmoothingMode mode = info.Graphics.SmoothingMode;
            info.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try {
                Pen pen = info.Cache.GetPen(Color.FromArgb(200, viewInfo.PaintAppearance.ForeColor), 1);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                pen.DashPattern = new float[] { 0.5f, 5.0f };
                info.Graphics.DrawLine(pen, new Point(viewInfo.ChartBounds.X, viewInfo.ChartBounds.Bottom), new Point(viewInfo.ChartBounds.Right, viewInfo.ChartBounds.Top));
            }
            finally {
                info.Graphics.SmoothingMode = mode;
            }
        }
    }

    public class ToneCurveControlHandler : HistogrammControlHandler {
        public ToneCurveControlHandler(ToneCurveControl control) : base(control) { }

        public ToneCurveControl ToneCurve { get { return (ToneCurveControl)Owner; } }
        public override bool OnMouseDown(MouseEventArgs e) {
            ToneCurve.GraphViewer.OnMouseDown(e);
            ToneCurveGraphDragMode = e.Button == MouseButtons.Left && ShouldShowAdornments(e.Location);
            DownPoint = e.Location;
            LastDragPoint = DownPoint;
            if(ToneCurve.GraphViewer.ShowMinMaxArea)
                Cursor.Hide();
            return true;
        }
        protected Point DownPoint { get; set; }
        protected Point LastDragPoint { get; set; }
        protected bool ToneCurveGraphDragMode { get; set; }
        public override bool OnMouseUp(MouseEventArgs e) {
            if(ToneCurveGraphDragMode) {
                Cursor.Position = RestoreCursorPosition();
                Cursor.Show();
            }
            ToneCurve.GraphViewer.OnMouseUp(e);
            return true;
        }

        protected virtual Point RestoreCursorPosition() {
            PointF pt = ToneCurve.GraphViewer.ScreenToClient(DownPoint);
            double x = ToneCurve.ToneCurveGraph.StartArgument + (ToneCurve.ToneCurveGraph.EndArgument - ToneCurve.ToneCurveGraph.StartArgument) * pt.X;
            double y = ToneCurve.ValueGraph.CalcValue(x);
            PointF local = ToneCurve.GraphViewer.ClientToScreen(new GraphPoint(pt.X, y));
            return ToneCurve.PointToScreen(new Point((int)local.X, (int)local.Y));
        }

        public override bool OnMouseLeave(EventArgs e) {
            ExitShowAdornmentsMode();
            return base.OnMouseLeave(e);
        }
        void EnterShowAdornmentsMode() {
            SavePrevCursor();
            ToneCurve.Cursor = Cursors.SizeNS;
            ToneCurve.GraphViewer.ShowMinMaxArea = true;
            ToneCurve.GraphViewer.ShowCurrentCurvePoint = true;
        }
        bool ShouldShowAdornments(Point location) {
            return ToneCurve.ToneCurveViewInfo.ChartBounds.Contains(location) && !ToneCurve.EditCurvePoints;
        }
        public override bool OnMouseMove(MouseEventArgs e) {
            ToneCurve.GraphViewer.OnMouseMove(e);
            if(e.Button == MouseButtons.None) {
                if(!ToneCurve.GraphViewer.IsHovered && ShouldShowAdornments(e.Location)) {
                    EnterShowAdornmentsMode();
                    ToneCurve.Invalidate();
                    ToneCurve.Update();
                }
                else {
                    ExitShowAdornmentsMode();
                }
            }
            else if(e.Button == MouseButtons.Left) {
                if(ToneCurve.GraphViewer.SelectedPoint == null && ShouldShowAdornments(DownPoint)) {
                    if(LastDragPoint.Y != e.Location.Y) {
                        ToneCurve.ToneCurveGraph.UpdateSelectedAreaValue(LastDragPoint.Y - e.Location.Y);
                        LastDragPoint = e.Location;
                    }
                }
                else {

                }
            }
            return true;
        }

        private void ExitShowAdornmentsMode() {
            RestoreCursor();
            ToneCurve.GraphViewer.ShowMinMaxArea = false;
            ToneCurve.GraphViewer.ShowCurrentCurvePoint = false;
        }

        private void RestoreCursor() {
            if(PrevCursor != null)
                ToneCurve.Cursor = PrevCursor;
            PrevCursor = null;
        }

        private void SavePrevCursor() {
            if(PrevCursor == null)
                PrevCursor = ToneCurve.Cursor;
        }

        public override bool OnMouseWheel(MouseEventArgs e) {
            //ToneCurve.Graph.OnMouseWheel(e);
            return true;
        }
        public override void OnDoubleClick(EventArgs e) {
            //ToneCurve.Graph.OnDoubleClick(e);
        }
    }

    public interface IGraphViewerOwner {
        Rectangle ViewportBounds { get; }
        void Invalidate();
        void Update();
        void OnSelectedPointChanged(GraphPoint point);
    }

    public class GraphViewer {
        public GraphViewer(IGraphViewerOwner owner) {
            Owner = owner;
        }

        public IGraphViewerOwner Owner { get; private set; }

        protected int UpdateCount { get; private set; }
        public bool IsLockUpdate { get { return UpdateCount > 0; } }

        public void BeginUpdate() {
            UpdateCount++;
        }
        public void EndUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
            if(UpdateCount == 0)
                OnPropertiesChanged();
        }
        public void CancelUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
        }

        protected internal virtual void OnPropertiesChanged() {
            if(IsLockUpdate)
                return;
            Owner.Invalidate();
            Owner.Update();
        }

        GraphCollection graphs;
        public GraphCollection Graphs {
            get {
                if(graphs == null)
                    graphs = new GraphCollection(this);
                return graphs;
            }
        }

        public void OnPaint(GraphicsCache cache) {
            foreach(Graph graph in Graphs) {
                if(!ShouldDrawGraph(graph))
                    continue;
                DrawGraph(cache, graph);
            }
        }

        private void UpdateGraphsCurrentMouseArgument() {
            foreach(Graph graph in Graphs) {
                graph.CurrentMouseArgument = graph.StartArgument + (graph.Viewer.CurrentMousePoint.X) * (graph.EndArgument - graph.StartArgument); ;
            }
        }

        protected virtual void DrawGraph(GraphicsCache cache, Graph graph) {
            System.Drawing.Drawing2D.SmoothingMode mode = cache.Graphics.SmoothingMode;
            cache.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try {
                DrawMinMaxArea(cache, graph);
                DrawLine(cache, graph);
                DrawPoints(cache, graph);
                if(graph.Viewer.ShowCurrentCurvePoint)
                    DrawCurrentCurvePoint(cache, graph);
            }
            finally {
                cache.Graphics.SmoothingMode = mode;
            }
        }
        private void DrawMinMaxArea(GraphicsCache cache, Graph graph) {
            if(!graph.CanShowMinVaxValues || !graph.Viewer.ShowMinMaxArea)
                return;

            PointF prevPointMin = PointF.Empty;
            PointF prevPointMax = PointF.Empty;

            Brush brush = cache.GetSolidBrush(Color.FromArgb(20, graph.Color));
            Pen pen = cache.GetPen(Color.FromArgb(20, graph.Color));

            for(double x = graph.StartArgument; x <= graph.EndArgument; x += 1.0f / Owner.ViewportBounds.Width) {
                double miny = graph.CalcMinValue(x);
                double maxy = graph.CalcMaxValue(x);
                if(double.IsNaN(miny) || double.IsInfinity(miny) || double.IsNaN(maxy) || double.IsInfinity(maxy))
                    continue;
                PointF pointMin = ClientToScreen(new PointF((float)x, (float)miny));
                PointF pointMax = ClientToScreen(new PointF((float)x, (float)maxy));

                if(!prevPointMin.IsEmpty) {
                    cache.Graphics.DrawLine(pen, pointMin, pointMax);
                }
                prevPointMin = pointMin;
                prevPointMax = pointMax;
            }
        }
        
        private void DrawCurrentCurvePoint(GraphicsCache cache, Graph graph) {
            double x = graph.StartArgument + (graph.Viewer.CurrentMousePoint.X) * (graph.EndArgument - graph.StartArgument);
            double y = graph.CalcValue(x);

            GraphPoint pt = new GraphPoint(x, y);
            DrawPoint(cache, graph, pt);
        }

        private void DrawPoints(GraphicsCache cache, Graph graph) {
            if(!graph.DrawPoints)
                return;
            foreach(GraphPoint pt in graph.Points) {
                DrawPoint(cache, graph, pt);
            }
        }

        public RectangleF GetPointBounds(Graph graph, GraphPoint pt) {
            PointF point = ClientToScreen(pt.ToPoint());
            RectangleF rect = new RectangleF(point.X - graph.PointSize / 2, point.Y - graph.PointSize / 2, graph.PointSize, graph.PointSize);
            return rect;
        }

        private void DrawPoint(GraphicsCache cache, Graph graph, GraphPoint point) {
            RectangleF rect = GetPointBounds(graph, point);
            Pen pen = null;
            if(point.IsSelected)
                pen = cache.GetPen(graph.SelectedColor, graph.SelectedWidth);
            else if(point.IsHovered)
                pen = cache.GetPen(graph.HoverColor, graph.HoverWidth);
            else
                pen = cache.GetPen(graph.Color, graph.Width);
            cache.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        private void DrawLine(GraphicsCache cache, Graph graph) {
            PointF prevPoint = PointF.Empty;
            for(double x = graph.StartArgument; x <= graph.EndArgument; x += 1.0f / Owner.ViewportBounds.Width) {
                double y = graph.CalcValue(x);
                if(double.IsNaN(y) || double.IsInfinity(y))
                    continue;
                PointF point = ClientToScreen(new PointF((float)x, (float)y));
                if(!prevPoint.IsEmpty) {
                    if(graph.DrawShadow) {
                        PointF spoint = point; spoint.Y += graph.ShadowOffset; spoint.X += graph.ShadowOffset;
                        PointF prevSpoint = prevPoint; prevSpoint.Y += graph.ShadowOffset; prevSpoint.X += graph.ShadowOffset;
                        cache.Graphics.DrawLine(cache.GetPen(graph.ShadowColor, graph.ShadowWidth), prevSpoint, spoint);
                    }
                    Pen pen = IsPointBelongsToCurve(HoverCurve, x)? 
                        cache.GetPen(graph.HoverColor, graph.HoverWidth): 
                        cache.GetPen(graph.Color, graph.Width);
                    cache.Graphics.DrawLine(pen, prevPoint, point);
                }
                prevPoint = point;
            }
        }

        private bool showCurveBoundsHelpers;
        [DefaultValue(false)]
        public bool ShowMinMaxArea {
            get { return showCurveBoundsHelpers; }
            set {
                if(ShowMinMaxArea == value)
                    return;
                showCurveBoundsHelpers = value;
                OnPropertiesChanged();
            }
        }

        private bool showCurrentCurvePoint;
        [DefaultValue(false)]
        public bool ShowCurrentCurvePoint {
            get { return showCurrentCurvePoint; }
            set {
                if(ShowCurrentCurvePoint == value)
                    return;
                showCurrentCurvePoint = value;
                OnPropertiesChanged();
            }
        }

        public double ApplyConstraints(double y) {
            return Math.Min(Math.Max(MinValue, y), MaxValue);
        }

        private bool IsPointBelongsToCurve(GraphCurve curve, double x) {
            if(curve == null)
                return false;
            return x >= curve.P1.X && x <= curve.P2.X;
        }

        public PointF ClientToScreen(GraphPoint pt) {
            return ClientToScreen(pt.ToPoint());
        }
        public PointF ClientToScreen(PointF pt) {
            return new PointF(Owner.ViewportBounds.X + pt.X * Owner.ViewportBounds.Width, Owner.ViewportBounds.Bottom - pt.Y * Owner.ViewportBounds.Height);
        }

        protected virtual bool ShouldDrawGraph(Graph graph) {
            return graph.ShouldDraw;
        }

        private GraphPoint selectedPoint;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphPoint SelectedPoint {
            get { return selectedPoint; }
            set {
                if(SelectedPoint == value)
                    return;
                selectedPoint = value;
                OnSelectedPointChanged();
                
            }
        }

        protected virtual void OnSelectedPointChanged() {
            Owner.OnSelectedPointChanged(SelectedPoint);
            OnPropertiesChanged();
        }

        private GraphPoint hoverPoint;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphPoint HoverPoint {
            get { return hoverPoint; }
            set {
                if(HoverPoint == value)
                    return;
                hoverPoint = value;
                OnHoverPointChanged();
            }
        }

        private void OnHoverPointChanged() {
            Owner.OnSelectedPointChanged(HoverPoint);
            Owner.Invalidate();
            Owner.Update();
        }

        public bool IsHovered { get { return HoverCurve != null || HoverPoint != null; } }
        public bool IsSelected { get { return SelectedPoint != null; } }

        private GraphCurve hoverCurve;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphCurve HoverCurve {
            get { return hoverCurve; }
            set {
                if(object.Equals(HoverCurve, value))
                    return;
                hoverCurve = value;
                OnPropertiesChanged();
            }
        }

        public virtual void OnMouseMove(MouseEventArgs e) {
            if(e.Button == MouseButtons.None) {
                CurrentMousePoint = ScreenToClient(e.Location);
                UpdateGraphsCurrentMouseArgument();
            }
            if(e.Button == MouseButtons.Left && SelectedPoint != null) {
                DragSelectedPoint(e);
                return;
            }
            HoverPoint = GetPointByMouse(e.Location);
            if(HoverPoint == null) {
                HoverCurve = GetCurveByMouse(e.Location);
            }
            else {
                HoverCurve = null;
            }
        }

        protected virtual GraphCurve GetCurveByMouse(Point location) {
            foreach(Graph graph in Graphs) {
                if(!graph.Visible || !graph.HitTestVisible)
                    continue;
                GraphCurve curve = graph.GetCurveByCoords(location);
                if(curve != null)
                    return curve;
            }
            return null;
        }

        protected virtual GraphPoint GetPointByMouse(Point location) {
            foreach(Graph graph in Graphs) {
                if(!graph.Visible || !graph.HitTestVisible)
                    continue;
                GraphPoint gp = graph.GetPointByCoords(location);
                if(gp != null)
                    return gp;
            }
            return null;
        }

        public virtual PointF ScreenToClient(Point location) {
            return new PointF((location.X - Owner.ViewportBounds.X) / (float)Owner.ViewportBounds.Width, (Owner.ViewportBounds.Bottom - location.Y) / (float)Owner.ViewportBounds.Height);
        }

        protected virtual void DragSelectedPoint(MouseEventArgs e) {
            PointF client = ScreenToClient(e.Location);
            client = ApplyConstraints(client);
            SelectedPoint.BeginUpdate();
            try {
                SelectedPoint.X = client.X;
                SelectedPoint.Y = client.Y;
            }
            finally {
                SelectedPoint.EndUpdate();
                Owner.OnSelectedPointChanged(SelectedPoint);
            }
        }

        private float minValue;
        [DefaultValue(0.0f)]
        public float MinValue {
            get { return minValue; }
            set {
                if(MinValue == value)
                    return;
                minValue = value;
                OnPropertiesChanged();
            }
        }

        private float maxValue = 1.0f;
        [DefaultValue(1.0f)]
        public float MaxValue {
            get { return maxValue; }
            set {
                if(MaxValue == value)
                    return;
                maxValue = value;
                OnPropertiesChanged();
            }
        }

        private double minArgumentDelta = 0.05;
        [DefaultValue(0.05)]
        public double MinArgunebtDelta {
            get { return minArgumentDelta; }
            set {
                if(MinArgunebtDelta == value)
                    return;
                minArgumentDelta = value;
                OnPropertiesChanged();
            }
        }


        private PointF ApplyConstraints(PointF client) {
            int index = SelectedPoint.Index;
            if(index > 0)
                client.X = (float)Math.Max(SelectedPoint.Collection[index - 1].X + MinArgunebtDelta, client.X);
            if(index < SelectedPoint.Collection.Count - 1)
                client.X = (float)Math.Min(SelectedPoint.Collection[index + 1].X - MinArgunebtDelta, client.X);
            client.Y = Math.Max(MinValue, client.Y);
            client.Y = Math.Min(MaxValue, client.Y);
            return client;
        }

        protected internal GraphPoint DownPoint { get; set; }
        public PointF CurrentMousePoint { get; private set; }

        public virtual void OnMouseDown(MouseEventArgs e) {
            DownPoint = GetPointByMouse(e.Location);
            UpdateGraphsCurrentMouseArgument();
            if(DownPoint != null && (DownPoint.Index == 0 || DownPoint.Index == DownPoint.Collection.Count - 1) && !DownPoint.Graph.AllowMoveEndPoints) {
                DownPoint = null;
                return;
            }
            if(e.Button != MouseButtons.Left)
                return;
            SelectedPoint = DownPoint;
            if(SelectedPoint != null)
                return;
            GraphCurve curve = GetCurveByMouse(e.Location);
            if(curve != null) {
                curve.Graph.InsertScreenPoint(e.Location);
                SelectedPoint = GetPointByMouse(e.Location);
            }
        }
        public virtual void OnMouseUp(MouseEventArgs e) {
            if(e.Button == MouseButtons.Right) {
                GraphPoint point = GetPointByMouse(e.Location);
                if(DownPoint != null && DownPoint == point) {
                    point.Graph.Points.Remove(point);
                }
            }
            DownPoint = null;
            SelectedPoint = null;
            HoverPoint = GetPointByMouse(e.Location);
        }
    }

    public class GraphCollection : Collection<Graph> {
        public GraphCollection(GraphViewer owner) {
            Owner = owner;
        }

        public GraphViewer Owner { get; private set; }
        protected internal void OnChanged() {
            if(Owner != null && !IsLockUpdate)
                Owner.OnPropertiesChanged();
        }

        protected int UpdateCount { get; private set; }
        public bool IsLockUpdate { get { return UpdateCount > 0; } }

        public void BeginUpdate() {
            UpdateCount++;
        }
        public void EndUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
            if(UpdateCount == 0)
                OnChanged();
        }
        public void CancelUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
        }
        protected override void InsertItem(int index, Graph item) {
            base.InsertItem(index, item);
            item.Collection = this;
            OnChanged();
        }
        protected override void RemoveItem(int index) {
            this[index].Collection = null;
            base.RemoveItem(index);
            OnChanged();
        }
        protected override void ClearItems() {
            BeginUpdate();
            try {
                base.ClearItems();
            }
            finally {
                EndUpdate();
            }
        }
        protected override void SetItem(int index, Graph item) {
            base.SetItem(index, item);
            OnChanged();
        }
    }

    public class ToneCurveGraph : BezieSplineGraph {
        public ToneCurveGraph() {
            UpdateSplines();
        }

        public override double StartArgument {
            get {
                return 0.0;
            }
        }

        public override double EndArgument {
            get {
                return 1.0;
            }
        }

        public override bool ShouldDraw {
            get { return Visible; }
        }

        private double shadowsMinValue = -0.2;
        [DefaultValue(-0.2)]
        public double ShadowsMinValue {
            get { return shadowsMinValue; }
            set {
                if(ShadowsMinValue == value)
                    return;
                shadowsMinValue = value;
                OnPropertiesChanged();
            }
        }

        private double shadowsMaxValue= 0.2;
        [DefaultValue(0.2)]
        public double ShadowsMaxValue {
            get { return shadowsMaxValue; }
            set {
                if(ShadowsMaxValue == value)
                    return;
                shadowsMaxValue = value;
                OnPropertiesChanged();
            }
        }

        private double darksMinValue = -0.2;
        [DefaultValue(-0.2)]
        public double DarksMinValue {
            get { return darksMinValue; }
            set {
                if(DarksMinValue == value)
                    return;
                darksMinValue = value;
                OnPropertiesChanged();
            }
        }

        private double darksMaxValue = 0.2;
        [DefaultValue(0.2)]
        public double DarksMaxValue {
            get { return darksMaxValue; }
            set {
                if(DarksMaxValue == value)
                    return;
                darksMaxValue = value;
                OnPropertiesChanged();
            }
        }

        private double lightsMinValue = -0.2;
        [DefaultValue(-0.2)]
        public double LightsMinValue {
            get { return lightsMinValue; }
            set {
                if(LightsMinValue == value)
                    return;
                lightsMinValue = value;
                OnPropertiesChanged();
            }
        }

        private double lightsMaxValue = 0.2;
        [DefaultValue(0.2)]
        public double LightsMaxValue {
            get { return lightsMaxValue; }
            set {
                if(LightsMaxValue == value)
                    return;
                lightsMaxValue = value;
                OnPropertiesChanged();
            }
        }


        private double highlightsMinValue = -0.2;
        [DefaultValue(-0.2)]
        public double HighlightsMinValue {
            get { return highlightsMinValue; }
            set {
                if(HighlightsMinValue == value)
                    return;
                highlightsMinValue = value;
                OnPropertiesChanged();
            }
        }

        private double highlightsMaxValue = 0.2;
        [DefaultValue(0.2)]
        public double HighlightsMaxValue {
            get { return highlightsMaxValue; }
            set {
                if(HighlightsMaxValue == value)
                    return;
                highlightsMaxValue = value;
                OnPropertiesChanged();
            }
        }

        private bool showShadowsLine;
        [DefaultValue(false)]
        public bool ShowShadowsLine {
            get { return showShadowsLine; }
            set {
                if(ShowShadowsLine == value)
                    return;
                showShadowsLine = value;
                OnPropertiesChanged();
            }
        }

        private bool showDarksLine;
        [DefaultValue(false)]
        public bool ShowDarksLine {
            get { return showDarksLine; }
            set {
                if(ShowDarksLine == value)
                    return;
                showDarksLine = value;
                OnPropertiesChanged();
            }
        }

        private bool showLightsLine;
        [DefaultValue(false)]
        public bool ShowLightsLine {
            get { return showLightsLine; }
            set {
                if(ShowLightsLine == value)
                    return;
                showLightsLine = value;
                OnPropertiesChanged();
            }
        }

        private double shadowsArgument = 0.25;
        [DefaultValue(0.25)]
        public double ShadowsArgument {
            get { return shadowsArgument; }
            set {
                if(ShadowsArgument == value)
                    return;
                shadowsArgument = value;
                OnPropertiesChanged();
            }
        }

        private double darksArgument = 0.5;
        [DefaultValue(0.5)]
        public double DarksArgument {
            get { return darksArgument; }
            set {
                if(DarksArgument == value)
                    return;
                darksArgument = value;
                OnPropertiesChanged();
            }
        }

        private double lightsArgument = 0.75;
        [DefaultValue(0.75)]
        public double LightsArgument {
            get { return lightsArgument; }
            set {
                if(LightsArgument == value)
                    return;
                lightsArgument = value;
                OnPropertiesChanged();
            }
        }

        private double shadowsValue = 0;
        [DefaultValue(0)]
        public double ShadowsValue {
            get { return shadowsValue; }
            set {
                value = ConstrainValue(value, ShadowsMinValue, ShadowsMaxValue);
                if(ShadowsValue == value)
                    return;
                shadowsValue = value;
                OnPropertiesChanged();
            }
        }

        private double ConstrainValue(double value, double min, double max) {
            return Math.Max(min, Math.Min(max, value));
        }

        private double darksValue = 0;
        [DefaultValue(0)]
        public double DarksValue {
            get { return darksValue; }
            set {
                value = ConstrainValue(value, DarksMinValue, DarksMaxValue);
                if(DarksValue == value)
                    return;
                darksValue = value;
                OnPropertiesChanged();
            }
        }

        private double lightsValue = 0;
        [DefaultValue(0)]
        public double LightsValue {
            get { return lightsValue; }
            set {
                value = ConstrainValue(value, LightsMinValue, LightsMaxValue);
                if(LightsValue == value)
                    return;
                lightsValue = value;
                OnPropertiesChanged();
            }
        }

        private double highlightsValue = 0;
        [DefaultValue(0)]
        public double HighlightsValue {
            get { return highlightsValue; }
            set {
                value = ConstrainValue(value, HighlightsMinValue, HighlightsMaxValue);
                if(HighlightsValue == value)
                    return;
                highlightsValue = value;
                OnPropertiesChanged();
            }
        }

        public override bool CanShowMinVaxValues {
            get {
                return true;
            }
        }

        protected internal override void OnPropertiesChanged() {
            UpdateSplines();
            base.OnPropertiesChanged();
        }

        public class ToneCurveGraphInfo {
            public BezieKoeff ShadowsSpline { get; private set; }
            public BezieKoeff ShadowsSpline2 { get; private set; }
            public BezieKoeff DarksSpline { get; private set; }
            public BezieKoeff DarksSpline2 { get; private set; }
            public BezieKoeff LightsSpline { get; private set; }
            public BezieKoeff LightsSpline2 { get; private set; }
            public BezieKoeff HighlightsSpline { get; private set; }
            public BezieKoeff HighlightsSpline2 { get; private set; }

            public double ShadowsArgument { get; set; }
            public double DarksArgument { get; set; }
            public double LightsArgument { get; set; }

            [DefaultValue(0)]
            public double ShadowsValue { get; set; }
            [DefaultValue(0)]
            public double DarksValue { get; set; }
            [DefaultValue(0)]
            public double LightsValue { get; set; }
            [DefaultValue(0)]
            public double HighlightsValue { get; set; }

            public void UpdateSplines() {
                ShadowsSpline = new BezieKoeff();
                ShadowsSpline2 = new BezieKoeff();
                DarksSpline = new BezieKoeff();
                DarksSpline2 = new BezieKoeff();
                LightsSpline = new BezieKoeff();
                LightsSpline2 = new BezieKoeff();
                HighlightsSpline = new BezieKoeff();
                HighlightsSpline2 = new BezieKoeff();

                UpdateSpline(ShadowsSpline, ShadowsSpline2, 0.0, DarksArgument, ShadowsArgument / 2, ShadowsValue);
                UpdateSpline(DarksSpline, DarksSpline2, 0.0, 1.0, (ShadowsArgument + DarksArgument) / 2, DarksValue);
                UpdateSpline(LightsSpline, LightsSpline2, 0.0, 1.0, (DarksArgument + LightsArgument) / 2, LightsValue);
                UpdateSpline(HighlightsSpline, HighlightsSpline2, DarksArgument, 1.0, (LightsArgument + 1.0) / 2, HighlightsValue);
            }

            protected void UpdateSpline(BezieKoeff sp1, BezieKoeff sp2, double x1, double x2, double center, double value) {
                sp1.CalcParams(new GraphPoint(x1, 0.0), new GraphPoint(center, value), 0.1);
                sp2.CalcParams(new GraphPoint(center, value), new GraphPoint(x2, 0.0), 0.1);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToneCurveGraphInfo Info { get; set; }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToneCurveGraphInfo MinInfo { get; set; }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToneCurveGraphInfo MaxInfo { get; set; }

        protected virtual void UpdateSplines() {
            Info = new ToneCurveGraphInfo() { DarksArgument = this.DarksArgument, LightsArgument = this.LightsArgument, ShadowsArgument = this.ShadowsArgument, ShadowsValue = this.ShadowsValue, DarksValue = this.DarksValue, LightsValue = this.LightsValue, HighlightsValue = this.HighlightsValue };
            MinInfo = new ToneCurveGraphInfo() { DarksArgument = this.DarksArgument, LightsArgument = this.LightsArgument, ShadowsArgument = this.ShadowsArgument, ShadowsValue = this.ShadowsMinValue, DarksValue = this.DarksMinValue, LightsValue = this.LightsMinValue, HighlightsValue = this.HighlightsMinValue };
            MaxInfo = new ToneCurveGraphInfo() { DarksArgument = this.DarksArgument, LightsArgument = this.LightsArgument, ShadowsArgument = this.ShadowsArgument, ShadowsValue = this.ShadowsMaxValue, DarksValue = this.DarksMaxValue, LightsValue = this.LightsMaxValue, HighlightsValue = this.HighlightsMaxValue };

            Info.UpdateSplines();
            MinInfo.UpdateSplines();
            MaxInfo.UpdateSplines();
        }

        protected double CalcValue(BezieKoeff s1, BezieKoeff s2, double x) {
            if(s1.Contains(x))
                return s1.CalcValue(x);
            else if(s2.Contains(x))
                return s2.CalcValue(x);
            return 0.0;
        }

        protected virtual double CalcValue(ToneCurveGraphInfo info, double x) {
            double res = 0.0;

            double res1 = CalcValue(info.ShadowsSpline, info.ShadowsSpline2, x);
            double res2 = CalcValue(info.DarksSpline, info.DarksSpline2, x);
            double res3 = CalcValue(info.LightsSpline, info.LightsSpline2, x);
            double res4 = CalcValue(info.HighlightsSpline, info.HighlightsSpline2, x);

            res = res1 + res2 + res3 + res4;
            return res;
        }

        public override double CalcValue(double x) {
            return CalcValue(Info, x);
        }

        bool IsShadowsAreaSelected {
            get { return CurrentMouseArgument < ShadowsArgument; }
        }

        bool IsDarksAreaSelected {
            get { return CurrentMouseArgument >= ShadowsArgument && CurrentMouseArgument < DarksArgument; }
        }

        bool IsLightsAreaSelected {
            get { return CurrentMouseArgument >= DarksArgument && CurrentMouseArgument < LightsArgument; }
        }

        bool IsHighlightsAreaSelected {
            get { return CurrentMouseArgument >= LightsArgument; }
        }

        public override double CalcMinValue(double x) {
            double res = 0.0;

            double res1 = IsShadowsAreaSelected ? CalcValue(MinInfo.ShadowsSpline, MinInfo.ShadowsSpline2, x): CalcValue(Info.ShadowsSpline, Info.ShadowsSpline2, x);
            double res2 = IsDarksAreaSelected ? CalcValue(MinInfo.DarksSpline, MinInfo.DarksSpline2, x): CalcValue(Info.DarksSpline, Info.DarksSpline2, x);
            double res3 = IsLightsAreaSelected ? CalcValue(MinInfo.LightsSpline, MinInfo.LightsSpline2, x): CalcValue(Info.LightsSpline, Info.LightsSpline2, x);
            double res4 = IsHighlightsAreaSelected ? CalcValue(MinInfo.HighlightsSpline, MinInfo.HighlightsSpline2, x): CalcValue(Info.HighlightsSpline, Info.HighlightsSpline2, x);

            res = res1 + res2 + res3 + res4;
            return res;
        }

        public override double CalcMaxValue(double x) {
            double res = 0.0;

            double res1 = IsShadowsAreaSelected ? CalcValue(MaxInfo.ShadowsSpline, MaxInfo.ShadowsSpline2, x) : CalcValue(Info.ShadowsSpline, Info.ShadowsSpline2, x);
            double res2 = IsDarksAreaSelected ? CalcValue(MaxInfo.DarksSpline, MaxInfo.DarksSpline2, x) : CalcValue(Info.DarksSpline, Info.DarksSpline2, x);
            double res3 = IsLightsAreaSelected ? CalcValue(MaxInfo.LightsSpline, MaxInfo.LightsSpline2, x) : CalcValue(Info.LightsSpline, Info.LightsSpline2, x);
            double res4 = IsHighlightsAreaSelected ? CalcValue(MaxInfo.HighlightsSpline, MaxInfo.HighlightsSpline2, x) : CalcValue(Info.HighlightsSpline, Info.HighlightsSpline2, x);

            res = res1 + res2 + res3 + res4;
            return res;
        }

        public virtual void UpdateSelectedAreaValue(double delta) {
            delta = (delta / Viewer.Owner.ViewportBounds.Height);
            if(IsShadowsAreaSelected) {
                ShadowsValue += delta;
            }
            else if(IsDarksAreaSelected) {
                DarksValue += delta;
            }
            else if(IsLightsAreaSelected) {
                LightsValue += delta;
            }
            else if(IsHighlightsAreaSelected) {
                HighlightsValue += delta;
            }
        }
    }

    public class BezieSplineGraph : Graph {
        protected override SplineInterpolatorBase CreateInterpolator() {
            return new BezieSplineInterpolator(Points);
        }
    }

    public class BSplineGraph : Graph {
        protected override SplineInterpolatorBase CreateInterpolator() {
            return new BSplineInterpolator(Points);
        }
    }

    public class Graph {

        public GraphCollection Collection { get; internal set; }
        protected internal virtual void OnPropertiesChanged() {
            OnPropertiesChanged(false);
        }

        public void BeginUpdate() {
            UpdateCount++;
        }

        public void EndUpdate() {
            CancelUpdate();
            if(UpdateCount == 0)
                OnPropertiesChanged(true);
        }

        public void CancelUpdate() {
            if(UpdateCount > 0)
                UpdateCount--;
        }

        public virtual bool CanShowMinVaxValues {
            get { return AdditiveModificatror != null? AdditiveModificatror.CanShowMinVaxValues: false; }
        }

        protected int UpdateCount { get; set; }

        public bool IsLockUpdate {
            get { return UpdateCount > 0; }
        }

        protected internal virtual void OnPropertiesChanged(bool collectionChanged) {
            if(IsLockUpdate)
                return;
            if(collectionChanged)
                Interpolator.Refresh();
            if(Collection != null)
                Collection.OnChanged();
        }

        protected internal double CurrentMouseArgument { get; set; }

        SplineInterpolatorBase interpolator;
        protected SplineInterpolatorBase Interpolator {
            get {
                if(interpolator == null)
                    interpolator = CreateInterpolator(); 
                return interpolator;
            }
        }

        private Graph additiveModificator;
        [DefaultValue(null)]
        public Graph AdditiveModificatror {
            get { return additiveModificator; }
            set {
                if(AdditiveModificatror == value)
                    return;
                additiveModificator = value;
                OnPropertiesChanged();
            }
        }

        protected virtual SplineInterpolatorBase CreateInterpolator() {
            return new SplineInterpolator(Points);
        }

        public virtual double CalcValue(double x) {
            double value = Interpolator.CalcValue(x);
            if(AdditiveModificatror != null)
                value += AdditiveModificatror.CalcValue(x);
            value = Viewer.ApplyConstraints(value);
            return value;
        }

        public virtual double CalcMinValue(double x) {
            double value = Interpolator.CalcValue(x);
            if(AdditiveModificatror != null) {
                value += AdditiveModificatror.CalcMinValue(x);
            }
            value = Viewer.ApplyConstraints(value);
            return value;
        }

        public virtual double CalcMaxValue(double x) {
            double value = Interpolator.CalcValue(x);
            if(AdditiveModificatror != null) { 
                value += AdditiveModificatror.CalcMaxValue(x);
            }
            value = Viewer.ApplyConstraints(value);
            return value;
        }

        public GraphViewer Viewer {
            get { return Collection == null? null: Collection.Owner; }
        }

        public GraphPoint GetPointByCoords(PointF pt) {
            foreach(GraphPoint p in Points) {
                PointF point = Viewer.ClientToScreen(p);
                if(Math.Abs(point.X - pt.X) < PointSize / 2.0 && Math.Abs(point.Y - pt.Y) < PointSize / 2)
                    return p;
            }
            return null;
        }

        public GraphCurve GetCurveByCoords(Point location) {
            GraphPoint minPoint = null;
            GraphPoint maxPoint = null;
            foreach(GraphPoint pt in Points) {
                PointF sp = Viewer.ClientToScreen(pt);
                if(sp.X <= location.X)
                    minPoint = pt;
                if(sp.X >= location.X) {
                    maxPoint = pt;
                    break;
                }
            }
            if(minPoint == null || maxPoint == null)
                return null;
            PointF local = Viewer.ScreenToClient(location);
            double y = CalcValue(local.X);
            PointF sc = Viewer.ClientToScreen(new PointF(local.X, (float)y));
            if(Math.Abs(location.Y - sc.Y) > PointSize / 2)
                return null;
            return new GraphCurve() { P1 = minPoint, P2 = maxPoint };
        }

        public void InsertScreenPoint(Point location) {
            PointF local = Viewer.ScreenToClient(location);
            double y = CalcValue(local.X);
            GraphPoint pt = GetPointFromRight(local.X);
            GraphPoint newPoint = new GraphPoint(local.X, (float)y);
            if(pt == null)
                Points.Add(newPoint);
            else 
                Points.Insert(Points.IndexOf(pt), newPoint);
        }

        public GraphPoint GetPointFromRight(float x) {
            return Points.FirstOrDefault((pt) => pt.X >= x);
        }
        
        private bool visible = true;
        [DefaultValue(true)]
        public bool Visible {
            get { return visible; }
            set {
                if(Visible == value)
                    return;
                visible = value;
                OnPropertiesChanged();
            }
        }

        private bool hitTestVisible = true;
        [DefaultValue(true)]
        public bool HitTestVisible {
            get { return hitTestVisible; }
            set {
                if(HitTestVisible == value)
                    return;
                hitTestVisible = value;
                OnPropertiesChanged();
            }
        }


        GraphPointCollection points;
        public GraphPointCollection Points {
            get {
                if(points == null)
                    points = new GraphPointCollection(this);
                return points;
            }
        }

        private Color hoverColor = Color.Orange;
        bool ShouldSerizalizeHoverColor() { return HoverColor != Color.Orange; }
        public Color HoverColor {
            get { return hoverColor; }
            set {
                if(HoverColor == value)
                    return;
                hoverColor = value;
                OnPropertiesChanged();
            }
        }

        private int hoverWidth = 2;
        [DefaultValue(2)]
        public int HoverWidth {
            get { return hoverWidth; }
            set {
                if(HoverWidth == value)
                    return;
                hoverWidth = value;
                OnPropertiesChanged();
            }
        }


        private Color selectedColor = Color.Lime;
        bool ShouldSerializeSelecedColor() { return SelectedColor != Color.Lime; }
        public Color SelectedColor {
            get { return selectedColor; }
            set {
                if(SelectedColor == value)
                    return;
                selectedColor = value;
                OnPropertiesChanged();
            }
        }

        private int selectedWidth = 1;
        [DefaultValue(1)]
        public int SelectedWidth {
            get { return selectedWidth; }
            set {
                if(SelectedWidth == value)
                    return;
                selectedWidth = value;
                OnPropertiesChanged();
            }
        }


        private Color lineColor = Color.White;
        bool ShouldSerializeColor() { return Color != Color.White; }
        public Color Color {
            get { return lineColor; }
            set {
                if(Color == value)
                    return;
                lineColor = value;
                OnPropertiesChanged();
            }
        }

        private Color shadowColor = Color.FromArgb(80,0,0,0);
        bool ShouldSerializeShadowColor() { return ShadowColor != Color.FromArgb(80, 0, 0, 0); }
        public Color ShadowColor {
            get { return shadowColor; }
            set {
                if(ShadowColor == value)
                    return;
                shadowColor = value;
                OnPropertiesChanged();
            }
        }

        private int shadowWidth;
        [DefaultValue(2)]
        public int ShadowWidth {
            get { return shadowWidth; }
            set {
                if(ShadowWidth == value)
                    return;
                shadowWidth = value;
                OnPropertiesChanged();
            }
        }

        private int width;
        [DefaultValue(1)]
        public int Width {
            get { return width; }
            set {
                if(Width == value)
                    return;
                width = value;
                OnPropertiesChanged();
            }
        }

        private bool drawShadow = true;
        [DefaultValue(true)]
        public bool DrawShadow {
            get { return drawShadow; }
            set {
                if(DrawShadow == value)
                    return;
                drawShadow = value;
                OnPropertiesChanged();
            }
        }

        private int shadowOffset;
        [DefaultValue(3)]
        public int ShadowOffset {
            get { return shadowOffset; }
            set {
                if(ShadowOffset == value)
                    return;
                shadowOffset = value;
                OnPropertiesChanged();
            }
        }

        private bool drawPoints = true;
        [DefaultValue(true)]
        public bool DrawPoints {
            get { return drawPoints; }
            set {
                if(DrawPoints == value)
                    return;
                drawPoints = value;
                OnPropertiesChanged();
            }
        }

        private float pointSize = 6.0f;
        [DefaultValue(6.0f)]
        public float PointSize {
            get { return pointSize; }
            set {
                if(PointSize == value)
                    return;
                pointSize = value;
                OnPropertiesChanged();
            }
        }
        [DefaultValue(false)]
        public bool AllowMoveEndPoints { get; set; }
        public virtual bool ShouldDraw {
            get { return Visible && Points.Count > 1; } }

        public virtual double StartArgument {
            get { return Points[0].X; } }
        public virtual double EndArgument {
            get { return Points[Points.Count - 1].X; } }
    }

    public class GraphPointCollection : Collection<GraphPoint> {

        public GraphPointCollection(Graph owner) {
            Owner = owner;
        } 

        public Graph Owner { get; private set; }
        protected internal void OnChanged() {
            if(Owner != null && !IsLockUpdate)
                Owner.OnPropertiesChanged(true);
        }

        protected int UpdateCount { get; private set; }
        public bool IsLockUpdate { get { return UpdateCount > 0; } }

        public void BeginUpdate() {
            UpdateCount++;
        }
        public void EndUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
            if(UpdateCount == 0)
                OnChanged();
        }
        public void CancelUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
        }
        protected override void InsertItem(int index, GraphPoint item) {
            base.InsertItem(index, item);
            item.Collection = this;
            OnChanged();
        }
        protected override void RemoveItem(int index) {
            this[index].Collection = null;
            base.RemoveItem(index);
            OnChanged();
        }
        protected override void ClearItems() {
            BeginUpdate();
            try {
                base.ClearItems();
            }
            finally {
                EndUpdate();
            }
        }
        protected override void SetItem(int index, GraphPoint item) {
            base.SetItem(index, item);
            OnChanged();
        }
    }

    public class GraphCurve {
        public GraphPoint P1 { get; set; }
        public GraphPoint P2 { get; set; }
        public Graph Graph { get { return P1.Graph; } }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override bool Equals(object obj) {
            GraphCurve curve = (GraphCurve)obj;
            if(curve == null)
                return false;
            return P1 == curve.P1 && P2 == curve.P2;
        }
    }

    public class GraphPoint {

        public GraphPoint() : this(0, 0) { }
        public GraphPoint(double x, double y) {
            BeginUpdate();
            try {
                X = x; Y = y;
            }
            finally {
                CancelUpdate();
            }
        }
        protected internal GraphPointCollection Collection {
            get; set;
        }

        protected int UpdateCount { get; private set; }
        public bool IsLockUpdate { get { return UpdateCount > 0; } }

        public void BeginUpdate() {
            UpdateCount++;
        }
        public void EndUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
            if(UpdateCount == 0)
                OnPropertiesChanged();
        }
        public void CancelUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
        }

        public PointF ToPoint() { return new PointF((float)X, (float)Y); }

        double x;
        [DefaultValue(0.0)]
        public double X {
            get { return x; }
            set {
                if(X == value)
                    return;
                x = value;
                OnPropertiesChanged();
            }
        }

        protected virtual void OnPropertiesChanged() {
            if(Collection == null || IsLockUpdate)
                return;
            Collection.OnChanged();
        }

        double y;
        [DefaultValue(0.0)]
        public double Y {
            get { return y; }
            set {
                if(Y == value)
                    return;
                y = value;
                OnPropertiesChanged();
            }
        }

        public int Index { get { return Collection == null? -1: Collection.IndexOf(this); } }

        public GraphViewer Viewer {
            get { return Collection == null? null : Collection.Owner.Collection.Owner; }
        }
        public bool IsHovered { get { return Viewer == null? false : Viewer.HoverPoint == this; } }
        public bool IsSelected { get { return Viewer == null? false : Viewer.SelectedPoint == this; } }

        public Graph Graph { get { return Collection.Owner; } }
        public override string ToString() {
            return X.ToString() + "," + Y.ToString();
        }
    }

    public class SplineInterpolatorBase {
        public SplineInterpolatorBase(GraphPointCollection points) {
            Points = points;
        }
        public GraphPointCollection Points { get; private set; }
        public virtual void Refresh() { }
        public virtual double CalcValue(double x) { return 0; }
    }

    public class BSplineKoeff {
        public int PointIndex { get; set; }

        public double A0 { get; set; }
        public double A1 { get; set; }
        public double A2 { get; set; }
        public double A3 { get; set; }

        public double B0 { get; set; }
        public double B1 { get; set; }
        public double B2 { get; set; }
        public double B3 { get; set; }
        public double DeltaX { get; set; }
    }

    public class BezieKoeff {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        public GraphPoint P1 { get; set; }
        public GraphPoint P2 { get; set; }
        public GraphPoint P3 { get; set; }
        public GraphPoint P4 { get; set; }

        public void CalcParams(GraphPoint p1, GraphPoint p2, double weightSize) {
            GraphPoint p3 = new GraphPoint(p1.X + (p2.X - p1.X) * weightSize, p1.Y);
            GraphPoint p4 = new GraphPoint(p2.X - (p2.X - p1.X) * weightSize, p2.Y);
            CalcParams(p1, p2, p3, p4);
        }
        public void CalcParams(GraphPoint p1, GraphPoint p2, GraphPoint p3, GraphPoint p4) {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;

            A = (-p1.Y + 3.0f * (p3.Y - p4.Y) + p2.Y);
            B = (-6.0f * p3.Y + 3.0f * (p1.Y + p4.Y));
            C = 3.0f * (p3.Y - p1.Y);
        }
        public bool Contains(double x) {
            return x >= P1.X && x <= P2.X;
        }

        public double CalcValue(double x) {
            double t = (x - P1.X) / (P2.X - P1.X);
            double t2 = t * t;
            double t3 = t2 * t;

            return t3 * A + t2 * B + t * C + P1.Y;
        }
    }

    public class BezieSplineInterpolator : SplineInterpolatorBase {
        BezieKoeff[] koefficients;

        public BezieSplineInterpolator(GraphPointCollection points) : base(points) { }

        public override void Refresh() {
            if(Points.Count < 4)
                return;
            if(koefficients == null || koefficients.Length != Points.Count)
                koefficients = new BezieKoeff[Points.Count / 3 + 1];
            for(int i = 0, j = 0; i <= Points.Count - 4; i += 3, j++) {
                koefficients[j] = CalcParams(Points[i], Points[i + 3], Points[i + 1], Points[i + 2]);    
            }
        }

        private BezieKoeff CalcParams(GraphPoint P1, GraphPoint P2, GraphPoint P3, GraphPoint P4) {
            BezieKoeff koeff = new BezieKoeff();
            koeff.CalcParams(P1, P2, P3, P4);
            return koeff;
        }

        public override double CalcValue(double x) {
            if(Points.Count == 0)
                return 0.0;
            if(Points.Count == 1)
                return Points[0].Y;
            if(Points.Count == 2 || Points.Count == 3)
                return Points[0].Y + (x - Points[0].X) * (Points[Points.Count - 1].Y - Points[0].Y) / (Points[Points.Count - 1].X - Points[0].X);

            BezieKoeff k = koefficients.FirstOrDefault((koeff) => koeff != null && koeff.P1.X <= x && koeff.P2.X >= x);
            if(k == null)
                return 0.0;
            double t = (x - k.P1.X) / (k.P2.X - k.P1.X);
            double t2 = t * t;
            double t3 = t2 * t;
            return t3 * k.A + t2 * k.B + t * k.C + k.P1.Y;
        }
    }

    public class BSplineInterpolator : SplineInterpolatorBase {
        BSplineKoeff[] k;

        public BSplineInterpolator(GraphPointCollection points) : base(points) {
            Refresh();
        }

        public override void Refresh() {
            if(k == null || k.Length != Points.Count)
                k = new BSplineKoeff[Points.Count];
            for(int i = 0; i < Points.Count - 3; i++) {
                k[i] = CalcParams(i, Points[i], Points[i + 1], Points[i + 2], Points[i + 3]);
            }
        }

        public override double CalcValue(double x) {
            BSplineKoeff k = GetKoefficientFor(x);
            double t = (x - Points[k.PointIndex].X) / k.DeltaX;

            return (k.B2 + t * (k.B1 + t * k.B0)) * t + k.B3;
        }

        private BSplineKoeff GetKoefficientFor(double x) {
            GraphPoint pt = null;
            for(int i = Points.Count - 1; i >= 0; i--) {
                if(Points[i].X <= x) {
                    pt = Points[i];
                    break;
                }
            }
            if(pt == null)
                return k[0];
            return k[pt.Index];
        }

        private BSplineKoeff CalcParams(int index, GraphPoint p1, GraphPoint p2, GraphPoint p3, GraphPoint p4) {

            BSplineKoeff k = new BSplineKoeff();
            k.PointIndex = index;
            k.DeltaX = p4.X - p1.X;
            k.A0 = (-p1.X + 3 * p2.X - 3 * p3.X + p4.X) / 6.0;
            k.A1 = (3 * p1.X - 6 * p2.X + 3 * p3.X) / 6.0;
            k.A2 = (-3 * p1.X + 3 * p3.X) / 6.0;
            k.A3 = (p1.X + 4 * p2.X + p3.X) / 6.0;
            k.B0 = (-p1.Y + 3 * p2.Y - 3 * p3.Y + p4.Y) / 6.0;
            k.B1 = (3 * p1.Y - 6 * p2.Y + 3 * p3.Y) / 6.0;
            k.B2 = (-3 * p1.Y + 3 * p3.Y) / 6.0;
            k.B3 = (p1.Y + 4 * p2.Y + p3.Y) / 6.0;

            return k;
        }
    }

    public class SplineInterpolator : SplineInterpolatorBase {
        private double[] _h;
        private double[] _a;

        public override void Refresh() {
            var n = Points.Count;
            if(_a == null || Points.Count != _a.Length) {
                _a = new double[n];
                _h = new double[n];
            }
            for(int i = 1; i < n; i++) {
                _h[i] = Points[i].X - Points[i - 1].X;
            }

            if(n > 2) {
                var sub = new double[n - 1];
                var diag = new double[n - 1];
                var sup = new double[n - 1];

                for(int i = 1; i <= n - 2; i++) {
                    diag[i] = (_h[i] + _h[i + 1]) / 3;
                    sup[i] = _h[i + 1] / 6;
                    sub[i] = _h[i] / 6;
                    _a[i] = (Points[i + 1].Y - Points[i].Y) / _h[i + 1] - (Points[i].Y - Points[i - 1].Y) / _h[i];
                }

                SolveTridiag(sub, diag, sup, ref _a, n - 2);
            }
        }

        public SplineInterpolator(GraphPointCollection points) : base(points) {
            if(Points == null) {
                throw new ArgumentNullException("nodes");
            }
            Refresh();
        }

        public override double CalcValue(double x) {
            int gap = 0;
            var previous = double.MinValue;

            if(Points.Count == 0)
                return 0;
            if(Points.Count == 1)
                return Points[0].Y;
            if(Points.Count == 2) {
                return Points[0].Y + (x - Points[0].X) * (Points[1].Y - Points[0].Y) / (Points[1].X - Points[0].X);
            }

            for(int i = 0; i < Points.Count; i++) {
                if(Points[i].X == x)
                    return Points[i].Y;
                if(Points[i].X < x && Points[i].X > previous) {
                    previous = Points[i].X;
                    gap = i + 1;
                }
            }

            var x1 = x - previous;
            var x2 = _h[gap] - x1;

            return ((-_a[gap - 1] / 6 * (x2 + _h[gap]) * x1 + Points[gap - 1].Y) * x2 +
                (-_a[gap] / 6 * (x1 + _h[gap]) * x2 + Points[gap].Y) * x1) / _h[gap];
        }

        private static void SolveTridiag(double[] sub, double[] diag, double[] sup, ref double[] b, int n) {
            int i;

            for(i = 2; i <= n; i++) {
                sub[i] = sub[i] / diag[i - 1];
                diag[i] = diag[i] - sub[i] * sup[i - 1];
                b[i] = b[i] - sub[i] * b[i - 1];
            }

            b[n] = b[n] / diag[n];

            for(i = n - 1; i >= 1; i--) {
                b[i] = (b[i] - sup[i] * b[i + 1]) / diag[i];
            }
        }
    }
}
