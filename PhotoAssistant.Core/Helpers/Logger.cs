using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace PhotoAssistant.Core {
    public static class Logger {
        public static void AddLog(string str) => Debug.WriteLine(str);
    }
}
