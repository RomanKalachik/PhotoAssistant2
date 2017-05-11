namespace PhotoAssistant.UI.View {
    partial class ExistingFileDialog {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ceRemember = new System.Windows.Forms.CheckBox();
            this.btGenerateNewName = new DevExpress.XtraEditors.SimpleButton();
            this.btOvewrite = new DevExpress.XtraEditors.SimpleButton();
            this.btSkip = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcLabel = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSkip = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGenerateNewName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOverride = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcRemember = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGenerateNewName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRemember)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceRemember);
            this.layoutControl1.Controls.Add(this.btGenerateNewName);
            this.layoutControl1.Controls.Add(this.btOvewrite);
            this.layoutControl1.Controls.Add(this.btSkip);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(533, 308, 1157, 664);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(553, 129);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceRemember
            // 
            this.ceRemember.Location = new System.Drawing.Point(12, 71);
            this.ceRemember.Name = "ceRemember";
            this.ceRemember.Size = new System.Drawing.Size(529, 20);
            this.ceRemember.TabIndex = 8;
            this.ceRemember.Text = "Remember My Choise";
            this.ceRemember.UseVisualStyleBackColor = true;
            // 
            // btGenerateNewName
            // 
            this.btGenerateNewName.AutoWidthInLayoutControl = true;
            this.btGenerateNewName.Location = new System.Drawing.Point(261, 95);
            this.btGenerateNewName.Name = "btGenerateNewName";
            this.btGenerateNewName.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.btGenerateNewName.Size = new System.Drawing.Size(171, 22);
            this.btGenerateNewName.StyleController = this.layoutControl1;
            this.btGenerateNewName.TabIndex = 7;
            this.btGenerateNewName.Text = "Generate New Name";
            this.btGenerateNewName.Click += new System.EventHandler(this.btGenerateNewName_Click);
            // 
            // btOvewrite
            // 
            this.btOvewrite.AutoWidthInLayoutControl = true;
            this.btOvewrite.Location = new System.Drawing.Point(436, 95);
            this.btOvewrite.Name = "btOvewrite";
            this.btOvewrite.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.btOvewrite.Size = new System.Drawing.Size(105, 22);
            this.btOvewrite.StyleController = this.layoutControl1;
            this.btOvewrite.TabIndex = 6;
            this.btOvewrite.Text = "Overwrite";
            this.btOvewrite.Click += new System.EventHandler(this.btOvewrite_Click);
            // 
            // btSkip
            // 
            this.btSkip.AutoWidthInLayoutControl = true;
            this.btSkip.Location = new System.Drawing.Point(184, 95);
            this.btSkip.Name = "btSkip";
            this.btSkip.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.btSkip.Size = new System.Drawing.Size(73, 22);
            this.btSkip.StyleController = this.layoutControl1;
            this.btSkip.TabIndex = 5;
            this.btSkip.Text = "Skip";
            this.btSkip.Click += new System.EventHandler(this.btSkip_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 17);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "labelControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcLabel,
            this.lcSkip,
            this.lcGenerateNewName,
            this.lcOverride,
            this.lcRemember,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(553, 129);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcLabel
            // 
            this.lcLabel.Control = this.labelControl1;
            this.lcLabel.Location = new System.Drawing.Point(0, 0);
            this.lcLabel.Name = "lcLabel";
            this.lcLabel.Size = new System.Drawing.Size(533, 21);
            this.lcLabel.TextSize = new System.Drawing.Size(0, 0);
            this.lcLabel.TextVisible = false;
            // 
            // lcSkip
            // 
            this.lcSkip.Control = this.btSkip;
            this.lcSkip.Location = new System.Drawing.Point(172, 83);
            this.lcSkip.Name = "lcSkip";
            this.lcSkip.Size = new System.Drawing.Size(77, 26);
            this.lcSkip.TextSize = new System.Drawing.Size(0, 0);
            this.lcSkip.TextVisible = false;
            // 
            // lcGenerateNewName
            // 
            this.lcGenerateNewName.Control = this.btGenerateNewName;
            this.lcGenerateNewName.Location = new System.Drawing.Point(249, 83);
            this.lcGenerateNewName.Name = "lcGenerateNewName";
            this.lcGenerateNewName.Size = new System.Drawing.Size(175, 26);
            this.lcGenerateNewName.TextSize = new System.Drawing.Size(0, 0);
            this.lcGenerateNewName.TextVisible = false;
            // 
            // lcOverride
            // 
            this.lcOverride.Control = this.btOvewrite;
            this.lcOverride.Location = new System.Drawing.Point(424, 83);
            this.lcOverride.Name = "lcOverride";
            this.lcOverride.Size = new System.Drawing.Size(109, 26);
            this.lcOverride.TextSize = new System.Drawing.Size(0, 0);
            this.lcOverride.TextVisible = false;
            // 
            // lcRemember
            // 
            this.lcRemember.Control = this.ceRemember;
            this.lcRemember.Location = new System.Drawing.Point(0, 59);
            this.lcRemember.Name = "lcRemember";
            this.lcRemember.Size = new System.Drawing.Size(533, 24);
            this.lcRemember.TextSize = new System.Drawing.Size(0, 0);
            this.lcRemember.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 21);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(533, 38);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 83);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(172, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ExistingFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 129);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ExistingFileDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ExistingFile...";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGenerateNewName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRemember)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.CheckBox ceRemember;
        private DevExpress.XtraEditors.SimpleButton btGenerateNewName;
        private DevExpress.XtraEditors.SimpleButton btOvewrite;
        private DevExpress.XtraEditors.SimpleButton btSkip;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lcLabel;
        private DevExpress.XtraLayout.LayoutControlItem lcSkip;
        private DevExpress.XtraLayout.LayoutControlItem lcGenerateNewName;
        private DevExpress.XtraLayout.LayoutControlItem lcOverride;
        private DevExpress.XtraLayout.LayoutControlItem lcRemember;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}