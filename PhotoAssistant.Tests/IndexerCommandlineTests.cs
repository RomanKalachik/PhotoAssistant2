using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoAssistant.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Tests {
    [TestClass]
    public class IndexerCommandlineTests {
        [TestMethod]
        public void SimpleCommandLine() {
            IndexerParameters indexperParamters = new IndexerParameters();
            indexperParamters.DataSource = "test1";
            indexperParamters.IndexPath = "test2";
            indexperParamters.PreviewWidth = 10;
            indexperParamters.ThumbWidth = 11;

            string cmd = IndexerParameters.CreateCommandLine(indexperParamters);
            IndexerParameters parsed = IndexerParameters.ParseCommandLine(cmd.Split(' '));
            Assert.AreEqual(indexperParamters.DataSource, parsed.DataSource);
            Assert.AreEqual(indexperParamters.IndexPath, parsed.IndexPath);
            Assert.AreEqual(indexperParamters.PreviewWidth, parsed.PreviewWidth);
            Assert.AreEqual(indexperParamters.ThumbWidth, parsed.ThumbWidth);
        }
        [TestMethod]
        public void InvokeIndexer() {
            string command= @"--DataSource C:\Users\kalachik\Documents\test3.ddm --IndexPath C:\PhotoAssistant\TestData --PreviewWidth 1024 --ThumbWidth 392";
            string[] commandLine = command.Split(' ');
            Indexer.Program.Main(commandLine);
        }
    }
}
