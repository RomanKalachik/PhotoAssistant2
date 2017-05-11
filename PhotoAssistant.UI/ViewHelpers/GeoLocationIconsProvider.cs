using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.UI.ViewHelpers {
    public class GeoLocationIconsProvider {
        static GeoLocationIconsProvider defaultProvider;
        public static GeoLocationIconsProvider Default {
            get {
                if(defaultProvider == null)
                    defaultProvider = new GeoLocationIconsProvider();
                return defaultProvider;
            }
        }

        public Image LocationIcon {
            get { return PhotoAssistant.UI.Properties.Resources.LocationIcon_Pirates; }
        }
    }
}
