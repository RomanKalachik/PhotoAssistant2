using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.UI.View.ExportControls {
    public class PresetNameEditor : TemplateNameEditor {

        protected override string OverwriteText {
            get { return "Template with specified name already exists. Overwrite?"; }
        }

        protected override string Title {
            get { return "Preset Name"; }
        }
    }
}
