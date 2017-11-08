using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoAssistant.Core.Model;
using PhotoAssistant.UI.View;

using PhotoAssistant.UI.ViewHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PhotoAssistant.Tests {
    [TestClass]
    public class ExportInfoTests {
        [TestMethod]
        public void TestSerialization() {
            SettingsStore store = SettingsStore.Default;
            SettingsStore.Default = new SettingsStore();

            ExportInfo info = new ExportInfo();

            info.AfterExportEvent = AfterExportEvent.OpenInApplication;

            ApplicationInfo appInfo = new ApplicationInfo();
            appInfo.CommandLine = "admin";
            appInfo.Id = Guid.NewGuid();
            appInfo.Name = "MyApplication";
            appInfo.Path = "c:\\MyApplication.exe";
            info.ApplicationIdString = appInfo.IdString;
            info.CreateSubFolder = true;
            info.DontEnlarge = true;
            info.Dpi = 300;
            info.ExistingFileMode = ExistingFileMode.OverrideWithoutPrompt;

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

            info.Folder = "c:\\MyFolder\\";
            info.Height = 1200;
            info.LongSide = 1300;
            info.Name = "MyExportInfo";
            info.RenameFiles = true;
            info.RenameMask = "[Index] - [Caption].[Extension]";
            info.ResizeImages = true;
            info.ResizeMode = FileResizeMode.ZoomInside;
            info.ResolutionMode = ResolutionMode.PixelsPerCm;
            info.ImageDimension = ImageDimensionMode.Inches;
            info.ShortSide = 1400;
            info.ShowWatermark = true;
            info.SubFolder = "SubFolder";
            info.Watermark.FontColor = System.Windows.Media.Colors.Red;
            info.Watermark.FontFamily = new System.Windows.Media.FontFamily("Arial");
            info.Watermark.FontFamilyName = "Arial";
            info.Watermark.FontSize = 13;
            info.Watermark.FontStyle = System.Windows.FontStyles.Italic;
            info.Watermark.FontStyleName = "Italic";
            info.Watermark.FontWeight = System.Windows.FontWeights.Bold;
            info.Watermark.FontWeightName = "Bold";
            info.Watermark.ImageToTextAlignment = WatermarkImageToTextAlign.Right;
            info.Watermark.ImageUri = "c:\\Images\\image.png";
            info.Watermark.Layout = WatermarkLayout.FillPhoto;
            info.Watermark.Opacity = 0.25;
            info.Watermark.RotateAngle = 75;
            info.Watermark.ShowWatermark = true;
            info.Watermark.Text = "Hello World";
            info.Watermark.WatermarkIndent = 5;
            info.Width = 1100;
            info.ImageFormat = ExportImageFormat.PNG;
            info.CompressionLevel = 95;
            info.IsLimitFileSize = true;
            info.LimitFileSize = 222;
            info.PngBitsPerChannel = 16;
            info.Application.Name = "AppName";
            info.Application.Path = "AppPath";
            info.Application.CommandLine = "AppLine";
            info.ApplicationIdString = Guid.NewGuid().ToString();

            SettingsStore.Default.ExportPresets.Add(info);
            SettingsStore.Default.SaveToXml();
            SettingsStore.Default.ExportPresets.Clear();
            SettingsStore.Default.RestoreFromXml();

            ExportInfo info2 = SettingsStore.Default.ExportPresets[0];
            Assert.AreEqual(info2.AfterExportEvent, info.AfterExportEvent);
            Assert.AreEqual(info2.ApplicationIdString, info.ApplicationIdString);
            Assert.AreEqual(info2.CreateSubFolder, info.CreateSubFolder);
            Assert.AreEqual(info2.DontEnlarge, info.DontEnlarge);
            Assert.AreEqual(info2.Dpi, info.Dpi);
            Assert.AreEqual(info2.ExistingFileMode, info.ExistingFileMode);

            for(int i = 0; i < info.FileRenameValues.Count; i++) {
                Assert.AreEqual(info.FileRenameValues[i].GetType(), info2.FileRenameValues[i].GetType());
                Assert.AreEqual(info.FileRenameValues[i].FileRenameValue.GetType(), info2.FileRenameValues[i].FileRenameValue.GetType());
            }

            Assert.AreEqual(info2.Folder, info.Folder);
            Assert.AreEqual(info2.Height, info.Height);
            Assert.AreEqual(info2.LongSide, info.LongSide);
            Assert.AreEqual(info2.Name, info.Name);
            Assert.AreEqual(info2.RenameFiles, info.RenameFiles);
            Assert.AreEqual(info2.RenameMask, info.RenameMask);
            Assert.AreEqual(info2.ResizeImages, info.ResizeImages);
            Assert.AreEqual(info2.ResizeMode, info.ResizeMode);
            Assert.AreEqual(info2.ResolutionMode, info.ResolutionMode);
            Assert.AreEqual(info2.ImageDimension, info.ImageDimension);
            Assert.AreEqual(info2.ShortSide, info.ShortSide);
            Assert.AreEqual(info2.ShowWatermark, info.ShowWatermark);
            Assert.AreEqual(info2.SubFolder, info.SubFolder);
            Assert.AreEqual(info2.Watermark.FontColor, info.Watermark.FontColor);
            Assert.AreEqual(info2.Watermark.FontFamily, info.Watermark.FontFamily);
            Assert.AreEqual(info2.Watermark.FontSize, info.Watermark.FontSize);
            Assert.AreEqual(info2.Watermark.FontStyle, info.Watermark.FontStyle);
            Assert.AreEqual(info2.Watermark.FontWeight, info.Watermark.FontWeight);
            Assert.AreEqual(info2.Watermark.ImageToTextAlignment, info.Watermark.ImageToTextAlignment);
            Assert.AreEqual(info2.Watermark.ImageUri, info.Watermark.ImageUri);
            Assert.AreEqual(info2.Watermark.Layout, info.Watermark.Layout);
            Assert.AreEqual(info2.Watermark.Opacity, info.Watermark.Opacity);
            Assert.AreEqual(info2.Watermark.RotateAngle, info.Watermark.RotateAngle);
            Assert.AreEqual(info2.Watermark.ShowWatermark, info.Watermark.ShowWatermark);
            Assert.AreEqual(info2.Watermark.Text, info.Watermark.Text);
            Assert.AreEqual(info2.Width, info.Width);
            Assert.AreEqual(info2.Watermark.WatermarkIndent, info.Watermark.WatermarkIndent);
            Assert.AreEqual(info2.ImageFormat, info.ImageFormat);
            Assert.AreEqual(info2.CompressionLevel, info.CompressionLevel);
            Assert.AreEqual(info2.IsLimitFileSize, info.IsLimitFileSize);
            Assert.AreEqual(info2.LimitFileSize, info.LimitFileSize);
            Assert.AreEqual(info2.PngBitsPerChannel, info.PngBitsPerChannel);
            Assert.AreEqual(info2.ApplicationIdString, info.ApplicationIdString);
            Assert.AreEqual(info2.Application.Name, info.Application.Name);
            Assert.AreEqual(info2.Application.Path, info.Application.Path);
            Assert.AreEqual(info2.Application.CommandLine, info.Application.CommandLine);
        }
        [TestMethod]
        public void TestSetParametersWhenThereIsNoFolder() {
            SettingsStore store = SettingsStore.Default;
            SettingsStore.Default = new SettingsStore();

            ExportInfo info = new ExportInfo();
            info.Folder = "c:\\TestFolder_NotExistInSettings\\";

            Assert.AreEqual(0, SettingsStore.Default.FoldersToExport.Count);

            ExportControl control = new ExportControl();
            control.OnShowView();
            control.ApplyExportInfo(info);

            Assert.AreEqual(1, SettingsStore.Default.FoldersToExport.Count);
            Assert.AreEqual(info.Folder, SettingsStore.Default.FoldersToExport[0].Path);

            store.FoldersToExport.Add(new PathInfo() { Path = "c:\\SomeFolder\\" });
            info.Folder = "c:\\AnotherTestFolder_NotExist";
            control.ApplyExportInfo(info);
            Assert.AreEqual(2, SettingsStore.Default.FoldersToExport.Count);
            Assert.AreEqual(info.Folder, SettingsStore.Default.FoldersToExport[1].Path);

            Assert.AreEqual(2, control.FoldersListCombo.Properties.Items.Count);
            Assert.AreEqual(true, control.FoldersListCombo.Properties.Items.Contains(SettingsStore.Default.FoldersToExport[0]));
            Assert.AreEqual(true, control.FoldersListCombo.Properties.Items.Contains(SettingsStore.Default.FoldersToExport[1]));
        }
        [TestMethod]
        public void TestSetParametersWhenThereIsNoRenameMask() {
            SettingsStore store = SettingsStore.Default;
            SettingsStore.Default = store;

            ExportInfo info = new ExportInfo();
            info.RenameMaskName = "MyMask";
            info.RenameMask = "{FileName} - {Width}x{Height} - {Index} of {Count}.{Extension}";
            FileRenameManager.Default.Template = info.RenameMask;

            foreach(FileRenameValueReference fref in FileRenameManager.Default.TemplateValues) {
                info.FileRenameValues.Add(fref);
            }

            int count = SettingsStore.Default.FileRenameTemplates.Count;
            ExportControl control = new ExportControl();
            control.OnShowView();
            control.ApplyExportInfo(info);

            Assert.AreEqual(count + 1, SettingsStore.Default.FileRenameTemplates.Count);
            Assert.AreEqual(info.RenameMaskName, SettingsStore.Default.FileRenameTemplates[count].Name);
            Assert.AreEqual(info.RenameMask, SettingsStore.Default.FileRenameTemplates[count].Template);

            Assert.AreEqual(count + 1, control.FileRenameMaskCombo.Properties.Items.Count);
            for(int i = 0; i < SettingsStore.Default.FileRenameTemplates.Count; i++) {
                Assert.AreEqual(SettingsStore.Default.FileRenameTemplates[i], control.FileRenameMaskCombo.Properties.Items[i]);
            }

            Assert.AreEqual(11, control.KeywordsListBox.Items.Count);
        }
        [TestMethod]
        public void TestAddApplicationWhenNoSuchApplication() {
            SettingsStore store = SettingsStore.Default;
            SettingsStore.Default = store;

            Assert.AreEqual(0, store.ExportApplications.Count);

            ExportInfo info = new ExportInfo();
            info.ApplicationIdString = Guid.NewGuid().ToString();
            info.Application.CommandLine = "test line";
            info.Application.Name = "tets name";
            info.Application.Path = "test path";
            ExportControl control = new ExportControl();
            control.OnShowView();
            control.ApplyExportInfo(info);

            Assert.AreEqual(1, store.ExportApplications.Count);
            Assert.AreEqual(info.Application.IdString, store.ExportApplications[0].IdString);
            Assert.AreEqual(1, control.ExportAppCombo.Properties.Items.Count);
            Assert.AreEqual(0, control.ExportAppCombo.SelectedIndex);
        }
        [TestMethod]
        public void TestExistingFileManager_IsFileExists() {
            FakeExistingFileManager manager = new FakeExistingFileManager();
            manager.ExistingCount = 2;

            string fileName = ((IExistingFileManager)manager).GenerateNewName("c:\\programmming\\photoassistant\\export\\photo.jpg");
            Assert.AreEqual("c:\\programmming\\photoassistant\\export\\photo_1.jpg", fileName);
        }
        protected string ExistingFileName => "c:\\programmming\\photoassistant\\export\\photo.jpg";
        [TestMethod]
        public void TestExistingFileManager_DoNotShowDialogIfChoiseRemembered() {
            FakeExistingFileManager manager = new FakeExistingFileManager();
            SettingsStore store = new SettingsStore();
            SettingsStore def = SettingsStore.Default;
            SettingsStore.Default = store;

            FakeExistingFileManager.RememberChoise = true;
            FakeExistingFileManager.ExistingFileMode = ExistingFileMode.OverrideWithoutPrompt;
            ExistingFileMode mode = ((IExistingFileManager)manager).AskUserForExistingFile(null, ExistingFileName);

            Assert.AreEqual(ExistingFileMode.OverrideWithoutPrompt, mode);

            SettingsStore.Default = def;
        }
    }
    class FakeExistingFileManager : ExistingFileManager {
        internal int ExistingCount {
            get; set;
        }
        protected override bool IsFileExists(string fileName) {
            ExistingCount--;
            if(ExistingCount <= 0) {
                return false;
            }

            return true;
        }
    }
}
