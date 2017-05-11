using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core {
    public static class MD5Helper {
        public static string CalculateMD5(string file) {
            using(var md5 = System.Security.Cryptography.MD5.Create()) {
                using(var stream = File.OpenRead(file)) {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }
    }
}
