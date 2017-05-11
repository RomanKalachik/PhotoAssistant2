using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DevExpress.XtraGrid.Views.Tile;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.View {
    public partial class ViewControlBase : XtraUserControl {
        public ViewControlBase() {
            InitializeComponent();
        }
        public DmModel Model { get; set; }
        public MainForm MainForm { get; set; }

        protected virtual bool AllowQuickGalleryPanel { get { return true; } }
        public virtual void OnShowView() {
            if(MainForm != null) {
                if(AllowQuickGalleryPanel)
                    MainForm.ShowQuickGalleryPanel();
                else
                    MainForm.HideQuickGalleryPanel();
            }
        }
        public virtual void OnHideView() { }
        public virtual void OnQuickGalleryItemClick(object sender, TileViewItemClickEventArgs e) { }
    }
}
