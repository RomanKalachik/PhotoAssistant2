using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
namespace PhotoAssistant.Tests {
    [TestClass]
    public class SysChecker {
        [TestMethod]
        public void ListFactories() {
            DataTable table = DbProviderFactories.GetFactoryClasses();

            foreach(DataRow row in table.Rows) {
                foreach(DataColumn column in table.Columns) {
                    Console.WriteLine(row[column]);
                }
            }
        }
    }
}
