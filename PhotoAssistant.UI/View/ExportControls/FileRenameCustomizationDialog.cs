using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.View.ExportControls {
    public partial class FileRenameCustomizationDialog : XtraForm {
        public FileRenameCustomizationDialog() {
            InitializeComponent();
            InitializeEditors();
        }

        private void InitializeEditors() {
            InitializeTemplates();
            InitializeKeywords();
            UpdateButtons();
        }

        private void InitializeKeywords() {
            this.gcKeywords.Gallery.BeginUpdate();
            try {
                this.gcKeywords.Gallery.Groups.Clear();
                this.gcKeywords.Gallery.Groups.Add(new GalleryItemGroup());
                foreach(FileRenameValueBase value in FileRenameManager.Default.FileRenameValues) {
                    this.gcKeywords.Gallery.Groups[0].Items.Add(CreateKeywordItem(value));
                }
            } finally {
                this.gcKeywords.Gallery.EndUpdate();
            }
        }

        private GalleryItem CreateKeywordItem(FileRenameValueBase value) {
            GalleryItem res = new GalleryItem() { Caption = value.Name };
            return res;
        }

        private void InitializeTemplates() {
            this.repositoryItemComboBox1.Items.BeginUpdate();
            try {
                this.repositoryItemComboBox1.Items.Clear();
                foreach(FileRenameTemplateInfo info in SettingsStore.Default.FileRenameTemplates) {
                    this.repositoryItemComboBox1.Items.Add(info);
                }
            } finally {
                this.repositoryItemComboBox1.Items.EndUpdate();
            }
            OnTemplateChanged();
        }

        FileRenameTemplateInfo template;
        public FileRenameTemplateInfo Template {
            get { return template; }
            set {
                if(Template == value)
                    return;
                template = value;
                OnTemplateChanged();
            }
        }

        private void OnTemplateChanged() {
            this.beTemplates.EditValue = Template;
            UpdateTemplateEditor();
        }

        private void UpdateTemplateEditor() {
            SuppressIsModified = true;
            try {
                if(Template == null)
                    this.meTemplateEditor.Text = "";
                else
                    this.meTemplateEditor.Text = Template.Template;
            } finally {
                SuppressIsModified = false;
            }
        }

        private void SelectItemByName(string name) {
            FileRenameTemplateInfo info = GetTemplateByName(name);
            this.beTemplates.EditValue = info;
        }

        private FileRenameTemplateInfo GetTemplateByName(string name) {
            foreach(FileRenameTemplateInfo info in this.repositoryItemComboBox1.Items) {
                if(info.Name == name) return info;
            }
            return null;
        }

        private void beTemplates_EditValueChanged(object sender, EventArgs e) {
            if(!IsModified) {
                biCopyFrom_ItemClick(this.biCopyFrom, new DevExpress.XtraBars.ItemClickEventArgs(this.biCopyFrom, this.biCopyFrom.Links[0]));
                TemplateName = ((FileRenameTemplateInfo)this.beTemplates.EditValue).Name;
            }
        }

        bool SuppressIsModified { get; set; }
        private void meTemplateEditor_TextChanged(object sender, EventArgs e) {
            ParseTemplate(this.meTemplateEditor.Text);
            if(SuppressIsModified)
                return;
            IsModified = true;
        }

        private void ParseTemplate(string text) {
            FileRenameManager.Default.Template = text;
            this.lbExample.Text = FileRenameManager.Default.GetFileName(FileRenameManager.Files, FileRenameManager.FileExample);
            UpdateErrorList();
        }

        private void UpdateErrorList() {
            this.lbErrors.Items.BeginUpdate();
            try {
                this.lbErrors.Items.Clear();
                FileRenameManager.Default.Errors.ForEach((err) => this.lbErrors.Items.Add(err));
            } finally {
                this.lbErrors.Items.EndUpdate();
            }
        }
        public void UpdateButtons() {
            this.biSave.Enabled = this.biSaveAs.Enabled = IsModified;
        }

        bool isModified;
        public bool IsModified {
            get { return isModified; }
            set {
                if(IsModified == value)
                    return;
                isModified = value;
                UpdateButtons();
            }
        }

        private void biSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(!TrySaveModified(false, true))
                return;
            IsModified = false;
        }

        private void biSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(!TrySaveModified(true, true))
                return;
            IsModified = false;
        }

        public bool TrySaveModified(bool showNameEditor, bool silent) {
            if(!IsModified) return true;
            if(!silent) {
                DialogResult res = XtraMessageBox.Show(this, "Template is modofied. Do you want to save it?", "Template Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if(res == System.Windows.Forms.DialogResult.Cancel)
                    return false;
                if(res == System.Windows.Forms.DialogResult.No)
                    return false;
            }
            if(FileRenameManager.Default.Errors.Count > 0) {
                XtraMessageBox.Show("Your Template contains error. Please fix them before saving.", "Template Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(string.IsNullOrEmpty(TemplateName) || showNameEditor) {
                TemplateName = GetNewTemplateName(TemplateName);
                if(string.IsNullOrEmpty(TemplateName))
                    return false;
            }
            FileRenameTemplateInfo info = SettingsStore.Default.GetFileRenameTemlate(TemplateName);
            if(info == null) {
                info = new FileRenameTemplateInfo();
                SettingsStore.Default.FileRenameTemplates.Add(info);
            }
            info.Name = TemplateName;
            info.Template = this.meTemplateEditor.Text.Trim();
            IsModified = false;
            Template = info;
            InitializeTemplates();
            return true;
        }

        private string GetNewTemplateName(string templateName) {
            TemplateNameEditor dlg = new TemplateNameEditor();
            dlg.TemplateName = templateName;
            if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return templateName;
            return dlg.TemplateName;
        }

        private void biCopyFrom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(!TrySaveModified(false, false))
                return;
            SuppressIsModified = true;
            try {
                this.meTemplateEditor.Text = ((FileRenameTemplateInfo)this.beTemplates.EditValue).Template;
            } finally {
                SuppressIsModified = false;
                IsModified = false;
            }
        }

        private void biNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(!TrySaveModified(false, false))
                return;
            SuppressIsModified = true;
            try {
                this.meTemplateEditor.Text = "";
            } finally {
                SuppressIsModified = false;
                IsModified = false;
            }
        }

        public string TemplateName { get; set; }

        private void gcKeywords_Gallery_ItemClick(object sender, GalleryItemClickEventArgs e) {
            this.meTemplateEditor.Text = this.meTemplateEditor.Text.Insert(this.meTemplateEditor.SelectionStart,
                FileRenameManager.Default.KeywordCharOpen + e.Item.Caption + FileRenameManager.Default.KeywordCharClose);
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            if(!TrySaveModified(false, false))
                return;
            Template = SettingsStore.Default.GetFileRenameTemlate(TemplateName);
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
