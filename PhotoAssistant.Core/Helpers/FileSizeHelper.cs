using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PhotoAssistant.Core {
    public class FileSizeHelper : ModelHelperBase {
        public FileSizeHelper(DmModel model) : base(model) {
        }
        public static string Size2String(long fileSize) {
            float kb = (float)fileSize / 1024.0f;
            float mb = kb / 1024.0f;
            float gb = mb / 1024.0f;
            float tb = gb / 1024.0f;

            if(tb > 0.5f) {
                return $"{tb:0.#} TB";
            }

            if(gb > 1.0f) {
                return $"{gb:0.#} GB";
            }

            if(mb > 1.0f) {
                return $"{mb:0.#} MB";
            }

            if(kb > 1.0f) {
                return $"{kb:0.#} KB";
            }

            return fileSize.ToString() + " byte";
        }
    }
}
