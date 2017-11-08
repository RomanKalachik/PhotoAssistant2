using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace PhotoAssistant.Core {
    public static class MD5Helper {
        public static string CalculateMD5(string file) {
            using(System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create()) {
                using(FileStream stream = File.OpenRead(file)) {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty).ToLower();
                }
            }
        }
    }
}
