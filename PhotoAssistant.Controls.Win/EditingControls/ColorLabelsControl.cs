using DevExpress.Images;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;

namespace PhotoAssistant.Controls.Win.EditingControls {
    [UserRepositoryItem("Register")]
    public class RepositoryItemColorLabelControl : RepositoryItem, IColorLabelCollectionOwner {
        #region Register
        internal const string EditorName = "ColorLabelControl";

        public static void RegisterColor() {
            if(EditorRegistrationInfo.Default.Editors.Contains(EditorName))
                return;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(ColorLabelControl),
                typeof(RepositoryItemColorLabelControl), typeof(ColorLabelControlViewInfo),
                new ColorLabelControlPainter(), true, EditImageIndexes.RadioGroup));
        }

        ColorLabelCollection labels;
        public ColorLabelCollection Labels {
            get {
                if(labels == null)
                    labels = CreateColorLabelCollection();
                return labels;
            }
        }
        
        int labelIndent = 2;
        [DefaultValue(2)]
        public int LabelIndent {
            get { return labelIndent; }
            set {
                if(LabelIndent == value)
                    return;
                labelIndent = value;
                OnPropertiesChanged();
            }
        }

        Image glyph;
        [DefaultValue(null)]
        public Image Glyph {
            get { return glyph; }
            set {
                if(Glyph == value)
                    return;
                glyph = value;
                OnPropertiesChanged();
            }
        }

        Image hoverGlyph;
        [DefaultValue(null)]
        public Image HoverGlyph {
            get { return hoverGlyph; }
            set {
                if(HoverGlyph == value)
                    return;
                hoverGlyph = value;
                OnPropertiesChanged();
            }
        }
        
        Image emptyGlyph;
        [DefaultValue(null)]
        public Image EmptyGlyph {
            get { return emptyGlyph; }
            set {
                if(EmptyGlyph == value)
                    return;
                emptyGlyph = value;
                OnPropertiesChanged();
            }
        }

        Image emptyHoverGlyph;
        [DefaultValue(null)]
        public Image EmptyHoverGlyph {
            get { return emptyHoverGlyph; }
            set {
                if(EmptyHoverGlyph == value)
                    return;
                emptyHoverGlyph = value;
                OnPropertiesChanged();
            }
        }

        Image selectionGlyph;
        [DefaultValue(null)]
        public Image SelectionGlyph {
            get { return selectionGlyph; }
            set {
                if(SelectionGlyph == value)
                    return;
                selectionGlyph = value;
                OnPropertiesChanged();
            }
        }

        public override void Assign(RepositoryItem item) {
            base.Assign(item);
            RepositoryItemColorLabelControl li = item as RepositoryItemColorLabelControl;
            if(li == null)
                return;
            this.labelIndent = li.LabelIndent;
            this.glyph = li.Glyph;
            this.hoverGlyph = li.HoverGlyph;
            this.emptyGlyph = li.EmptyGlyph;
            this.emptyHoverGlyph = li.EmptyHoverGlyph;
            this.selectionGlyph = li.SelectionGlyph;
            this.Labels.BeginUpdate();
            this.Labels.Clear();
            foreach(ColorLabel label in li.Labels) {
                Labels.Add(label);
            }
            this.Labels.CancelUpdate();
        }

        protected virtual ColorLabelCollection CreateColorLabelCollection() {
            return new ColorLabelCollection(this);
        }

        void IColorLabelCollectionOwner.OnColorsChanged() {
            OnPropertiesChanged();
        }

        static RepositoryItemColorLabelControl() {
            RegisterColor();
        }
        public override string EditorTypeName {
            get { return EditorName; }
        }
        #endregion
    }

    public class ColorLabelControlPainter : BaseEditPainter {
        protected override void DrawContent(ControlGraphicsInfoArgs info) {
            base.DrawContent(info);
            DrawBackground(info);
            DrawLabels(info);
        }

        private void DrawBackground(ControlGraphicsInfoArgs e) {
            if(!BaseEditPainter.DrawParentBackground((BaseEditViewInfo)e.ViewInfo, e.Cache))
                e.Paint.FillRectangle(e.Graphics, e.Cache.GetSolidBrush(e.ViewInfo.PaintAppearance.BackColor), e.ViewInfo.ClientRect);
        }

        protected virtual void DrawLabels(ControlGraphicsInfoArgs info) {
            ColorLabelControlViewInfo viewInfo = (ColorLabelControlViewInfo)info.ViewInfo;
            for(int i = 0; i < viewInfo.LabelItem.Labels.Count; i++) {
                DrawLabel(info, viewInfo.GetLabelGlyph(i), viewInfo.LabelsBounds[i], viewInfo.Attributes[i]);
                if(viewInfo.IsChecked(i))
                    DrawSelection(info, viewInfo.SelectionGlyph, viewInfo.LabelsBounds[i]);
            }
        }

        protected virtual void DrawSelection(ControlGraphicsInfoArgs info, Image selectionGlyph, Rectangle bounds) {
            info.Graphics.DrawImage(selectionGlyph, bounds);
        }

        private void DrawLabel(ControlGraphicsInfoArgs info, Image glyph, Rectangle bounds, ImageAttributes attr) {
            info.Graphics.DrawImage(glyph, bounds, 0, 0, glyph.Width, glyph.Height, GraphicsUnit.Pixel, attr);
        }
    }

    public class ColorLabelControlViewInfo : BaseEditViewInfo {
        public ColorLabelControlViewInfo(RepositoryItem item) : base(item) { }

        protected internal ImageAttributes[] Attributes { get; protected set; }
        public Rectangle[] LabelsBounds { get; protected set; }
        public Rectangle LabelsContentBounds { get; protected set; }
        public Size ActualLabelSize { get; set; }
        Image defaultGlyph;
        protected Image DefaultGlyph {
            get {
                if(defaultGlyph == null)
                    defaultGlyph = Properties.Resources.ColorLabelNormalGlyph;
                //EditorsSkins.GetSkin(DefaultSkinProvider.Default)[EditorsSkins.SkinContextItemRatingIndicator].Image.GetImages().Images[0];
                return defaultGlyph;
            }
        }
        Image defaultEmptyGlyph;
        protected Image DefaultEmptyGlyph {
            get {
                if(defaultEmptyGlyph == null)
                    defaultEmptyGlyph = Properties.Resources.ColorLabelEmptyGlyph;
                //ImageResourceCache.Default.GetImage("grayscaleimages/edit/delete_16x16.png");
                return defaultEmptyGlyph;
            }
        }
        Image defaultHoverGlyph;
        protected Image DefaultHoverGlyph {
            get {
                if(defaultHoverGlyph == null)
                    defaultHoverGlyph = Properties.Resources.ColorLabelHoverGlyph;
                //EditorsSkins.GetSkin(DefaultSkinProvider.Default)[EditorsSkins.SkinContextItemRatingIndicator].Image.GetImages().Images[1];
                return defaultHoverGlyph;
            }
        }
        Image defaultEmptyHoverGlyph;
        protected Image DefaultEmptyHoverGlyph {
            get {
                if(defaultEmptyHoverGlyph == null)
                    defaultEmptyHoverGlyph = Properties.Resources.ColorLabelEmptyHoverGlyph;
                //ImageResourceCache.Default.GetImage("grayscaleimages/edit/delete_16x16.png");
                return defaultEmptyHoverGlyph;
            }
        }
        Image defaultSelectionGlyph;
        protected Image DefaultSelectionGlyph {
            get {
                if(defaultSelectionGlyph == null)
                    defaultSelectionGlyph = Properties.Resources.SelectionGlyph;
                //EditorsSkins.GetSkin(DefaultSkinProvider.Default)[EditorsSkins.SkinContextItemRatingIndicator].Image.GetImages().Images[2];
                return defaultSelectionGlyph;
            }
        }
        public Image Glyph {
            get { return LabelItem.Glyph ?? DefaultGlyph; }
        }
        public Image HoverGlyph {
            get { return LabelItem.HoverGlyph ?? DefaultHoverGlyph; }
        }
        public Image SelectionGlyph {
            get { return LabelItem.SelectionGlyph ?? DefaultSelectionGlyph; }
        }
        public Image EmptyGlyph {
            get { return LabelItem.EmptyGlyph ?? DefaultEmptyGlyph; }
        }
        public Image EmptyHoverGlyph {
            get { return LabelItem.EmptyHoverGlyph ?? DefaultEmptyHoverGlyph; }
        }
        public Rectangle LabelsClientBounds {
            get { return ContentRect; }
        }
        protected override void CalcRects() {
            base.CalcRects();
            CreateAttributes();
            
            ActualLabelSize = CalcActualLabelSize();
            LabelsContentBounds = CalcLabelsContentBounds();
            CalcLabels(LabelsContentBounds);
        }

        protected virtual void CreateAttributes() {
            ClearAttributes();
            Attributes = new ImageAttributes[LabelItem.Labels.Count];
            for(int i = 0; i < Attributes.Length; i++) {
                Attributes[i] = CreateImageAttributes(LabelItem.Labels[i]);
            }
        }

        public override AppearanceDefault DefaultAppearance {
            get {
                AppearanceDefault res = base.DefaultAppearance;
                res.BackColor = Color.Transparent;
                return res;
            }
        }

        protected override bool AllowDrawParentBackground {
            get { return true; }
        }

        protected virtual ImageAttributes CreateImageAttributes(ColorLabel label) {
            float[][] m = new float[5][];

            Color color = IsEmptyLabel(LabelItem.Labels.IndexOf(label)) ? Color.White : label.Color;
            m[0] = new float[] { color.R / 255.0f, 0.0f, 0.0f, 0.0f, 0.0f };
            m[1] = new float[] { 0.0f, color.G / 255.0f, 0.0f, 0.0f, 0.0f };
            m[2] = new float[] { 0.0f, 0.0f, color.B / 255.0f, 0.0f, 0.0f };
            m[3] = new float[] { 0.0f, 0.0f, 0.0f, color.A / 255.0f, 0.0f };
            m[4] = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f };

            ColorMatrix cm = new ColorMatrix(m);
            ImageAttributes attr = new ImageAttributes();
            attr.SetColorMatrix(cm);
            return attr;
        }

        protected virtual void ClearAttributes() {
            if(Attributes == null)
                return;
            for(int i = 0; i < Attributes.Length; i++)
                Attributes[i].Dispose();
            Attributes = null;
        }

        protected virtual Size CalcActualLabelSize() {
            Size actualLabelSize = Glyph.Size;
            Size labelsBestSize = CalcLabelsBestSize(Glyph.Size);
            if(LabelsClientBounds.Width < labelsBestSize.Width) {
                actualLabelSize.Width = (LabelsContentBounds.Width - (LabelItem.Labels.Count - 1) * LabelItem.LabelIndent) / LabelItem.Labels.Count;
            }
            labelsBestSize = CalcLabelsBestSize(actualLabelSize);
            if(LabelsClientBounds.Height < labelsBestSize.Height)
                actualLabelSize.Height = LabelsClientBounds.Height;
            return actualLabelSize;
        }

        protected virtual void CalcLabels(Rectangle bounds) {
            LabelsBounds = new Rectangle[LabelItem.Labels.Count];
            for(int i = 0; i < LabelItem.Labels.Count; i++) {
                LabelsBounds[i] = new Rectangle(bounds.X + i * (ActualLabelSize.Width + LabelItem.LabelIndent), bounds.Y, ActualLabelSize.Width, ActualLabelSize.Height);
            }
        }

        protected virtual Rectangle CalcLabelsContentBounds() {
            Size labelsBestSize = CalcLabelsBestSize(ActualLabelSize);
            return new Rectangle(LabelsClientBounds.X + (LabelsClientBounds.Width - labelsBestSize.Width) / 2, 
                LabelsClientBounds.Y + (LabelsClientBounds.Height - labelsBestSize.Height) / 2, 
                labelsBestSize.Width, labelsBestSize.Height);
        }

        public RepositoryItemColorLabelControl LabelItem {
            get { return (RepositoryItemColorLabelControl)Item; }
        }
        protected virtual Size CalcLabelsBestSize(Size labelSize) {
            return new Size(labelSize.Width * LabelItem.Labels.Count + (LabelItem.Labels.Count - 1) * LabelItem.LabelIndent, labelSize.Height);
        }
        public int SelectedIndex {
            get {
                int index = 0;
                foreach(ColorLabel label in LabelItem.Labels) {
                    if(object.Equals(label.Value, EditValue))
                        return index;
                    index++;
                }
                return -1;
            }
        }
        public virtual Image GetLabelGlyph(int labelIndex) {
            if(IsEmptyLabel(labelIndex)) {
                if(IsHovered(labelIndex))
                    return EmptyHoverGlyph?? EmptyGlyph;
                if(IsChecked(labelIndex))
                    return EmptyGlyph;
                return EmptyGlyph;
            }
            if(IsHovered(labelIndex))
                return HoverGlyph?? Glyph;
            if(IsChecked(labelIndex))
                return Glyph;
            return Glyph;
        }

        public virtual bool IsEmptyLabel(int labelIndex) {
            return LabelItem.Labels[labelIndex].Color == Color.Empty || LabelItem.Labels[labelIndex].Color == Color.Transparent;
        }

        ColorLabelHitInfo hoverInfo;
        public ColorLabelHitInfo HoverInfo {
            get {
                if(hoverInfo == null)
                    hoverInfo = new ColorLabelHitInfo(Point.Empty);
                return hoverInfo;
            }
            set {
                if(HoverInfo.IsEquals(value))
                    return;
                ColorLabelHitInfo prevInfo = HoverInfo;
                hoverInfo = value;
                OnHoverInfoChanged(prevInfo, HoverInfo);
            }
        }

        ColorLabelHitInfo pressedInfo;
        public override EditHitInfo PressedInfo {
            get {
                if(pressedInfo == null)
                    pressedInfo = new ColorLabelHitInfo(Point.Empty);
                return pressedInfo;
            }
            set {
                if(!(value is ColorLabelHitInfo))
                    value = new ColorLabelHitInfo(value.HitPoint);
                if(PressedInfo.Equals(value))
                    return;
                ColorLabelHitInfo prevInfo = (ColorLabelHitInfo)PressedInfo;
                pressedInfo = (ColorLabelHitInfo)value;
                OnPressedInfoChanged(prevInfo, (ColorLabelHitInfo)PressedInfo);
            }
        }

        private void OnPressedInfoChanged(ColorLabelHitInfo prevInfo, ColorLabelHitInfo pressedInfo) {
            if(OwnerControl != null) {
                OwnerControl.Invalidate();
                OwnerControl.Update();
            }
        }

        public override EditHitInfo HotInfo {
            get { return HoverInfo; }
            set { HoverInfo = value as ColorLabelHitInfo; }
        }

        private void OnHoverInfoChanged(ColorLabelHitInfo prevInfo, ColorLabelHitInfo hoverInfo) {
            if(OwnerControl != null) {
                OwnerControl.Invalidate();
                OwnerControl.Update();
            }       
        }

        protected internal virtual bool IsHovered(int labelIndex) {
            return HoverInfo.HitTest == EditHitTest.Button && HoverInfo.LabelIndex == labelIndex;
        }

        protected internal virtual bool IsChecked(int labelIndex) {
            return SelectedIndex == labelIndex;
        }

        public override void Offset(int x, int y) {
            base.Offset(x, y);
            foreach(Rectangle rect in LabelsBounds) {
                rect.Offset(x, y);
            }    
        }
         
        public override EditHitInfo CalcHitInfo(Point p) {
            ColorLabelHitInfo hitInfo = new ColorLabelHitInfo(p);
            for(int i = 0; i < LabelItem.Labels.Count; i++) {
                if(LabelsBounds[i].Contains(p)) {
                    hitInfo.SetHitTestCore(EditHitTest.Button);
                    hitInfo.SetHitObjectCore(LabelItem.Labels[i]);
                    hitInfo.LabelIndex = i;
                    break;
                }
            }
            return hitInfo;
        }

        public override bool UpdateObjectState(MouseButtons mouseButtons, Point mousePosition) {
            ColorLabelHitInfo hitInfo = (ColorLabelHitInfo)CalcHitInfo(mousePosition);
            bool res = !HoverInfo.IsEquals(hitInfo);
            HoverInfo = hitInfo;
            return res;
        }
        protected override bool UpdateObjectState() {
            return UpdateObjectState(MouseButtons.None, MousePosition);
        }
    }

    public class ColorLabelControlHandler {
        public ColorLabelControlHandler(ColorLabelControl owner) {
            Control = owner;
        }

        public ColorLabelControl Control { get; private set; }
        public ColorLabelControlViewInfo ViewInfo {
            get { return Control.ColorViewInfo; }
        }

        public virtual void OnMouseEnter(EventArgs e) {
        }
        public virtual void OnMouseLeave(EventArgs e) {
            ViewInfo.HoverInfo = new ColorLabelHitInfo(Point.Empty);
        }
        public virtual void OnMouseDown(MouseEventArgs e) {
            ViewInfo.PressedInfo = (ColorLabelHitInfo)ViewInfo.CalcHitInfo(e.Location);
        }
        public virtual void OnMouseUp(MouseEventArgs e) {
            ColorLabelHitInfo info = (ColorLabelHitInfo)ViewInfo.CalcHitInfo(e.Location);
            if(ViewInfo.PressedInfo.IsEquals(info)) {
                if(info.HitTest == EditHitTest.Button)
                    Control.EditValue = info.LabelIndex > 0 ? Control.Properties.Labels[info.LabelIndex].Value : null;
            }
            ViewInfo.PressedInfo = new ColorLabelHitInfo(Point.Empty);
        }
        public virtual void OnMouseMove(MouseEventArgs e) {
            ViewInfo.HoverInfo = (ColorLabelHitInfo)ViewInfo.CalcHitInfo(e.Location);
        }
    }

    public class ColorLabelControl : BaseEdit {
        public override string EditorTypeName {
            get { return RepositoryItemColorLabelControl.EditorName; }
        }
        ColorLabelControlHandler handler;
        protected ColorLabelControlHandler Handler {
            get {
                if(handler == null)
                    handler = CreateHandler();
                return handler;
            }
        }

        public ColorLabelControlViewInfo ColorViewInfo { get { return (ColorLabelControlViewInfo)ViewInfo; } }

        protected virtual ColorLabelControlHandler CreateHandler() {
            return new ColorLabelControlHandler(this);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemColorLabelControl Properties {
            get { return (RepositoryItemColorLabelControl)base.Properties; }
        }

        public ColorLabel ColorLabel {
            get {
                foreach(ColorLabel label in Properties.Labels)
                    if(label.Value == EditValue)
                        return label;
                return null;
            }
        }

        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);
            Handler.OnMouseEnter(e);
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            Handler.OnMouseUp(e);
        }
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            Handler.OnMouseDown(e);
        }
        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
            Handler.OnMouseLeave(e);
        }
        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            Handler.OnMouseMove(e);
        }
    }

    public interface IColorLabelCollectionOwner {
        void OnColorsChanged();
    }

    public class ColorLabelCollection : Collection<ColorLabel> {
        public ColorLabelCollection(IColorLabelCollectionOwner owner) {
            Owner = owner;
        }

        public IColorLabelCollectionOwner Owner { get; private set; }

        protected virtual void OnValuesChanged() {
            if(UpdateCount > 0)
                return;
            if(Owner != null)
                Owner.OnColorsChanged();
        }

        protected override void InsertItem(int index, ColorLabel item) {
            base.InsertItem(index, item);
            OnValuesChanged();
        }
        protected override void RemoveItem(int index) {
            base.RemoveItem(index);
            OnValuesChanged();
        }
        protected override void SetItem(int index, ColorLabel item) {
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

    public class ColorLabel {
        public Color Color { get; set; }
        public object Value { get; set; }
    }

    public class ColorLabelHitInfo : EditHitInfo {
        public ColorLabelHitInfo(Point p) {
            LabelIndex = -1;
            SetHitPoint(p);
        }
        public int LabelIndex { get; set; }
        internal void SetHitObjectCore(object obj) { SetHitObject(obj); }
        internal void SetHitTestCore(EditHitTest hitTest) { SetHitTest(hitTest); }
        public bool IsEquals(ColorLabelHitInfo hitInfo) {
            if(hitInfo == null)
                return false;
            return HitTest == hitInfo.HitTest && LabelIndex == hitInfo.LabelIndex;
        }
    }
}
