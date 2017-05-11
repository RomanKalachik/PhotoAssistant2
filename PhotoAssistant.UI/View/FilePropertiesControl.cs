using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;

using DevExpress.XtraLayout;
using DevExpress.XtraBars.Navigation;
using PhotoAssistant.Core.Model;
using PhotoAssistant.UI.ViewHelpers;

namespace PhotoAssistant.UI.View {
    public partial class FilePropertiesControl : UserControl {
        public FilePropertiesControl() {
            InitializeComponent();
            CreateTagSelectionControl();
            UpdateAccordionContainerHeight();
        }

        TagSelectionControl TagSelectionControl { get; set; }
        private void CreateTagSelectionControl() {
            TagSelectionControl = new TagSelectionControl();
            TagSelectionControl.TagType = TagType.Tag;
            TagSelectionControl.ApplyToAccordionControl(this.accordionControl1.Elements[4]);
        }

        public void UpdateAccordionContainerHeight() {
            foreach(AccordionControlElement node in this.accordionControl1.Elements)
                UpdateAccordionContainerHeight(node);
        }

        private void UpdateAccordionContainerHeight(AccordionControlElement node) {
            if(node.ContentContainer != null)
                UpdateAccordionContainerHeight(node.ContentContainer);
            foreach(AccordionControlElement child in node.Elements) {
                UpdateAccordionContainerHeight(child);
            }
        }

        private void UpdateAccordionContainerHeight(AccordionContentContainer control) {
            if(control.Controls.Count == 0)
                return;
            LayoutControl layout = control.Controls[0] as LayoutControl;
            if(layout != null)
                control.Height = layout.Root.MinSize.Height;
            if(control.Controls[0].AutoSize)
                control.Height = control.Controls[0].GetPreferredSize(new Size(int.MaxValue, int.MaxValue)).Height;
        }

        private void InitializeLabelsComboBox() {
            this.colorLabelComboBox.Properties.SmallImages = ColorLabelImagesCreator.CreateColorLabelsImageCollection(Model);
            int imageIndex = 0;
            this.colorLabelComboBox.Properties.Items.Add(new ImageComboBoxItem(DmColorLabel.NoneString, null, imageIndex));
            imageIndex++;
            foreach(DmColorLabel label in Model.GetColorLabels()) {
                this.colorLabelComboBox.Properties.Items.Add(new ImageComboBoxItem(label.Text, label, imageIndex));
                imageIndex++;
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
            InitializeLabelsComboBox();
            InitializeTagSelectionControl();
        }

        private void InitializeTagSelectionControl() {
            TagSelectionControl.Model = Model;
        }

        DmFile fileInfo;
        public DmFile FileInfo {
            get {
                return fileInfo;
            }
            set {
                if(FileInfo == value)
                    return;
                DmFile prev = FileInfo;
                fileInfo = value;
                OnFileInfoChanged(prev);
            }
        }

        bool SuppressPropertiesChanged { get; set; }

        public void UpdateProperties() {
            this.accordionControl1.SuspendLayout();
            SuppressPropertiesChanged = true;
            try {
                if(FileInfo == null) {
                    TagSelectionControl.File = null;
                    this.accordionControl1.Enabled = false;
                    this.fileNameLabel.Text = "";
                    this.lcFolder.Text = "";
                    this.fileSizeLabel.Text = "";
                    this.mediaFormatLabel.Text = "";
                    this.dimensionLabel.Text = "";
                    this.dpiLabel.Text = "";
                    this.ratingControl.Rating = 0;
                    this.colorLabelComboBox.EditValue = null;
                    this.creationDateTimeEdit.EditValue = null;
                    this.importDateTimeEdit.EditValue = null;
                    this.сaptionTextEdit.Text = "";
                    this.descriptionMemoEdit.Text = "";
                    this.countryTextEdit.Text = "";
                    this.stateTextEdit.Text = "";
                    this.cityTextEdit.Text = "";
                    this.locationTextEdit.Text = "";
                    this.eventTextEdit.Text = "";
                    this.projectTextEdit.Text = "";
                    this.clientTextEdit.Text = "";
                    this.sceneTextEdit.Text = "";
                    this.commentMemoEdit.Text = "";
                    this.officeDocumentSubjectTextEdit.Text = "";
                    this.officeDocumentManagerTextEdit.Text = "";
                    return;
                }
                TagSelectionControl.File = FileInfo;
                this.accordionControl1.Enabled = true;
                this.fileNameLabel.Text = FileInfo.FileName;
                this.lcFolder.Text = FileInfo.Folder;
                this.fileSizeLabel.Text = FileInfo.FileSize.ToString();
                if(FileInfo.MediaFormat == null) return;
                this.mediaFormatLabel.Text = FileInfo.MediaFormat.Extension;
                this.dimensionLabel.Text = FileInfo.ImageDimension;
                this.dpiLabel.Text = FileInfo.Dpi.ToString();
                this.ratingControl.Rating = FileInfo.Rating;
                this.colorLabelComboBox.EditValue = FileInfo.ColorLabel;
                this.creationDateTimeEdit.DateTime = FileInfo.CreationDate;
                this.importDateTimeEdit.DateTime = FileInfo.ImportDate;
                this.сaptionTextEdit.Text = FileInfo.Caption;
                this.descriptionMemoEdit.Text = FileInfo.Description;
                this.countryTextEdit.Text = FileInfo.Country;
                this.stateTextEdit.Text = FileInfo.State;
                this.cityTextEdit.Text = FileInfo.City;
                this.locationTextEdit.Text = FileInfo.Location;
                this.eventTextEdit.Text = FileInfo.Event;
                this.projectTextEdit.Text = FileInfo.Project;
                this.clientTextEdit.Text = FileInfo.Client;
                this.sceneTextEdit.Text = FileInfo.Scene;
                this.commentMemoEdit.Text = FileInfo.Comment;
                this.officeDocumentSubjectTextEdit.Text = FileInfo.OfficeDocumentSubject;
                this.officeDocumentManagerTextEdit.Text = FileInfo.OfficeDocumentManager;
            } finally {
                SuppressPropertiesChanged = false;
                this.accordionControl1.ResumeLayout();
            }
        }

        private void OnFileInfoChanged(DmFile prev) {
            if(ShouldUpdateItem && prev != null) {
                UpdateFile(prev);
            }
            ShouldUpdateItem = false;
            UpdateProperties();
            UpdateHistogramm();
        }

        private void UpdateHistogramm() {
            if(FileInfo == null || FileInfo.ThumbImage == null) {
                this.histogrammControl1.ClearChannels(true);
                this.histogrammControl1.ClearLabels();
            }
            else {
                this.histogrammControl1.SetLabels(
                    string.Format("ISO {0}", FileInfo.ISO),
                    string.Format("{0} mm", FileInfo.FocalLength),
                    string.Format("f / {0}", FileInfo.Aperture),
                    string.Format("{0} sec", FileInfo.ShutterSpeed)
                    );
                this.histogrammControl1.CreateHistogramm((Bitmap)FileInfo.ThumbImage);
        }
        }

        bool shouldUpdateItem;
        protected bool ShouldUpdateItem {
            get { return shouldUpdateItem; }
            set {
                if(ShouldUpdateItem == value)
                    return;
                shouldUpdateItem = value;
                this.bbSave.Enabled = ShouldUpdateItem;
            }
        }

        private void descriptionMemoEdit_TextChanged(object sender, EventArgs e) {
            this.descriptionMemoEdit.SuperTip = new DevExpress.Utils.SuperToolTip();
            this.descriptionMemoEdit.SuperTip.Items.Add(new ToolTipItem() { Text = this.descriptionMemoEdit.Text });
            OnEditValueChanged(sender, e);
        }

        private void commentMemoEdit_EditValueChanged(object sender, System.EventArgs e) {
            this.commentMemoEdit.SuperTip = new DevExpress.Utils.SuperToolTip();
            this.commentMemoEdit.SuperTip.Items.Add(new ToolTipItem() { Text = this.commentMemoEdit.Text });
            OnEditValueChanged(sender, e);
        }

        private void textEdit_ParseEditValue(object sender, ConvertEditValueEventArgs e) {
            if(e.Value is string)
                e.Value = e.Value.ToString().Trim();
        }

        private void OnEditValueChanged(object sender, EventArgs e) {
            if(SuppressPropertiesChanged)
                return;
            ShouldUpdateItem = true;
        }

        private void bbSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            UpdateFile(FileInfo);
            ShouldUpdateItem = false;
        }

        protected virtual void UpdateFile(DmFile file) {
            Model.BeginUpdateFile(file);
            ApplyChangesToFile(file);
            Model.EndUpdateFile(file);
        }

        private void ApplyChangesToFile(DmFile file) {
            file.ColorLabel = (DmColorLabel)((ImageComboBoxItem)this.colorLabelComboBox.SelectedItem).Value;
            file.CreationDate = this.creationDateTimeEdit.DateTime;
            file.ImportDate = this.importDateTimeEdit.DateTime;
            file.Caption = this.сaptionTextEdit.Text;
            file.Description = this.descriptionMemoEdit.Text;
            file.Country = this.countryTextEdit.Text;
            file.State = this.stateTextEdit.Text;
            file.City = this.cityTextEdit.Text;
            file.Location = this.locationTextEdit.Text;
            file.Event = this.eventTextEdit.Text;
            file.Project = this.projectTextEdit.Text;
            file.Client = this.clientTextEdit.Text;
            file.Scene = this.sceneTextEdit.Text;
            file.OfficeDocumentSubject = this.officeDocumentSubjectTextEdit.Text;
            file.OfficeDocumentManager = this.officeDocumentManagerTextEdit.Text;
            file.Comment = this.commentMemoEdit.Text;
            file.Rating = (int)this.ratingControl.Rating;
        }

        private void accordionControl1_Click(object sender, EventArgs e) {

        }
    }
}
