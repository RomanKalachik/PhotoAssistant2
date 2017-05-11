using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;

namespace PhotoAssistant.UI.View {
    public partial class WaitForm : DevExpress.XtraWaitForm.WaitForm {
        public WaitForm() {
            InitializeComponent();
            this.layoutControl2.LookAndFeel.ParentLookAndFeel = this.panelControl1.LookAndFeel;
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            this.layoutControl2.LayoutChanged();
            Size = this.layoutControl2.Root.MinSize;
        }

        void UpdateSize() {
            this.layoutControl2.LayoutChanged();

            Size size = this.layoutControl2.Root.MinSize;
            size.Width += 20;
            size.Height += 20;
            Size = size;
        }

        public string Caption {
            get { return lblCaption.Text; }
            set {
                lblCaption.Text = value;
                lblCaption.Visible = !string.IsNullOrEmpty(lblCaption.Text);
            }
        }

        public string Description {
            get { return layoutControlItem2.Text; }
            set {
                lblDescription.Text = value;
                lblDescription.Visible = !string.IsNullOrEmpty(layoutControlItem2.Text);
            }
        }

        public bool IsMarquee {
            get {
                return this.layoutControlItem4.Visible;
            }
            set {
                this.layoutControlItem3.Visibility = value ? LayoutVisibility.Never : LayoutVisibility.Always;
                this.layoutControlItem4.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never;
            }
        }
        public override void ProcessCommand(Enum cmd, object arg) {
            if((DmWaitFormCommand)cmd == DmWaitFormCommand.SetCaption)
                Caption = (string)arg;
            else if((DmWaitFormCommand)cmd == DmWaitFormCommand.SetDescription)
                Description = (string)arg;
            else if((DmWaitFormCommand)cmd == DmWaitFormCommand.SetProgressValue)
                this.pbProgress.EditValue = (int)arg;
            else if((DmWaitFormCommand)cmd == DmWaitFormCommand.SetUndefined)
                IsMarquee = (bool)arg;
            UpdateSize();
        }

        public void ShowWait(IWin32Window owner) {
            if(!Visible)
                ShowDialog(owner);
        }
    }

    public enum DmWaitFormCommand { SetCaption, SetDescription, SetProgressValue, SetUndefined }
}
