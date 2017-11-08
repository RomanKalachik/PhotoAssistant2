using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Media;
namespace PhotoAssistant.Core.Model {
    public class FileResizeManager {
        static FileResizeManager defaultManager;
        public static FileResizeManager Default {
            get {
                if(defaultManager == null) {
                    defaultManager = new FileResizeManager();
                }

                return defaultManager;
            }
        }
        public FileResizeMode ResizeMode {
            get; set;
        }
        public ImageDimensionMode DimensionMode {
            get; set;
        }
        public ResolutionMode ResolutionMode {
            get; set;
        }
        public SizeF Size {
            get => new SizeF(Width, Height);
            set {
                Width = value.Width;
                Height = value.Height;
            }
        }
        public SizeF SizeInPixels => Dimenstion2Pixels(Size, DimensionMode, Dpi, ResolutionMode);
        public float Width {
            get; set;
        }
        public float Height {
            get; set;
        }
        public float LongSide {
            get; set;
        }
        public float ShortSide {
            get; set;
        }
        public int Dpi {
            get; set;
        }
        public ResizeInfo Resize(DmFile file) => GetResizeInfo(file);
        public ResizeInfo GetResizeInfo(DmFile file) {
            switch(ResizeMode) {
                case Model.FileResizeMode.ZoomInside:
                    return GetResizeInfoZoomInside(file, SizeInPixels);
                case Model.FileResizeMode.ZoomOutside:
                    return GetResizeInfoZoomOutside(file, SizeInPixels);
                case Model.FileResizeMode.LongSide:
                    return GetResizeInfoLongSide(file, LongSide);
                case Model.FileResizeMode.ShortSide:
                    return GetResizeInfoShortSide(file, ShortSide);
            }
            return null;
        }
        ResizeInfo GetResizeInfoShortSide(DmFile file, float shortSide) {
            float k = file.WidthPixels > file.HeightPixels ? shortSide / file.HeightPixels : shortSide / file.WidthPixels;
            return new ResizeInfo() { ImageSize = new SizeF(file.WidthPixels * k, file.HeightPixels * k), SourceRect = new RectangleF(0, 0, file.WidthPixels, file.HeightPixels) };
        }
        ResizeInfo GetResizeInfoLongSide(DmFile file, float longSide) {
            float k = file.WidthPixels < file.HeightPixels ? longSide / file.HeightPixels : longSide / file.WidthPixels;
            return new ResizeInfo() { ImageSize = new SizeF(file.WidthPixels * k, file.HeightPixels * k), SourceRect = new RectangleF(0, 0, file.WidthPixels, file.HeightPixels) };
        }
        ResizeInfo GetResizeInfoZoomOutside(DmFile file, SizeF fit) {
            float k1 = fit.Width / file.WidthPixels;
            float k2 = fit.Height / file.HeightPixels;
            float k = Math.Max(k1, k2);

            float srcWidth = fit.Width / k;
            float srcHeight = fit.Height / k;
            ResizeInfo info = new ResizeInfo() { ImageSize = fit, SourceRect = new RectangleF((file.WidthPixels - srcWidth) / 2, (file.HeightPixels - srcHeight) / 2, srcWidth, srcHeight) };
            return info;
        }
        ResizeInfo GetResizeInfoZoomInside(DmFile file, SizeF fit) {
            float k1 = fit.Width / file.WidthPixels;
            float k2 = fit.Height / file.HeightPixels;
            float k = Math.Min(k1, k2);

            ResizeInfo info = new ResizeInfo() { ImageSize = new SizeF(file.WidthPixels * k, file.HeightPixels * k), SourceRect = new RectangleF(0, 0, file.WidthPixels, file.HeightPixels) };
            return info;
        }
        const float Inches2Cm = 2.54f;
        SizeF GetFileSizeByDimension(DmFile file, ImageDimensionMode mode) {
            if(mode == ImageDimensionMode.Pixels) {
                return new SizeF(file.WidthPixels, file.HeightPixels);
            }

            if(mode == ImageDimensionMode.Inches) {
                new SizeF(((float)file.WidthPixels) / file.Dpi, ((float)file.HeightPixels) / file.Dpi);
            }

            return new SizeF(((float)file.WidthPixels) / file.Dpi * Inches2Cm, ((float)file.HeightPixels) / file.Dpi * Inches2Cm);
        }
        SizeF Pixels2Dimension(SizeF size, ImageDimensionMode mode, int dpi) {
            if(mode == ImageDimensionMode.Pixels) {
                return size;
            }

            if(mode == ImageDimensionMode.Inches) {
                new SizeF(size.Width / dpi, size.Height / dpi);
            }

            return new SizeF(size.Width / dpi * Inches2Cm, size.Height / dpi * Inches2Cm);
        }
        SizeF Dimenstion2Pixels(SizeF size, ImageDimensionMode mode, int dpi, ResolutionMode rmode) {
            float k = rmode == Model.ResolutionMode.PixelsPerCm ? Inches2Cm : 1.0f;
            switch(mode) {
                case ImageDimensionMode.Pixels:
                    return size;
                case ImageDimensionMode.Inches:
                    return new SizeF(size.Width * dpi * k, size.Height * dpi * k);
                case ImageDimensionMode.Centimeters:
                    return new SizeF(size.Width / Inches2Cm * dpi * k, size.Height / Inches2Cm * dpi * k);
            }
            return SizeF.Empty;
        }
        int GetBitPerChannel(Image img) {
            if(img.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb || img.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb || img.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppPArgb || img.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppRgb) {
                return 8;
            }

            return 16;
        }
        public Image GetImage(Image inImage, ResizeInfo resizeInfo, System.Drawing.Imaging.PixelFormat pixelFormat) {
            if(inImage == null) {
                return null;
            }

            Image res = GetCachedImage((int)resizeInfo.ImageSize.Width, (int)resizeInfo.ImageSize.Height, pixelFormat);
            bool shouldDispose = false;
            if(GetBitPerChannel(inImage) != GetBitPerChannel(res)) {
                inImage = ((Bitmap)inImage).Clone(new Rectangle(0, 0, inImage.Width, inImage.Height), res.PixelFormat);
            }
            using(Graphics g = Graphics.FromImage(res)) {
                g.DrawImage(inImage, new Rectangle(0, 0, (int)resizeInfo.ImageSize.Width, (int)resizeInfo.ImageSize.Height), resizeInfo.SourceRect.X, resizeInfo.SourceRect.Y, resizeInfo.SourceRect.Width, resizeInfo.SourceRect.Height, GraphicsUnit.Pixel);
            }
            if(shouldDispose) {
                inImage.Dispose();
            }

            return res;
        }
        Dictionary<Size, Image> cachedImages;
        protected Dictionary<Size, Image> CachedImages {
            get {
                if(cachedImages == null) {
                    cachedImages = new Dictionary<Size, Image>();
                }

                return cachedImages;
            }
        }
        Image GetCachedImage(int width, int height, System.Drawing.Imaging.PixelFormat pixelFormat) {
            if(CachedImages.Count > 5) {
                ClearCache();
            }

            Image res = null;
            if(CachedImages.TryGetValue(new Size(width, height), out res)) {
                if(res.PixelFormat == pixelFormat) {
                    return res;
                }

                res.Dispose();
                CachedImages.Remove(new Size(width, height));
            }
            res = new Bitmap(width, height, pixelFormat);
            if(Dpi != 0) {
                ((Bitmap)res).SetResolution(Dpi, Dpi);
            }

            CachedImages.Add(new Size(width, height), res);
            return res;
        }
        public void ClearCache() {
            foreach(Image image in CachedImages.Values) {
                image.Dispose();
            }
            CachedImages.Clear();
        }
        public ImageSource GetImage(ImageSource inImage, ImageSource watermark, ResizeInfo resizeInfo) => CreateResizedImage(inImage, watermark, resizeInfo);
        ImageSource CreateResizedImage(ImageSource source, ImageSource watermark, ResizeInfo info) {
            System.Windows.Rect rect = new System.Windows.Rect(info.SourceRect.X, info.SourceRect.Y, info.SourceRect.Width, info.SourceRect.Height);

            DrawingGroup group = new DrawingGroup();
            RenderOptions.SetBitmapScalingMode(group, BitmapScalingMode.HighQuality);
            group.Children.Add(new ImageDrawing(source, rect));
            if(watermark != null) {
                group.Children.Add(new ImageDrawing(watermark, new System.Windows.Rect(0, 0, watermark.Width, watermark.Height)));
            }

            DrawingVisual drawingVisual = new DrawingVisual();
            using(DrawingContext drawingContext = drawingVisual.RenderOpen()) {
                drawingContext.DrawDrawing(group);
            }

            System.Windows.Media.Imaging.RenderTargetBitmap resizedImage = new System.Windows.Media.Imaging.RenderTargetBitmap((int)info.ImageSize.Width, (int)info.ImageSize.Height, Dpi, Dpi, PixelFormats.Default);
            resizedImage.Render(drawingVisual);

            return resizedImage;
        }
    }
    public class ResizeInfo {
        public ResizeInfo() {
        }
        public ResizeInfo(int width, int height) {
        ImageSize = new SizeF(width, height);
            SourceRect = new RectangleF(Point.Empty, ImageSize);
        }
        public SizeF ImageSize {
            get; set;
        }
        public RectangleF SourceRect {
            get; set;
        }
        public bool ShouldScale {
            get {
                if(SourceRect.X != 0.0f || SourceRect.Y != 0.0f) {
                    return true;
                }

                return ImageSize.Width != SourceRect.Width || ImageSize.Height != SourceRect.Height;
            }
        }
    }
    public enum FileResizeMode {
        [Description("Fit Long Side")]
        ZoomInside,
        [Description("Fit Short Side")]
        ZoomOutside,
        [Description("Resize Long Side")]
        LongSide,
        [Description("Resize Short Side")]
        ShortSide
    }
    public enum ImageDimensionMode {
        [Description("cm.")]
        Centimeters,
        [Description("in.")]
        Inches,
        [Description("px.")]
        Pixels
    }
    public enum ResolutionMode {
        [Description("pixels per inch")]
        PixelsPerInch,
        [Description("pixels per cm")]
        PixelsPerCm,
    }
}
