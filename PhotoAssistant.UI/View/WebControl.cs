using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.Utils.Zip;
using System.Reflection;


using DevExpress.Utils.Drawing;
using System.Threading;
using PhotoAssistant.Core.Model;
using PhotoAssistant.UI.ViewHelpers;

namespace PhotoAssistant.UI.View {
    public partial class WebControl : ViewControlBase {
        public WebControl(DmModel model) {
            this.Model = model;
            this.Disposed += WebControl_Disposed;
            InitializeComponent();

            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
        }

        void WebControl_Disposed(object sender, EventArgs e) {
            FreeGallery(Gallery);
        }

        List<DmFile> files;
        public List<DmFile> Files {
            get { return files; }
            set {
                if(Files == value)
                    return;
                files = value;
                OnFilesChanged();
            }
        }

        void OnFilesChanged() {
            Gallery = new WebGalleryAngular();
        }

        WebGalleryBase _gallery;
        WebGalleryBase Gallery {
            get { return _gallery; }
            set {
                if(_gallery == value)
                    return;
                var old = _gallery;
                _gallery = value;
                OnGalleryChanged(_gallery, old);
            }
        }

        void OnGalleryChanged(WebGalleryBase newGallery, WebGalleryBase oldGallery) {
            FreeGallery(oldGallery);
            InitGallery(newGallery);
            webBrowser1.Navigate(newGallery.IndexPageFullPath);
        }

        void InitGallery(WebGalleryBase newGallery) {
            InitGallery(newGallery, FolderHelper.CreateTempFolder());
        }

        void InitGallery(WebGalleryBase gallery, string targetFolder) {
            gallery.TargetFolder = targetFolder;
            WebGalleryHelper.PrepareEngineStructure(gallery);
            WebGalleryHelper.PatchItemsFile(gallery, Files);
        }

        void FreeGallery(WebGalleryBase gallery) {
            if(gallery == null) return;
            FolderHelper.RemoveFolder(gallery.TargetFolder);
        }

        void simpleButton1_Click(object sender, EventArgs e) {
            ExportGallery();
        }

        void ExportGallery() {
            if(Gallery != null) {
                var newGallery = Activator.CreateInstance(Gallery.GetType()) as WebGalleryBase;
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if(dialog.ShowDialog() == DialogResult.OK) {
                    InitGallery(newGallery, dialog.SelectedPath);
                }
            }
        }
    }

    public abstract class WebGalleryBase {

        public string TargetFolder { get; set; }
        public string IndexPageFullPath { get { return Path.Combine(TargetFolder, IndexPage); } }
        public string ImagesFolderFullPath { get { return Path.Combine(TargetFolder, ImagesFolder); } }
        public string ThumbsFolderFullPath { get { return Path.Combine(TargetFolder, ThumbsFolder); } }

        public abstract string IndexPage { get; }
        public abstract string ImagesFolder { get; }
        public abstract string ThumbsFolder { get; }
        public abstract string ZipName { get; }
        public abstract string ItemsFile { get; }

        public const string GalleriesFolder = "PhotoAssistant.Resources.WebGalleries.";
        public const string inputPoint = "***INSERT_ITEMS_HERE***";

        public abstract string GenerateItemTag(string imgFileName, string thumbFileName);
    }

    public class WebGalleryAngular : WebGalleryBase {
        public override string ImagesFolder { get { return @"images\"; } }
        public override string ThumbsFolder { get { return @"thumbs\"; } }
        public override string ZipName { get { return "angular_gallery.zip"; } }
        public override string IndexPage { get { return "index.html"; } }
        public override string ItemsFile { get { return @"js\app.js"; } }

        public override string GenerateItemTag(string imgFileName, string thumbFileName) {
            return String.Format(itemTemplate, imgFileName);
        }

        const string itemTemplate = @"{{src: '{0}', desc: '{0}'}},";
    }

    public class WebGallery1 : WebGalleryBase {

        public override string ImagesFolder { get { return @"images\slides\"; } }
        public override string ThumbsFolder { get { return @"images\thumbs\"; } }
        public override string ZipName { get { return "diapo.zip"; } }
        public override string IndexPage { get { return "index.html"; } }
        public override string ItemsFile { get { return "index.html"; } }

        public override string GenerateItemTag(string imgFileName, string thumbFileName) {
            return String.Format(itemTemplate, thumbFileName, imgFileName);
        }

        const string itemTemplate = @"<div data-thumb=""{0}""> <img src=""{1}""> </div>";
    }

    public static class WebGalleryHelper {

        public static void PrepareEngineStructure(WebGalleryBase gallery) {
            if(!Directory.Exists(gallery.TargetFolder))
                return;
            string name = WebGalleryBase.GalleriesFolder + gallery.ZipName;
            using(Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name)) {
                ZipHelper.Decompress(stream, gallery.TargetFolder);
            }
        }

        public static void PatchItemsFile(WebGalleryBase gallery, List<DmFile> files) {
            string htmlFile = Path.Combine(gallery.TargetFolder, gallery.ItemsFile);

            if(!File.Exists(htmlFile)) {
                throw new InvalidOperationException();
            }
            if(files == null || files.Count < 1)
                return;

            string html = File.ReadAllText(htmlFile);
            int insertInd = html.IndexOf(WebGalleryBase.inputPoint);
            StringBuilder sb = new StringBuilder(html);
            sb.Remove(insertInd, WebGalleryBase.inputPoint.Length);

            foreach(DmFile file in files) {
                //TO DO
                string pathFull = gallery.ImagesFolderFullPath + file.FileName;
                string pathRelative = Path.Combine(gallery.ImagesFolder, file.FileName);
                ThumbHelper.GetThumbnailImage(file).Save(pathFull, System.Drawing.Imaging.ImageFormat.Jpeg);

                //string pathFullThumb = gallery.ThumbsFolderFullPath + file.FileName;
                //string pathRelativeThumb = Path.Combine(gallery.ThumbsFolder, file.FileName);
                //ThumbHelper.GetIconImage(file).Save(pathFullThumb, System.Drawing.Imaging.ImageFormat.Jpeg);

                pathRelative = pathRelative.Replace(@"\", "/");
                //pathRelativeThumb = pathRelativeThumb.Replace(@"\", "/");
                string newString = gallery.GenerateItemTag(pathRelative, null);
                newString += Environment.NewLine;
                sb.Insert(insertInd, newString);
                insertInd += newString.Length;
            }
            File.WriteAllText(htmlFile, sb.ToString());
        }
    }

    public class ZipHelper {
        public static void CompressDir(string sourceDir, string targetFile) {
            if(!Directory.Exists(sourceDir))
                throw new ArgumentException("sourceDir");

            using(InternalZipArchive zipper = new InternalZipArchive(targetFile)) {
                DirectoryInfo di = new DirectoryInfo(sourceDir);
                foreach(FileInfo fi in di.GetFiles()) {
                    using(FileStream fs = fi.OpenRead()) {
                        zipper.Add(fi.Name, DateTime.Now, fs);
                    }
                }
            }
        }
        public static void Decompress(string sourceFile, string targetDir) {
            if(!File.Exists(sourceFile))
                throw new ArgumentException("sourceFile");
            FileStream fs = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
            Decompress(fs, targetDir);
            fs.Close();
        }
        public static void Decompress(Stream sourceStream, string targetDir) {
            if(sourceStream == null)
                throw new ArgumentException("sourceStream");

            if(!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);

            using(sourceStream) {
                InternalZipFileCollection zipFiles = InternalZipArchive.Open(sourceStream);
                foreach(InternalZipFile zipFile in zipFiles) {
                    byte[] buffer = new byte[zipFile.UncompressedSize];
                    zipFile.FileDataStream.Read(buffer, 0, buffer.Length);

                    string targetPath = Path.Combine(targetDir, zipFile.FileName);
                    var dirname = Path.GetDirectoryName(targetPath);
                    if(!Directory.Exists(dirname)) {
                        Directory.CreateDirectory(dirname);
                    }
                    if(zipFile.CompressedSize == 0)
                        continue;
                    using(FileStream ws = new FileStream(targetPath, FileMode.Create, FileAccess.Write)) {
                        ws.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }

    public class FolderHelper {
        public static string CreateTempFolder() {
            var tempPath = Path.GetTempPath();
            var guid = Guid.NewGuid().ToString();
            tempPath = Path.Combine(tempPath, guid);
            if(!Directory.Exists(tempPath)) {
                Directory.CreateDirectory(tempPath);
            }
            return tempPath;
        }

        public static void RemoveFolder(string folder) {
            if(Directory.Exists(folder)) {
                try {
                    Directory.Delete(folder, true);
                } catch { }
            }
        }
    }
}
