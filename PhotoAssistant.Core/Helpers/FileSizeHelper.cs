using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core {
    public class FileSizeHelper : ModelHelperBase {
        public FileSizeHelper(DmModel model) : base(model) { }

        public static string Size2String(long fileSize) {
            float kb = (float)fileSize / 1024.0f;
            float mb = kb / 1024.0f;
            float gb = mb / 1024.0f;
            float tb = gb / 1024.0f;

            if(tb > 0.5f)
                return string.Format("{0:0.#} TB", tb);
            if(gb > 1.0f)
                return string.Format("{0:0.#} GB", gb);
            if(mb > 1.0f)
                return string.Format("{0:0.#} MB", mb);
            if(kb > 1.0f)
                return string.Format("{0:0.#} KB", kb);
            return fileSize.ToString() + " byte";
        }
    }
}
