using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PhotoAssistant.Tests {
    [TestClass]
    public class FileRenameManagerTests {
        [TestMethod]
        public void TestFileName() {
            FileRenameManager manager = new FileRenameManager();
            List<FileRenameValueError> errors = new List<FileRenameValueError>();
            FileRenameValueReferenceCollection res = manager.ParseString("{FileName}", errors);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual("FileName", res[0].FileRenameValue.Name);
        }
        [TestMethod]
        public void TestFileNameFailed() {
            FileRenameManager manager = new FileRenameManager();
            List<FileRenameValueError> errors = new List<FileRenameValueError>();
            FileRenameValueReferenceCollection res = manager.ParseString("{Filename}", errors);
            Assert.AreEqual(0, res.Count);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Filename", errors[0].Name);
        }
        [TestMethod]
        public void TestFileNameNoCloseBracket() {
            FileRenameManager manager = new FileRenameManager();
            List<FileRenameValueError> errors = new List<FileRenameValueError>();
            FileRenameValueReferenceCollection res = manager.ParseString("{FileName", errors);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual("FileName", res[0].FileRenameValue.Name);
        }
        [TestMethod]
        public void TestFailedWhenEmptyKeyword() {
            FileRenameManager manager = new FileRenameManager();
            List<FileRenameValueError> errors = new List<FileRenameValueError>();
            FileRenameValueReferenceCollection res = manager.ParseString("{}", errors);
            Assert.AreEqual(0, res.Count);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual(string.Empty, errors[0].Name);
        }
        [TestMethod]
        public void TestNoKeyword() {
            FileRenameManager manager = new FileRenameManager();
            List<FileRenameValueError> errors = new List<FileRenameValueError>();
            FileRenameValueReferenceCollection res = manager.ParseString("Hello World", errors);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual("CustomText", res[0].FileRenameValue.Name);
            Assert.AreEqual("Hello World", res[0].ValueCore);
        }
        [TestMethod]
        public void TestFileName_Spaces() {
            FileRenameManager manager = new FileRenameManager();
            List<FileRenameValueError> errors = new List<FileRenameValueError>();
            FileRenameValueReferenceCollection res = manager.ParseString("{   FileName }", errors);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual("FileName", res[0].FileRenameValue.Name);
        }
        [TestMethod]
        public void TestFileNameNoCloseBracket_Spaces() {
            FileRenameManager manager = new FileRenameManager();
            List<FileRenameValueError> errors = new List<FileRenameValueError>();
            FileRenameValueReferenceCollection res = manager.ParseString("{   FileName   ", errors);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual("FileName", res[0].FileRenameValue.Name);
        }
        [TestMethod]
        public void TestCustomNameXofY() {
            FileRenameManager manager = new FileRenameManager();
            List<FileRenameValueError> errors = new List<FileRenameValueError>();
            FileRenameValueReferenceCollection res = manager.ParseString("{CustomText} - {Index} of {Count}", errors);
            Assert.AreEqual(5, res.Count);
            Assert.AreEqual("CustomText", res[0].FileRenameValue.Name);
            Assert.AreEqual("CustomText", res[1].FileRenameValue.Name);
            Assert.AreEqual(" - ", res[1].ValueCore);
            Assert.AreEqual("Index", res[2].FileRenameValue.Name);
            Assert.AreEqual("CustomText", res[3].FileRenameValue.Name);
            Assert.AreEqual(" of ", res[3].ValueCore);
            Assert.AreEqual("Count", res[4].FileRenameValue.Name);
        }
        [TestMethod]
        public void TestTemplate() {
            FileRenameManager manager = new FileRenameManager();
            manager.Template = "{CustomText} - {Index} of {Count}";
            Assert.AreEqual(5, manager.TemplateValues.Count);
            Assert.AreEqual("CustomText", manager.TemplateValues[0].FileRenameValue.Name);
            Assert.AreEqual("CustomText", manager.TemplateValues[1].FileRenameValue.Name);
            Assert.AreEqual(" - ", manager.TemplateValues[1].ValueCore);
            Assert.AreEqual("Index", manager.TemplateValues[2].FileRenameValue.Name);
            Assert.AreEqual("CustomText", manager.TemplateValues[3].FileRenameValue.Name);
            Assert.AreEqual(" of ", manager.TemplateValues[3].ValueCore);
            Assert.AreEqual("Count", manager.TemplateValues[4].FileRenameValue.Name);
        }
        [TestMethod]
        public void TestGetFileName() {
            FileRenameManager manager = new FileRenameManager();
            manager.Template = "{FileName} - {Width}x{Height} - {Index} of {Count}.{Extension}";
            DmFile file = new DmFile();
            file.FileName = "MyPhoto.JPG";
            file.Width = 2000;
            file.Height = 1000;
            List<DmFile> list = new List<DmFile>();
            list.Add(new DmFile());
            list.Add(new DmFile());
            list.Add(new DmFile());
            list.Add(file);
            list.Add(new DmFile());
            string res = manager.GetFileName(list, file);
            Assert.AreEqual(0, manager.Errors.Count);
            Assert.AreEqual("MyPhoto - 2000x1000 - 4 of 5.JPG", res);
        }
        [TestMethod]
        public void TestSerialization() {
            SettingsStore store = SettingsStore.Default;
            SettingsStore.Default = new SettingsStore();

            ExportInfo info = new ExportInfo();
            info.FileRenameValues.Clear();

            FileRenameValueProperty prop = (FileRenameValueProperty)FileRenameManager.Default.GetFileRenameValue("Caption");
            FileRenameValueCustom custom = (FileRenameValueCustom)FileRenameManager.Default.GetFileRenameValue("CustomText");
            FileRenameValueIndex index = (FileRenameValueIndex)FileRenameManager.Default.GetFileRenameValue("Index");
            FileRenameValueCount count = (FileRenameValueCount)FileRenameManager.Default.GetFileRenameValue("Count");

            index.StartIndex = 99;

            FileRenameValueReference propRef = prop.CreateReference();
            FileRenameValueReferenceString propCustom = (FileRenameValueReferenceString)custom.CreateReference();
            FileRenameValueReference propIndex = index.CreateReference();
            FileRenameValueReference propCount = count.CreateReference();

            Assert.AreEqual("Caption", propRef.FileRenameValueName);
            Assert.AreEqual("CustomText", propCustom.FileRenameValueName);
            Assert.AreEqual("Index", propIndex.FileRenameValueName);
            Assert.AreEqual("Count", propCount.FileRenameValueName);

            propCustom.Text = "My Custom String";

            info.FileRenameValues.Add(propRef);
            info.FileRenameValues.Add(propCustom);
            info.FileRenameValues.Add(propIndex);
            info.FileRenameValues.Add(propCount);

            SettingsStore.Default.ExportPresets.Add(info);
            SettingsStore.Default.SaveToXml();

            SettingsStore.Default.ExportPresets.Clear();
            SettingsStore.Default.RestoreFromXml();

            Assert.AreEqual(1, SettingsStore.Default.ExportPresets.Count);
            ExportInfo info2 = SettingsStore.Default.ExportPresets[0];

            Assert.AreEqual(info.FileRenameValues.Count, info2.FileRenameValues.Count);
            for(int i = 0; i < info.FileRenameValues.Count; i++) {
                Assert.AreEqual(info.FileRenameValues[i].GetType(), info2.FileRenameValues[i].GetType());
                Assert.AreEqual(info.FileRenameValues[i].FileRenameValue.GetType(), info2.FileRenameValues[i].FileRenameValue.GetType());
            }
            SettingsStore.Default = store;
        }
    }
}
