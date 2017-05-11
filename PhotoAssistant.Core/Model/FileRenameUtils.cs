using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core.Model {
    public abstract class FileRenameValueBase {
        public abstract string Name { get; set; }
        protected abstract string GetValueCore(List<DmFile> files, DmFile file);
        public object Value { get; set; }
        public virtual void ApplyParams(FileRenameManager manager) { }
        public string GetValueText(List<DmFile> files, DmFile file) {
            string value = GetValueCore(files, file);
            switch(CaseMode) {
                case Model.CaseMode.Default:
                    return value;
                case Model.CaseMode.LowerCase:
                    return value.ToLower();
                case Model.CaseMode.UpperCase:
                    return value.ToUpper();
                case Model.CaseMode.FirstLetter:
                    return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1).ToLower();
            }
            return value;
        }

        public CaseMode CaseMode { get; set; }
        public abstract FileRenameValueReference CreateReference();
    }
    public class FileRenameValueProperty : FileRenameValueBase {
        string name;

        public FileRenameValueProperty() { }
        public FileRenameValueProperty(string propertyName) : this(propertyName, propertyName) { }
        public FileRenameValueProperty(string propertyName, string name) {
            this.name = name;
            PropertyName = propertyName;
        }
        public override string Name { get { return name; } set { name = value; } }
        public string PropertyName { get; set; }
        protected override string GetValueCore(List<DmFile> files, DmFile file) {
            PropertyInfo info = typeof(DmFile).GetProperty(PropertyName, BindingFlags.Instance | BindingFlags.Public);
            if(info == null)
                return "null";
            object value = info.GetValue(file, null);
            if(value == null)
                return "";
            return value.ToString();
        }
        public override FileRenameValueReference CreateReference() {
            return new FileRenameValueReference(this);
        }
    }

    public class FileRenameValueExtension : FileRenameValueProperty {
        public FileRenameValueExtension() : base("Extension") { }
        public string Extension { get; set; }
        public override void ApplyParams(FileRenameManager manager) {
            base.ApplyParams(manager);
            Extension = manager.Extension;
        }
        protected override string GetValueCore(List<DmFile> files, DmFile file) {
            if(string.IsNullOrEmpty(Extension))
                return base.GetValueCore(files, file);
            return Extension;
        }
    }

    public class FileRenameValueCustom : FileRenameValueBase {
        public FileRenameValueCustom() : base() { }
        public override string Name {
            get { return "CustomText"; }
            set { }
        }
        protected override string GetValueCore(List<DmFile> files, DmFile file) {
            return Value == null ? "" : Value.ToString();
        }
        public override FileRenameValueReference CreateReference() {
            return new FileRenameValueReferenceString(this);
        }
        public string Text { get { return Value == null ? "" : Value.ToString(); } set { Value = value; } }
    }

    public class FileRenameValueIndex : FileRenameValueCustom {
        public FileRenameValueIndex()
            : base() {
            StartIndex = 1;
        }
        public int StartIndex { get; set; }
        protected override string GetValueCore(List<DmFile> files, DmFile file) {
            int value = StartIndex + files.IndexOf(file);
            int count = files.Count.ToString().Length;

            string str = "{0," + count.ToString() + "}";
            return string.Format(str, value);
        }
        public override string Name {
            get { return "Index"; }
            set { }
        }
        public override FileRenameValueReference CreateReference() {
            return new FileRenameValueReferenceIndex(this);
        }
    }

    public class FileRenameValueCount : FileRenameValueCustom {
        public FileRenameValueCount() : base() { }
        protected override string GetValueCore(List<DmFile> files, DmFile file) {
            return files.Count.ToString();
        }
        public override string Name {
            get { return "Count"; }
            set { }
        }
    }

    public static class FileRenameValueHelper {
        public static FileRenameValueBase CreateFileRenameValue(string className) {
            switch(className) {
                case "Property": return new FileRenameValueProperty();
                case "Custom": return new FileRenameValueCustom();
                case "Index": return new FileRenameValueIndex();
                case "Count": return new FileRenameValueCount();
            }
            return null;
        }
    }

    public class FileRenameValueReference {
        public FileRenameValueReference(FileRenameValueBase value) {
            FileRenameValue = value;
        }
        FileRenameValueBase renameValue;
        [Browsable(false)]
        public FileRenameValueBase FileRenameValue {
            get { return renameValue; }
            set {
                if(FileRenameValue == value)
                    return;
                renameValue = value;
            }
        }
        public virtual object ValueCore { get { return null; } }
        [XtraSerializableProperty]
        public string FileRenameValueName { get { return FileRenameValue == null ? "" : FileRenameValue.Name; } set { } }
        [XtraSerializableProperty]
        public CaseMode CaseMode { get; set; }
        public override string ToString() {
            string res = FileRenameValue.Name;
            res += " - Case:" + CaseMode;
            if(ValueCore != null)
                res += " (" + ValueCore.ToString() + ")";
            return res;
        }
        public virtual void Assign(FileRenameValueReference reference) {
            CaseMode = reference.CaseMode;
        }
    }
    public class FileRenameValueReferenceString : FileRenameValueReference {
        public FileRenameValueReferenceString(FileRenameValueBase value) : base(value) { }
        [XtraSerializableProperty]
        public string Text { get; set; }
        public override object ValueCore { get { return Text; } }
        public override void Assign(FileRenameValueReference reference) {
            base.Assign(reference);
            FileRenameValueReferenceString sr = reference as FileRenameValueReferenceString;
            if(sr != null)
                Text = sr.Text;
        }
    }
    public class FileRenameValueReferenceIndex : FileRenameValueReference {
        public FileRenameValueReferenceIndex(FileRenameValueBase value) : base(value) { }
        [XtraSerializableProperty]
        public int Index { get; set; }
        public override object ValueCore {
            get { return Index; }
        }
        public override void Assign(FileRenameValueReference reference) {
            base.Assign(reference);
            FileRenameValueReferenceIndex sr = reference as FileRenameValueReferenceIndex;
            if(sr != null)
                Index = sr.Index;
        }
    }

    public class FileRenameValueCollection : Collection<FileRenameValueBase> { }
    public class FileRenameValueReferenceCollection : Collection<FileRenameValueReference> { }

    public enum CaseMode { Default, UpperCase, LowerCase, FirstLetter }

    public class FileRenameManager {
        static FileRenameManager defaultManager;
        public static FileRenameManager Default {
            get {
                if(defaultManager == null)
                    defaultManager = new FileRenameManager();
                return defaultManager;
            }
        }

        static DmFile fileExample;
        public static DmFile FileExample {
            get {
                if(fileExample == null)
                    fileExample = CreateFileExample();
                return fileExample;
            }
        }
        static List<DmFile> files;
        public static List<DmFile> Files {
            get {
                if(files == null)
                    files = CreateFiles();
                return files;
            }
        }

        private static List<DmFile> CreateFiles() {
            List<DmFile> res = new List<DmFile>();
            res.Add(new DmFile());
            res.Add(new DmFile());
            res.Add(FileExample);
            res.Add(new DmFile());
            return res;
        }

        private static DmFile CreateFileExample() {
            DmFile file = new DmFile();
            file.Aperture = 1.5f;
            file.AspectRatio = 1.33f;
            file.Author = "Autor";
            file.CameraModel = "D5000";
            file.CameraProducer = "NIKON";
            file.Caption = "MyPhotoCaption";
            file.City = "Madrid";
            file.Client = "MyClient";
            file.ColorDepth = 32;
            file.Comment = "My comment";
            file.Country = "Spain";
            file.CreationDate = DateTime.Now;
            file.Description = "My description";
            file.Dpi = 96;
            file.Event = "My Birthday";
            file.FileName = "MyPhoto.jpg";
            file.FileSize = 3000;
            file.FlashUsed = 1.0f;
            file.Flip = 90f;
            file.FocalLength = 0.25f;
            file.Folder = "c:\\Photos";
            file.Height = 4000;
            file.ImportDate = DateTime.Now;
            file.ISO = 800f;
            file.Latitude = 0.0f;
            file.Longitude = 0.0f;
            file.Project = "My Project";
            file.Rating = 4;
            file.Scene = "My Scene";
            file.ShutterSpeed = 22f;
            file.State = "Region of Madrid";
            file.Width = 5000;
            return file;
        }

        public FileRenameManager() {
            InitializeCollection();
        }

        private void InitializeCollection() {
            FileRenameValues = new FileRenameValueCollection();
            FileRenameValues.Add(new FileRenameValueProperty("FileNameWithoutExtension", "FileName"));
            FileRenameValues.Add(new FileRenameValueExtension());
            FileRenameValues.Add(new FileRenameValueProperty("Width"));
            FileRenameValues.Add(new FileRenameValueProperty("Height"));
            FileRenameValues.Add(new FileRenameValueProperty("ISO"));
            FileRenameValues.Add(new FileRenameValueProperty("CameraProducer"));
            FileRenameValues.Add(new FileRenameValueProperty("CameraModel"));
            FileRenameValues.Add(new FileRenameValueProperty("Autor"));
            FileRenameValues.Add(new FileRenameValueProperty("Dpi"));
            FileRenameValues.Add(new FileRenameValueProperty("CreationDate"));
            FileRenameValues.Add(new FileRenameValueProperty("ImportDate"));
            FileRenameValues.Add(new FileRenameValueProperty("Caption"));
            FileRenameValues.Add(new FileRenameValueProperty("Description"));
            FileRenameValues.Add(new FileRenameValueProperty("Country"));
            FileRenameValues.Add(new FileRenameValueProperty("State"));
            FileRenameValues.Add(new FileRenameValueProperty("City"));
            FileRenameValues.Add(new FileRenameValueProperty("Location"));
            FileRenameValues.Add(new FileRenameValueProperty("Event"));
            FileRenameValues.Add(new FileRenameValueProperty("Project"));
            FileRenameValues.Add(new FileRenameValueProperty("Client"));
            FileRenameValues.Add(new FileRenameValueProperty("Scene"));
            FileRenameValues.Add(new FileRenameValueProperty("Comment"));
            FileRenameValues.Add(new FileRenameValueIndex());
            FileRenameValues.Add(new FileRenameValueCount());
            FileRenameValues.Add(new FileRenameValueCustom());
        }

        public FileRenameValueCollection FileRenameValues {
            get;
            set;
        }

        public FileRenameValueReferenceCollection TemplateValues { get; set; }
        public List<FileRenameValueError> Errors { get; set; }

        string template;
        public string Template {
            get { return template; }
            set {
                if(Template == value)
                    return;
                template = value;
                OnTemplateChanged();
            }
        }

        protected virtual void OnTemplateChanged() {
            Errors = new List<FileRenameValueError>();
            TemplateValues = ParseString(Template, Errors);
        }

        public FileRenameValueReferenceCollection ParseString(string template, List<FileRenameValueError> errors) {
            FileRenameValueReferenceCollection res = new FileRenameValueReferenceCollection();
            if(template == null)
                return res;

            StringBuilder builder = new StringBuilder();
            FileRenameValueBase value = null;
            errors.Clear();
            for(int pos = 0; pos < template.Length; ) {
                if(IsKeywordBegin(template[pos])) {
                    if(builder.Length > 0) {
                        value = GetFileRenameValue("CustomText");
                        if(value == null)
                            throw new Exception("Error: FileRenameValue's list not initialized");
                        FileRenameValueReferenceString fref = (FileRenameValueReferenceString)value.CreateReference();
                        fref.Text = builder.ToString();
                        res.Add(fref);
                        builder.Clear();
                    }
                    pos++;
                    int index = pos;
                    string[] keyword = GetKeyword(template, ref pos);
                    if(keyword == null || keyword.Length == 0) {
                        errors.Add(new FileRenameValueError() { Index = index, Name = "" });
                        pos++;
                        continue;
                    }
                    value = GetFileRenameValue(keyword[0]);
                    if(value != null) {
                        FileRenameValueReference fref = value.CreateReference();
                        if(keyword.Length > 1)
                            fref.CaseMode = GetCaseMode(keyword[1]);
                        res.Add(fref);
                    } else
                        errors.Add(new FileRenameValueError() { Index = index, Name = keyword[0] });
                    pos++;
                } else {
                    builder.Append(template[pos]);
                    pos++;
                }
            }
            if(builder.Length > 0) {
                value = GetFileRenameValue("CustomText");
                if(value == null)
                    throw new Exception("Error: FileRenameValue's list not initialized");
                FileRenameValueReferenceString fref = (FileRenameValueReferenceString)value.CreateReference();
                fref.Text = builder.ToString();
                res.Add(fref);
            }
            return res;
        }

        private CaseMode GetCaseMode(string p) {
            CaseMode res = CaseMode.Default;
            if(!Enum.TryParse(p, out res))
                Errors.Add(new FileRenameValueError() { Name = p });
            return res;
        }

        public string Extension { get; set; }

        public string GetFileName(List<DmFile> files, DmFile file) {
            if(TemplateValues == null)
                return string.Empty;
            StringBuilder builder = new StringBuilder();
            foreach(FileRenameValueReference valueRef in TemplateValues) {
                valueRef.FileRenameValue.ApplyParams(this);
                valueRef.FileRenameValue.Value = valueRef.ValueCore;
                valueRef.FileRenameValue.CaseMode = valueRef.CaseMode;
                builder.Append(valueRef.FileRenameValue.GetValueText(files, file));
            }
            return builder.ToString();
        }

        public string GetExample() {
            return GetFileName(Files, FileExample);
        }

        public FileRenameValueBase GetFileRenameValue(string keyword) {
            return FileRenameValues.FirstOrDefault((fv) => fv.Name == keyword);
        }

        private string[] GetKeyword(string template, ref int i) {
            int end = template.IndexOf('}', i);
            if(end == -1) end = template.Length;
            string keyword = template.Substring(i, end - i).Trim();
            i = end;
            string[] keywords = keyword.Split(':');
            return keywords;
        }

        private bool IsKeywordBegin(char p) {
            return p == KeywordCharOpen;
        }

        private bool IsKeywordEnd(char p) {
            return p == KeywordCharClose;
        }

        public char KeywordCharOpen { get { return '{'; } }
        public char KeywordCharClose { get { return '}'; } }

        public void AssignValues(FileRenameValueReferenceCollection values) {
            for(int i = 0; i < TemplateValues.Count; i++) {
                TemplateValues[i].Assign(values[i]);
            }
        }
    }

    public class FileRenameValueError {
        public int Index { get; set; }
        public string Name { get; set; }
        public override string ToString() {
            return "Unrecognized keyword '" + Name + "' at " + Index + " position";
        }
    }

    public class FileRenameTemplateInfo {
        [XtraSerializableProperty]
        public string Template { get; set; }
        [XtraSerializableProperty]
        public string Name { get; set; }

        public override string ToString() {
            return Name;
        }
    }
}
