using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace PhotoAssistant.Controls.Win.EditingControls {
    [UserRepositoryItem("Register")]
    public class RepositoryItemScrollableSpinEdit : RepositoryItemSpinEdit {
        internal const string EditorName = "ScrollableSpinEdit";
        static RepositoryItemScrollableSpinEdit() => Register();
        public static void Register() {
            if(EditorRegistrationInfo.Default.Editors.Contains(EditorName)) {
                return;
            }

            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(ScrollableSpinEdit), typeof(RepositoryItemScrollableSpinEdit), typeof(ScrollableSpinEditViewInfo), new ButtonEditPainter(), true, EditImageIndexes.SpinEdit));
        }
        public override string EditorTypeName => EditorName;
        public RepositoryItemScrollableSpinEdit() => SpeedDivider = new decimal(0.3);
        public override void CreateDefaultButton() {
        }
        [DefaultValue(BorderStyles.NoBorder)]
        public override BorderStyles BorderStyle {
            get => BorderStyles.NoBorder;
            set => base.BorderStyle = value;
        }
        void ResetSpeedDivider() => SpeedDivider = new decimal(0.3);
        bool ShouldSerializeSpeedDivider() => SpeedDivider != new decimal(0.3);
        public decimal SpeedDivider {
            get; set;
        }
    }
    public class ScrollableSpinEditViewInfo : BaseSpinEditViewInfo {
        public ScrollableSpinEditViewInfo(RepositoryItem item) : base(item) {
        }
        public override bool AllowMaskBox => SpinEdit == null ? false : SpinEdit.AllowMaskBox;
        protected ScrollableSpinEdit SpinEdit => (ScrollableSpinEdit)OwnerEdit;
    }
    [ToolboxItem(true)]
    public class ScrollableSpinEdit : SpinEdit {
        static ScrollableSpinEdit() => RepositoryItemScrollableSpinEdit.Register();
        public override string EditorTypeName => RepositoryItemScrollableSpinEdit.EditorName;
        public new RepositoryItemScrollableSpinEdit Properties => (RepositoryItemScrollableSpinEdit)base.Properties;
        protected override TextBoxMaskBox CreateMaskBoxInstance() => new ScrollableSpinEditMaskBox(this) { Visible = false };
        protected override void OnMaskBox_LostFocus(object sender, EventArgs e) {
            base.OnMaskBox_LostFocus(sender, e);
            BeginInvoke(new MethodInvoker(HideMaskBox));
        }
        protected internal bool AllowMaskBox {
            get; set;
        }
        protected internal void HideMaskBox() {
            AllowMaskBox = false;
            LayoutChanged();
        }
        protected internal void ShowMaskBox() {
            AllowMaskBox = true;
            LayoutChanged();
            MaskBox.Focus();
        }
        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            Handler.OnMouseMove(e);
        }
        protected override void OnMouseDown(MouseEventArgs e) {
            if(Handler.OnMouseDown(e)) {
                return;
            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            if(Handler.OnMouseUp(e)) {
                return;
            }

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
                if(handler == null) {
                    handler = CreateHandler();
                }

                return handler;
            }
        }
        protected virtual ScrollableSpinEditHandler CreateHandler() => new ScrollableSpinEditHandler(this);
    }
    public class ScrollableSpinEditMaskBox : TextBoxMaskBox {
        public ScrollableSpinEditMaskBox(TextEdit owner) : base(owner) {
        }
        protected ScrollableSpinEdit SpinEdit => (ScrollableSpinEdit)OwnerEdit;
    }
    public class ScrollableSpinEditHandler {
        public ScrollableSpinEditHandler(ScrollableSpinEdit owner) => SpinEdit = owner;
        public ScrollableSpinEdit SpinEdit {
            get; set;
        }
        Cursor PrevCursor {
            get; set;
        }
        public virtual void OnMouseEnter(EventArgs e) {
            PrevCursor = SpinEdit.Cursor;
            SpinEdit.Cursor = Cursors.SizeWE;
        }
        public virtual void OnMouseLeave(EventArgs e) => SpinEdit.Cursor = PrevCursor;
        protected int GetDelta(Point cursor) => Math.Abs(cursor.X - DownPoint.X);
        protected int DragDelta => 3;
        public virtual void OnMouseMove(MouseEventArgs e) {
            if(SpinEdit.IsDesignMode) {
                return;
            }

            if(e.Button != MouseButtons.Left) {
                return;
            }

            if(State == ScrollableSpinEditState.Normal) {
                if(GetDelta(e.Location) > DragDelta) {
                    State = ScrollableSpinEditState.Drag;
                    LastPoint = e.Location;
                    Cursor.Hide();
                }
            } else {
                int delta = e.Location.X - DownPoint.X;
                if(delta == 0) {
                    return;
                }

                SpinEdit.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Default;
                SpinEdit.Value = Math.Max(SpinEdit.Properties.MinValue, Math.Min(SpinEdit.Properties.MaxValue, SpinEdit.Value + delta * SpinEdit.Properties.Increment * SpinEdit.Properties.SpeedDivider));
                SpinEdit.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Buffered;
                SpinEdit.Invalidate();
                SpinEdit.Update();
                Cursor.Position = SpinEdit.PointToScreen(DownPoint);
            }
        }
        protected ScrollableSpinEditState State {
            get; set;
        }
        protected Point DownPoint {
            get; set;
        }
        protected Point LastPoint {
            get; set;
        }
        public virtual bool OnMouseDown(MouseEventArgs e) {
            if(SpinEdit.IsDesignMode) {
                return false;
            }

            if(e.Button == MouseButtons.Left) {
                DownPoint = e.Location;
                return true;
            }
            return false;
        }
        public virtual bool OnMouseUp(MouseEventArgs e) {
            if(SpinEdit.IsDesignMode) {
                return false;
            }

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
    public enum ScrollableSpinEditState {
        Normal, Drag
    }
}
