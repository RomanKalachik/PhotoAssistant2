using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.Controls.Wpf {
    public class BackgroundImageLoader {
        static BackgroundImageLoader defaultLoader;
        public static BackgroundImageLoader Default {
            get {
                if(defaultLoader == null)
                    defaultLoader = new BackgroundImageLoader();
                return defaultLoader;
            }
        }
        public void LoadFileImageInBackground(DmFile file, RunWorkerCompletedEventHandler handler) {
            if(file == null)
                return;
            ImageSource source = (ImageSource)file.ImageSource;
            if(source != null && source.IsFrozen) {
                handler(this, new RunWorkerCompletedEventArgs(source, null, false));
                return;
            }
            if(file.LoadingImageSource)
                return;
            file.LoadingImageSource = true;
            var worker = new BackgroundWorker() { WorkerReportsProgress = true };
            file.Worker = worker;
            worker.DoWork += (sender, args) => {
                var uri = args.Argument as Uri;
                var image = new BitmapImage();

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.DownloadProgress += (s, e) => worker.ReportProgress(e.Progress);
                image.DownloadFailed += (s, e) => Dispatcher.CurrentDispatcher.InvokeShutdown();
                image.DecodeFailed += (s, e) => Dispatcher.CurrentDispatcher.InvokeShutdown();
                image.DownloadCompleted += (s, e) => {
                    image.Freeze();
                    args.Result = image;
                    file.ImageSource = image;
                    file.LoadingImageSource = false;
                    Dispatcher.CurrentDispatcher.InvokeShutdown();
                };
                image.UriSource = uri;
                image.EndInit();

                if(image.IsDownloading == false) {
                    image.Freeze();
                    args.Result = image;
                    file.ImageSource = image;
                    file.LoadingImageSource = false;
                } else {
                    Dispatcher.Run();
                }
            };
            worker.RunWorkerCompleted += handler;
            worker.RunWorkerAsync(GetFileUri(file));
        }
        private Uri GetFileUri(DmFile file) {
            if(System.IO.File.Exists(file.Path))
                return new Uri(file.Path, UriKind.Absolute);
            else
                return new Uri(file.ThumbFileName, UriKind.Absolute);
        }

        public void LoadFileImageInBackground(List<DmFile> files, RunWorkerCompletedEventHandler handler) {
            var worker = new BackgroundWorker() { WorkerReportsProgress = true };
            worker.DoWork += (sender, args) => {
                List<DmFile> fileList = args.Argument as List<DmFile>;
                foreach(DmFile file in fileList) {
                    if(file.ImageSource != null || file.LoadingImageSource)
                        continue;
                    file.LoadingImageSource = true;
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.DownloadProgress += (s, e) => worker.ReportProgress(e.Progress);
                    image.DownloadFailed += (s, e) => Dispatcher.CurrentDispatcher.InvokeShutdown();
                    image.DecodeFailed += (s, e) => Dispatcher.CurrentDispatcher.InvokeShutdown();
                    image.DownloadCompleted += (s, e) => {
                        image.Freeze();
                        args.Result = image;
                        file.ImageSource = image;
                        file.ImageSource = image;
                        file.LoadingImageSource = false;
                        Dispatcher.CurrentDispatcher.InvokeShutdown();
                    };
                    image.UriSource = GetFileUri(file);
                    image.EndInit();

                    if(image.IsDownloading == false) {
                        image.Freeze();
                        args.Result = image;
                        file.LoadingImageSource = false;
                        file.ImageSource = image;
                        file.ImageSource = image;
                    } else {
                        Dispatcher.Run();
                    }
                }
            };
            worker.RunWorkerCompleted += handler;
            worker.RunWorkerAsync(files);
        }
    }
}
