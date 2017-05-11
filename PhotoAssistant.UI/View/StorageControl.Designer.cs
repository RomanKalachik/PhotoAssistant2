using DevExpress.XtraEditors;

namespace PhotoAssistant.UI.View {
    partial class StorageControl {
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.storageMediaControl = new StorageMediaControl();
            this.backupMediaControl = new StorageMediaControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.storageMediaControl);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.backupMediaControl);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(651, 423);
            this.splitContainerControl1.SplitterPosition = 325;
            this.splitContainerControl1.TabIndex = 7;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // storageMediaControl
            // 
            this.storageMediaControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storageMediaControl.Location = new System.Drawing.Point(0, 0);
            this.storageMediaControl.Name = "storageMediaControl";
            this.storageMediaControl.Size = new System.Drawing.Size(325, 423);
            this.storageMediaControl.TabIndex = 0;
            this.storageMediaControl.Text = "Storage Media";
            // 
            // backupMediaControl2
            // 
            this.backupMediaControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backupMediaControl.Location = new System.Drawing.Point(0, 0);
            this.backupMediaControl.Name = "backupMediaControl2";
            this.backupMediaControl.Size = new System.Drawing.Size(314, 423);
            this.backupMediaControl.TabIndex = 1;
            this.backupMediaControl.Text = "Backup Media";
            // 
            // StorageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "StorageControl";
            this.Size = new System.Drawing.Size(651, 423);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private StorageMediaControl storageMediaControl;
        private StorageMediaControl backupMediaControl;
    }
}
