using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;


using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoAssistant.Core.Model;
using PhotoAssistant.Core;
using PhotoAssistant.UI.View;
using System.Data;
using System.Data.Common;

namespace PhotoAssistant.Tests {
    [TestClass]
    public class SysChecker {
        [TestMethod]
        public void ListFactories() {
            {
                // Retrieve the installed providers and factories.
                DataTable table = DbProviderFactories.GetFactoryClasses();

                // Display each row and column value.
                foreach (DataRow row in table.Rows) {
                    foreach (DataColumn column in table.Columns) {
                        Console.WriteLine(row[column]);
                    }

                }
            }
        }

    }
    //[TestClass]
    //public class ModelTests {

    //    protected DmModel Model { get; set; }
    //    protected SettingsStore TestSettings { get; set; }
    //    protected SettingsStore DefaultSettings { get; set; }

    //    string photoFile = "\\testPhoto-" + Guid.NewGuid() + ".png";
    //    protected string PhotoFile { get { return TestDirectory + photoFile; } }
    //    protected string PhotoFile2 { get { return TestDirectory2 + photoFile; } }

    //    protected string TestDirectory { get { return "c:\\temp\\PhotoAssistantTests"; } }
    //    protected string TestDirectory2 { get { return "c:\\temp\\PhotoAssistantTests2"; } }

    //    SettingsStore ApplyTestSettings() {
    //        SettingsStore testSettings = new SettingsStore();
    //        testSettings.CurrentDataSource = TestDirectory + "\\" + Guid.NewGuid() + ".phas";
    //        SettingsStore settings = SettingsStore.Default;
    //        SettingsStore.Default = testSettings;

    //        return settings;
    //    }

    //    [TestInitialize]
    //    public void TestInitialize() {
    //        DmModel.AllowGenerateDefaultTags = false;
    //        Model = new DmModel();
    //        //Model.UseInMemoryDatabase = true;
    //        DefaultSettings = SettingsStore.Default;
    //        TestSettings = ApplyTestSettings();
    //        string res = Model.ConnectToDataSource();
    //        Assert.AreEqual("", res);
    //        if(!File.Exists(PhotoFile)) {
    //            Bitmap bmp = new Bitmap(100, 100);
    //            bmp.Save(PhotoFile);
    //            bmp.Dispose();
    //        }
    //        if(!File.Exists(PhotoFile2)) {
    //            if(!Directory.Exists(TestDirectory2))
    //                Directory.CreateDirectory(TestDirectory2);
    //            Bitmap bmp = new Bitmap(100, 100);
    //            bmp.Save(PhotoFile2);
    //            bmp.Dispose();
    //        }
    //    }

    //    [TestCleanup]
    //    public void TestCleanup() {
    //        Model.CloseConnection();
    //        SettingsStore.Default = DefaultSettings;
    //    }

    //    [TestMethod]
    //    public void TestFiltersDefault() {
    //        Model.GetVisibleFilters();
    //        Assert.AreEqual(0, Model.GetFilter(FilterType.LastImported).MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestColorLabelFiltersHasValues() {
    //        IEnumerable<Filter> filters = Model.GetVisibleFilters();
    //        foreach(Filter filter in filters) {
    //            if(filter.Text == DmColorLabel.NoneString)
    //                Assert.AreEqual(null, filter.Value);
    //            else if(filter.Type == FilterType.ColorLabel) {
    //                Assert.IsNotNull(filter.Value);
    //                Assert.AreEqual(((DmColorLabel)filter.Value).Text, filter.Text);
    //            }
    //        }
    //    }

    //    [TestMethod]
    //    public void TestMediaFormatFiltersHasValues() {
    //        IEnumerable<Filter> filters = Model.GetVisibleFilters();
    //        foreach(Filter filter in filters) {
    //            if(filter.Type == FilterType.MediaFormatType) {
    //                Assert.AreNotEqual(MediaType.Unknown, filter.Value);
    //                Assert.AreEqual(((MediaType)filter.Value).ToString(), filter.Text);
    //            }
    //            if(filter.Type == FilterType.MediaFormat) {
    //                Assert.IsNotNull(filter.Value);
    //                Assert.AreEqual(((MediaFormat)filter.Value).Text, filter.Text);
    //            }
    //        }
    //    }

    //    DmFile GetTestPhotoModel() { return Model.Helper.AddFileHelper.CreateFileInfoModel(new FileInfo(PhotoFile)); }
    //    DmFile GetTestPhotoModel2() { return Model.Helper.AddFileHelper.CreateFileInfoModel(new FileInfo(PhotoFile2)); }

    //    [TestMethod]
    //    public void TestSavingDataInMemory() {
    //        Model.AddTag("Arsen", TagType.Tag);
    //        Model.Context.SaveChanges();
    //    }

    //    [TestMethod]
    //    public void TestFilterWhenFileAdded() {
    //        DmFile file = GetTestPhotoModel();
    //        file.CreationDate = new DateTime(1982, 1, 23);
    //        DateTime yesterday = DateTime.Now.AddDays(-1).Date;
    //        file.ImportDate = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.LastImported).MatchedCount);
    //        Assert.AreEqual(0, Model.GetFilter(FilterType.ImportedToday).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.ImportedYesterday).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.ImportedLastWeek).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.ImportedLastMonth).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.FolderHeader).Children.Count);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Folder, TestDirectory).MatchedCount);

    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Rating, 0).MatchedCount);

    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children.Count);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children[0].MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children[0].Children.Count);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children[0].Children[0].Children.Count);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children[0].Children[0].Children[0].MatchedCount);

    //        Assert.AreEqual(null, file.ColorLabel);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.ColorLabel, file.ColorLabel).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.MediaFormatType, (int)file.MediaFormat.Type).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.MediaFormat, file.MediaFormat).MatchedCount);

    //        Filter header = Model.GetFilter(FilterType.CreationDateTimeHeader);

    //        Assert.AreEqual(1, header.Children.Count);
    //        Assert.AreEqual(1, header.Children[0].MatchedCount);
    //        Assert.AreEqual(file.CreationDate.Year, header.Children[0].Value);
    //        Assert.AreEqual(1, header.Children[0].Children.Count);
    //        Assert.AreEqual(1, header.Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(file.CreationDate.Month, header.Children[0].Children[0].Value);
    //        Assert.AreEqual(1, header.Children[0].Children[0].Children.Count);
    //        Assert.AreEqual(1, header.Children[0].Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(file.CreationDate.Day, header.Children[0].Children[0].Children[0].Value);

    //        header = Model.GetFilter(FilterType.ImportDateTimeHeader);

    //        Assert.AreEqual(1, header.Children.Count);
    //        Assert.AreEqual(1, header.Children[0].MatchedCount);
    //        Assert.AreEqual(file.ImportDate.Year, header.Children[0].Value);
    //        Assert.AreEqual(1, header.Children[0].Children.Count);
    //        Assert.AreEqual(1, header.Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(file.ImportDate.Month, header.Children[0].Children[0].Value);
    //        Assert.AreEqual(1, header.Children[0].Children[0].Children.Count);
    //        Assert.AreEqual(1, header.Children[0].Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(file.ImportDate.Day, header.Children[0].Children[0].Children[0].Value);

    //        DmFile firstFile = file;
    //        Model.BeginAddFiles();
    //        file = GetTestPhotoModel();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(1, Model.GetFilter(FilterType.LastImported).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.ImportedToday).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.ImportedYesterday).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.ImportedLastWeek).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.ImportedLastMonth).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.FolderHeader).Children.Count);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.Folder, TestDirectory).MatchedCount);

    //        Assert.AreEqual(2, Model.GetFilter(FilterType.Rating, 0).MatchedCount);

    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children.Count);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.Place).Children[0].MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children[0].Children.Count);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.Place).Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children[0].Children[0].Children.Count);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.Place).Children[0].Children[0].Children[0].MatchedCount);

    //        Assert.AreEqual(2, Model.GetFilter(FilterType.ColorLabel, file.ColorLabel).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.MediaFormatType, (int)file.MediaFormat.Type).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.MediaFormat, file.MediaFormat).MatchedCount);

    //        header = Model.GetFilter(FilterType.CreationDateTimeHeader);
    //        Assert.AreEqual(2, header.Children.Count);

    //        Assert.AreEqual(1, header.Children[0].MatchedCount);
    //        Assert.AreEqual(firstFile.CreationDate.Year, header.Children[0].Value);
    //        Assert.AreEqual(1, header.Children[0].Children.Count);
    //        Assert.AreEqual(1, header.Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(firstFile.CreationDate.Month, header.Children[0].Children[0].Value);
    //        Assert.AreEqual(1, header.Children[0].Children[0].Children.Count);
    //        Assert.AreEqual(1, header.Children[0].Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(firstFile.CreationDate.Day, header.Children[0].Children[0].Children[0].Value);

    //        Assert.AreEqual(1, header.Children[1].MatchedCount);
    //        Assert.AreEqual(file.CreationDate.Year, header.Children[1].Value);
    //        Assert.AreEqual(1, header.Children[1].Children.Count);
    //        Assert.AreEqual(1, header.Children[1].Children[0].MatchedCount);
    //        Assert.AreEqual(file.CreationDate.Month, header.Children[1].Children[0].Value);
    //        Assert.AreEqual(1, header.Children[1].Children[0].Children.Count);
    //        Assert.AreEqual(1, header.Children[1].Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(file.CreationDate.Day, header.Children[1].Children[0].Children[0].Value);

    //        header = Model.GetFilter(FilterType.ImportDateTimeHeader);
    //        Assert.AreEqual(1, header.Children.Count);
    //        Assert.AreEqual(2, header.Children[0].MatchedCount);
    //        Assert.AreEqual(firstFile.ImportDate.Year, header.Children[0].Value);
    //        Assert.AreEqual(1, header.Children[0].Children.Count);
    //        Assert.AreEqual(2, header.Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(firstFile.ImportDate.Month, header.Children[0].Children[0].Value);
    //        Assert.AreEqual(2, header.Children[0].Children[0].Children.Count);
    //        Assert.AreEqual(1, header.Children[0].Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(1, header.Children[0].Children[0].Children[1].MatchedCount);
    //        Assert.AreEqual(firstFile.ImportDate.Day, header.Children[0].Children[0].Children[0].Value);
    //        Assert.AreEqual(file.ImportDate.Day, header.Children[0].Children[0].Children[1].Value);

    //        Model.BeginAddFiles();
    //        file = GetTestPhotoModel2();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(1, Model.GetFilter(FilterType.LastImported).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.ImportedToday).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.ImportedYesterday).MatchedCount);
    //        Assert.AreEqual(3, Model.GetFilter(FilterType.ImportedLastWeek).MatchedCount);
    //        Assert.AreEqual(3, Model.GetFilter(FilterType.ImportedLastMonth).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.FolderHeader).Children.Count);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.Folder, TestDirectory).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Folder, TestDirectory2).MatchedCount);

    //        Assert.AreEqual(3, Model.GetFilter(FilterType.Rating, 0).MatchedCount);

    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children.Count);
    //        Assert.AreEqual(3, Model.GetFilter(FilterType.Place).Children[0].MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children[0].Children.Count);
    //        Assert.AreEqual(3, Model.GetFilter(FilterType.Place).Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Place).Children[0].Children[0].Children.Count);
    //        Assert.AreEqual(3, Model.GetFilter(FilterType.Place).Children[0].Children[0].Children[0].MatchedCount);

    //        Assert.AreEqual(3, Model.GetFilter(FilterType.ColorLabel, file.ColorLabel).MatchedCount);
    //        Assert.AreEqual(3, Model.GetFilter(FilterType.MediaFormatType, (int)file.MediaFormat.Type).MatchedCount);
    //        Assert.AreEqual(3, Model.GetFilter(FilterType.MediaFormat, file.MediaFormat).MatchedCount);

    //        header = Model.GetFilter(FilterType.CreationDateTimeHeader);
    //        Assert.AreEqual(2, header.Children[1].MatchedCount);
    //        Assert.AreEqual(file.CreationDate.Year, header.Children[1].Value);
    //        Assert.AreEqual(1, header.Children[1].Children.Count);
    //        Assert.AreEqual(2, header.Children[1].Children[0].MatchedCount);
    //        Assert.AreEqual(file.CreationDate.Month, header.Children[1].Children[0].Value);
    //        Assert.AreEqual(1, header.Children[1].Children[0].Children.Count);
    //        Assert.AreEqual(2, header.Children[1].Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(file.CreationDate.Day, header.Children[1].Children[0].Children[0].Value);

    //        header = Model.GetFilter(FilterType.ImportDateTimeHeader);
    //        Assert.AreEqual(1, header.Children.Count);
    //        Assert.AreEqual(3, header.Children[0].MatchedCount);
    //        Assert.AreEqual(firstFile.ImportDate.Year, header.Children[0].Value);
    //        Assert.AreEqual(1, header.Children[0].Children.Count);
    //        Assert.AreEqual(3, header.Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(firstFile.ImportDate.Month, header.Children[0].Children[0].Value);
    //        Assert.AreEqual(2, header.Children[0].Children[0].Children.Count);
    //        Assert.AreEqual(1, header.Children[0].Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(2, header.Children[0].Children[0].Children[1].MatchedCount);
    //        Assert.AreEqual(firstFile.ImportDate.Day, header.Children[0].Children[0].Children[0].Value);
    //        Assert.AreEqual(file.ImportDate.Day, header.Children[0].Children[0].Children[1].Value);
    //    }

    //    [TestMethod]
    //    public void TestMarkFilterWhenFileAdd() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        file.Marked = true;
    //        file2.Marked = true;

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(2, Model.MarkHeaderFilter.Children.Count);

    //        Filter marked = Model.GetFilter(Model.MarkHeaderFilter, true);
    //        Filter unmarked = Model.GetFilter(Model.MarkHeaderFilter, false);

    //        Assert.AreEqual(2, marked.MatchedCount);
    //        Assert.AreEqual(1, unmarked.MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestMarkFilterWhenFileUpdated() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        file.Marked = true;
    //        file2.Marked = true;

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Model.BeginUpdateFile(file);
    //        file.Marked = false;
    //        Model.EndUpdateFile(file);

    //        Filter marked = Model.GetFilter(Model.MarkHeaderFilter, true);
    //        Filter unmarked = Model.GetFilter(Model.MarkHeaderFilter, false);

    //        Assert.AreEqual(1, marked.MatchedCount);
    //        Assert.AreEqual(2, unmarked.MatchedCount);
    //        Assert.AreEqual(2, Model.MarkHeaderFilter.Children.Count);

    //        Model.BeginUpdateFile(file2);
    //        file2.Marked = false;
    //        Model.EndUpdateFile(file2);
    //        Assert.AreEqual(2, Model.MarkHeaderFilter.Children.Count);

    //        Assert.AreEqual(0, marked.MatchedCount);
    //        Assert.AreEqual(3, unmarked.MatchedCount);
    //        Assert.AreEqual(2, Model.MarkHeaderFilter.Children.Count);
    //    }

    //    [TestMethod]
    //    public void TestMarkFilterWhenFileRemoved() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        file.Marked = true;
    //        file2.Marked = true;

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Filter marked = Model.GetFilter(Model.MarkHeaderFilter, true);
    //        Filter unmarked = Model.GetFilter(Model.MarkHeaderFilter, false);

    //        Assert.AreEqual(2, marked.MatchedCount);
    //        Assert.AreEqual(1, unmarked.MatchedCount);

    //        Model.RemoveFile(file);

    //        Assert.AreEqual(1, marked.MatchedCount);
    //        Assert.AreEqual(1, unmarked.MatchedCount);
    //        Assert.AreEqual(2, Model.MarkHeaderFilter.Children.Count);

    //        Model.RemoveFile(file2);
    //        Assert.AreEqual(0, marked.MatchedCount);
    //        Assert.AreEqual(1, unmarked.MatchedCount);
    //        Assert.AreEqual(2, Model.MarkHeaderFilter.Children.Count);

    //        Model.RemoveFile(file3);
    //        Assert.AreEqual(0, marked.MatchedCount);
    //        Assert.AreEqual(0, unmarked.MatchedCount);
    //        Assert.AreEqual(2, Model.MarkHeaderFilter.Children.Count);
    //    }

    //    [TestMethod]
    //    public void TestEventFilterWhenAdd() {
    //        DmFile file = GetTestPhotoModel();
    //        file.Event = "Arsen Birthday";

    //        DmFile file2 = GetTestPhotoModel();
    //        file2.Event = "Arsen Birthday";

    //        DmFile file3 = GetTestPhotoModel();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Filter header = Model.GetFilter(FilterType.EventHeader);
    //        Assert.AreEqual(2, header.Children.Count);
    //        Assert.AreEqual(Filter.UnassignedString, header.Children[0].Text);
    //        Assert.AreEqual(1, header.Children[0].MatchedCount);
    //        Assert.AreEqual(2, header.Children[1].MatchedCount);
    //        Assert.AreEqual(file.Event, header.Children[1].Value);
    //    }

    //    [TestMethod]
    //    public void TestAddKeywordsToFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Tag);
    //        Model.AddTag("Arina", TagType.Tag);
    //        Model.AddTag("Artem", TagType.Tag);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Tag);
    //        Assert.AreEqual(2, file.Keywords.Count);
    //        foreach(DmKeyword keyword in file.Keywords) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[0] || keyword.Tag == Model.Context.Tags.Local[2]);
    //        }
    //    }

    //    [TestMethod]
    //    public void TestRemoveKeywordsFromFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Tag);
    //        Model.AddTag("Arina", TagType.Tag);
    //        Model.AddTag("Artem", TagType.Tag);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Tag);
    //        Assert.AreEqual(2, file.Keywords.Count);
    //        foreach(DmKeyword keyword in file.Keywords) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[0] || keyword.Tag == Model.Context.Tags.Local[2]);
    //        }

    //        List<DmTag> tagsToRemove = new List<DmTag>();
    //        tagsToRemove.Add(tags[0]);
    //        Model.RemoveKeywords(file, tagsToRemove, TagType.Tag);
    //        int index = 0;
    //        foreach(DmKeyword keyword in file.Keywords) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[2]);
    //            index++;
    //        }
    //        Assert.AreEqual(1, index);
    //    }

    //    [TestMethod]
    //    public void TestAddCategoriesToFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Category);
    //        Model.AddTag("Arina", TagType.Category);
    //        Model.AddTag("Artem", TagType.Category);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Category);
    //        Assert.AreEqual(2, file.Categories.Count);
    //        foreach(DmCategory cat in file.Categories) {
    //            Assert.AreEqual(true, cat.Tag == Model.Context.Tags.Local[0] || cat.Tag == Model.Context.Tags.Local[2]);
    //        }
    //    }

    //    [TestMethod]
    //    public void TestRemoveCategoriesFromFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Category);
    //        Model.AddTag("Arina", TagType.Category);
    //        Model.AddTag("Artem", TagType.Category);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Category);
    //        Assert.AreEqual(2, file.Categories.Count);
    //        foreach(DmCategory keyword in file.Categories) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[0] || keyword.Tag == Model.Context.Tags.Local[2]);
    //        }

    //        List<DmTag> tagsToRemove = new List<DmTag>();
    //        tagsToRemove.Add(tags[0]);
    //        Model.RemoveKeywords(file, tagsToRemove, TagType.Category);
    //        int index = 0;
    //        foreach(DmCategory keyword in file.Categories) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[2]);
    //            index++;
    //        }
    //        Assert.AreEqual(1, index);
    //    }

    //    [TestMethod]
    //    public void TestAddGenresToFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Genre);
    //        Model.AddTag("Arina", TagType.Genre);
    //        Model.AddTag("Artem", TagType.Genre);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Genre);
    //        Assert.AreEqual(2, file.Genres.Count);
    //        foreach(DmGenre genre in file.Genres) {
    //            Assert.AreEqual(true, genre.Tag == Model.Context.Tags.Local[0] || genre.Tag == Model.Context.Tags.Local[2]);
    //        }
    //    }

    //    [TestMethod]
    //    public void TestRemoveGenresFromFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Genre);
    //        Model.AddTag("Arina", TagType.Genre);
    //        Model.AddTag("Artem", TagType.Genre);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Genre);
    //        Assert.AreEqual(2, file.Genres.Count);
    //        foreach(DmGenre keyword in file.Genres) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[0] || keyword.Tag == Model.Context.Tags.Local[2]);
    //        }

    //        List<DmTag> tagsToRemove = new List<DmTag>();
    //        tagsToRemove.Add(tags[0]);
    //        Model.RemoveKeywords(file, tagsToRemove, TagType.Genre);
    //        int index = 0;
    //        foreach(DmGenre keyword in file.Genres) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[2]);
    //            index++;
    //        }
    //        Assert.AreEqual(1, index);
    //    }

    //    [TestMethod]
    //    public void TestAddAutorsToFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Autor);
    //        Model.AddTag("Arina", TagType.Autor);
    //        Model.AddTag("Artem", TagType.Autor);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Autor);
    //        Assert.AreEqual(2, file.Autors.Count);
    //        foreach(DmAutor autor in file.Autors) {
    //            Assert.AreEqual(true, autor.Tag == Model.Context.Tags.Local[0] || autor.Tag == Model.Context.Tags.Local[2]);
    //        }
    //    }

    //    [TestMethod]
    //    public void TestRemoveAutorsFromFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Autor);
    //        Model.AddTag("Arina", TagType.Autor);
    //        Model.AddTag("Artem", TagType.Autor);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Autor);
    //        Assert.AreEqual(2, file.Autors.Count);
    //        foreach(DmAutor keyword in file.Autors) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[0] || keyword.Tag == Model.Context.Tags.Local[2]);
    //        }

    //        List<DmTag> tagsToRemove = new List<DmTag>();
    //        tagsToRemove.Add(tags[0]);
    //        Model.RemoveKeywords(file, tagsToRemove, TagType.Autor);
    //        int index = 0;
    //        foreach(DmAutor keyword in file.Autors) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[2]);
    //            index++;
    //        }
    //        Assert.AreEqual(1, index);
    //    }

    //    [TestMethod]
    //    public void TestAddPeopleToFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.People);
    //        Model.AddTag("Arina", TagType.People);
    //        Model.AddTag("Artem", TagType.People);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.People);
    //        Assert.AreEqual(2, file.Peoples.Count);
    //        foreach(DmPeople people in file.Peoples) {
    //            Assert.AreEqual(true, people.Tag == Model.Context.Tags.Local[0] || people.Tag == Model.Context.Tags.Local[2]);
    //        }
    //    }

    //    [TestMethod]
    //    public void TestRemovePeoplesFromFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.People);
    //        Model.AddTag("Arina", TagType.People);
    //        Model.AddTag("Artem", TagType.People);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.People);
    //        Assert.AreEqual(2, file.Peoples.Count);
    //        foreach(DmPeople keyword in file.Peoples) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[0] || keyword.Tag == Model.Context.Tags.Local[2]);
    //        }

    //        List<DmTag> tagsToRemove = new List<DmTag>();
    //        tagsToRemove.Add(tags[0]);
    //        Model.RemoveKeywords(file, tagsToRemove, TagType.People);
    //        int index = 0;
    //        foreach(DmPeople keyword in file.Peoples) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[2]);
    //            index++;
    //        }
    //        Assert.AreEqual(1, index);
    //    }

    //    [TestMethod]
    //    public void TestAddCollectionToFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Collection);
    //        Model.AddTag("Arina", TagType.Collection);
    //        Model.AddTag("Artem", TagType.Collection);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Collection);
    //        Assert.AreEqual(2, file.Collections.Count);
    //        foreach(DmCollection coll in file.Collections) {
    //            Assert.AreEqual(true, coll.Tag == Model.Context.Tags.Local[0] || coll.Tag == Model.Context.Tags.Local[2]);
    //        }
    //    }

    //    [TestMethod]
    //    public void TestRemoveCollectionFromFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Collection);
    //        Model.AddTag("Arina", TagType.Collection);
    //        Model.AddTag("Artem", TagType.Collection);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Collection);
    //        Assert.AreEqual(2, file.Collections.Count);
    //        foreach(DmCollection keyword in file.Collections) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[0] || keyword.Tag == Model.Context.Tags.Local[2]);
    //        }

    //        List<DmTag> tagsToRemove = new List<DmTag>();
    //        tagsToRemove.Add(tags[0]);
    //        Model.RemoveKeywords(file, tagsToRemove, TagType.Collection);
    //        int index = 0;
    //        foreach(DmCollection keyword in file.Collections) {
    //            Assert.AreEqual(true, keyword.Tag == Model.Context.Tags.Local[2]);
    //            index++;
    //        }
    //        Assert.AreEqual(1, index);
    //    }

    //    [TestMethod]
    //    public void TestPeopleFilterWhenAddFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.People);
    //        Model.AddTag("Arina", TagType.People);
    //        Model.AddTag("Artem", TagType.People);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.People);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Filter filter = Model.GetFilter(FilterType.PeopleHeader);
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(0, filter.Children[0].MatchedCount);
    //        Assert.AreEqual(Filter.UnassignedString, filter.Children[0].Text);
    //        Assert.AreEqual(null, filter.Children[0].Value);
    //        Assert.AreEqual(true, filter.Children[0].IsSystem);
    //        Assert.AreEqual(1, filter.Children[1].MatchedCount);
    //        Assert.AreEqual(tags[0], filter.Children[1].Value);
    //        Assert.AreEqual(false, filter.Children[1].IsSystem);
    //        Assert.AreEqual(1, filter.Children[2].MatchedCount);
    //        Assert.AreEqual(tags[1], filter.Children[2].Value);
    //        Assert.AreEqual(false, filter.Children[2].IsSystem);
    //        Assert.AreEqual(1, filter.Children[3].MatchedCount);
    //        Assert.AreEqual(false, filter.Children[3].IsSystem);
    //        Assert.AreEqual(tags[2], filter.Children[3].Value);

    //        file = GetTestPhotoModel();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(1, filter.Children[0].MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestTagsFilterWhenAddFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Tag);
    //        Model.AddTag("Arina", TagType.Tag);
    //        Model.AddTag("Artem", TagType.Tag);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Tag);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Filter filter = Model.GetFilter(FilterType.TagsHeader);
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(0, filter.Children[0].MatchedCount);
    //        Assert.AreEqual(Filter.UnassignedString, filter.Children[0].Text);
    //        Assert.AreEqual(null, filter.Children[0].Value);
    //        Assert.AreEqual(true, filter.Children[0].IsSystem);
    //        Assert.AreEqual(1, filter.Children[1].MatchedCount);
    //        Assert.AreEqual(tags[0], filter.Children[1].Value);
    //        Assert.AreEqual(false, filter.Children[1].IsSystem);
    //        Assert.AreEqual(1, filter.Children[2].MatchedCount);
    //        Assert.AreEqual(tags[1], filter.Children[2].Value);
    //        Assert.AreEqual(false, filter.Children[2].IsSystem);
    //        Assert.AreEqual(1, filter.Children[3].MatchedCount);
    //        Assert.AreEqual(false, filter.Children[3].IsSystem);
    //        Assert.AreEqual(tags[2], filter.Children[3].Value);

    //        file = GetTestPhotoModel();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(1, filter.Children[0].MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestAutorsFilterWhenAddFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Autor);
    //        Model.AddTag("Arina", TagType.Autor);
    //        Model.AddTag("Artem", TagType.Autor);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Autor);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Filter filter = Model.GetFilter(FilterType.AutorsHeader);
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(0, filter.Children[0].MatchedCount);
    //        Assert.AreEqual(Filter.UnassignedString, filter.Children[0].Text);
    //        Assert.AreEqual(null, filter.Children[0].Value);
    //        Assert.AreEqual(true, filter.Children[0].IsSystem);
    //        Assert.AreEqual(1, filter.Children[1].MatchedCount);
    //        Assert.AreEqual(tags[0], filter.Children[1].Value);
    //        Assert.AreEqual(false, filter.Children[1].IsSystem);
    //        Assert.AreEqual(1, filter.Children[2].MatchedCount);
    //        Assert.AreEqual(tags[1], filter.Children[2].Value);
    //        Assert.AreEqual(false, filter.Children[2].IsSystem);
    //        Assert.AreEqual(1, filter.Children[3].MatchedCount);
    //        Assert.AreEqual(false, filter.Children[3].IsSystem);
    //        Assert.AreEqual(tags[2], filter.Children[3].Value);

    //        file = GetTestPhotoModel();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(1, filter.Children[0].MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestGenresFilterWhenAddFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Genre);
    //        Model.AddTag("Arina", TagType.Genre);
    //        Model.AddTag("Artem", TagType.Genre);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Genre);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Filter filter = Model.GetFilter(FilterType.GenresHeader);
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(0, filter.Children[0].MatchedCount);
    //        Assert.AreEqual(Filter.UnassignedString, filter.Children[0].Text);
    //        Assert.AreEqual(null, filter.Children[0].Value);
    //        Assert.AreEqual(true, filter.Children[0].IsSystem);
    //        Assert.AreEqual(1, filter.Children[1].MatchedCount);
    //        Assert.AreEqual(tags[0], filter.Children[1].Value);
    //        Assert.AreEqual(false, filter.Children[1].IsSystem);
    //        Assert.AreEqual(1, filter.Children[2].MatchedCount);
    //        Assert.AreEqual(tags[1], filter.Children[2].Value);
    //        Assert.AreEqual(false, filter.Children[2].IsSystem);
    //        Assert.AreEqual(1, filter.Children[3].MatchedCount);
    //        Assert.AreEqual(false, filter.Children[3].IsSystem);
    //        Assert.AreEqual(tags[2], filter.Children[3].Value);

    //        file = GetTestPhotoModel();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(1, filter.Children[0].MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestCollectionsFilterWhenAddFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Collection);
    //        Model.AddTag("Arina", TagType.Collection);
    //        Model.AddTag("Artem", TagType.Collection);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Collection);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Filter filter = Model.GetFilter(FilterType.CollectionsHeader);
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(0, filter.Children[0].MatchedCount);
    //        Assert.AreEqual(Filter.UnassignedString, filter.Children[0].Text);
    //        Assert.AreEqual(null, filter.Children[0].Value);
    //        Assert.AreEqual(true, filter.Children[0].IsSystem);
    //        Assert.AreEqual(1, filter.Children[1].MatchedCount);
    //        Assert.AreEqual(tags[0], filter.Children[1].Value);
    //        Assert.AreEqual(false, filter.Children[1].IsSystem);
    //        Assert.AreEqual(1, filter.Children[2].MatchedCount);
    //        Assert.AreEqual(tags[1], filter.Children[2].Value);
    //        Assert.AreEqual(false, filter.Children[2].IsSystem);
    //        Assert.AreEqual(1, filter.Children[3].MatchedCount);
    //        Assert.AreEqual(false, filter.Children[3].IsSystem);
    //        Assert.AreEqual(tags[2], filter.Children[3].Value);

    //        file = GetTestPhotoModel();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(1, filter.Children[0].MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestCategoriesFilterWhenAddFile() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.Category);
    //        Model.AddTag("Arina", TagType.Category);
    //        Model.AddTag("Artem", TagType.Category);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.Category);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Filter filter = Model.GetFilter(FilterType.CategoriesHeader);
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(0, filter.Children[0].MatchedCount);
    //        Assert.AreEqual(Filter.UnassignedString, filter.Children[0].Text);
    //        Assert.AreEqual(null, filter.Children[0].Value);
    //        Assert.AreEqual(true, filter.Children[0].IsSystem);
    //        Assert.AreEqual(1, filter.Children[1].MatchedCount);
    //        Assert.AreEqual(tags[0], filter.Children[1].Value);
    //        Assert.AreEqual(false, filter.Children[1].IsSystem);
    //        Assert.AreEqual(1, filter.Children[2].MatchedCount);
    //        Assert.AreEqual(tags[1], filter.Children[2].Value);
    //        Assert.AreEqual(false, filter.Children[2].IsSystem);
    //        Assert.AreEqual(1, filter.Children[3].MatchedCount);
    //        Assert.AreEqual(false, filter.Children[3].IsSystem);
    //        Assert.AreEqual(tags[2], filter.Children[3].Value);

    //        file = GetTestPhotoModel();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(1, filter.Children[0].MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated() {
    //        DmFile file = GetTestPhotoModel();
    //        file.CreationDate = new DateTime(1982, 1, 23);
    //        DateTime yesterday = DateTime.Now.AddDays(-1).Date;
    //        file.ImportDate = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);

    //        DmFile file4 = GetTestPhotoModel();
    //        file4.CreationDate = new DateTime(1982, 1, 23);
    //        file4.ImportDate = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);

    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(4, Model.GetFilter(FilterType.Rating, 0).MatchedCount);
    //        Model.BeginUpdateFile(file);
    //        file.Rating = 1;
    //        Model.EndUpdateFile(file);
    //        Assert.AreEqual(3, Model.GetFilter(FilterType.Rating, 0).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Rating, 1).MatchedCount);

    //        Model.BeginUpdateFile(file);
    //        Assert.AreEqual(null, file.ColorLabel);
    //        file.ColorLabel = (DmColorLabel)Model.GetFilter(FilterType.ColorLabelHeader).Children[0].Value;
    //        Model.EndUpdateFile(file);
    //        Assert.AreEqual(3, Model.GetFilter(FilterType.ColorLabel, (DmColorLabel)null).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.ColorLabel, file.ColorLabel).MatchedCount);

    //        Filter crHeader = Model.GetFilter(FilterType.CreationDateTimeHeader);
    //        Filter yearFilter = crHeader.Children.Find((f) => (int)f.Value == 1982);

    //        Model.BeginUpdateFile(file);
    //        file.CreationDate = new DateTime(1989, 10, 27);
    //        Model.EndUpdateFile(file);

    //        yearFilter = crHeader.Children.Find((f) => (int)f.Value == 1982);

    //        Assert.AreEqual(1, yearFilter.MatchedCount);
    //        Assert.AreEqual(1, yearFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(1, yearFilter.Children[0].Children[0].MatchedCount);

    //        yearFilter = crHeader.Children.Find((f) => (int)f.Value == 1989);
    //        Assert.AreEqual(1, yearFilter.MatchedCount);
    //        Assert.AreEqual(1, yearFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(1, yearFilter.Children[0].Children[0].MatchedCount);

    //        Model.BeginUpdateFile(file4);
    //        file4.CreationDate = new DateTime(1979, 12, 29);
    //        Model.EndUpdateFile(file4);

    //        Assert.AreEqual(null, Model.GetFilter(FilterType.CreationYear, 1982));
    //        Assert.AreEqual(null, Model.GetFilter(FilterType.CreationMonth, 1));
    //        Assert.AreEqual(null, Model.GetFilter(FilterType.CreationDay, 23));

    //        yearFilter = crHeader.Children.Find((f) => (int)f.Value == 1989);
    //        Assert.AreEqual(1, yearFilter.MatchedCount);
    //        Assert.AreEqual(1, yearFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(1, yearFilter.Children[0].Children[0].MatchedCount);

    //        yearFilter = crHeader.Children.Find((f) => (int)f.Value == 1979);
    //        Assert.AreEqual(1, yearFilter.MatchedCount);
    //        Assert.AreEqual(1, yearFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(1, yearFilter.Children[0].Children[0].MatchedCount);

    //        yearFilter = crHeader.Children.Find((f) => (int)f.Value == 2014);
    //        Assert.AreEqual(2, yearFilter.MatchedCount);
    //        Assert.AreEqual(2, yearFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(2, yearFilter.Children[0].Children[0].MatchedCount);

    //        Model.BeginUpdateFile(file2);
    //        file2.CreationDate = new DateTime(file2.CreationDate.Year, file2.CreationDate.Month, file2.CreationDate.Day > 15 ? 14 : 16);
    //        Model.EndUpdateFile(file2);

    //        yearFilter = crHeader.Children.Find((f) => (int)f.Value == 2014);
    //        Assert.AreEqual(2, yearFilter.MatchedCount);
    //        Assert.AreEqual(2, yearFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(1, yearFilter.Children[0].Children[0].MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestFilterWhenFileUpdatedPlace() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();
    //        DmFile file4 = GetTestPhotoModel();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.EndAddFiles();

    //        Filter placeHeader = Model.GetFilter(FilterType.Place);
    //        Assert.AreEqual(1, placeHeader.Children.Count);
    //        Assert.AreEqual(4, placeHeader.Children[0].MatchedCount);

    //        Model.BeginUpdateFile(file);
    //        file.Country = "Russia";
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(2, placeHeader.Children.Count);
    //        Assert.AreEqual(3, placeHeader.Children[0].MatchedCount);
    //        Assert.AreEqual(1, placeHeader.Children[1].MatchedCount);
    //        Assert.AreEqual(1, placeHeader.Children[1].Children.Count);
    //        Assert.AreEqual(1, placeHeader.Children[1].Children[0].MatchedCount);
    //        Assert.AreEqual(1, placeHeader.Children[1].Children[0].Children.Count);
    //        Assert.AreEqual(1, placeHeader.Children[1].Children[0].Children[0].MatchedCount);
    //        Assert.AreEqual(1, placeHeader.Children[1].Children[0].Children[0].Children.Count);
    //        Assert.AreEqual(1, placeHeader.Children[1].Children[0].Children[0].Children[0].MatchedCount);

    //        Model.BeginUpdateFile(file2);
    //        file2.State = "Tulskaya obl";
    //        Model.EndUpdateFile(file2);

    //        Assert.AreEqual(2, placeHeader.Children.Count);
    //        Assert.AreEqual(3, placeHeader.Children[0].MatchedCount);
    //        Assert.AreEqual(1, placeHeader.Children[1].MatchedCount);

    //        Assert.AreEqual(2, placeHeader.Children[0].Children.Count);
    //        Assert.AreEqual(file3.State, placeHeader.Children[0].Children[0].Value);
    //        Assert.AreEqual(file2.State, placeHeader.Children[0].Children[1].Value);

    //        Model.BeginUpdateFile(file2);
    //        file2.Country = "Russia";
    //        Model.EndUpdateFile(file2);

    //        Assert.AreEqual(2, placeHeader.Children.Count);
    //        Assert.AreEqual(2, placeHeader.Children[0].MatchedCount);
    //        Assert.AreEqual(2, placeHeader.Children[1].MatchedCount);

    //        Assert.AreEqual(1, placeHeader.Children[0].Children.Count);
    //        Assert.AreEqual(2, placeHeader.Children[1].Children.Count);
    //        Assert.AreEqual(file.State, placeHeader.Children[1].Children[0].Value);
    //        Assert.AreEqual(file2.State, placeHeader.Children[1].Children[1].Value);

    //        Model.BeginUpdateFile(file3);
    //        file3.State = "Tulskaya obl";
    //        Model.EndUpdateFile(file3);

    //        Assert.AreEqual(2, placeHeader.Children.Count);
    //        Assert.AreEqual(2, placeHeader.Children[0].MatchedCount);
    //        Assert.AreEqual(2, placeHeader.Children[1].MatchedCount);

    //        Assert.AreEqual(2, placeHeader.Children[0].Children.Count);
    //        Assert.AreEqual(file4.State, placeHeader.Children[0].Children[0].Value);
    //        Assert.AreEqual(file3.State, placeHeader.Children[0].Children[1].Value);
    //    }

    //    [TestMethod]
    //    public void TestEventFilterWhenEventUpdated() {
    //        DmFile file = GetTestPhotoModel();

    //        DmFile file2 = GetTestPhotoModel();
    //        file2.Event = "Arsen Birthday";

    //        DmFile file3 = GetTestPhotoModel();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Filter header = Model.GetFilter(FilterType.EventHeader);
    //        Assert.AreEqual(2, header.Children.Count);
    //        Assert.AreEqual(2, header.Children[0].MatchedCount);
    //        Assert.AreEqual(1, header.Children[1].MatchedCount);

    //        Model.BeginUpdateFile(file);
    //        file.Event = file2.Event;
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(2, header.Children.Count);
    //        Assert.AreEqual(1, header.Children[0].MatchedCount);
    //        Assert.AreEqual(2, header.Children[1].MatchedCount);

    //        Model.BeginUpdateFile(file3);
    //        file3.Event = "Artem Birthday";
    //        Model.EndUpdateFile(file3);

    //        Assert.AreEqual(3, header.Children.Count);
    //        Assert.AreEqual(0, header.Children[0].MatchedCount);
    //        Assert.AreEqual(2, header.Children[1].MatchedCount);
    //        Assert.AreEqual(1, header.Children[2].MatchedCount);

    //        Model.BeginUpdateFile(file);
    //        file.Event = "";
    //        Model.EndUpdateFile(file);
    //        Model.BeginUpdateFile(file3);
    //        file3.Event = "";
    //        Model.EndUpdateFile(file3);
    //        Model.BeginUpdateFile(file2);
    //        file2.Event = "";
    //        Model.EndUpdateFile(file2);

    //        Assert.AreEqual(1, header.Children.Count);
    //        Assert.AreEqual(3, header.Children[0].MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestWhenKeywordRemovedFileShouldNotBeZero() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.People);
    //        Model.AddTag("Arina", TagType.People);
    //        Model.AddTag("Artem", TagType.People);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.People);

    //        DmPeople[] arr = new DmPeople[3];
    //        file.Peoples.CopyTo(arr, 0);
    //        file.RemoveTag(Model, tags[0], TagType.People);
    //        Assert.AreEqual(tags[0], arr[0].RemovedTag);
    //    }

    //    [TestMethod]
    //    public void TestCorrectRemovingTags() {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", TagType.People);
    //        Model.AddTag("Arina", TagType.People);
    //        Model.AddTag("Artem", TagType.People);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, TagType.People);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        List<DmTag> tagsToRemove = new List<DmTag>();
    //        tagsToRemove.Add(tags[0]);
    //        Model.BeginUpdateFile(file);
    //        Model.RemoveKeywords(file, tagsToRemove, TagType.People);
    //        Model.EndUpdateFile(file);

    //        Model.GetFilter(FilterType.MediaFormat, file.MediaFormat);
    //    }

    //    void UpdateFilterTagsCore(TagType tagType, FilterType headerType) {
    //        DmFile file = GetTestPhotoModel();

    //        Model.AddTag("Arsen", tagType);
    //        Model.AddTag("Arina", tagType);
    //        Model.AddTag("Artem", tagType);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);
    //        Model.AddKeywords(file, tags, tagType);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Filter filter = Model.GetFilter(headerType);
    //        Assert.AreEqual(4, filter.Children.Count);
    //        Assert.AreEqual(0, filter.Children[0].MatchedCount);
    //        Assert.AreEqual(1, filter.Children[1].MatchedCount);
    //        Assert.AreEqual(1, filter.Children[2].MatchedCount);
    //        Assert.AreEqual(1, filter.Children[3].MatchedCount);

    //        List<DmTag> tagsToRemove = new List<DmTag>();
    //        tagsToRemove.Add(tags[0]);

    //        Model.BeginUpdateFile(file);
    //        Assert.AreEqual(true, file.IsTagsEquals(Model.FileBeforeUpdate, tagType));

    //        Model.RemoveKeywords(file, tagsToRemove, tagType);
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(3, filter.Children.Count);
    //        Assert.AreEqual(0, filter.Children[0].MatchedCount);
    //        Assert.AreEqual(1, filter.Children[1].MatchedCount);
    //        Assert.AreEqual(1, filter.Children[1].MatchedCount);

    //        tagsToRemove.Add(tags[1]);
    //        Model.BeginUpdateFile(file);
    //        Model.RemoveKeywords(file, tagsToRemove, tagType);
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(2, filter.Children.Count);
    //        Assert.AreEqual(0, filter.Children[0].MatchedCount);
    //        Assert.AreEqual(1, filter.Children[1].MatchedCount);

    //        tagsToRemove.Add(tags[2]);
    //        Model.BeginUpdateFile(file);
    //        Model.RemoveKeywords(file, tagsToRemove, tagType);
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(1, filter.Children.Count);
    //        Assert.AreEqual(1, filter.Children[0].MatchedCount);

    //        List<DmTag> newTags = new List<DmTag>();
    //        newTags.Add(new DmTag() { Value = "Alena", Type = tagType });
    //        Model.BeginUpdateFile(file);
    //        Model.AddKeywords(file, newTags, tagType);
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(2, filter.Children.Count);
    //        Assert.AreEqual(0, filter.Children[0].MatchedCount);
    //        Assert.AreEqual(1, filter.Children[1].MatchedCount);
    //    }

    //    [TestMethod]
    //    public void UpdateFilterPeople() {
    //        UpdateFilterTagsCore(TagType.People, FilterType.PeopleHeader);
    //    }

    //    [TestMethod]
    //    public void UpdateFilterAutor() {
    //        UpdateFilterTagsCore(TagType.Autor, FilterType.AutorsHeader);
    //    }

    //    [TestMethod]
    //    public void UpdateFilterKeywords() {
    //        UpdateFilterTagsCore(TagType.Tag, FilterType.TagsHeader);
    //    }

    //    [TestMethod]
    //    public void UpdateFilterCollections() {
    //        UpdateFilterTagsCore(TagType.Collection, FilterType.CollectionsHeader);
    //    }

    //    [TestMethod]
    //    public void UpdateFilterCategory() {
    //        UpdateFilterTagsCore(TagType.Category, FilterType.CategoriesHeader);
    //    }

    //    [TestMethod]
    //    public void UpdateFilterGenres() {
    //        UpdateFilterTagsCore(TagType.Genre, FilterType.GenresHeader);
    //    }

    //    [TestMethod]
    //    public void TestFilterLastImportedWhenRemoveFile() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(3, Model.GetFilter(FilterType.LastImported).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        Assert.AreEqual(2, Model.GetFilter(FilterType.LastImported).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.RemoveFile(file3);
    //        Model.EndUpdate();

    //        Assert.AreEqual(0, Model.GetFilter(FilterType.LastImported).MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestFilterImportedYesterdayWhenRemoveFile() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();

    //        file.ImportDate = DateTime.Now.AddDays(-1);
    //        file2.ImportDate = DateTime.Now.AddDays(-1);
    //        file3.ImportDate = DateTime.Now.AddDays(-1);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(3, Model.GetFilter(FilterType.ImportedYesterday).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        Assert.AreEqual(2, Model.GetFilter(FilterType.ImportedYesterday).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.RemoveFile(file3);
    //        Model.EndUpdate();

    //        Assert.AreEqual(0, Model.GetFilter(FilterType.ImportedYesterday).MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestFilterImportedLastWeekWhenRemoveFile() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(3, Model.GetFilter(FilterType.ImportedLastWeek).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        Assert.AreEqual(2, Model.GetFilter(FilterType.ImportedLastWeek).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.RemoveFile(file3);
    //        Model.EndUpdate();

    //        Assert.AreEqual(0, Model.GetFilter(FilterType.ImportedLastWeek).MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestFilterImportedLastMonthWhenRemoveFile() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(3, Model.GetFilter(FilterType.ImportedLastMonth).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        Assert.AreEqual(2, Model.GetFilter(FilterType.ImportedLastMonth).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.RemoveFile(file3);
    //        Model.EndUpdate();

    //        Assert.AreEqual(0, Model.GetFilter(FilterType.ImportedLastMonth).MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestFilterFolderWhenRemoveFile() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(2, Model.GetFilter(FilterType.FolderHeader).Children.Count);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.Folder, file2.Folder).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Folder, file.Folder).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        Assert.AreEqual(1, Model.GetFilter(FilterType.FolderHeader).Children.Count);
    //        Assert.AreEqual(2, Model.GetFilter(FilterType.Folder, file2.Folder).MatchedCount);


    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.EndUpdate();

    //        Assert.AreEqual(1, Model.GetFilter(FilterType.FolderHeader).Children.Count);
    //        Assert.AreEqual(1, Model.GetFilter(FilterType.Folder, file2.Folder).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file3);
    //        Model.EndUpdate();

    //        Assert.AreEqual(0, Model.GetFilter(FilterType.FolderHeader).MatchedCount);
    //    }

    //    protected void CheckFilterTreeText(Filter filter, List<FilterChecker> fc) {
    //        Assert.AreEqual(fc.Count, filter.Children.Count);
    //        for(int i = 0; i < fc.Count; i++) {
    //            Assert.AreEqual(filter.Children[i].Text, fc[i].Text);
    //            Assert.AreEqual(filter.Children[i].MatchedCount, fc[i].MatchedCount);
    //            CheckFilterTreeText(filter.Children[i], fc[i].Children);
    //        }
    //    }

    //    [TestMethod]
    //    public void TestPlaceFilterWhenFilesAdded() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        file2.Country = "Russia";
    //        file2.State = "Tulskaya obl";
    //        file2.City = "Tula";
    //        file2.Location = "Tsiolkovskogo st";

    //        file3.Country = "Russia";
    //        file3.State = "Tulskaya obl";

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        List<FilterChecker> fc = new List<FilterChecker>();
    //        fc.Add(new FilterChecker("Country", 1));
    //        fc[0].Children.Add(new FilterChecker("State", 1));
    //        fc[0].Children[0].Children.Add(new FilterChecker("City", 1));
    //        fc[0].Children[0].Children[0].Children.Add(new FilterChecker("Location", 1));
    //        fc.Add(new FilterChecker("Russia", 2));
    //        fc[1].Children.Add(new FilterChecker("Tulskaya obl", 2));
    //        fc[1].Children[0].Children.Add(new FilterChecker("Tula", 1));
    //        fc[1].Children[0].Children[0].Children.Add(new FilterChecker("Tsiolkovskogo st", 1));
    //        fc[1].Children[0].Children.Add(new FilterChecker("City", 1));
    //        fc[1].Children[0].Children[1].Children.Add(new FilterChecker("Location", 1));


    //        CheckFilterTreeText(Model.PlaceFilter, fc);

    //    }

    //    public class FilterChecker {
    //        public FilterChecker(string text, int matchCount) {
    //            Text = text;
    //            MatchedCount = matchCount;
    //            Children = new List<FilterChecker>();
    //        }

    //        public string Text { get; set; }
    //        public int MatchedCount { get; set; }
    //        public List<FilterChecker> Children { get; private set; }
    //    }

    //    [TestMethod]
    //    public void TestUpdateFilterPlaceWhenFileRemoved() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        file2.Country = "Russia";
    //        file2.State = "Tulskaya obl";
    //        file2.City = "Tula";
    //        file2.Location = "Tsiolkovskogo st";

    //        file3.Country = "Russia";
    //        file3.State = "Tulskaya obl";

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Model.RemoveFile(file);

    //        List<FilterChecker> fc = new List<FilterChecker>();
    //        fc.Add(new FilterChecker("Russia", 2));
    //        fc[0].Children.Add(new FilterChecker("Tulskaya obl", 2));
    //        fc[0].Children[0].Children.Add(new FilterChecker("Tula", 1));
    //        fc[0].Children[0].Children[0].Children.Add(new FilterChecker("Tsiolkovskogo st", 1));
    //        fc[0].Children[0].Children.Add(new FilterChecker("City", 1));
    //        fc[0].Children[0].Children[1].Children.Add(new FilterChecker("Location", 1));
    //        CheckFilterTreeText(Model.PlaceFilter, fc);

    //        Model.RemoveFile(file2);

    //        fc = new List<FilterChecker>();
    //        fc.Add(new FilterChecker("Russia", 1));
    //        fc[0].Children.Add(new FilterChecker("Tulskaya obl", 1));
    //        fc[0].Children[0].Children.Add(new FilterChecker("City", 1));
    //        fc[0].Children[0].Children[0].Children.Add(new FilterChecker("Location", 1));
    //        CheckFilterTreeText(Model.PlaceFilter, fc);

    //        Model.RemoveFile(file3);
    //        fc = new List<FilterChecker>();
    //        CheckFilterTreeText(Model.PlaceFilter, fc);
    //    }

    //    [TestMethod]
    //    public void TestUpdateColorLabelFilterWhenFileRemoved() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();
    //        DmFile file4 = GetTestPhotoModel2();

    //        file.ColorLabel = Model.Context.ColorLabels.Local[1];
    //        file2.ColorLabel = Model.Context.ColorLabels.Local[2];

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(Model.Context.ColorLabels.Local.Count + 1, Model.ColorLabelHeaderFilter.Children.Count);
    //        foreach(Filter filter in Model.ColorLabelHeaderFilter.Children) {
    //            if(filter.Value == file.ColorLabel)
    //                Assert.AreEqual(1, filter.MatchedCount);
    //            else if(filter.Value == file2.ColorLabel)
    //                Assert.AreEqual(1, filter.MatchedCount);
    //            else if(filter.Value == file3.ColorLabel)
    //                Assert.AreEqual(2, filter.MatchedCount);
    //            else
    //                Assert.AreEqual(0, filter.MatchedCount);
    //        }

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.RemoveFile(file2);
    //        Model.RemoveFile(file3);
    //        Model.RemoveFile(file4);
    //        Model.EndUpdate();

    //        Assert.AreEqual(Model.Context.ColorLabels.Local.Count + 1, Model.ColorLabelHeaderFilter.Children.Count);
    //        foreach(Filter filter in Model.ColorLabelHeaderFilter.Children) {
    //            Assert.AreEqual(0, filter.MatchedCount);
    //        }
    //    }

    //    [TestMethod]
    //    public void TestUpdateMediaFormatFilterWhenRemoveFile() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();
    //        DmFile file4 = GetTestPhotoModel2();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.EndAddFiles();

    //        Assert.AreNotEqual(null, file.MediaFormat);
    //        Assert.AreNotEqual(null, file2.MediaFormat);
    //        Assert.AreNotEqual(null, file3.MediaFormat);
    //        Assert.AreNotEqual(null, file4.MediaFormat);

    //        Assert.AreEqual(4, Model.MediaFormatHeaderFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(4, Model.GetFilter(Model.MediaFormatHeaderFilter.Children[0], file.MediaFormat).MatchedCount);

    //        Filter mediaFormatFilter = Model.GetFilter(Model.MediaFormatHeaderFilter.Children[0], file.MediaFormat);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        Assert.AreEqual(3, Model.MediaFormatHeaderFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(3, mediaFormatFilter.MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.EndUpdate();

    //        Assert.AreEqual(2, Model.MediaFormatHeaderFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(2, mediaFormatFilter.MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file3);
    //        Model.EndUpdate();

    //        Assert.AreEqual(1, Model.MediaFormatHeaderFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(1, mediaFormatFilter.MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file4);
    //        Model.EndUpdate();

    //        Assert.AreEqual(0, Model.MediaFormatHeaderFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(0, mediaFormatFilter.MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestUpdateMediaFormatFilterWhenFileUpdate() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();
    //        DmFile file4 = GetTestPhotoModel2();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.EndAddFiles();

    //        Assert.AreNotEqual(null, file.MediaFormat);
    //        Assert.AreNotEqual(null, file2.MediaFormat);
    //        Assert.AreNotEqual(null, file3.MediaFormat);
    //        Assert.AreNotEqual(null, file4.MediaFormat);

    //        Assert.AreEqual(4, Model.MediaFormatHeaderFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(4, Model.GetFilter(Model.MediaFormatHeaderFilter.Children[0], file.MediaFormat).MatchedCount);

    //        Filter mediaFilter = Model.GetFilter(Model.MediaFormatHeaderFilter.Children[0], file.MediaFormat);

    //        Model.BeginUpdateFile(file);
    //        file.MediaFormat = Model.Context.MediaFormat.Local[3];
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(4, Model.MediaFormatHeaderFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(3, mediaFilter.MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(Model.MediaFormatHeaderFilter.Children[0], file.MediaFormat).MatchedCount);

    //        Model.BeginUpdateFile(file2);
    //        file2.MediaFormat = Model.Context.MediaFormat.Local[3];
    //        Model.EndUpdateFile(file2);

    //        Assert.AreEqual(4, Model.MediaFormatHeaderFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(2, mediaFilter.MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(Model.MediaFormatHeaderFilter.Children[0], file.MediaFormat).MatchedCount);

    //        Model.BeginUpdateFile(file3);
    //        file3.MediaFormat = Model.Context.MediaFormat.Local[3];
    //        Model.EndUpdateFile(file3);

    //        Assert.AreEqual(4, Model.MediaFormatHeaderFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(1, mediaFilter.MatchedCount);
    //        Assert.AreEqual(3, Model.GetFilter(Model.MediaFormatHeaderFilter.Children[0], file.MediaFormat).MatchedCount);

    //        Model.BeginUpdateFile(file4);
    //        file4.MediaFormat = Model.Context.MediaFormat.Local[3];
    //        Model.EndUpdateFile(file4);

    //        Assert.AreEqual(4, Model.MediaFormatHeaderFilter.Children[0].MatchedCount);
    //        Assert.AreEqual(0, mediaFilter.MatchedCount);
    //        Assert.AreEqual(4, Model.GetFilter(Model.MediaFormatHeaderFilter.Children[0], file.MediaFormat).MatchedCount);

    //    }

    //    [TestMethod]
    //    public void TestProjectFilterWhenFileAdded() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        DmFile file4 = GetTestPhotoModel();
    //        DmFile file5 = GetTestPhotoModel();

    //        file1.Client = "Alena";
    //        file1.Project = "Wedding";
    //        file1.Scene = "Square";

    //        file2.Client = "Alena";
    //        file2.Project = "Wedding";

    //        file3.Client = "Alena";
    //        file4.Scene = "Beach";
    //        file5.Project = "Vacation";

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.AddFile(file5);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(2, Model.ProjectHeaderFilter.Children.Count);
    //        Filter clientUnAssigned = Model.GetFilter(Model.ProjectHeaderFilter, "");
    //        Filter clientAlena = Model.GetFilter(Model.ProjectHeaderFilter, "Alena");

    //        Assert.AreEqual(2, clientAlena.Children.Count);
    //        Assert.AreEqual(3, clientAlena.MatchedCount);

    //        Filter wedding = Model.GetFilter(clientAlena, "Wedding");
    //        Assert.AreEqual(2, wedding.MatchedCount);
    //        Assert.AreEqual(2, wedding.Children.Count);
    //        Assert.AreEqual(1, wedding.Children[0].MatchedCount);
    //        Assert.AreEqual(1, wedding.Children[1].MatchedCount);

    //        Assert.AreEqual(2, clientUnAssigned.MatchedCount);
    //        Assert.AreEqual(2, clientUnAssigned.Children.Count);

    //        Filter vacation = Model.GetFilter(clientUnAssigned, "Vacation");
    //        Assert.AreEqual(1, vacation.Children.Count);
    //        Assert.AreEqual(1, vacation.MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestProjectFilterWhenFileUpdated() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        file1.Client = "Alena";
    //        file2.Client = "Alena";
    //        file1.Project = "Wedding";
    //        file2.Project = "Wedding";
    //        file1.Scene = "Square";
    //        file2.Scene = "Square";

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Filter clientAlena = Model.GetFilter(Model.ProjectHeaderFilter, "Alena");
    //        Filter clientUnAssigned = Model.GetFilter(Model.ProjectHeaderFilter, "");
    //        Filter weddingProject = Model.GetFilter(clientAlena, "Wedding");
    //        Filter unassignedProject = Model.GetFilter(clientUnAssigned, "");
    //        Filter squareScene = Model.GetFilter(weddingProject, "Square");
    //        Filter unassignedScene = Model.GetFilter(unassignedProject, "");

    //        Model.BeginUpdateFile(file1);
    //        file1.Client = "";
    //        file1.Scene = "";
    //        file1.Project = "";
    //        Model.EndUpdateFile(file1);

    //        Assert.AreEqual(1, clientAlena.MatchedCount);
    //        Assert.AreEqual(1, weddingProject.MatchedCount);
    //        Assert.AreEqual(1, squareScene.MatchedCount);

    //        Assert.AreEqual(2, clientUnAssigned.MatchedCount);
    //        Assert.AreEqual(2, unassignedProject.MatchedCount);
    //        Assert.AreEqual(2, unassignedScene.MatchedCount);

    //        Model.BeginUpdateFile(file2);
    //        file2.Scene = file2.Project = file2.Client = "";
    //        Model.EndUpdateFile(file2);

    //        Assert.AreEqual(1, Model.ProjectHeaderFilter.Children.Count);
    //        Assert.AreEqual(null, clientAlena.Parent);

    //        Assert.AreEqual(3, clientUnAssigned.MatchedCount);
    //        Assert.AreEqual(3, unassignedProject.MatchedCount);
    //        Assert.AreEqual(3, unassignedScene.MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestFilterProjectWhenFileRemoved() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        file1.Client = "Alena";
    //        file2.Client = "Alena";
    //        file1.Project = "Wedding";
    //        file2.Project = "Wedding";
    //        file1.Scene = "Square";
    //        file2.Scene = "Square";

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Filter clientAlena = Model.GetFilter(Model.ProjectHeaderFilter, "Alena");
    //        Filter clientUnAssigned = Model.GetFilter(Model.ProjectHeaderFilter, "");
    //        Filter weddingProject = Model.GetFilter(clientAlena, "Wedding");
    //        Filter unassignedProject = Model.GetFilter(clientUnAssigned, "");
    //        Filter squareScene = Model.GetFilter(weddingProject, "Square");
    //        Filter unassignedScene = Model.GetFilter(unassignedProject, "");

    //        Model.RemoveFile(file1);

    //        Assert.AreEqual(1, clientAlena.MatchedCount);
    //        Assert.AreEqual(1, weddingProject.MatchedCount);
    //        Assert.AreEqual(1, squareScene.MatchedCount);

    //        Assert.AreEqual(1, clientUnAssigned.MatchedCount);
    //        Assert.AreEqual(1, unassignedProject.MatchedCount);
    //        Assert.AreEqual(1, unassignedScene.MatchedCount);

    //        Model.RemoveFile(file2);

    //        Assert.AreEqual(1, Model.ProjectHeaderFilter.Children.Count);
    //        Assert.AreEqual(null, clientAlena.Parent);

    //        Assert.AreEqual(1, clientUnAssigned.MatchedCount);
    //        Assert.AreEqual(1, unassignedProject.MatchedCount);
    //        Assert.AreEqual(1, unassignedScene.MatchedCount);

    //        Model.RemoveFile(file3);
    //        Assert.AreEqual(0, Model.ProjectHeaderFilter.Children.Count);
    //    }

    //    [TestMethod]
    //    public void TestCreationDateFilterWhenFileUpdate() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();

    //        file.CreationDate = new DateTime(1982, 1, 23);
    //        file2.CreationDate = new DateTime(1982, 1, 23);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        List<FilterChecker> fc = new List<FilterChecker>();
    //        fc.Add(new FilterChecker("1982", 2));
    //        fc[0].Children.Add(new FilterChecker("January", 2));
    //        fc[0].Children[0].Children.Add(new FilterChecker("23", 2));

    //        CheckFilterTreeText(Model.CreationDateHeaderFilter, fc);

    //        Model.BeginUpdateFile(file);
    //        file.CreationDate = new DateTime(2007, 6, 14);
    //        Model.EndUpdateFile(file);

    //        fc = new List<FilterChecker>();
    //        fc.Add(new FilterChecker("1982", 1));
    //        fc[0].Children.Add(new FilterChecker("January", 1));
    //        fc[0].Children[0].Children.Add(new FilterChecker("23", 1));

    //        fc.Add(new FilterChecker("2007", 1));
    //        fc[1].Children.Add(new FilterChecker("June", 1));
    //        fc[1].Children[0].Children.Add(new FilterChecker("14", 1));

    //        CheckFilterTreeText(Model.CreationDateHeaderFilter, fc);

    //        Model.BeginUpdateFile(file2);
    //        file2.CreationDate = new DateTime(2007, 6, 14);
    //        Model.EndUpdateFile(file2);

    //        fc = new List<FilterChecker>();
    //        fc.Add(new FilterChecker("2007", 2));
    //        fc[0].Children.Add(new FilterChecker("June", 2));
    //        fc[0].Children[0].Children.Add(new FilterChecker("14", 2));
    //        CheckFilterTreeText(Model.CreationDateHeaderFilter, fc);
    //    }

    //    [TestMethod]
    //    public void TestCreationDateFilterWhenFileRemove() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();

    //        file.CreationDate = new DateTime(1982, 1, 23);
    //        file2.CreationDate = new DateTime(1982, 1, 23);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        List<FilterChecker> fc = new List<FilterChecker>();
    //        fc.Add(new FilterChecker("1982", 2));
    //        fc[0].Children.Add(new FilterChecker("January", 2));
    //        fc[0].Children[0].Children.Add(new FilterChecker("23", 2));

    //        CheckFilterTreeText(Model.CreationDateHeaderFilter, fc);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        fc = new List<FilterChecker>();
    //        fc.Add(new FilterChecker("1982", 1));
    //        fc[0].Children.Add(new FilterChecker("January", 1));
    //        fc[0].Children[0].Children.Add(new FilterChecker("23", 1));
    //        CheckFilterTreeText(Model.CreationDateHeaderFilter, fc);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.EndUpdate();

    //        fc = new List<FilterChecker>();
    //        CheckFilterTreeText(Model.CreationDateHeaderFilter, fc);
    //    }

    //    [TestMethod]
    //    public void TestImportDateFilterWhenFileRemove() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();

    //        file.ImportDate = new DateTime(1982, 1, 23);
    //        file2.ImportDate = new DateTime(1982, 1, 23);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        List<FilterChecker> fc = new List<FilterChecker>();
    //        fc.Add(new FilterChecker("1982", 2));
    //        fc[0].Children.Add(new FilterChecker("January", 2));
    //        fc[0].Children[0].Children.Add(new FilterChecker("23", 2));

    //        CheckFilterTreeText(Model.ImportDateHeaderFilter, fc);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        fc = new List<FilterChecker>();
    //        fc.Add(new FilterChecker("1982", 1));
    //        fc[0].Children.Add(new FilterChecker("January", 1));
    //        fc[0].Children[0].Children.Add(new FilterChecker("23", 1));
    //        CheckFilterTreeText(Model.ImportDateHeaderFilter, fc);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.EndUpdate();

    //        fc = new List<FilterChecker>();
    //        CheckFilterTreeText(Model.ImportDateHeaderFilter, fc);
    //    }
    //    [TestMethod]
    //    public void TestRatingWhenFileUpdated() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();

    //        file.Rating = 3;
    //        file2.Rating = 3;
    //        file3.Rating = 5;

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(2, Model.GetFilter(Model.RatingHeaderFilter, 3).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(Model.RatingHeaderFilter, 5).MatchedCount);

    //        Model.BeginUpdateFile(file);
    //        file.Rating = 5;
    //        Model.EndUpdateFile(file);
    //        Assert.AreEqual(1, Model.GetFilter(Model.RatingHeaderFilter, 3).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(Model.RatingHeaderFilter, 5).MatchedCount);

    //        Model.BeginUpdateFile(file2);
    //        file2.Rating = 5;
    //        Model.EndUpdateFile(file2);
    //        Assert.AreEqual(0, Model.GetFilter(Model.RatingHeaderFilter, 3).MatchedCount);
    //        Assert.AreEqual(3, Model.GetFilter(Model.RatingHeaderFilter, 5).MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestRatingWhenRemoveFile() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();

    //        file.Rating = 3;
    //        file2.Rating = 3;
    //        file3.Rating = 5;

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(2, Model.GetFilter(Model.RatingHeaderFilter, 3).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(Model.RatingHeaderFilter, 5).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        Assert.AreEqual(1, Model.GetFilter(Model.RatingHeaderFilter, 3).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(Model.RatingHeaderFilter, 5).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.EndUpdate();

    //        Assert.AreEqual(0, Model.GetFilter(Model.RatingHeaderFilter, 3).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(Model.RatingHeaderFilter, 5).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file3);
    //        Model.EndUpdate();

    //        Assert.AreEqual(0, Model.GetFilter(Model.RatingHeaderFilter, 5).MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestEventFilterWhenFileUpdated() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();

    //        string event1 = "Artem Birthday", event2 = "Arina Birthday", event3 = "Alena Birthday";

    //        file.Event = event1;
    //        file2.Event = event1;
    //        file3.Event = event2;

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(2, Model.GetFilter(Model.EventsHeaderFilter, event1).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(Model.EventsHeaderFilter, event2).MatchedCount);

    //        Model.BeginUpdateFile(file);
    //        file.Event = event2;
    //        Model.EndUpdateFile(file);
    //        Assert.AreEqual(1, Model.GetFilter(Model.EventsHeaderFilter, event1).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(Model.EventsHeaderFilter, event2).MatchedCount);

    //        Model.BeginUpdateFile(file2);
    //        file2.Event = event2;
    //        Model.EndUpdateFile(file2);
    //        Assert.AreEqual(null, Model.GetFilter(Model.EventsHeaderFilter, event1));
    //        Assert.AreEqual(3, Model.GetFilter(Model.EventsHeaderFilter, event2).MatchedCount);

    //        Model.BeginUpdateFile(file);
    //        file.Event = event3;
    //        Model.EndUpdateFile(file);
    //        Assert.AreEqual(1, Model.GetFilter(Model.EventsHeaderFilter, event3).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(Model.EventsHeaderFilter, event2).MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestEventFilterWhenFileRemoved() {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();

    //        string event1 = "Artem Birthday", event2 = "Arina Birthday";

    //        file.Event = event1;
    //        file2.Event = event1;
    //        file3.Event = event2;

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        Assert.AreEqual(1, Model.GetFilter(Model.EventsHeaderFilter, event1).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(Model.EventsHeaderFilter, event2).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.EndUpdate();

    //        Assert.AreEqual(null, Model.GetFilter(Model.EventsHeaderFilter, event1));
    //        Assert.AreEqual(1, Model.GetFilter(Model.EventsHeaderFilter, event2).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file3);
    //        Model.EndUpdate();

    //        Assert.AreEqual(null, Model.GetFilter(Model.EventsHeaderFilter, event1));
    //        Assert.AreEqual(null, Model.GetFilter(Model.EventsHeaderFilter, event2));
    //    }

    //    public void TestTagsFilterWhenRemoveFileCore(FilterType filterType, TagType type) {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();
    //        DmFile file4 = GetTestPhotoModel2();

    //        Model.AddTag("Arsen", type);
    //        Model.AddTag("Arina", type);
    //        Model.AddTag("Artem", type);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);

    //        Model.AddKeywords(file, tags, type);
    //        Model.AddKeywords(file2, tags, type);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(2, Model.GetFilter(filterType, null).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(filterType, tags[0]).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(filterType, tags[1]).MatchedCount);
    //        Assert.AreEqual(2, Model.GetFilter(filterType, tags[2]).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        Assert.AreEqual(2, Model.GetFilter(filterType, null).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(filterType, tags[0]).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(filterType, tags[1]).MatchedCount);
    //        Assert.AreEqual(1, Model.GetFilter(filterType, tags[2]).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.EndUpdate();

    //        Assert.AreEqual(2, Model.GetFilter(filterType, null).MatchedCount);
    //        Assert.AreEqual(null, Model.GetFilter(filterType, tags[0]));
    //        Assert.AreEqual(null, Model.GetFilter(filterType, tags[1]));
    //        Assert.AreEqual(null, Model.GetFilter(filterType, tags[2]));

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file3);
    //        Model.EndUpdate();

    //        Assert.AreEqual(1, Model.GetFilter(filterType, null).MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file4);
    //        Model.EndUpdate();

    //        Assert.AreEqual(0, Model.GetFilter(filterType, null).MatchedCount);
    //    }

    //    [TestMethod]
    //    public void TestPeopleFilterWhenFileRemoved() {
    //        TestTagsFilterWhenRemoveFileCore(FilterType.People, TagType.People);
    //    }

    //    [TestMethod]
    //    public void TestAutorsFilterWhenFileRemoved() {
    //        TestTagsFilterWhenRemoveFileCore(FilterType.Autor, TagType.Autor);
    //    }

    //    [TestMethod]
    //    public void TestKeywordsFilterWhenFileRemoved() {
    //        TestTagsFilterWhenRemoveFileCore(FilterType.Tag, TagType.Tag);
    //    }

    //    [TestMethod]
    //    public void TestCategoryFilterWhenFileRemoved() {
    //        TestTagsFilterWhenRemoveFileCore(FilterType.Category, TagType.Category);
    //    }

    //    [TestMethod]
    //    public void TestCollectionFilterWhenFileRemoved() {
    //        TestTagsFilterWhenRemoveFileCore(FilterType.Collection, TagType.Collection);
    //    }

    //    [TestMethod]
    //    public void TestGenreFilterWhenFileRemoved() {
    //        TestTagsFilterWhenRemoveFileCore(FilterType.Genre, TagType.Genre);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionLastImported() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.EndAddFiles();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        Model.LastImportedFilter.IsActive = true;
    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file2, list[0]);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionImportedToday() {
    //        DmFile file1 = GetTestPhotoModel();
    //        file1.ImportDate = file1.ImportDate.AddDays(-1);
    //        DmFile file2 = GetTestPhotoModel();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.EndAddFiles();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        Model.ImportedTodayFilter.IsActive = true;
    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file2, list[0]);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionImportedTodayOrLastImported() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        file2.ImportDate = file2.ImportDate.AddDays(1);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.EndAddFiles();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        Model.ImportedTodayFilter.Parent.OperationType = FilterOperationType.Or;
    //        Model.LastImportedFilter.IsActive = true;
    //        Model.ImportedTodayFilter.IsActive = true;
    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.Or);
    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(file1, list[0]);
    //        Assert.AreEqual(file2, list[1]);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionImportedYesterday() {
    //        DmFile file1 = GetTestPhotoModel();
    //        file1.ImportDate = file1.ImportDate.AddDays(-1);
    //        DmFile file2 = GetTestPhotoModel();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        Model.ImportedYesterdayFilter.IsActive = true;
    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file1, list[0]);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionImportedLastWeek() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        file2.ImportDate = file2.ImportDate.AddDays(-60);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.EndAddFiles();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        Model.ImportedLastWeekFilter.IsActive = true;
    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file1, list[0]);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionImportedLastMonth() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        file2.ImportDate = file2.ImportDate.AddDays(-60);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.EndAddFiles();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        Model.ImportedLastMonthFilter.IsActive = true;
    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file1, list[0]);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionByFolder() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        Model.FolderHeaderFilter.Children[0].IsActive = true;
    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file1, list[0]);
    //    }

    //    [TestMethod]
    //    public void TestHasActiveChildren() {
    //        Filter country = new Filter(FilterType.Country, Guid.Empty);
    //        Filter state1 = new Filter(FilterType.State, country.Id);
    //        Filter state2 = new Filter(FilterType.State, country.Id);

    //        Filter city1 = new Filter(FilterType.City, state1.Id);
    //        Filter city2 = new Filter(FilterType.City, state1.Id);
    //        state1.Children.Add(city1);
    //        state1.Children.Add(city2);

    //        Filter city3 = new Filter(FilterType.City, state2.Id);
    //        Filter city4 = new Filter(FilterType.City, state2.Id);
    //        state2.Children.Add(city3);
    //        state2.Children.Add(city4);

    //        city1.IsActive = true;
    //        state2.IsActive = true;

    //        country.Children.Add(state1);
    //        country.Children.Add(state2);

    //        Model.UpdateHasActiveChildrenFlag(country);
    //        Assert.AreEqual(true, country.HasActiveChildren);
    //        Assert.AreEqual(2, country.ActiveChildren.Count);
    //        Assert.AreEqual(state1, country.ActiveChildren[0]);
    //        Assert.AreEqual(state2, country.ActiveChildren[1]);

    //        Assert.AreEqual(true, state1.HasActiveChildren);
    //        Assert.AreEqual(1, state1.ActiveChildren.Count);
    //        Assert.AreEqual(city1, state1.ActiveChildren[0]);

    //        Assert.AreEqual(false, state2.HasActiveChildren);

    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionByMediaFormatType() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();

    //        Model.Context.MediaFormat.Add(new MediaFormat() { Type = MediaType.Sound, Text = "Music", Extension = "mp3" });
    //        file2.MediaFormat = Model.Context.MediaFormat.Local[Model.Context.MediaFormat.Local.Count - 1];

    //        Filter soundMediaTypeFilter = new Filter(FilterType.MediaFormatType, Model.MediaFormatHeaderFilter.Id);
    //        soundMediaTypeFilter.Value = MediaType.Sound;
    //        soundMediaTypeFilter.Parent = Model.MediaFormatHeaderFilter;

    //        Filter mp3Media = new Filter(FilterType.MediaFormat, soundMediaTypeFilter.Id);
    //        mp3Media.Value = file2.MediaFormat;
    //        mp3Media.Parent = soundMediaTypeFilter;

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        Assert.AreEqual(MediaType.Image, (MediaType)Model.MediaFormatHeaderFilter.Children[0].Value);
    //        Model.MediaFormatHeaderFilter.Children[0].IsActive = true;

    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);

    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file1, list[0]);

    //        Model.MediaFormatHeaderFilter.Children[1].IsActive = true;
    //        Model.MediaFormatHeaderFilter.OperationType = FilterOperationType.Or;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);

    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));

    //        Model.MediaFormatHeaderFilter.OperationType = FilterOperationType.And;
    //        Model.MediaFormatHeaderFilter.Children[0].IsActive = false;
    //        Model.MediaFormatHeaderFilter.Children[1].IsActive = false;

    //        Filter file1Filter = Model.GetFilter(Model.MediaFormatHeaderFilter.Children[0], file1.MediaFormat);
    //        file1Filter.IsActive = true;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);

    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file1, list[0]);

    //        DmFile file3 = GetTestPhotoModel();
    //        file3.MediaFormat = Model.Context.MediaFormat.Local[0];

    //        Model.BeginAddFiles();
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Filter file3Filter = Model.GetFilter(Model.MediaFormatHeaderFilter.Children[0], file3.MediaFormat);
    //        file3Filter.IsActive = true;

    //        file3Filter.Parent.OperationType = FilterOperationType.Or;
    //        list = Model.GetFilteredFiles(FilterOperationType.And);

    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(file1, list[0]);
    //        Assert.AreEqual(file3, list[1]);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionByRating() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        file2.Rating = 1;
    //        file3.Rating = 5;

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(3, list.Count);

    //        Model.GetFilter(Model.RatingHeaderFilter, 0).IsActive = true;
    //        list = Model.GetFilteredFiles(FilterOperationType.And);

    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file1, list[0]);

    //        Model.GetFilter(Model.RatingHeaderFilter, 1).IsActive = true;
    //        Model.RatingHeaderFilter.OperationType = FilterOperationType.Or;
    //        list = Model.GetFilteredFiles(FilterOperationType.And);

    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionByColorLabel() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        file1.ColorLabel = null;
    //        file2.ColorLabel = Model.Context.ColorLabels.Local[1];
    //        file3.ColorLabel = Model.Context.ColorLabels.Local[2];

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Model.GetFilter(Model.ColorLabelHeaderFilter, file1.ColorLabel).IsActive = true;
    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file1, list[0]);

    //        Model.GetFilter(Model.ColorLabelHeaderFilter, file1.ColorLabel).IsActive = false;
    //        Model.GetFilter(Model.ColorLabelHeaderFilter, file2.ColorLabel).IsActive = true;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file2, list[0]);

    //        Model.GetFilter(Model.ColorLabelHeaderFilter, file3.ColorLabel).IsActive = true;
    //        Model.ColorLabelHeaderFilter.OperationType = FilterOperationType.Or;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);

    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionCreationDateTime() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        file1.CreationDate = new DateTime(1982, 1, 23);
    //        file2.CreationDate = new DateTime(1982, 1, 24);
    //        file3.CreationDate = new DateTime(1982, 7, 8);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Filter filter = Model.GetFilter(Model.CreationDateHeaderFilter, 1982);
    //        filter.IsActive = true;

    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(3, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));

    //        Filter month = Model.GetFilter(filter, 1);
    //        month.IsActive = true;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));

    //        Filter day = Model.GetFilter(month, 23);
    //        day.IsActive = true;
    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));

    //        Filter day2 = Model.GetFilter(month, 24);
    //        day2.IsActive = true;
    //        month.OperationType = FilterOperationType.Or;
    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));

    //        filter.OperationType = FilterOperationType.Or;
    //        Filter month2Filter = Model.GetFilter(filter, 7);
    //        month2Filter.IsActive = true;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(3, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionImportDateTime() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        file1.ImportDate = new DateTime(1982, 1, 23);
    //        file2.ImportDate = new DateTime(1982, 1, 24);
    //        file3.ImportDate = new DateTime(1982, 7, 8);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Filter filter = Model.GetFilter(Model.ImportDateHeaderFilter, 1982);
    //        filter.IsActive = true;

    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(3, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));

    //        Filter month = Model.GetFilter(filter, 1);
    //        month.IsActive = true;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));

    //        Filter day = Model.GetFilter(month, 23);
    //        day.IsActive = true;
    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));

    //        Filter day2 = Model.GetFilter(month, 24);
    //        day2.IsActive = true;
    //        month.OperationType = FilterOperationType.Or;
    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));

    //        filter.OperationType = FilterOperationType.Or;
    //        Filter month2Filter = Model.GetFilter(filter, 7);
    //        month2Filter.IsActive = true;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(3, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));
    //    }

    //    public void TestFilterExpressionTagsCore(Filter tagParentFilter, TagType type) {
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();
    //        DmFile file4 = GetTestPhotoModel2();

    //        Model.AddTag("Arsen", type);
    //        Model.AddTag("Arina", type);
    //        Model.AddTag("Artem", type);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);

    //        Model.AddKeywords(file, tags, type);
    //        Model.AddKeywords(file2, tags, type);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.EndAddFiles();

    //        Filter arsenFilter = Model.GetFilter(tagParentFilter, Model.Context.Tags.Local[0]);
    //        arsenFilter.IsActive = true;

    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file));
    //        Assert.AreEqual(true, list.Contains(file2));

    //        Filter unassigned = Model.GetFilter(tagParentFilter, null);
    //        unassigned.IsActive = true;

    //        tagParentFilter.OperationType = FilterOperationType.Or;
    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(4, list.Count);
    //        Assert.AreEqual(true, list.Contains(file));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));
    //        Assert.AreEqual(true, list.Contains(file4));
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionPeople() {
    //        TestFilterExpressionTagsCore(Model.PeoplesHeaderFilter, TagType.People);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionAutors() {
    //        TestFilterExpressionTagsCore(Model.AutorsHeaderFilter, TagType.Autor);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionCategories() {
    //        TestFilterExpressionTagsCore(Model.CategoriesHeaderFilter, TagType.Category);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionCollections() {
    //        TestFilterExpressionTagsCore(Model.CollectionsHeaderFilter, TagType.Collection);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionGenres() {
    //        TestFilterExpressionTagsCore(Model.GenresHeaderFilter, TagType.Genre);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionKeywords() {
    //        TestFilterExpressionTagsCore(Model.TagsHeaderFilter, TagType.Tag);
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionEvent() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();

    //        file1.Event = "Artem's Birthday";
    //        file2.Event = "Arina's Birthday";

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Model.EventsHeaderFilter.Children[1].IsActive = true;
    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file1, list[0]);

    //        Model.EventsHeaderFilter.Children[2].IsActive = true;
    //        Model.EventsHeaderFilter.OperationType = FilterOperationType.Or;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);

    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));

    //        Model.EventsHeaderFilter.Children[0].IsActive = true;
    //        list = Model.GetFilteredFiles(FilterOperationType.And);

    //        Assert.AreEqual(3, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));
    //    }

    //    [TestMethod]
    //    public void TestFilterExpressionPlace() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();
    //        DmFile file4 = GetTestPhotoModel();
    //        DmFile file5 = GetTestPhotoModel();
    //        DmFile file6 = GetTestPhotoModel();
    //        DmFile file7 = GetTestPhotoModel();
    //        DmFile file8 = GetTestPhotoModel();
    //        DmFile file9 = GetTestPhotoModel();

    //        file1.Country = "Russia";
    //        file1.Caption = "file1";
    //        file2.Country = "Russia";
    //        file2.Caption = "file2";
    //        file3.Country = "Russia";
    //        file2.Caption = "file3";
    //        file4.Country = "Russia";
    //        file4.Caption = "file4";
    //        file6.Country = "Russia";
    //        file6.Caption = "file6";
    //        file7.Country = "Russia";
    //        file7.Caption = "file7";
    //        file8.Country = "Russia";
    //        file8.Caption = "file8";
    //        {
    //            file1.State = "Tulskaya obl";
    //            file2.State = "Tulskaya obl";
    //            file3.State = "Tulskaya obl";
    //            file6.State = "Tulskaya obl";
    //            file7.State = "Tulskaya obl";
    //            {
    //                file1.City = "Tula";
    //                file2.City = "Tula";
    //                file6.City = "Tula";
    //                {
    //                    file1.Location = "Tsiolkovskogo";
    //                    file2.Location = "Orujeynaya";
    //                }
    //                file3.City = "Aleksin";
    //            }
    //            file4.State = "Orlovskaya obl";
    //            {
    //                file4.City = "Orel";
    //            }
    //        }
    //        file5.Country = "Spain";
    //        {
    //            file5.State = "Mursia";
    //            {
    //                file5.City = "Cartaghena";
    //                {
    //                    file5.Location = "Beach";
    //                }
    //            }
    //        }

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.AddFile(file5);
    //        Model.AddFile(file6);
    //        Model.AddFile(file7);
    //        Model.AddFile(file8);
    //        Model.AddFile(file9);
    //        Model.EndAddFiles();

    //        Filter russia = Model.GetFilter(Model.PlaceFilter, "Russia");
    //        Filter tulObl = Model.GetFilter(russia, "Tulskaya obl");
    //        Filter tula = Model.GetFilter(tulObl, "Tula");
    //        Filter tsiolkovskogo = Model.GetFilter(tula, "Tsiolkovskogo");
    //        Filter orujeynaya = Model.GetFilter(tula, "Orujeynaya");
    //        Filter location = Model.GetFilter(tula, "");

    //        tsiolkovskogo.IsActive = true;
    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(file1, list[0]);

    //        tula.OperationType = FilterOperationType.Or;
    //        orujeynaya.IsActive = true;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));

    //        location.IsActive = true;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(3, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file6));

    //        location.IsActive = false;
    //        orujeynaya.IsActive = false;
    //        tsiolkovskogo.IsActive = false;

    //        tula.IsActive = true;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(3, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file6));

    //        location.IsActive = true;
    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, list.Count);
    //        Assert.AreEqual(true, list.Contains(file6));

    //        Filter aleksin = Model.GetFilter(tulObl, "Aleksin");
    //        Filter orlObl = Model.GetFilter(russia, "Orlovskaya obl");
    //        Filter orel = Model.GetFilter(orlObl, "Orel");

    //        tulObl.IsActive = true;
    //        tulObl.OperationType = FilterOperationType.Or;
    //        aleksin.IsActive = true;

    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file6));
    //        Assert.AreEqual(true, list.Contains(file3));

    //        Filter spain = Model.GetFilter(Model.PlaceFilter, "Spain");
    //        Filter mursia = Model.GetFilter(spain, "Mursia");
    //        Filter cartaghena = Model.GetFilter(mursia, "Cartaghena");

    //        russia.IsActive = false;
    //        russia.OperationType = FilterOperationType.Or;
    //        {
    //            tulObl.IsActive = true;
    //            tulObl.OperationType = FilterOperationType.And;
    //            {
    //                tula.IsActive = false;
    //                tula.OperationType = FilterOperationType.And;
    //                {
    //                    orujeynaya.IsActive = false;
    //                    tsiolkovskogo.IsActive = false;
    //                    location.IsActive = false;
    //                }
    //                aleksin.IsActive = false;
    //            }
    //            orlObl.IsActive = true;
    //            {
    //                orel.IsActive = false;
    //            }
    //        }
    //        spain.IsActive = false;
    //        {
    //            mursia.IsActive = false;
    //            {
    //                cartaghena.IsActive = false;
    //            }
    //        }

    //        Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(6, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));
    //        Assert.AreEqual(true, list.Contains(file6));
    //        Assert.AreEqual(true, list.Contains(file7));
    //        Assert.AreEqual(true, list.Contains(file4));

    //        Model.PlaceFilter.OperationType = FilterOperationType.Or;

    //        russia.IsActive = true;
    //        russia.OperationType = FilterOperationType.And;
    //        {
    //            tulObl.IsActive = false;
    //            tulObl.OperationType = FilterOperationType.And;
    //            {
    //                tula.IsActive = false;
    //                tula.OperationType = FilterOperationType.And;
    //                {
    //                    orujeynaya.IsActive = false;
    //                    tsiolkovskogo.IsActive = false;
    //                    location.IsActive = false;
    //                }
    //                aleksin.IsActive = false;
    //            }
    //            orlObl.IsActive = false;
    //            {
    //                orel.IsActive = false;
    //            }
    //        }
    //        spain.IsActive = false;
    //        {
    //            mursia.IsActive = false;
    //            {
    //                cartaghena.IsActive = true;
    //            }
    //        }

    //        Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(8, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));
    //        Assert.AreEqual(true, list.Contains(file6));
    //        Assert.AreEqual(true, list.Contains(file7));
    //        Assert.AreEqual(true, list.Contains(file4));
    //        Assert.AreEqual(true, list.Contains(file8));
    //        Assert.AreEqual(true, list.Contains(file5));
    //    }

    //    [TestMethod]
    //    public void TestGrouping() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();
    //        DmFile file4 = GetTestPhotoModel();

    //        file1.IsGroupOwner = true;
    //        file2.GroupOwner = file1;
    //        file3.GroupOwner = file1;

    //        Assert.AreEqual(file1.Id, file2.GroupId);
    //        Assert.AreEqual(file1.Id, file3.GroupId);
    //        Assert.AreEqual(true, file2.IsGrouped);
    //        Assert.AreEqual(true, file3.IsGrouped);
    //        Assert.AreEqual(2, file1.GroupedFiles.Count);
    //        Assert.AreEqual(file2, file1.GroupedFiles[0]);
    //        Assert.AreEqual(file3, file1.GroupedFiles[1]);

    //        file2.GroupOwner = file4;
    //        Assert.AreEqual(1, file1.GroupedFiles.Count);
    //        Assert.AreEqual(file3, file1.GroupedFiles[0]);

    //        Assert.AreEqual(1, file4.GroupedFiles.Count);
    //        Assert.AreEqual(file2, file4.GroupedFiles[0]);
    //        Assert.AreEqual(file4.Id, file2.GroupId);
    //        Assert.AreEqual(true, file4.IsGroupOwner);

    //        file3.GroupOwner = file4;
    //        Assert.AreEqual(false, file1.IsGroupOwner);
    //        Assert.AreEqual(0, file1.GroupedFiles.Count);

    //        file2.GroupOwner = null;
    //        file3.GroupOwner = null;

    //        Assert.AreEqual(false, file2.IsGrouped);
    //        Assert.AreEqual(false, file3.IsGrouped);
    //    }

    //    [TestMethod]
    //    public void TestFilterByGrouping() {
    //        DmFile file1 = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();
    //        DmFile file4 = GetTestPhotoModel();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file1);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.EndAddFiles();

    //        Model.GroupFiles(new List<DmFile>(new DmFile[] { file1, file2, file3 }));

    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.Or);
    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file4));

    //        Model.ExpandGroup(new List<DmFile>(new DmFile[] { file1, file4 }));

    //        list = Model.GetFilteredFiles(FilterOperationType.Or);
    //        Assert.AreEqual(4, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file4));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));

    //        Model.CollapseGroup(new List<DmFile>(new DmFile[] { file1, file4 }));
    //        list = Model.GetFilteredFiles(FilterOperationType.Or);
    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file4));

    //        Model.UngroupFiles(new List<DmFile>(new DmFile[] { file1, file4 }));
    //        list = Model.GetFilteredFiles(FilterOperationType.Or);
    //        Assert.AreEqual(4, list.Count);
    //        Assert.AreEqual(true, list.Contains(file1));
    //        Assert.AreEqual(true, list.Contains(file4));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));

    //        foreach(DmFile file in list) {
    //            Assert.AreEqual(false, file.IsGroupOwner);
    //            Assert.AreEqual(null, file.GroupOwner);
    //            Assert.AreEqual(Guid.Empty, file.GroupId);
    //        }
    //    }

    //    [TestMethod]
    //    public void TestFileClone() {
    //        DmFile file = GetTestPhotoModel();

    //        var sourceProperties = typeof(DmFile).GetProperties().Where(p => p.CanRead && p.CanWrite);
    //        int index = 0;
    //        foreach(var property in sourceProperties) {
    //            object value = property.GetValue(file);
    //            if(value is string)
    //                property.SetValue(file, "Alena" + index);
    //            if(value is DmColorLabel)
    //                property.SetValue(file, Model.Context.ColorLabels.Local[3]);
    //            if(value is bool)
    //                property.SetValue(file, true);
    //            if(value is int)
    //                property.SetValue(file, index);
    //            if(value is Guid)
    //                property.SetValue(file, Guid.NewGuid());
    //            if(value is MediaFormat)
    //                property.SetValue(file, Model.Context.MediaFormat.Local[3]);
    //            index++;
    //        }

    //        DmFile file2 = file.Clone();
    //        foreach(var property in sourceProperties) {
    //            if(property.Name == "Id")
    //                continue;
    //            object value = property.GetValue(file);
    //            object value2 = property.GetValue(file2);
    //            Assert.AreEqual(true, object.Equals(value, value2));
    //        }
    //    }
    //    [TestMethod]
    //    public void TestThereIsNoDublicateStringTags() {
    //        DmModel.AllowGenerateDefaultTags = true;
    //        DmModel model2 = new DmModel();
    //        ApplyTestSettings();
    //        model2.ConnectToDataSource();

    //        model2.Context.Configuration.AutoDetectChangesEnabled = false;
    //        Assert.AreEqual(852, model2.Context.Tags.Local.Count); // Change 852 if DefaultTags.xml is changed
    //        for(int i = 0; i < model2.Context.Tags.Local.Count; i++) {
    //            for(int j = i + 1; j < model2.Context.Tags.Local.Count; j++) {
    //                Assert.AreNotEqual(0, string.Compare(model2.Context.Tags.Local[i].Value, model2.Context.Tags.Local[j].Value));
    //            }
    //        }
    //    }
    //    [TestMethod]
    //    public void TestAspectRationIsNonNan() {
    //        Assert.AreEqual(false, float.IsNaN(Model.Properties.AspectRatio));
    //    }
    //    #region Categorized Tags
    //    public void TestCreateTagNodesByAddingTagsCore(TagType type) {
    //        Model.AddTag("MyFamily", type);
    //        Model.AddTag("Arsen", type);
    //        Model.AddTag("Arina", type);
    //        Model.AddTag("Artem", type);
    //        Model.AddTag("Alena", type);

    //        DmTag family = Model.GetTag("MyFamily", type);
    //        DmTag arsen = Model.GetTag("Arsen", type);
    //        DmTag arina = Model.GetTag("Arina", type);
    //        DmTag artem = Model.GetTag("Artem", type);
    //        DmTag alena = Model.GetTag("Alena", type);

    //        DmTagNode familyNode = Model.GetTagNode(family);
    //        DmTagNode arsenNode = Model.GetTagNode(arsen);
    //        DmTagNode arinaNode = Model.GetTagNode(arina);
    //        DmTagNode artemNode = Model.GetTagNode(artem);
    //        DmTagNode alenaNode = Model.GetTagNode(alena);

    //        Assert.AreEqual(type, familyNode.Type);
    //        Assert.AreEqual(type, arsenNode.Type);
    //        Assert.AreEqual(type, arinaNode.Type);
    //        Assert.AreEqual(type, artemNode.Type);
    //        Assert.AreEqual(type, alenaNode.Type);

    //        Assert.AreEqual(5, Model.Context.TagNodes.Local.Count);
    //        Assert.AreEqual(family, familyNode.Tag);
    //        Assert.AreEqual(arsen, arsenNode.Tag);
    //        Assert.AreEqual(arina, arinaNode.Tag);
    //        Assert.AreEqual(artem, artemNode.Tag);
    //        Assert.AreEqual(alena, alenaNode.Tag);

    //        Model.AddChildTag(family, arsen);
    //        Assert.AreEqual(1, familyNode.Children.Count);
    //        Assert.AreEqual(familyNode, arsenNode.Parent);

    //        Model.AddChildTag(family, alena);
    //        Assert.AreEqual(2, familyNode.Children.Count);
    //        Assert.AreEqual(familyNode, alenaNode.Parent);

    //        Model.AddChildTag(family, arina);
    //        Assert.AreEqual(3, familyNode.Children.Count);
    //        Assert.AreEqual(familyNode, arinaNode.Parent);

    //        Model.AddChildTag(family, artem);
    //        Assert.AreEqual(4, familyNode.Children.Count);
    //        Assert.AreEqual(familyNode, artemNode.Parent);
    //    }
    //    [TestMethod]
    //    public void TestCreateTagNodesByAddingTags() {
    //        TestCreateTagNodesByAddingTagsCore(TagType.Tag);
    //    }
    //    [TestMethod]
    //    public void TestCreateAutorNodesByAddingTags() {
    //        TestCreateTagNodesByAddingTagsCore(TagType.Autor);
    //    }
    //    [TestMethod]
    //    public void TestCreateCategoryNodesByAddingTags() {
    //        TestCreateTagNodesByAddingTagsCore(TagType.Category);
    //    }
    //    [TestMethod]
    //    public void TestCreateCollectionNodesByAddingTags() {
    //        TestCreateTagNodesByAddingTagsCore(TagType.Collection);
    //    }
    //    [TestMethod]
    //    public void TestCreateGenreNodesByAddingTags() {
    //        TestCreateTagNodesByAddingTagsCore(TagType.Genre);
    //    }
    //    [TestMethod]
    //    public void TestCreatePeopleNodesByAddingTags() {
    //        TestCreateTagNodesByAddingTagsCore(TagType.People);
    //    }
    //    [TestMethod]
    //    public void TestGetTag() {
    //        Model.AddTag("Nature", TagType.People);
    //        Model.AddTag("Architecture", TagType.Tag);

    //        Assert.AreEqual(null, Model.GetTag("Nature", TagType.Tag));
    //        Assert.AreNotEqual(null, Model.GetTag("Nature", TagType.People));

    //        Assert.AreEqual(null, Model.GetTag("Architecture", TagType.People));
    //        Assert.AreNotEqual(null, Model.GetTag("Architecture", TagType.Tag));
    //    }
    //    public void TestOneTagWithTwoTagNodes_GetReversedNodesCore(TagType type) {
    //        Model.BeginUpdate();
    //        DmTag nature = Model.AddTag("Nature", type);
    //        DmTag arch = Model.AddTag("Architecture", type);
    //        DmTag land = Model.AddTag("Landscape", type);
    //        DmTag build = Model.AddTag("Building", type);
    //        DmTag stone = Model.AddTag("Stone", type);

    //        Model.AddChildTag(nature, land);
    //        Model.AddChildTag(arch, build);
    //        Model.AddChildTag(land, stone);
    //        Model.AddChildTag(build, stone);
    //        Model.EndUpdate();

    //        DmTagNodeReversed stoneNode = Model.GetTagNodeReversed(stone);
    //        DmTagNodeReversed landNode = Model.GetTagNodeReversed(land);
    //        DmTagNodeReversed buildNode = Model.GetTagNodeReversed(build);
    //        DmTagNodeReversed natureNode = Model.GetTagNodeReversed(nature);
    //        DmTagNodeReversed archNode = Model.GetTagNodeReversed(arch);
    //        Assert.AreEqual(2, stoneNode.Children.Count);
    //        Assert.AreEqual(true, stoneNode.Children.Contains(landNode));
    //        Assert.AreEqual(true, stoneNode.Children.Contains(buildNode));
    //        Assert.AreEqual(stoneNode, landNode.Parent);
    //        Assert.AreEqual(stoneNode, buildNode.Parent);
    //        Assert.AreEqual(landNode, natureNode.Parent);
    //        Assert.AreEqual(buildNode, archNode.Parent);
    //    }
    //    [TestMethod]
    //    public void TestOneTagWithTwoTagNodes_GetReversedNodes() {
    //        TestOneTagWithTwoTagNodes_GetReversedNodesCore(TagType.Tag);
    //    }
    //    [TestMethod]
    //    public void TestOneAutorWithTwoAutorNodes_GetReversedNodes() {
    //        TestOneTagWithTwoTagNodes_GetReversedNodesCore(TagType.Autor);
    //    }
    //    [TestMethod]
    //    public void TestOneCategoryWithTwoCategoryNodes_GetReversedNodes() {
    //        TestOneTagWithTwoTagNodes_GetReversedNodesCore(TagType.Category);
    //    }
    //    [TestMethod]
    //    public void TestOneCollectionWithTwoCollectionNodes_GetReversedNodes() {
    //        TestOneTagWithTwoTagNodes_GetReversedNodesCore(TagType.Collection);
    //    }
    //    [TestMethod]
    //    public void TestOneGenreWithTwoGenreNodes_GetReversedNodes() {
    //        TestOneTagWithTwoTagNodes_GetReversedNodesCore(TagType.Genre);
    //    }
    //    [TestMethod]
    //    public void TestOnePeopleWithTwoPeopleNodes_GetReversedNodes() {
    //        TestOneTagWithTwoTagNodes_GetReversedNodesCore(TagType.People);
    //    }
    //    DmTagNodeReversed GetTagNode(IEnumerable<DmTagNodeReversed> nodes, string tagText) {
    //        return nodes.FirstOrDefault(n => n.Tag.Value == tagText);
    //    }
    //    public void TestSuggestedTagsCore(TagType type) {
    //        Model.BeginUpdate();
    //        Model.AddTag("Nature", type);
    //        Model.AddTag("Architecture", type);
    //        Model.AddTag("Landscape", type);
    //        Model.AddTag("Building", type);
    //        Model.AddTag("Stone", type);
    //        Model.AddTag("Invalid", type == TagType.Tag ? TagType.Autor : TagType.Tag);

    //        Model.AddChildTag(Model.GetTag("Nature", type), Model.GetTag("Landscape", type));
    //        Model.AddChildTag(Model.GetTag("Architecture", type), Model.GetTag("Building", type));
    //        Model.AddChildTag(Model.GetTag("Landscape", type), Model.GetTag("Stone", type));
    //        Model.AddChildTag(Model.GetTag("Building", type), Model.GetTag("Stone", type));
    //        Model.EndUpdate();

    //        DmTag stone = Model.GetTag("Stone", type);

    //        IEnumerable<DmTagNodeReversed> suggestedTags = Model.GetSuggestedTags(stone);
    //        Assert.AreEqual(4, suggestedTags.Count());
    //        Assert.AreNotEqual(null, GetTagNode(suggestedTags, "Building"));
    //        Assert.AreNotEqual(null, GetTagNode(suggestedTags, "Landscape"));
    //        Assert.AreNotEqual(null, GetTagNode(suggestedTags, "Nature"));
    //        Assert.AreNotEqual(null, GetTagNode(suggestedTags, "Architecture"));

    //        Model.BeginUpdate();
    //        Model.AddTag("City", type);
    //        Model.AddChildTag(Model.GetTag("City", type), Model.GetTag("Building", type));
    //        Model.EndUpdate();

    //        DmTag building = Model.GetTag("Building", type);
    //        suggestedTags = Model.GetSuggestedTags(building);
    //        Assert.AreEqual(2, suggestedTags.Count());
    //        Assert.AreNotEqual(null, GetTagNode(suggestedTags, "Architecture"));
    //        Assert.AreNotEqual(null, GetTagNode(suggestedTags, "City"));
    //    }
    //    [TestMethod]
    //    public void TestSuggestedTags() {
    //        TestSuggestedTagsCore(TagType.Tag);
    //    }
    //    [TestMethod]
    //    public void TestSuggestedAutors() {
    //        TestSuggestedTagsCore(TagType.Autor);
    //    }
    //    [TestMethod]
    //    public void TestSuggestedCategories() {
    //        TestSuggestedTagsCore(TagType.Category);
    //    }
    //    [TestMethod]
    //    public void TestSuggestedCollection() {
    //        TestSuggestedTagsCore(TagType.Collection);
    //    }
    //    [TestMethod]
    //    public void TestSuggestedGenre() {
    //        TestSuggestedTagsCore(TagType.Genre);
    //    }
    //    [TestMethod]
    //    public void TestSuggestedPeople() {
    //        TestSuggestedTagsCore(TagType.People);
    //    }

    //    public void TestMostRecentUsedTagsCore(TagType type) {
    //        Model.BeginUpdate();
    //        Model.AddTag("Nature", type);
    //        Model.AddTag("Architecture", type);
    //        Model.AddTag("Landscape", type);
    //        Model.AddTag("Building", type);
    //        Model.AddTag("Stone", type);
    //        Model.AddTag("Invalid", type == TagType.Tag ? TagType.Autor : TagType.Tag);

    //        Model.AddChildTag(Model.GetTag("Nature", type), Model.GetTag("Landscape", type));
    //        Model.AddChildTag(Model.GetTag("Architecture", type), Model.GetTag("Building", type));
    //        Model.AddChildTag(Model.GetTag("Landscape", type), Model.GetTag("Stone", type));
    //        Model.AddChildTag(Model.GetTag("Building", type), Model.GetTag("Stone", type));

    //        Model.AddTag("City", type);
    //        Model.AddChildTag(Model.GetTag("City", type), Model.GetTag("Building", type));
    //        Model.EndUpdate();

    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        List<DmTag> tagsToAdd = new List<DmTag>();
    //        tagsToAdd.Add(Model.GetTag("Stone", type));

    //        Model.BeginUpdateFile(file);
    //        Model.AddKeywords(file, tagsToAdd, type);
    //        Model.EndUpdateFile(file);
    //        Assert.AreEqual(1, Model.GetTag("Stone", type).AddCount);

    //        tagsToAdd.Add(Model.GetTag("Building", type));
    //        Model.BeginUpdateFile(file2);
    //        Model.AddKeywords(file2, tagsToAdd, type);
    //        Model.EndUpdateFile(file2);

    //        Assert.AreEqual(2, Model.GetTag("Stone", type).AddCount);
    //        Assert.AreEqual(1, Model.GetTag("Building", type).AddCount);
    //    }
    //    [TestMethod]
    //    public void TestMostRecentUsedAutors() {
    //        TestMostRecentUsedTagsCore(TagType.Autor);
    //    }
    //    [TestMethod]
    //    public void TestMostRecentUsedCategories() {
    //        TestMostRecentUsedTagsCore(TagType.Category);
    //    }
    //    [TestMethod]
    //    public void TestMostRecentUsedCollections() {
    //        TestMostRecentUsedTagsCore(TagType.Collection);
    //    }
    //    [TestMethod]
    //    public void TestMostRecentUsedGenres() {
    //        TestMostRecentUsedTagsCore(TagType.Genre);
    //    }
    //    [TestMethod]
    //    public void TestMostRecentUsedPeoples() {
    //        TestMostRecentUsedTagsCore(TagType.People);
    //    }
    //    [TestMethod]
    //    public void TestMostRecentUsedTags() {
    //        TestMostRecentUsedTagsCore(TagType.Tag);
    //    }
    //    public void TestLastAddedTagsCore(TagType type) {
    //        Model.BeginUpdate();
    //        Model.AddTag("Nature", type);
    //        Model.AddTag("Architecture", type);
    //        Model.AddTag("Landscape", type);
    //        Model.AddTag("Building", type);
    //        Model.AddTag("Stone", type);

    //        Model.AddChildTag(Model.GetTag("Nature", type), Model.GetTag("Landscape", type));
    //        Model.AddChildTag(Model.GetTag("Architecture", type), Model.GetTag("Building", type));
    //        Model.AddChildTag(Model.GetTag("Landscape", type), Model.GetTag("Stone", type));
    //        Model.AddChildTag(Model.GetTag("Building", type), Model.GetTag("Stone", type));

    //        Model.AddTag("City", type);
    //        Model.AddChildTag(Model.GetTag("City", type), Model.GetTag("Building", type));
    //        Model.EndUpdate();

    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        List<DmTag> tagsToAdd = new List<DmTag>();
    //        tagsToAdd.Add(Model.GetTag("Stone", type));

    //        Model.BeginUpdateFile(file);
    //        Model.AddKeywords(file, tagsToAdd, type);
    //        Model.EndUpdateFile(file);

    //        tagsToAdd.Add(Model.GetTag("Building", type));
    //        Model.BeginUpdateFile(file2);
    //        Model.AddKeywords(file2, tagsToAdd, type);
    //        Model.EndUpdateFile(file2);

    //        var sorted = (from tag in Model.Context.Tags
    //                      orderby tag.TimeStamp descending
    //                      select tag).ToList();

    //        Assert.AreEqual(Model.GetTag("Building", type), sorted[0]);
    //        Assert.AreEqual(Model.GetTag("Stone", type), sorted[1]);
    //    }
    //    [TestMethod]
    //    public void TestLastAddedAutors() {
    //        TestLastAddedTagsCore(TagType.Autor);
    //    }
    //    [TestMethod]
    //    public void TestLastAddedCategories() {
    //        TestLastAddedTagsCore(TagType.Category);
    //    }
    //    [TestMethod]
    //    public void TestLastAddedCollections() {
    //        TestLastAddedTagsCore(TagType.Collection);
    //    }
    //    [TestMethod]
    //    public void TestLastAddedGenres() {
    //        TestLastAddedTagsCore(TagType.Genre);
    //    }
    //    [TestMethod]
    //    public void TestLastAddedPeoples() {
    //        TestLastAddedTagsCore(TagType.People);
    //    }
    //    [TestMethod]
    //    public void TestLastAddedTags() {
    //        TestLastAddedTagsCore(TagType.Tag);
    //    }
    //    public void TestFilterWhenFileAdded_Hierarchically_Tag(TagType type, Filter headerFilter) {
    //        Model.BeginUpdate();
    //        DmTag nature = Model.AddTag("Nature", type);
    //        DmTag arch = Model.AddTag("Architecture", type);
    //        DmTag land = Model.AddTag("Landscape", type);
    //        DmTag build = Model.AddTag("Building", type);
    //        DmTag stone = Model.AddTag("Stone", type);

    //        Model.AddChildTag(nature, land);
    //        Model.AddChildTag(arch, build);
    //        Model.AddChildTag(land, stone);
    //        Model.AddChildTag(build, stone);

    //        DmTag city = Model.AddTag("City", type);
    //        Model.AddChildTag(city, build);
    //        Model.EndUpdate();

    //        DmFile file = GetTestPhotoModel();
    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(stone);
    //        tags.Add(land);
    //        tags.Add(nature);
    //        tags.Add(arch);
    //        Model.AddKeywords(file, tags, type);

    //        Model.AllowHierarchicallyKeywordFilters = true;
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Filter fStone = Model.GetFilter(headerFilter, stone);
    //        Assert.AreEqual(1, fStone.MatchedCount);
    //        Assert.AreEqual(2, fStone.Children.Count);

    //        Filter fLand = Model.GetFilter(fStone, land);
    //        Filter fNature = Model.GetFilter(fLand, nature);

    //        Filter fBuild = Model.GetFilter(fStone, build);
    //        Filter fArch = Model.GetFilter(fBuild, arch);

    //        Filter fCity = Model.GetFilter(fBuild, city);

    //        Assert.AreEqual(1, fBuild.Children.Count);

    //        Assert.AreEqual(1, fLand.MatchedCount);
    //        Assert.AreEqual(0, fBuild.MatchedCount);
    //        Assert.AreEqual(1, fNature.MatchedCount);
    //        Assert.AreEqual(1, fArch.MatchedCount);
    //        Assert.AreEqual(null, fCity);

    //        DmFile file2 = GetTestPhotoModel();
    //        tags = new List<DmTag>();
    //        tags.Add(city);
    //        Model.AddKeywords(file2, tags, type);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file2);
    //        Model.EndAddFiles();

    //        fCity = Model.GetFilter(fBuild, city);
    //        Assert.AreEqual(1, fCity.MatchedCount);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileAdded_Hierarchically_Tag() {
    //        TestFilterWhenFileAdded_Hierarchically_Tag(TagType.Tag, Model.TagsHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileAdded_Hierarchically_Autor() {
    //        TestFilterWhenFileAdded_Hierarchically_Tag(TagType.Autor, Model.AutorsHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileAdded_Hierarchically_Category() {
    //        TestFilterWhenFileAdded_Hierarchically_Tag(TagType.Category, Model.CategoriesHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileAdded_Hierarchically_Collection() {
    //        TestFilterWhenFileAdded_Hierarchically_Tag(TagType.Collection, Model.CollectionsHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileAdded_Hierarchically_Genre() {
    //        TestFilterWhenFileAdded_Hierarchically_Tag(TagType.Genre, Model.GenresHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileAdded_Hierarchically_People() {
    //        TestFilterWhenFileAdded_Hierarchically_Tag(TagType.People, Model.PeoplesHeaderFilter);
    //    }
    //    public void TestFilterWhenFileRemoved_Hierarchically_Tag(TagType type, Filter headerFilter) {
    //        Model.BeginUpdate();
    //        DmTag nature = Model.AddTag("Nature", type);
    //        DmTag arch = Model.AddTag("Architecture", type);
    //        DmTag land = Model.AddTag("Landscape", type);
    //        DmTag build = Model.AddTag("Building", type);
    //        DmTag stone = Model.AddTag("Stone", type);

    //        Model.AddChildTag(nature, land);
    //        Model.AddChildTag(arch, build);
    //        Model.AddChildTag(land, stone);
    //        Model.AddChildTag(build, stone);

    //        DmTag city = Model.AddTag("City", type);
    //        Model.AddChildTag(city, build);
    //        Model.EndUpdate();

    //        DmFile file = GetTestPhotoModel();
    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(stone);
    //        tags.Add(arch);
    //        Model.AddKeywords(file, tags, type);

    //        DmFile file2 = GetTestPhotoModel();
    //        tags = new List<DmTag>();
    //        tags.Add(nature);

    //        Model.AddKeywords(file2, tags, type);

    //        DmFile file3 = GetTestPhotoModel();
    //        tags = new List<DmTag>();
    //        tags.Add(land);
    //        Model.AddKeywords(file3, tags, type);

    //        DmFile file4 = GetTestPhotoModel();
    //        tags = new List<DmTag>();
    //        tags.Add(nature);
    //        Model.AddKeywords(file4, tags, type);

    //        Model.AllowHierarchicallyKeywordFilters = true;
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.EndAddFiles();

    //        Filter fStone = Model.GetFilter(headerFilter, stone);
    //        Assert.AreEqual(1, fStone.MatchedCount);
    //        Assert.AreEqual(2, fStone.Children.Count);

    //        Filter fLand = Model.GetFilter(fStone, land);
    //        Filter fNature = Model.GetFilter(fLand, nature);

    //        Assert.AreEqual(1, fLand.MatchedCount);
    //        Assert.AreEqual(2, fNature.MatchedCount);

    //        Filter fBuild = Model.GetFilter(fStone, build);
    //        Filter fArch = Model.GetFilter(fBuild, arch);

    //        Filter fCity = Model.GetFilter(fBuild, city);

    //        Assert.AreEqual(2, fNature.MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file3);
    //        Model.EndUpdate();

    //        fLand = Model.GetFilter(fStone, land);
    //        Assert.AreEqual(0, fLand.MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file2);
    //        Model.EndUpdate();

    //        Assert.AreEqual(1, fNature.MatchedCount);
    //        Assert.AreEqual(0, fLand.MatchedCount);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file4);
    //        Model.EndUpdate();

    //        Assert.AreEqual(0, fLand.MatchedCount);
    //        Assert.AreEqual(0, fNature.MatchedCount);

    //        fLand = Model.GetFilter(fStone, land);
    //        Assert.AreEqual(null, fLand);

    //        Model.BeginUpdate();
    //        Model.RemoveFile(file);
    //        Model.EndUpdate();

    //        fStone = Model.GetFilter(headerFilter, stone);
    //        Assert.AreEqual(null, fStone);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileRemoved_Hierarchically_Tag() {
    //        TestFilterWhenFileRemoved_Hierarchically_Tag(TagType.Tag, Model.TagsHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileRemoved_Hierarchically_Autor() {
    //        TestFilterWhenFileRemoved_Hierarchically_Tag(TagType.Autor, Model.AutorsHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileRemoved_Hierarchically_Category() {
    //        TestFilterWhenFileRemoved_Hierarchically_Tag(TagType.Category, Model.CategoriesHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileRemoved_Hierarchically_Collection() {
    //        TestFilterWhenFileRemoved_Hierarchically_Tag(TagType.Collection, Model.CollectionsHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileRemoved_Hierarchically_Genre() {
    //        TestFilterWhenFileRemoved_Hierarchically_Tag(TagType.Genre, Model.GenresHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileRemoved_Hierarchically_People() {
    //        TestFilterWhenFileRemoved_Hierarchically_Tag(TagType.People, Model.PeoplesHeaderFilter);
    //    }
    //    public void TestFilterWhenFileUpdated_Hierarchically(TagType type, Filter header) {
    //        Model.AllowHierarchicallyKeywordFilters = true;

    //        DmFile file = GetTestPhotoModel();
    //        DmTag stone = Model.AddTag("Stone", type);
    //        DmTag land = Model.AddTag("Landscape", type);
    //        DmTag nature = Model.AddTag("Nature", type);
    //        DmTag build = Model.AddTag("Building", type);
    //        DmTag arch = Model.AddTag("Architecture", type);

    //        Model.AddChildTag(nature, land);
    //        Model.AddChildTag(land, stone);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(stone);
    //        tags.Add(land);

    //        Model.AddKeywords(file, tags, type);
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Filter fStone = Model.GetFilter(header, stone);
    //        Filter fLand = Model.GetFilter(header, land);
    //        Filter fLand2 = Model.GetFilter(fStone, land);

    //        Assert.AreEqual(1, fStone.MatchedCount);
    //        Assert.AreEqual(1, fLand2.MatchedCount);
    //        Assert.AreEqual(1, fStone.Children.Count);
    //        Assert.AreEqual(0, fLand2.Children.Count);

    //        Model.BeginUpdateFile(file);
    //        tags = new List<DmTag>();
    //        tags.Add(nature);
    //        Model.AddKeywords(file, tags, type);
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(1, fStone.MatchedCount);
    //        Assert.AreEqual(1, fLand2.MatchedCount);
    //        Assert.AreEqual(1, fStone.Children.Count);
    //        Assert.AreEqual(1, fLand2.Children.Count);

    //        Filter fNature2 = Model.GetFilter(fLand2, nature);
    //        Assert.AreEqual(1, fNature2.MatchedCount);

    //        Filter fNature = Model.GetFilter(fLand, nature);
    //        Assert.AreEqual(1, fNature.MatchedCount);
    //    }
    //    public void TestFilterWhenFileUpdated_Hierarchically2(TagType type, Filter header) {
    //        Model.AllowHierarchicallyKeywordFilters = true;

    //        DmFile file = GetTestPhotoModel();
    //        DmTag stone = Model.AddTag("Stone", type);
    //        DmTag land = Model.AddTag("Landscape", type);
    //        DmTag nature = Model.AddTag("Nature", type);
    //        DmTag build = Model.AddTag("Building", type);
    //        DmTag arch = Model.AddTag("Architecture", type);

    //        Model.AddChildTag(nature, land);
    //        Model.AddChildTag(land, stone);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(nature);

    //        Model.AddKeywords(file, tags, type);
    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Filter fStone = Model.GetFilter(header, stone);
    //        Filter fLand = Model.GetFilter(header, land);
    //        Filter fStone_fLand = Model.GetFilter(fStone, land);
    //        Filter fStone_fLand_fNature = Model.GetFilter(fStone_fLand, nature);
    //        Filter fLand_fNature = Model.GetFilter(fLand, nature);
    //        Filter fNature = Model.GetFilter(header, nature);

    //        Assert.AreEqual(false, fStone.Enabled);
    //        Assert.AreEqual(0, fStone.MatchedCount);
    //        Assert.AreEqual(false, fStone_fLand.Enabled);
    //        Assert.AreEqual(0, fStone_fLand.MatchedCount);
    //        Assert.AreEqual(false, fStone_fLand_fNature.Enabled);
    //        Assert.AreEqual(1, fStone_fLand_fNature.MatchedCount);

    //        Assert.AreEqual(false, fLand.Enabled);
    //        Assert.AreEqual(0, fLand.MatchedCount);
    //        Assert.AreEqual(false, fLand_fNature.Enabled);
    //        Assert.AreEqual(1, fLand_fNature.MatchedCount);

    //        Assert.AreEqual(true, fNature.Enabled);
    //        Assert.AreEqual(1, fNature.MatchedCount);

    //        Model.BeginUpdateFile(file);
    //        Model.AddKeyword(file, land, type);
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(false, fStone.Enabled);
    //        Assert.AreEqual(0, fStone.MatchedCount);
    //        Assert.AreEqual(false, fStone_fLand.Enabled);
    //        Assert.AreEqual(1, fStone_fLand.MatchedCount);
    //        Assert.AreEqual(false, fStone_fLand_fNature.Enabled);
    //        Assert.AreEqual(1, fStone_fLand_fNature.MatchedCount);

    //        Assert.AreEqual(true, fLand.Enabled);
    //        Assert.AreEqual(1, fLand.MatchedCount);
    //        Assert.AreEqual(true, fLand_fNature.Enabled);
    //        Assert.AreEqual(1, fLand_fNature.MatchedCount);

    //        Assert.AreEqual(true, fNature.Enabled);
    //        Assert.AreEqual(1, fNature.MatchedCount);

    //        Model.BeginUpdateFile(file);
    //        Model.AddKeyword(file, stone, type);
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(true, header.Children.Contains(fStone));
    //        Assert.AreEqual(true, fStone.Enabled);
    //        Assert.AreEqual(1, fStone.MatchedCount);
    //        Assert.AreEqual(true, fStone_fLand.Enabled);
    //        Assert.AreEqual(1, fStone_fLand.MatchedCount);
    //        Assert.AreEqual(true, fStone_fLand_fNature.Enabled);
    //        Assert.AreEqual(1, fStone_fLand_fNature.MatchedCount);

    //        Assert.AreEqual(true, header.Children.Contains(fLand));
    //        Assert.AreEqual(true, fLand.Enabled);
    //        Assert.AreEqual(1, fLand.MatchedCount);
    //        Assert.AreEqual(true, fLand_fNature.Enabled);
    //        Assert.AreEqual(1, fLand_fNature.MatchedCount);

    //        Assert.AreEqual(true, header.Children.Contains(fNature));
    //        Assert.AreEqual(true, fNature.Enabled);
    //        Assert.AreEqual(1, fNature.MatchedCount);

    //        Model.BeginUpdateFile(file);
    //        Model.RemoveKeyword(file, land, type);
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(true, header.Children.Contains(fStone));
    //        Assert.AreEqual(true, fStone.Enabled);
    //        Assert.AreEqual(1, fStone.MatchedCount);
    //        Assert.AreEqual(true, fStone_fLand.Enabled);
    //        Assert.AreEqual(0, fStone_fLand.MatchedCount);
    //        Assert.AreEqual(true, fStone_fLand_fNature.Enabled);
    //        Assert.AreEqual(1, fStone_fLand_fNature.MatchedCount);

    //        Assert.AreEqual(true, header.Children.Contains(fLand));
    //        Assert.AreEqual(false, fLand.Enabled);
    //        Assert.AreEqual(0, fLand.MatchedCount);
    //        Assert.AreEqual(false, fLand_fNature.Enabled);
    //        Assert.AreEqual(1, fLand_fNature.MatchedCount);

    //        Assert.AreEqual(true, header.Children.Contains(fNature));
    //        Assert.AreEqual(true, fNature.Enabled);
    //        Assert.AreEqual(1, fNature.MatchedCount);


    //        Model.BeginUpdateFile(file);
    //        Model.RemoveKeyword(file, stone, type);
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(true, header.Children.Contains(fStone));
    //        Assert.AreEqual(false, fStone.Enabled);
    //        Assert.AreEqual(0, fStone.MatchedCount);
    //        Assert.AreEqual(false, fStone_fLand.Enabled);
    //        Assert.AreEqual(0, fStone_fLand.MatchedCount);
    //        Assert.AreEqual(false, fStone_fLand_fNature.Enabled);
    //        Assert.AreEqual(1, fStone_fLand_fNature.MatchedCount);

    //        Assert.AreEqual(true, header.Children.Contains(fLand));
    //        Assert.AreEqual(false, fLand.Enabled);
    //        Assert.AreEqual(0, fLand.MatchedCount);
    //        Assert.AreEqual(false, fLand_fNature.Enabled);
    //        Assert.AreEqual(1, fLand_fNature.MatchedCount);

    //        Assert.AreEqual(true, header.Children.Contains(fNature));
    //        Assert.AreEqual(true, fNature.Enabled);
    //        Assert.AreEqual(1, fNature.MatchedCount);

    //        Model.BeginUpdateFile(file);
    //        Model.RemoveKeyword(file, nature, type);
    //        Model.EndUpdateFile(file);

    //        Assert.AreEqual(false, header.Children.Contains(fStone));
    //        Assert.AreEqual(false, header.Children.Contains(fLand));
    //        Assert.AreEqual(false, header.Children.Contains(fNature));
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Tags() {
    //        TestFilterWhenFileUpdated_Hierarchically(TagType.Tag, Model.TagsHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Tags2() {
    //        TestFilterWhenFileUpdated_Hierarchically2(TagType.Tag, Model.TagsHeaderFilter);
    //    }

    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Autors() {
    //        TestFilterWhenFileUpdated_Hierarchically(TagType.Autor, Model.AutorsHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Autors2() {
    //        TestFilterWhenFileUpdated_Hierarchically2(TagType.Autor, Model.AutorsHeaderFilter);
    //    }

    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Categories() {
    //        TestFilterWhenFileUpdated_Hierarchically(TagType.Category, Model.CategoriesHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Categories2() {
    //        TestFilterWhenFileUpdated_Hierarchically2(TagType.Category, Model.CategoriesHeaderFilter);
    //    }

    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Collections() {
    //        TestFilterWhenFileUpdated_Hierarchically(TagType.Collection, Model.CollectionsHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Collections2() {
    //        TestFilterWhenFileUpdated_Hierarchically2(TagType.Collection, Model.CollectionsHeaderFilter);
    //    }

    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Genres() {
    //        TestFilterWhenFileUpdated_Hierarchically(TagType.Genre, Model.GenresHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Genres2() {
    //        TestFilterWhenFileUpdated_Hierarchically2(TagType.Genre, Model.GenresHeaderFilter);
    //    }

    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Peoples() {
    //        TestFilterWhenFileUpdated_Hierarchically(TagType.People, Model.PeoplesHeaderFilter);
    //    }
    //    [TestMethod]
    //    public void TestFilterWhenFileUpdated_Hierarchically_Peoples2() {
    //        TestFilterWhenFileUpdated_Hierarchically2(TagType.People, Model.PeoplesHeaderFilter);
    //    }

    //    public void TestFilterExpressionTagsCore2(Filter tagParentFilter, TagType type) {
    //        Model.AllowHierarchicallyKeywordFilters = true;
    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel2();
    //        DmFile file3 = GetTestPhotoModel2();
    //        DmFile file4 = GetTestPhotoModel2();

    //        Model.AddTag("Arsen", type);
    //        Model.AddTag("Arina", type);
    //        Model.AddTag("Artem", type);

    //        List<DmTag> tags = new List<DmTag>();
    //        tags.Add(Model.Context.Tags.Local[0]);
    //        tags.Add(Model.Context.Tags.Local[1]);
    //        tags.Add(Model.Context.Tags.Local[2]);

    //        Model.AddKeywords(file, tags, type);
    //        Model.AddKeywords(file2, tags, type);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.AddFile(file4);
    //        Model.EndAddFiles();

    //        Filter arsenFilter = Model.GetFilter(tagParentFilter, Model.Context.Tags.Local[0]);
    //        arsenFilter.IsActive = true;

    //        List<DmFile> list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(2, list.Count);
    //        Assert.AreEqual(true, list.Contains(file));
    //        Assert.AreEqual(true, list.Contains(file2));

    //        Filter unassigned = Model.GetFilter(tagParentFilter, null);
    //        unassigned.IsActive = true;

    //        tagParentFilter.OperationType = FilterOperationType.Or;
    //        list = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(4, list.Count);
    //        Assert.AreEqual(true, list.Contains(file));
    //        Assert.AreEqual(true, list.Contains(file2));
    //        Assert.AreEqual(true, list.Contains(file3));
    //        Assert.AreEqual(true, list.Contains(file4));
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionHierarchically_Tags() {
    //        TestFilterExpressionTagsCore(Model.TagsHeaderFilter, TagType.Tag);
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionHierarchically_Autors() {
    //        TestFilterExpressionTagsCore(Model.AutorsHeaderFilter, TagType.Autor);
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionHierarchically_Categories() {
    //        TestFilterExpressionTagsCore(Model.CategoriesHeaderFilter, TagType.Category);
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionHierarchically_Collections() {
    //        TestFilterExpressionTagsCore(Model.CollectionsHeaderFilter, TagType.Collection);
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionHierarchically_Genres() {
    //        TestFilterExpressionTagsCore(Model.GenresHeaderFilter, TagType.Genre);
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionHierarchically_Peoples() {
    //        TestFilterExpressionTagsCore(Model.PeoplesHeaderFilter, TagType.People);
    //    }

    //    public void TestFilterExpressionTagsCore3(Filter header, TagType type) {
    //        Model.AllowHierarchicallyKeywordFilters = true;

    //        DmTag stone = Model.AddTag("Stone", type);
    //        DmTag land = Model.AddTag("Landscape", type);
    //        DmTag nature = Model.AddTag("Nature", type);
    //        DmTag build = Model.AddTag("Building", type);
    //        DmTag arch = Model.AddTag("Architecture", type);

    //        Model.AddChildTag(nature, land);
    //        Model.AddChildTag(land, stone);

    //        DmFile file = GetTestPhotoModel();
    //        DmFile file2 = GetTestPhotoModel();
    //        DmFile file3 = GetTestPhotoModel();

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.AddFile(file2);
    //        Model.AddFile(file3);
    //        Model.EndAddFiles();

    //        Model.BeginUpdateFile(file);
    //        Model.AddKeyword(file, nature, type);
    //        Model.EndUpdateFile(file);

    //        List<DmFile> files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(3, files.Count);

    //        Filter fUnassigned = Model.GetFilter(header, null);
    //        Assert.AreNotEqual(null, fUnassigned);
    //        Assert.AreEqual(2, fUnassigned.MatchedCount);

    //        fUnassigned.IsActive = true;
    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(2, files.Count);
    //        Assert.AreEqual(false, files.Contains(file));

    //        header.OperationType = FilterOperationType.Or;
    //        Filter fNature = Model.GetFilter(header, nature);
    //        fNature.IsActive = true;
    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(3, files.Count);

    //        fNature.IsActive = false;
    //        Filter fLand = Model.GetFilter(header, land);
    //        Filter fLand_fNature = Model.GetFilter(fLand, nature);
    //        fLand_fNature.IsActive = true;

    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(2, files.Count);
    //        Assert.AreEqual(false, files.Contains(file));

    //        fLand.IsActive = true;
    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(2, files.Count);
    //        Assert.AreEqual(false, files.Contains(file));

    //        Model.BeginUpdateFile(file);
    //        Model.AddKeyword(file, land, type);
    //        Model.EndUpdateFile(file);

    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(3, files.Count);
    //        fLand.IsActive = false;
    //        fLand_fNature.IsActive = false;

    //        Filter fStone = Model.GetFilter(header, stone);
    //        Filter fStone_fLand = Model.GetFilter(fStone, land);
    //        Filter fStone_fLand_fNature = Model.GetFilter(fStone_fLand, nature);

    //        fUnassigned.IsActive = false;
    //        fStone.IsActive = true;

    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(0, files.Count);

    //        Model.BeginUpdateFile(file);
    //        Model.AddKeyword(file, stone, type);
    //        Model.EndUpdateFile(file);

    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, files.Count);
    //        Assert.AreEqual(true, files.Contains(file));

    //        fStone_fLand.IsActive = true;
    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, files.Count);
    //        Assert.AreEqual(true, files.Contains(file));

    //        Model.BeginUpdateFile(file);
    //        Model.RemoveKeyword(file, land, type);
    //        Model.EndUpdateFile(file);

    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(0, files.Count);

    //        fStone_fLand_fNature.IsActive = true;
    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(0, files.Count);

    //        Model.BeginUpdateFile(file);
    //        Model.AddKeyword(file, land, type);
    //        Model.RemoveKeyword(file, nature, type);
    //        Model.EndUpdateFile(file);

    //        fStone_fLand.IsActive = true;
    //        fStone_fLand_fNature.IsActive = false;

    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, files.Count);
    //        Assert.AreEqual(true, files.Contains(file));

    //        fStone_fLand_fNature.IsActive = true;
    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, files.Count);
    //        Assert.AreEqual(true, files.Contains(file));
    //        Assert.AreEqual(0, fStone_fLand.Children.Count);

    //        Model.BeginUpdateFile(file);
    //        Model.AddKeyword(file, nature, type);
    //        Model.EndUpdateFile(file);

    //        fStone_fLand_fNature = Model.GetFilter(fStone_fLand, nature);
    //        fStone_fLand_fNature.IsActive = false;

    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, files.Count);
    //        Assert.AreEqual(true, files.Contains(file));

    //        fStone_fLand_fNature.IsActive = true;
    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, files.Count);
    //        Assert.AreEqual(true, files.Contains(file));

    //        Model.BeginUpdateFile(file2);
    //        Model.AddKeyword(file, nature, type);
    //        Model.EndUpdateFile(file2);

    //        Model.BeginUpdateFile(file);
    //        Model.RemoveKeyword(file, nature, type);
    //        Model.EndUpdateFile(file);

    //        fStone_fLand_fNature.IsActive = false;
    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(1, files.Count);
    //        Assert.AreEqual(true, files.Contains(file));

    //        fStone_fLand_fNature.IsActive = true;
    //        files = Model.GetFilteredFiles(FilterOperationType.And);
    //        Assert.AreEqual(null, fStone_fLand_fNature.Parent);
    //        Assert.AreEqual(1, files.Count);
    //        Assert.AreEqual(true, files.Contains(file));
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionTags_Hierarchically_Tags() {
    //        TestFilterExpressionTagsCore3(Model.TagsHeaderFilter, TagType.Tag);
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionTags_Hierarchically_Autors() {
    //        TestFilterExpressionTagsCore3(Model.AutorsHeaderFilter, TagType.Autor);
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionTags_Hierarchically_Categories() {
    //        TestFilterExpressionTagsCore3(Model.CategoriesHeaderFilter, TagType.Category);
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionTags_Hierarchically_Collections() {
    //        TestFilterExpressionTagsCore3(Model.CollectionsHeaderFilter, TagType.Collection);
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionTags_Hierarchically_Genres() {
    //        TestFilterExpressionTagsCore3(Model.GenresHeaderFilter, TagType.Genre);
    //    }
    //    [TestMethod]
    //    public void TestFilterExpressionTags_Hierarchically_Peoples() {
    //        TestFilterExpressionTagsCore3(Model.PeoplesHeaderFilter, TagType.People);
    //    }
    //    #endregion
    //    [TestMethod]
    //    public void TestTagSelectionControl_SelectToken() {
    //        TagSelectionControl control = new TagSelectionControl();
    //        control.Model = Model;
    //        control.TagType = TagType.Tag;

    //        DmFile file = GetTestPhotoModel();
    //        DmTag stone = Model.AddTag("Stone", TagType.Tag);
    //        DmTag land = Model.AddTag("Landscape", TagType.Tag);
    //        DmTag nature = Model.AddTag("Nature", TagType.Tag);
    //        DmTag build = Model.AddTag("Building", TagType.Tag);
    //        DmTag arch = Model.AddTag("Architecture", TagType.Tag);
    //        DmTag city = Model.AddTag("City", TagType.Tag);

    //        Model.AddChildTag(nature, land);
    //        Model.AddChildTag(land, stone);

    //        Model.AddChildTag(arch, build);
    //        Model.AddChildTag(build, stone);

    //        Model.AddChildTag(city, build);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        Model.BeginUpdateFile(file);
    //        Model.AddKeyword(file, stone, TagType.Tag);
    //        Model.AddKeyword(file, build, TagType.Tag);
    //        Model.EndUpdateFile(file);

    //        control.File = file;
    //        IList<DmTag> tags = (IList<DmTag>)control.teAssignedKeywords.EditValue;
    //        Assert.AreNotEqual(null, tags);
    //        Assert.AreEqual(2, tags.Count);
    //        Assert.AreEqual(true, tags.Contains(stone));
    //        Assert.AreEqual(true, tags.Contains(build));
    //        Assert.AreEqual(1, control.teAssignedKeywords.Properties.CheckedItems.Count);
    //        Assert.AreEqual(stone, control.teAssignedKeywords.Properties.CheckedItems[0].Value);

    //        Assert.AreEqual(1, control.gcSuggestedKeywords.Gallery.Groups.Count);
    //        Assert.AreEqual(4, control.gcSuggestedKeywords.Gallery.Groups[0].Items.Count);

    //        Assert.AreNotEqual(null, GetItemByTag(control.gcSuggestedKeywords.Gallery, land));
    //        Assert.AreNotEqual(null, GetItemByTag(control.gcSuggestedKeywords.Gallery, nature));
    //        Assert.AreNotEqual(null, GetItemByTag(control.gcSuggestedKeywords.Gallery, arch));
    //        Assert.AreNotEqual(null, GetItemByTag(control.gcSuggestedKeywords.Gallery, city));

    //        control.teAssignedKeywords.CheckItem(build);
    //        Assert.AreEqual(1, control.teAssignedKeywords.Properties.CheckedItems.Count);
    //        Assert.AreEqual(build, control.teAssignedKeywords.Properties.CheckedItems[0].Value);

    //        Assert.AreEqual(1, control.gcSuggestedKeywords.Gallery.Groups.Count);
    //        Assert.AreEqual(2, control.gcSuggestedKeywords.Gallery.Groups[0].Items.Count);
    //        Assert.AreNotEqual(null, GetItemByTag(control.gcSuggestedKeywords.Gallery, city));
    //        Assert.AreNotEqual(null, GetItemByTag(control.gcSuggestedKeywords.Gallery, arch));
    //    }
    //    [TestMethod]
    //    public void TestTagSelectionControl_AddNewToken_ExistingTag() {
    //        TagSelectionControl control = new TagSelectionControl();
    //        control.Model = Model;
    //        control.TagType = TagType.Tag;

    //        DmFile file = GetTestPhotoModel();
    //        DmTag stone = Model.AddTag("Stone", TagType.Tag);
    //        DmTag land = Model.AddTag("Landscape", TagType.Tag);
    //        DmTag nature = Model.AddTag("Nature", TagType.Tag);
    //        DmTag build = Model.AddTag("Building", TagType.Tag);
    //        DmTag arch = Model.AddTag("Architecture", TagType.Tag);
    //        DmTag city = Model.AddTag("City", TagType.Tag);

    //        Model.AddChildTag(nature, land);
    //        Model.AddChildTag(land, stone);

    //        Model.AddChildTag(arch, build);
    //        Model.AddChildTag(build, stone);

    //        Model.AddChildTag(city, build);

    //        Model.BeginAddFiles();
    //        Model.AddFile(file);
    //        Model.EndAddFiles();

    //        control.File = file;
    //        Assert.AreEqual(0, control.gcSuggestedKeywords.Gallery.Groups.Count);

    //        control.AddKeyword("Stone");
    //        Assert.AreNotEqual(null, Model.GetFilter(Model.TagsHeaderFilter, stone));

    //        Assert.AreEqual(1, control.teAssignedKeywords.Properties.SelectedItems.Count);
    //        Assert.AreEqual(Model.GetTag("Stone", TagType.Tag), control.teAssignedKeywords.Properties.SelectedItems[0].Value);

    //        Assert.AreEqual(1, control.gcSuggestedKeywords.Gallery.Groups.Count);
    //        Assert.AreEqual(5, control.gcSuggestedKeywords.Gallery.Groups[0].Items.Count);

    //        Assert.AreNotEqual(null, GetItemByTag(control.gcSuggestedKeywords.Gallery, land));
    //        Assert.AreNotEqual(null, GetItemByTag(control.gcSuggestedKeywords.Gallery, nature));
    //        Assert.AreNotEqual(null, GetItemByTag(control.gcSuggestedKeywords.Gallery, build));
    //        Assert.AreNotEqual(null, GetItemByTag(control.gcSuggestedKeywords.Gallery, arch));
    //        Assert.AreNotEqual(null, GetItemByTag(control.gcSuggestedKeywords.Gallery, city));
    //    }
    //    GalleryItem GetItemByTag(BaseGallery gallery, DmTag tag) {
    //        foreach(GalleryItemGroup group in gallery.Groups) {
    //            foreach(GalleryItem item in group.Items)
    //                if(item.Tag == tag)
    //                    return item;
    //        }
    //        return null;
    //    }
    //    [TestMethod]
    //    public void TestDefaultTagsAreGenerated() {
    //        DmModel.AllowGenerateDefaultTags = true;
    //        DmModel model2 = new DmModel();
    //        ApplyTestSettings();
    //        model2.ConnectToDataSource();

    //        Assert.AreEqual(true, model2.Context.Tags.Local.Count > 0);
    //        Assert.AreEqual(true, model2.Context.TagNodes.Local.Count > 0);
    //        Assert.AreEqual(true, model2.Context.TagNodesReversed.Local.Count > 0);
    //    }
    //    [TestMethod]
    //    public void TestReverseCalculator() {
    //        DmTag arch = Model.AddTag("Architecture", TagType.Tag);
    //        DmTag build = Model.AddTag("Building", TagType.Tag);
    //        DmTag wood = Model.AddTag("Wood", TagType.Tag);
    //        DmTag stone = Model.AddTag("Stone", TagType.Tag);
    //        DmTag nature = Model.AddTag("Nature", TagType.Tag);
    //        DmTag land = Model.AddTag("Landscape", TagType.Tag);

    //        DmTagNode narch = new DmTagNode() { Tag = arch };
    //        DmTagNode nbuild = new DmTagNode() { Tag = build, Parent = narch };
    //        DmTagNode nstone = new DmTagNode() { Tag = stone, Parent = nbuild };
    //        DmTagNode nwood = new DmTagNode() { Tag = wood, Parent = nbuild };
    //        DmTagNode nnature = new DmTagNode() { Tag = nature };
    //        DmTagNode nland = new DmTagNode() { Tag = land, Parent = nnature };
    //        DmTagNode nstone2 = new DmTagNode() { Tag = stone, Parent = nland };

    //        Model.Context.TagNodes.Add(narch);
    //        Model.Context.TagNodes.Add(nbuild);
    //        Model.Context.TagNodes.Add(nstone);
    //        Model.Context.TagNodes.Add(nwood);
    //        Model.Context.TagNodes.Add(nnature);
    //        Model.Context.TagNodes.Add(nland);
    //        Model.Context.TagNodes.Add(nstone2);

    //        Assert.AreEqual(1, narch.Children.Count);
    //        Assert.AreEqual(true, narch.Children.Contains(nbuild));
    //        Assert.AreEqual(2, nbuild.Children.Count);
    //        Assert.AreEqual(true, nbuild.Children.Contains(nstone));
    //        Assert.AreEqual(true, nbuild.Children.Contains(nwood));

    //        while(Model.Context.TagNodesReversed.Local.Count > 0) {
    //            Model.Context.Entry<DmTagNodeReversed>(Model.Context.TagNodesReversed.Local[0]).State = System.Data.Entity.EntityState.Detached;
    //        }
    //        TagNodeReverseCalculator.ReverseTagNodesTree(Model.Context);
    //        Assert.AreEqual(8, Model.Context.TagNodesReversed.Local.Count);


    //        List<DmTagNodeReversed> rootNodes = Model.Context.TagNodesReversed.Local.Where(n => n.Parent == null).ToList();
    //        Assert.AreEqual(2, rootNodes.Count);

    //        DmTagNodeReversed rstone = Model.Context.TagNodesReversed.Local.FirstOrDefault(n => n.Tag == stone);
    //        DmTagNodeReversed rwood = Model.Context.TagNodesReversed.Local.FirstOrDefault(n => n.Tag == wood);
    //        Assert.AreEqual(true, rootNodes.Contains(rstone));
    //        Assert.AreEqual(true, rootNodes.Contains(rwood));

    //        List<DmTagNodeReversed> rstoneNodes = rstone.Children.ToList();
    //        List<DmTagNodeReversed> rwoordNodes = rwood.Children.ToList();

    //        Assert.AreEqual(2, rstoneNodes.Count);
    //        Assert.AreEqual(build, rstoneNodes[0].Tag);
    //        Assert.AreEqual(land, rstoneNodes[1].Tag);

    //        Assert.AreEqual(1, rwoordNodes.Count);
    //        Assert.AreEqual(build, rwoordNodes[0].Tag);

    //        List<DmTagNodeReversed> rbuildNodes = rstoneNodes[0].Children.ToList();
    //        List<DmTagNodeReversed> rlandNodes = rstoneNodes[1].Children.ToList();

    //        Assert.AreEqual(1, rbuildNodes.Count);
    //        Assert.AreEqual(arch, rbuildNodes[0].Tag);

    //        Assert.AreEqual(1, rlandNodes.Count);
    //        Assert.AreEqual(nature, rlandNodes[0].Tag);

    //        List<DmTagNodeReversed> rbuild2Nodes = rwoordNodes[0].Children.ToList();
    //        Assert.AreEqual(1, rbuild2Nodes.Count);
    //        Assert.AreEqual(arch, rbuild2Nodes[0].Tag);

    //    }
    //    [TestMethod]
    //    public void TestAddChildTag() {
    //        DmTag arch = Model.AddTag("Architecture", TagType.Tag);
    //        DmTag build = Model.AddTag("Building", TagType.Tag);
    //        DmTag wood = Model.AddTag("Wood", TagType.Tag);
    //        DmTag stone = Model.AddTag("Stone", TagType.Tag);

    //        Model.AddChildTag(arch, build);
    //        Model.AddChildTag(build, stone);
    //        Model.AddChildTag(build, wood);

    //        DmTagNodeReversed nStone = Model.GetTagNodeReversed(stone);
    //        DmTagNodeReversed nWood = Model.GetTagNodeReversed(wood);
    //        Assert.AreEqual(1, nStone.Children.Count);
    //        Assert.AreEqual(true, nStone.Children.FirstOrDefault(c => c.Tag == build));
    //        Assert.AreEqual(1, nWood.Children.Count);
    //        Assert.AreEqual(true, nWood.Children.FirstOrDefault(c => c.Tag == build));
    //    }
    //}
}
