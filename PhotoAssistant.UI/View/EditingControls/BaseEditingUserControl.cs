using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.UI.View.EditingControls
{
    public class BaseEditingUserControl : XtraUserControl {


        private EditingControl editingControl;
        [DefaultValue(null)]
        public EditingControl EditingControl
        {
            get { return editingControl; }
            set
            {
                if (EditingControl == value)
                    return;
                editingControl = value;
                OnEditingControlChanged();
            }
        }

        protected virtual void OnEditingControlChanged()
        {
            
        }
    }
}
