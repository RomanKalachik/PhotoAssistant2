using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core.Model {
    public class DmContextInitializer : CreateDatabaseIfNotExists<DmContext> {

        protected override void Seed(DmContext context) {
            base.Seed(context);

            InitializeProperties(context);
            InitializeDefaultTags(context);
            InitializeColorLabels(context);
            InitializeMediaFormats(context);
            InitializeFilterValues(context);

            context.SaveChanges();
        }

        private void InitializeProperties(DmContext context) {
            context.Properties.Add(new DbPropertiesModel());
        }

        private void InitializeMediaFormats(DmContext context) {
            context.MediaFormat.Add(new MediaFormat() { Text = MediaFormat.BmpString, Extension = MediaFormat.BmpFormatString, Type = MediaType.Image });
            context.MediaFormat.Add(new MediaFormat() { Text = MediaFormat.JpgString, Extension = MediaFormat.JpgFormatString, Type = MediaType.Image });
            context.MediaFormat.Add(new MediaFormat() { Text = MediaFormat.JpegString, Extension = MediaFormat.JpegFormatString, Type = MediaType.Image });
            context.MediaFormat.Add(new MediaFormat() { Text = MediaFormat.PngString, Extension = MediaFormat.PngFormatString, Type = MediaType.Image });
            context.MediaFormat.Add(new MediaFormat() { Text = MediaFormat.TgaString, Extension = MediaFormat.TgaFormatString, Type = MediaType.Image });
            context.MediaFormat.Add(new MediaFormat() { Text = MediaFormat.TiffString, Extension = MediaFormat.TiffFormatString, Type = MediaType.Image });
            context.MediaFormat.Add(new MediaFormat() { Text = MediaFormat.Cr2String, Extension = MediaFormat.Cr2FormatString, Type = MediaType.Image });
        }

        private void InitializeColorLabels(DmContext context) {
            context.ColorLabels.Add(new DmColorLabel() { Color = DmColorLabel.Red, Text = DmColorLabel.RedString });
            context.ColorLabels.Add(new DmColorLabel() { Color = DmColorLabel.Orange, Text = DmColorLabel.OrangeString });
            context.ColorLabels.Add(new DmColorLabel() { Color = DmColorLabel.Yellow, Text = DmColorLabel.YellowString });
            context.ColorLabels.Add(new DmColorLabel() { Color = DmColorLabel.Green, Text = DmColorLabel.GreenString });
            context.ColorLabels.Add(new DmColorLabel() { Color = DmColorLabel.Cyan, Text = DmColorLabel.CyanString });
            context.ColorLabels.Add(new DmColorLabel() { Color = DmColorLabel.Blue, Text = DmColorLabel.BlueString });
            context.ColorLabels.Add(new DmColorLabel() { Color = DmColorLabel.Pink, Text = DmColorLabel.PinkString });
        }

        Dictionary<int, Guid> tagId;
        protected Dictionary<int, Guid> TagId {
            get {
                if(tagId == null)
                    tagId = new Dictionary<int, Guid>();
                return tagId;
            }
        }

        protected Guid GetTagGuidForId(int id) {
            if(!TagId.ContainsKey(id))
                TagId.Add(id, Guid.NewGuid());
            return TagId[id];
        }

        Dictionary<string, DmTag> tagText;
        protected Dictionary<string, DmTag> TagText {
            get {
                if(tagText == null)
                    tagText = new Dictionary<string, DmTag>();
                return tagText;
            }
        }

        protected DmTag GetTagByString(string tagText) {
            if(TagText.ContainsKey(tagText))
                return TagText[tagText];
            return null;
        }

        protected virtual void InitializeDefaultTags(DmContext context) {
            if(!DmModel.AllowGenerateDefaultTags)
                return;
            TagNodeDataSource source = new TagNodeDataSource();
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream stream = asm.GetManifestResourceStream("PhotoAssistant.Core.Resources.DefaultTags.xml");
            source.LoadDataFromStream(stream);

            InitializeTags(context, source);
            InitializeTagNodes(source);
        }

        private static void InitializeTagNodes(TagNodeDataSource source) {
            foreach(TagNode node in source.Nodes) {
                if(node.ParentId == -1)
                    continue;
                TagNode parent = source.Nodes.FirstOrDefault(n => n.Id == node.ParentId);
                node.Node.Parent = parent.Node;
            }
        }

        private void InitializeTags(DmContext context, TagNodeDataSource source) {
            foreach(TagNode node in source.Nodes) {
                DmTag tag = GetOrCreateTag(context, node);
                node.Tag = tag;
                DmTagNode tagNode = CreateNode(node);
                context.TagNodes.Add(tagNode);
            }
        }

        private DmTag GetOrCreateTag(DmContext context, TagNode node) {
            DmTag tag = GetTagByString(node.Value);
            if(tag == null) {
                tag = new DmTag() { Id = GetTagGuidForId(node.Id), Value = node.Value, Color = node.Color };
                TagText.Add(tag.Value, tag);
                context.Tags.Add(tag);
            }
            return tag;
        }

        private DmTagNode CreateNode(TagNode node) {
            DmTagNode dn = new DmTagNode() { Id = GetTagGuidForId(node.Id), Tag = node.Tag, Type = TagType.Tag };
            if(node.ParentId != -1)
                dn.ParentId = GetTagGuidForId(node.ParentId);
            node.Node = dn;
            return dn;
        }

        protected virtual void InitializeFilterValues(DmContext context) {
            Filter savedSearches = new Filter(FilterType.SavedSearches, Guid.Empty);
            Filter lastImported = new Filter(FilterType.LastImported, savedSearches.Id);
            Filter importedToday = new Filter(FilterType.ImportedToday, savedSearches.Id);
            Filter importedYesterday = new Filter(FilterType.ImportedYesterday, savedSearches.Id);
            Filter importedLastWeek = new Filter(FilterType.ImportedLastWeek, savedSearches.Id);
            Filter importedLastMonth = new Filter(FilterType.ImportedLastMonth, savedSearches.Id);

            Filter folders = new Filter(FilterType.FolderHeader, Guid.Empty);

            Filter mediaFormat = new Filter(FilterType.MediaFormatHeader, Guid.Empty);
            Filter mark = new Filter(FilterType.Mark, Guid.Empty);
            Filter marked = new Filter(FilterType.Marked, mark.Id, true) { Text = Filter.MarkedString };
            Filter unmarked = new Filter(FilterType.Marked, mark.Id, false) { Text = Filter.UnmarkedString };
            Filter reject = new Filter(FilterType.Reject, Guid.Empty);
            Filter rejected = new Filter(FilterType.Rejected, reject.Id, true) { Text = Filter.RejectedString };
            Filter notRejected = new Filter(FilterType.Rejected, reject.Id, false) { Text = Filter.NotRejectedString };

            Filter rating = new Filter(FilterType.RatingHeader, Guid.Empty);

            Filter colorLabel = new Filter(FilterType.ColorLabelHeader, Guid.Empty);

            Filter creationDateTime = new Filter(FilterType.CreationDateTimeHeader, Guid.Empty);

            Filter importDateTime = new Filter(FilterType.ImportDateTimeHeader, Guid.Empty);

            Filter people = new Filter(FilterType.PeopleHeader, Guid.Empty);
            Filter peopleUnassigned = new Filter(FilterType.People, people.Id);
            peopleUnassigned.IsSystem = true;

            Filter events = new Filter(FilterType.EventHeader, Guid.Empty);
            Filter eventsUnassigned = new Filter(FilterType.Event, events.Id) { Index = Filter.MaxIndex };
            eventsUnassigned.Value = string.Empty;
            eventsUnassigned.IsSystem = true;

            Filter place = new Filter(FilterType.Place, Guid.Empty);

            Filter tags = new Filter(FilterType.TagsHeader, Guid.Empty);
            Filter tagUnassigned = new Filter(FilterType.Tag, tags.Id);
            tagUnassigned.IsSystem = true;

            Filter categories = new Filter(FilterType.CategoriesHeader, Guid.Empty);
            Filter categoryUnassigned = new Filter(FilterType.Category, categories.Id);
            categoryUnassigned.IsSystem = true;

            Filter genres = new Filter(FilterType.GenresHeader, Guid.Empty);
            Filter genreUnassigned = new Filter(FilterType.Genre, genres.Id);
            genreUnassigned.IsSystem = true;

            Filter collections = new Filter(FilterType.CollectionsHeader, Guid.Empty);
            Filter collectionUnassigned = new Filter(FilterType.Collection, collections.Id);
            collectionUnassigned.IsSystem = true;

            Filter autors = new Filter(FilterType.AutorsHeader, Guid.Empty);
            Filter autorUnassigned = new Filter(FilterType.Autor, autors.Id);
            autorUnassigned.IsSystem = true;

            Filter project = new Filter(FilterType.ProjectHeader, Guid.Empty);

            context.Filters.Add(savedSearches);
            context.Filters.Add(lastImported);
            context.Filters.Add(importedToday);
            context.Filters.Add(importedYesterday);
            context.Filters.Add(importedLastWeek);
            context.Filters.Add(importedLastMonth);
            context.Filters.Add(folders);
            context.Filters.Add(mediaFormat);
            context.Filters.Add(mark);
            context.Filters.Add(marked);
            context.Filters.Add(unmarked);
            context.Filters.Add(reject);
            context.Filters.Add(rejected);
            context.Filters.Add(notRejected);
            context.Filters.Add(rating);
            context.Filters.Add(colorLabel);
            context.Filters.Add(creationDateTime);
            context.Filters.Add(importDateTime);
            context.Filters.Add(people);
            context.Filters.Add(peopleUnassigned);
            context.Filters.Add(place);
            context.Filters.Add(events);
            context.Filters.Add(eventsUnassigned);
            context.Filters.Add(tags);
            context.Filters.Add(tagUnassigned);
            context.Filters.Add(categories);
            context.Filters.Add(categoryUnassigned);
            context.Filters.Add(genres);
            context.Filters.Add(genreUnassigned);
            context.Filters.Add(collections);
            context.Filters.Add(collectionUnassigned);
            context.Filters.Add(autors);
            context.Filters.Add(autorUnassigned);
            context.Filters.Add(project);

            InitializeColorLabelFilters(context, colorLabel);
            InitializeMediaFormatFilters(context, mediaFormat);
            InitializeRatingFilters(context, rating);
        }

        private void InitializeRatingFilters(DmContext context, Filter rating) {
            for(int i = 0; i < 6; i++) {
                Filter filter = new Filter(FilterType.Rating, rating.Id);
                filter.Value = i;
                context.Filters.Add(filter);
            }
        }

        private static void InitializeMediaFormatFilters(DmContext context, Filter mediaFormat) {
            List<MediaType> mediaTypes = new List<MediaType>();
            foreach(MediaFormat format in context.MediaFormat.Local) {
                if(!mediaTypes.Contains(format.Type)) {
                    Filter filter = new Filter(FilterType.MediaFormatType, mediaFormat.Id);
                    filter.Value = format.Type;
                    context.Filters.Add(filter);
                    mediaTypes.Add(format.Type);
                }
            }
            foreach(MediaFormat format in context.MediaFormat.Local) {
                Filter filterType = context.Filters.Local.FirstOrDefault((f) => object.Equals(f.Value, (int)format.Type));
                Filter filter = new Filter(FilterType.MediaFormat, filterType.Id);
                filter.Value = format;
                filter.Text = format.Text;
                context.Filters.Add(filter);
            }
        }

        protected virtual void InitializeColorLabelFilters(DmContext context, Filter colorLabel) {
            foreach(DmColorLabel label in context.ColorLabels.Local) {
                Filter filter = new Filter(FilterType.ColorLabel, colorLabel.Id);
                filter.Value = label;
                filter.Text = label.Text;
                context.Filters.Add(filter);
            }
            Filter noneColorLabel = new Filter(FilterType.ColorLabel, colorLabel.Id);
            noneColorLabel.Text = DmColorLabel.NoneString;
            context.Filters.Add(noneColorLabel);
        }
    }
}
