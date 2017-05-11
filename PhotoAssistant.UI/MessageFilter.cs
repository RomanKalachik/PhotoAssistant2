using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils.Drawing.Helpers;

namespace PhotoAssistant.UI{
    public class MessageFilter : IMessageFilter {
        public MessageFilter(MainForm form) {
            Form = form;
        }

        protected MainForm Form { get; set; }

        bool IMessageFilter.PreFilterMessage(ref Message m) {
            if(Form.tcMain.SelectedTabPage == Form.tpLibrary && m.Msg == MSG.WM_MOUSEWHEEL) {
                System.Windows.Rect rect = new System.Windows.Rect(0, 0, Form.LibraryControl.PicturePreview.ActualWidth, Form.LibraryControl.PicturePreview.ActualHeight);
                System.Windows.Point pt = System.Windows.Input.Mouse.GetPosition(Form.LibraryControl.PicturePreview);

                if(rect.Contains(pt)) {
                    System.Windows.Interop.HwndSource hwndSource = (System.Windows.Interop.HwndSource)System.Windows.Interop.HwndSource.FromVisual(Form.LibraryControl.PicturePreview);
                    NativeMethods.SendMessage(hwndSource.Handle, m.Msg, m.WParam, m.LParam);
                    return true;
                }
            }
            if(m.Msg == MSG.WM_KEYDOWN || m.Msg == MSG.WM_SYSKEYDOWN || m.Msg == MSG.WM_SYSKEYUP || m.Msg == MSG.WM_KEYUP) {
                if(Form.tcMain.SelectedTabPage == Form.tpLibrary) {
                    if(Form.LibraryControl.filterTree.ClientRectangle.Contains(Form.LibraryControl.filterTree.PointToClient(Control.MousePosition))) {
                        Form.LibraryControl.TemporaryUpdateFilterOperation(true);
                    }
                }
                //else if(Form.tcMain.SelectedTabPage == Form.tpViewer) {
                //    Form.ViewerControl.ProcessKeyDown((Keys)m.WParam.ToInt32());
                //}
            }
            return false;
        }
    }
}
