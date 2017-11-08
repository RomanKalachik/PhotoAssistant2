using System;
using System.Collections.Generic;
using System.Linq;
namespace PhotoAssistant.Controls.Wpf {
    public class WpfDragDropHelper {
        static WpfDragDropHelper defaultHelper;
        public static WpfDragDropHelper Default {
            get {
                if(defaultHelper == null) {
                    defaultHelper = new WpfDragDropHelper();
                }

                return defaultHelper;
            }
        }
        public object DragObject {
            get; set;
        }
    }
}
