using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.WinExplorer;
using DevExpress.Data;
using System.Collections.ObjectModel;
using PhotoAssistant.Core.Model;
using PhotoAssistant.Core;
using System.Diagnostics;
using PhotoAssistant.Core.Helpers;

namespace PhotoAssistant.UI.View {
    public partial class AddFilesForm : XtraForm {
        DmModel Model { get; set; }
        public AddFilesForm(DmModel model) {
            InitializeComponent();
            InitializeFilesGrid();
            this.Model = model;
        }

        IImportDataSource importDataSource;
        public IImportDataSource ImportDataSource {
            get { return importDataSource; }
            private set {
                if(importDataSource == value) return;
                var oldValue = importDataSource;
                importDataSource = value;
                OnImportDataSourceChanged(oldValue, value);
            }
        }

        void OnImportDataSourceChanged(IImportDataSource oldValue, IImportDataSource newValue) {
            if(oldValue != null) {
                oldValue.CancelBackgroundProcessing();
                oldValue.ReleaseTempResources();
            }
        }

        private void InitializeFilesGrid() {
            this.filesGridControl.DataSource = new List<DmFile>();
            this.filesExplorerView.BeginUpdate();
            this.filesExplorerView.OptionsImageLoad.ClearCacheOnDataSourceUpdate = false;
            this.filesExplorerView.ColumnSet.TextColumn = this.filesExplorerView.Columns["FileName"];
            this.filesExplorerView.ColumnSet.DescriptionColumn = this.filesExplorerView.Columns["Path"];
            this.filesExplorerView.ColumnSet.MediumImageColumn = this.filesExplorerView.Columns["ThumbImage"];
            this.filesExplorerView.ColumnSet.LargeImageColumn = this.filesExplorerView.Columns["ThumbImage"];
            this.filesExplorerView.ColumnSet.SmallImageColumn = this.filesExplorerView.Columns["ThumbImage"];
            this.filesExplorerView.ColumnSet.ExtraLargeImageColumn = this.filesExplorerView.Columns["ThumbImage"];
            this.filesExplorerView.ColumnSet.CheckBoxColumn = this.filesExplorerView.Columns["AllowAdd"];
            this.filesExplorerView.OptionsImageLoad.RandomShow = true;
            this.filesExplorerView.OptionsImageLoad.AnimationType = (DevExpress.Utils.ImageContentAnimationType)((int)SettingsStore.Default.ImageAnimationType);
            zoomTrackBarControl1_EditValueChanged(zoomTrackBarControl1, null);
            this.filesExplorerView.EndUpdate();
        }
        public List<DmFile> GetFilesToAdd() {
            if(ImportDataSource != null)
                return ImportDataSource.Files.ToList().FindAll(x => x.AllowAdd);
            return new List<DmFile>();
        }

        private void filesExplorerView_GetThumbnailImage(object sender, DevExpress.Utils.ThumbnailImageEventArgs e) {
            int rowHandle = this.filesExplorerView.GetRowHandle(e.DataSourceIndex);
            var index = this.filesExplorerView.GetDataSourceRowIndex(rowHandle);
            if(index == DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                return;
            DmFile model = ImportDataSource.Files[index] as DmFile;
            e.ThumbnailImage = ImportDataSource.GetThumbnail(model);
        }
        void fileExplorerControl1_SourcePathCanged(object sender, EventArgs e) {
            var path = fileExplorerControl1.SourcePath;
            if(Directory.Exists(path)) {
                this.ImportDataSource = new ImportDataSource(Model, path, true);
                this.filesGridControl.DataSource = this.ImportDataSource.RealtimeSource;
                this.filesExplorerView.ClearImageLoader();
            }
        }
        private void zoomTrackBarControl1_EditValueChanged(object sender, EventArgs e) {
            int value = (int)zoomTrackBarControl1.EditValue;
            this.filesExplorerView.OptionsViewStyles.Large.ImageSize = new Size(value, (int)(value / 1.4f));
        }

        private void sbCheckAll_Click(object sender, EventArgs e) {
            SetCheckForAllFiles(true);
        }
        private void sbUncheckAll_Click(object sender, EventArgs e) {
            SetCheckForAllFiles(false);
        }
        void SetCheckForAllFiles(bool value) {
            this.filesExplorerView.BeginUpdate();
            foreach(DmFile file in (this.filesExplorerView.DataSource as List<DmFile>)) {
                file.AllowAdd = value;
            }
            this.filesExplorerView.EndUpdate();
        }
        private void filesExplorerView_ItemDoubleClick(object sender, DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemDoubleClickEventArgs e) {
            var file = e.ItemInfo.Row.RowKey as DmFile;
            file.AllowAdd = !file.AllowAdd;
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            OnImportDataSourceChanged(this.ImportDataSource, null);
            CreateIndexerProcess(this.ImportDataSource.Files.FirstOrDefault().Folder);


        }
        protected void CreateIndexerProcess(string pathToProcess) {
            Process process = new Process();
            process.StartInfo.FileName = "PhotoAssistant.Indexer.exe";
            IndexerParameters indexperParamters = new IndexerParameters();
            indexperParamters.IndexPath = pathToProcess;
            indexperParamters.DataSource = SettingsStore.Default.CurrentDataSource;
            indexperParamters.ThumbWidth = SettingsStore.Default.ThumbSize.Width;
            indexperParamters.PreviewWidth = SettingsStore.Default.PreviewSize.Width;
            string cmd = IndexerParameters.CreateCommandLine(indexperParamters);
            process.StartInfo.Arguments = cmd;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
        }

        private void sbImport_Click(object sender, EventArgs e) {

        }
    }

    public interface IImportDataSource {
        RealTimeSource RealtimeSource { get; }
        IList<DmFile> Files { get; }
        Image GetThumbnail(DmFile file);
        void ReleaseTempResources();
        void CancelBackgroundProcessing();
    }

    public class ImportDataSource : IImportDataSource {
        object locker = new object();
        ObservableCollection<DmFile> _files = new ObservableCollection<DmFile>();
        RealTimeSource _rts;
        BackgroundWorker _worker;

        public ImportDataSource(DmModel model, string folderPath) 
            : this(model, folderPath, false) {
        }

        public ImportDataSource(DmModel model, string folderPath, bool includeSubfolders) {
            this._rts = new RealTimeSource() {
                DataSource = this._files,
            };
            SearchOption option = includeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            IEnumerable<string> folders = new List<string> { folderPath } as IEnumerable<string>;
            folders = folders.Concat(SafeFileEnumerator.EnumerateDirectories(folderPath, "*", option));

            this._worker = new BackgroundWorker();
            this._worker.WorkerSupportsCancellation = true;
            this._worker.DoWork += (sender, args) => {
                var dirs = args.Argument as IEnumerable<string>;
                var w = sender as BackgroundWorker;
                foreach(var dir in dirs) {
                    if((w.CancellationPending)) {
                        args.Cancel = true;
                        break;
                    }
                    var files = SafeFileEnumerator.EnumerateFiles(dir, "*", SearchOption.TopDirectoryOnly);
                    var dmFiles = model.Helper.AddFileHelper.GetFilesToAdd(files);
                    foreach(var dmfile in dmFiles) {
                        lock (locker) { this._files.Add(dmfile); }
                    }
                }
            };
            this._worker.RunWorkerAsync(folders);
        }

        RealTimeSource IImportDataSource.RealtimeSource {
            get { return this._rts; }
        }
        IList<DmFile> IImportDataSource.Files {
            get { return this._files; }
        }
        Image IImportDataSource.GetThumbnail(DmFile file) {

            PhotoAssistant.Indexer.Indexer indexer = new PhotoAssistant.Indexer.Indexer();
            indexer.ThumbSize = SettingsStore.Default.ThumbSize;
            indexer.PreviewSize = SettingsStore.Default.PreviewSize;
            return indexer.ExtractThumb(file.ImportPath);
        }
        void IImportDataSource.ReleaseTempResources() { }

        void IImportDataSource.CancelBackgroundProcessing() {
            if(this._worker != null)
                this._worker.CancelAsync();
        }
    }

    public class WIAImportDataSource : IImportDataSource {
        RealTimeSource _rts = new RealTimeSource();
        List<DmFile> _files = new List<DmFile>();
        WIAItemList _wiaItems = new WIAItemList();
        Dictionary<string, WIAItem> _fileNames = new Dictionary<string, WIAItem>();
        string _tempFolderForThumbs = null;

        public WIAImportDataSource(DmModel model, DeviceInfoWrapper deviceInfoWrapper) {
            this._tempFolderForThumbs = FolderHelper.CreateTempFolder();
            this._wiaItems.Populate(deviceInfoWrapper);

            foreach(WIAItem item in this._wiaItems) {
                FileInfo info = new FileInfo(item.Path);
                if(!model.Helper.AddFileHelper.ShouldProcessFile(info))
                    continue;
                DmFile file = model.Helper.AddFileHelper.CreateFileInfoModel(info);
                this._files.Add(file);
                this._fileNames.Add(file.Path, item);
            }
        }

        IList<DmFile> IImportDataSource.Files {
            get { return this._files; }
        }
        RealTimeSource IImportDataSource.RealtimeSource {
            get { return this._rts; }
        }
        Image IImportDataSource.GetThumbnail(DmFile file) {
            WIAItem wiaItem = null;
            this._fileNames.TryGetValue(file.Path, out wiaItem);
            if(wiaItem != null)
                return WIAHelper.Default.GetThumbnail(wiaItem, this._tempFolderForThumbs);
            return null;
        }
        void IImportDataSource.ReleaseTempResources() {
            FolderHelper.RemoveFolder(this._tempFolderForThumbs);
            this._tempFolderForThumbs = null;
            this._fileNames.Clear();
        }

        void IImportDataSource.CancelBackgroundProcessing() { }
    }

    public static class SafeFileEnumerator {
        public static IEnumerable<string> EnumerateDirectories(string path, string pattern, SearchOption opt) {
            try {
                var directories = Enumerable.Empty<string>();
                if(opt == SearchOption.AllDirectories) {
                    directories = Directory.EnumerateDirectories(path).SelectMany(x => EnumerateDirectories(x, pattern, opt));
                }
                return directories.Concat(Directory.EnumerateDirectories(path, pattern));
            }
            catch {
                return Enumerable.Empty<string>();
            }
        }

        public static IEnumerable<string> EnumerateFiles(string path, string pattern, SearchOption opt) {
            try {
                var dirFiles = Enumerable.Empty<string>();
                if(opt == SearchOption.AllDirectories) {
                    dirFiles = Directory.EnumerateDirectories(path).SelectMany(x => EnumerateFiles(x, pattern, opt));
                }
                return dirFiles.Concat(Directory.EnumerateFiles(path, pattern));
            }
            catch {
                return Enumerable.Empty<string>();
            }
        }
    }
}
