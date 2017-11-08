using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
namespace PhotoAssistant.Core.Model {
    public class Filter {
        public static string SavedSearchesString => "SavedSearches";
        public static string LastImportedString => "Last Imported";
        public static string ImportedTodayString => "Imported Today";
        public static string ImportedYesterdayString => "Imported Yesterday";
        public static string ImportedLastWeekString => "Imported Last Week";
        public static string ImportedLastMonthString => "Imported Last Month";
        public static string FoldersString => "Folders";
        public static string MediaFormatString => nameof(MediaFormat);
        public static string ImagesString => "Images";
        public static string MarkString => "Mark";
        public static string RejectString => "Reject";
        public static string MarkedString => "Marked";
        public static string RejectedString => "Rejected";
        public static string UnmarkedString => "Unmarked";
        public static string NotRejectedString => "Not Rejected";
        public static string RatingHeaderString => "Rating";
        public static string ColorLabelString => "Color Label";
        public static string ColorLabelNoneString => "None";
        public static string PlaceString => "Place";
        public static string CountryString => "Country";
        public static string StateString => "State";
        public static string CityString => "City";
        public static string LocationString => "Location";
        public static string CreationDateTimeString => "Creation DateTime";
        public static string ImportDateTimeString => "Import DateTime";
        public static string Year => "Year";
        public static string Month => "Month";
        public static string Day => "Day";
        public static string PeopleHeaderString => "People";
        public static string UnassignedString => "Unassigned";
        public static string EventHeaderString => "Event";
        public static string TagsHeaderString => "Tags";
        public static string CategoriesHeaderString => "Categories";
        public static string CollectionsHeaderString => "Collections";
        public static string AutorsHeaderString => "Autors";
        public static string GenresHeaderString => "Genres";
        public static string ProjectHeaderString => "Projects";
        public static string ProjectString => "Project";
        public static string SceneString => "Scene";
        public static string ClientString => "Client";
        public static int MaxIndex => int.MaxValue;
        static int index = 0;
        public static int GetNextIndex() => index++;
        public Filter() {
        Id = Guid.NewGuid();
            Index = GetNextIndex();
            OperationType = FilterOperationType.And;
            valueInt = short.MinValue;
        }
        public Filter(string text, bool isHeader, bool isSystem, Guid parentId)
            : this(text, isHeader, parentId) => IsSystem = isSystem;
        public Filter(string text, bool isHeader, Guid parentId)
            : this() {
        Text = text;
            IsHeader = isHeader;
            ParentId = parentId;
        }
        public Filter(FilterType type, Guid parentId, object value) : this(type, parentId) => Value = value;
        public Filter(FilterType type, Guid parentId) : this(type, GetText(type), GetIsHeader(type), true, parentId) {
        }
        public Filter(FilterType type, string text, bool isHeader, bool isSystem, Guid parentId)
            : this(text, isHeader, isSystem, parentId) => Type = type;

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id {
            get; set;
        }
        public int Index {
            get; set;
        }
        public Guid ParentId {
            get; set;
        }
        object val;
        [NotMapped]
        public object Value {
            get => val;
            set {
                if(Value == value) {
                    return;
                }

                val = value;
                OnValueChanged();
            }
        }
        FilterValueType valueType = FilterValueType.Object;
        public FilterValueType ValueType {
            get => valueType;
            set {
                if(ValueType == value) {
                    return;
                }

                valueType = value;
                OnValueTypeChanged();
            }
        }
        protected virtual void OnValueTypeChanged() {
            switch(ValueType) {
                case FilterValueType.Boolean:
                    Value = ValueBoolean;
                    break;
                case FilterValueType.Integer:
                    Value = ValueInt;
                    break;
                case FilterValueType.String:
                    Value = ValueString;
                    break;
            }
        }
        void OnValueChanged() {
            if(Value is ISupportId) {
                ValueId = ((ISupportId)Value).Id;
                valueType = FilterValueType.Object;
            }
            if(Value is int) {
                ValueInt = (int)Value;
                valueType = FilterValueType.Integer;
            }
            if(Value is bool) {
                ValueBoolean = (bool)Value;
                valueType = FilterValueType.Boolean;
            }
            if(Value is Enum) {
                valueInt = (int)Value;
                val = ValueInt;
                valueType = FilterValueType.Integer;
            }
            if(Value is string) {
                ValueString = (string)Value;
                valueType = FilterValueType.String;
            }
            Text = GetFilterTextByType(Value);
        }
        int valueInt;
        public int ValueInt {
            get => valueInt;
            set {
                if(ValueInt == value) {
                    return;
                }

                valueInt = value;
                if(ValueType == FilterValueType.Integer) {
                    Value = ValueInt;
                }
            }
        }
        bool valueBoolean;
        public bool ValueBoolean {
            get => valueBoolean;
            set {
                if(ValueBoolean == value) {
                    return;
                }

                valueBoolean = value;
                if(ValueType == FilterValueType.Boolean) {
                    Value = ValueBoolean;
                }
            }
        }
        string valueString;
        public string ValueString {
            get => valueString;
            set {
                if(ValueString == value) {
                    return;
                }

                valueString = value;
                if(ValueType == FilterValueType.String) {
                    Value = ValueString;
                }
            }
        }
        public Guid ValueId {
            get;
            set;
        }
        public string Text {
            get; set;
        }
        public bool IsHeader {
            get; set;
        }
        public bool IsActive {
            get; set;
        }
        public bool IsSystem {
            get; set;
        }
        FilterType type = FilterType.None;
        public FilterType Type {
            get => type;
            set {
                if(Type == value) {
                    return;
                }

                type = value;
                Text = GetFilterTextByType(Value);
            }
        }
        public FilterOperationType OperationType {
            get; set;
        }
        [NotMapped]
        public FilterOperationType ForcedOperationType {
            get; set;
        }
        [NotMapped]
        public FilterOperationType ViewOperationType => ForcedOperationType != FilterOperationType.None ? ForcedOperationType : OperationType;
        public int MatchedCount {
            get; set;
        }
        Filter parent;
        [NotMapped]
        public Filter Parent {
            get => parent;
            set {
                Filter prevParent = Parent;
                parent = value;
                OnParentChanged(prevParent, Parent);
            }
        }
        void OnParentChanged(Filter prevParent, Filter newParent) {
            if(prevParent != null) {
                prevParent.Children.Remove(this);
            }
            if(newParent != null && !newParent.Children.Contains(this)) {
                newParent.Children.Add(this);
            }
            ParentId = newParent != null ? newParent.Id : Guid.Empty;
        }
        [NotMapped]
        public bool HasActiveChildren => ActiveChildren.Count > 0;
        List<Filter> activeChildren;
        [NotMapped]
        public List<Filter> ActiveChildren {
            get {
                if(activeChildren == null) {
                    activeChildren = new List<Filter>();
                }

                return activeChildren;
            }
        }
        List<Filter> children;
        [NotMapped]
        public List<Filter> Children {
            get {
                if(children == null) {
                    children = new List<Filter>();
                }

                return children;
            }
        }
        static bool GetIsHeader(FilterType type) {
            switch(type) {
                case FilterType.SavedSearches:
                case FilterType.FolderHeader:
                case FilterType.MediaFormatHeader:
                case FilterType.MediaFormatType:
                case FilterType.Mark:
                case FilterType.Reject:
                case FilterType.RatingHeader:
                case FilterType.ColorLabelHeader:
                case FilterType.CreationDateTimeHeader:
                case FilterType.ImportDateTimeHeader:
                case FilterType.Place:
                case FilterType.TagsHeader:
                case FilterType.PeopleHeader:
                case FilterType.EventHeader:
                case FilterType.CollectionsHeader:
                case FilterType.CategoriesHeader:
                case FilterType.GenresHeader:
                case FilterType.AutorsHeader:
                case FilterType.ProjectHeader:
                    return true;
            }
            return false;
        }
        static string GetText(FilterType type) {
            switch(type) {
                case FilterType.SavedSearches:
                    return Filter.SavedSearchesString;
                case FilterType.LastImported:
                    return Filter.LastImportedString;
                case FilterType.ImportedToday:
                    return Filter.ImportedTodayString;
                case FilterType.ImportedYesterday:
                    return Filter.ImportedYesterdayString;
                case FilterType.ImportedLastWeek:
                    return Filter.ImportedLastWeekString;
                case FilterType.ImportedLastMonth:
                    return Filter.ImportedLastMonthString;
                case FilterType.FolderHeader:
                    return Filter.FoldersString;
                case FilterType.MediaFormatHeader:
                    return Filter.MediaFormatString;
                case FilterType.Images:
                    return Filter.ImagesString;
                case FilterType.Mark:
                    return Filter.MarkString;
                case FilterType.Reject:
                    return Filter.RejectString;
                case FilterType.RatingHeader:
                    return Filter.RatingHeaderString;
                case FilterType.ColorLabelHeader:
                    return Filter.ColorLabelString;
                case FilterType.CreationDateTimeHeader:
                    return Filter.CreationDateTimeString;
                case FilterType.ImportDateTimeHeader:
                    return Filter.ImportDateTimeString;
                case FilterType.Place:
                    return Filter.PlaceString;
                case FilterType.Country:
                    return Filter.CountryString;
                case FilterType.State:
                    return Filter.StateString;
                case FilterType.City:
                    return Filter.CityString;
                case FilterType.Location:
                    return Filter.LocationString;
                case FilterType.TagsHeader:
                    return Filter.TagsHeaderString;
                case FilterType.PeopleHeader:
                    return Filter.PeopleHeaderString;
                case FilterType.EventHeader:
                    return Filter.EventHeaderString;
                case FilterType.CollectionsHeader:
                    return Filter.CollectionsHeaderString;
                case FilterType.CategoriesHeader:
                    return Filter.CategoriesHeaderString;
                case FilterType.AutorsHeader:
                    return Filter.AutorsHeaderString;
                case FilterType.GenresHeader:
                    return Filter.GenresHeaderString;
                case FilterType.ProjectHeader:
                    return Filter.ProjectHeaderString;
            }
            return string.Empty;
        }
        public string GetFilterTextByType(object value) {
            switch(Type) {
                case FilterType.Scene:
                    return string.IsNullOrEmpty((string)value) ? SceneString : value.ToString();
                case FilterType.Project:
                    return string.IsNullOrEmpty((string)value) ? ProjectString : value.ToString();
                case FilterType.Client:
                    return string.IsNullOrEmpty((string)value) ? ClientString : value.ToString();
                case FilterType.Country:
                    return string.IsNullOrEmpty((string)value) ? CountryString : value.ToString();
                case FilterType.State:
                    return string.IsNullOrEmpty((string)value) ? StateString : value.ToString();
                case FilterType.City:
                    return string.IsNullOrEmpty((string)value) ? CityString : value.ToString();
                case FilterType.Location:
                    return string.IsNullOrEmpty((string)value) ? LocationString : value.ToString();
                case FilterType.CreationYear:
                case FilterType.ImportYear:
                    return value == null ? string.Empty : value.ToString();
                case FilterType.CreationMonth:
                case FilterType.ImportMonth:
                    return value == null ? string.Empty : CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((int)value);
                case FilterType.ImportDay:
                case FilterType.CreationDay:
                    return value == null ? string.Empty : value.ToString();
                case FilterType.Marked:
                    return (value != null && (bool)value) == true ? MarkedString : UnmarkedString;
                case FilterType.Rejected:
                    return (value != null && (bool)value) == true ? RejectedString : NotRejectedString;
                case FilterType.ColorLabel:
                    return value != null ? ((DmColorLabel)value).Text : string.Empty;
                case FilterType.MediaFormatType:
                    return value == null ? string.Empty : Enum.GetName(typeof(MediaType), value);
                case FilterType.MediaFormat:
                    return value == null ? string.Empty : ((MediaFormat)value).Text;
                case FilterType.Folder:
                    return value == null ? string.Empty : value.ToString();
                case FilterType.Rating:
                    return value == null ? string.Empty : value.ToString();
                case FilterType.Event:
                    return string.IsNullOrEmpty((string)value) ? Filter.UnassignedString : value.ToString();
                case FilterType.People:
                case FilterType.Autor:
                case FilterType.Tag:
                case FilterType.Collection:
                case FilterType.Category:
                case FilterType.Genre:
                    return value == null ? Filter.UnassignedString : ((DmTag)value).Value;
            }
            return GetText(Type);
        }
        public override string ToString() => Text + " - " + MatchedCount;
        public bool HasChildWithNonZeroMatchCount() {
            Filter filter = this;
            if(filter.MatchedCount > 0) {
                return true;
            }

            foreach(Filter child in filter.Children) {
                if(child.HasChildWithNonZeroMatchCount()) {
                    return true;
                }
            }
            return false;
        }
        [NotMapped]
        public bool Enabled {
            get {
                if(Parent == null) {
                    return true;
                }

                if(Parent.Type >= FilterType.AutorsHeader && Parent.Type <= FilterType.TagsHeader) {
                    return MatchedCount > 0;
                }

                if(Type >= FilterType.Autor && Type <= FilterType.Tag) {
                    return Parent.Enabled;
                }

                return true;
            }
        }
        [NotMapped]
        public bool Visible => MatchedCount > 0;
    }
    public enum FilterValueType {
        Object, Integer, Boolean, String
    }
    public enum FilterOperationType {
        None,
        And,
        Or
    }
    public enum FilterType {
        None,
        FolderHeader,
        Folder,
        RatingHeader,
        ColorLabelHeader,
        ColorLabel,
        SavedSearches,
        LastImported,
        ImportedToday,
        ImportedYesterday,
        ImportedLastWeek,
        ImportedLastMonth,
        MediaFormatHeader,
        MediaFormatType,
        MediaFormat,
        Images,
        Mark,
        Marked,
        Unmarked,
        Reject,
        Rejected,
        NotRejected,
        CreationDateTimeHeader,
        CreationYear,
        CreationMonth,
        CreationDay,
        ImportDateTimeHeader,
        ImportYear,
        ImportDay,
        ImportMonth,
        Place,
        Country,
        State,
        City,
        Location,
        EventHeader,
        Rating,
        Event,
        AutorsHeader,
        CategoriesHeader,
        CollectionsHeader,
        GenresHeader,
        PeopleHeader,
        TagsHeader,
        Autor,
        Category,
        Collection,
        Genre,
        People,
        Tag,
        Project,
        ProjectHeader,
        Client,
        Scene
    }
    public interface ISupportId {
        Guid Id {
            get;
        }
    }
}
