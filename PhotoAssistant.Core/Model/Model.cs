using DevExpress.Data.Filtering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoAssistant.Core.Model {
    public enum FileOperation { Add, Remove, Update }

    public class DmModel {
        public event EventHandler FilesRowDataChanged;
        public event EventHandler FilterValuesChanged;
        public event EventHandler FilterStateChanged;
        public event EventHandler DatabaseConnected;

        public static bool AllowGenerateDefaultTags { get; set; }
        static DmModel() {
            AllowGenerateDefaultTags = true;
        }

        DmModelHelpers helper;
        public DmModelHelpers Helper {
            get {
                if(helper == null)
                    helper = new DmModelHelpers(this);
                return helper;
            }
        }

        public bool AllowHierarchicallyKeywordFilters { get; set; }
        protected bool ShouldResetLastImportedFiltrer { get; set; }
        public void BeginAddFiles() {
           // Properties.ImportIndex++;
            ShouldResetLastImportedFiltrer = true;
        }

        public void EndAddFiles() {
            ShouldResetLastImportedFiltrer = false;
            //Properties.ShouldCalcAspectRatio = true;
            if(!IsLockUpdate)
                Context.SaveChanges();
        }
        public void AddFile(DmFile file) {
            if(Context == null)
                return;
            //if(ShouldResetLastImportedFiltrer) {
            //    GetFilter(FilterType.LastImported).MatchedCount = 0;
            //    ShouldResetLastImportedFiltrer = false;
            //}
            //file.ImportIndex = Properties.ImportIndex;
            Context.Files.Add(file);
            //UpdateFiltersByFile(file, null, FileOperation.Add);
        }

        public void AddFiles(List<DmFile> list) {
            if(Context == null)
                return;
            Properties.ImportIndex++;
            foreach(DmFile file in list) {
                if(!file.AllowAdd)
                    continue;
                file.ImportIndex = Properties.ImportIndex;
                Context.Files.Add(file);
            }
            if(!IsLockUpdate)
                Context.SaveChanges();
        }

        private string CreateThumb(string fileName) {
            return fileName;
        }

        public virtual IEnumerable<DmFile> GetActualGalleryDataSource() {
            return GetFilteredFiles(FilterOperationType.Or);
        }

        public virtual IEnumerable<DmFile> GetFiles() {
            return Context.Files.Local.ToArray();
        }

        string tabs = "";
        public void PrintFilterTree() {
            var res = from filter in Context.Filters.Local
                      where filter.Parent == null
                      orderby filter.Index ascending
                      select filter;

            foreach(Filter f in res) {
                DebugPrintFilter(f);
            }
        }

        private void DebugPrintFilter(Filter f) {
            Debug.WriteLine(tabs + f.Text + "      " + f.MatchedCount);
            tabs += "    ";
            foreach(Filter child in f.Children)
                DebugPrintFilter(child);
            tabs = tabs.Substring(0, tabs.Length - 4);
        }

        public virtual IEnumerable<Filter> GetVisibleFilters() {
            if(Context == null)
                return null;
            var res = from filter in Context.Filters.Local
                      where filter.Visible == true || filter.IsHeader
                      orderby filter.Index ascending
                      select filter;
            return res.ToList();
        }

        public virtual IEnumerable<DmColorLabel> GetColorLabels() {
            if(Context == null)
                return null;
            return Context.ColorLabels.Local;
        }

        protected virtual void ClearFilterValues() {
            Context.Filters.RemoveRange(Context.Filters.Where((filter) => !filter.IsSystem));
            foreach(Filter filter in Context.Filters) {
                filter.MatchedCount = 0;
            }
        }

        Filter folderHeader;
        public Filter FolderHeaderFilter {
            get {
                if(folderHeader == null)
                    folderHeader = GetFilter(FilterType.FolderHeader);
                return folderHeader;
            }
        }

        Filter ratingHeader;
        public Filter RatingHeaderFilter {
            get {
                if(ratingHeader == null)
                    ratingHeader = GetFilter(FilterType.RatingHeader);
                return ratingHeader;
            }
        }

        Filter colorLabelHeader;
        public Filter ColorLabelHeaderFilter {
            get {
                if(colorLabelHeader == null)
                    colorLabelHeader = GetFilter(FilterType.ColorLabelHeader);
                return colorLabelHeader;
            }
        }

        Filter lastImported;
        public Filter LastImportedFilter {
            get {
                if(lastImported == null)
                    lastImported = GetFilter(FilterType.LastImported);
                return lastImported;
            }
        }

        Filter importedToday;
        public Filter ImportedTodayFilter {
            get {
                if(importedToday == null)
                    importedToday = GetFilter(FilterType.ImportedToday);
                return importedToday;
            }
        }

        Filter importedYesterday;
        public Filter ImportedYesterdayFilter {
            get {
                if(importedYesterday == null)
                    importedYesterday = GetFilter(FilterType.ImportedYesterday);
                return importedYesterday;
            }
        }

        public List<DmStorageVolume> GetVolumes() {
            return Context.Volumes.Local.ToList();
        }

        Filter importedLastMonth;
        public Filter ImportedLastMonthFilter {
            get {
                if(importedLastMonth == null)
                    importedLastMonth = GetFilter(FilterType.ImportedLastMonth);
                return importedLastMonth;
            }
        }

        Filter importedLastWeek;
        public Filter ImportedLastWeekFilter {
            get {
                if(importedLastWeek == null)
                    importedLastWeek = GetFilter(FilterType.ImportedLastWeek);
                return importedLastWeek;
            }
        }

        Filter mediaFormatHeader;
        public Filter MediaFormatHeaderFilter {
            get {
                if(mediaFormatHeader == null)
                    mediaFormatHeader = GetFilter(FilterType.MediaFormatHeader);
                return mediaFormatHeader;
            }
        }

        Filter mark;
        public Filter MarkHeaderFilter {
            get {
                if(mark == null)
                    mark = GetFilter(FilterType.Mark);
                return mark;
            }
        }

        Filter reject;
        public Filter RejectHeaderFilter {
            get {
                if(reject == null)
                    reject = GetFilter(FilterType.Reject);
                return reject;
            }
        }

        Filter creationDateFilter;
        public Filter CreationDateHeaderFilter {
            get {
                if(creationDateFilter == null)
                    creationDateFilter = GetFilter(FilterType.CreationDateTimeHeader);
                return creationDateFilter;
            }
        }

        Filter projectHeaderFilter;
        public Filter ProjectHeaderFilter {
            get {
                if(projectHeaderFilter == null)
                    projectHeaderFilter = GetFilter(FilterType.ProjectHeader);
                return projectHeaderFilter;
            }
        }

        Filter importDateFilter;
        public Filter ImportDateHeaderFilter {
            get {
                if(importDateFilter == null)
                    importDateFilter = GetFilter(FilterType.ImportDateTimeHeader);
                return importDateFilter;
            }
        }

        Filter placeFilter;
        public Filter PlaceFilter {
            get {
                if(placeFilter == null)
                    placeFilter = GetFilter(FilterType.Place);
                return placeFilter;
            }
        }

        Filter tagsHeaderFilter;
        public Filter TagsHeaderFilter {
            get {
                if(tagsHeaderFilter == null)
                    tagsHeaderFilter = GetFilter(FilterType.TagsHeader);
                return tagsHeaderFilter;
            }
        }

        Filter peopleHeaderFilter;
        public Filter PeoplesHeaderFilter {
            get {
                if(peopleHeaderFilter == null)
                    peopleHeaderFilter = GetFilter(FilterType.PeopleHeader);
                return peopleHeaderFilter;
            }
        }

        Filter eventHeaderFilter;
        public Filter EventsHeaderFilter {
            get {
                if(eventHeaderFilter == null)
                    eventHeaderFilter = GetFilter(FilterType.EventHeader);
                return eventHeaderFilter;
            }
        }


        Filter categoryHeaderFilter;
        public Filter CategoriesHeaderFilter {
            get {
                if(categoryHeaderFilter == null)
                    categoryHeaderFilter = GetFilter(FilterType.CategoriesHeader);
                return categoryHeaderFilter;
            }
        }

        Filter collectionHeaderFilter;
        public Filter CollectionsHeaderFilter {
            get {
                if(collectionHeaderFilter == null)
                    collectionHeaderFilter = GetFilter(FilterType.CollectionsHeader);
                return collectionHeaderFilter;
            }
        }

        Filter autorsHeaderFilter;
        public Filter AutorsHeaderFilter {
            get {
                if(autorsHeaderFilter == null)
                    autorsHeaderFilter = GetFilter(FilterType.AutorsHeader);
                return autorsHeaderFilter;
            }
        }

        Filter genresHeaderFilter;
        public Filter GenresHeaderFilter {
            get {
                if(genresHeaderFilter == null)
                    genresHeaderFilter = GetFilter(FilterType.GenresHeader);
                return genresHeaderFilter;
            }
        }

        public Filter GetFilter(Filter parent, object value) {
            return parent.Children.FirstOrDefault((f) => object.Equals(f.Value, value));
        }

        public Filter GetFilter(Filter parent, FilterType type, object value) {
            return parent.Children.FirstOrDefault((f) => f.Type == type && object.Equals(f.Value, value));
        }

        public Filter GetFilter(Filter parent, DmTag tag, FilterType type, bool createIfNotExist) {
            Filter filter = GetFilter(parent, tag);
            if(filter != null)
                return filter;
            filter = CreateFilter(type, tag, parent);
            return filter;
        }

        public Filter GetFilter(FilterType type) {
            return Context.Filters.Local.FirstOrDefault((filter) => filter.Type == type);
        }

        protected void BuildFilterParentChildRelations() {
            foreach(Filter filter in Context.Filters.Local) {
                filter.Parent = Context.Filters.Local.FirstOrDefault((f) => f.Id == filter.ParentId);
            }
        }

        protected bool UpdateHasActiveChildrenFlag() {
            bool res = false;
            foreach(Filter filter in Context.Filters.Local) {
                if(filter.Parent != null)
                    continue;
                UpdateHasActiveChildrenFlag(filter);
                res |= filter.IsActive || filter.HasActiveChildren;
            }
            return res;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void UpdateHasActiveChildrenFlag(Filter filter) {
            filter.ActiveChildren.Clear();
            foreach(Filter child in filter.Children) {
                UpdateHasActiveChildrenFlag(child);
                if(child.IsActive || child.HasActiveChildren) {
                    filter.ActiveChildren.Add(child);
                }
            }
        }

        protected bool ProcessFilteredFile(DmFile file, FilterResult res, FilterOperationType rootFilterType) {
            if(res == FilterResult.True) {
                if(rootFilterType == FilterOperationType.Or) {
                    FilteredFiles.Add(file);
                    return true;
                }
            }
            if(res == FilterResult.False) {
                if(rootFilterType != FilterOperationType.Or)
                    return true;
            }
            return false;
        }

        List<DmFile> filteredFiles;
        public List<DmFile> FilteredFiles {
            get {
                if(filteredFiles == null)
                    filteredFiles = new List<DmFile>();
                return filteredFiles;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public List<DmFile> GetFilteredFiles(FilterOperationType rootFilterType) {
            FilteredFiles.Clear();
            bool hasActiveFilter = UpdateHasActiveChildrenFlag();
            foreach(DmFile file in Context.Files.Local) {
                if(!hasActiveFilter) {
                    if(MatchFileByGrouping(file))
                        FilteredFiles.Add(file);
                    continue;
                }

                FilterResult totalRes = FilterResult.None;
                FilterResult res = MatchSavedSearchesFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchFolderFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchMediaFormatTypeFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchRatingFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchColorLabelFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchCreationDateTimeFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchImportDateTimeFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchPeopleFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchAutorFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchCategoryFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchCollectionFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchGenreFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchTagFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchEventFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                res = MatchPlaceFilter(file);
                totalRes = CalcTotalRes(totalRes, res, rootFilterType);
                if(ProcessFilteredFile(file, res, rootFilterType))
                    continue;

                if(!MatchFileByGrouping(file))
                    continue;

                if(totalRes == FilterResult.True)
                    FilteredFiles.Add(file);
            }

            return FilteredFiles;
        }

        public DmStorageVolume CheckAddStorageVolume(StorageVolumeInfo volume) {
            if(volume == null)
                return null;
            DmStorageVolume vm = Context.Volumes.FirstOrDefault((v) => v.VolumeId == volume.VolumeId);
            if(vm != null)
                return vm;
            return AddVolume(volume);
        }

        public DmStorageVolume AddVolume(StorageVolumeInfo volume) {
            DmStorageVolume dmv = new DmStorageVolume();
            dmv.VolumeId = volume.VolumeId;
            dmv.ProjectFolder = volume.ProjectFolder;
            Context.Volumes.Add(dmv);
            if(!IsLockUpdate)
                Context.SaveChanges();
            return dmv;
        }

        public void SaveProjectParameters(ProjectInfo project) {
            BeginUpdate();
            try {
                Properties.ProjectDescription = project.Description;
                Properties.ProjectFileCount = project.FileCount;
                Properties.ProjectId = project.Id;
                Properties.ProjectName = project.Name;
                Properties.ProjectOpenCount = project.OpenCount;
            } finally {
                EndUpdate();
            }
        }

        private bool MatchFileByGrouping(DmFile file) {
            return !file.IsGrouped || file.IsGroupOwner || file.GroupOwner.IsExpanded;
        }

        private FilterResult MatchPlaceFilter(DmFile file) {
            return MatchFilterCore(file, PlaceFilter, (f, val) => f.Country == (string)val, (f, ft) => MatchStateFilter(f, ft));
        }

        private FilterResult MatchStateFilter(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.State == (string)val, (f, ft) => MatchCityFilter(f, ft));
        }

        private FilterResult MatchCityFilter(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.City == (string)val, (f, ft) => MatchLocationFilter(f, ft));
        }

        private FilterResult MatchLocationFilter(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.Location == (string)val, null);
        }

        private FilterResult MatchEventFilter(DmFile file) {
            return MatchFilterCore(file, EventsHeaderFilter, (f, val) => f.Event == (string)val, null);
        }

        private FilterResult MatchTagFilter(DmFile file) {
            if(!AllowHierarchicallyKeywordFilters)
                return MatchFilterCore(file, TagsHeaderFilter, (f, val) => f.ContainsKeywordOrUnssigned((DmTag)val), null);
            return MatchTagFilterCore(file, TagsHeaderFilter);
        }

        private FilterResult MatchTagFilterCore(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.ContainsKeywordOrUnssigned((DmTag)val), (f, ft) => MatchTagFilterCore(f, ft));
        }

        private FilterResult MatchGenreFilter(DmFile file) {
            if(!AllowHierarchicallyKeywordFilters)
                return MatchFilterCore(file, GenresHeaderFilter, (f, val) => f.ContainsGenreOrUnssigned((DmTag)val), null);
            return MatchGenreFilterCore(file, GenresHeaderFilter);
        }

        private FilterResult MatchGenreFilterCore(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.ContainsGenreOrUnssigned((DmTag)val), (f, ft) => MatchGenreFilterCore(f, ft));
        }

        private FilterResult MatchCollectionFilter(DmFile file) {
            if(!AllowHierarchicallyKeywordFilters)
                return MatchFilterCore(file, CollectionsHeaderFilter, (f, val) => f.ContainsCollectionOrUnssigned((DmTag)val), null);
            return MatchCollectionFilterCore(file, CollectionsHeaderFilter);
        }

        private FilterResult MatchCollectionFilterCore(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.ContainsCollectionOrUnssigned((DmTag)val), (f, ft) => MatchCollectionFilterCore(f, ft));
        }

        private FilterResult MatchCategoryFilter(DmFile file) {
            if(!AllowHierarchicallyKeywordFilters)
                return MatchFilterCore(file, CategoriesHeaderFilter, (f, val) => f.ContainsCategoryOrUnssigned((DmTag)val), null);
            return MatchCategoryFilterCore(file, CategoriesHeaderFilter);
        }

        private FilterResult MatchCategoryFilterCore(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.ContainsCategoryOrUnssigned((DmTag)val), (f, ft) => MatchCategoryFilterCore(f, ft));
        }

        private FilterResult MatchAutorFilter(DmFile file) {
            if(!AllowHierarchicallyKeywordFilters)
                return MatchFilterCore(file, AutorsHeaderFilter, (f, val) => f.ContainsAutorOrUnssigned((DmTag)val), null);
            return MatchAutorFilterCore(file, AutorsHeaderFilter);
        }

        private FilterResult MatchAutorFilterCore(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.ContainsAutorOrUnssigned((DmTag)val), (f, ft) => MatchAutorFilterCore(f, ft));
        }

        private FilterResult MatchPeopleFilter(DmFile file) {
            if(!AllowHierarchicallyKeywordFilters)
                return MatchFilterCore(file, PeoplesHeaderFilter, (f, val) => f.ContainsPeopleOrUnssigned((DmTag)val), null);
            return MatchPeopleFilterCore(file, PeoplesHeaderFilter);
        }

        private FilterResult MatchPeopleFilterCore(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.ContainsPeopleOrUnssigned((DmTag)val), (f, ft) => MatchPeopleFilterCore(f, ft));
        }

        private FilterResult MatchImportDateTimeFilter(DmFile file) {
            return MatchFilterCore(file, ImportDateHeaderFilter, (f, val) => f.ImportDate.Year == (int)val, (f, ft) => MatchImportMonthFilter(f, ft));
        }

        private FilterResult MatchImportMonthFilter(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.ImportDate.Month == (int)val, (f, ft) => MatchImportDayFilter(f, ft));
        }

        private FilterResult MatchImportDayFilter(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.ImportDate.Day == (int)val, null);
        }

        private FilterResult MatchCreationDateTimeFilter(DmFile file) {
            return MatchFilterCore(file, CreationDateHeaderFilter, (f, val) => f.CreationDate.Year == (int)val, (f, ft) => MatchCreationMonthFilter(f, ft));
        }

        private FilterResult MatchCreationMonthFilter(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.CreationDate.Month == (int)val, (f, ft) => MatchCreationDayFilter(f, ft));
        }

        private FilterResult MatchCreationDayFilter(DmFile file, Filter filter) {
            return MatchFilterCore(file, filter, (f, val) => f.CreationDate.Day == (int)val, null);
        }

        private FilterResult MatchColorLabelFilter(DmFile file) {
            return MatchFilterCore(file, ColorLabelHeaderFilter, (f, val) => f.ColorLabel == val, null);
        }

        private FilterResult MatchRatingFilter(DmFile file) {
            return MatchFilterCore(file, RatingHeaderFilter, (f, val) => f.Rating == (int)val, null);
        }

        protected virtual FilterResult MatchMediaFormatTypeFilter(DmFile file) {
            return MatchFilterCore(file, MediaFormatHeaderFilter, (f, val) => ((int)f.MediaFormat.Type) == ((int)val), (f, ft) => MatchMediaFormatFilter(ft, f));
        }

        protected virtual FilterResult MatchMediaFormatFilter(Filter mediaTypeFilter, DmFile file) {
            return MatchFilterCore(file, mediaTypeFilter, (f, val) => f.MediaFormat == val, null);
        }

        protected virtual FilterResult MatchFolderFilter(DmFile file) {
            return MatchFilterCore(file, FolderHeaderFilter, (f, val) => object.Equals(f.Folder, val), null);
        }

        private FilterResult CalcTotalRes(FilterResult totalRes, FilterResult res, FilterOperationType rootFilterType) {
            if(totalRes == FilterResult.False)
                return totalRes;
            if(res == FilterResult.True)
                return FilterResult.True;
            if(res == FilterResult.False) {
                if(rootFilterType == FilterOperationType.Or) {
                    totalRes = totalRes == FilterResult.True ? totalRes : FilterResult.False;
                } else {
                    totalRes = FilterResult.False;
                }
            }
            return totalRes;
        }

        protected virtual FilterResult MatchFilterCore(DmFile file, Filter parentFilter, Func<DmFile, object, bool> matchFilter, Func<DmFile, Filter, FilterResult> childFilterMatch) {
            if(!parentFilter.HasActiveChildren)
                return FilterResult.None;
            FilterResult parentResult = FilterResult.None;
            foreach(Filter child in parentFilter.ActiveChildren) {
                if(child.IsActive || child.HasActiveChildren) {
                    parentResult = matchFilter(file, child.Value) ? FilterResult.True : FilterResult.False;
                    if(parentResult == FilterResult.False) {
                        if(parentFilter.OperationType != FilterOperationType.Or)
                            return FilterResult.False;
                    }
                }
                FilterResult childResult = childFilterMatch != null ? childFilterMatch(file, child) : FilterResult.None;
                if(childResult == FilterResult.False) {
                    if(parentFilter.OperationType != FilterOperationType.Or)
                        return FilterResult.False;
                } else if(childResult == FilterResult.True) {
                    if(parentResult == FilterResult.True)
                        return FilterResult.True;
                } else {
                    if(parentResult == FilterResult.True)
                        return FilterResult.True;
                }
            }
            return FilterResult.False;
        }

        protected virtual FilterResult MatchSavedSearchesFilter(DmFile file) {
            Filter savedSearches = LastImportedFilter.Parent;
            if(!savedSearches.HasActiveChildren)
                return FilterResult.None;
            if(LastImportedFilter.IsActive) {
                if(file.ImportIndex != Properties.ImportIndex) {
                    if(savedSearches.OperationType != FilterOperationType.Or)
                        return FilterResult.False;
                } else return FilterResult.True;
            }
            if(ImportedTodayFilter.IsActive) {
                if(file.ImportDate != DateTime.Now.Date) {
                    if(savedSearches.OperationType != FilterOperationType.Or)
                        return FilterResult.False;
                } else return FilterResult.True;
            }
            if(ImportedYesterdayFilter.IsActive) {
                if(file.ImportDate != DateTime.Now.Date.AddDays(-1)) {
                    if(savedSearches.OperationType != FilterOperationType.Or)
                        return FilterResult.False;
                } else return FilterResult.True;
            }
            if(ImportedLastWeekFilter.IsActive) {
                DateTime startDate = GetLastWeekStartDate();
                if(file.ImportDate < startDate || file.ImportDate > startDate.AddDays(7)) {
                    if(savedSearches.OperationType != FilterOperationType.Or)
                        return FilterResult.False;
                } else return FilterResult.True;
            }
            if(ImportedLastMonthFilter.IsActive) {
                if(file.ImportDate.Year != DateTime.Now.Year || file.ImportDate.Month != DateTime.Now.Month) {
                    if(savedSearches.OperationType != FilterOperationType.Or)
                        return FilterResult.False;
                } else return FilterResult.True;
            }
            return FilterResult.False;
        }

        private List<Filter> GetActiveFilters() {
            if(Context == null)
                return null;
            var res = from filter in Context.Filters
                      where filter.IsActive
                      select filter;
            return res.ToList();
        }

        protected virtual bool ShouldSkipFilter(Filter filter) {
            while(filter.Parent != null) {
                if(filter.Parent.IsActive)
                    return true;
                filter = filter.Parent;
            }
            return false;
        }

        private bool FileMatchLastImported(DmFile file) {
            return file.ImportIndex == Properties.ImportIndex;
        }
        int LockRaiseFilterValuesChangedCount { get; set; }
        protected void BeginUpdateFilters() {
            LockRaiseFilterValuesChangedCount++;
            Context.Configuration.AutoDetectChangesEnabled = false;
        }
        protected void EndUpdateFilters() {
            Context.Configuration.AutoDetectChangesEnabled = true;
            if(LockRaiseFilterValuesChangedCount > 0)
                LockRaiseFilterValuesChangedCount--;
            if(LockRaiseFilterValuesChangedCount == 0)
                RaiseFilterValuesChanged();
        }
        public virtual void UpdateDateTimeFilters() {
            BeginUpdateFilters();
            try {
                ImportedTodayFilter.MatchedCount = 0;
                ImportedYesterdayFilter.MatchedCount = 0;
                ImportedLastWeekFilter.MatchedCount = 0;
                ImportedLastMonthFilter.MatchedCount = 0;

                foreach(DmFile file in Context.Files) {
                    UpdateFilterByImportedToday(ImportedTodayFilter, file, null, FileOperation.Add);
                    UpdateFilterByImportedYesterday(ImportedYesterdayFilter, file, null, FileOperation.Add);
                    UpdateFilterByImportedLastWeek(ImportedLastWeekFilter, file, null, FileOperation.Add);
                    UpdateFilterByImportedLastMonth(ImportedLastMonthFilter, file, null, FileOperation.Add);
                }
            }
            finally {
                EndUpdateFilters();
            }
        }
        public virtual void UpdateFiltersCore() {
            BeginUpdateFilters();
            try {
                ClearFilterValues();

                foreach(DmFile file in Context.Files) {
                    UpdateFiltersByFile(file, null, FileOperation.Add);
                }
                BuildFilterParentChildRelations();
            }
            finally {
                EndUpdateFilters();
            }
        }
        public virtual void UpdateFilters() {
            BeginUpdateFilters();
            try {
                ClearFilterValues();

                foreach(DmFile file in Context.Files) {
                    UpdateFiltersByFile(file, null, FileOperation.Add);
                }
                Context.SaveChanges();
                BuildFilterParentChildRelations();
            } finally {
                EndUpdateFilters();
            }
        }

        public void UpdateFiltersByFile(DmFile file, DmFile prevState, FileOperation operation) {
            UpdateFilterByLastImported(LastImportedFilter, file, prevState, operation);
            UpdateFilterByImportedToday(ImportedTodayFilter, file, prevState, operation);
            UpdateFilterByImportedYesterday(ImportedYesterdayFilter, file, prevState, operation);
            UpdateFilterByImportedLastWeek(ImportedLastWeekFilter, file, prevState, operation);
            UpdateFilterByImportedLastMonth(ImportedLastMonthFilter, file, prevState, operation);
            UpdateFilterByFolder(file, prevState, operation);
            UpdateFilterByProject(file, prevState, operation);
            UpdateFilterByPlace(file, prevState, operation);
            UpdateFilterByColorLabel(file, prevState, operation);
            UpdateFilterByMark(file, prevState, operation);
            UpdateFilterByReject(file, prevState, operation);
            UpdateFilterByMediaFormat(file, prevState, operation);
            UpdateFilterByCreationDate(file, prevState, operation);
            UpdateFilterByImportDate(file, prevState, operation);
            UpdateFilterByRating(file, prevState, operation);
            UpdateFilterByEvent(file, prevState, operation);
            UpdateFilterByPeoples(file, prevState, operation);
            UpdateFilterByTags(file, prevState, operation);
            UpdateFilterByAutors(file, prevState, operation);
            UpdateFilterByGenres(file, prevState, operation);
            UpdateFilterByCollections(file, prevState, operation);
            UpdateFilterByCategories(file, prevState, operation);
        }

        private void UpdateFilterByCategories(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Add) {
                IncrementFilterCategories(file);
                return;
            }
            if(operation == FileOperation.Update) {
                DecrementFilterCategories(prevState);
                IncrementFilterCategories(file);
                RemoveZeroMatchFilters(CategoriesHeaderFilter);
                return;
            }
            if(operation == FileOperation.Remove) {
                DecrementFilterCategories(file);
                RemoveZeroMatchFilters(CategoriesHeaderFilter);
                return;
            }
        }

        private void UpdateFilterByCollections(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Add) {
                IncrementFilterCollections(file);
                return;
            }
            if(operation == FileOperation.Update) {
                DecrementFilterCollections(prevState);
                IncrementFilterCollections(file);
                RemoveZeroMatchFilters(CollectionsHeaderFilter);
                return;
            }
            if(operation == FileOperation.Remove) {
                DecrementFilterCollections(file);
                RemoveZeroMatchFilters(CollectionsHeaderFilter);
                return;
            }
        }

        private void UpdateFilterByGenres(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Add) {
                IncrementFilterGenres(file);
                return;
            }
            if(operation == FileOperation.Update) {
                DecrementFilterGenres(prevState);
                IncrementFilterGenres(file);
                RemoveZeroMatchFilters(GenresHeaderFilter);
                return;
            }
            if(operation == FileOperation.Remove) {
                DecrementFilterGenres(file);
                RemoveZeroMatchFilters(GenresHeaderFilter);
                return;
            }
        }

        private void UpdateFilterByTags(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Add) {
                IncrementFilterKeywords(file);
                return;
            }
            if(operation == FileOperation.Update) {
                DecrementFilterKeywords(prevState);
                IncrementFilterKeywords(file);
                RemoveZeroMatchFilters(TagsHeaderFilter);
                return;
            }
            if(operation == FileOperation.Remove) {
                DecrementFilterKeywords(file);
                RemoveZeroMatchFilters(TagsHeaderFilter);
                return;
            }
        }

        private void UpdateFilterByAutors(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Add) {
                IncrementFilterAutors(file);
                return;
            }
            if(operation == FileOperation.Update) {
                DecrementFilterAutors(prevState);
                IncrementFilterAutors(file);
                RemoveZeroMatchFilters(AutorsHeaderFilter);
                return;
            }
            if(operation == FileOperation.Remove) {
                DecrementFilterAutors(file);
                RemoveZeroMatchFilters(AutorsHeaderFilter);
                return;
            }
        }

        private void UpdateFilterByPeoples(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Add) {
                IncrementFilterPeoples(file);
                return;
            }
            if(operation == FileOperation.Update) {
                DecrementFilterPeoples(prevState);
                IncrementFilterPeoples(file);
                RemoveZeroMatchFilters(PeoplesHeaderFilter);
                return;
            }
            if(operation == FileOperation.Remove) {
                DecrementFilterPeoples(file);
                RemoveZeroMatchFilters(PeoplesHeaderFilter);
                return;
            }
        }

        private void RemoveZeroMatchFilters(Filter header) {
            for(int i = 0; i < header.Children.Count; ) {
                RemoveZeroMatchFilters(header.Children[i]);
                if(!header.Children[i].IsSystem &&
                    header.Children[i].MatchedCount == 0 &&
                    !header.Children[i].HasChildWithNonZeroMatchCount()) {
                    RemoveFilter(header.Children[i]);
                } else
                    i++;
            }
        }

        private void IncrementFilterPeoples(DmFile file) {
            if(file.HasPeoplesTag)
                IncrementFilter(PeoplesHeaderFilter, FilterType.People, file, file.Peoples);
            else
                IncrementFilter(PeoplesHeaderFilter, FilterType.People, null);
        }

        private void DecrementFilterPeoples(DmFile file) {
            if(file.HasPeoplesTag)
                DecrementFilter(PeoplesHeaderFilter, FilterType.People, file.Peoples);
            else
                DecrementFilter(PeoplesHeaderFilter, (object)null, false);
        }

        private void IncrementFilterAutors(DmFile file) {
            if(file.HasAutorsTag)
                IncrementFilter(AutorsHeaderFilter, FilterType.Autor, file, file.Autors);
            else
                IncrementFilter(AutorsHeaderFilter, FilterType.Autor, null);
        }

        private void DecrementFilterAutors(DmFile file) {
            if(file.HasAutorsTag)
                DecrementFilter(AutorsHeaderFilter, FilterType.Autor, file.Autors);
            else
                DecrementFilter(AutorsHeaderFilter, (object)null, false);
        }

        private void IncrementFilterCategories(DmFile file) {
            if(file.HasCategoriesTag)
                IncrementFilter(CategoriesHeaderFilter, FilterType.Category, file, file.Categories);
            else
                IncrementFilter(CategoriesHeaderFilter, FilterType.Category, null);
        }

        private void DecrementFilterCategories(DmFile file) {
            if(file.HasCategoriesTag)
                DecrementFilter(CategoriesHeaderFilter, FilterType.Category, file.Categories);
            else
                DecrementFilter(CategoriesHeaderFilter, (object)null, false);
        }

        private void IncrementFilterCollections(DmFile file) {
            if(file.HasCollectionsTag)
                IncrementFilter(CollectionsHeaderFilter, FilterType.Collection, file, file.Collections);
            else
                IncrementFilter(CollectionsHeaderFilter, FilterType.Collection, null);
        }

        private void DecrementFilterCollections(DmFile file) {
            if(file.HasCollectionsTag)
                DecrementFilter(CollectionsHeaderFilter, FilterType.Collection, file.Collections);
            else
                DecrementFilter(CollectionsHeaderFilter, (object)null, false);
        }

        private void IncrementFilterGenres(DmFile file) {
            if(file.HasGenresTag)
                IncrementFilter(GenresHeaderFilter, FilterType.Genre, file, file.Genres);
            else
                IncrementFilter(GenresHeaderFilter, FilterType.Genre, null);
        }

        private void DecrementFilterGenres(DmFile file) {
            if(file.HasGenresTag)
                DecrementFilter(GenresHeaderFilter, FilterType.Genre, file.Genres);
            else
                DecrementFilter(GenresHeaderFilter, (object)null, false);
        }

        private void IncrementFilterKeywords(DmFile file) {
            if(file.HasKeywordsTag)
                IncrementFilter(TagsHeaderFilter, FilterType.Tag, file, file.Keywords);
            else
                IncrementFilter(TagsHeaderFilter, FilterType.Tag, null);
        }

        private void DecrementFilterKeywords(DmFile file) {
            if(file.HasKeywordsTag)
                DecrementFilter(TagsHeaderFilter, FilterType.Tag, file.Keywords);
            else
                DecrementFilter(TagsHeaderFilter, (object)null, false);
        }

        private void IncrementFilter(Filter parentFilter, FilterType type, DmFile file, IEnumerable<IDmKeyword> values) {
            if(values == null) {
                IncrementFilter(parentFilter, type, (object)null);
                return;
            }
            bool hasValue = false;
            foreach(IDmKeyword key in values) {
                hasValue = true;
                IncrementFilterTag(parentFilter, type, file, key.Tag);
            }
            if(!hasValue) {
                IncrementFilter(parentFilter, type, (object)null);
            }
        }

        private void DecrementFilter(Filter parentFilter, FilterType type, IEnumerable<IDmKeyword> values) {
            if(values == null) {
                Filter unassigned = GetFilter(parentFilter, (object)null);
                if(unassigned != null)
                    unassigned.MatchedCount--;
                return;
            }
            foreach(IDmKeyword key in values) {
                DmTag tag = key.RemovedTag != null ? (DmTag)key.RemovedTag : key.Tag;
                DecrementFilter(parentFilter, type, tag, false);
            }
        }

        private void DecrementTagFilterFlat(Filter parentFilter, FilterType type, DmTag tag, bool removeZeroMatchCount) {
            Filter filter = GetFilter(parentFilter, tag);
            if(filter == null)
                return;
            filter.MatchedCount--;
            if(filter.MatchedCount <= 0 && removeZeroMatchCount && !filter.IsSystem)
                RemoveFilter(filter);
        }

        private void DecrementFilter(Filter parentFilter, FilterType type, DmTag tag, bool removeZeroMatchCount) {
            if(!AllowHierarchicallyKeywordFilters)
                DecrementTagFilterFlat(parentFilter, type, tag, removeZeroMatchCount);
            else
                DecrementTagFilterHierarchical(parentFilter, type, tag, removeZeroMatchCount);
        }

        protected bool ShouldRemoveFilter(Filter filter) {
            return filter.MatchedCount == 0 && !filter.IsSystem && !filter.HasChildWithNonZeroMatchCount();
        }

        private void DecrementTagFilterHierarchical(Filter parentFilter, FilterType type, DmTag tag, bool removeZeroMatchCount) {
            IEnumerable<DmTagNodeReversed> nodes = GetTagNodeReversedList(tag);
            foreach(DmTagNodeReversed node in nodes) {
                List<DmTagNodeReversed> path = node.GetPath();
                for(int i = 0; i < path.Count; i++) {
                    Filter filter = GetFilter(parentFilter, type, path, i, false);
                    if(filter != null && filter.MatchedCount > 0)
                        filter.MatchedCount--;
                    if(removeZeroMatchCount && ShouldRemoveFilter(filter)) {
                        RemoveFilter(filter);
                    }
                }
            }
        }

        private Filter GetFilter(Filter root, FilterType type, List<DmTagNodeReversed> path, int endIndex, bool createIfNotExist) {
            Filter filter = root;
            for(int i = endIndex; i >= 0; i--) {
                filter = GetFilter(filter, path[i].Tag, type, createIfNotExist);
                if(filter == null)
                    return null;
            }
            return filter;
        }

        private void UpdateFilterByEvent(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Add)
                IncrementFilter(EventsHeaderFilter, FilterType.Event, file.Event);
            else if(operation == FileOperation.Remove)
                DecrementFilter(EventsHeaderFilter, file.Event, true);
            else if(operation == FileOperation.Update && file.Event != prevState.Event) {
                DecrementFilter(EventsHeaderFilter, prevState.Event, true);
                IncrementFilter(EventsHeaderFilter, FilterType.Event, file.Event);
            }
        }

        private void UpdateFilterByMark(DmFile file, DmFile prevState, FileOperation operation) {
            UpdateFilterByMark(file, prevState == null ? false : prevState.Marked, operation);
        }

        private void UpdateFilterByReject(DmFile file, DmFile prevState, FileOperation operation) {
            UpdateFilterByRejected(file, prevState == null ? false : prevState.Rejected, operation);
        }

        private void UpdateFilterByRejected(DmFile file, bool prev, FileOperation operation) {
            if(operation == FileOperation.Add)
                IncrementFilter(RejectHeaderFilter, FilterType.Rejected, file.Rejected);
            else if(operation == FileOperation.Remove)
                DecrementFilter(RejectHeaderFilter, file.Rejected, false);
            else if(operation == FileOperation.Update) {
                DecrementFilter(RejectHeaderFilter, prev, false);
                IncrementFilter(RejectHeaderFilter, FilterType.Rejected, file.Rejected);
            }
        }

        private void UpdateFilterByMark(DmFile file, bool prev, FileOperation operation) {
            if(operation == FileOperation.Add)
                IncrementFilter(MarkHeaderFilter, FilterType.Marked, file.Marked);
            else if(operation == FileOperation.Remove)
                DecrementFilter(MarkHeaderFilter, file.Marked, false);
            else if(operation == FileOperation.Update) {
                DecrementFilter(MarkHeaderFilter, prev, false);
                IncrementFilter(MarkHeaderFilter, FilterType.Marked, file.Marked);
            }
        }

        private void UpdateFilterByRating(DmFile file, int prevRating, FileOperation operation) {
            if(operation == FileOperation.Add) {
                IncrementFilter(RatingHeaderFilter, FilterType.Rating, file.Rating);
            }
            if(operation == FileOperation.Update) {
                DecrementFilter(RatingHeaderFilter, prevRating, false);
                IncrementFilter(RatingHeaderFilter, FilterType.Rating, file.Rating);
            }
            if(operation == FileOperation.Remove) {
                DecrementFilter(RatingHeaderFilter, file.Rating, false);
            }
        }

        private void UpdateFilterByRating(DmFile file, DmFile prevState, FileOperation operation) {
            UpdateFilterByRating(file, prevState == null ? 0 : prevState.Rating, operation);
        }

        private void UpdateFilterByImportDate(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Update)
                return;
            if(operation == FileOperation.Add) {
                IncrementFilter(ImportDateHeaderFilter,
                    new FilterType[] { FilterType.ImportYear, FilterType.ImportMonth, FilterType.ImportDay },
                    new object[] { null, null, null },
                    new object[] { file.ImportDate.Year, file.ImportDate.Month, file.ImportDate.Day });
            } else {
                DecrementFilter(ImportDateHeaderFilter,
                    new object[] { file.ImportDate.Year, file.ImportDate.Month, file.ImportDate.Day }, true);
            }
        }

        protected virtual Filter CreateFilter(FilterType type, object value, Filter parent) {
            Filter res = new Filter(type, parent.Id);
            res.IsSystem = false;
            res.Value = value;
            res.Parent = parent;
            Context.Filters.Add(res);
            return res;
        }

        public void RotateFile(DmFile file, int degrees) {
            file.DeltaAngle = degrees;
            file.RotateAngle += degrees;
            file.RotateAngle = file.RotateAngle % 360;
            Properties.ShouldCalcAspectRatio = true;
        }

        protected virtual Filter GetOrCreateFilter(FilterType type, object value, Filter parent) {
            Filter res = GetFilter(parent, type, value);
            if(res == null)
                res = CreateFilter(type, value, parent);
            return res;
        }

        private Filter IncrementFilter(Filter parentFilter, FilterType type, object value) {
            return IncrementFilter(parentFilter, type, value, true);
        }

        private Filter IncrementFilter(Filter parentFilter, FilterType type, object value, bool increment) {
            Filter filter = GetOrCreateFilter(type, value, parentFilter);
            if(increment)
                filter.MatchedCount++;
            return filter;
        }

        private Filter IncrementFilterTag(Filter parentFilter, FilterType type, DmFile file, DmTag tag) {
            Filter filter = null;
            if(!AllowHierarchicallyKeywordFilters) {
                filter = IncrementFilter(parentFilter, type, tag, file.ContainsTag(tag, tag.Type));
            } else {
                IEnumerable<DmTagNodeReversed> nodes = GetTagNodeReversedList(tag);
                foreach(DmTagNodeReversed node in nodes) {
                    List<DmTagNodeReversed> path = node.GetPath();
                    for(int i = 0; i < path.Count; i++) {
                        Filter nodeFilter = GetFilter(parentFilter, type, path, i, true);
                        nodeFilter.MatchedCount++;
                        if(nodeFilter.Value == tag)
                            filter = nodeFilter;
                    }
                }
            }
            return filter;
        }

        private void UpdateFilterByCreationDate(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Add) {
                IncrementFilter(CreationDateHeaderFilter,
                    new FilterType[] { FilterType.CreationYear, FilterType.CreationMonth, FilterType.CreationDay },
                    new object[] { null, null, null },
                    new object[] { file.CreationDate.Year, file.CreationDate.Month, file.CreationDate.Day });
            } else if(operation == FileOperation.Update) {
                if(prevState.CreationDate == file.CreationDate)
                    return;

                DecrementFilter(CreationDateHeaderFilter,
                    new object[] { prevState.CreationDate.Year, prevState.CreationDate.Month, prevState.CreationDate.Day },
                    new object[] { file.CreationDate.Year, file.CreationDate.Month, file.CreationDate.Day }, true);
                IncrementFilter(CreationDateHeaderFilter,
                    new FilterType[] { FilterType.CreationYear, FilterType.CreationMonth, FilterType.CreationDay },
                    new object[] { prevState.CreationDate.Year, prevState.CreationDate.Month, prevState.CreationDate.Day },
                    new object[] { file.CreationDate.Year, file.CreationDate.Month, file.CreationDate.Day });
            } else {
                DecrementFilter(CreationDateHeaderFilter,
                    new object[] { file.CreationDate.Year, file.CreationDate.Month, file.CreationDate.Day }, true);
            }
        }

        private void UpdateFilterByProject(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Add) {
                IncrementFilter(ProjectHeaderFilter,
                    new FilterType[] { FilterType.Client, FilterType.Project, FilterType.Scene },
                    new object[] { null, null, null },
                    new object[] { file.Client, file.Project, file.Scene });
            } else if(operation == FileOperation.Update) {
                if(prevState.Client == file.Client &&
                    prevState.Project == file.Project &&
                    prevState.Scene == file.Scene)
                    return;

                DecrementFilter(ProjectHeaderFilter,
                    new object[] { prevState.Client, prevState.Project, prevState.Scene },
                    new object[] { file.Client, file.Client, file.Scene }, true);
                IncrementFilter(ProjectHeaderFilter,
                    new FilterType[] { FilterType.Client, FilterType.Project, FilterType.Scene },
                    new object[] { prevState.Client, prevState.Project, prevState.Scene },
                    new object[] { file.Client, file.Project, file.Scene });
            } else {
                DecrementFilter(ProjectHeaderFilter,
                    new object[] { file.Client, file.Project, file.Scene }, true);
            }
        }

        private void RemoveFilter(Filter filter) {
            RemoveFilterCore(filter);
            filter.Parent = null;
        }
        private void RemoveFilterCore(Filter filter) {
            while(filter.Children.Count > 0) {
                Filter child = filter.Children[0];
                RemoveFilterCore(child);
                filter.Children.Remove(child);
            }
            Context.Filters.Remove(filter);
        }

        private bool DecrementFilter(Filter filter, bool shouldRemoveWhenZeroMatchCount) {
            if(filter == null)
                return false;
            filter.MatchedCount--;
            if(filter.MatchedCount <= 0 && shouldRemoveWhenZeroMatchCount) {
                RemoveFilter(filter);
                return true;
            }
            return false;
        }

        private void UpdateFilterByMediaFormat(DmFile file, DmFile prevState, FileOperation operation) {
            if(file.MediaFormat == null) return;
            if(operation == FileOperation.Add) {
                Filter filterFormat = GetFilter(MediaFormatHeaderFilter, new object[] { (int)file.MediaFormat.Type, file.MediaFormat });
                filterFormat.MatchedCount++;
                filterFormat.Parent.MatchedCount++;
            } else if(operation == FileOperation.Update) {
                Filter filterFormat = GetFilter(MediaFormatHeaderFilter, new object[] { (int)prevState.MediaFormat.Type, prevState.MediaFormat });
                filterFormat.MatchedCount--;
                filterFormat.Parent.MatchedCount--;

                filterFormat = GetFilter(MediaFormatHeaderFilter, new object[] { (int)file.MediaFormat.Type, file.MediaFormat });
                filterFormat.MatchedCount++;
                filterFormat.Parent.MatchedCount++;
            } else {
                Filter filterFormat = GetFilter(MediaFormatHeaderFilter, new object[] { (int)file.MediaFormat.Type, file.MediaFormat });
                filterFormat.MatchedCount--;
                filterFormat.Parent.MatchedCount--;
            }
        }

        private void UpdateFilterByColorLabel(DmFile file, DmFile prevState, FileOperation operation) {
            UpdateFilterByColorLabel(file, prevState == null ? null : prevState.ColorLabel, operation);
        }

        private void UpdateFilterByColorLabel(DmFile file, DmColorLabel prevLabel, FileOperation operation) {
            if(operation == FileOperation.Add) {
                IncrementFilter(ColorLabelHeaderFilter, FilterType.ColorLabel, file.ColorLabel);
                return;
            }
            if(operation == FileOperation.Update) {
                if(prevLabel == file.ColorLabel)
                    return;
                DecrementFilter(ColorLabelHeaderFilter, prevLabel, false);
                IncrementFilter(ColorLabelHeaderFilter, FilterType.ColorLabel, file.ColorLabel);
            }
            if(operation == FileOperation.Remove) {
                DecrementFilter(ColorLabelHeaderFilter, file.ColorLabel, false);
            }
        }

        private void UpdateFilterByFolder(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Update)
                return;
            if(operation == FileOperation.Remove)
                DecrementFilter(FolderHeaderFilter, file.Folder, true);
            if(operation == FileOperation.Add)
                IncrementFilter(FolderHeaderFilter, FilterType.Folder, file.Folder);
        }

        private void DecrementFilter(Filter parentFilter, object value, bool removeIfMatchedCountZero) {
            Filter filter = GetFilter(parentFilter, value);
            if(filter != null)
                filter.MatchedCount--;
            if(filter.MatchedCount <= 0 && removeIfMatchedCountZero && !filter.IsSystem) {
                RemoveFilter(filter);
            }
        }

        protected void DecrementFilter(FilterType type, object value, bool removeIfMatchedCountZero) {
            Filter filter = GetFilter(type, value);
            if(filter != null)
                filter.MatchedCount--;
            if(filter.MatchedCount <= 0 && removeIfMatchedCountZero && !filter.IsSystem) {
                RemoveFilter(filter);
            }
        }

        private void RemoveFilterWithZeroMatchedCount(Filter filter, bool recursive) {
            while(filter.MatchedCount <= 0) {
                Filter parent = filter.Parent;
                if(!filter.IsSystem)
                    Context.Filters.Remove(filter);
                if(!recursive || parent == null || parent.IsHeader)
                    break;
                parent.MatchedCount--;
                filter = parent;
            }
        }

        private void UpdateFilterByPlace(DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Add) {
                IncrementFilter(PlaceFilter,
                    new FilterType[] { FilterType.Country, FilterType.State, FilterType.City, FilterType.Location },
                    new object[] { null, null, null, null },
                    new object[] { file.Country, file.State, file.City, file.Location });

                return;
            }

            if(operation == FileOperation.Remove) {
                DecrementFilter(PlaceFilter,
                     new object[] { file.Country, file.State, file.City, file.Location }, true);

                return;
            }
            if(operation == FileOperation.Update) {
                if(prevState.EqualsPlace(file))
                    return;
                DecrementFilter(PlaceFilter,
                    new object[] { prevState.Country, prevState.State, prevState.City, prevState.Location },
                    new object[] { file.Country, file.State, file.City, file.Location }, true);
                IncrementFilter(PlaceFilter,
                    new FilterType[] { FilterType.Country, FilterType.State, FilterType.City, FilterType.Location },
                    new object[] { prevState.Country, prevState.State, prevState.City, prevState.Location },
                    new object[] { file.Country, file.State, file.City, file.Location });
            }
        }

        protected void DecrementFilter(Filter parentFilter, object[] prevValues, object[] values, bool removeWhenZeroMatchCount) {
            Filter filter = parentFilter;
            int decrement = 0;
            for(int i = 0; i < values.Length; i++) {
                if(filter == null)
                    break;
                filter = GetFilter(filter, prevValues[i]);
                if(!object.Equals(prevValues[i], values[i]))
                    decrement = 1;
                if(filter != null && decrement > 0) {
                    bool removed = DecrementFilter(filter, removeWhenZeroMatchCount);
                    if(removed)
                        break;
                }
            }
        }

        protected void DecrementFilter(FilterType rootType, object[] values, bool removeWhenZeroMatchCount) {
            DecrementFilter(GetFilter(rootType), values, removeWhenZeroMatchCount);
        }

        protected void DecrementFilter(Filter parentFilter, object[] values, bool removeWhenZeroMatchCount) {
            Filter filter = parentFilter;
            for(int i = 0; i < values.Length; i++) {
                filter = GetFilter(filter, values[i]);
                if(filter != null) {
                    bool removed = DecrementFilter(filter, removeWhenZeroMatchCount);
                    if(removed)
                        break;
                }
            }
        }

        protected void IncrementFilter(FilterType rootType, FilterType[] types, object[] prevValues, object[] values) {
            IncrementFilter(GetFilter(rootType), types, prevValues, values);
        }


        protected void IncrementFilter(Filter filter, FilterType[] types, object[] prevValues, object[] values) {
            int increment = 0;
            for(int i = 0; i < values.Length; i++) {
                filter = GetOrCreateFilter(types[i], values[i], filter);
                if(!object.Equals(prevValues[i], values[i]))
                    increment = 1;
                filter.MatchedCount += increment;
            }
        }

        private Filter GetFilter(Filter filter, object[] values) {
            if(filter == null)
                return null;
            for(int i = 0; i < values.Length; i++) {
                Filter childFilter = filter.Children.FirstOrDefault((child) => object.Equals(child.Value, values[i]));
                if(childFilter == null)
                    break;
                filter = childFilter;
            }
            return filter;
        }

        private Filter GetFilter(FilterType filterType, object[] values) {
            Filter filter = GetFilter(filterType, values[0]);
            if(filter == null)
                return null;
            for(int i = 1; i < values.Length; i++) {
                Filter childFilter = filter.Children.FirstOrDefault((child) => object.Equals(child.Value, values[i]));
                if(childFilter == null)
                    break;
                filter = childFilter;
            }
            return filter;
        }

        public Filter GetFilter(FilterType filterType, object value) {
            return Context.Filters.Local.FirstOrDefault(
                (filter) => filter.Type == filterType && object.Equals(filter.Value, value)
                );
        }

        private void UpdateFilterByImportedLastMonth(Filter filter, DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Update)
                return;
            if(operation == FileOperation.Add)
                filter.MatchedCount++;
            else if(file.ImportDate.Year == DateTime.Now.Year && file.ImportDate.Month == DateTime.Now.Month)
                filter.MatchedCount--;
        }

        private void UpdateFilterByImportedLastWeek(Filter filter, DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Update)
                return;
            if(operation == FileOperation.Add) {
                filter.MatchedCount++;
                return;
            }

            DateTime start = GetLastWeekStartDate();
            DateTime end = start.AddDays(7);

            if(file.ImportDate.Date >= start && file.ImportDate.Date <= end)
                filter.MatchedCount--;
        }

        private void UpdateFilterByImportedYesterday(Filter filter, DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Update)
                return;
            DateTime yesterday = DateTime.Now.Date.AddDays(-1);
            if(operation == FileOperation.Add &&
                file.ImportDate.Year == yesterday.Year && file.ImportDate.Month == yesterday.Month && file.ImportDate.Day == yesterday.Day)
                filter.MatchedCount++;
            else if(file.ImportDate.Date == yesterday)
                filter.MatchedCount--;
        }

        private void UpdateFilterByImportedToday(Filter filter, DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Update)
                return;
            if(operation == FileOperation.Add &&
                file.ImportDate.Year == DateTime.Now.Year && file.ImportDate.Month == DateTime.Now.Month && file.ImportDate.Day == DateTime.Now.Day)
                filter.MatchedCount++;
            else if(file.ImportDate.Date == DateTime.Now.Date)
                filter.MatchedCount--;
        }

        private void UpdateFilterByLastImported(Filter filter, DmFile file, DmFile prevState, FileOperation operation) {
            if(operation == FileOperation.Update)
                return;
            if(operation == FileOperation.Add)
                filter.MatchedCount++;
            else if(file.ImportIndex == Properties.ImportIndex)
                filter.MatchedCount--;
        }

        protected void RaiseFilterValuesChanged() {
            if(LockRaiseFilterValuesChangedCount > 0)
                return;
            if(FilterValuesChanged != null) FilterValuesChanged(this, EventArgs.Empty);
        }

        private string GetFilterTextByType(FilterType type, object value) {
            return value.ToString();
        }

        protected DateTime GetLastWeekStartDate() {
            return DateTime.Now.AddDays(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Now.DayOfWeek).Date;
        }

        [Browsable(false)]
        public DmContext Context { get; set; }

        public bool OpenDataSource(string dataSource) {
            return ConnectToDataSource(dataSource, false);
        }

        public bool OpenDataSource(string dataSource, bool failIfMissing) {
            return ConnectToDataSource(dataSource, failIfMissing);
        }

        public bool CreateDataSource(string dataSource) {
            return ConnectToDataSource(dataSource, false);
        }



        //public bool UseInMemoryDatabase { get; set; }
        public string ConnectToDataSource() {
            //if(UseInMemoryDatabase) {
            //    SettingsStore.Default.CurrentDataSource = ":memory:";
            //}

            if(string.IsNullOrEmpty(SettingsStore.Default.CurrentDataSource)) {
                if(!CreateDefaultDataSource()) {
                    return MessageStrings.CannotCreateDataSource;
                }
                //SettingsStore.Default.CurrentDataSource = SettingsStore.Default.DefaultDataSourcePath;
            } else {
                if(!OpenDataSource(SettingsStore.Default.CurrentDataSource)) {
                    return MessageStrings.CannotOpenDataSource;
                }
            }

            Context.Configuration.AutoDetectChangesEnabled = false;
            LoadVolumes();
            LoadFiles();
            InitializeTagNodes();
            InitializeFilters();
            CalcAspectRatio();
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.ChangeTracker.DetectChanges();
            return string.Empty;
        }

        public void LoadVolumes() {
            Context.Volumes.Load();
        }

        private void InitializeTagNodes() {
            Context.Tags.Load();
            Context.TagNodes.Load();
            Context.TagNodesReversed.Load();
            if(Context.TagNodes.Local.Count > 0 && Context.TagNodesReversed.Local.Count == 0) {
                TagNodeReverseCalculator.ReverseTagNodesTree(Context);
                Context.TagNodesReversed.Load();
            }
        }

        public void CalcAspectRatio() {
            if(float.IsNaN(Properties.AspectRatio))
                Properties.AspectRatio = 1.0f;
            if(!Properties.ShouldCalcAspectRatio)
                return;
            Properties.ShouldCalcAspectRatio = false;
            int count = Context.Files.Local.Count;
            if(count == 0) {
                return;
            }
            int[] aspectArrayCount = new int[(int)((10.0f - 0.0f) / 0.05f)];
            foreach(DmFile file in Context.Files.Local) {
                int index = (int)(file.AspectRatio / 0.05f);
                aspectArrayCount[index]++;
            }
            float aspectRatio = 0.0f;
            for(int i = 0; i < aspectArrayCount.Length; i++) {
                aspectRatio += (0.05f * (i + 1) * aspectArrayCount[i]) / count;
            }
            Properties.AspectRatio = aspectRatio;
        }

        private void LoadFiles() {
            Context.Files.Load();
        }

        private void InitializeFilters() {
            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Filters.Load();
            Context.ColorLabels.Load();
            Context.MediaFormat.Load();
            BuildFilterParentChildRelations();
            Filter colorLabel = GetFilter(FilterType.ColorLabelHeader);
            foreach(Filter filter in colorLabel.Children) {
                if(filter.ValueId == Guid.Empty)
                    filter.Text = DmColorLabel.NoneString;
                else
                    filter.Value = Context.ColorLabels.Local.FirstOrDefault((c) => c.Id == filter.ValueId);
            }
            Filter mediaHeader = GetFilter(FilterType.MediaFormatHeader);
            foreach(Filter mediaType in mediaHeader.Children) {
                foreach(Filter mediaFormat in mediaType.Children) {
                    mediaFormat.Value = Context.MediaFormat.Local.FirstOrDefault((m) => m.Id == mediaFormat.ValueId);
                }
            }
            Filter header = GetFilter(FilterType.PeopleHeader);
            foreach(Filter filter in header.Children)
                filter.Value = Context.Tags.FirstOrDefault((t) => t.Id == filter.ValueId);
            header = GetFilter(FilterType.TagsHeader);
            foreach(Filter filter in header.Children)
                filter.Value = Context.Tags.FirstOrDefault((t) => t.Id == filter.ValueId);
            header = GetFilter(FilterType.CategoriesHeader);
            foreach(Filter filter in header.Children)
                filter.Value = Context.Tags.FirstOrDefault((t) => t.Id == filter.ValueId);
            header = GetFilter(FilterType.CollectionsHeader);
            foreach(Filter filter in header.Children)
                filter.Value = Context.Tags.FirstOrDefault((t) => t.Id == filter.ValueId);
            header = GetFilter(FilterType.AutorsHeader);
            foreach(Filter filter in header.Children)
                filter.Value = Context.Tags.FirstOrDefault((t) => t.Id == filter.ValueId);
            header = GetFilter(FilterType.GenresHeader);
            foreach(Filter filter in header.Children)
                filter.Value = Context.Tags.FirstOrDefault((t) => t.Id == filter.ValueId);
            Context.Configuration.AutoDetectChangesEnabled = true;
            UpdateFiltersCore();
            //UpdateDateTimeFilters();
        }

        public void CloseConnection() {
            if(Context == null)
                return;

            Context.Database.Connection.Close();
            Context.Dispose();
            Context = null;
        }

        protected bool ConnectToDataSource(string dataSource, bool failIfMissing) {

            Database.SetInitializer<DmContext>(new DmContextInitializer());

            if(Context != null) {
                Context.Dispose();
                Context = null;
            }

            //if(!UseInMemoryDatabase) {
                string directory = Path.GetDirectoryName(dataSource);
                if(!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
            //}

            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = dataSource;
            sb.FailIfMissing = failIfMissing;
            sb.Version = 3;
            var connection = CreateConnection(dataSource, failIfMissing);
            Context = new DmContext(connection);
            RaiseDatabaseConnected();
            return true;
        }
        static DbConnection CreateConnection(string filePath, bool failIfMissing) {
            try {
                if (File.Exists(filePath)) {
                    var attributes = File.GetAttributes(filePath);
                    if (attributes.HasFlag(FileAttributes.ReadOnly)) {
                        File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                    }
                }
            } catch { }

            return new SQLiteConnection {
                ConnectionString = new SQLiteConnectionStringBuilder { DataSource = filePath, FailIfMissing = failIfMissing, Version = 3}.ConnectionString
            };
        }
        protected virtual void RaiseDatabaseConnected() {
            if(DatabaseConnected != null)
                DatabaseConnected(this, EventArgs.Empty);
        }

        public bool CreateDefaultDataSource() {
            return CreateDataSource(SettingsStore.Default.DefaultDataSourcePath);
        }

        public Version GetDataSourceVersion() {
            return new Version();
        }

        public void CheckUpgradeDataSourceVersion() {
            if(SettingsStore.Default.CurrentVersion == GetDataSourceVersion())
                return;
            UpgradeDataSourceVersion();
        }

        public void UpgradeDataSourceVersion() {
        }

        public void DisconnectDataSource() {
            Context.Dispose();
            Context = null;
        }

        public object GetFilterRootId() {
            return Guid.Empty;
        }

        DbPropertiesModel PropertiesCore { get; set; }
        public DbPropertiesModel Properties {
            get {
                if(Context == null)
                    return null;
                if(PropertiesCore == null)
                    PropertiesCore = Context.Properties.FirstOrDefault();
                return PropertiesCore;
            }
        }

        [Browsable(false)]
        public DmFile FileBeforeUpdate { get; set; }
        public void BeginUpdateFile(DmFile fileInfo) {
            FileBeforeUpdate = fileInfo == null ? null : fileInfo.Clone();
        }

        public void EndUpdateFile(DmFile fileInfo) {
            Context.Configuration.AutoDetectChangesEnabled = false;
            UpdateFiltersByFile(fileInfo, FileBeforeUpdate, FileOperation.Update);
            OnFilePropertyChangedCore();
        }

        public void OnFileRejectedChanged(DmFile file, bool rejected) {
            Context.Configuration.AutoDetectChangesEnabled = false;
            bool prevRejected = file.Rejected;
            file.Rejected = rejected;
            UpdateFilterByRejected(file, prevRejected, FileOperation.Update);
            OnFilePropertyChangedCore();
        }

        public void OnFileMarkChanged(DmFile file, bool marked) {
            Context.Configuration.AutoDetectChangesEnabled = false;
            bool prevMarked = file.Marked;
            file.Marked = marked;
            UpdateFilterByMark(file, prevMarked, FileOperation.Update);
            OnFilePropertyChangedCore();
        }
        public void OnFileMarkChanged(List<DmFile> files, bool marked) {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach(DmFile file in files) {
                bool prevMarked = file.Marked;
                file.Marked = marked;
                UpdateFilterByMark(file, prevMarked, FileOperation.Update);
            }
            OnFilePropertyChangedCore();
        }
        public void OnFileRatingChanged(DmFile file, int prevRating) {
            Context.Configuration.AutoDetectChangesEnabled = false;
            UpdateFilterByRating(file, prevRating, FileOperation.Update);
            OnFilePropertyChangedCore();
        }
        public void OnFileColorLabelChanged(DmFile file, DmColorLabel prevLabel) {
            Context.Configuration.AutoDetectChangesEnabled = false;
            UpdateFilterByColorLabel(file, prevLabel, FileOperation.Update);
            OnFilePropertyChangedCore();
        }
        public void OnFileColorLabelChanged(List<DmFile> files, DmColorLabel newLabel) {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach(DmFile file in files) {
                DmColorLabel prevLabel = file.ColorLabel;
                file.ColorLabel = newLabel;
                UpdateFilterByColorLabel(file, prevLabel, FileOperation.Update);
            }
            OnFilePropertyChangedCore();
        }
        public void OnFileRejectedChanged(List<DmFile> files, bool rejected) {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach(DmFile file in files) {
                bool prevRejected = file.Rejected;
                file.Rejected = rejected;
                UpdateFilterByRejected(file, prevRejected, FileOperation.Update);
            }
            OnFilePropertyChangedCore();
        }

        public void OnFileRatingChanged(List<DmFile> files, int rating) {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach(DmFile file in files) {
                int prevRating = file.Rating;
                file.Rating = rating;
                UpdateFilterByRating(file, prevRating, FileOperation.Update);
            }
            OnFilePropertyChangedCore();
        }

        protected virtual void OnFilePropertyChangedCore() {
            if(UpdateCount > 0)
                return;
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.ChangeTracker.DetectChanges();
            Context.SaveChanges();

            if(FilesRowDataChanged != null)
                FilesRowDataChanged(this, EventArgs.Empty);
        }

        List<MediaFormat> mediaFormatList;
        public IEnumerable<MediaFormat> GetMediaFormats() {
            if(Context == null)
                return null;
            if(mediaFormatList == null)
                mediaFormatList = Context.MediaFormat.Local.ToList();
            return mediaFormatList;
        }

        public void UpdateFilterState(Filter filterModel) {
            Context.SaveChanges();
            if(FilterStateChanged != null)
                FilterStateChanged(this, EventArgs.Empty);
        }

        public void ClearForcedFilterValues() {
            foreach(Filter filter in Context.Filters) {
                filter.ForcedOperationType = FilterOperationType.None;
            }
        }

        public Filter GetParentForFilter(Filter filter) {
            if(filter.IsHeader)
                return null;
            return filter.Parent;
        }

        public Filter GetHeaderForFilter(Filter filter) {
            while(filter != null && !filter.IsHeader)
                filter = filter.Parent;
            return filter;
        }

        public void SetHeaderFilterTypeForFilter(Filter filter, bool filterCheckedWithControl) {
            Filter header = GetHeaderForFilter(filter);
            header.OperationType = filterCheckedWithControl ? FilterOperationType.Or : FilterOperationType.And;
        }

        public void UpdateFiltersState(Filter selectedFilter, bool isActive) {
            selectedFilter.IsActive = isActive;
            Filter header = GetHeaderForFilter(selectedFilter);
            foreach(Filter filter in Context.Filters) {
                if(filter == selectedFilter) continue;
                Filter h = GetHeaderForFilter(filter);
                if(h == header && header.OperationType != FilterOperationType.Or)
                    filter.IsActive = false;
            }
            Context.SaveChanges();
        }

        public void GroupFiles(List<DmFile> files) {
            if(files == null || files.Count < 2)
                return;
            DmFile groupOwner = files[0];
            groupOwner.IsGroupOwner = true;
            groupOwner.IsExpanded = false;
            for(int i = 1; i < files.Count; i++) {
                files[i].GroupOwner = groupOwner;
            }
            Context.SaveChanges();
        }

        public void UngroupFiles(List<DmFile> files) {
            foreach(DmFile file in files) {
                UngroupFile(file);
            }
            Context.SaveChanges();
        }

        private void UngroupFile(DmFile groupOwner) {
            if(!groupOwner.IsGroupOwner)
                return;
            groupOwner.IsGroupOwner = false;
            var res = from file in Context.Files.Local
                      where file.GroupId == groupOwner.Id
                      select file;
            foreach(DmFile file in res) {
                file.GroupOwner = null;
            }
        }

        public void ToggleGroupExpand(List<DmFile> files) {
            foreach(DmFile file in files)
                ToggleGroupExpand(file);
            Context.SaveChanges();
        }

        public void ExpandGroup(List<DmFile> files) {
            foreach(DmFile file in files)
                SetGroupExpanded(file, true);
            Context.SaveChanges();
        }

        public void CollapseGroup(List<DmFile> files) {
            foreach(DmFile file in files)
                SetGroupExpanded(file, false);
            Context.SaveChanges();
        }

        private void SetGroupExpanded(DmFile file, bool isExpanded) {
            file.IsExpanded = isExpanded;
        }

        private void ToggleGroupExpand(DmFile groupOwner) {
            if(!groupOwner.IsGroupOwner)
                return;
            var res = from file in Context.Files.Local
                      where file.GroupId == groupOwner.Id
                      select file;
            groupOwner.IsExpanded = !groupOwner.IsExpanded;
            foreach(DmFile file in res) {
                file.IsExpanded = groupOwner.IsExpanded;
            }
        }

        public DmTag AddTag(string tagText, TagType type) {
            DmTag tag = new DmTag() { Value = tagText, Type = type };
            Context.Tags.Add(tag);

            AddTagNode(tag);
            AddTagNodeReversed(tag);

            if (!IsLockUpdate)
                Context.SaveChanges();
            return tag;
        }

        private void AddTagNodeReversed(DmTag tag) {
            DmTagNodeReversed node = new DmTagNodeReversed() { Tag = tag, Type = tag.Type };
            Context.TagNodesReversed.Add(node);
        }

        private void AddTagNode(DmTag tag) {
            DmTagNode node = new DmTagNode() { Tag = tag, Type = tag.Type };
            Context.TagNodes.Add(node);
        }

        public void AddKeyword(DmFile file, DmTag tag, TagType tagType) { AddKeyword(file, tag, tagType, true); }

        public void AddKeyword(DmFile file, DmTag tag, TagType tagType, bool raiseOnTagAdded) {
            if(file.ContainsTag(tag, tagType))
                return;
            UpdateAddedTagProperties(tag);
            IDmKeyword tagReference = CreateKeyword(tag, tagType);
            tagReference.File = file;
            Context.AddTag(tagReference);
            if(raiseOnTagAdded)
                file.OnTagAdded(tagType);
        }

        public void AddKeywords(DmFile file, IEnumerable<DmTag> tags, TagType tagType) {
            foreach(DmTag tag in tags) {
                AddKeyword(file, tag, tagType, false);
            }
            file.OnTagAdded(tagType);
        }

        void UpdateAddedTagProperties(DmTag tag) {
            tag.AddCount++;
            tag.TimeStamp = DateTime.Now.Ticks;
        }

        private IDmKeyword CreateKeyword(DmTag tag, TagType tagType) {
            IDmKeyword res = null;
            switch(tagType) {
                case TagType.Autor:
                    res = new DmAutor();
                    break;
                case TagType.Category:
                    res = new DmCategory();
                    break;
                case TagType.Genre:
                    res = new DmGenre();
                    break;
                case TagType.People:
                    res = new DmPeople();
                    break;
                case TagType.Tag:
                    res = new DmKeyword();
                    break;
                case TagType.Collection:
                    res = new DmCollection();
                    break;
            }
            res.Tag = tag;
            return res;
        }

        public void RemoveKeywords(DmFile file, List<DmTag> tags, TagType tagType) {
            foreach(DmTag tag in tags) {
                file.RemoveTag(this, tag, tagType);
            }
        }

        public void RemoveKeyword(DmFile file, DmTag tag, TagType tagType) {
            file.RemoveTag(this, tag, tagType);
        }

        int updateCount = 0;
        protected int UpdateCount {
            get { return updateCount; }
            set { updateCount = Math.Max(0, value); }
        }

        public void BeginUpdate() {
            UpdateCount++;
        }

        public void EndUpdate() {
            UpdateCount--;
            if(UpdateCount == 0)
                Context.SaveChanges();
        }

        public void CancelUpdate() {
            UpdateCount--;
        }

        protected bool IsLockUpdate { get { return UpdateCount > 0; } }

        public void RemoveFile(DmFile file) {
            UpdateFiltersByFile(file, null, FileOperation.Remove);
            if(file.IsGroupOwner && !file.IsExpanded) {
                ToggleGroupExpand(file);
                UngroupFile(file);
            }
            RemoveKeywords(file);
            Context.Files.Remove(file);
            if(!IsLockUpdate)
                Context.SaveChanges();
            Properties.ShouldCalcAspectRatio = true;
        }

        private void RemoveKeywords(DmFile file) {
            RemoveKeywords(file, file.Autors);
            RemoveKeywords(file, file.Categories);
            RemoveKeywords(file, file.Collections);
            RemoveKeywords(file, file.Genres);
            RemoveKeywords(file, file.Keywords);
            RemoveKeywords(file, file.Peoples);
        }

        private void RemoveKeywords(DmFile file, ICollection<DmPeople> collection) {
            if(collection == null)
                return;
            List<DmPeople> list = collection.ToList();
            foreach(DmPeople item in list) {
                Context.Peoples.Remove(item);
            }
        }

        private void RemoveKeywords(DmFile file, ICollection<DmKeyword> collection) {
            if(collection == null)
                return;
            List<DmKeyword> list = collection.ToList();
            foreach(DmKeyword item in list) {
                Context.Keywords.Remove(item);
            }
        }

        private void RemoveKeywords(DmFile file, ICollection<DmGenre> collection) {
            if(collection == null)
                return;
            List<DmGenre> list = collection.ToList();
            foreach(DmGenre item in list) {
                Context.Genres.Remove(item);
            }
        }

        private void RemoveKeywords(DmFile file, ICollection<DmCollection> collection) {
            if(collection == null)
                return;
            List<DmCollection> list = collection.ToList();
            foreach(DmCollection item in list) {
                Context.Collections.Remove(item);
            }
        }

        private void RemoveKeywords(DmFile file, ICollection<DmCategory> collection) {
            if(collection == null)
                return;
            List<DmCategory> list = collection.ToList();
            foreach(DmCategory item in list) {
                Context.Categories.Remove(item);
            }
        }

        private void RemoveKeywords(DmFile file, ICollection<DmAutor> collection) {
            if(collection == null)
                return;
            List<DmAutor> list = collection.ToList();
            foreach(DmAutor item in list) {
                Context.Autors.Remove(item);
            }
        }

        public void RemoveFiles(List<DmFile> items) {
            foreach(DmFile file in items) {
                RemoveFile(file);
            }
            if(!IsLockUpdate)
                Context.SaveChanges();
        }

        public CriteriaOperator ApplyFiltration() {
            return null;
        }

        public void ClearAllTags() {
            Context.Configuration.AutoDetectChangesEnabled = false;
            try {
                SilentClearTags(TagType.People);
                SilentClearTags(TagType.Collection);
                SilentClearTags(TagType.Category);
                SilentClearTags(TagType.Autor);
                SilentClearTags(TagType.Genre);
                SilentClearTags(TagType.Tag);
            } finally {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public void SilentClearTags(TagType tagType) {
            switch(tagType) {
                case TagType.Tag:
                    SilentClearTags(Context.Tags);
                    break;
            }
        }

        private void SilentClearTags(DbSet<DmTag> tags) {
            tags.Local.ToList().ForEach(t => Context.Entry(t).State = EntityState.Detached);
        }

        public DmTag GetTag(string value) {
            return GetTag(value, TagType.Tag);
        }

        public DmTag GetTag(string value, TagType type) {
            return Context.Tags.Local.FirstOrDefault((t) => t.Value == value && t.Type == type);
        }

        public void AddChildTag(DmTag parent, DmTag child) {
            DmTagNode parentNode = GetTagNode(parent);
            DmTagNode childNode = GetTagNode(child);
            childNode.Parent = parentNode;
            DmTagNodeReversed revParentNode = GetTagNodeReversed(child);
            DmTagNodeReversed revChildNode = GetTagNodeReversed(parent);
            revChildNode.Parent = revParentNode;
            if(!IsLockUpdate)
                Context.SaveChanges();
        }

        public DmTagNode GetTagNode(DmTag tag) {
            try {
                Context.Configuration.AutoDetectChangesEnabled = false;
                return Context.TagNodes.Local.FirstOrDefault(t => t.Tag == tag);
            } finally {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public IEnumerable<DmTagNode> GetTagNodeList(DmTag tag) {
            try {
                Context.Configuration.AutoDetectChangesEnabled = false;
                return Context.TagNodes.Local.Where(t => t.Tag == tag);
            } finally {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public IEnumerable<DmTagNode> GetTagNodeList(TagType type) {
            try {
                Context.Configuration.AutoDetectChangesEnabled = false;
                return Context.TagNodes.Local.Where(t => t.Type == type).ToList();
            } finally {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public IEnumerable<DmTag> GetTagList(TagType type) {
            try {
                Context.Configuration.AutoDetectChangesEnabled = false;
                return Context.Tags.Local.Where(t => t.Type == type).ToList();
            } finally {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public DmTagNodeReversed GetTagNodeReversed(DmTag tag) {
            try {
                Context.Configuration.AutoDetectChangesEnabled = false;
                return Context.TagNodesReversed.Local.FirstOrDefault(t => t.Tag == tag);
            } finally {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public IEnumerable<DmTagNodeReversed> GetTagNodeReversedList(DmTag tag) {
            try {
                Context.Configuration.AutoDetectChangesEnabled = false;
                return Context.TagNodesReversed.Local.Where(t => t.Tag == tag).ToList();
            } finally {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public IEnumerable<DmTagNodeReversed> GetSuggestedTags(DmTag tag) {
            List<DmTagNodeReversed> res = new List<DmTagNodeReversed>();
            DmTagNodeReversed tagNode = GetTagNodeReversed(tag);
            FillSuggestedTags(tagNode.Children, res);
            return res;
        }

        private void FillSuggestedTags(ICollection<DmTagNodeReversed> nodes, List<DmTagNodeReversed> suggestList) {
            if(nodes == null)
                return;
            foreach(DmTagNodeReversed node in nodes) {
                suggestList.Add(node);
                FillSuggestedTags(node.Children, suggestList);
            }
        }

        public DmColorLabel GetColorLabel(string text) {
            try {
                Context.Configuration.AutoDetectChangesEnabled = false;
                return Context.ColorLabels.Local.FirstOrDefault(l => string.Equals(l.Text, text));
            } finally {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public List<DmFile> GetMarkedItems() {
            try {
                Context.Configuration.AutoDetectChangesEnabled = false;
                return Context.Files.Local.Where(f => f.Marked).ToList();
            } finally {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public object GetLabeledItems(DmColorLabel label) {
            try {
                Context.Configuration.AutoDetectChangesEnabled = false;
                return Context.Files.Local.Where(f => f.ColorLabel == label).ToList();
            } finally {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }
    }

    public enum FilterResult { None, False, True }

    public class DmModelHelpers {
        public DmModelHelpers(DmModel model) {
            Model = model;
        }
        public DmModel Model { get; private set; }
        AddFileHelper addFileHelper;
        public AddFileHelper AddFileHelper {
            get {
                if(addFileHelper == null)
                    addFileHelper = new AddFileHelper(Model);
                return addFileHelper;
            }
        }
        FileSizeHelper fileSizeHelper;
        public FileSizeHelper FileSizeHelper {
            get {
                if(fileSizeHelper == null)
                    fileSizeHelper = new FileSizeHelper(Model);
                return fileSizeHelper;
            }
        }

        MediaFormatHelper mediaFormatHelper;
        public MediaFormatHelper MediaFormatHelper {
            get {
                if(mediaFormatHelper == null)
                    mediaFormatHelper = new MediaFormatHelper(Model);
                return mediaFormatHelper;
            }
        }
    }
}
