using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows.Forms;
using PhotoAssistant.Core.Model;
using PhotoAssistant.UI.View;

namespace PhotoAssistant.UI.ViewHelpers {
    public interface IExistingFileManager {
        ExistingFileMode AskUserForExistingFile(Form parent, string fileName);
        string GenerateNewName(string fileName);
    }

    public class ExistingFileManager : IExistingFileManager {
        public static ExistingFileMode ExistingFileMode { get; set; }
        public static bool RememberChoise { get; set; }

        static ExistingFileManager defaultManager;
        public static ExistingFileManager Default {
            get {
                if(defaultManager == null)
                    defaultManager = new ExistingFileManager();
                return defaultManager;
            }
        }

        public ExistingFileMode AskUserForExistingFile(Form parent, string fileName) {
            if(ExistingFileManager.RememberChoise)
                return ExistingFileManager.ExistingFileMode;
            ExistingFileDialog dlg = new ExistingFileDialog();
            dlg.FileName = fileName;
            dlg.ShowDialog(parent);
            ExistingFileManager.RememberChoise = dlg.RememberChoise;
            ExistingFileManager.ExistingFileMode = dlg.Result;
            return dlg.Result;
        }

        public string GenerateNewName(string fileName) {
            int index = 0;
            string path = Path.GetDirectoryName(fileName);
            string file = Path.GetFileNameWithoutExtension(fileName);
            string ext = Path.GetExtension(fileName);
            string newFile = fileName;
            while(IsFileExists(newFile)) {
                index++;
                newFile = path + "\\" + file + "_" + index + ext;
            }
            return newFile;
        }

        protected virtual bool IsFileExists(string fileName) {
            return File.Exists(fileName);
        }
    }
}
