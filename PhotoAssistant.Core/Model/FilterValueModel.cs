using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils;

namespace PhotoAssistant.Core.Model {
    public class Filter {
        public static string SavedSearchesString { get { return "SavedSearches"; } }
        public static string LastImportedString { get { return "Last Imported"; } }
        public static string ImportedTodayString { get { return "Imported Today"; } }
        public static string ImportedYesterdayString { get { return "Imported Yesterday"; } }
        public static string ImportedLastWeekString { get { return "Imported Last Week"; } }
        public static string ImportedLastMonthString { get { return "Imported Last Month"; } }
        public static string FoldersString { get { return "Folders"; } }
        public static string MediaFormatString { get { return "MediaFormat"; } }
        public static string ImagesString { get { return "Images"; } }
        public static string MarkString { get { return "Mark"; } }
        public static string RejectString { get { return "Reject"; } }
        public static string MarkedString { get { return "Marked"; } }
        public static string RejectedString { get { return "Rejected"; } }
        public static string UnmarkedString { get { return "Unmarked"; } }
        public static string NotRejectedString { get { return "Not Rejected"; } }
        public static string RatingHeaderString { get { return "Rating"; } }
        public static string ColorLabelString { get { return "Color Label"; } }
        public static string ColorLabelNoneString { get { return "None"; } }
        public static string PlaceString { get { return "Place"; } }
        public static string CountryString { get { return "Country"; } }
        public static string StateString { get { return "State"; } }
        public static string CityString { get { return "City"; } }
        public static string LocationString { get { return "Location"; } }
        public static string CreationDateTimeString { get { return "Creation DateTime"; } }
        public static string ImportDateTimeString { get { return "Import DateTime"; } }
        public static string Year { get { return "Year"; } }
        public static string Month { get { return "Month"; } }
        public static string Day { get { return "Day"; } }
        public static string PeopleHeaderString { get { return "People"; } }
        public static string UnassignedString { get { return "Unassigned"; } }
        public static string EventHeaderString { get { return "Event"; } }
        public static string TagsHeaderString { get { return "Tags"; } }
        public static string CategoriesHeaderString { get { return "Categories"; } }
        public static string CollectionsHeaderString { get { return "Collections"; } }
        public static string AutorsHeaderString { get { return "Autors"; } }
        public static string GenresHeaderString { get { return "Genres"; } }
        public static string ProjectHeaderString { get { return "Projects"; } }
        public static string ProjectString { get { return "Project"; } }
        public static string SceneString { get { return "Scene"; } }
        public static string ClientString { get { return "Client"; } }

        public static int MaxIndex { get { return int.MaxValue; } }
        static int index = 0;
        public static int GetNextIndex() { return index++; }

        public Filter() {
            Id = Guid.NewGuid();
            Index = GetNextIndex();
            OperationType = FilterOperationType.And;
            this.valueInt = Int16.MinValue;
        }

        public Filter(string text, bool isHeader, bool isSystem, Guid parentId)
            : this(text, isHeader, parentId) {
            IsSystem = isSystem;
        }
        public Filter(string text, bool isHeader, Guid parentId)
            : this() {
            Text = text;
            IsHeader = isHeader;
            ParentId = parentId;
        }
        public Filter(FilterType type, Guid parentId, object value) : this(type, parentId) {
            Value = value;
        }
        public Filter(FilterType type, Guid parentId) : this(type, GetText(type), GetIsHeader(type), true, parentId) { }
        public Filter(FilterType type, string text, bool isHeader, bool isSystem, Guid parentId)
            : this(text, isHeader, isSystem, parentId) {
            Type = type;
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }
        public int Index { get; set; }
        public Guid ParentId { get; set; }
        object val;
        [NotMapped]
        public object Value {
            get {
                return val;
            }
            set {
                if(Value == value)
                    return;
                val = value;
                OnValueChanged();
            }
        }
        FilterValueType valueType = FilterValueType.Object;
        public FilterValueType ValueType {
            get { return valueType; }
            set {
                if(ValueType == value)
                    return;
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
        private void OnValueChanged() {
            if(Value is ISupportId) {
                ValueId = ((ISupportId)Value).Id;
                this.valueType = FilterValueType.Object;
            }
            if(Value is int) {
                ValueInt = (int)Value;
                this.valueType = FilterValueType.Integer;
            }
            if(Value is bool) {
                ValueBoolean = (bool)Value;
                this.valueType = FilterValueType.Boolean;
            }
            if(Value is Enum) {
                this.valueInt = (int)Value;
                this.val = ValueInt;
                this.valueType = FilterValueType.Integer;
            }
            if(Value is string) {
                ValueString = (string)Value;
                this.valueType = FilterValueType.String;
            }
            Text = GetFilterTextByType(Value);
        }

        int valueInt;
        public int ValueInt {
            get { return valueInt; }
            set {
                if(ValueInt == value)
                    return;
                valueInt = value;
                if(ValueType == FilterValueType.Integer)
                    Value = ValueInt;
            }
        }
        bool valueBoolean;
        public bool ValueBoolean {
            get { return valueBoolean; }
            set {
                if(ValueBoolean == value)
                    return;
                valueBoolean = value;
                if(ValueType == FilterValueType.Boolean)
                    Value = ValueBoolean;
            }
        }
        string valueString;
        public string ValueString {
            get { return valueString; }
            set {
                if(ValueString == value)
                    return;
                valueString = value;
                if(ValueType == FilterValueType.String)
                    Value = ValueString;
            }
        }
        public Guid ValueId {
            get;
            set;
        }
        public string Text { get; set; }
        public bool IsHeader { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        FilterType type = FilterType.None;
        public FilterType Type {
            get { return type; }
            set {
                if(Type == value)
                    return;
                type = value;
                Text = GetFilterTextByType(Value);
            }
        }
        public FilterOperationType OperationType { get; set; }
        [NotMapped]
        public FilterOperationType ForcedOperationType { get; set; }
        [NotMapped]
        public FilterOperationType ViewOperationType { get { return ForcedOperationType != FilterOperationType.None ? ForcedOperationType : OperationType; } }
        public int MatchedCount { get; set; }
        Filter parent;
        [NotMapped]
        public Filter Parent {
            get { return parent; }
            set {
                Filter prevParent = Parent;
                parent = value;
                OnParentChanged(prevParent, Parent);
            }
        }

        private void OnParentChanged(Filter prevParent, Filter newParent) {
            if(prevParent != null) {
                prevParent.Children.Remove(this);
            }
            if(newParent != null && !newParent.Children.Contains(this)) {
                newParent.Children.Add(this);
            }
            ParentId = newParent != null ? newParent.Id : Guid.Empty;
        }
        [NotMapped]
        public bool HasActiveChildren { get { return ActiveChildren.Count > 0; } }
        List<Filter> activeChildren;
        [NotMapped]
        public List<Filter> ActiveChildren {
            get {
                if(activeChildren == null)
                    activeChildren = new List<Filter>();
                return activeChildren;
            }
        }

        List<Filter> children;
        [NotMapped]
        public List<Filter> Children {
            get {
                if(children == null)
                    children = new List<Filter>();
                return children;
            }
        }

        private static bool GetIsHeader(FilterType type) {
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

        private static string GetText(FilterType type) {
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
                //case FilterType.Marked:
                //    return Filter.MarkedString;
                //case FilterType.Rejected:
                //    return Filter.RejectedString;
                //case FilterType.Unmarked:
                //    return Filter.UnmarkedString;
                //case FilterType.NotRejected:
                //    return Filter.NotRejectedString;
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
        public override string ToString() {
            return Text + " - " + MatchedCount;
        }

        public bool HasChildWithNonZeroMatchCount() {
            Filter filter = this;
            if(filter.MatchedCount > 0)
                return true;
            foreach(Filter child in filter.Children) {
                if(child.HasChildWithNonZeroMatchCount())
                    return true;
            }
            return false;
        }

        [NotMapped]
        public bool Enabled {
            get {
                if(Parent == null)
                    return true;
                if(Parent.Type >= FilterType.AutorsHeader && Parent.Type <= FilterType.TagsHeader)
                    return MatchedCount > 0;
                if(Type >= FilterType.Autor && Type <= FilterType.Tag)
                    return Parent.Enabled;
                return true;
            }
        }
        [NotMapped]
        public bool Visible {
            get { return MatchedCount > 0; }
        }
    }

    public enum FilterValueType { Object, Integer, Boolean, String }

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
        Guid Id { get; }
    }
}
