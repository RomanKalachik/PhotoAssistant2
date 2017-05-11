using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core.Model {
    public class MediaFormat : ISupportId {
        public static string BmpString { get { return "bmp"; } }
        public static string PngString { get { return "png"; } }
        public static string JpgString { get { return "jpg"; } }
        public static string JpegString { get { return "jpeg"; } }
        public static string TgaString { get { return "tga"; } }
        public static string TiffString { get { return "tiff"; } }
        public static string Cr2String { get { return "cr2"; } }
        public static string BmpFormatString { get { return "BMP"; } }
        public static string PngFormatString { get { return "PNG"; } }
        public static string JpgFormatString { get { return "JPG"; } }
        public static string JpegFormatString { get { return "JPEG"; } }
        public static string TgaFormatString { get { return "TGA"; } }
        public static string TiffFormatString { get { return "TIFF"; } }
        public static string Cr2FormatString { get { return "CR2"; } }

        public MediaFormat() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Extension { get; set; }
        public MediaType Type { get; set; }
    }

    public enum MediaType {
        Unknown,
        Image,
        Video,
        Sound
    }
}
