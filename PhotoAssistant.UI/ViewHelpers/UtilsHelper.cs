using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraLayout;

namespace PhotoAssistant.UI.ViewHelpers {
    public static class UtilsHelper {
        public static System.Drawing.Color NormalizedColorToColor(System.Windows.Media.Color color) {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
        public static System.Windows.Media.Color ColorToNormalizedColor(System.Drawing.Color color) {
            return new System.Windows.Media.Color() { A = color.A, R = color.R, G = color.G, B = color.B };
        }
        public static System.Windows.FontWeight FontWeigthToWpfFontWeight(Font font) {
            if(font.Bold)
                return System.Windows.FontWeights.Bold;
            return System.Windows.FontWeights.Normal;
        }
        public static System.Windows.FontStyle FontStyleToWpfFontStyle(Font font) {
            if(font.Italic)
                return System.Windows.FontStyles.Italic;
            return System.Windows.FontStyles.Normal;
        }
    }

    public static class AccordionControlHelper {
        public static void UpdateAccordionControlHeight(AccordionControl control) {
            UpdateAccordionControlHeight(control.Elements);
        }
        static void UpdateAccordionControlHeight(AccordionControlElementCollection elems) {
            foreach(AccordionControlElement elem in elems) {
                if(elem.Style == ElementStyle.Item) {
                    UpdateAccordionControlHeight(elem);
                } else {
                    UpdateAccordionControlHeight(elem.Elements);
                }
            }
        }

        private static void UpdateAccordionControlHeight(AccordionControlElement elem) {
            Control control = elem.ContentContainer.Controls.Count > 0 ? elem.ContentContainer.Controls[0] : null;
            LayoutControl lc = control as LayoutControl;
            int height = elem.ContentContainer.Padding.Vertical;
            if(lc == null && control != null) {
                if(control.Controls.Count > 0) {
                    lc = control.Controls[0] as LayoutControl;
                    height = control.Padding.Vertical;
                }
            }
            if(lc != null) {
                lc.LayoutChanged();
                elem.ContentContainer.Height = lc.Root.MinSize.Height + height;
            }
        }
    }
}
