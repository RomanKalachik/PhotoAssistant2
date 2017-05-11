namespace PhotoAssistant.UI.View {
    partial class ProjectsControl {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectsControl));
            DevExpress.Utils.ContextButton contextButton2 = new DevExpress.Utils.ContextButton();
            this.tileControl1 = new DevExpress.XtraEditors.TileControl();
            this.SuspendLayout();
            // 
            // tileControl1
            // 
            this.tileControl1.AllowDrag = false;
            this.tileControl1.AppearanceItem.Normal.BackColor = System.Drawing.Color.Gray;
            this.tileControl1.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Transparent;
            this.tileControl1.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileControl1.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tileControl1.BackgroundImage = global::PhotoAssistant.UI.Properties.Resources.SplashScreenImageBig;
            this.tileControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tileControl1.ContextButtonOptions.AllowGlyphSkinning = true;
            this.tileControl1.ContextButtonOptions.AnimationType = DevExpress.Utils.ContextAnimationType.OutAnimation;
            this.tileControl1.ContextButtonOptions.TopPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tileControl1.ContextButtonOptions.TopPanelPadding = new System.Windows.Forms.Padding(7);
            contextButton1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            contextButton1.AppearanceHover.ForeColor = System.Drawing.Color.White;
            contextButton1.AppearanceHover.Options.UseForeColor = true;
            contextButton1.AppearanceNormal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(162)))));
            contextButton1.AppearanceNormal.Options.UseForeColor = true;
            contextButton1.Glyph = ((System.Drawing.Image)(resources.GetObject("contextButton1.Glyph")));
            contextButton1.Id = new System.Guid("6f341045-0d40-4774-92ab-f6f8e3d4cb92");
            contextButton1.Name = "editButton";
            contextButton1.ToolTip = "Edit Project Settings";
            contextButton2.Alignment = DevExpress.Utils.ContextItemAlignment.TopFar;
            contextButton2.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            contextButton2.AppearanceHover.ForeColor = System.Drawing.Color.White;
            contextButton2.AppearanceHover.Options.UseForeColor = true;
            contextButton2.AppearanceNormal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(162)))));
            contextButton2.AppearanceNormal.Options.UseForeColor = true;
            contextButton2.Glyph = ((System.Drawing.Image)(resources.GetObject("contextButton2.Glyph")));
            contextButton2.Id = new System.Guid("538b6a8f-464c-4be3-8d11-ee01f375fe8e");
            contextButton2.Name = "removeButton";
            contextButton2.ToolTip = "Remove Project from recet list";
            this.tileControl1.ContextButtons.Add(contextButton1);
            this.tileControl1.ContextButtons.Add(contextButton2);
            this.tileControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileControl1.DragSize = new System.Drawing.Size(0, 0);
            this.tileControl1.ItemPadding = new System.Windows.Forms.Padding(0);
            this.tileControl1.Location = new System.Drawing.Point(0, 0);
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.ShowGroupText = true;
            this.tileControl1.ShowText = true;
            this.tileControl1.Size = new System.Drawing.Size(895, 506);
            this.tileControl1.TabIndex = 0;
            this.tileControl1.Text = "Projects";
            // 
            // ProjectsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tileControl1);
            this.Name = "ProjectsControl";
            this.Size = new System.Drawing.Size(895, 506);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TileControl tileControl1;


    }
}
