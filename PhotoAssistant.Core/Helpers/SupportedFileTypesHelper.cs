using System;
using System.Collections.Generic;
using System.Linq;
namespace PhotoAssistant.Core {
    public static class SupportedFileTypesHelper {
        public static bool SupportFile(string extension) => extension == ".bmp" || extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".tga";
    }
}
