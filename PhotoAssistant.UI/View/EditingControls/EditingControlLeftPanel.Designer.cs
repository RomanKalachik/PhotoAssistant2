using PhotoAssistant.Controls.Win.EditingControls;

namespace PhotoAssistant.UI.View.EditingControls {
    partial class EditingControlLeftPanel {
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
            DevExpress.XtraBars.Navigation.AccordionContextButton accordionContextButton1 = new DevExpress.XtraBars.Navigation.AccordionContextButton();
            DevExpress.XtraBars.Navigation.AccordionContextButton accordionContextButton2 = new DevExpress.XtraBars.Navigation.AccordionContextButton();
            DevExpress.XtraBars.Navigation.AccordionContextButton accordionContextButton3 = new DevExpress.XtraBars.Navigation.AccordionContextButton();
            DevExpress.XtraBars.Navigation.AccordionContextButton accordionContextButton4 = new DevExpress.XtraBars.Navigation.AccordionContextButton();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionContentContainer1 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.pictureNavigator = new PictureNavigator();
            this.aceNavigator = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            this.accordionControl1.SuspendLayout();
            this.accordionContentContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // accordionControl1
            // 
            this.accordionControl1.Controls.Add(this.accordionContentContainer1);
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.aceNavigator});
            accordionContextButton1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            accordionContextButton1.AppearanceHover.ForeColor = System.Drawing.Color.White;
            accordionContextButton1.AppearanceHover.Options.UseForeColor = true;
            accordionContextButton1.AppearanceNormal.ForeColor = System.Drawing.Color.White;
            accordionContextButton1.AppearanceNormal.Options.UseForeColor = true;
            accordionContextButton1.Caption = "1:2";
            accordionContextButton1.Id = new System.Guid("f57fe196-afb3-44f7-b7f4-316bb9796be6");
            accordionContextButton1.Name = "Zoom";
            accordionContextButton1.Padding = new System.Windows.Forms.Padding(5);
            accordionContextButton1.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            accordionContextButton2.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            accordionContextButton2.AppearanceHover.ForeColor = System.Drawing.Color.White;
            accordionContextButton2.AppearanceHover.Options.UseForeColor = true;
            accordionContextButton2.AppearanceNormal.ForeColor = System.Drawing.Color.White;
            accordionContextButton2.AppearanceNormal.Options.UseForeColor = true;
            accordionContextButton2.Caption = "1:1";
            accordionContextButton2.Id = new System.Guid("79ccd9a9-1c62-4eb4-839c-5aa86ab4deb6");
            accordionContextButton2.Name = "ZoomOrigin";
            accordionContextButton2.Padding = new System.Windows.Forms.Padding(5);
            accordionContextButton2.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            accordionContextButton3.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            accordionContextButton3.AppearanceHover.ForeColor = System.Drawing.Color.White;
            accordionContextButton3.AppearanceHover.Options.UseForeColor = true;
            accordionContextButton3.AppearanceNormal.ForeColor = System.Drawing.Color.White;
            accordionContextButton3.AppearanceNormal.Options.UseForeColor = true;
            accordionContextButton3.Caption = "FILL";
            accordionContextButton3.Id = new System.Guid("f5628860-4eb1-4c5e-8e60-d3ab8cb681a6");
            accordionContextButton3.Name = "ZoomFill";
            accordionContextButton3.Padding = new System.Windows.Forms.Padding(5);
            accordionContextButton3.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            accordionContextButton4.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            accordionContextButton4.AppearanceHover.ForeColor = System.Drawing.Color.White;
            accordionContextButton4.AppearanceHover.Options.UseForeColor = true;
            accordionContextButton4.AppearanceNormal.ForeColor = System.Drawing.Color.White;
            accordionContextButton4.AppearanceNormal.Options.UseForeColor = true;
            accordionContextButton4.Caption = "FIT";
            accordionContextButton4.Id = new System.Guid("3f8d98ca-ae7f-4fc7-ab21-d668ed68d9c5");
            accordionContextButton4.Name = "ZoomFit";
            accordionContextButton4.Padding = new System.Windows.Forms.Padding(5);
            accordionContextButton4.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            this.accordionControl1.ItemContextButtons.Add(accordionContextButton1);
            this.accordionControl1.ItemContextButtons.Add(accordionContextButton2);
            this.accordionControl1.ItemContextButtons.Add(accordionContextButton3);
            this.accordionControl1.ItemContextButtons.Add(accordionContextButton4);
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.Size = new System.Drawing.Size(365, 643);
            this.accordionControl1.TabIndex = 0;
            this.accordionControl1.Text = "accordionControl1";
            this.accordionControl1.ContextButtonCustomize += new DevExpress.XtraBars.Navigation.AccordionControlContextButtonCustomizeEventHandler(this.accordionControl1_ContextButtonCustomize);
            this.accordionControl1.ContextButtonClick += new DevExpress.Utils.ContextItemClickEventHandler(this.accordionControl1_ContextButtonClick);
            // 
            // accordionContentContainer1
            // 
            this.accordionContentContainer1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.accordionContentContainer1.Appearance.Options.UseBackColor = true;
            this.accordionContentContainer1.Controls.Add(this.pictureNavigator);
            this.accordionContentContainer1.Name = "accordionContentContainer1";
            this.accordionContentContainer1.Size = new System.Drawing.Size(348, 219);
            this.accordionContentContainer1.TabIndex = 1;
            // 
            // pictureNavigator
            // 
            this.pictureNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureNavigator.EditValue = "button1";
            this.pictureNavigator.Location = new System.Drawing.Point(0, 0);
            this.pictureNavigator.Name = "pictureNavigator";
            this.pictureNavigator.Size = new System.Drawing.Size(348, 219);
            this.pictureNavigator.TabIndex = 0;
            // 
            // aceNavigator
            // 
            this.aceNavigator.ContentContainer = this.accordionContentContainer1;
            this.aceNavigator.Expanded = true;
            this.aceNavigator.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceNavigator.Text = "Navigator";
            // 
            // EditingControlLeftPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.accordionControl1);
            this.Name = "EditingControlLeftPanel";
            this.Size = new System.Drawing.Size(365, 643);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            this.accordionControl1.ResumeLayout(false);
            this.accordionContentContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceNavigator;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContentContainer1;
        private PhotoAssistant.Controls.Win.EditingControls.PictureNavigator pictureNavigator;
    }
}
