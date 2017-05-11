using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Drawing;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using System.Drawing.Drawing2D;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Controls;

namespace PhotoAssistant.UI.View.EditingControls {
    public class RepositoryItemColorTrackBarControl : RepositoryItemMultiTrackBarControl, ITrackBarColorsOwner {
        #region Register
        internal const string ColorEditorName = "ColorTrackBarControl";

        public static void RegisterColor() {
            if(EditorRegistrationInfo.Default.Editors.Contains(ColorEditorName))
                return;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(ColorEditorName, typeof(ColorTrackBarControl),
                typeof(RepositoryItemColorTrackBarControl), typeof(ColorTrackBarControlViewInfo),
                new MultiTrackBarPainter(), true, EditImageIndexes.ProgressBarControl));
        }

        void ITrackBarColorsOwner.OnColorsChanged() {
            OnPropertiesChanged();
        }

        static RepositoryItemColorTrackBarControl() {
            RegisterColor();
        }
        public override string EditorTypeName {
            get { return ColorEditorName; }
        }
        #endregion

        ColorTrackBarGradientStops colors;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorTrackBarGradientStops Colors {
            get {
                if(colors == null)
                    colors = CreateColors();
                return colors;
            }
        }

        protected virtual ColorTrackBarGradientStops CreateColors() {
            return new ColorTrackBarGradientStops(this);
        }
    }

    public class ColorTrackBarControlViewInfo : MultiTrackBarControlViewInfo {
        public ColorTrackBarControlViewInfo(RepositoryItem item) : base(item) { }

        public override TrackBarObjectPainter GetTrackPainter() {
            if(LookAndFeel.ActiveStyle == ActiveLookAndFeelStyle.Skin)
                return new SkinColorTrackBarObjectPainter(LookAndFeel.ActiveLookAndFeel);
            return new ColorTrackBarObjectPainter();
        }
    }

    public class ColorTrackBarObjectPainter : MultiTrackBarObjectPainter {
        protected override void DrawTrackLineCore(TrackBarObjectInfoArgs e, Rectangle bounds) {
            ColorTrackBarControlViewInfo viewInfo = (ColorTrackBarControlViewInfo)e.ViewInfo;
            RepositoryItemColorTrackBarControl item = (RepositoryItemColorTrackBarControl)viewInfo.Item;
            for(int i = 0; i < item.Colors.Count - 1; i++) {
                PointF pt1 = new PointF(bounds.X + item.Colors[i].Position * bounds.Width, bounds.Y);
                PointF pt2 = new PointF(bounds.X + item.Colors[i+1].Position * bounds.Width, bounds.Y);
                LinearGradientBrush brush = new LinearGradientBrush(pt1, pt2, item.Colors[i].Color, item.Colors[i + 1].Color);
                e.Graphics.FillRectangle(brush, new RectangleF(pt1.X, bounds.Top, pt2.X - pt1.X, bounds.Height));
                brush.Dispose();
            }
            new BorderPainter().DrawObject(new BorderObjectInfoArgs(e.Cache, viewInfo.PaintAppearance, bounds));
        }
    }

    public class SkinColorTrackBarObjectPainter : SkinMultiTrackBarObjectPainter {
        public SkinColorTrackBarObjectPainter(ISkinProvider provider) : base(provider) { }
        protected override void DrawTrackLineCore(TrackBarObjectInfoArgs e, Rectangle bounds) {
            ColorTrackBarControlViewInfo viewInfo = (ColorTrackBarControlViewInfo)e.ViewInfo;
            RepositoryItemColorTrackBarControl item = (RepositoryItemColorTrackBarControl)viewInfo.Item;
            for(int i = 0; i < item.Colors.Count - 1; i++) {
                PointF pt1 = new PointF(bounds.X + item.Colors[i].Position * bounds.Width, bounds.Y);
                PointF pt2 = new PointF(bounds.X + item.Colors[i + 1].Position * bounds.Width, bounds.Y);
                LinearGradientBrush brush = new LinearGradientBrush(pt1, pt2, item.Colors[i].Color, item.Colors[i + 1].Color);
                e.Graphics.FillRectangle(brush, new RectangleF(pt1.X, bounds.Top, pt2.X - pt1.X, bounds.Height));
                brush.Dispose();
            }
            new SkinTextBorderPainter(Provider).DrawObject(new BorderObjectInfoArgs(e.Cache, viewInfo.PaintAppearance, bounds));
        }
    }

    public class ColorTrackBarControl : MultiTrackBarControl {

        static ColorTrackBarControl() {
            RepositoryItemColorTrackBarControl.RegisterColor();
        }

        public override string EditorTypeName {
            get {
                return RepositoryItemColorTrackBarControl.ColorEditorName;
            }
        }

        public new RepositoryItemColorTrackBarControl Properties {
            get { return base.Properties as RepositoryItemColorTrackBarControl; }
        }
    }

    public class ColorGradientStop {
        public float Position { get; set; }
        public Color Color { get; set; }
    }

    public interface ITrackBarColorsOwner {
        void OnColorsChanged();
    }

    public class ColorTrackBarGradientStops : Collection<ColorGradientStop> {
        public ColorTrackBarGradientStops(ITrackBarColorsOwner owner) {
            Owner = owner;
        }

        public ITrackBarColorsOwner Owner { get; private set; }

        protected virtual void OnValuesChanged() {
            if(UpdateCount > 0)
                return;
            if(Owner != null)
                Owner.OnColorsChanged();
        }

        protected override void InsertItem(int index, ColorGradientStop item) {
            base.InsertItem(index, item);
            OnValuesChanged();
        }
        protected override void RemoveItem(int index) {
            base.RemoveItem(index);
            OnValuesChanged();
        }
        protected override void SetItem(int index, ColorGradientStop item) {
            base.SetItem(index, item);
            OnValuesChanged();
        }
        protected override void ClearItems() {
            base.ClearItems();
            OnValuesChanged();
        }

        public int UpdateCount { get; private set; }

        public void BeginUpdate() {
            UpdateCount++;
        }
        public void EndUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
            if(UpdateCount == 0)
                Owner.OnColorsChanged();
        }
        public void CancelUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
        }
    }
}