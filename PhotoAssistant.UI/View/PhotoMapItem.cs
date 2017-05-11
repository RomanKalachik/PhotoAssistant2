using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using DevExpress.XtraMap;
using DevExpress.XtraMap.Native;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.View {
    public class PhotoMapItem : MapCustomElement {
        public PhotoMapItem() {
            MapUtils.SetBackgroundVisibility(this, ElementState.Normal);
        }

        List<DmFile> files;
        public List<DmFile> Files {
            get {
                if(files == null)
                    files = new List<DmFile>();
                return files;
            }
        }
    }
}
