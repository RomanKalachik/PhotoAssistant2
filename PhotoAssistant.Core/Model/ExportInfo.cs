using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils.Serializing;
using DevExpress.Utils.Serializing.Helpers;

namespace PhotoAssistant.Core.Model {
    public class ExportInfoCollection : Collection<ExportInfo> { }

    public class ExportInfo {
        public ExportInfo() {
            Watermark = new WatermarkParameters();
        }

        [XtraSerializableProperty]
        public string Name { get; set; }
        [XtraSerializableProperty]
        public string Folder { get; set; }
        [XtraSerializableProperty]
        public bool CreateSubFolder { get; set; }
        [XtraSerializableProperty]
        public string SubFolder { get; set; }
        [XtraSerializableProperty]
        public ExistingFileMode ExistingFileMode { get; set; }
        [XtraSerializableProperty]
        public bool RenameFiles { get; set; }
        [XtraSerializableProperty]
        public string RenameMaskName { get; set; }
        [XtraSerializableProperty]
        public string RenameMask { get; set; }

        FileRenameValueReferenceCollection fileRenameValues;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true, 0)]
        public FileRenameValueReferenceCollection FileRenameValues {
            get {
                if(fileRenameValues == null)
                    fileRenameValues = new FileRenameValueReferenceCollection();
                return fileRenameValues;
            }
        }

        FileRenameValueReference XtraCreateFileRenameValuesItem(XtraItemEventArgs e) {
            XtraPropertyInfo item = e.Item.ChildProperties["FileRenameValueName"];
            if(item == null || item.Value == null)
                return null;
            string name = item.Value.ToString();
            FileRenameValueBase value = FileRenameManager.Default.GetFileRenameValue(name);
            return value.CreateReference();
        }

        void XtraSetIndexFileRenameValuesItem(XtraSetItemIndexEventArgs e) {
            if(e.Index > -1)
                FileRenameValues.Insert(e.Index, (FileRenameValueReference)e.Item.Value);
            else
                FileRenameValues.Add((FileRenameValueReference)e.Item.Value);
        }

        [XtraSerializableProperty]
        public bool ResizeImages { get; set; }
        [XtraSerializableProperty]
        public FileResizeMode ResizeMode { get; set; }
        [XtraSerializableProperty]
        public bool DontEnlarge { get; set; }
        [XtraSerializableProperty]
        public int Width { get; set; }
        [XtraSerializableProperty]
        public int Height { get; set; }
        [XtraSerializableProperty]
        public int ShortSide { get; set; }
        [XtraSerializableProperty]
        public int LongSide { get; set; }
        [XtraSerializableProperty]
        public int Dpi {
            get;
            set;
        }
        [XtraSerializableProperty]
        public ResolutionMode ResolutionMode { get; set; }
        [XtraSerializableProperty]
        public ImageDimensionMode ImageDimension { get; set; }
        [XtraSerializableProperty]
        public bool ShowWatermark { get; set; }
        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public WatermarkParameters Watermark { get; set; }
        [XtraSerializableProperty]
        public AfterExportEvent AfterExportEvent { get; set; }
        string applicationIdString;
        [XtraSerializableProperty]
        public string ApplicationIdString {
            get { return applicationIdString; }
            set {
                if(ApplicationIdString == value)
                    return;
                applicationIdString = value;
                appInfo = null;
            }
        }
        ApplicationInfo appInfo = null;
        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public ApplicationInfo Application {
            get {
                if(appInfo == null)
                    appInfo = SettingsStore.Default.ExportApplications.FirstOrDefault((a) => a.IdString == ApplicationIdString);
                if(appInfo == null)
                    appInfo = new ApplicationInfo();
                return appInfo;
            }
        }
        [XtraSerializableProperty]
        public ExportImageFormat ImageFormat {
            get;
            set;
        }
        [XtraSerializableProperty]
        public int CompressionLevel {
            get;
            set;
        }
        [XtraSerializableProperty]
        public int LimitFileSize {
            get;
            set;
        }
        [XtraSerializableProperty]
        public bool IsLimitFileSize {
            get;
            set;
        }
        int ConstrainBitsPerChannel(int value) {
            if(value != 8 && value != 16)
                value = 8;
            return value;
        }
        int bitsPerChannel;
        [XtraSerializableProperty]
        public int PngBitsPerChannel {
            get { return bitsPerChannel; }
            set {
                bitsPerChannel = ConstrainBitsPerChannel(value);
            }
        }
        ImageFormat ToImageFormat(ExportImageFormat format) {
            switch(format) {
                case ExportImageFormat.BMP:
                    return System.Drawing.Imaging.ImageFormat.Bmp;
                case ExportImageFormat.JPEG:
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case ExportImageFormat.PNG:
                    return System.Drawing.Imaging.ImageFormat.Png;
                case ExportImageFormat.TIFF:
                    return System.Drawing.Imaging.ImageFormat.Tiff;
                //case ExportImageFormat.PSD:
                //    return System.Drawing.Imaging.ImageFormat.
            }
            return null;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format) {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach(ImageCodecInfo codec in codecs) {
                if(codec.FormatID == format.Guid) {
                    return codec;
                }
            }
            return null;
        }

        public PixelFormat GetPixelFormat() {
            if(PngBitsPerChannel == 8)
                return PixelFormat.Format32bppArgb;
            return PixelFormat.Format64bppArgb;
        }
    }

    public class ExportNodeInfo {
        public ExportNodeInfo() {
            Id = Guid.NewGuid();
        }
        string name;
        public string Name {
            get {
                if(ExportInfo != null)
                    return ExportInfo.Name;
                return name;
            }
            set { name = value; }
        }
        public ExportInfo ExportInfo { get; set; }
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
    }

    public enum ExistingFileMode {
        AskUser,
        GenerateNewName,
        OverrideWithoutPrompt,
        Skip
    }

    public enum AfterExportEvent {
        DoNothing,
        ShowInExplorer,
        OpenInApplication
    }

    public enum ExportImageFormat {
        JPEG,
        PNG,
        BMP,
        TIFF,
        PSD
    }
}
