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

namespace PhotoAssistant.UI.View.EditingControls {
    public partial class BasicAdjustmentControl : BaseEditingUserControl {
        public BasicAdjustmentControl() {
            InitializeComponent();
        }
        protected override void OnEditingControlChanged() {
            base.OnEditingControlChanged();
            this.cropParamsControl1.EditingControl = EditingControl;
        }

        private void BcCrop_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.bcCrop.Checked) {
                this.navigationFrame1.SelectedPage = this.npCrop;
                EditingControl.ActivateCropTool();
            }
            else {
                EditingControl.DeactivateCropTool();
            }
        }
    }
}
