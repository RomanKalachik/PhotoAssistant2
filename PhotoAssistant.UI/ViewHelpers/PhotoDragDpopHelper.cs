using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.Utils.Internal;
using PhotoAssistant.Core.Model;
using PhotoAssistant.Controls.Wpf;

namespace PhotoAssistant.UI.ViewHelpers {
    public class PhotoDragDpopHelper {
        static PhotoDragDpopHelper defaultHelper;
        public PhotoDragDpopHelper() {
            PhotoSize = new Size(128, 128);
            Application.AddMessageFilter(new PhotoDragMessageFilter(this));

        }
        public static PhotoDragDpopHelper Default {
            get {
                if(defaultHelper == null)
                    defaultHelper = new PhotoDragDpopHelper();
                return defaultHelper;
            }
        }

        protected internal PhotoDragMode Mode { get; set; }
        Point DownPoint { get; set; }
        protected internal Form OwnerForm { get; set; }
        protected internal Control OwnerControl { get; set; }
        protected internal Point CurrentPoint { get; set; }
        protected internal Point Offset { get; set; }
        DmFile photo;
        protected internal DmFile Photo {
            get {
                if(Photos != null)
                    return Photos[0];
                return photo;
            }
            set {
                photo = value;
            }
        }
        protected internal List<DmFile> Photos { get; private set; }
        public void OnMouseDown(Form ownerForm, Control ownerControl, MouseEventArgs e, DmFile photo, Rectangle screen) {
            Photo = photo;
            OnMouseDownCore(ownerForm, ownerControl, e, screen);
        }
        public void OnMouseDown(Form ownerForm, Control ownerControl, MouseEventArgs e, List<DmFile> photos, Rectangle screen) {
            Photos = photos;
            OnMouseDownCore(ownerForm, ownerControl, e, screen);
        }

        private void OnMouseDownCore(Form ownerForm, Control ownerControl, MouseEventArgs e, Rectangle screen) {
            OwnerForm = ownerForm;
            OwnerControl = ownerControl;
            DownPoint = Control.MousePosition;
            PhotoSize = screen.Size;
            Offset = new Point(DownPoint.X - screen.X, DownPoint.Y - screen.Y);
            Mode = PhotoDragMode.WaitDrag;
        }
        static Size DragSize = new Size(10, 10);
        public void OnMouseMove(MouseEventArgs e) {
            if(Mode == PhotoDragMode.NoDrag || Mode == PhotoDragMode.Drag)
                return;
            CurrentPoint = Control.MousePosition;
            if(Math.Abs(CurrentPoint.X - DownPoint.X) < DragSize.Width &&
                Math.Abs(CurrentPoint.Y - DownPoint.Y) < DragSize.Height)
                return;
            Mode = PhotoDragMode.Drag;
            WpfDragDropHelper.Default.DragObject = Photo;
            ShowDragWindow();
        }

        protected virtual Size CalcWindowSize(Size size) {
            size.Width += Window.BorderThickness * 2;
            size.Height += Window.BorderThickness * 2;
            return size;
        }

        protected internal Image GetThumbImage() {
            if(Photo != null) {
                if(Photo.ThumbImage == null && File.Exists(Photo.ThumbFileName))
                    Photo.ThumbImage = Image.FromFile(Photo.ThumbFileName);
                return Photo.ThumbImage;
            }
            return null;
        }

        private void ShowDragWindow() {
            OwnerControl.Capture = false;
            Image img = GetThumbImage();
            if(img == null)
                img = new Bitmap(100, 100);
            Rectangle rect = ImageLayoutHelper.GetImageBounds(new Rectangle(Point.Empty, PhotoSize), img.Size, ImageLayoutMode.Squeeze);
            Window.Location = new Point(CurrentPoint.X - Offset.X, CurrentPoint.Y - Offset.Y);
            Window.Size = CalcWindowSize(rect.Size);
            Window.Show();
        }

        PhotoDragWindow window;
        protected internal PhotoDragWindow Window {
            get {
                if(window == null)
                    window = CreateDragWindow();
                return window;
            }
        }
        public Size PhotoSize { get; set; }
        private PhotoDragWindow CreateDragWindow() {
            PhotoDragWindow win = new PhotoDragWindow(this);
            OwnerForm.AddOwnedForm(win);
            return win;
        }

        internal void MoveWindow() {
            OwnerControl.Capture = false;
            CurrentPoint = Control.MousePosition;
            Window.Location = new Point(CurrentPoint.X - Offset.X, CurrentPoint.Y - Offset.Y);
        }

        event PhotoDragDropEventHandler dragDrop;
        public event PhotoDragDropEventHandler DragDrop {
            add { dragDrop += value; }
            remove { dragDrop -= value; }
        }

        void RaiseDragDrop(PhotoDragDropEventArgs e) {
            if(dragDrop != null)
                dragDrop(this, e);
        }

        internal void DoDragDrop(Control control) {
            Window.Hide();
            Mode = PhotoDragMode.NoDrag;
            PhotoDragDropEventArgs e = new PhotoDragDropEventArgs();
            e.Control = control;
            if(e.Control != null)
                e.Location = e.Control.PointToClient(Control.MousePosition);
            else
                e.Location = Control.MousePosition;
            RaiseDragDrop(e);
        }

        internal void OnMouseUp() {
            if(this.window != null)
                Window.Hide();
            Mode = PhotoDragMode.NoDrag;
        }

        internal void OnDoubleClick() {
            OnMouseUp();
        }

        internal DmFile GetPhoto() {
            if(Photo != null)
                return Photo;
            return Photos[0];
        }
    }

    public delegate void PhotoDragDropEventHandler(object sender, PhotoDragDropEventArgs e);
    public class PhotoDragDropEventArgs {
        public Control Control { get; set; }
        public Point Location { get; set; }
    }

    public class PhotoDragMessageFilter : IMessageFilter {
        public PhotoDragMessageFilter(PhotoDragDpopHelper helper) {
            Helper = helper;
        }
        PhotoDragDpopHelper Helper { get; set; }

        bool IMessageFilter.PreFilterMessage(ref Message m) {
            if(m.Msg == MSG.WM_MOUSEMOVE) {
                if(Helper.Mode == PhotoDragMode.Drag) {
                    Helper.MoveWindow();
                    return true;
                }
            } else if(m.Msg == MSG.WM_LBUTTONUP) {
                if(Helper.Mode == PhotoDragMode.Drag)
                    Helper.DoDragDrop(Control.FromHandle(m.HWnd));
                else
                    Helper.OnMouseUp();
            }
            return false;
        }
    }

    public enum PhotoDragMode { NoDrag, WaitDrag, Drag }

    public class PhotoDragWindow : Form {
        public PhotoDragWindow(PhotoDragDpopHelper helper) {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Helper = helper;
            AllowTransparency = true;
            ShowInTaskbar = false;
            Opacity = 0.6;
            BorderThickness = 5;
        }

        public int BorderThickness { get; set; }

        PhotoDragDpopHelper Helper { get; set; }
        protected override void OnPaint(PaintEventArgs e) {
            Rectangle client = ClientRectangle;
            client.Inflate(-BorderThickness, -BorderThickness);
            e.Graphics.FillRectangle(Brushes.White, ClientRectangle);
            Rectangle border = ClientRectangle;
            border.Width--; border.Height--;
            e.Graphics.DrawRectangle(Pens.Gray, border);
            Rectangle rect = ImageLayoutHelper.GetImageBounds(client, Helper.GetThumbImage().Size, ImageLayoutMode.Squeeze);
            e.Graphics.DrawImage(Helper.GetThumbImage(), rect);
        }

        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);
            if(m.Msg == MSG.WM_NCHITTEST)
                m.Result = new IntPtr(DevExpress.Utils.Drawing.Helpers.NativeMethods.HT.HTTRANSPARENT);
        }
    }

    public interface ISupportPhotoDragDrop {
        bool AllowDragDrop { get; }
    }
}
