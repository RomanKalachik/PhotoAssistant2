using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DevExpress.Utils.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars.Ribbon;

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraBars.Navigation;
using PhotoAssistant.Core.Model;
using PhotoAssistant.UI.View.TokenEditTest;

namespace PhotoAssistant.UI.View {
    public partial class TagSelectionControl : XtraUserControl {
        public TagSelectionControl() {
            InitializeComponent();
            InitializeCategoriesCombo();
        }

        private void InitializeCategoriesCombo() {
            this.cbSelectCategories.Properties.Items.AddEnum<KeywordCategory>();
            this.cbSelectCategories.SelectedIndex = 0;
        }

        public void ApplyToAccordionControl(AccordionControlElement group) {
            group.Style = ElementStyle.Group;
            AccordionControlElement[] nodes = new AccordionControlElement[this.accordionControl1.Elements.Count];
            this.accordionControl1.Elements.CopyTo(nodes, 0);
            foreach(AccordionControlElement node in nodes) {
                node.ContentContainer.Name = "TagSelectionControl" + node.ContentContainer.Name;
                this.accordionControl1.Elements.Remove(node);
                group.Elements.Add(node);
                group.AccordionControl.Controls.Add(node.ContentContainer);
            }
        }

        DmModel model;
        public DmModel Model {
            get { return model; }
            set {
                if(Model == value)
                    return;
                model = value;
                OnModelChanged();
            }
        }

        private void OnModelChanged() {
            InitializeAssignedKeywordsTokens();
            InitializeCategorizedTags();
        }

        private void InitializeCategorizedTags() {
            UpdateCategoriesSorting();
        }

        private void UpdateCategoriesSorting() {
            if(Model == null)
                return;
            KeywordCategory category = (KeywordCategory)((ImageComboBoxItem)this.cbSelectCategories.SelectedItem).Value;
            switch(category) {
                case KeywordCategory.Categorized:
                    this.tlCategories.ParentFieldName = "ParentId";
                    this.tlcText.FieldName = "Text";
                    this.tlAddCount.SortOrder = SortOrder.None;
                    this.tlcTimeStamp.SortOrder = SortOrder.None;
                    this.tlCategories.DataSource = Model.GetTagNodeList(TagType);
                    break;
                case KeywordCategory.MostAdded:
                    this.tlcText.FieldName = "Value";
                    this.tlCategories.ParentFieldName = "";
                    this.tlAddCount.SortOrder = SortOrder.Descending;
                    this.tlcTimeStamp.SortOrder = SortOrder.None;
                    this.tlCategories.DataSource = Model.GetTagList(TagType);
                    break;
                case KeywordCategory.RecentlyAdded:
                    this.tlcText.FieldName = "Value";
                    this.tlCategories.ParentFieldName = "";
                    this.tlAddCount.SortOrder = SortOrder.None;
                    this.tlcTimeStamp.SortOrder = SortOrder.Descending;
                    this.tlCategories.DataSource = Model.GetTagList(TagType);
                    break;
            }
            this.tlCategories.ExpandAll();
        }

        private void InitializeAssignedKeywordsTokens() {
            this.teAssignedKeywords.Properties.CustomDropDownControl = new TagTokenEditDropDownControl() { Model = Model, TagType = TagType };
            this.teAssignedKeywords.Properties.Tokens.Clear();
            IEnumerable<DmTag> allTags = Model.GetTagList(TagType);
            foreach(DmTag tag in allTags) {
                this.teAssignedKeywords.Properties.Tokens.Add(new TokenEditToken(tag.Value, tag));
            }
        }

        TagType tagType;
        public TagType TagType {
            get { return tagType; }
            set {
                if(TagType == value)
                    return;
                tagType = value;
                OnTagTypeChanged();
            }
        }

        private void OnTagTypeChanged() {
            TagTokenEditDropDownControl control = (TagTokenEditDropDownControl)this.teAssignedKeywords.Properties.CustomDropDownControl;
            if(control != null)
                control.TagType = TagType;
            InitializeCategorizedTags();
        }

        DmFile file;
        public DmFile File {
            get { return file; }
            set {
                if(File == value)
                    return;
                DmFile prev = File;
                file = value;
                //OnFileChanged(prev, file); TODO
            }
        }

        private void OnFileChanged(DmFile prev, DmFile current) {
            InitializeAddedKeywordsEdit(null);
        }

        private void InitializeAddedKeywordsEdit(DmTag selectTag) {
            this.teAssignedKeywords.Properties.BeginUpdate();
            this.teAssignedKeywords.Properties.EditValueType = TokenEditValueType.List;
            try {
                if(File == null)
                    return;
                List<DmTag> tags = File.GetTags(TagType);
                this.teAssignedKeywords.EditValue = tags;
                UpdateSuggestedKeywordsGallery();
            } finally {
                this.teAssignedKeywords.Properties.EndUpdate();
            }
        }

        private void UpdateSuggestedKeywordsGallery() {
            this.gcSuggestedKeywords.Gallery.BeginUpdate();
            try {
                this.gcSuggestedKeywords.Gallery.Groups.Clear();
                foreach(TokenEditToken token in this.teAssignedKeywords.Properties.SelectedItems) {
                    DmTag tag = (DmTag)token.Value;
                    GalleryItemGroup group = null;
                    IEnumerable<DmTagNodeReversed> suggestedTags = Model.GetSuggestedTags(tag);
                    foreach(DmTagNodeReversed suggested in suggestedTags) {
                        if(File.ContainsTag(suggested.Tag))
                            continue;
                        if(group == null) {
                            group = new GalleryItemGroup();
                            group.Caption = tag.Value;
                            group.Tag = tag;
                            gcSuggestedKeywords.Gallery.Groups.Add(group);
                        }
                        GalleryItem item = new GalleryItem() { Caption = suggested.Tag.Value, Tag = suggested.Tag };
                        group.Items.Add(item);
                    }
                }
            } finally {
                this.gcSuggestedKeywords.Gallery.EndUpdate();
            }
        }

        public void AddKeyword(string keywordText) {
            DmTag tag = Model.GetTag(keywordText, TagType);
            if(tag == null) {
                tag = Model.AddTag(keywordText, TagType);
            }
            Model.BeginUpdateFile(File);
            Model.AddKeyword(File, tag, TagType);
            Model.EndUpdateFile(File);
            InitializeAddedKeywordsEdit(tag);
        }

        private void galleryControlGallery1_ItemDoubleClick(object sender, GalleryItemClickEventArgs e) {
            DmTag tag = (DmTag)e.Item.Tag;
            if(File.ContainsTag(tag, TagType))
                return;
            Model.BeginUpdateFile(File);
            Model.AddKeyword(File, tag, TagType);
            Model.EndUpdateFile(File);
            InitializeAddedKeywordsEdit(tag);
        }

        private void btAddSelected_Click(object sender, EventArgs e) {
            AddTags(this.gcSuggestedKeywords.Gallery.GetCheckedItems());
        }

        protected virtual void AddTags(List<GalleryItem> items) {
            if(items == null || items.Count == 0)
                return;
            List<DmTag> tags = new List<DmTag>();
            foreach(GalleryItem item in items) {
                tags.Add((DmTag)item.Tag);
            }
            Model.BeginUpdateFile(File);
            Model.AddKeywords(File, tags, TagType);
            Model.EndUpdateFile(File);
            InitializeAddedKeywordsEdit(null);
        }

        private void btAddAll_Click(object sender, EventArgs e) {
            AddTags(this.gcSuggestedKeywords.Gallery.GetAllItems());
        }

        private void cbSelectCategories_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateCategoriesSorting();
        }

        private void tlCategories_DoubleClick(object sender, EventArgs e) {
            if(File == null)
                return;
            TreeListNode node = this.tlCategories.CalcHitInfo(this.tlCategories.PointToClient(Control.MousePosition)).Node;
            if(node == null)
                return;
            object item = this.tlCategories.GetDataRecordByNode(node);
            DmTag tag = item as DmTag;
            DmTagNode tnode = item as DmTagNode;
            if(tnode != null)
                tag = tnode.Tag;
            if(File.ContainsTag(tag, TagType)) {
                XtraMessageBox.Show("This tag has been already added.", SettingsStore.ApplicationName);
            }
            Model.AddKeyword(File, tag, TagType);
            InitializeAddedKeywordsEdit(tag);
        }

        private void teAssignedKeywords_Properties_TokenAdded(object sender, TokenEditTokenAddedEventArgs e) {
            DmTag tag = (DmTag)e.Token.Value;
            if(File.ContainsTag(tag))
                return;
            Model.AddKeyword(File, tag, TagType);
            UpdateSuggestedKeywordsGallery();
        }

        private void teAssignedKeywords_Properties_TokenRemoved(object sender, TokenEditTokenRemovedEventArgs e) {
            DmTag tag = (DmTag)e.Token.Value;
            Model.RemoveKeyword(File, tag, tagType);
            UpdateSuggestedKeywordsGallery();
        }
    }

    public enum KeywordCategory {
        [Description("Recently Added")]
        RecentlyAdded,
        [Description("Most Added")]
        MostAdded,
        [Description("Categorized")]
        Categorized,
    }
}
