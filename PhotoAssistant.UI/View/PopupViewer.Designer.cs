namespace PhotoAssistant.UI.View {
    partial class PopupViewer {
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
            this.galleryControl1 = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.galleryControlClient2 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).BeginInit();
            this.galleryControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // galleryControl1
            // 
            this.galleryControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.galleryControl1.Controls.Add(this.galleryControlClient1);
            this.galleryControl1.DesignGalleryGroupIndex = 0;
            this.galleryControl1.DesignGalleryItemIndex = 0;
            this.galleryControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            // 
            // galleryControlGallery1
            // 
            this.galleryControl1.Gallery.ColumnCount = 1;
            this.galleryControl1.Gallery.FirstItemVertAlignment = DevExpress.XtraBars.Ribbon.Gallery.GalleryItemAlignment.Center;
            this.galleryControl1.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleRadio;
            this.galleryControl1.Gallery.LastItemVertAlignment = DevExpress.XtraBars.Ribbon.Gallery.GalleryItemAlignment.Center;
            this.galleryControl1.Gallery.OptionsImageLoad.AnimationType = DevExpress.Utils.ImageContentAnimationType.Push;
            this.galleryControl1.Gallery.OptionsImageLoad.AsyncLoad = true;
            this.galleryControl1.Gallery.OptionsImageLoad.DesiredThumbnailSize = new System.Drawing.Size(64, 64);
            this.galleryControl1.Gallery.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.galleryControl1.Gallery.ScrollMode = DevExpress.XtraBars.Ribbon.Gallery.GalleryScrollMode.Smooth;
            this.galleryControl1.Gallery.ShowGroupCaption = false;
            this.galleryControl1.Gallery.StretchItems = true;
            this.galleryControl1.Gallery.ItemCheckedChanged += new DevExpress.XtraBars.Ribbon.GalleryItemEventHandler(this.galleryControlGallery1_ItemCheckedChanged);
            this.galleryControl1.Gallery.GetThumbnailImage += new DevExpress.XtraBars.Ribbon.Gallery.GalleryThumbnailImageEventHandler(this.galleryControlGallery1_GetThumbnailImage);
            this.galleryControl1.Location = new System.Drawing.Point(0, 430);
            this.galleryControl1.Name = "galleryControl1";
            this.galleryControl1.Size = new System.Drawing.Size(756, 86);
            this.galleryControl1.TabIndex = 0;
            this.galleryControl1.Text = "galleryControl1";
            this.galleryControl1.Visible = false;
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl1;
            this.galleryControlClient1.Location = new System.Drawing.Point(1, 1);
            this.galleryControlClient1.Size = new System.Drawing.Size(754, 67);
            // 
            // galleryControlClient2
            // 
            this.galleryControlClient2.GalleryControl = null;
            this.galleryControlClient2.Location = new System.Drawing.Point(0, 0);
            this.galleryControlClient2.Size = new System.Drawing.Size(0, 0);
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(756, 430);
            this.elementHost1.TabIndex = 1;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // PopupViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.galleryControl1);
            this.Name = "PopupViewer";
            this.Size = new System.Drawing.Size(756, 516);
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).EndInit();
            this.galleryControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient2;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
    }
}
