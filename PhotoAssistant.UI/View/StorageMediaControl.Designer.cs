namespace PhotoAssistant.UI.View {
    partial class StorageMediaControl {
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
            DevExpress.Utils.ContextButton contextButton1 = new DevExpress.Utils.ContextButton();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StorageMediaControl));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbRemoveMedia = new DevExpress.XtraEditors.SimpleButton();
            this.sbAddStorageMedia = new DevExpress.XtraEditors.SimpleButton();
            this.galleryControl1 = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).BeginInit();
            this.galleryControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbRemoveMedia);
            this.layoutControl1.Controls.Add(this.sbAddStorageMedia);
            this.layoutControl1.Controls.Add(this.galleryControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(543, 380);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sbRemoveMedia
            // 
            this.sbRemoveMedia.Location = new System.Drawing.Point(12, 346);
            this.sbRemoveMedia.Name = "sbRemoveMedia";
            this.sbRemoveMedia.Size = new System.Drawing.Size(519, 22);
            this.sbRemoveMedia.StyleController = this.layoutControl1;
            this.sbRemoveMedia.TabIndex = 6;
            this.sbRemoveMedia.Text = "Remove Media";
            this.sbRemoveMedia.Click += new System.EventHandler(this.sbRemoveMedia_Click);
            // 
            // sbAddStorageMedia
            // 
            this.sbAddStorageMedia.Location = new System.Drawing.Point(12, 320);
            this.sbAddStorageMedia.Name = "sbAddStorageMedia";
            this.sbAddStorageMedia.Size = new System.Drawing.Size(519, 22);
            this.sbAddStorageMedia.StyleController = this.layoutControl1;
            this.sbAddStorageMedia.TabIndex = 5;
            this.sbAddStorageMedia.Text = "Add Media";
            this.sbAddStorageMedia.Click += new System.EventHandler(this.sbAddStorageMedia_Click);
            // 
            // galleryControl1
            // 
            this.galleryControl1.Controls.Add(this.galleryControlClient1);
            this.galleryControl1.DesignGalleryGroupIndex = 0;
            this.galleryControl1.DesignGalleryItemIndex = 0;
            // 
            // 
            // 
            this.galleryControl1.Gallery.CheckSelectedItemViaKeyboard = true;
            contextButton1.Alignment = DevExpress.Utils.ContextItemAlignment.FarCenter;
            contextButton1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            contextButton1.AppearanceHover.ForeColor = System.Drawing.Color.White;
            contextButton1.AppearanceHover.Options.UseForeColor = true;
            contextButton1.AppearanceNormal.ForeColor = System.Drawing.Color.White;
            contextButton1.AppearanceNormal.Options.UseForeColor = true;
            contextButton1.Glyph = ((System.Drawing.Image)(resources.GetObject("contextButton1.Glyph")));
            contextButton1.Id = new System.Guid("393ef68d-f61c-475e-9fdb-12ac097215f0");
            contextButton1.Name = "ContextButton";
            this.galleryControl1.Gallery.ContextButtons.Add(contextButton1);
            this.galleryControl1.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.Multiple;
            this.galleryControl1.Gallery.ContextButtonClick += new DevExpress.Utils.ContextItemClickEventHandler(this.galleryControl1_Gallery_ContextButtonClick);
            this.galleryControl1.Gallery.ItemCheckedChanged += new DevExpress.XtraBars.Ribbon.GalleryItemEventHandler(this.galleryControl1_Gallery_ItemCheckedChanged);
            this.galleryControl1.Location = new System.Drawing.Point(24, 43);
            this.galleryControl1.Name = "galleryControl1";
            this.galleryControl1.Size = new System.Drawing.Size(495, 261);
            this.galleryControl1.StyleController = this.layoutControl1;
            this.galleryControl1.TabIndex = 4;
            this.galleryControl1.Text = "galleryControl1";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl1;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(474, 257);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutGroup});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(543, 380);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbAddStorageMedia;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 308);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(523, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbRemoveMedia;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 334);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(523, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutGroup
            // 
            this.layoutGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutGroup.Name = "layoutGroup";
            this.layoutGroup.Size = new System.Drawing.Size(523, 308);
            this.layoutGroup.Text = "Storage Media";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.galleryControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(499, 265);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // StorageMediaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "StorageMediaControl";
            this.Size = new System.Drawing.Size(543, 380);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).EndInit();
            this.galleryControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton sbRemoveMedia;
        private DevExpress.XtraEditors.SimpleButton sbAddStorageMedia;
        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
