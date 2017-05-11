using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PhotoAssistant.Core;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.View.EditingControls {
    public partial class EditingControlLeftPanel : UserControl {
        public EditingControlLeftPanel() {
            InitializeComponent();
        }

        private DmFile currentFile;
        [DefaultValue(0)]
        public DmFile CurrentFile {
            get { return currentFile; }
            set {
                if(CurrentFile == value)
                    return;
                currentFile = value;
                OnCurrentFileChanged();
            }
        }

        public IPictureNavigatorClient NavigatroClient {
            get { return this.pictureNavigator.Properties.Client; }
            set { this.pictureNavigator.Properties.Client = value; }
        }

        protected virtual void OnCurrentFileChanged() {
            this.pictureNavigator.Image = CurrentFile.ThumbImage;
        }

        private void accordionControl1_ContextButtonCustomize(object sender, DevExpress.XtraBars.Navigation.AccordionControlContextButtonCustomizeEventArgs e) {
            if(e.Element != this.aceNavigator)
                e.ContextItem.Visibility = DevExpress.Utils.ContextItemVisibility.Hidden;
        }

        private void accordionControl1_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e) {
            if(e.DataItem != aceNavigator)
                return;
            if(e.Item.Name == "ZoomFit") {
                OnZoomFitClick();
            }
            else if(e.Item.Name == "ZoomFill") {
                OnZoomFillClick();
            }
            else if(e.Item.Name == "ZoomOrigin") {
                OnZoomOriginClick();
            }
            else if(e.Item.Name == "Zoom") {
                OnZoomMenuClick(e.ScreenBounds);
            }
        }

        private void OnZoomMenuClick(Rectangle screenBounds) {
            
        }

        private void OnZoomOriginClick() {
            this.pictureNavigator.Properties.Client.Zoom = 1.0;
        }

        private void OnZoomFillClick() {
            this.pictureNavigator.Properties.Client.ZoomFill();
        }

        private void OnZoomFitClick() {
            this.pictureNavigator.Properties.Client.ZoomFit();
        }
    }
}
