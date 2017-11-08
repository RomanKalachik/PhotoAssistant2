using DevExpress.Utils.Serializing;
using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace PhotoAssistant.Core {
    public class StorageManager {
        static StorageManager defaultManager;
        public static StorageManager Default {
            get {
                if(defaultManager == null) {
                    defaultManager = new StorageManager();
                }
                return defaultManager;
            }
        }
        public StorageManager() {
        LastCheckedTime = DateTime.Now.AddYears(-1);
            CheckPeriod = TimeSpan.FromMinutes(1);
            UpdateDevices();
        }
        IStorageManagerDialogsProvider dialogsProvider;
        public IStorageManagerDialogsProvider DialogsProvider {
            get => dialogsProvider;
            set {
                if(DialogsProvider == value) {
                    return;
                }

                IStorageManagerDialogsProvider prev = DialogsProvider;
                dialogsProvider = value;
                OnDialogsProviderChanged(prev);
            }
        }
        void OnDialogsProviderChanged(IStorageManagerDialogsProvider prev) {
            if(prev != null) {
                prev.Manager = null;
            }

            if(DialogsProvider != null) {
                DialogsProvider.Manager = this;
            }
        }
        DmModel model;
        public DmModel Model {
            get => model;
            set {
                if(Model == value) {
                    return;
                }

                DmModel prevModel = Model;
                model = value;
                OnModelChanged(prevModel, Model);
            }
        }
        void OnModelChanged(DmModel prevModel, DmModel model) {
            Debug.WriteLine("StorageManager.ModelChanged");
            if(prevModel != null) {
                prevModel.DatabaseConnected -= OnDatabaseConnected;
            }

            if(model != null) {
                model.DatabaseConnected += OnDatabaseConnected;
            }

            StartCheckFiles();
        }
        void OnDatabaseConnected(object sender, EventArgs e) => StartCheckFiles();
        Thread CheckingThread {
            get; set;
        }
        void RunCheckingThread() {
            Debug.WriteLine("StorageManager.RunCheckingThread");
            if(CheckingThread != null) {
                CheckingThread.Abort();
            }
            CheckingThread = new Thread(DoFileCheckWork);
            CheckingThread.SetApartmentState(ApartmentState.STA);
            CheckingThread.Start();
        }
        public List<FileStorageInfo> ProblemFiles {
            get;
            set;
        }
        protected FileStorageInfoResult GetFileStorageResult(DmFile file, DmStorageVolume volume, DateTime volumeWriteTime, string path, bool shouldCheckMD5, ref string md5, ref DateTime lastWriteTime) {
            if(volume == null) {
                return FileStorageInfoResult.StorageNotSpecified;
            }

            if(!IsVolumePresent(volume.VolumeId)) {
                return FileStorageInfoResult.StorageNotPresent;
            }

            if(!File.Exists(path)) {
                return FileStorageInfoResult.FileNotExistsOnStorage;
            }

            lastWriteTime = File.GetLastWriteTime(path);
            if(lastWriteTime != volumeWriteTime) {
                return FileStorageInfoResult.LastWriteTimeDifferent;
            }
            if(shouldCheckMD5) {
                md5 = MD5Helper.CalculateMD5(path);
                if(!string.Equals(md5, file.MD5Hash)) {
                    return FileStorageInfoResult.MD5NotValid;
                }
            }
            return FileStorageInfoResult.Ok;
        }
        public bool CheckStorage() {
            if(!HasAnyStorage) {
                DisplayNoStorageTask();
                if(!HasAnyStorage) {
                    DisplayOperationCanceledTask();
                    return false;
                }
            }
            if(!HasAnyBackup) {
                DisplayNoBackupTask();
            }
            if(HasAnyStorage && !IsAnyStoragePresent) {
                DisplayStorageNotPresentTask();
                if(!IsAnyStoragePresent) {
                    return false;
                }
            }
            if(HasAnyBackup && !IsAnyBackupStoragePresent) {
                DisplayBackupNotPresentTask();
            }
            return true;
        }
        void DisplayOperationCanceledTask() => DialogsProvider.DisplayOperationCanceledTask();
        void DisplayBackupNotPresentTask() => DialogsProvider.DisplayBackupNotPresentTask();
        void DisplayStorageNotPresentTask() => DialogsProvider.DisplayStorageNotPresentTask();
        void DisplayNoBackupTask() => DialogsProvider.DisplayNoBackupTask();
        void DisplayNoStorageTask() => DialogsProvider.DisplayNoStorageTask();
        public StorageVolumeInfo GetStorageVolumeInfo(string root) {
            try {
                DriveInfo info = new DriveInfo(root);
                return GetStorageVolume(info);
            } catch(Exception) {
                return null;
            }
        }
        static readonly int TimerPeriod = 30 * 1000;
        System.Windows.Forms.Timer SchedulerTimer {
            get; set;
        }
        protected void RunSchedulerTimer() {
            Debug.WriteLine("StorageManager.RunSchedulerTimer");
            if(SchedulerTimer != null) {
                SchedulerTimer.Stop();
                SchedulerTimer.Dispose();
            }
            if(Model == null) {
                return;
            }

            SchedulerTimer = new System.Windows.Forms.Timer();
            SchedulerTimer.Interval = TimerPeriod;
            SchedulerTimer.Tick += SchedulerTimer_Tick;
            SchedulerTimer.Start();
        }
        void SchedulerTimer_Tick(object sender, EventArgs e) {
            Debug.WriteLine("OnSchedulerTimerTick: " + DateTime.Now.ToLongTimeString());
            if(!CheckScheduler()) {
                return;
            }

            IsCheckingFiles = true;
            RunCheckingThread();
        }
        protected bool IsCheckingFiles {
            get; set;
        }
        bool CheckScheduler() {
            TimeSpan period = TimeSpan.FromTicks(DateTime.Now.Ticks - LastCheckedTime.Ticks);
            Debug.WriteLine("CheckPeriod = " + CheckPeriod + "  Elapsed time = " + period);
            if(IsCheckingFiles) {
                Debug.WriteLine("Already Checking Files...");
            }

            if(period < CheckPeriod || IsCheckingFiles) {
                Debug.WriteLine("Do not start checking.");
                return false;
            }
            Debug.WriteLine("Start checking.");
            return true;
        }
        protected List<StorageVolumeInfo> StorageSaveState {
            get; set;
        }
        protected List<StorageVolumeInfo> BackupSaveState {
            get; set;
        }
        public void SaveStorageState() {
            StorageSaveState = new List<StorageVolumeInfo>();
            foreach(StorageVolumeInfo volume in Storage) {
                StorageSaveState.Add(volume);
            }
        }
        public void SaveBackupState() {
            BackupSaveState = new List<StorageVolumeInfo>();
            foreach(StorageVolumeInfo volume in Backup) {
                BackupSaveState.Add(volume);
            }
        }
        protected bool StorageStateChangedCore(List<StorageVolumeInfo> state, StorageVolumeInfoCollection volumes) {
            if(state == null) {
                return true;
            }

            if(state.Count != volumes.Count) {
                return true;
            }

            for(int i = 0; i < state.Count; i++) {
                if(state[i] != volumes[i]) {
                    return true;
                }
            }
            return false;
        }
        public bool BackupStateChanged() => StorageStateChangedCore(BackupSaveState, Backup);
        public bool StorageStateChanged() => StorageStateChangedCore(StorageSaveState, Storage);
        protected DateTime InvalidDateTime => new DateTime(1900, 1, 1);
        DateTime lastCheckedTime;
        [XtraSerializableProperty]
        public DateTime LastCheckedTime {
            get {
                if(lastCheckedTime == null) {
                    lastCheckedTime = InvalidDateTime;
                }

                return lastCheckedTime;
            }
            set => lastCheckedTime = value;
        }
        public void StartCheckFiles() {
            Debug.WriteLine("StorageManager.StartCheckFiles");
            LastCheckedTime = InvalidDateTime;
            RunSchedulerTimer();
        }
        [XtraSerializableProperty]
        public TimeSpan CheckPeriod {
            get; set;
        }
        public bool ShouldCheckMD5 {
            get; set;
        }
        void DoFileCheckWork() {
            if(Model == null) {
                return;
            }

            UpdateVolumeMountNames();
            CheckFilesForProblems();
        }
        public void UpdateVolumeMountNames() {
            UpdateVolumesPresence();
            List<DmStorageVolume> volumes = Model.GetVolumes();
            StorageVolumeInfoCollection present = GetVolumes();
            foreach(DmStorageVolume volume in volumes) {
                StorageVolumeInfo info = present.FirstOrDefault((v) => v.VolumeId == volume.VolumeId);
                volume.Name = info.IsPresent ? info.ActualName : string.Empty;
            }
        }
        public void UpdateVolumesPresence() {
            foreach(StorageVolumeInfo info in Storage) {
                info.UpdateIsPresent();
            }

            foreach(StorageVolumeInfo info in Backup) {
                info.UpdateIsPresent();
            }
        }
        protected virtual void CheckFilesForProblems() {
            ProblemFiles = new List<FileStorageInfo>();

            IEnumerable<DmFile> files = Model.GetFiles();
            string volume1md5 = string.Empty;
            DateTime volume1LastWriteTime = DateTime.Now;
            string volume2md5 = string.Empty;
            DateTime volume2LastWriteTime = DateTime.Now;

            foreach(DmFile file in files) {
                FileStorageInfoResult volume1 = GetFileStorageResult(file, file.Volume1, file.Volume1LastWriteTime, file.Path, ShouldCheckMD5, ref volume1md5, ref volume1LastWriteTime);
                FileStorageInfoResult volume2 = GetFileStorageResult(file, file.Volume2, file.Volume2LastWriteTime, file.BackupPath, ShouldCheckMD5, ref volume2md5, ref volume2LastWriteTime);

                Debug.WriteLine("StorageManager.CheckFile " + file.FileName);
                if(volume1 != FileStorageInfoResult.Ok || volume2 != FileStorageInfoResult.Ok) {
                    ProblemFiles.Add(new FileStorageInfo() { File = file, Volume1Result = volume1, Volume2Result = volume2, Volume1MD5 = volume1md5, Volume2MD5 = volume2md5, Volume1LastWriteTime = volume1LastWriteTime, Volume2LastWriteTime = volume2LastWriteTime });
                }
                Debug.WriteLine("Result: Storage -> " + volume1.ToString() + "  Backup -> " + volume2.ToString());
            }
            IsCheckingFiles = false;
            LastCheckedTime = DateTime.Now;
        }
        bool IsVolumePresent(string volumeId) {
            foreach(StorageVolumeInfo info in Storage) {
                if(info.VolumeId == volumeId && info.IsPresent) {
                    return true;
                }
            }
            foreach(StorageVolumeInfo info in Backup) {
                if(info.VolumeId == volumeId && info.IsPresent) {
                    return true;
                }
            }
            return false;
        }
        public StorageVolumeInfoCollection PresentVolumes {
            get; private set;
        }
        public void UpdateDevices() {
            PresentVolumes = GetVolumes();
            foreach(StorageVolumeInfo storage in Storage) {
                StorageVolumeInfo present = PresentVolumes.FirstOrDefault((v) => v.VolumeId == storage.VolumeId);
                if(present != null) {
                    storage.UpdateState(present);
                }
            }
            foreach(StorageVolumeInfo storage in Backup) {
                StorageVolumeInfo present = PresentVolumes.FirstOrDefault((v) => v.VolumeId == storage.VolumeId);
                if(present != null) {
                    storage.UpdateState(present);
                }
            }
            Debug.WriteLine("StorageManager.UpdateDevices - success!");
        }
        public virtual string GetStorageMediaList(StorageVolumeInfoCollection coll, bool skipPresent) {
            StringBuilder builder = new StringBuilder();
            List<string> addedDrives = new List<string>();
            foreach(StorageVolumeInfo volume in coll) {
                if(skipPresent && volume.IsPresent) {
                    continue;
                }

                if(addedDrives.Contains(volume.Device.ProductId)) {
                    continue;
                }

                addedDrives.Add(volume.Device.ProductId);
                builder.AppendLine(volume.Device.ProductId);
            }
            return builder.ToString();
        }
        string SavedVolumeForRelativePath {
            get; set;
        }
        public string GetRelativePath(string fullName) {
            if(string.IsNullOrEmpty(SavedVolumeForRelativePath) || !fullName.StartsWith(SavedVolumeForRelativePath)) {
                SavedVolumeForRelativePath = GetVolumeFromPath(fullName);
            }

            return Path.GetDirectoryName(fullName.Substring(SavedVolumeForRelativePath.Length, fullName.Length - SavedVolumeForRelativePath.Length));
        }
        string GetVolumeFromPath(string fullName) {
            StringBuilder builder = new StringBuilder(1024);
            bool res = GetVolumePathName(fullName, builder, (uint)builder.Capacity);
            if(res) {
                return builder.ToString();
            }

            return string.Empty;
        }
        StorageVolumeInfoCollection storage;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true, 2)]
        public StorageVolumeInfoCollection Storage {
            get {
                if(storage == null) {
                    storage = new StorageVolumeInfoCollection();
                }
                return storage;
            }
        }
        StorageVolumeInfo XtraCreateStorageItem(XtraItemEventArgs e) => new StorageVolumeInfo();
        public void OnCopyFileFailed() => DialogsProvider.DisplayOperationCanceledTask();
        void XtraSetIndexStorageItem(XtraSetItemIndexEventArgs e) {
            if(e.Index == -1) {
                Storage.Add((StorageVolumeInfo)e.Item.Value);
            } else {
                Storage.Insert(e.NewIndex, ((StorageVolumeInfo)e.Item.Value));
            }
        }
        public bool SavePreviewFile(Image fullPreview, string relativePath, string fullPreviewFileName) {
            while(true) {
                try {
                    string fullName = StorageVolume.ActualName + StorageVolume.ProjectFolder + "\\" + relativePath + "\\" + fullPreviewFileName;
                    if(File.Exists(fullName)) {
                        return true;
                    }

                    fullPreview.Save(fullName);
                    break;
                } catch(Exception e) {
                    if(!ParseException(null, StorageVolume, e, false)) {
                        return false;
                    }
                }
            }
            return true;
        }
        protected bool StorageVolumeInitialized {
            get; set;
        }
        protected bool BackupVolumeInitialized {
            get; set;
        }
        StorageVolumeInfo storageVolume;
        public StorageVolumeInfo StorageVolume {
            get => storageVolume;
            protected set {
                if(StorageVolume == value) {
                    return;
                }

                storageVolume = value;
                OnStorageVolumeChanged();
            }
        }
        protected virtual void OnStorageVolumeChanged() {
            if(StorageVolumeChanged != null) {
                StorageVolumeChanged(this, EventArgs.Empty);
            }
        }
        protected virtual void OnBackupVolumeChanged() {
            if(BackupVolumeChanged != null) {
                BackupVolumeChanged(this, EventArgs.Empty);
            }
        }
        StorageVolumeInfo backupVolume;
        public StorageVolumeInfo BackupVolume {
            get => backupVolume;
            protected set {
                if(BackupVolume == value) {
                    return;
                }

                backupVolume = value;
                OnBackupVolumeChanged();
            }
        }
        public void ResetVolumes() => StorageVolumeInitialized = BackupVolumeInitialized = false;
        public event EventHandler StorageVolumeChanged;
        public event EventHandler BackupVolumeChanged;
        public void CheckInitializeStorageVolumes() {
            if(!StorageVolumeInitialized) {
                StorageVolume = GetAvailableStorageVolume();
                StorageVolumeInitialized = true;
            }
            if(!BackupVolumeInitialized) {
                BackupVolume = GetAvailableBackupVolume();
                BackupVolumeInitialized = true;
            }
        }
        public event FileCopyEventHandler FileCopied;
        protected void RaiseFileCopied(FileCopyEventArgs e) {
            if(FileCopied != null) {
                FileCopied(this, e);
            }
        }
        public bool CopyFile(string file, string relativePath) {
            FileInfo fileInfo = null;
            while(true) {
                try {
                    fileInfo = CopyFile(file, StorageVolume, relativePath, false);
                    RaiseFileCopied(new FileCopyEventArgs() { FileInfo = fileInfo, IsBackupVolume = false, StorageVolume = StorageVolume });
                    break;
                } catch(Exception e) {
                    if(ParseException(fileInfo, StorageVolume, e, false)) {
                        continue;
                    }

                    return false;
                }
            }
            while(true) {
                try {
                    fileInfo = CopyFile(file, BackupVolume, relativePath, true);
                    RaiseFileCopied(new FileCopyEventArgs() { FileInfo = fileInfo, IsBackupVolume = true, StorageVolume = BackupVolume });
                    break;
                } catch(Exception e) {
                    if(ParseException(fileInfo, BackupVolume, e, true)) {
                        continue;
                    }

                    return false;
                }
            }
            return true;
        }
        protected virtual bool IsStorageFullException(Exception e) {
            IOException ioe = e as IOException;
            if(ioe == null) {
                return false;
            }

            const int ERROR_HANDLE_DISK_FULL = 0x27;
            const int ERROR_DISK_FULL = 0x70;
            return ioe.HResult == ERROR_HANDLE_DISK_FULL || ioe.HResult == ERROR_DISK_FULL;
        }
        bool ParseException(FileInfo fileInfo, StorageVolumeInfo volume, Exception e, bool isBackup) {
            if(IsStorageFullException(e)) {
                return ProcessStorageFullException(fileInfo, volume, e, isBackup);
            }

            IOException ioe = e as IOException;
            if(ioe != null) {
                return ProcessIOException(fileInfo, e, isBackup);
            }

            UnauthorizedAccessException uae = e as UnauthorizedAccessException;
            if(uae != null) {
                return ProcessUnautorizedException(fileInfo, e, isBackup);
            }

            return false;
        }
        bool ProcessIOException(FileInfo fileInfo, Exception e, bool isBackup) => false;
        bool ProcessUnautorizedException(FileInfo fileInfo, Exception e, bool isBackup) => false;
        bool ProcessStorageFullException(FileInfo fileInfo, StorageVolumeInfo volume, Exception e, bool isBackup) {
            volume.IsFull = true;
            UpdateVolumesPresence();
            if(isBackup) {
                while(true) {
                    StorageVolumeInfo info = GetAvailableBackupVolume();
                    if(info != null) {
                        BackupVolume = info;
                        return true;
                    }
                    if(HasNonPresentBackupDevices) {
                        if(!DialogsProvider.DisplayAttachNonPresentBackupDevices()) {
                            return false;
                        }
                    } else if(!DialogsProvider.DisplayAddNewDevicesToBackupMessage()) {
                        return false;
                    }

                    UpdateVolumesPresence();
                }
            } else {
                while(true) {
                    StorageVolumeInfo info = GetAvailableStorageVolume();
                    if(info != null) {
                        StorageVolume = info;
                        return true;
                    }
                    if(HasNonPresentStorageDevices) {
                        if(!DialogsProvider.DisplayAttachNonPresentStorageDevices()) {
                            return false;
                        }
                    } else if(!DialogsProvider.DisplayAddNewDevicesToStorageMessage()) {
                        return false;
                    }

                    UpdateVolumesPresence();
                }
            }
        }
        public virtual FileInfo CopyFile(string file, StorageVolumeInfo volume, string relativePath, bool isBackup) {
            if(volume == null) {
                return null;
            }

            string destPath = volume.ActualName + volume.ProjectFolder + "\\" + relativePath;
            if(!Directory.Exists(Path.GetDirectoryName(destPath))) {
                Directory.CreateDirectory(Path.GetDirectoryName(destPath));
            }

            File.Copy(file, destPath, true);
            return new FileInfo(destPath);
        }
        public void AllowStorage(object sender, AllowStorageEventArgs e) {
            if(ContainsStorage(Storage, e.StorageInfo)) {
                e.Allow = false;
            }
        }
        bool ContainsStorage(StorageVolumeInfoCollection storage, StorageVolumeInfo storageInfo) {
            foreach(StorageVolumeInfo info in storage) {
                if(info.VolumeId == storageInfo.VolumeId) {
                    return true;
                }
            }
            return false;
        }
        StorageVolumeInfoCollection backup;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true, 3)]
        public StorageVolumeInfoCollection Backup {
            get {
                if(backup == null) {
                    backup = new StorageVolumeInfoCollection();
                }
                return backup;
            }
        }
        public bool HasAnyStorage => Storage.Count > 0;
        public bool HasAnyBackup => Backup.Count > 0;
        public bool IsAnyStoragePresent => IsAnyStoragePresentCore(Storage);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetVolumePathNamesForVolumeNameW(string lpszVolumeName, char[] lpszVolumePathNames, uint cchBuferLength, ref uint lpcchReturnLength);
        bool IsAnyStoragePresentCore(StorageVolumeInfoCollection storage) {
            foreach(StorageVolumeInfo info in storage) {
                info.UpdateIsPresent();
                if(info.IsPresent) {
                    return true;
                }
            }
            return false;
        }
        public bool IsAnyBackupStoragePresent => IsAnyStoragePresentCore(Backup);
        StorageVolumeInfo XtraCreateBackupItem(XtraItemEventArgs e) => new StorageVolumeInfo();
        protected virtual StorageVolumeInfo GetStorageVolumeForFileCore(StorageVolumeInfoCollection coll, long freeSpace) {
            foreach(StorageVolumeInfo volume in coll) {
                if(!volume.IsPresent || volume.IsFull) {
                    continue;
                }

                long space = GetAvailableFreeSpace(volume);
                if(space >= freeSpace) {
                    return volume;
                }
            }
            return null;
        }
        protected virtual long GetAvailableFreeSpace(StorageVolumeInfo volume) {
            DriveInfo di = new DriveInfo(volume.ActualName);
            return di.AvailableFreeSpace;
        }
        protected bool IsAllStorageVolumesPresent => Storage.Count((v) => v.IsPresent) == Storage.Count;
        protected bool IsAllBackupVolumesPresent => Backup.Count((v) => v.IsPresent) == Backup.Count;
        static long MinimumFreeSpace = 100 * 1024 * 1024;
        public StorageVolumeInfo GetAvailableStorageVolume() {
            while(true) {
                StorageVolumeInfo volume = GetStorageVolumeForFileCore(Storage, MinimumFreeSpace);
                if(volume != null) {
                    return volume;
                }

                if(IsAllStorageVolumesPresent) {
                    if(!DialogsProvider.DisplayAddNewDevicesToStorageMessage()) {
                        break;
                    }

                    UpdateVolumesPresence();
                } else {
                    if(!DialogsProvider.DisplayAttachNonPresentStorageDevices()) {
                        break;
                    }

                    UpdateVolumesPresence();
                }
            }
            return null;
        }
        public StorageVolumeInfo GetAvailableBackupVolume() {
            StorageVolumeInfo volume = GetStorageVolumeForFileCore(Backup, MinimumFreeSpace);
            if(volume != null) {
                return volume;
            }

            return null;
        }
        void XtraSetIndexBackupItem(XtraSetItemIndexEventArgs e) {
            if(e.Index == -1) {
                Backup.Add((StorageVolumeInfo)e.Item.Value);
            } else {
                Backup.Insert(e.NewIndex, ((StorageVolumeInfo)e.Item.Value));
            }
        }
        public static class DeviceClassGuid {
            public static Guid DiskDrive = new Guid("{4d36e967-e325-11ce-bfc1-08002be10318}");
        }
        public static class DeviceInterfaceGuid {
            public static Guid DiskInterface = new Guid("{53f56307-b6bf-11d0-94f2-00a0c91efb8b}");
            public static Guid VolumeInterface = new Guid("53F5630D-B6BF-11D0-94F2-00A0C91EFB8B");
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateFile([MarshalAs(UnmanagedType.LPTStr)] string filename, [MarshalAs(UnmanagedType.U4)] EFileAccess access, [MarshalAs(UnmanagedType.U4)] EFileShare share, IntPtr securityAttributes, [MarshalAs(UnmanagedType.U4)] ECreationDisposition creationDisposition, [MarshalAs(UnmanagedType.U4)] EFileAttributes flagsAndAttributes, IntPtr templateFile);
        [Flags]
        public enum EFileAccess : uint {
            None = 0,
            AccessSystemSecurity = 0x1000000,
            MaximumAllowed = 0x2000000,

            Delete = 0x10000,
            ReadControl = 0x20000,
            WriteDAC = 0x40000,
            WriteOwner = 0x80000,
            Synchronize = 0x100000,

            StandardRightsRequired = 0xF0000,
            StandardRightsRead = ReadControl,
            StandardRightsWrite = ReadControl,
            StandardRightsExecute = ReadControl,
            StandardRightsAll = 0x1F0000,
            SpecificRightsAll = 0xFFFF,

            FILE_READ_DATA = 0x0001,
            FILE_LIST_DIRECTORY = 0x0001,
            FILE_WRITE_DATA = 0x0002,
            FILE_ADD_FILE = 0x0002,
            FILE_APPEND_DATA = 0x0004,
            FILE_ADD_SUBDIRECTORY = 0x0004,
            FILE_CREATE_PIPE_INSTANCE = 0x0004,
            FILE_READ_EA = 0x0008,
            FILE_WRITE_EA = 0x0010,
            FILE_EXECUTE = 0x0020,
            FILE_TRAVERSE = 0x0020,
            FILE_DELETE_CHILD = 0x0040,
            FILE_READ_ATTRIBUTES = 0x0080,
            FILE_WRITE_ATTRIBUTES = 0x0100,

            GenericRead = 0x80000000,
            GenericWrite = 0x40000000,
            GenericExecute = 0x20000000,
            GenericAll = 0x10000000,

            SPECIFIC_RIGHTS_ALL = 0x00FFFF,
            FILE_ALL_ACCESS =
            StandardRightsRequired | Synchronize | 0x1FF,

            FILE_GENERIC_READ =
            StandardRightsRead | FILE_READ_DATA | FILE_READ_ATTRIBUTES | FILE_READ_EA | Synchronize,

            FILE_GENERIC_WRITE =
            StandardRightsWrite | FILE_WRITE_DATA | FILE_WRITE_ATTRIBUTES | FILE_WRITE_EA | FILE_APPEND_DATA | Synchronize,

            FILE_GENERIC_EXECUTE =
            StandardRightsExecute | FILE_READ_ATTRIBUTES | FILE_EXECUTE | Synchronize
        }
        [Flags]
        public enum EFileShare : uint {
            /// <summary>
            /// 
            /// </summary>
            None = 0x00000000,
            /// <summary>
            /// Enables subsequent open operations on an object to request read access. 
            /// Otherwise, other processes cannot open the object if they request read access. 
            /// If this flag is not specified, but the object has been opened for read access, the function fails.
            /// </summary>
            Read = 0x00000001,
            /// <summary>
            /// Enables subsequent open operations on an object to request write access. 
            /// Otherwise, other processes cannot open the object if they request write access. 
            /// If this flag is not specified, but the object has been opened for write access, the function fails.
            /// </summary>
            Write = 0x00000002,
            /// <summary>
            /// Enables subsequent open operations on an object to request delete access. 
            /// Otherwise, other processes cannot open the object if they request delete access.
            /// If this flag is not specified, but the object has been opened for delete access, the function fails.
            /// </summary>
            Delete = 0x00000004
        }
        public enum ECreationDisposition : uint {
            /// <summary>
            /// Creates a new file. The function fails if a specified file exists.
            /// </summary>
            New = 1,
            /// <summary>
            /// Creates a new file, always. 
            /// If a file exists, the function overwrites the file, clears the existing attributes, combines the specified file attributes, 
            /// and flags with FILE_ATTRIBUTE_ARCHIVE, but does not set the security descriptor that the SECURITY_ATTRIBUTES structure specifies.
            /// </summary>
            CreateAlways = 2,
            /// <summary>
            /// Opens a file. The function fails if the file does not exist. 
            /// </summary>
            OpenExisting = 3,
            /// <summary>
            /// Opens a file, always. 
            /// If a file does not exist, the function creates a file as if dwCreationDisposition is CREATE_NEW.
            /// </summary>
            OpenAlways = 4,
            /// <summary>
            /// Opens a file and truncates it so that its size is 0 (zero) bytes. The function fails if the file does not exist.
            /// The calling process must open the file with the GENERIC_WRITE access right. 
            /// </summary>
            TruncateExisting = 5
        }
        [Flags]
        public enum EFileAttributes : uint {
            None = 0,
            Readonly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
            Directory = 0x00000010,
            Archive = 0x00000020,
            Device = 0x00000040,
            Normal = 0x00000080,
            Temporary = 0x00000100,
            SparseFile = 0x00000200,
            ReparsePoint = 0x00000400,
            Compressed = 0x00000800,
            Offline = 0x00001000,
            NotContentIndexed = 0x00002000,
            Encrypted = 0x00004000,
            Write_Through = 0x80000000,
            Overlapped = 0x40000000,
            NoBuffering = 0x20000000,
            RandomAccess = 0x10000000,
            SequentialScan = 0x08000000,
            DeleteOnClose = 0x04000000,
            BackupSemantics = 0x02000000,
            PosixSemantics = 0x01000000,
            OpenReparsePoint = 0x00200000,
            OpenNoRecall = 0x00100000,
            FirstPipeInstance = 0x00080000
        }
        [Flags]
        public enum EMethod : uint {
            Buffered = 0,
            InDirect = 1,
            OutDirect = 2,
            Neither = 3
        }
        [Flags]
        public enum EFileDevice : uint {
            Beep = 0x00000001,
            CDRom = 0x00000002,
            CDRomFileSytem = 0x00000003,
            Controller = 0x00000004,
            Datalink = 0x00000005,
            Dfs = 0x00000006,
            Disk = 0x00000007,
            DiskFileSystem = 0x00000008,
            FileSystem = 0x00000009,
            InPortPort = 0x0000000a,
            Keyboard = 0x0000000b,
            Mailslot = 0x0000000c,
            MidiIn = 0x0000000d,
            MidiOut = 0x0000000e,
            Mouse = 0x0000000f,
            MultiUncProvider = 0x00000010,
            NamedPipe = 0x00000011,
            Network = 0x00000012,
            NetworkBrowser = 0x00000013,
            NetworkFileSystem = 0x00000014,
            Null = 0x00000015,
            ParallelPort = 0x00000016,
            PhysicalNetcard = 0x00000017,
            Printer = 0x00000018,
            Scanner = 0x00000019,
            SerialMousePort = 0x0000001a,
            SerialPort = 0x0000001b,
            Screen = 0x0000001c,
            Sound = 0x0000001d,
            Streams = 0x0000001e,
            Tape = 0x0000001f,
            TapeFileSystem = 0x00000020,
            Transport = 0x00000021,
            Unknown = 0x00000022,
            Video = 0x00000023,
            VirtualDisk = 0x00000024,
            WaveIn = 0x00000025,
            WaveOut = 0x00000026,
            Port8042 = 0x00000027,
            NetworkRedirector = 0x00000028,
            Battery = 0x00000029,
            BusExtender = 0x0000002a,
            Modem = 0x0000002b,
            Vdm = 0x0000002c,
            MassStorage = 0x0000002d,
            Smb = 0x0000002e,
            Ks = 0x0000002f,
            Changer = 0x00000030,
            Smartcard = 0x00000031,
            Acpi = 0x00000032,
            Dvd = 0x00000033,
            FullscreenVideo = 0x00000034,
            DfsFileSystem = 0x00000035,
            DfsVolume = 0x00000036,
            Serenum = 0x00000037,
            Termsrv = 0x00000038,
            Ksec = 0x00000039,
            Fips = 0x0000003A,
            Infiniband = 0x0000003B,
            Vmbus = 0x0000003E,
            CryptProvider = 0x0000003F,
            Wpd = 0x00000040,
            Bluetooth = 0x00000041,
            MtComposite = 0x00000042,
            MtTransport = 0x00000043,
            Biometric = 0x00000044,
            Pmi = 0x00000045,
            Volume = 0x00000056
        }
        /// <summary>
        /// IO Control Codes
        /// Useful links:
        ///     http://www.ioctls.net/
        ///     http://msdn.microsoft.com/en-us/library/windows/hardware/ff543023(v=vs.85).aspx
        /// </summary>
        [Flags]
        public enum EIOControlCode : uint {
            VolumeGetVolumeDiskExtents = (EFileDevice.Volume << 16) | (0x0000 << 2) | EMethod.Buffered | (0 << 14),
            StorageQueryProperty = (EFileDevice.MassStorage << 16) | (0x0500 << 2) | EMethod.Buffered | (0 << 14),
            StorageCheckVerify = (EFileDevice.MassStorage << 16) | (0x0200 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            StorageCheckVerify2 = (EFileDevice.MassStorage << 16) | (0x0200 << 2) | EMethod.Buffered | (0 << 14),
            StorageMediaRemoval = (EFileDevice.MassStorage << 16) | (0x0201 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            StorageEjectMedia = (EFileDevice.MassStorage << 16) | (0x0202 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            StorageLoadMedia = (EFileDevice.MassStorage << 16) | (0x0203 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            StorageLoadMedia2 = (EFileDevice.MassStorage << 16) | (0x0203 << 2) | EMethod.Buffered | (0 << 14),
            StorageReserve = (EFileDevice.MassStorage << 16) | (0x0204 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            StorageRelease = (EFileDevice.MassStorage << 16) | (0x0205 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            StorageFindNewDevices = (EFileDevice.MassStorage << 16) | (0x0206 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            StorageEjectionControl = (EFileDevice.MassStorage << 16) | (0x0250 << 2) | EMethod.Buffered | (0 << 14),
            StorageMcnControl = (EFileDevice.MassStorage << 16) | (0x0251 << 2) | EMethod.Buffered | (0 << 14),
            StorageGetMediaTypes = (EFileDevice.MassStorage << 16) | (0x0300 << 2) | EMethod.Buffered | (0 << 14),
            StorageGetMediaTypesEx = (EFileDevice.MassStorage << 16) | (0x0301 << 2) | EMethod.Buffered | (0 << 14),
            StorageResetBus = (EFileDevice.MassStorage << 16) | (0x0400 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            StorageResetDevice = (EFileDevice.MassStorage << 16) | (0x0401 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            StorageGetDeviceNumber = (EFileDevice.MassStorage << 16) | (0x0420 << 2) | EMethod.Buffered | (0 << 14),
            StoragePredictFailure = (EFileDevice.MassStorage << 16) | (0x0440 << 2) | EMethod.Buffered | (0 << 14),
            StorageObsoleteResetBus = (EFileDevice.MassStorage << 16) | (0x0400 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            StorageObsoleteResetDevice = (EFileDevice.MassStorage << 16) | (0x0401 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskGetDriveGeometry = (EFileDevice.Disk << 16) | (0x0000 << 2) | EMethod.Buffered | (0 << 14),
            DiskGetDriveGeometryEx = (EFileDevice.Disk << 16) | (0x0028 << 2) | EMethod.Buffered | (0 << 14),
            DiskGetPartitionInfo = (EFileDevice.Disk << 16) | (0x0001 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            DiskGetPartitionInfoEx = (EFileDevice.Disk << 16) | (0x0012 << 2) | EMethod.Buffered | (0 << 14),
            DiskSetPartitionInfo = (EFileDevice.Disk << 16) | (0x0002 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskGetDriveLayout = (EFileDevice.Disk << 16) | (0x0003 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            DiskSetDriveLayout = (EFileDevice.Disk << 16) | (0x0004 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskVerify = (EFileDevice.Disk << 16) | (0x0005 << 2) | EMethod.Buffered | (0 << 14),
            DiskFormatTracks = (EFileDevice.Disk << 16) | (0x0006 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskReassignBlocks = (EFileDevice.Disk << 16) | (0x0007 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskPerformance = (EFileDevice.Disk << 16) | (0x0008 << 2) | EMethod.Buffered | (0 << 14),
            DiskIsWritable = (EFileDevice.Disk << 16) | (0x0009 << 2) | EMethod.Buffered | (0 << 14),
            DiskLogging = (EFileDevice.Disk << 16) | (0x000a << 2) | EMethod.Buffered | (0 << 14),
            DiskFormatTracksEx = (EFileDevice.Disk << 16) | (0x000b << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskHistogramStructure = (EFileDevice.Disk << 16) | (0x000c << 2) | EMethod.Buffered | (0 << 14),
            DiskHistogramData = (EFileDevice.Disk << 16) | (0x000d << 2) | EMethod.Buffered | (0 << 14),
            DiskHistogramReset = (EFileDevice.Disk << 16) | (0x000e << 2) | EMethod.Buffered | (0 << 14),
            DiskRequestStructure = (EFileDevice.Disk << 16) | (0x000f << 2) | EMethod.Buffered | (0 << 14),
            DiskRequestData = (EFileDevice.Disk << 16) | (0x0010 << 2) | EMethod.Buffered | (0 << 14),
            DiskControllerNumber = (EFileDevice.Disk << 16) | (0x0011 << 2) | EMethod.Buffered | (0 << 14),
            DiskSmartGetVersion = (EFileDevice.Disk << 16) | (0x0020 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            DiskSmartSendDriveCommand = (EFileDevice.Disk << 16) | (0x0021 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskSmartRcvDriveData = (EFileDevice.Disk << 16) | (0x0022 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskUpdateDriveSize = (EFileDevice.Disk << 16) | (0x0032 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskGrowPartition = (EFileDevice.Disk << 16) | (0x0034 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskGetCacheInformation = (EFileDevice.Disk << 16) | (0x0035 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            DiskSetCacheInformation = (EFileDevice.Disk << 16) | (0x0036 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskDeleteDriveLayout = (EFileDevice.Disk << 16) | (0x0040 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskFormatDrive = (EFileDevice.Disk << 16) | (0x00f3 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskSenseDevice = (EFileDevice.Disk << 16) | (0x00f8 << 2) | EMethod.Buffered | (0 << 14),
            DiskCheckVerify = (EFileDevice.Disk << 16) | (0x0200 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            DiskMediaRemoval = (EFileDevice.Disk << 16) | (0x0201 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            DiskEjectMedia = (EFileDevice.Disk << 16) | (0x0202 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            DiskLoadMedia = (EFileDevice.Disk << 16) | (0x0203 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            DiskReserve = (EFileDevice.Disk << 16) | (0x0204 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            DiskRelease = (EFileDevice.Disk << 16) | (0x0205 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            DiskFindNewDevices = (EFileDevice.Disk << 16) | (0x0206 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            DiskGetMediaTypes = (EFileDevice.Disk << 16) | (0x0300 << 2) | EMethod.Buffered | (0 << 14),
            DiskSetPartitionInfoEx = (EFileDevice.Disk << 16) | (0x0013 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskGetDriveLayoutEx = (EFileDevice.Disk << 16) | (0x0014 << 2) | EMethod.Buffered | (0 << 14),
            DiskSetDriveLayoutEx = (EFileDevice.Disk << 16) | (0x0015 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskCreateDisk = (EFileDevice.Disk << 16) | (0x0016 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            DiskGetLengthInfo = (EFileDevice.Disk << 16) | (0x0017 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            ChangerGetParameters = (EFileDevice.Changer << 16) | (0x0000 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            ChangerGetStatus = (EFileDevice.Changer << 16) | (0x0001 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            ChangerGetProductData = (EFileDevice.Changer << 16) | (0x0002 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            ChangerSetAccess = (EFileDevice.Changer << 16) | (0x0004 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            ChangerGetElementStatus = (EFileDevice.Changer << 16) | (0x0005 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            ChangerInitializeElementStatus = (EFileDevice.Changer << 16) | (0x0006 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            ChangerSetPosition = (EFileDevice.Changer << 16) | (0x0007 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            ChangerExchangeMedium = (EFileDevice.Changer << 16) | (0x0008 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            ChangerMoveMedium = (EFileDevice.Changer << 16) | (0x0009 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            ChangerReinitializeTarget = (EFileDevice.Changer << 16) | (0x000A << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            ChangerQueryVolumeTags = (EFileDevice.Changer << 16) | (0x000B << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            FsctlRequestOplockLevel1 = (EFileDevice.FileSystem << 16) | (0 << 2) | EMethod.Buffered | (0 << 14),
            FsctlRequestOplockLevel2 = (EFileDevice.FileSystem << 16) | (1 << 2) | EMethod.Buffered | (0 << 14),
            FsctlRequestBatchOplock = (EFileDevice.FileSystem << 16) | (2 << 2) | EMethod.Buffered | (0 << 14),
            FsctlOplockBreakAcknowledge = (EFileDevice.FileSystem << 16) | (3 << 2) | EMethod.Buffered | (0 << 14),
            FsctlOpBatchAckClosePending = (EFileDevice.FileSystem << 16) | (4 << 2) | EMethod.Buffered | (0 << 14),
            FsctlOplockBreakNotify = (EFileDevice.FileSystem << 16) | (5 << 2) | EMethod.Buffered | (0 << 14),
            FsctlLockVolume = (EFileDevice.FileSystem << 16) | (6 << 2) | EMethod.Buffered | (0 << 14),
            FsctlUnlockVolume = (EFileDevice.FileSystem << 16) | (7 << 2) | EMethod.Buffered | (0 << 14),
            FsctlDismountVolume = (EFileDevice.FileSystem << 16) | (8 << 2) | EMethod.Buffered | (0 << 14),
            FsctlIsVolumeMounted = (EFileDevice.FileSystem << 16) | (10 << 2) | EMethod.Buffered | (0 << 14),
            FsctlIsPathnameValid = (EFileDevice.FileSystem << 16) | (11 << 2) | EMethod.Buffered | (0 << 14),
            FsctlMarkVolumeDirty = (EFileDevice.FileSystem << 16) | (12 << 2) | EMethod.Buffered | (0 << 14),
            FsctlQueryRetrievalPointers = (EFileDevice.FileSystem << 16) | (14 << 2) | EMethod.Neither | (0 << 14),
            FsctlGetCompression = (EFileDevice.FileSystem << 16) | (15 << 2) | EMethod.Buffered | (0 << 14),
            FsctlSetCompression = (EFileDevice.FileSystem << 16) | (16 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            FsctlMarkAsSystemHive = (EFileDevice.FileSystem << 16) | (19 << 2) | EMethod.Neither | (0 << 14),
            FsctlOplockBreakAckNo2 = (EFileDevice.FileSystem << 16) | (20 << 2) | EMethod.Buffered | (0 << 14),
            FsctlInvalidateVolumes = (EFileDevice.FileSystem << 16) | (21 << 2) | EMethod.Buffered | (0 << 14),
            FsctlQueryFatBpb = (EFileDevice.FileSystem << 16) | (22 << 2) | EMethod.Buffered | (0 << 14),
            FsctlRequestFilterOplock = (EFileDevice.FileSystem << 16) | (23 << 2) | EMethod.Buffered | (0 << 14),
            FsctlFileSystemGetStatistics = (EFileDevice.FileSystem << 16) | (24 << 2) | EMethod.Buffered | (0 << 14),
            FsctlGetNtfsVolumeData = (EFileDevice.FileSystem << 16) | (25 << 2) | EMethod.Buffered | (0 << 14),
            FsctlGetNtfsFileRecord = (EFileDevice.FileSystem << 16) | (26 << 2) | EMethod.Buffered | (0 << 14),
            FsctlGetVolumeBitmap = (EFileDevice.FileSystem << 16) | (27 << 2) | EMethod.Neither | (0 << 14),
            FsctlGetRetrievalPointers = (EFileDevice.FileSystem << 16) | (28 << 2) | EMethod.Neither | (0 << 14),
            FsctlMoveFile = (EFileDevice.FileSystem << 16) | (29 << 2) | EMethod.Buffered | (0 << 14),
            FsctlIsVolumeDirty = (EFileDevice.FileSystem << 16) | (30 << 2) | EMethod.Buffered | (0 << 14),
            FsctlGetHfsInformation = (EFileDevice.FileSystem << 16) | (31 << 2) | EMethod.Buffered | (0 << 14),
            FsctlAllowExtendedDasdIo = (EFileDevice.FileSystem << 16) | (32 << 2) | EMethod.Neither | (0 << 14),
            FsctlReadPropertyData = (EFileDevice.FileSystem << 16) | (33 << 2) | EMethod.Neither | (0 << 14),
            FsctlWritePropertyData = (EFileDevice.FileSystem << 16) | (34 << 2) | EMethod.Neither | (0 << 14),
            FsctlFindFilesBySid = (EFileDevice.FileSystem << 16) | (35 << 2) | EMethod.Neither | (0 << 14),
            FsctlDumpPropertyData = (EFileDevice.FileSystem << 16) | (37 << 2) | EMethod.Neither | (0 << 14),
            FsctlSetObjectId = (EFileDevice.FileSystem << 16) | (38 << 2) | EMethod.Buffered | (0 << 14),
            FsctlGetObjectId = (EFileDevice.FileSystem << 16) | (39 << 2) | EMethod.Buffered | (0 << 14),
            FsctlDeleteObjectId = (EFileDevice.FileSystem << 16) | (40 << 2) | EMethod.Buffered | (0 << 14),
            FsctlSetReparsePoint = (EFileDevice.FileSystem << 16) | (41 << 2) | EMethod.Buffered | (0 << 14),
            FsctlGetReparsePoint = (EFileDevice.FileSystem << 16) | (42 << 2) | EMethod.Buffered | (0 << 14),
            FsctlDeleteReparsePoint = (EFileDevice.FileSystem << 16) | (43 << 2) | EMethod.Buffered | (0 << 14),
            FsctlEnumUsnData = (EFileDevice.FileSystem << 16) | (44 << 2) | EMethod.Neither | (0 << 14),
            FsctlSecurityIdCheck = (EFileDevice.FileSystem << 16) | (45 << 2) | EMethod.Neither | (FileAccess.Read << 14),
            FsctlReadUsnJournal = (EFileDevice.FileSystem << 16) | (46 << 2) | EMethod.Neither | (0 << 14),
            FsctlSetObjectIdExtended = (EFileDevice.FileSystem << 16) | (47 << 2) | EMethod.Buffered | (0 << 14),
            FsctlCreateOrGetObjectId = (EFileDevice.FileSystem << 16) | (48 << 2) | EMethod.Buffered | (0 << 14),
            FsctlSetSparse = (EFileDevice.FileSystem << 16) | (49 << 2) | EMethod.Buffered | (0 << 14),
            FsctlSetZeroData = (EFileDevice.FileSystem << 16) | (50 << 2) | EMethod.Buffered | (FileAccess.Write << 14),
            FsctlQueryAllocatedRanges = (EFileDevice.FileSystem << 16) | (51 << 2) | EMethod.Neither | (FileAccess.Read << 14),
            FsctlEnableUpgrade = (EFileDevice.FileSystem << 16) | (52 << 2) | EMethod.Buffered | (FileAccess.Write << 14),
            FsctlSetEncryption = (EFileDevice.FileSystem << 16) | (53 << 2) | EMethod.Neither | (0 << 14),
            FsctlEncryptionFsctlIo = (EFileDevice.FileSystem << 16) | (54 << 2) | EMethod.Neither | (0 << 14),
            FsctlWriteRawEncrypted = (EFileDevice.FileSystem << 16) | (55 << 2) | EMethod.Neither | (0 << 14),
            FsctlReadRawEncrypted = (EFileDevice.FileSystem << 16) | (56 << 2) | EMethod.Neither | (0 << 14),
            FsctlCreateUsnJournal = (EFileDevice.FileSystem << 16) | (57 << 2) | EMethod.Neither | (0 << 14),
            FsctlReadFileUsnData = (EFileDevice.FileSystem << 16) | (58 << 2) | EMethod.Neither | (0 << 14),
            FsctlWriteUsnCloseRecord = (EFileDevice.FileSystem << 16) | (59 << 2) | EMethod.Neither | (0 << 14),
            FsctlExtendVolume = (EFileDevice.FileSystem << 16) | (60 << 2) | EMethod.Buffered | (0 << 14),
            FsctlQueryUsnJournal = (EFileDevice.FileSystem << 16) | (61 << 2) | EMethod.Buffered | (0 << 14),
            FsctlDeleteUsnJournal = (EFileDevice.FileSystem << 16) | (62 << 2) | EMethod.Buffered | (0 << 14),
            FsctlMarkHandle = (EFileDevice.FileSystem << 16) | (63 << 2) | EMethod.Buffered | (0 << 14),
            FsctlSisCopyFile = (EFileDevice.FileSystem << 16) | (64 << 2) | EMethod.Buffered | (0 << 14),
            FsctlSisLinkFiles = (EFileDevice.FileSystem << 16) | (65 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            FsctlHsmMsg = (EFileDevice.FileSystem << 16) | (66 << 2) | EMethod.Buffered | (FileAccess.ReadWrite << 14),
            FsctlNssControl = (EFileDevice.FileSystem << 16) | (67 << 2) | EMethod.Buffered | (FileAccess.Write << 14),
            FsctlHsmData = (EFileDevice.FileSystem << 16) | (68 << 2) | EMethod.Neither | (FileAccess.ReadWrite << 14),
            FsctlRecallFile = (EFileDevice.FileSystem << 16) | (69 << 2) | EMethod.Neither | (0 << 14),
            FsctlNssRcontrol = (EFileDevice.FileSystem << 16) | (70 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
            VideoQuerySupportedBrightness = (EFileDevice.Video << 16) | (0x0125 << 2) | EMethod.Buffered | (0 << 14),
            VideoQueryDisplayBrightness = (EFileDevice.Video << 16) | (0x0126 << 2) | EMethod.Buffered | (0 << 14),
            VideoSetDisplayBrightness = (EFileDevice.Video << 16) | (0x0127 << 2) | EMethod.Buffered | (0 << 14)
        }
        [DllImport("Kernel32.dll", SetLastError = false, CharSet = CharSet.Auto)]
        public static extern bool DeviceIoControl(IntPtr hDevice, EIOControlCode IoControlCode, IntPtr InBuffer, uint nInBufferSize, IntPtr OutBuffer, uint nOutBufferSize, ref uint pBytesReturned, IntPtr Overlapped);
        [StructLayout(LayoutKind.Sequential)]
        public struct DISK_EXTENT {
            [MarshalAs(UnmanagedType.U8)]
            public long DiskNumber;
            [MarshalAs(UnmanagedType.U8)]
            public ulong StartingOffset;
            [MarshalAs(UnmanagedType.U8)]
            public ulong ExtentLength;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DISK_EXTENTS {
            public uint NumberOfDiskExtents;
            public DISK_EXTENT[] DiskExtents;
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);
        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, ref SP_DEVINFO_DATA devInfo, ref Guid interfaceClassGuid, uint memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);
        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, IntPtr devInfo, ref Guid interfaceClassGuid, uint memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);
        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVICE_INTERFACE_DATA {
            public int cbSize;
            public Guid interfaceClassGuid;
            public int flags;
            UIntPtr reserved;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        public struct SP_DEVICE_INTERFACE_DETAIL_DATA {
            public int size;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string DevicePath;
        }
        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr hDevInfo, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, IntPtr deviceInterfaceDetailData, uint deviceInterfaceDetailDataSize, out uint requiredSize, ref SP_DEVINFO_DATA deviceInfoData);
        [StructLayout(LayoutKind.Sequential)]
        public struct STORAGE_DEVICE_NUMBER {
            public int DeviceType;
            public int DeviceNumber;
            public int PartitionNumber;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PARTITION_INFORMATION {
            public long StartingOffset;
            public long PartitionLength;
            public int HiddenSectors;
            public int PartitionNumber;
            public byte PartitionType;
            [MarshalAs(UnmanagedType.I1)]
            public bool BootIndicator;
            [MarshalAs(UnmanagedType.I1)]
            public bool RecognizedPartition;
            [MarshalAs(UnmanagedType.I1)]
            public bool RewritePartition;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DRIVE_LAYOUT_INFORMATION_MBR {
            public byte PartitionType;
            [MarshalAs(UnmanagedType.U1)]
            public bool BootIndicator;
            [MarshalAs(UnmanagedType.U1)]
            public bool RecognizedPartition;
            public uint HiddenSectors;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DRIVE_LAYOUT_INFORMATION_GPT {
            public Guid DiskId;
            public long StartingUsableOffset;
            public long UsableLength;
            public uint MaxPartitionCount;
        }
        [StructLayout(LayoutKind.Explicit)]
        public struct DRIVE_LAYOUT_INFORMATION_UNION {
            [FieldOffset(0)]
            public DRIVE_LAYOUT_INFORMATION_MBR Mbr;
            [FieldOffset(0)]
            public DRIVE_LAYOUT_INFORMATION_GPT Gpt;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PARTITION_INFORMATION_EX {
            [MarshalAs(UnmanagedType.U4)]
            public PARTITION_STYLE PartitionStyle;
            public long StartingOffset;
            public long PartitionLength;
            public int PartitionNumber;
            [MarshalAs(UnmanagedType.U1)]
            public bool RewritePartition;
            public PARTITION_INFORMATION_UNION DriveLayoutInformaiton;
        }
        [StructLayout(LayoutKind.Explicit)]
        public struct PARTITION_INFORMATION_UNION {
            [FieldOffset(0)]
            public PARTITION_INFORMATION_GPT Gpt;
            [FieldOffset(0)]
            public PARTITION_INFORMATION_MBR Mbr;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct PARTITION_INFORMATION_GPT {
            public Guid PartitionType;
            public Guid PartitionId;
            [MarshalAs(UnmanagedType.U8)]
            public EFIPartitionAttributes Attributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
            public string Name;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PARTITION_INFORMATION_MBR {
            public byte PartitionType;
            [MarshalAs(UnmanagedType.U1)]
            public bool BootIndicator;
            [MarshalAs(UnmanagedType.U1)]
            public bool RecognizedPartition;
            public uint HiddenSectors;
        }
        [Flags]
        public enum EFIPartitionAttributes : ulong {
            GPT_ATTRIBUTE_PLATFORM_REQUIRED = 0x0000000000000001,
            LegacyBIOSBootable = 0x0000000000000004,
            GPT_BASIC_DATA_ATTRIBUTE_NO_DRIVE_LETTER = 0x8000000000000000,
            GPT_BASIC_DATA_ATTRIBUTE_HIDDEN = 0x4000000000000000,
            GPT_BASIC_DATA_ATTRIBUTE_SHADOW_COPY = 0x2000000000000000,
            GPT_BASIC_DATA_ATTRIBUTE_READ_ONLY = 0x1000000000000000
        }
        public enum PARTITION_STYLE : int {
            MasterBootRecord = 0,
            GuidPartitionTable = 1,
            Raw = 2
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DRIVE_LAYOUT_INFORMATION_EX {
            public PARTITION_STYLE PartitionStyle;
            public int PartitionCount;
            public DRIVE_LAYOUT_INFORMATION_UNION DriveLayoutInformaiton;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 0x16)]
            public PARTITION_INFORMATION_EX[] PartitionEntry;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct VOLUME_DISK_EXTENTS {
            public int NumberOfDiskExtents;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 0x16)]
            public DISK_EXTENT[] Extents;
        }
        public enum STORAGE_PROPERTY_ID {
            StorageDeviceProperty = 0,
            StorageAdapterProperty = 1,
            StorageDeviceIdProperty = 2,
            StorageDeviceUniqueIdProperty = 3,
            StorageDeviceWriteCacheProperty = 4,
            StorageMiniportProperty = 5,
            StorageAccessAlignmentProperty = 6,
            StorageDeviceSeekPenaltyProperty = 7,
            StorageDeviceTrimProperty = 8,
            StorageDeviceWriteAggregationProperty = 9,
            StorageDeviceDeviceTelemetryProperty = 10,
            StorageDeviceLBProvisioningProperty = 11,
            StorageDevicePowerProperty = 12,
            StorageDeviceCopyOffloadProperty = 13,
            StorageDeviceResiliencyProperty = 14
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct STORAGE_PROPERTY_QUERY {
            public STORAGE_PROPERTY_ID PropertyId;
            public STORAGE_QUERY_TYPE QueryType;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1024)]
            public byte[] AdditionalParameters;
        }
        public enum STORAGE_QUERY_TYPE {
            PropertyStandardQuery = 0,
            PropertyExistsQuery = 1,
            PropertyMaskQuery = 2,
            PropertyQueryMaxDefined = 3
        }
        public struct STORAGE_DESCRIPTOR_HEADER {
            public ulong Version;
            public ulong Size;
        }
        public enum STORAGE_BUS_TYPE {
            BusTypeUnknown = 0x00,
            BusTypeScsi = 0x1,
            BusTypeAtapi = 0x2,
            BusTypeAta = 0x3,
            BusType1394 = 0x4,
            BusTypeSsa = 0x5,
            BusTypeFibre = 0x6,
            BusTypeUsb = 0x7,
            BusTypeRAID = 0x8,
            BusTypeiScsi = 0x9,
            BusTypeSas = 0xA,
            BusTypeSata = 0xB,
            BusTypeSd = 0xC,
            BusTypeMmc = 0xD,
            BusTypeVirtual = 0xE,
            BusTypeFileBackedVirtual = 0xF,
            BusTypeMax = 0x10,
            BusTypeMaxReserved = 0x7F
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct STORAGE_DEVICE_DESCRIPTOR {
            public int Version;
            public int Size;
            public byte DeviceType;
            public byte DeviceTypeModifier;
            [MarshalAs(UnmanagedType.U1)]
            public bool RemovableMedia;
            [MarshalAs(UnmanagedType.U1)]
            public bool CommandQueueing;
            public int VendorIdOffset;
            public int ProductIdOffset;
            public int ProductRevisionOffset;
            public int SerialNumberOffset;
            public STORAGE_BUS_TYPE BusType;
            public int RawPropertiesLength;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 1024)]
            public byte[] RawDeviceProperties;
        }
        [DllImport("Kernel32.dll", SetLastError = false)]
        static extern void ZeroMemory(IntPtr dest, int size);
        bool IsInvalidHandle(IntPtr handle) => handle.ToInt32() == -1;
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GetVolumeInformation(string RootPathName, StringBuilder VolumeNameBuffer, int VolumeNameSize, out uint VolumeSerialNumber, out uint MaximumComponentLength, out FileSystemFeature FileSystemFlags, StringBuilder FileSystemNameBuffer, int nFileSystemNameSize);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetVolumePathName(string lpszFileName, [Out] StringBuilder lpszVolumePathName, uint cchBufferLength);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetVolumeNameForVolumeMountPoint(string
            lpszVolumeMountPoint, [Out] StringBuilder lpszVolumeName, uint cchBufferLength);
        [Flags]
        public enum FileSystemFeature : uint {
            /// <summary>
            /// The file system supports case-sensitive file names.
            /// </summary>
            CaseSensitiveSearch = 1,
            /// <summary>
            /// The file system preserves the case of file names when it places a name on disk.
            /// </summary>
            CasePreservedNames = 2,
            /// <summary>
            /// The file system supports Unicode in file names as they appear on disk.
            /// </summary>
            UnicodeOnDisk = 4,
            /// <summary>
            /// The file system preserves and enforces access control lists (ACL).
            /// </summary>
            PersistentACLS = 8,
            /// <summary>
            /// The file system supports file-based compression.
            /// </summary>
            FileCompression = 0x10,
            /// <summary>
            /// The file system supports disk quotas.
            /// </summary>
            VolumeQuotas = 0x20,
            /// <summary>
            /// The file system supports sparse files.
            /// </summary>
            SupportsSparseFiles = 0x40,
            /// <summary>
            /// The file system supports re-parse points.
            /// </summary>
            SupportsReparsePoints = 0x80,
            /// <summary>
            /// The specified volume is a compressed volume, for example, a DoubleSpace volume.
            /// </summary>
            VolumeIsCompressed = 0x8000,
            /// <summary>
            /// The file system supports object identifiers.
            /// </summary>
            SupportsObjectIDs = 0x10000,
            /// <summary>
            /// The file system supports the Encrypted File System (EFS).
            /// </summary>
            SupportsEncryption = 0x20000,
            /// <summary>
            /// The file system supports named streams.
            /// </summary>
            NamedStreams = 0x40000,
            /// <summary>
            /// The specified volume is read-only.
            /// </summary>
            ReadOnlyVolume = 0x80000,
            /// <summary>
            /// The volume supports a single sequential write.
            /// </summary>
            SequentialWriteOnce = 0x100000,
            /// <summary>
            /// The volume supports transactions.
            /// </summary>
            SupportsTransactions = 0x200000,
        }
        public bool HasNonPresentBackupDevices => Backup.FirstOrDefault((v) => v.IsPresent == false) != null;
        public bool HasNonPresentStorageDevices => Storage.FirstOrDefault((v) => v.IsPresent == false) != null;
        public StorageVolumeInfoCollection GetVolumes() {
            Debug.WriteLine("StorageManager.ObtainDrives - start...");
            StorageVolumeInfoCollection storages = new StorageVolumeInfoCollection();

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach(DriveInfo di in drives) {
                StorageVolumeInfo volume = GetStorageVolume(di);
                if(volume == null) {
                    continue;
                }

                volume.IsPresent = true;
                storages.Add(volume);
            }
            return storages;
        }
        StorageVolumeInfo GetStorageVolume(DriveInfo di) {
            if(di.DriveType != DriveType.Fixed && di.DriveType != DriveType.Removable) {
                return null;
            }

            StringBuilder volumePathName = new StringBuilder(512);
            IntPtr outBuffer = Marshal.AllocHGlobal(1024 * 1024);
            bool returnValue;
            uint bytesReturned = 0;

            string letter = di.Name.Substring(0, di.Name.IndexOf('\\'));
            string volumePath = $@"\\.\{letter}";
            Debug.WriteLine("StorageManager.CreateFile - start...");
            IntPtr volumeHandle = CreateFile(volumePath, EFileAccess.FILE_GENERIC_READ, EFileShare.Write | EFileShare.Read, IntPtr.Zero, ECreationDisposition.OpenExisting, EFileAttributes.None, IntPtr.Zero);
            if(volumeHandle.ToInt32() == -1) {
                return null;
            }

            Debug.WriteLine("StorageManager.CreateFile - success...");
            Debug.WriteLine("StorageManager.VolumeGetVolumeDiskExtents - start...");
            returnValue = DeviceIoControl(volumeHandle, EIOControlCode.VolumeGetVolumeDiskExtents, IntPtr.Zero, 0, outBuffer, 1024, ref bytesReturned, IntPtr.Zero);
            if(!returnValue) {
                return null;
            }
            VOLUME_DISK_EXTENTS diskExtents = (VOLUME_DISK_EXTENTS)Marshal.PtrToStructure(outBuffer, typeof(VOLUME_DISK_EXTENTS));
            CloseHandle(volumeHandle);
            Debug.WriteLine("StorageManager.VolumeGetVolumeDiskExtents - success...");

            StorageDeviceInfo info = GetStorageDeviceInfo((int)(diskExtents.Extents[0].DiskNumber));
            if(info == null) {
                return null;
            }

            Debug.WriteLine("StorageManager.GetVolumeNameForVolumeMountPoint - start...");
            returnValue = GetVolumeNameForVolumeMountPoint(di.Name, volumePathName, (uint)volumePathName.Capacity);
            if(!returnValue) {
                return null;
            }
            Debug.WriteLine("StorageManager.GetVolumeNameForVolumeMountPoint - success...");
            Marshal.FreeHGlobal(outBuffer);

            StorageVolumeInfo result = null;
            try {
                result = new StorageVolumeInfo() { AvailableFreeSpace = di.AvailableFreeSpace, TotalFreeSpace = di.TotalFreeSpace, VolumeId = volumePathName.ToString(), ActualName = di.Name, VolumeLabel = di.VolumeLabel, Device = info, ExtentLength = diskExtents.Extents[0].ExtentLength, HintName = di.Name, StartingOffset = diskExtents.Extents[0].StartingOffset };
            } catch { result = null; }
            return result;
        }
        bool ShouldSkipThisDevice(SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData) => deviceInterfaceDetailData.DevicePath.Contains("#cdrom");
        StorageDeviceInfo GetStorageDeviceInfo(int diskNumber) {
            Debug.WriteLine("StorageManager.CreateFileDevice - start...");
            IntPtr buffer = Marshal.AllocHGlobal(1024 * 1024);
            string diskPath = $"\\\\.\\PhysicalDrive{diskNumber}";
            IntPtr disk = CreateFile(diskPath, EFileAccess.FILE_GENERIC_READ, EFileShare.Read | EFileShare.Write, IntPtr.Zero, ECreationDisposition.OpenExisting, EFileAttributes.Normal, IntPtr.Zero);

            if(disk.ToInt32() == -1) {
                return null;
            }
            Debug.WriteLine("StorageManager.CreateFileDevice - success...");

            uint returnBytes = 0;
            ZeroMemory(buffer, Marshal.SizeOf(typeof(STORAGE_DEVICE_NUMBER)));

            Debug.WriteLine("StorageManager.StorageGetDeviceNumber - start...");
            bool res = DeviceIoControl(disk, EIOControlCode.StorageGetDeviceNumber, IntPtr.Zero, 0, buffer, 4096, ref returnBytes, IntPtr.Zero);
            if(!res) {
                CloseHandle(disk);
                return null;
            }
            Debug.WriteLine("StorageManager.StorageGetDeviceNumber - success...");

            STORAGE_DEVICE_NUMBER deviceNumber = (STORAGE_DEVICE_NUMBER)Marshal.PtrToStructure(buffer, typeof(STORAGE_DEVICE_NUMBER));

            STORAGE_PROPERTY_QUERY query = new STORAGE_PROPERTY_QUERY();
            query.PropertyId = STORAGE_PROPERTY_ID.StorageDeviceProperty;
            query.QueryType = STORAGE_QUERY_TYPE.PropertyStandardQuery;

            ZeroMemory(buffer, 256);
            IntPtr queryPtr = Marshal.AllocHGlobal(2048);
            int inBufferSize = 1024;
            Marshal.StructureToPtr(query, queryPtr, true);

            Debug.WriteLine("StorageManager.StorageQueryProperty - start...");
            res = DeviceIoControl(disk, EIOControlCode.StorageQueryProperty, queryPtr, (uint)inBufferSize, buffer, 4096, ref returnBytes, IntPtr.Zero);
            if(!res) {
                CloseHandle(disk);
                return null;
            }

            STORAGE_DEVICE_DESCRIPTOR dd = (STORAGE_DEVICE_DESCRIPTOR)Marshal.PtrToStructure(buffer, typeof(STORAGE_DEVICE_DESCRIPTOR));
            Debug.WriteLine("StorageManager.StorageQueryProperty - success...");
            IntPtr productIdPtr = buffer + dd.ProductIdOffset;
            IntPtr productRevisionPtr = buffer + dd.ProductRevisionOffset;
            IntPtr serialNumberPtr = buffer + dd.SerialNumberOffset;
            IntPtr vendorPtr = buffer + dd.VendorIdOffset;

            string productId = dd.ProductIdOffset == 0 ? string.Empty : Marshal.PtrToStringAnsi(productIdPtr);
            string productRevision = dd.ProductRevisionOffset == 0 ? string.Empty : Marshal.PtrToStringAnsi(productRevisionPtr);
            string serialNummber = dd.SerialNumberOffset == 0 ? string.Empty : Marshal.PtrToStringAnsi(serialNumberPtr);
            string vendor = dd.VendorIdOffset == 0 ? string.Empty : Marshal.PtrToStringAnsi(vendorPtr);

            ZeroMemory(buffer, 4096);
            Debug.WriteLine("StorageManager.DiskGetDriveLayoutEx - start...");
            res = DeviceIoControl(disk, EIOControlCode.DiskGetDriveLayoutEx, IntPtr.Zero, 0, buffer, 4096, ref returnBytes, IntPtr.Zero);
            if(!res) {
                CloseHandle(disk);
                return null;
            }
            Debug.WriteLine("StorageManager.DiskGetDriveLayoutEx - success...");

            StorageDeviceInfo deviceInfo = new StorageDeviceInfo() { DeviceType = deviceNumber.DeviceType, PartitionNumber = deviceNumber.PartitionNumber, DiskNumber = diskNumber, ProductId = productId, RevisionId = productRevision, Vendor = vendor, SerialNumber = serialNummber };

            DRIVE_LAYOUT_INFORMATION_EX layoutInfo = (DRIVE_LAYOUT_INFORMATION_EX)Marshal.PtrToStructure(buffer, typeof(DRIVE_LAYOUT_INFORMATION_EX));
            for(int i = 0; i < layoutInfo.PartitionCount; i++) {
                if(layoutInfo.PartitionEntry[i].PartitionLength == 0) {
                    continue;
                }

                StoragePartitionInfo pInfo = new StoragePartitionInfo();
                pInfo.Number = layoutInfo.PartitionEntry[i].PartitionNumber;
                pInfo.Length = layoutInfo.PartitionEntry[i].PartitionLength;
                deviceInfo.Partitions.Add(pInfo);
            }

            Marshal.FreeHGlobal(buffer);
            Marshal.FreeHGlobal(queryPtr);
            CloseHandle(disk);

            return deviceInfo;
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr FindFirstVolume([Out] StringBuilder lpszVolumeName, uint cchBufferLength);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FindNextVolume(IntPtr hFindVolume, [Out] StringBuilder lpszVolumeName, uint cchBufferLength);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FindVolumeClose(IntPtr hFindVolume);
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);
        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiGetDeviceRegistryProperty(IntPtr deviceInfoSet, ref SP_DEVINFO_DATA deviceInfoData, uint property, out uint propertyRegDataType, byte[] propertyBuffer, uint propertyBufferSize, out uint requiredSize);
        static Guid GUID_DEVINTERFACE_DISK = new Guid(0x53f56307, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);
        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, IntPtr Enumerator, IntPtr hwndParent, int Flags);
        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SetupDiGetClassDevs(IntPtr ClassGuid, string Enumerator, IntPtr hwndParent, int Flags);
        public class DiGetClassFlags {
            public static int DIGCF_DEFAULT = 0x00000001;
            public static int DIGCF_PRESENT = 0x00000002;
            public static int DIGCF_ALLCLASSES = 0x00000004;
            public static int DIGCF_PROFILE = 0x00000008;
            public static int DIGCF_DEVICEINTERFACE = 0x00000010;
        }
        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, uint MemberIndex, ref SP_DEVINFO_DATA DeviceInfoData);
        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVINFO_DATA {
            public uint cbSize;
            public Guid classGuid;
            public uint devInst;
            public IntPtr reserved;
        }
    }
    public class StorageDeviceInfoCollection : Collection<StorageDeviceInfo> {
    }
    public class StorageVolumeInfoCollection : Collection<StorageVolumeInfo> {
        public event StoreageInfoCollectionEventHandler CollectionChanged;
        StorageInfoCollectionEventArgs EventArgs;
        protected override void InsertItem(int index, StorageVolumeInfo item) {
            base.InsertItem(index, item);
            if(!IsLockUpdate) {
                EventArgs = new StorageInfoCollectionEventArgs();
            }

            EventArgs.AddedStorage.Add(item);
            RaiseCollectionChanged();
        }
        protected override void RemoveItem(int index) {
            StorageVolumeInfo info = this[index];
            base.RemoveItem(index);
            if(!IsLockUpdate) {
                EventArgs = new StorageInfoCollectionEventArgs();
            }

            EventArgs.RemovedStorage.Add(info);
            RaiseCollectionChanged();
        }
        protected override void SetItem(int index, StorageVolumeInfo item) {
            StorageVolumeInfo info = this[index];
            base.SetItem(index, item);
            if(!IsLockUpdate) {
                EventArgs = new StorageInfoCollectionEventArgs();
            }

            EventArgs.RemovedStorage.Add(info);
            EventArgs.AddedStorage.Add(item);
            RaiseCollectionChanged();
        }
        int UpdateCount {
            get; set;
        }
        public void BeginUpdate() {
            UpdateCount++;
            EventArgs = new StorageInfoCollectionEventArgs();
        }
        public bool IsLockUpdate => UpdateCount > 0;
        public void EndUpdate() {
            if(UpdateCount > 0) {
                UpdateCount--;
            }

            if(UpdateCount == 0) {
                RaiseCollectionChanged();
            }
        }
        void RaiseCollectionChanged() {
            if(IsLockUpdate) {
                return;
            }

            if(CollectionChanged != null) {
                CollectionChanged(this, EventArgs);
            }
        }
    }
    public class StorageInfoCollectionEventArgs : EventArgs {
        public StorageInfoCollectionEventArgs() {
        AddedStorage = new List<StorageVolumeInfo>();
            RemovedStorage = new List<StorageVolumeInfo>();
        }
        public List<StorageVolumeInfo> AddedStorage {
            get; private set;
        }
        public List<StorageVolumeInfo> RemovedStorage {
            get; private set;
        }
    }
    public delegate void StoreageInfoCollectionEventHandler(object sender, StorageInfoCollectionEventArgs e);

    public delegate void AllowStorageEventHandler(object sender, AllowStorageEventArgs e);
    public class AllowStorageEventArgs : EventArgs {
        public AllowStorageEventArgs(StorageVolumeInfo info) {
        StorageInfo = info;
            Allow = true;
        }
        public StorageVolumeInfo StorageInfo {
            get; private set;
        }
        public bool Allow {
            get; set;
        }
    }
    public class StorageDeviceInfo {
        [XtraSerializableProperty]
        public int DiskNumber {
            get; set;
        }
        [XtraSerializableProperty]
        public string Vendor {
            get; set;
        }
        [XtraSerializableProperty]
        public string ProductId {
            get; set;
        }
        [XtraSerializableProperty]
        public string RevisionId {
            get; set;
        }
        [XtraSerializableProperty]
        public string SerialNumber {
            get; set;
        }
        StoragePartitionInfoCollection partitions;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public StoragePartitionInfoCollection Partitions {
            get {
                if(partitions == null) {
                    partitions = new StoragePartitionInfoCollection();
                }

                return partitions;
            }
        }
        [XtraSerializableProperty]
        public int DeviceType {
            get; set;
        }
        [XtraSerializableProperty]
        public int PartitionNumber {
            get; set;
        }
        StoragePartitionInfo XtraCreatePartitionsItem(XtraItemEventArgs e) => new StoragePartitionInfo();
        void XtraSetIndexPartitionsItem(XtraSetItemIndexEventArgs e) {
            if(e.Index == -1) {
                Partitions.Add((StoragePartitionInfo)e.Item.Value);
            } else {
                Partitions.Insert(e.Index, (StoragePartitionInfo)e.Item.Value);
            }
        }
    }
    public class StotageVolumeInfoCollection : Collection<StorageVolumeInfo> {
    }
    public class StoragePartitionInfoCollection : Collection<StoragePartitionInfo> {
    }
    public class StorageVolumeInfo {
        static StringBuilder builder = new StringBuilder(1024);
        public virtual bool UpdateIsPresent() {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach(DriveInfo drive in drives) {
                if(drive.DriveType != DriveType.Fixed && drive.DriveType != DriveType.Removable) {
                    continue;
                }

                bool res = StorageManager.GetVolumeNameForVolumeMountPoint(drive.Name, builder, (uint)builder.Capacity);
                if(!res) {
                    continue;
                }

                if(builder.ToString() == VolumeId) {
                    ActualName = drive.Name;
                    IsPresent = true;
                    return true;
                }
            }
            IsPresent = false;
            return false;
        }
        public void UpdateState(StorageVolumeInfo present) {
            ActualName = present.ActualName;
            AvailableFreeSpace = present.AvailableFreeSpace;
            VolumeLabel = present.VolumeLabel;
            TotalFreeSpace = present.TotalFreeSpace;
            StartingOffset = present.StartingOffset;
            IsPresent = true;
        }
        [XtraSerializableProperty]
        public string ProjectFolder {
            get; set;
        }
        public bool IsPresent {
            get; set;
        }
        [XtraSerializableProperty]
        public string VolumeLabel {
            get; set;
        }
        [XtraSerializableProperty]
        public string HintName {
            get; set;
        }
        [XtraSerializableProperty]
        public string ActualName {
            get; set;
        }
        [XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public StorageDeviceInfo Device {
            get; set;
        }
        [XtraSerializableProperty]
        public ulong StartingOffset {
            get; set;
        }
        [XtraSerializableProperty]
        public ulong ExtentLength {
            get; set;
        }
        [XtraSerializableProperty]
        public string VolumeId {
            get; set;
        }
        [XtraSerializableProperty]
        public long AvailableFreeSpace {
            get; set;
        }
        [XtraSerializableProperty]
        public long TotalFreeSpace {
            get; set;
        }
        public bool IsFull {
            get; internal set;
        }
    }
    public class StoragePartitionInfo {
        public StoragePartitionInfo() {
        }
        [XtraSerializableProperty]
        public long Length {
            get; set;
        }
        [XtraSerializableProperty]
        public int Number {
            get; set;
        }
    }
    public interface IStorageManagerDialogsProvider {
        StorageManager Manager {
            get; set;
        }
        void DisplayOperationCanceledTask();
        void DisplayBackupNotPresentTask();
        void DisplayStorageNotPresentTask();
        void DisplayNoBackupTask();
        void DisplayNoStorageTask();
        bool DisplayAddNewDevicesToStorageMessage();
        bool DisplayAddStorageDialog();
        bool DisplayAttachNonPresentStorageDevices();
        bool DisplayAddNewDevicesToBackupMessage();
        bool DisplayAddBackupDialog();
        bool DisplayAttachNonPresentBackupDevices();
        DialogResult DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsed();
        void DisplayVolumeIsNotCorrect(string root);
    }
    public enum FileStorageInfoResult {
        Ok = 0, StorageNotPresent = 1, FileNotExistsOnStorage = 2, StorageNotSpecified = 3, MD5NotValid = 4, LastWriteTimeDifferent = 5
    }
    public class FileStorageInfo {
        public DmFile File {
            get; set;
        }
        public FileStorageInfoResult Volume1Result {
            get; set;
        }
        public FileStorageInfoResult Volume2Result {
            get; set;
        }
        public string Volume1MD5 {
            get; set;
        }
        public string Volume2MD5 {
            get; set;
        }
        public DateTime Volume1LastWriteTime {
            get; set;
        }
        public DateTime Volume2LastWriteTime {
            get; set;
        }
    }
    public delegate void FileCopyEventHandler(object sender, FileCopyEventArgs e);
    public class FileCopyEventArgs : EventArgs {
        public StorageVolumeInfo StorageVolume {
            get; set;
        }
        public bool IsBackupVolume {
            get; set;
        }
        public FileInfo FileInfo {
            get; set;
        }
    }
}
