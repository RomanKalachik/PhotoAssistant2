namespace PhotoAssistant.UI.View {
    partial class TagSelectionControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlCategories = new DevExpress.XtraTreeList.TreeList();
            this.tlcText = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcTimeStamp = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlAddCount = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btAddSelected = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.gcSuggestedKeywords = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.btAddAll = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.teAssignedKeywords = new DevExpress.XtraEditors.TokenEdit();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cbSelectCategories = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionContainerControl1 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.accordionContainerControl2 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.accordionContainerControl3 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.accordionControlItem1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlItem2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlItem3 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlCategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSuggestedKeywords)).BeginInit();
            this.gcSuggestedKeywords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAssignedKeywords.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSelectCategories.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            this.accordionControl1.SuspendLayout();
            this.accordionContainerControl1.SuspendLayout();
            this.accordionContainerControl2.SuspendLayout();
            this.accordionContainerControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeList
            // 
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList.Location = new System.Drawing.Point(0, 0);
            this.treeList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeList.Name = "treeList";
            this.treeList.OptionsView.ShowButtons = false;
            this.treeList.OptionsView.ShowColumns = false;
            this.treeList.OptionsView.ShowHorzLines = false;
            this.treeList.OptionsView.ShowVertLines = false;
            this.treeList.Size = new System.Drawing.Size(437, 694);
            this.treeList.TabIndex = 0;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Text";
            this.treeListColumn1.FieldName = "Text";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // tlCategories
            // 
            this.tlCategories.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tlcText,
            this.tlcTimeStamp,
            this.tlAddCount});
            this.tlCategories.KeyFieldName = "Id";
            this.tlCategories.Location = new System.Drawing.Point(32, 40);
            this.tlCategories.Name = "tlCategories";
            this.tlCategories.OptionsBehavior.Editable = false;
            this.tlCategories.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tlCategories.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            this.tlCategories.OptionsView.ShowColumns = false;
            this.tlCategories.OptionsView.ShowHorzLines = false;
            this.tlCategories.OptionsView.ShowIndicator = false;
            this.tlCategories.OptionsView.ShowVertLines = false;
            this.tlCategories.ParentFieldName = "ParentId";
            this.tlCategories.Size = new System.Drawing.Size(374, 301);
            this.tlCategories.TabIndex = 10;
            this.tlCategories.DoubleClick += new System.EventHandler(this.tlCategories_DoubleClick);
            // 
            // tlcText
            // 
            this.tlcText.Caption = "Text";
            this.tlcText.FieldName = "Text";
            this.tlcText.Name = "tlcText";
            this.tlcText.Visible = true;
            this.tlcText.VisibleIndex = 0;
            // 
            // tlcTimeStamp
            // 
            this.tlcTimeStamp.Caption = "TimeStamp";
            this.tlcTimeStamp.FieldName = "TimeStamp";
            this.tlcTimeStamp.Name = "tlcTimeStamp";
            // 
            // tlAddCount
            // 
            this.tlAddCount.Caption = "AddCount";
            this.tlAddCount.FieldName = "AddCount";
            this.tlAddCount.Name = "tlAddCount";
            // 
            // btAddSelected
            // 
            this.btAddSelected.Location = new System.Drawing.Point(32, 120);
            this.btAddSelected.Name = "btAddSelected";
            this.btAddSelected.Size = new System.Drawing.Size(357, 24);
            this.btAddSelected.StyleController = this.layoutControl3;
            this.btAddSelected.TabIndex = 8;
            this.btAddSelected.Text = "Add Selected";
            this.btAddSelected.Click += new System.EventHandler(this.btAddSelected_Click);
            // 
            // layoutControl3
            // 
            this.layoutControl3.BackColor = System.Drawing.Color.Transparent;
            this.layoutControl3.Controls.Add(this.gcSuggestedKeywords);
            this.layoutControl3.Controls.Add(this.btAddSelected);
            this.layoutControl3.Controls.Add(this.btAddAll);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(418, 183);
            this.layoutControl3.TabIndex = 0;
            // 
            // gcSuggestedKeywords
            // 
            this.gcSuggestedKeywords.Controls.Add(this.galleryControlClient1);
            this.gcSuggestedKeywords.DesignGalleryGroupIndex = 0;
            this.gcSuggestedKeywords.DesignGalleryItemIndex = 0;
            // 
            // galleryControlGallery1
            // 
            this.gcSuggestedKeywords.Gallery.AllowMarqueeSelection = true;
            this.gcSuggestedKeywords.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.Multiple;
            this.gcSuggestedKeywords.Gallery.ShowItemImage = false;
            this.gcSuggestedKeywords.Gallery.ShowItemText = true;
            this.gcSuggestedKeywords.Gallery.ItemDoubleClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(this.galleryControlGallery1_ItemDoubleClick);
            this.gcSuggestedKeywords.Location = new System.Drawing.Point(32, 12);
            this.gcSuggestedKeywords.Name = "gcSuggestedKeywords";
            this.gcSuggestedKeywords.Size = new System.Drawing.Size(357, 104);
            this.gcSuggestedKeywords.StyleController = this.layoutControl3;
            this.gcSuggestedKeywords.TabIndex = 6;
            this.gcSuggestedKeywords.Text = "galleryControl1";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.gcSuggestedKeywords;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(336, 100);
            // 
            // btAddAll
            // 
            this.btAddAll.Location = new System.Drawing.Point(32, 148);
            this.btAddAll.Name = "btAddAll";
            this.btAddAll.Size = new System.Drawing.Size(357, 24);
            this.btAddAll.StyleController = this.layoutControl3;
            this.btAddAll.TabIndex = 7;
            this.btAddAll.Text = "Add All";
            this.btAddAll.Click += new System.EventHandler(this.btAddAll_Click);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "Root";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(30, 10, 10, 10);
            this.layoutControlGroup2.Size = new System.Drawing.Size(401, 184);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcSuggestedKeywords;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 108);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(54, 108);
            this.layoutControlItem2.Name = "layoutControlItem3";
            this.layoutControlItem2.Size = new System.Drawing.Size(361, 108);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btAddSelected;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 108);
            this.layoutControlItem3.Name = "layoutControlItem5";
            this.layoutControlItem3.Size = new System.Drawing.Size(361, 28);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btAddAll;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 136);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(361, 28);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // teAssignedKeywords
            // 
            this.teAssignedKeywords.Location = new System.Drawing.Point(32, 12);
            this.teAssignedKeywords.Name = "teAssignedKeywords";
            this.teAssignedKeywords.Properties.AutoHeightMode = DevExpress.XtraEditors.TokenEditAutoHeightMode.Expand;
            this.teAssignedKeywords.Properties.MinRowCount = 5;
            this.teAssignedKeywords.Properties.Separators.AddRange(new string[] {
            ","});
            this.teAssignedKeywords.Properties.ShowRemoveTokenButtons = true;
            this.teAssignedKeywords.Properties.TokenGlyphLocation = DevExpress.XtraEditors.TokenEditGlyphLocation.Right;
            this.teAssignedKeywords.Properties.TokenAdded += new DevExpress.XtraEditors.TokenEditTokenAddedEventHandler(this.teAssignedKeywords_Properties_TokenAdded);
            this.teAssignedKeywords.Properties.TokenRemoved += new DevExpress.XtraEditors.TokenEditTokenRemovedEventHandler(this.teAssignedKeywords_Properties_TokenRemoved);
            this.teAssignedKeywords.Size = new System.Drawing.Size(374, 108);
            this.teAssignedKeywords.StyleController = this.layoutControl2;
            this.teAssignedKeywords.TabIndex = 4;
            // 
            // layoutControl2
            // 
            this.layoutControl2.BackColor = System.Drawing.Color.Transparent;
            this.layoutControl2.Controls.Add(this.teAssignedKeywords);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(418, 160);
            this.layoutControl2.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(30, 10, 10, 10);
            this.layoutControlGroup1.Size = new System.Drawing.Size(418, 160);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teAssignedKeywords;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 140);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(54, 140);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(378, 140);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // cbSelectCategories
            // 
            this.cbSelectCategories.Location = new System.Drawing.Point(32, 12);
            this.cbSelectCategories.Name = "cbSelectCategories";
            this.cbSelectCategories.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSelectCategories.Size = new System.Drawing.Size(374, 24);
            this.cbSelectCategories.StyleController = this.layoutControl4;
            this.cbSelectCategories.TabIndex = 9;
            this.cbSelectCategories.SelectedIndexChanged += new System.EventHandler(this.cbSelectCategories_SelectedIndexChanged);
            // 
            // layoutControl4
            // 
            this.layoutControl4.BackColor = System.Drawing.Color.Transparent;
            this.layoutControl4.Controls.Add(this.cbSelectCategories);
            this.layoutControl4.Controls.Add(this.tlCategories);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup3;
            this.layoutControl4.Size = new System.Drawing.Size(418, 353);
            this.layoutControl4.TabIndex = 0;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "Root";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(30, 10, 10, 10);
            this.layoutControlGroup3.Size = new System.Drawing.Size(418, 353);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cbSelectCategories;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem6";
            this.layoutControlItem5.Size = new System.Drawing.Size(378, 28);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.tlCategories;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(104, 132);
            this.layoutControlItem6.Name = "layoutControlItem7";
            this.layoutControlItem6.Size = new System.Drawing.Size(378, 305);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // accordionControl1
            // 
            this.accordionControl1.Controls.Add(this.accordionContainerControl1);
            this.accordionControl1.Controls.Add(this.accordionContainerControl2);
            this.accordionControl1.Controls.Add(this.accordionContainerControl3);
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlItem1,
            this.accordionControlItem2,
            this.accordionControlItem3});
            this.accordionControl1.Size = new System.Drawing.Size(437, 694);
            this.accordionControl1.TabIndex = 1;
            // 
            // accordionContainerControl1
            // 
            this.accordionContainerControl1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.accordionContainerControl1.Appearance.Options.UseBackColor = true;
            this.accordionContainerControl1.Controls.Add(this.layoutControl2);
            this.accordionContainerControl1.Name = "accordionContainerControl1";
            this.accordionContainerControl1.Size = new System.Drawing.Size(418, 160);
            this.accordionContainerControl1.TabIndex = 1;
            // 
            // accordionContainerControl2
            // 
            this.accordionContainerControl2.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.accordionContainerControl2.Appearance.Options.UseBackColor = true;
            this.accordionContainerControl2.Controls.Add(this.layoutControl3);
            this.accordionContainerControl2.Name = "accordionContainerControl2";
            this.accordionContainerControl2.Size = new System.Drawing.Size(418, 183);
            this.accordionContainerControl2.TabIndex = 2;
            // 
            // accordionContainerControl3
            // 
            this.accordionContainerControl3.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.accordionContainerControl3.Appearance.Options.UseBackColor = true;
            this.accordionContainerControl3.Controls.Add(this.layoutControl4);
            this.accordionContainerControl3.Name = "accordionContainerControl3";
            this.accordionContainerControl3.Size = new System.Drawing.Size(418, 353);
            this.accordionContainerControl3.TabIndex = 3;
            // 
            // accordionControlItem1
            // 
            this.accordionControlItem1.ContentContainer = this.accordionContainerControl1;
            this.accordionControlItem1.HeaderControlToContextButtonsDistance = 5;
            this.accordionControlItem1.TextToImageDistance = 5;
            this.accordionControlItem1.Expanded = true;
            this.accordionControlItem1.HeaderControl = null;
            this.accordionControlItem1.HeaderVisible = true;
            this.accordionControlItem1.ImageIndex = -1;
            this.accordionControlItem1.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Stretch;
            this.accordionControlItem1.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlItem1.Text = "Assigned Keywords";
            this.accordionControlItem1.TextPosition = DevExpress.XtraBars.Navigation.TextPosition.BeforeImage;
            // 
            // accordionControlItem2
            // 
            this.accordionControlItem2.ContentContainer = this.accordionContainerControl2;
            this.accordionControlItem2.HeaderControlToContextButtonsDistance = 5;
            this.accordionControlItem2.TextToImageDistance = 5;
            this.accordionControlItem2.Expanded = true;
            this.accordionControlItem2.HeaderControl = null;
            this.accordionControlItem2.HeaderVisible = true;
            this.accordionControlItem2.ImageIndex = -1;
            this.accordionControlItem2.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Stretch;
            this.accordionControlItem2.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlItem2.Text = "Suggested Keywords";
            this.accordionControlItem2.TextPosition = DevExpress.XtraBars.Navigation.TextPosition.BeforeImage;
            // 
            // accordionControlItem3
            // 
            this.accordionControlItem3.ContentContainer = this.accordionContainerControl3;
            this.accordionControlItem3.HeaderControlToContextButtonsDistance = 5;
            this.accordionControlItem3.TextToImageDistance = 5;
            this.accordionControlItem3.HeaderControl = null;
            this.accordionControlItem3.HeaderVisible = true;
            this.accordionControlItem3.ImageIndex = -1;
            this.accordionControlItem3.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Stretch;
            this.accordionControlItem3.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlItem3.Text = "Keyword Categories";
            this.accordionControlItem3.TextPosition = DevExpress.XtraBars.Navigation.TextPosition.BeforeImage;
            // 
            // TagSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.treeList);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TagSelectionControl";
            this.Size = new System.Drawing.Size(437, 694);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlCategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSuggestedKeywords)).EndInit();
            this.gcSuggestedKeywords.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAssignedKeywords.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSelectCategories.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            this.accordionControl1.ResumeLayout(false);
            this.accordionContainerControl1.ResumeLayout(false);
            this.accordionContainerControl2.ResumeLayout(false);
            this.accordionContainerControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraEditors.SimpleButton btAddSelected;
        private DevExpress.XtraEditors.SimpleButton btAddAll;
        private DevExpress.XtraTreeList.TreeList tlCategories;
        private DevExpress.XtraEditors.ImageComboBoxEdit cbSelectCategories;
        public DevExpress.XtraEditors.TokenEdit teAssignedKeywords;
        public DevExpress.XtraBars.Ribbon.GalleryControl gcSuggestedKeywords;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcText;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcTimeStamp;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlAddCount;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContainerControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContainerControl2;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContainerControl3;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlItem1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlItem2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlItem3;
    }
}
