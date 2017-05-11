﻿using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core {
    public class MediaFormatHelper : ModelHelperBase {
        public MediaFormatHelper(DmModel model) : base(model) { }

        public MediaFormat GetMediaFormat(string ext) {
            if(ext.StartsWith("."))
                ext = ext.Substring(1, ext.Length - 1);
            return Model.GetMediaFormats().Where((format) => format.Extension.Equals(StandartizeExtension(ext))).FirstOrDefault();
        }
        public string StandartizeExtension(string ext) {
            return ext.ToUpper();
        }
        public bool Support(string ext) {
            return GetMediaFormat(ext) != null;
        }
    }
}
