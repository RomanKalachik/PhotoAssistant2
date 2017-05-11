using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Controls;
using System.ComponentModel;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using System.Windows.Forms;
using System.Drawing;

namespace PhotoAssistant.Controls.Win.EditingControls {
    [UserRepositoryItem("Register")]
    public class RepositoryItemScrollableSpinEdit : RepositoryItemSpinEdit {
        #region Register
        internal const string EditorName = "ScrollableSpinEdit";
        static RepositoryItemScrollableSpinEdit() {
            Register();
        }
        public static void Register() {
            if(EditorRegistrationInfo.Default.Editors.Contains(EditorName))
                return;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(ScrollableSpinEdit),
                typeof(RepositoryItemScrollableSpinEdit), typeof(ScrollableSpinEditViewInfo),
                new ButtonEditPainter(), true, EditImageIndexes.SpinEdit));
        }
        public override string EditorTypeName {
            get {
                return EditorName;
            }
        }
        #endregion

        public RepositoryItemScrollableSpinEdit() {
            SpeedDivider = new decimal(0.3);
        }

        public override void CreateDefaultButton() {
            
        }

        [DefaultValue(BorderStyles.NoBorder)]
        public override BorderStyles BorderStyle {
            get { return BorderStyles.NoBorder; }
            set { base.BorderStyle = value; }
        }

        void ResetSpeedDivider() { SpeedDivider = new decimal(0.3); }
        bool ShouldSerializeSpeedDivider() { return SpeedDivider != new decimal(0.3); }
        public decimal SpeedDivider { get; set; }
    }

    public class ScrollableSpinEditViewInfo : BaseSpinEditViewInfo {
        public ScrollableSpinEditViewInfo(RepositoryItem item) : base(item) { }
        public override bool AllowMaskBox { get { return SpinEdit == null ? false : SpinEdit.AllowMaskBox; } }
        protected ScrollableSpinEdit SpinEdit {
            get { return (ScrollableSpinEdit)OwnerEdit; }
        }
    }

    [ToolboxItem(true)]
    public class ScrollableSpinEdit : SpinEdit {
        static ScrollableSpinEdit() {
            RepositoryItemScrollableSpinEdit.Register();
        }
        public override string EditorTypeName {
            get { return RepositoryItemScrollableSpinEdit.EditorName; }
        }
        public new RepositoryItemScrollableSpinEdit Properties {
            get { return (RepositoryItemScrollableSpinEdit)base.Properties; }
        }
        protected override TextBoxMaskBox CreateMaskBoxInstance() {
            return new ScrollableSpinEditMaskBox(this) { Visible = false };
        }
        protected override void OnMaskBox_LostFocus(object sender, EventArgs e) {
            base.OnMaskBox_LostFocus(sender, e);
            BeginInvoke(new MethodInvoker(HideMaskBox));
        }
        protected internal bool AllowMaskBox { get; set; }
        protected internal void HideMaskBox() {
            AllowMaskBox = false;
            //MaskBox.Visible = false;
            LayoutChanged();
        }
        protected internal void ShowMaskBox() {
            AllowMaskBox = true;
            //MaskBox.Visible = true;
            LayoutChanged();
            MaskBox.Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            Handler.OnMouseMove(e);
        }
        protected override void OnMouseDown(MouseEventArgs e) {
            if(Handler.OnMouseDown(e))
                return;
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            if(Handler.OnMouseUp(e))
                return;
            base.OnMouseUp(e);
        }
        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);
            Handler.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
            Handler.OnMouseLeave(e);
        }

        ScrollableSpinEditHandler handler;
        protected ScrollableSpinEditHandler Handler {
            get {
                if(handler == null)
                    handler = CreateHandler();
                return handler;
            }
        }
        protected virtual ScrollableSpinEditHandler CreateHandler() {
            return new ScrollableSpinEditHandler(this);
        }
    }

    public class ScrollableSpinEditMaskBox : TextBoxMaskBox {
        public ScrollableSpinEditMaskBox(TextEdit owner) : base(owner) { }
        protected ScrollableSpinEdit SpinEdit { get { return (ScrollableSpinEdit)OwnerEdit; } }
    }

    public class ScrollableSpinEditHandler {
        public ScrollableSpinEditHandler(ScrollableSpinEdit owner) {
            SpinEdit = owner;
        }

        public ScrollableSpinEdit SpinEdit { get; set; }
        Cursor PrevCursor { get; set; }
        public virtual void OnMouseEnter(EventArgs e) {
            PrevCursor = SpinEdit.Cursor;
            SpinEdit.Cursor = Cursors.SizeWE;
        }
        public virtual void OnMouseLeave(EventArgs e) {
            SpinEdit.Cursor = PrevCursor;
        }
        protected int GetDelta(Point cursor) {
            return Math.Abs(cursor.X - DownPoint.X);
        }
        protected int DragDelta {
            get { return 3; }
        }
        public virtual void OnMouseMove(MouseEventArgs e) {
            if(SpinEdit.IsDesignMode)
                return;
            if(e.Button != MouseButtons.Left)
                return;
            if(State == ScrollableSpinEditState.Normal) {
                if(GetDelta(e.Location) > DragDelta) {
                    State = ScrollableSpinEditState.Drag;
                    LastPoint = e.Location;
                    Cursor.Hide();
                }
            }
            else {
                int delta = e.Location.X - DownPoint.X;
                if(delta == 0)
                    return;
                SpinEdit.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Default;
                SpinEdit.Value = Math.Max(SpinEdit.Properties.MinValue, Math.Min(SpinEdit.Properties.MaxValue, SpinEdit.Value + delta * SpinEdit.Properties.Increment * SpinEdit.Properties.SpeedDivider));
                SpinEdit.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Buffered;
                SpinEdit.Invalidate();
                SpinEdit.Update();
                Cursor.Position = SpinEdit.PointToScreen(DownPoint);
            }
        }
        protected ScrollableSpinEditState State { get; set; }
        protected Point DownPoint { get; set; }
        protected Point LastPoint { get; set; }
        public virtual bool OnMouseDown(MouseEventArgs e) {
            if(SpinEdit.IsDesignMode)
                return false;
            if(e.Button == MouseButtons.Left) {
                DownPoint = e.Location;
                return true;
            }
            return false;
        }
        public virtual bool OnMouseUp(MouseEventArgs e) {
            if(SpinEdit.IsDesignMode)
                return false;
            if(State == ScrollableSpinEditState.Normal) {
                SpinEdit.ShowMaskBox();
                SpinEdit.Cursor = PrevCursor;
                return true;
            }
            Cursor.Show();
            State = ScrollableSpinEditState.Normal;
            return false;
        }
    }
    public enum ScrollableSpinEditState { Normal, Drag }
}
