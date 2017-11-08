using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
namespace PhotoAssistant.Core.Model {
    public class DmFile : ISupportId {
        public DmFile() {
        Id = Guid.NewGuid();
            Caption = string.Empty;
            Description = string.Empty;
            Country = string.Empty;
            State = string.Empty;
            City = string.Empty;
            Location = string.Empty;
            Event = string.Empty;
            Project = string.Empty;
            Client = string.Empty;
            Scene = string.Empty;
            Comment = string.Empty;
            OfficeDocumentSubject = string.Empty;
            OfficeDocumentManager = string.Empty;
            GroupId = Guid.Empty;
            AspectRatio = 1.0f;
            Latitude = InvalidGeoLocation;
            Longitude = InvalidGeoLocation;
        }
        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id {
            get; set;
        }
        public string MD5Hash {
            get; set;
        }
        [Index(IsClustered = true, IsUnique = true)]
        public long Index {
            get; set;
        }
        public virtual MediaFormat MediaFormat {
            get; set;
        }
        [NotMapped]
        public string Path {
            get {
                if(Volume1 == null) {
                    return string.Empty;
                }

                return Volume1.Name + Volume1.ProjectFolder + "\\" + RelativePath + "\\" + FileName;
            }
        }
        [NotMapped]
        public string FullPreviewPath {
            get {
                if(Volume1 == null) {
                    return string.Empty;
                }

                return Volume1.Name + Volume1.ProjectFolder + "\\" + RelativePath + "\\" + FullPreviewFileName;
            }
        }
        [NotMapped]
        public BackgroundWorker Worker {
            get; set;
        }
        [NotMapped]
        public string BackupPath {
            get {
                if(Volume2 == null) {
                    return string.Empty;
                }

                return Volume2 == null ? string.Empty : Volume2.Name + Volume2.ProjectFolder + "\\" + RelativePath + FileName;
            }
        }
        [NotMapped]
        public bool HasBackupPath => Volume2 != null && string.IsNullOrEmpty(Volume2.VolumeId);
        [NotMapped]
        public string ImportPath {
            get; set;
        }
        public string RelativePath {
            get; set;
        }
        public string ThumbFileName {
            get; set;
        }
        public string PreviewFileName {
            get; set;
        }
        public string FullPreviewFileName {
            get; set;
        }
        string fileName;
        public string FileName {
            get => fileName;
            set {
                if(FileName == value) {
                    return;
                }

                fileName = value;
                fileNameWithoutExtension = null;
                extension = null;
            }
        }
        string fileNameWithoutExtension;
        [NotMapped]
        public string FileNameWithoutExtension {
            get {
                if(fileNameWithoutExtension == null) {
                    fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(FileName);
                }

                return fileNameWithoutExtension;
            }
        }
        string extension;
        [NotMapped]
        public string Extension {
            get {
                if(extension == null) {
                    extension = System.IO.Path.GetExtension(FileName);
                    if(extension.StartsWith(".")) {
                        extension = extension.Remove(0, 1);
                    }
                }
                return extension;
            }
        }
        public string Folder {
            get; set;
        }
        public long FileSize {
            get; set;
        }
        public int Width {
            get; set;
        }
        public int Height {
            get; set;
        }
        [NotMapped]
        public int WidthPixels => (int)(Width * Dpi / 96.0f);
        [NotMapped]
        public int HeightPixels => (int)(Height * Dpi / 96.0f);
        public float ISO {
            get; set;
        }
        public float ShutterSpeed {
            get; set;
        }
        public float Aperture {
            get; set;
        }
        public float Flip {
            get; set;
        }
        public float FlashUsed {
            get; set;
        }
        public float FocalLength {
            get; set;
        }
        public string CameraProducer {
            get; set;
        }
        public string CameraModel {
            get; set;
        }
        public string Author {
            get; set;
        }
        public float AspectRatio {
            get; set;
        }
        public int ColorDepth {
            get; set;
        }
        public bool Marked {
            get; set;
        }
        public bool Rejected {
            get; set;
        }
        public bool IsGroupOwner {
            get; set;
        }
        public bool IsExpanded {
            get; set;
        }
        public Guid GroupId {
            get; set;
        }
        DmFile groupOwner;
        [NotMapped]
        public DmFile GroupOwner {
            get => groupOwner;
            set {
                if(GroupOwner == value) {
                    return;
                }

                DmFile prev = GroupOwner;
                groupOwner = value;
                OnGroupOwnerChanged(prev, GroupOwner);
            }
        }
        [NotMapped]
        public object ImageSource {
            get; set;
        }
        [NotMapped]
        public bool LoadingImageSource {
            get; set;
        }
        protected virtual void OnGroupOwnerChanged(DmFile prevOwner, DmFile newOwner) {
            if(prevOwner != null) {
                prevOwner.GroupedFiles.Remove(this);
                if(prevOwner.GroupedFiles.Count == 0) {
                    prevOwner.IsGroupOwner = false;
                }
            }
            IsGrouped = false;
            GroupId = Guid.Empty;
            if(newOwner != null) {
                newOwner.GroupedFiles.Add(this);
                newOwner.IsGroupOwner = true;
                GroupId = newOwner.Id;
                IsGrouped = true;
            }
        }
        [NotMapped]
        public bool IsGrouped {
            get; internal set;
        }
        List<DmFile> groupedFiles;
        [NotMapped]
        public List<DmFile> GroupedFiles {
            get {
                if(groupedFiles == null) {
                    groupedFiles = new List<DmFile>();
                }

                return groupedFiles;
            }
        }
        [NotMapped]
        public bool AllowAdd {
            get; set;
        }
        Image image;
        [NotMapped]
        public Image Image {
            get => image;
            internal set {
                image = value;
                UpdateImageParams();
            }
        }
        public int Dpi {
            get; set;
        }
        public int Rating {
            get; set;
        }
        public virtual DmColorLabel ColorLabel {
            get; set;
        }
        public DateTime CreationDate {
            get; set;
        }
        public DateTime ImportDate {
            get; set;
        }
        public DateTime Volume1LastWriteTime {
            get; set;
        }
        public DateTime Volume2LastWriteTime {
            get; set;
        }
        public string Caption {
            get; set;
        }
        public string Description {
            get; set;
        }
        public string Country {
            get; set;
        }
        public string State {
            get; set;
        }
        public string City {
            get; set;
        }
        public string Location {
            get; set;
        }
        public string Event {
            get; set;
        }
        public float Latitude {
            get; set;
        }
        public float Longitude {
            get; set;
        }
        [NotMapped]
        public bool HasPeoplesTag => PeoplesCount > 0;
        [NotMapped]
        public bool HasKeywordsTag => KeywordsCount > 0;
        [NotMapped]
        public bool HasCategoriesTag => CategoriesCount > 0;
        [NotMapped]
        public bool HasGenresTag => GenresCount > 0;
        [NotMapped]
        public bool HasAutorsTag => AutorsCount > 0;
        [NotMapped]
        public bool HasCollectionsTag => CollectionsCount > 0;
        public int PeoplesCount {
            get; set;
        }
        public int KeywordsCount {
            get; set;
        }
        public int CategoriesCount {
            get; set;
        }
        public int GenresCount {
            get; set;
        }
        public int AutorsCount {
            get; set;
        }
        public int CollectionsCount {
            get; set;
        }
        public virtual ICollection<DmPeople> Peoples {
            get; set;
        }
        public virtual ICollection<DmKeyword> Keywords {
            get; set;
        }
        public virtual ICollection<DmCategory> Categories {
            get; set;
        }
        public virtual ICollection<DmGenre> Genres {
            get; set;
        }
        public virtual ICollection<DmAutor> Autors {
            get; set;
        }
        public virtual ICollection<DmCollection> Collections {
            get; set;
        }
        public virtual DmStorageVolume Volume1 {
            get; set;
        }
        public virtual DmStorageVolume Volume2 {
            get; set;
        }
        public string Project {
            get; set;
        }
        public string Client {
            get; set;
        }
        public string Scene {
            get; set;
        }
        public string Comment {
            get; set;
        }
        public string OfficeDocumentSubject {
            get; set;
        }
        public string OfficeDocumentManager {
            get; set;
        }
        public long ImportIndex {
            get; set;
        }
        void UpdateImageParams() {
            Width = image.Width;
            Height = image.Height;
        }
        [NotMapped]
        public Image ThumbImage {
            get; set;
        }
        [NotMapped]
        public Image PreviewImage {
            get; set;
        }
        [NotMapped]
        public Image IconImage {
            get; set;
        }
        string displayInfo;
        public string DisplayInfo {
            get {
                if(string.IsNullOrEmpty(displayInfo)) {
                    displayInfo = FileSizeHelper.Size2String(FileSize) + ", " + Width + "x" + Height;
                }

                return displayInfo;
            }
        }
        public string ImageDimension => Width + "x" + Height + "x" + ColorDepth;
        public DmFile Clone() {
            DmFile res = new DmFile();
            res.AllowAdd = AllowAdd;
            res.Aperture = Aperture;
            res.Author = Author;
            res.CameraModel = CameraModel;
            res.CameraProducer = CameraProducer;
            res.Caption = Caption;
            res.City = City;
            res.Client = Client;
            res.MediaFormat = MediaFormat;
            res.ColorDepth = ColorDepth;
            res.ColorLabel = ColorLabel;
            res.Comment = Comment;
            res.Country = Country;
            res.CreationDate = CreationDate;
            res.Description = Description;
            res.Dpi = Dpi;
            res.Event = Event;
            res.FileName = FileName;
            res.FileSize = FileSize;
            res.FlashUsed = FlashUsed;
            res.Flip = Flip;
            res.FocalLength = FocalLength;
            res.Folder = Folder;
            res.GroupId = GroupId;
            res.Height = Height;
            res.ImportDate = ImportDate;
            res.ImportIndex = ImportIndex;
            res.IsExpanded = IsExpanded;
            res.IsGrouped = IsGrouped;
            res.IsGroupOwner = IsGroupOwner;
            res.ISO = ISO;
            res.Location = Location;
            res.Marked = Marked;
            res.Rejected = Rejected;
            res.OfficeDocumentManager = OfficeDocumentManager;
            res.OfficeDocumentSubject = OfficeDocumentSubject;
            res.RelativePath = RelativePath;
            res.Volume1 = Volume1;
            res.Volume2 = Volume2;
            res.Project = Project;
            res.Rating = Rating;
            res.Scene = Scene;
            res.ShutterSpeed = ShutterSpeed;
            res.State = State;
            res.ThumbFileName = ThumbFileName;
            res.Width = Width;
            res.AutorsCount = AutorsCount;
            res.CategoriesCount = CategoriesCount;
            res.CollectionsCount = CollectionsCount;
            res.GenresCount = GenresCount;
            res.KeywordsCount = KeywordsCount;
            res.PeoplesCount = PeoplesCount;
            if(Autors != null) {
                res.Autors = Autors.ToList();
            }

            if(Keywords != null) {
                res.Keywords = Keywords.ToList();
            }

            if(Collections != null) {
                res.Collections = Collections.ToList();
            }

            if(Categories != null) {
                res.Categories = Categories.ToList();
            }

            if(Genres != null) {
                res.Genres = Genres.ToList();
            }

            if(Peoples != null) {
                res.Peoples = Peoples.ToList();
            }

            return res;
        }
        public bool EqualsPlace(DmFile file) => Country == file.Country && State == file.State && City == file.City && Location == file.Location;
        public bool ContainsTag(DmTag tag, TagType tagType) => GetTagRef(tag, tagType) != null;
        public bool ContainsPeopleOrUnssigned(DmTag tag) {
            if(tag == null) {
                return !HasPeoplesTag;
            }

            return HasPeoplesTag && Peoples.FirstOrDefault((t) => t.Tag == tag) != null;
        }
        public bool ContainsAutorOrUnssigned(DmTag tag) {
            if(tag == null) {
                return !HasAutorsTag;
            }

            return HasAutorsTag && Autors.FirstOrDefault((t) => t.Tag == tag) != null;
        }
        public bool ContainsCategoryOrUnssigned(DmTag tag) {
            if(tag == null) {
                return !HasCategoriesTag;
            }

            return HasCategoriesTag && Categories.FirstOrDefault((t) => t.Tag == tag) != null;
        }
        public bool ContainsCollectionOrUnssigned(DmTag tag) {
            if(tag == null) {
                return !HasCollectionsTag;
            }

            return HasCollectionsTag && Collections.FirstOrDefault((t) => t.Tag == tag) != null;
        }
        public bool ContainsGenreOrUnssigned(DmTag tag) {
            if(tag == null) {
                return !HasGenresTag;
            }

            return HasGenresTag && Genres.FirstOrDefault((t) => t.Tag == tag) != null;
        }
        public bool ContainsKeywordOrUnssigned(DmTag tag) {
            if(tag == null) {
                return !HasKeywordsTag;
            }

            return HasKeywordsTag && Keywords.FirstOrDefault((t) => t.Tag == tag) != null;
        }
        public IDmKeyword GetTagRef(DmTag tag, TagType tagType) {
            switch(tagType) {
                case TagType.Autor:
                    return !HasAutorsTag ? null : Autors.FirstOrDefault((t) => t.Tag == tag);
                case TagType.Category:
                    return !HasCategoriesTag ? null : Categories.FirstOrDefault((t) => t.Tag == tag);
                case TagType.Genre:
                    return !HasGenresTag ? null : Genres.FirstOrDefault((t) => t.Tag == tag);
                case TagType.People:
                    return !HasPeoplesTag ? null : Peoples.FirstOrDefault((t) => t.Tag == tag);
                case TagType.Tag:
                    return !HasKeywordsTag ? null : Keywords.FirstOrDefault((t) => t.Tag == tag);
                case TagType.Collection:
                    return !HasCollectionsTag ? null : Collections.FirstOrDefault((t) => t.Tag == tag);
            }
            return null;
        }
        public void RemoveTag(DmModel model, DmTag tag, TagType tagType) {
            IDmKeyword keyword = GetTagRef(tag, tagType);
            if(keyword == null) {
                return;
            }

            DmFile file = keyword.File;
            switch(tagType) {
                case TagType.Autor:
                    model.Context.Autors.Remove((DmAutor)keyword);
                    AutorsCount = Autors.Count;
                    break;
                case TagType.Category:
                    model.Context.Categories.Remove((DmCategory)keyword);
                    CategoriesCount = Categories.Count;
                    break;
                case TagType.Genre:
                    model.Context.Genres.Remove((DmGenre)keyword);
                    GenresCount = Genres.Count;
                    break;
                case TagType.People:
                    model.Context.Peoples.Remove((DmPeople)keyword);
                    PeoplesCount = Peoples.Count;
                    break;
                case TagType.Tag:
                    model.Context.Keywords.Remove((DmKeyword)keyword);
                    KeywordsCount = Keywords.Count;
                    break;
                case TagType.Collection:
                    model.Context.Collections.Remove((DmCollection)keyword);
                    CollectionsCount = Collections.Count;
                    break;
            }
            keyword.RemovedTag = tag;
        }
        public bool IsTagsEquals(DmFile file, TagType tagType) {
            switch(tagType) {
                case TagType.Autor:
                    return IsTagsEquals(Autors, file.Autors);
                case TagType.Category:
                    return IsTagsEquals(Categories, file.Categories);
                case TagType.Collection:
                    return IsTagsEquals(Collections, file.Collections);
                case TagType.Genre:
                    return IsTagsEquals(Genres, file.Genres);
                case TagType.People:
                    return IsTagsEquals(Peoples, file.Peoples);
                case TagType.Tag:
                    return IsTagsEquals(Keywords, file.Keywords);
            }
            return false;
        }
        public bool IsTagsEquals(IEnumerable tags, IEnumerable tags2) {
            IEnumerator en1 = tags.GetEnumerator();
            IEnumerator en2 = tags2.GetEnumerator();

            while(true) {
                bool res1 = en1.MoveNext();
                bool res2 = en2.MoveNext();
                if(res1 ^ res2) {
                    return false;
                }

                if(!res1 && !res2) {
                    break;
                }

                if(en1.Current != en2.Current) {
                    return false;
                }
            }
            return true;
        }
        protected internal void OnTagAdded(TagType tagType) {
            switch(tagType) {
                case TagType.Autor:
                    AutorsCount = Autors.Count;
                    break;
                case TagType.Category:
                    CategoriesCount = Categories.Count;
                    break;
                case TagType.Collection:
                    CollectionsCount = Collections.Count;
                    break;
                case TagType.Genre:
                    GenresCount = Genres.Count;
                    break;
                case TagType.People:
                    PeoplesCount = Peoples.Count;
                    break;
                case TagType.Tag:
                    KeywordsCount = Keywords.Count;
                    break;
            }
        }
        public bool ContainsTag(DmTag dmTag) => ContainsTag(dmTag, dmTag.Type);
        public List<DmTag> GetTags(TagType type) {
            List<DmTag> res = new List<DmTag>();
            switch(type) {
                case TagType.Autor:
                    if(!HasAutorsTag) {
                        return res;
                    }

                    foreach(DmAutor autor in Autors) {
                        res.Add(autor.Tag);
                    }

                    break;
                case TagType.Category:
                    if(!HasCategoriesTag) {
                        return res;
                    }

                    foreach(DmCategory category in Categories) {
                        res.Add(category.Tag);
                    }

                    break;
                case TagType.Collection:
                    if(!HasCollectionsTag) {
                        return res;
                    }

                    foreach(DmCollection coll in Collections) {
                        res.Add(coll.Tag);
                    }

                    break;
                case TagType.Genre:
                    if(!HasGenresTag) {
                        return res;
                    }

                    foreach(DmGenre genre in Genres) {
                        res.Add(genre.Tag);
                    }

                    break;
                case TagType.People:
                    if(!HasPeoplesTag) {
                        return res;
                    }

                    foreach(DmPeople people in Peoples) {
                        res.Add(people.Tag);
                    }

                    break;
                case TagType.Tag:
                    if(!HasKeywordsTag) {
                        return res;
                    }

                    foreach(DmKeyword key in Keywords) {
                        res.Add(key.Tag);
                    }

                    break;
            }
            return res;
        }
        [NotMapped]
        public bool VisibleOnMap {
            get; set;
        }
        public static float InvalidGeoLocation => float.MinValue;
        [NotMapped]
        public bool HasGeoLocation => Latitude != InvalidGeoLocation && Longitude != InvalidGeoLocation;
        public int RotateAngle {
            get; set;
        }
        [NotMapped]
        public int DeltaAngle {
            get; set;
        }
    }
}
