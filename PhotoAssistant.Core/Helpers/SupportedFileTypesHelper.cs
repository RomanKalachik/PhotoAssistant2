using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core {
    public static class SupportedFileTypesHelper {
        public static bool SupportFile(string extension) {
            return extension == ".bmp" || extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".tga";
        }
    }
}
