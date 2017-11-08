using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace PhotoAssistant.Core.Model {
    public class MediaFormat : ISupportId {
        public static string BmpString => "bmp";
        public static string PngString => "png";
        public static string JpgString => "jpg";
        public static string JpegString => "jpeg";
        public static string TgaString => "tga";
        public static string TiffString => "tiff";
        public static string Cr2String => "cr2";
        public static string BmpFormatString => "BMP";
        public static string PngFormatString => "PNG";
        public static string JpgFormatString => "JPG";
        public static string JpegFormatString => "JPEG";
        public static string TgaFormatString => "TGA";
        public static string TiffFormatString => "TIFF";
        public static string Cr2FormatString => "CR2";
        public MediaFormat() => Id = Guid.NewGuid();

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id {
            get; set;
        }
        public string Text {
            get; set;
        }
        public string Extension {
            get; set;
        }
        public MediaType Type {
            get; set;
        }
    }
    public enum MediaType {
        Unknown,
        Image,
        Video,
        Sound
    }
}
