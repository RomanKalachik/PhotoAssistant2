using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using PhotoAssistant.Indexer.Wrappers;
using System.Drawing.Imaging;
using PhotoAssistant.Core.Helpers;
using System.Diagnostics;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.Indexer {
    public static class Program {
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger("PhotoAssistant.Indexer");
        static string[] CreateFakeParams() {
            return @"--DataSource,C:\Users\kalachik\Documents\test3.ddm,--IndexPath,C:\Users\kalachik\Documents\My Web Sites\WebSite1\,--PreviewWidth,1024,--ThumbWidth,392".Split(',');
        }
        [STAThread]
        public static void Main(string[] args) {
#if DEBUG
            if (args.Length < 2) args = CreateFakeParams();
#endif
            try {
                log4net.GlobalContext.Properties["pid"] = Process.GetCurrentProcess().Id;
                Log.Info("indexer started");
                IndexerParameters parameters = IndexerParameters.ParseCommandLine(args);
                Log.Info("Arguments list");
                foreach (var argument in args) Log.Info(string.Format("{0}", argument));
                if (parameters != null) {
                    Log.Info("arguments parsed succesfully");
                    SettingsStore.Default.CurrentDataSource = parameters.DataSource;
                    Indexer indexer = new Indexer();
                    indexer.Model = new DmModel();
                    indexer.Model.OpenDataSource(parameters.DataSource);
                    Log.Info("data source ready");
                    indexer.Process(parameters);
                } else {
                    Log.Info("parameters incorrect");
                }
                Log.Info("indexer stopped");
            } catch(Exception e) {
                Log.Error(e.Message, e);
            }
        }
    }
}