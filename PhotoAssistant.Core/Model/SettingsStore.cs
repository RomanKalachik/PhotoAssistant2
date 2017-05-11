using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using DevExpress.Utils;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PhotoAssistant.Core.Model {
    public enum GalleryViewStyle { Thumbnails = 0, Detail = 1 }
    public enum PreviewLocation { Default, Left, Top, Right }
    public enum QuickGalleryMode {
        [Description("Selected Items")]
        SelectedItems,
        [Description("Marked Items")]
        MarkedItems,
        [Description("Labeled Items")]
        LabeledItems
    }

    public class ProjectInfo {
        public ProjectInfo() {
            Guid = Guid.NewGuid();
        }
        Guid guid;
        public Guid Guid {
            get { return guid; }
            set {
                if(Guid == value)
                    return;
                guid = value;
                if(!inIdSetter)
                    Id = Guid.ToString();
            }
        }

        bool inIdSetter = false;
        [XtraSerializableProperty]
        public string Id {
            get { return Guid.ToString(); }
            set {
                inIdSetter = true;
                try {
                    Guid = new Guid(value);
                } finally {
                    inIdSetter = false;
                }
            }
        }

        [XtraSerializableProperty]
        public string ThumbFileName { get; set; }
        public Image ThumbImage { get; set; }
        [XtraSerializableProperty]
        public string ThumbFileName2 { get; set; }
        public Image ThumbImage2 { get; set; }
        [XtraSerializableProperty]
        public string ThumbFileName3 { get; set; }
        public Image ThumbImage3 { get; set; }
        [XtraSerializableProperty]
        public string ThumbFileName4 { get; set; }
        public Image ThumbImage4 { get; set; }
        [XtraSerializableProperty]
        public string FileName { get; set; }
        [XtraSerializableProperty]
        public string Name { get; set; }
        [XtraSerializableProperty]
        public int OpenCount { get; set; }
        [XtraSerializableProperty]
        public string Description { get; set; }
        [XtraSerializableProperty]
        public int FileCount { get; set; }
    }

    public class ApplicationInfo {
        public ApplicationInfo() {
            Id = Guid.NewGuid();
        }
        [XtraSerializableProperty]
        public string IdString {
            get { return Id.ToString(); }
            set {
                Guid guid = Guid.NewGuid();
                Guid.TryParse(value, out guid);
                Id = guid;
            }
        }
        public Guid Id { get; set; }
        [XtraSerializableProperty]
        public string Name { get; set; }
        [XtraSerializableProperty]
        public string Path { get; set; }
        [XtraSerializableProperty]
        public string CommandLine { get; set; }
        public override string ToString() {
            return Name;
        }
    }

    public class ApplicationInfoCollection : Collection<ApplicationInfo> { }

    public class PathInfo {
        [XtraSerializableProperty]
        public string Path { get; set; }
        public override string ToString() {
            return Path;
        }
        public override bool Equals(object obj) {
            PathInfo info = obj as PathInfo;
            if(info != null)
                return info.Path == Path;
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }

    public class ProjectInfoCollection : Collection<ProjectInfo> { }
    public class PathInfoCollection : Collection<PathInfo> {
        public void Remove(string path) {
            foreach(PathInfo info in this) {
                if(info.Path == path) {
                    Remove(info);
                    return;
                }
            }
        }
        public void Insert(int index, string path) { Insert(index, new PathInfo() { Path = path }); }
    }
    public class FileRenameTemplateCollection : Collection<FileRenameTemplateInfo> { }

    public class SettingsStore : IXtraSerializable {
        static SettingsStore defaultSettings;
        public static SettingsStore Default {
            get {
                if(defaultSettings == null) {
                    defaultSettings = new SettingsStore();
                    defaultSettings.StorageManager = StorageManager.Default;
                    defaultSettings.RestoreFromXml();
                }
                return defaultSettings;
            }
            set { defaultSettings = value; }
        }
        public static string SettingsFileName { get { return "settings.xml"; } }
        public static string ApplicationName { get { return "Titsian"; } }
        static string SettingsSectionName { get { return "Settings"; } }
        static string StorageManagerSectionName {
            get { return "StorageManager"; }
        }

        public SettingsStore() {
            SelectedThemeName = "Office 2013 Light Gray";
            MaskColor = Color.Empty;
            MaskColor2 = Color.Empty;
            ImageAnimationType = ImageContentAnimationType.Push;
            EditingToolbarColorLabelsVisible = true;
            EditingToolbarGridVisible = true;
            EditingToolbarMarkVisible = true;
            EditingToolbarPreviewModeVisible = true;
            EditingToolbarRatingVisible = true;
            EditingToolbarZoomVisible = true;
            InitializeFileRenameTemplates();
        }

        private void InitializeFileRenameTemplates() {
            FileRenameTemplates.Add(new FileRenameTemplateInfo() { Name = "Filename", Template = "{FileName}.{Extension}" });
        }

        [XtraSerializableProperty]
        public bool EditingToolbarPreviewModeVisible { get; set; }
        [XtraSerializableProperty]
        public bool EditingToolbarGridVisible { get; set; }
        [XtraSerializableProperty]
        public bool EditingToolbarZoomVisible { get; set; }
        [XtraSerializableProperty]
        public bool EditingToolbarMarkVisible { get; set; }
        [XtraSerializableProperty]
        public bool EditingToolbarRatingVisible { get; set; }
        [XtraSerializableProperty]
        public bool EditingToolbarColorLabelsVisible { get; set; }

        ProjectInfoCollection projects;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public virtual ProjectInfoCollection Projects {
            get {
                if(projects == null)
                    projects = new ProjectInfoCollection();
                return projects;
            }
        }

        FileRenameTemplateCollection fileRenameTemplates;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public virtual FileRenameTemplateCollection FileRenameTemplates {
            get {
                if(fileRenameTemplates == null)
                    fileRenameTemplates = new FileRenameTemplateCollection();
                return fileRenameTemplates;
            }
        }

        public FileRenameTemplateInfo GetFileRenameTemlate(string name) {
            return fileRenameTemplates.FirstOrDefault((t) => t.Name == name);
        }

        public FileRenameTemplateInfo GetFileRenameTemlate(string name, string mask) {
            FileRenameTemplateInfo info = GetFileRenameTemlate(name);
            if(info == null) {
                info = new FileRenameTemplateInfo();
                info.Name = name;
                info.Template = mask;
                FileRenameTemplates.Add(info);
            }
            return info;
        }

        FileRenameTemplateInfo XtraCreateFileRenameTemplatesItem(XtraItemEventArgs e) {
            return new FileRenameTemplateInfo();
        }

        void XtraSetIndexFileRenameTemplatesItem(XtraSetItemIndexEventArgs e) {
            if(e.Index == -1)
                FileRenameTemplates.Add((FileRenameTemplateInfo)e.Item.Value);
            else
                FileRenameTemplates.Insert(e.Index, (FileRenameTemplateInfo)e.Item.Value);
        }

        ProjectInfo XtraCreateProjectsItem(XtraItemEventArgs e) {
            return new ProjectInfo();
        }

        void XtraSetIndexProjectsItem(XtraSetItemIndexEventArgs e) {
            if(e.Index == -1)
                Projects.Add((ProjectInfo)e.Item.Value);
            else
                Projects.Insert(e.Index, (ProjectInfo)e.Item.Value);
        }

        ApplicationInfoCollection exportApplications;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public virtual ApplicationInfoCollection ExportApplications {
            get {
                if(exportApplications == null)
                    exportApplications = new ApplicationInfoCollection();
                return exportApplications;
            }
        }

        ApplicationInfo XtraCreateExportApplicationsItem(XtraItemEventArgs e) {
            return new ApplicationInfo();
        }

        void XtraSetIndexExportApplicationsItem(XtraSetItemIndexEventArgs e) {
            int index = e.Index > -1 ? e.Index : 0;
            ExportApplications.Insert(index, (ApplicationInfo)e.Item.Value);
        }

        ExportInfoCollection exportPresets;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public virtual ExportInfoCollection ExportPresets {
            get {
                if(exportPresets == null)
                    exportPresets = new ExportInfoCollection();
                return exportPresets;
            }
        }

        ExportInfo XtraCreateExportPresetsItem(XtraItemEventArgs e) {
            return new ExportInfo();
        }

        void XtraSetIndexExportPresetsItem(XtraSetItemIndexEventArgs e) {
            int index = e.Index > -1 ? e.Index : 0;
            ExportPresets.Insert(index, (ExportInfo)e.Item.Value);
        }

        [XtraSerializableProperty]
        public bool NoStorageTaskShowed { get; set; }

        [XtraSerializableProperty]
        public bool NoBackupTaskShowed { get; set; }

        PathInfoCollection foldersToExport;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public virtual PathInfoCollection FoldersToExport {
            get {
                if(foldersToExport == null)
                    foldersToExport = new PathInfoCollection();
                return foldersToExport;
            }
        }

        PathInfo XtraCreateFoldersToExportItem(XtraItemEventArgs e) {
            return new PathInfo();
        }

        void XtraSetIndexFoldersToExportItem(XtraSetItemIndexEventArgs e) {
            if(e.Index == -1)
                FoldersToExport.Add((PathInfo)e.Item.Value);
            else
                FoldersToExport.Insert(e.Index, (PathInfo)e.Item.Value);
        }

        public StorageManager StorageManager {
            get;
            set;
        }

        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this), new XtraObjectInfo(StorageManagerSectionName, StorageManager) }, stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(
                    new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this), new XtraObjectInfo(StorageManagerSectionName, StorageManager) }, path.ToString(), this.GetType().Name);
        }
        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this), new XtraObjectInfo(StorageManagerSectionName, StorageManager) },
                    stream, this.GetType().Name);
            else
                serializer.DeserializeObjects(new XtraObjectInfo[] { new XtraObjectInfo(SettingsSectionName, this), new XtraObjectInfo(StorageManagerSectionName, StorageManager) },
                    path.ToString(), this.GetType().Name);
            ImageAnimationType = GetRandomAnimationType();
            Debug.WriteLine("SettingsStore.RestoreLayout - success!");
        }

        private ImageContentAnimationType GetRandomAnimationType() {
            ImageContentAnimationType[] img = new ImageContentAnimationType[] { ImageContentAnimationType.Expand, ImageContentAnimationType.Push, ImageContentAnimationType.SegmentedFade, ImageContentAnimationType.Slide };
            Random rnd = new Random();
            int index = rnd.Next(8) % 4;
            return img[index] == ImageAnimationType ? img[(index + 1) % 4] : img[index];
        }

        public void RestoreFromXml() {
            if(!File.Exists(SettingsFileName))
                return;
            RestoreLayoutCore(new XmlXtraSerializer(), SettingsFileName);
        }

        public void SaveToXml() {
            SaveLayoutCore(new XmlXtraSerializer(), SettingsFileName);
        }

        [XtraSerializableProperty]
        public string SelectedThemeName {
            get;
            set;
        }

        [XtraSerializableProperty]
        public bool UseInches { get; set; }

        [XtraSerializableProperty]
        public ImageContentAnimationType ImageAnimationType { get; set; }

        [XtraSerializableProperty]
        public Color MaskColor { get; set; }

        [XtraSerializableProperty]
        public Color MaskColor2 { get; set; }

        string currentDataSource;
        public string CurrentDataSource {
            get {
                if(!string.IsNullOrEmpty(currentDataSource))
                    return currentDataSource;
                if(ActiveProject == null) return null;
                return ActiveProject.FileName;
            }
            set { currentDataSource = value; }
        }

        [XtraSerializableProperty]
        public string ActiveProjectId { get; set; }

        public Version CurrentVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public string DefaultDataSource {
            get {
                return "Default.PhotoAssistant";
            }
        }
        public string ThumbsFolderName {
            get { return ActiveProject.Name + "_Thumbs"; }
        }
        public string PreviewFolderName {
            get { return ActiveProject.Name + "_Preview"; }
        }

        ProjectInfo activeProject;
        public ProjectInfo ActiveProject {
            get { return activeProject; }
            set {
                if(ActiveProject == value)
                    return;
                activeProject = value;
                OnActiveProjectChanged();
            }
        }

        private void OnActiveProjectChanged() {
            if(ActiveProject != null)
                ActiveProjectId = ActiveProject.Id;
        }

        #region IXtraSerializable
        void IXtraSerializable.OnEndDeserializing(string restoredVersion) {
            InitializeProjectsImage();
            InitializeActiveProject();
        }

        public bool SaveProjectParameters { get; set; }

        private void InitializeActiveProject() {
            if(Projects.Count == 0) {
                CreateDefaultProject();
                return;
            }
            ActiveProject = Projects.FirstOrDefault((p) => p.Id == ActiveProjectId);
            if(ActiveProject == null)
                ActiveProject = Projects.First();
        }

        private void CreateDefaultProject() {
            ProjectInfo info = new ProjectInfo();
            info.Name = "Default";
            info.Description = "This is default project.";
            info.FileName = DefaultDataSourcePath;
            Projects.Insert(0, info);
            ActiveProject = info;
            SaveProjectParameters = true;
        }

        private void InitializeProjectsImage() {
            foreach(ProjectInfo info in Projects) {
                if(File.Exists(info.ThumbFileName))
                    info.ThumbImage = Image.FromFile(info.ThumbFileName);
            }
        }

        void IXtraSerializable.OnEndSerializing() {

        }

        void IXtraSerializable.OnStartDeserializing(DevExpress.Utils.LayoutAllowEventArgs e) {

        }

        void IXtraSerializable.OnStartSerializing() {

        }
        #endregion

        string thumbPath;
        public string ThumbPath {
            get {
                if(string.IsNullOrEmpty(thumbPath)) {
                    thumbPath = System.IO.Path.GetDirectoryName(ActiveProject.FileName) + "\\" + ThumbsFolderName;
                    if(!Directory.Exists(thumbPath))
                        Directory.CreateDirectory(thumbPath);
                }
                return thumbPath;
            }
            set { thumbPath = value; }
        }
        string previewPath;
        public string PreviewPath {
            get {
                if(string.IsNullOrEmpty(previewPath)) {
                    previewPath = System.IO.Path.GetDirectoryName(ActiveProject.FileName) + "\\" + PreviewFolderName;
                    if(!Directory.Exists(previewPath))
                        Directory.CreateDirectory(previewPath);
                }
                return previewPath;
            }
            set {
                previewPath = value;
            }
        }

        GalleryViewStyle viewStyle = GalleryViewStyle.Thumbnails;
        [XtraSerializableProperty]
        public GalleryViewStyle ViewStyle {
            get { return viewStyle; }
            set { viewStyle = value; }
        }

        QuickGalleryMode quickGalleryMode = QuickGalleryMode.SelectedItems;
        [XtraSerializableProperty]
        public QuickGalleryMode QuickGalleryMode {
            get { return quickGalleryMode; }
            set { quickGalleryMode = value; }
        }

        string quickGalleryColorLabel = DmColorLabel.NoneString;
        [XtraSerializableProperty]
        public string QuickGalleryColorLabel {
            get { return quickGalleryColorLabel; }
            set { quickGalleryColorLabel = value; }
        }

        PreviewLocation previewLocation = PreviewLocation.Default;
        [XtraSerializableProperty]
        public PreviewLocation PreviewLocation {
            get { return previewLocation; }
            set { previewLocation = value; }
        }

        SplitPicturePreviewType previewMode = SplitPicturePreviewType.Single; 
        [XtraSerializableProperty]
        public SplitPicturePreviewType PreviewMode {
            get { return previewMode; }
            set { previewMode = value; }
        }

        int zoom = 128;
        [XtraSerializableProperty]
        public int Zoom {
            get { return zoom; }
            set { zoom = value; }
        }

        Size previewSize = new Size(1024, 1024);
        [XtraSerializableProperty]
        public Size PreviewSize {
            get { return previewSize; }
            set { previewSize = value; }
        }

        Size thumbSize = new Size(392, 392);
        [XtraSerializableProperty]
        public Size ThumbSize {
            get { return thumbSize; }
            set { thumbSize = value; }
        }
        bool showPreview = false;
        [XtraSerializableProperty]
        public bool ShowPreview {
            get { return showPreview; }
            set { showPreview = value; }
        }
        int previewPanelSize = 200;
        [XtraSerializableProperty]
        public int PreviewPanelSize {
            get { return previewPanelSize; }
            set { previewPanelSize = value; }
        }

        public string DefaultDataSourcePath {
            get {
                return System.IO.Path.GetDirectoryName("." + "\\" + "Projects\\" + DefaultDataSource);
            }
        }

        public Size ColorLabelImageSize { get { return new Size(24, 16); } }

        public ProjectInfo GetProject(string fileName) {
            return Projects.FirstOrDefault((p) => p.FileName == fileName);
        }

        public PathInfo GetExportFolderInfo(string path) {
            PathInfo info = FoldersToExport.FirstOrDefault((p) => p.Path == path);
            if(info == null) {
                info = new PathInfo() { Path = path };
                FoldersToExport.Add(info);
            }
            return info;
        }

        public ApplicationInfo GetExportApplication(string idString) {
            return ExportApplications.FirstOrDefault(app => app.IdString == idString);
        }
    }

    public enum SplitPicturePreviewType {
        Single,
        Two,
        Three,
        Editing
    }
    public enum ImageContentAnimationType { Expand, SegmentedFade, Slide, Push, None }

}
