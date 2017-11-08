using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Drawing;
using PhotoAssistant.Core.Model;
using DevExpress.Utils;
using PhotoAssistant.Core;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using PhotoAssistant.Indexer.Wrappers;
using PhotoAssistant.Core.Helpers;
namespace PhotoAssistant.Indexer {

    public class ImageInfo : IDisposable {
        public Image image;
        public Image thumbImage;
        public string description, producer, model, artist;
        public float flashUsed, iso, shutter, aperture, focalLength, flip;
        public int height, width;
        public int colorDepth, dpi;
        public void Dispose() {
            if (image != null) image.Dispose();
            image = null;
            if (thumbImage != null) thumbImage.Dispose();
            thumbImage = null;
        }
    }
    //public delegate void IndexerReportingCallback(DmFile file, int fileIndex, int filesCount);

    public class Indexer {
        static Indexer() {
            InitDCRaw();
        }
        //IndexerReportingCallback reportProgressCallbackCore;
        //Action0 processCompletedCallbackCore;
        //Action1<int> processStartedCallbackCore;
        //public Indexer(IndexerReportingCallback reportProgressCallback, Action1<int> processStartedCallback, Action0 processCompletedCallback) {
        //    this.processStartedCallbackCore = processStartedCallback;
        //    this.reportProgressCallbackCore = reportProgressCallback;
        //    this.processCompletedCallbackCore = processCompletedCallback;
        //}
        IEnumerable<string> Files { get; set; }
        int FilesCount { get; set; }
        public Size ThumbSize { get; set; }
        public Size PreviewSize { get; set; }
        //public StorageManager StorageManager { get; set; }
        //public IStorageManagerDialogsProvider StorageManagerDialogsProvider { get; set; }
        public void ProcessFiles(string[] files) {
            if (!CheckStorage())
                return;
            Files = files;
            StartProcess();
        }
        private bool CheckStorage() {
            //if(StorageManager == null) {
            //    StorageManager = StorageManager.Default;
            //    //StorageManager.DialogsProvider = StorageManagerDialogsProvider.Default;
            //}
            //return StorageManager.CheckStorage();
            return true;
        }

        public void Process(IndexerParameters parameters) {
            ThumbSize = new Size(parameters.ThumbWidth, parameters.ThumbWidth);
            PreviewSize = new Size(parameters.PreviewWidth, parameters.PreviewWidth);//TODO
            ProcessDirectory(parameters.IndexPath);
        }
        public void ProcessDirectory(string path) {
            Files = Directory.EnumerateFiles(path);
            StartProcess();
        }

        void StartProcess() {
            if (Model != null) Model.BeginAddFiles();
            FilesCount = Files.Count();
            Program.Log.Info(string.Format("{0} files to process", FilesCount));
            DoWork();
            ////if(processStartedCallbackCore != null)
            ////    processStartedCallbackCore(FilesCount);
            //CheckStorage();
            //Thread thread = new Thread(new ThreadStart(DoWork));
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
        }
        void DoWork() {
            int index = 1;
            if (Model != null)
                Model.Context.Configuration.AutoDetectChangesEnabled = false;
            //SubscribeStorageManagerEvents();
            foreach (string file in Files) {
                ProcessFile(file, index, FilesCount);
                index++;
                //if (FatalErrorOccured)
                //    break;
            }
            if (Model != null)
                Model.Context.Configuration.AutoDetectChangesEnabled = true;
            if (Model != null) Model.EndAddFiles();
            //if(processCompletedCallbackCore != null) {
            //    Application.OpenForms[0].BeginInvoke(processCompletedCallbackCore);
            //}
            //UnsubscribeStorageManagerEvents();
        }

        //private void UnsubscribeStorageManagerEvents() {
        //    StorageManager.BackupVolumeChanged -= StorageManager_BackupVolumeChanged;
        //    StorageManager.StorageVolumeChanged -= StorageManager_StorageVolumeChanged;
        //    StorageManager.FileCopied -= StorageManager_FileCopied;
        //}

        //private void SubscribeStorageManagerEvents() {
        //    StorageManager.BackupVolumeChanged += StorageManager_BackupVolumeChanged;
        //    StorageManager.StorageVolumeChanged += StorageManager_StorageVolumeChanged;
        //    StorageManager.FileCopied += StorageManager_FileCopied;
        //}

        protected DmFile FileToSave { get; set; }

        private void StorageManager_FileCopied(object sender, FileCopyEventArgs e) {
            if (e.FileInfo == null)
                return;
            if (e.IsBackupVolume) {
                FileToSave.Volume2 = StorageVolumeModel;
                FileToSave.Volume2LastWriteTime = e.FileInfo.LastWriteTime;
            } else {
                FileToSave.Volume1 = StorageVolumeModel;
                FileToSave.Volume1LastWriteTime = e.FileInfo.LastWriteTime;
            }
        }

        //private void StorageManager_StorageVolumeChanged(object sender, EventArgs e) {
        //    if(Model != null)
        //        StorageVolumeModel = Model.CheckAddStorageVolume(StorageManager.StorageVolume);
        //}

        //private void StorageManager_BackupVolumeChanged(object sender, EventArgs e) {
        //    if(Model != null)
        //        BackupVolumeModel = Model.CheckAddStorageVolume(StorageManager.BackupVolume);
        //}

        //[DllImport("RawProcessor.dll", EntryPoint = "main", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //static extern ResultInfo main(int argc, IntPtr[] argv);
        //[DllImport("RawProcessor.dll", EntryPoint = "freeResultInfo", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //static extern void freeResultInfo();

        public static Size ResampleBitmap(Size source, int imageWidth, int imageHeight) {
            if (source == null || source.Width == 0 || source.Height == 0) {
                return Size.Empty;
            }
            if (imageWidth == 0 || imageHeight == 0 || imageWidth == -1 || imageHeight == -1) {
                return source;
            }
            float k = (float)source.Width / (float)imageWidth;
            float k2 = (float)source.Height / (float)imageHeight;
            int w;
            int h;
            if (k2 > k) {
                w = Math.Max(1, (int)Math.Round((double)((float)source.Width / k2)));
                h = imageHeight;
            } else {
                w = imageWidth;
                h = Math.Max(1, (int)Math.Round((double)((float)source.Height / k)));
            }
            return new Size(w, h);
        }

        public static Image ResampleBitmap(Image source, int imageWidth, int imageHeight) {
            if (source == null || source.Width == 0 || source.Height == 0) {
                return null;
            }
            if (imageWidth == 0 || imageHeight == 0 || imageWidth == -1 || imageHeight == -1) {
                return new Bitmap(source);
            }
            float k = (float)source.Width / (float)imageWidth;
            float k2 = (float)source.Height / (float)imageHeight;
            int w;
            int h;
            if (k2 > k) {
                w = Math.Max(1, (int)Math.Round((double)((float)source.Width / k2)));
                h = imageHeight;
            } else {
                w = imageWidth;
                h = Math.Max(1, (int)Math.Round((double)((float)source.Height / k)));
            }
            System.Drawing.Imaging.PixelFormat format = IsIndexedFormat(source.PixelFormat) ? System.Drawing.Imaging.PixelFormat.Format32bppPArgb : source.PixelFormat;
            Bitmap dest = new Bitmap(w, h, format);
            using (Graphics g = Graphics.FromImage(dest)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.DrawImage(source, 0, 0, w, h);
            }
            return dest;
        }

        private static bool IsIndexedFormat(System.Drawing.Imaging.PixelFormat pixelFormat) {
            if (pixelFormat == System.Drawing.Imaging.PixelFormat.Indexed ||
                pixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed ||
                pixelFormat == System.Drawing.Imaging.PixelFormat.Format4bppIndexed ||
                pixelFormat == System.Drawing.Imaging.PixelFormat.Format1bppIndexed)
                return true;
            return false;
        }

        private static System.Drawing.Imaging.PixelFormat ConvertPixelFormat(System.Windows.Media.PixelFormat sourceFormat) {
            if (sourceFormat == PixelFormats.Bgr24)
                return System.Drawing.Imaging.PixelFormat.Format24bppRgb;
            if (sourceFormat == PixelFormats.Bgra32)
                return System.Drawing.Imaging.PixelFormat.Format32bppArgb;
            if (sourceFormat == PixelFormats.Bgr32)
                return System.Drawing.Imaging.PixelFormat.Format32bppRgb;
            return System.Drawing.Imaging.PixelFormat.Format24bppRgb;//TODO
        }
        Bitmap GetBitmap(BitmapSource source) {
            Bitmap bmp = new Bitmap(
              source.PixelWidth,
              source.PixelHeight,
              ConvertPixelFormat(source.Format));
            BitmapData data = bmp.LockBits(
              new Rectangle(System.Drawing.Point.Empty, bmp.Size),
              ImageLockMode.WriteOnly,
              ConvertPixelFormat(source.Format));
            source.CopyPixels(
              System.Windows.Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }
        Image GetImageFromBitmapSource(BitmapSource source) {
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(source));
            var stream = new MemoryStream();
            encoder.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return Image.FromStream(stream);
        }
        Image GetImageFromBitmapSource(BitmapFrame frame) {
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(frame);
            var stream = new MemoryStream();
            encoder.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return Image.FromStream(stream);
        }

        DmFile CreateModel(FileInfo fileInfo) {
            if (Model == null) {
                DmFile res = new DmFile() {
                    RelativePath = StorageManager.Default.GetRelativePath(fileInfo.FullName),
                    Folder = fileInfo.DirectoryName,
                    FileName = fileInfo.Name,
                    Caption = fileInfo.Name,
                    FileSize = fileInfo.Length,
                    AllowAdd = true,
                    ColorLabel = null,
                    CreationDate = File.GetCreationTime(fileInfo.FullName),
                    ImportDate = DateTime.Now.Date,
                };
                return res;
            }
            return Model.Helper.AddFileHelper.CreateFileInfoModel(fileInfo);
        }

        public Image ExtractThumb(string file) {
            ImageInfo image = null;
            Image reducedImage = null;
            Image previewImage = null;
            try {
                image = LoadImageInfo(file);
                if (image == null) return null;//Properties.Resources.ImageNotFound;
                previewImage = image.thumbImage != null && (image.thumbImage.Width >= PreviewSize.Width || image.thumbImage.Height >= PreviewSize.Height) ? ResizeImageToPreview(image.thumbImage) : ResizeImageToPreview(image.image);
                reducedImage = previewImage == null ? ResizeImageToThumb(image.thumbImage) : ResizeImageToThumb(previewImage);
            } finally {
                if (image != null) {
                    image.thumbImage = null;
                    image.Dispose();
                }
            }
            return reducedImage;
        }

        ImageInfo LoadImageInfo(string file) {
            ImageInfo image = null;
            try {
                image = LoadImageUsingDCRaw(file);
            } catch { return null; }
            return image;
        }

        public DmFile ProcessFile(string file) {
            return ProcessFileCore(file);
        }

        bool StorageVolumeInitialized { get; set; }
        bool BackupVolumeInitialized { get; set; }
        StorageVolumeInfo StorageVolume { get; set; }
        StorageVolumeInfo BackupVolume { get; set; }
        DmStorageVolume StorageVolumeModel { get; set; }
        DmStorageVolume BackupVolumeModel { get; set; }

        //protected bool FatalErrorOccured { get; set; }

        DmFile ProcessFileCore(string file) {
            DmFile model = null;
            ImageInfo image = null;
            Image reducedImage = null;
            Image previewImage = null;
            try {
                image = LoadImageInfo(file);
                if (image == null) return model; //Error could not decode image!
                previewImage = ResizeImageToPreview(image.image); //TODO create preview more correctly
                reducedImage = previewImage == null ? ResizeImageToThumb(image.thumbImage) : ResizeImageToThumb(previewImage);

                FileInfo info = new FileInfo(file);
                model = CreateModel(info);
                string thumbPath = ThumbsFolderManager.GetNextThumbPath(model.Id);
                string previewPath = ThumbsFolderManager.GetNextPreviewPath(model.Id);
                reducedImage.Save(thumbPath, ImageFormat.Jpeg);
                if (previewImage != null) {
                    previewImage.Save(previewPath, ImageFormat.Jpeg);
                    previewImage.Dispose();
                }
                model.MD5Hash = MD5Helper.CalculateMD5(file);
                model.ThumbFileName = thumbPath;
                model.PreviewFileName = previewPath;
                model.ThumbImage = reducedImage;
                model.Width = image.width;
                model.Height = image.height;
                model.AspectRatio = ((float)model.Width) / model.Height;
                model.Description = image.description;
                model.ISO = image.iso;
                model.ShutterSpeed = image.shutter;
                model.Aperture = image.aperture;
                model.Author = image.artist;
                model.CameraModel = image.model;
                model.CameraProducer = image.model;
                model.FlashUsed = image.flashUsed;
                model.FocalLength = image.focalLength;
                model.Flip = image.flip;

                //CheckInitializeStorageVolumes();
                //if(!CopyFile(file, model)) {
                //    StorageManager.OnCopyFileFailed();
                //    FatalErrorOccured = true;
                //    return model;
                //}

                //model.FullPreviewFileName = model.FileNameWithoutExtension + "_Preview" + "." + model.Extension;
                //Image fullPreview = image.thumbImage;
                //if (!StorageManager.SavePreviewFile(fullPreview, model.RelativePath, model.FullPreviewFileName)) {
                //    StorageManager.OnCopyFileFailed();
                //    FatalErrorOccured = true;
                //    return model;
                //}
                //fullPreview.Dispose();

                model.Dpi = image.dpi;
                model.ColorDepth = image.colorDepth;
            } finally {
                if (image != null) {
                    image.thumbImage = null;
                    image.Dispose();
                }
            }
            return model;
        }

        //private bool CopyFile(string file, DmFile model) {
        //    FileToSave = model;
        //    try {
        //        return StorageManager.CopyFile(file, model.RelativePath + "\\" + model.FileName);
        //    } finally {
        //        FileToSave = null;
        //    }
        //}

        //private void CheckInitializeStorageVolumes() {
        //    StorageManager.CheckInitializeStorageVolumes();
        //}

        public void ProcessFile(string file, int index, int count) {
            Program.Log.Info(string.Format("Processing file {0}", file));
            var dmfile = ProcessFileCore(file);
            if (Model != null && dmfile != null)
                Model.AddFile(dmfile);
            Program.Log.Info(string.Format("Success", file));
        }
        //ImageInfo LoadImageUsingWindowsImaging(string file) {
        //    using(FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read)) {
        //        try {
        //            BitmapDecoder decoder = BitmapDecoder.Create(fs, BitmapCreateOptions.None | BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.None);
        //            if(decoder != null && decoder.Frames.Count > 0) {
        //                ImageInfo imageInfo = new ImageInfo();
        //                BitmapSource bs = decoder.Frames[0];
        //                if(bs == null) return null;
        //                imageInfo.thumbImage = GetImageFromBitmapSource2(decoder.Frames[0]);
        //                imageInfo.width = (int)bs.Width;
        //                imageInfo.height = (int)bs.Height;
        //                imageInfo.dpi = (int)bs.DpiX;
        //                imageInfo.colorDepth = bs.Format.BitsPerPixel;
        //                http://msdn.microsoft.com/en-us/library/windows/desktop/ee719904%28v=vs.85%29.aspx
        //                http://msdn.microsoft.com/en-us/library/windows/desktop/ee872007%28v=vs.85%29.aspx
        //                BitmapMetadata bitmapMetadata = (BitmapMetadata)bs.Metadata;
        //                if(bitmapMetadata == null) return imageInfo;
        //                imageInfo.producer = TryGetMetadataValue<string>(bitmapMetadata, "System.Photo.CameraManufacturer");
        //                imageInfo.model = TryGetMetadataValue<string>(bitmapMetadata, "System.Photo.CameraModel");
        //                imageInfo.shutter = (float)TryGetMetadataValue<double>(bitmapMetadata, "System.Photo.ShutterSpeed");
        //                imageInfo.iso = (float)TryGetMetadataValue<ushort>(bitmapMetadata, "System.Photo.ISOSpeed");
        //                imageInfo.focalLength = (float)TryGetMetadataValue<double>(bitmapMetadata, "System.Photo.FocalLength");
        //                imageInfo.flip = TryGetMetadataValue<ushort>(bitmapMetadata, "System.Photo.Orientation");
        //                imageInfo.flashUsed = TryGetMetadataValue<ushort>(bitmapMetadata, "System.Photo.Flash");
        //                return imageInfo;
        //            }
        //        } catch { }
        //        return null;
        //    }
        //}

        private Image GetImageFromBitmapSource2(BitmapFrame bitmapFrame) {
            Size size = ResampleBitmap(new Size((int)bitmapFrame.Width, (int)bitmapFrame.Height), ThumbSize.Width, ThumbSize.Height);
            TransformedBitmap tbBitmap = new TransformedBitmap(bitmapFrame, new ScaleTransform(((double)size.Width) / bitmapFrame.PixelWidth, ((double)size.Height) / bitmapFrame.PixelHeight, 0, 0));
            BitmapFrame frame = BitmapFrame.Create(tbBitmap);
            Image res = GetImageFromBitmapSource(frame);
            return res;
        }
        protected RenderTargetBitmap ResizeBitmapSource(BitmapSource imageSource) {
            Size size = ResampleBitmap(new Size((int)imageSource.Width, (int)imageSource.Height), ThumbSize.Width, ThumbSize.Height);
            var group = new DrawingGroup();
            RenderOptions.SetBitmapScalingMode(group, BitmapScalingMode.HighQuality);
            group.Children.Add(new ImageDrawing(imageSource, new System.Windows.Rect(0.0, 0.0, size.Width, size.Height)));
            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
                drawingContext.DrawDrawing(group);
            RenderTargetBitmap bmp = new RenderTargetBitmap(size.Width, size.Height, 96, 96, PixelFormats.Default);
            bmp.Render(drawingVisual);
            return bmp;
        }
        protected T TryGetMetadataValue<T>(BitmapMetadata bitmapMetadata, string query) {
            object res = null;
            try {
                res = bitmapMetadata.GetQuery(query);
            } catch { }
            if (res == null)
                return default(T);
            return (T)res;
        }
        public static void InitDCRaw() {
            Settings settings = new Settings();
            libPhotoAssistantImageProcessing.init(settings, ".", ".");
        }
        private ImageInfo GetBitmapFormImage(string file) {
            ImageData imageData = null;
            PreviewImage image = null;
            try {
                string fileExtension = Path.GetExtension(file).Substring(1);
                if(fileExtension == "xmp" || fileExtension == "pp3" || fileExtension == "ini")
                    return null;
                image = new PreviewImage(file, fileExtension, PreviewImage.PreviewImageMode.PIM_EmbeddedOrRaw);

                Bitmap bmp;
                int imageW = image.getWidth();
                int imageH = image.getHeight();
                Format imageFormat = image.getPixelFormat();
                int imageStride = image.getStride();
                IntPtr imagePointer = SWIGTYPE_p_unsigned_char.getCPtr(image.getImagePtr()).Handle;
                int bytes = imageStride * imageH;
                byte[] rgbValues;
                rgbValues = new byte[bytes];
                Marshal.Copy(imagePointer, rgbValues, 0, bytes);
                bmp = new Bitmap(imageW, imageH, imageStride, System.Drawing.Imaging.PixelFormat.Format32bppRgb, Marshal.UnsafeAddrOfPinnedArrayElement(rgbValues, 0));
                ImageInfo result = new ImageInfo();
                imageData = new ImageData(file);

                result.image = bmp;
                result.height = imageH;//!!
                result.width = imageW;
                result.shutter = (float)imageData.getShutterSpeed();
                result.producer = imageData.getMake();
                result.model = imageData.getModel();
                result.artist = "";
                result.iso = imageData.getISOSpeed();
                result.focalLength = (float)imageData.getFocalLen();
                return result;
            } finally {
                    if(image != null)
                        image.Dispose();
                    //bmp.Dispose();
                    if(imageData != null)
                        imageData.Dispose();
            }
        }
        ImageInfo LoadImageUsingDCRaw(string file) {
            return GetBitmapFormImage(file);
        }
        public Image ImageFromByteArray(byte[] imgData) {
            using (MemoryStream ms = new MemoryStream(imgData)) {
                return Image.FromStream(ms);
            }
        }
        public Image ResizeImageToThumb(Image image) {
            return ResampleBitmap(image, ThumbSize.Width, ThumbSize.Height);
        }
        public Image ResizeImageToPreview(Image image) {
            return ResampleBitmap(image, PreviewSize.Width, PreviewSize.Height);
        }
        public DmModel Model {
            get;
            set;
        }
        private bool CreateDefaultDataSource() {
            return Model.CreateDefaultDataSource();
        }
        private void ShowError(string errorText) {
            MessageBox.Show(errorText);
        }
        private bool OpenDataSource(string dataSource) {
            return Model.OpenDataSource(dataSource, false);
        }

        private void ConnectToDataSource() {
            if (string.IsNullOrEmpty(SettingsStore.Default.CurrentDataSource)) {
                if (!CreateDefaultDataSource()) {
                    ShowError(MessageStrings.CannotCreateDataSource);
                }
                SettingsStore.Default.CurrentDataSource = SettingsStore.Default.DefaultDataSourcePath;
            } else {
                if (!OpenDataSource(SettingsStore.Default.CurrentDataSource)) {
                    ShowError(MessageStrings.CannotOpenDataSource);
                    return;
                }
            }
        }

    }
    public class ThumbsFolderManager {
        protected static void EnsureThumbsFolderExist() {
            if (!Directory.Exists(SettingsStore.Default.ThumbPath)) Directory.CreateDirectory(SettingsStore.Default.ThumbPath);
        }
        protected static void EnsurePreviewFolderExist() {
            if (!Directory.Exists(SettingsStore.Default.PreviewPath)) Directory.CreateDirectory(SettingsStore.Default.PreviewPath);
        }
        public static string GetNextThumbPath(Guid guid) {
            EnsureThumbsFolderExist();
            return SettingsStore.Default.ThumbPath + string.Format("\\{0}.jpg", guid.ToString());
        }
        public static string GetNextPreviewPath(Guid guid) {
            EnsurePreviewFolderExist();
            return SettingsStore.Default.PreviewPath + string.Format("\\{0}.jpg", guid.ToString());
        }
    }
}