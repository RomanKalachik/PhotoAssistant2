using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace PhotoAssistant.Core {
    public class AddFileHelper : ModelHelperBase {
        public AddFileHelper(DmModel model) : base(model) {
        }
        public List<DmFile> GetFilesToAdd(System.Collections.IEnumerable fileNames) {
            List<DmFile> res = new List<DmFile>();
            foreach(string fileName in fileNames) {
                FileInfo info = new FileInfo(fileName);
                if(!ShouldProcessFile(info)) {
                    continue;
                }

                res.Add(CreateFileInfoModel(info));
            }
            return res;
        }
        public DmFile CreateFileInfoModel(FileInfo info) {
            bool exists = File.Exists(info.FullName);
            DmFile model = new DmFile() { RelativePath = StorageManager.Default.GetRelativePath(info.FullName), ImportPath = info.FullName, Folder = info.DirectoryName, FileName = info.Name, Caption = info.Name, FileSize = exists ? info.Length : 0, AllowAdd = true, ColorLabel = null, CreationDate = exists ? File.GetCreationTime(info.FullName) : DateTime.Now, ImportDate = DateTime.Now.Date, };
            return model;
        }
        long GetFileIndex() => 0;
        public bool ShouldProcessFile(FileInfo info) => Model.Helper.MediaFormatHelper.Support(info.Extension);
        public List<DmFile> GetFilesToAdd(string directoryName) {
            IEnumerable<string> files = Directory.EnumerateFiles(directoryName, "*.*", SearchOption.AllDirectories);
            return GetFilesToAdd(files);
        }
    }
}
