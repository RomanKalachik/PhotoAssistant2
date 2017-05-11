using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core {
    public static class Logger {
        public static void AddLog(string str) {
            Debug.WriteLine(str);
        }
    }
}
