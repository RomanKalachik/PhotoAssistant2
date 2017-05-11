namespace PhotoAssistant.UI.View.ImportControls {
    partial class FileExplorerControl {
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
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accPlacesItem = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accFilesItem = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accDevicesItem = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accPlacesItem,
            this.accFilesItem,
            this.accDevicesItem});
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.Size = new System.Drawing.Size(267, 510);
            this.accordionControl1.TabIndex = 0;
            this.accordionControl1.Text = "accordionControl1";
            // 
            // accPlacesItem
            // 
            this.accPlacesItem.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accPlacesItem.Text = "Places";
            // 
            // accFilesItem
            // 
            this.accFilesItem.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accFilesItem.Text = "Files";
            // 
            // accDevicesItem
            // 
            this.accDevicesItem.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accDevicesItem.Text = "Element1";
            // 
            // FileExplorerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.accordionControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FileExplorerControl";
            this.Size = new System.Drawing.Size(267, 510);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accFilesItem;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accPlacesItem;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accDevicesItem;
    }
}
