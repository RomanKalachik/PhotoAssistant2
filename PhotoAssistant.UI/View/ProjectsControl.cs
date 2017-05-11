using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Tile;
using System.IO;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.View {
    public partial class ProjectsControl : ViewControlBase {
        public ProjectsControl(DmModel model, MainForm mainForm)
            : this() {
            Model = model;
            MainForm = mainForm;
        }
        public ProjectsControl() {
            InitializeComponent();

            tileControl1.ContextButtonClick += tileControl1_ContextButtonClick;
            tileControl1.ContextButtonCustomize += tileControl1_ContextButtonCustomize;
            tileControl1.ItemClick += tileControl1_ItemClick;
        }

        void tileControl1_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e) {
            if(e.Item.Name == "removeButton") {
                TileItem item = e.DataItem as TileItem;
                if(item != null && item.Tag is ProjectInfo)
                    RemoveProject(item.Tag as ProjectInfo);
            } else if(e.Item.Name == "editButton") {
                TileItem item = e.DataItem as TileItem;
                if(item != null && item.Tag is ProjectInfo)
                    EditProjectSettings(item.Tag as ProjectInfo);
            }
        }

        void tileControl1_ItemClick(object sender, TileItemEventArgs e) {
            if(e.Item == itemNewProj) {
                CreateNewProject();
                return;
            }
            if(e.Item == itemOpenProj) {
                OpenProject();
                return;
            }
            if(e.Item != null && e.Item.Tag is ProjectInfo) {
                OpenProject(e.Item.Tag as ProjectInfo);
                return;
            }
        }

        public static ProjectInfo CreateNewProjectCore(IWin32Window owner) {
            ProjectPropertiesForm form = new ProjectPropertiesForm();
            if(form.ShowDialog(owner) != DialogResult.OK)
                return null;
            ProjectInfo info = SettingsStore.Default.GetProject(form.ProjectFileName);
            if(info != null)
                SettingsStore.Default.Projects.Remove(info);
            info = new ProjectInfo();
            info.Name = form.ProjectName;
            info.FileName = form.ProjectFileName;
            info.Description = form.ProjectDescription;

            return info;
        }

        public static ProjectInfo OpenNewProjectCore(IWin32Window owner) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "PhotoAssistant projects (*.phas)|*.phas|All files (*.*)|*.*";
            dlg.FilterIndex = 0;
            dlg.CheckFileExists = true;
            if(dlg.ShowDialog() != DialogResult.OK)
                return null;

            ProjectInfo info = SettingsStore.Default.GetProject(dlg.FileName);
            if(info != null)
                return info;
            info = new ProjectInfo();
            info.FileName = dlg.FileName;
            return info;

            //ProjectPropertiesForm form = new ProjectPropertiesForm();
            //form.IsOpenProject = true;
            //if(form.ShowDialog(owner) != DialogResult.OK)
            //    return null;
            //ProjectInfo info = SettingsStore.Default.GetProject(form.ProjectFileName);
            //if(info == null) {
            //    info = new ProjectInfo();
            //    info.FileName = form.ProjectFileName;
            //}
            //info.Name = form.ProjectName;
            //info.Description = form.ProjectDescription;

            //return info;
        }

        void CreateNewProject() {
            ProjectInfo info = CreateNewProjectCore(this);
            if(info == null)
                return;
            MainForm.LibraryControl.OpenProject(info);
            MainForm.ActivateLibraryControl();
        }

        private void ReadProjectPropertiesFromDataSource(ProjectInfo activeProject) {
            SettingsStore.Default.ActiveProject.Name = Model.Properties.ProjectName;
            SettingsStore.Default.ActiveProject.FileCount = Model.Properties.ProjectFileCount;
            SettingsStore.Default.ActiveProject.Id = Model.Properties.ProjectId;
            SettingsStore.Default.ActiveProject.OpenCount = Model.Properties.ProjectOpenCount;
            SettingsStore.Default.ActiveProject.Description = Model.Properties.ProjectDescription;
        }

        public void OpenProject() {
            ProjectInfo info = OpenNewProjectCore(this);
            if(info == null)
                return;
            bool shouldReadParamsFromDatabase = false;
            if(!SettingsStore.Default.Projects.Contains(info)) {
                shouldReadParamsFromDatabase = true;
            }
            MainForm.LibraryControl.OpenProject(info);
            MainForm.ActivateLibraryControl();
            if(!shouldReadParamsFromDatabase) {
                ReadProjectPropertiesFromDataSource(info);
            }
        }

        void OpenProject(ProjectInfo info) {
            MainForm.LibraryControl.OpenProject(info);
            MainForm.ActivateLibraryControl();
        }

        void RemoveProject(ProjectInfo info) {
            if(XtraMessageBox.Show("Remove project form recent list?", "Projects", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            ITileItem item = GetTileItem(info);
            SettingsStore.Default.Projects.Remove(info);
            this.recentTileGroup.Items.Remove((TileItem)item);
        }

        private void EditProjectSettings(ProjectInfo projectInfo) {
            ProjectPropertiesForm form = new ProjectPropertiesForm();
            form.EnableSelectLocation = false;
            form.ProjectName = projectInfo.Name;
            form.ProjectFileName = projectInfo.FileName;
            form.ProjectDescription = projectInfo.Description;
            if(form.ShowDialog(this) != DialogResult.OK)
                return;
            projectInfo.Name = form.ProjectName;
            projectInfo.Description = form.ProjectDescription;
            SettingsStore.Default.SaveToXml();
            MainForm.LibraryControl.UpdateFormCaption();
            UpdateTileItem(projectInfo);
        }

        private void UpdateTileItem(ProjectInfo projectInfo) {
            TileItem item = (TileItem)GetTileItem(projectInfo);
            InititalizeFrames(item, projectInfo);
        }

        private ITileItem GetTileItem(ProjectInfo info) {
            return this.recentTileGroup.Items.FirstOrDefault((i) => i.Tag == info);
        }

        void tileControl1_ContextButtonCustomize(object sender, TileContextButtonCustomizeEventArgs e) {
            if(e.TileItem == itemNewProj || e.TileItem == itemOpenProj) {
                e.Item.Visibility = DevExpress.Utils.ContextItemVisibility.Hidden;
            }
        }

        TileGroup recentTileGroup, startTileGroup;
        TileItem itemNewProj, itemOpenProj;

        public void UpdateTileView() {
            this.tileControl1.Groups.Clear();
            this.tileControl1.Groups.Add(startTileGroup = new TileGroup() { Text = "Start" });
            this.tileControl1.Groups.Add(recentTileGroup = new TileGroup() { Text = "Recent" });

            InitStartTiles();
            InitRecentTiles();
            tileControl1.AnimateArrival = true;
            StartRecentItemsAnimation();
        }

        void StartRecentItemsAnimation() {
            foreach(TileItem item in recentTileGroup.Items) {
                item.SetContent(item.Frames[0], false);
                item.StartAnimation();
            }
        }

        void InitRecentTiles() {
            foreach(ProjectInfo info in SettingsStore.Default.Projects) {
                this.recentTileGroup.Items.Add(CreateRecentTileItem(info));
            }
        }

        void InitStartTiles() {
            if(startTileGroup == null) return;
            this.startTileGroup.Items.Add(itemNewProj = CreateStartItem("New Project...", Color.FromArgb(43, 87, 151)));
            this.startTileGroup.Items.Add(itemOpenProj = CreateStartItem("Open Project...", Color.FromArgb(126, 56, 120)));
        }

        TileItem CreateStartItem(string text, Color backColor) {
            TileItem item = new TileItem() { ItemSize = TileItemSize.Wide };
            item.AppearanceItem.Normal.BackColor = backColor;

            TileItemElement elemText = CreateBottomTextElement(text);
            item.Elements.Add(elemText);
            return item;
        }

        TileItemElement CreateBottomTextElement(string text) {
            TileItemElement elemText = new TileItemElement();
            elemText.TextAlignment = TileItemContentAlignment.BottomLeft;
            elemText.TextLocation = new Point(10, 0);
            elemText.Text = text;
            elemText.Height = 40;
            elemText.Appearance.Normal.FontSizeDelta = 2;
            elemText.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
            elemText.Appearance.Normal.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            elemText.Appearance.Normal.ForeColor = Color.FromArgb(255, 220, 220, 220);
            elemText.Appearance.Normal.BackColor = Color.FromArgb(180, Color.Black);
            elemText.StretchHorizontal = true;
            elemText.AnimateTransition = DevExpress.Utils.DefaultBoolean.False;
            return elemText;
        }

        TileItemElement CreateFileCountElement(int count) {
            TileItemElement elemCount = new TileItemElement();
            elemCount.TextAlignment = TileItemContentAlignment.BottomRight;
            elemCount.TextLocation = new Point(-10, 0);
            elemCount.Text = count.ToString();
            elemCount.Height = 40;
            elemCount.Appearance.Normal.ForeColor = Color.FromArgb(255, 220, 220, 220);
            elemCount.Appearance.Normal.FontSizeDelta = 2;
            elemCount.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
            elemCount.Appearance.Normal.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            elemCount.StretchHorizontal = true;
            elemCount.AnimateTransition = DevExpress.Utils.DefaultBoolean.False;
            return elemCount;
        }

        private ITileItem CreateRecentTileItem(ProjectInfo info) {
            TileItem item = new TileItem() {
                ItemSize = TileItemSize.Large,
                Tag = info
            };
            InititalizeFrames(item, info);
            return item;
        }

        private void InititalizeFrames(TileItem item, ProjectInfo info) {
            item.Frames.Clear();

            for(int i = 1; i <= 4; i++) {
                TileItemFrame frame = new TileItemFrame();
                frame.Animation = GetRandomAnimationType();
                frame.Interval = GetRandomAnimationInterval();
                TileItemElement elemText = CreateBottomTextElement(info.Name);
                TileItemElement elemCount = CreateFileCountElement(info.FileCount);
                TileItemElement elemImage = new TileItemElement();

                elemImage.ImageScaleMode = TileItemImageScaleMode.ZoomOutside;
                elemImage.Image = GetProjectThumb(info, i);

                frame.Elements.Add(elemText);
                frame.Elements.Add(elemCount);
                frame.Elements.Add(elemImage);

                item.Frames.Add(frame);
            }
            item.ResetSuperTip();
            if(!string.IsNullOrEmpty(info.Description)) {
                var tooltip = new DevExpress.Utils.SuperToolTip();
                tooltip.Items.Add(info.Description);
                item.SuperTip = tooltip;
            }
        }

        int lastAnimationType = 0;
        TileItemContentAnimationType GetRandomAnimationType() {
            var values = Enum.GetValues(typeof(TileItemContentAnimationType));
            int result = rnd.Next(1, values.Length);
            if(result == lastAnimationType) {
                result++;
                result = result > values.Length - 1 ? 1 : result;
            }
            lastAnimationType = result;
            return (TileItemContentAnimationType)values.GetValue(result);
        }

        int GetRandomAnimationInterval() {
            return rnd.Next(3000, 7000);
        }

        Random rnd = new Random();
        const string strThumbImage = "ThumbImage";
        const string strThumbFileName = "ThumbFileName";

        Image GetProjectThumb(ProjectInfo info, int index) {
            string imgPropName = strThumbImage;
            string filePropName = strThumbFileName;
            if(index > 1) {
                imgPropName += index.ToString();
                filePropName += index.ToString();
            }
            var thumbImgProp = (typeof(ProjectInfo)).GetProperty(imgPropName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            var fileNameProp = (typeof(ProjectInfo)).GetProperty(filePropName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if(thumbImgProp == null || filePropName == null)
                return null;

            Image img = (Image)thumbImgProp.GetValue(info);
            string fileName = (String)fileNameProp.GetValue(info);

            if(img != null)
                return img;
            else if(System.IO.File.Exists(fileName)) {
                var newImg = Image.FromFile(fileName);
                thumbImgProp.SetValue(info, newImg);
                return newImg;
            }

            return null;
        }

        public override void OnShowView() {
            base.OnShowView();
            UpdateTileView();
        }

        protected override bool AllowQuickGalleryPanel {
            get { return false; }
        }
    }
}
