namespace PhotoAssistant.UI.View {
    partial class StorageForm {
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
            this.storageControl1 = new StorageControl();
            this.SuspendLayout();
            // 
            // storageControl1
            // 
            this.storageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storageControl1.Location = new System.Drawing.Point(0, 0);
            this.storageControl1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.storageControl1.Name = "storageControl1";
            this.storageControl1.Size = new System.Drawing.Size(689, 484);
            this.storageControl1.TabIndex = 0;
            // 
            // StorageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 484);
            this.Controls.Add(this.storageControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "StorageForm";
            this.Text = "Manage Storage";
            this.ResumeLayout(false);

        }

        #endregion

        private StorageControl storageControl1;
    }
}