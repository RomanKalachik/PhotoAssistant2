using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Drawing.Animation;
using DevExpress.XtraEditors;

namespace PhotoAssistant.UI.View {
    public partial class NotificationForm : XtraForm, ISupportXtraAnimation, IXtraAnimationListener {
        public NotificationForm() {
            AllowTransparency = true;
            ShowInTaskbar = false;
            TopMost = true;
            InitializeComponent();
        }

        public Image Image { get { return this.pictureEdit1.Image; } set { this.pictureEdit1.Image = value; } }
        public string Description { get { return this.labelControl1.Text; } set { this.labelControl1.Text = value; } }
        public string Caption { get { return this.labelControl3.Text; } set { this.labelControl3.Text = value; } }
        public Control OwnerControl { get; set; }
        protected Form OwnerForm { get; set; }
        public bool IsClosed { get; set; }

        public void ShowInfo(Form owner, Image image, string caption, string text) {
            ShowInfo(owner, null, image, caption, text, 0);
        }
        public int AutoHideMiliseconds { get; set; }
        public void ShowInfo(Form owner, Control ownerControl, Image image, string caption, string text, int autoHideMiliseconds) {
            OwnerControl = ownerControl;
            OwnerForm = owner;
            Image = image;
            Caption = caption;
            Description = text;
            ShouldHide = false;
            AutoHideMiliseconds = autoHideMiliseconds;
            if(Visible)
                return;
            ShowInfoAnimated();
            if(AutoHideMiliseconds > 0) {
                AutoHideTimer.Interval = AutoHideMiliseconds + 1000;
                AutoHideTimer.Start();
            }
        }
        Timer autoHideTimer;
        protected Timer AutoHideTimer {
            get {
                if(autoHideTimer == null) {
                    autoHideTimer = new Timer();
                    autoHideTimer.Tick += autoHideTimer_Tick;
                }
                return autoHideTimer;
            }
        }

        void autoHideTimer_Tick(object sender, EventArgs e) {
            AutoHideTimer.Stop();
            IsClosed = true;
            HideInfo();
        }
        protected Rectangle DestinationBounds { get; set; }
        protected Rectangle StartBounds { get; set; }
        protected virtual void ShowInfoAnimated() {
            Point location = Point.Empty;
            if(OwnerControl != null) {
                Rectangle rect = OwnerControl.RectangleToScreen(OwnerControl.ClientRectangle);
                DestinationBounds = new Rectangle(rect.Right - Width, rect.Bottom - Height, Width, Height);
            } else {
                Screen screen = Screen.FromControl(OwnerForm);
                DestinationBounds = new Rectangle(screen.WorkingArea.Right - Width, screen.WorkingArea.Bottom - Height, Width, Height);
            }
            StartBounds = new Rectangle(DestinationBounds.X + Offset, DestinationBounds.Y, DestinationBounds.Width, DestinationBounds.Height);
            Opacity = 0.0f;
            Visible = true;
            Bounds = StartBounds;
            FloatAnimationInfo info = new FloatAnimationInfo(this, this, 1000, 0.0f, 1.0f, true);
            XtraAnimator.Current.AddAnimation(info);
        }
        protected bool ShouldHide { get; set; }
        public void HideInfo() {
            FloatAnimationInfo info = new FloatAnimationInfo(this, this, 1000, 1.0f, 0.0f, true);
            XtraAnimator.Current.AddAnimation(info);
            ShouldHide = true;
        }

        private void labelControl2_Click(object sender, EventArgs e) {
            IsClosed = true;
            AutoHideTimer.Stop();
            HideInfo();
        }

        bool ISupportXtraAnimation.CanAnimate {
            get { return true; }
        }

        Control ISupportXtraAnimation.OwnerControl {
            get { return this; }
        }

        void IXtraAnimationListener.OnAnimation(BaseAnimationInfo info) {
            FloatAnimationInfo finfo = (FloatAnimationInfo)info;
            Location = new Point((int)(StartBounds.X + finfo.Value * (DestinationBounds.X - StartBounds.X)), DestinationBounds.Y); ;
            Opacity = finfo.Value;
        }

        void IXtraAnimationListener.OnEndAnimation(BaseAnimationInfo info) {
            if(ShouldHide)
                Hide();
            ShouldHide = false;
        }

        protected int Offset { get { return Bounds.Width / 5; } }
    }
}
