using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
namespace PhotoAssistant.Core {
    public interface IPictureNavigatorClient {
        double Zoom {
            get; set;
        }
        bool AllowScrollAnimation {
            get; set;
        }
        PointF ScrollPosition {
            get; set;
        }
        SizeF ImageSize {
            get;
        }
        SizeF ScreenSize {
            get;
        }
        event EventHandler PropertiesChanged;
        void ZoomFill();
        void ZoomFit();
        double ZoomChange {
            get;
        }
    }
}
