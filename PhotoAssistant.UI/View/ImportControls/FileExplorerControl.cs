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
using System.IO;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;
using PhotoAssistant.Core;

namespace PhotoAssistant.UI.View.ImportControls {
    public partial class FileExplorerControl : XtraUserControl {
        public string SourcePath { get; private set; }
        public event EventHandler SourcePathCanged;
        Dictionary<string, string> _customCaptions = new Dictionary<string, string>();
        List<string> _wiaIDs = new List<string>();

        public FileExplorerControl() {
            InitializeComponent();
            Explore();
        }

        public void Explore() {
            ClearAll();
            LoadDrives();
            LoadPlaces();
            LoadDevices();
            //accFilesItem.Expanded = true;
            //accPlacesItem.Expanded = true;
        }

        void LoadDevices() {
            var devices = WIAHelper.Default.GetWIADevices();
            if(devices.Count <= 0) {
                accDevicesItem.Visible = false;
                return;
            }
            foreach(var device in devices) {
                _wiaIDs.Add(device.Id);
                _customCaptions.Add(device.Id, device.DisplayName);
            }
            CreateItem(accDevicesItem, _wiaIDs);
        }

        void LoadDrives() {
            var drives = Directory.GetLogicalDrives();
            CreateItem(accFilesItem, drives.ToList());
        }

        void LoadPlaces() {
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");

            _customCaptions.Add(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Desktop");
            _customCaptions.Add(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "My Documents");
            _customCaptions.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "My Pictures");
            _customCaptions.Add(pathDownload, "Download");

            var paths = _customCaptions.Keys.ToList();
            CreateItem(accPlacesItem, paths);
        }

        void CreateItem(AccordionControlElement parent, List<string> paths) {
            var container = CreateContainerWithTree(paths);
            if(container != null) {
                accordionControl1.Controls.Add(container);
                parent.ContentContainer = container;
            }
        }

        AccordionContentContainer CreateContainerWithTree(List<string> paths) {
            var res = new AccordionContentContainer();
            var tree = CreateTree(paths);
            if(tree == null) return null;

            tree.Dock = DockStyle.Fill;
            res.Height = 350;
            res.Controls.Add(tree);
            return res;
        }

        TreeList CreateTree(List<string> paths) {
            paths = GetValidPaths(paths);
            var res = new TreeList();
            res.DataSource = new TreeListRoots(paths);
            res.OptionsView.ShowColumns = false;
            res.OptionsView.ShowHorzLines = false;
            res.OptionsView.ShowVertLines = false;
            res.OptionsView.ShowIndicator = false;
            res.OptionsBehavior.AllowExpandOnDblClick = true;
            res.OptionsBehavior.AllowPixelScrolling = DefaultBoolean.True;
            res.OptionsBehavior.Editable = false;
            res.BorderStyle = BorderStyles.NoBorder;
            res.Columns.Add(new TreeListColumn()
            {
                Name = "Name",
                Visible = true
            });

            SubscribeTreeEvents(res);
            return res;
        }

        List<string> GetValidPaths(List<string> paths) {
            var res = new List<string>();
            foreach(string path in paths) {
                if(IsWIAPath(path)) {
                    res.Add(path);
                    continue;
                }
                try { var rootDirs = Directory.GetDirectories(path); }
                catch { continue; } // checking drive is ready
                res.Add(path);
            }
            return res;
        }

        bool IsWIAPath(string path) {
            try {
                Path.GetFullPath(path);
            }
            catch { return true; }
            return false;
        }

        void VirtualTreeGetChildNodes(object sender, VirtualTreeGetChildNodesInfo e) {
            try {
                if(e.Node is TreeListRoots) {
                    e.Children = (e.Node as TreeListRoots).Roots;
                    return;
                }
                string path = (string)e.Node;
                if(Directory.Exists(path)) {
                    string[] dirs = Directory.GetDirectories(path);
                    e.Children = dirs;
                }
                else e.Children = new object[] { };
            }
            catch { e.Children = new object[] { }; }
        }

        void VirtualTreeGetCellValue(object sender, VirtualTreeGetCellValueInfo e) {
            string caption;
            if(_customCaptions.TryGetValue((string)e.Node, out caption)) {
                e.CellData = caption;
                return;
            }

            DirectoryInfo di = new DirectoryInfo((string)e.Node);
            if(e.Column.Name == "Name")
                e.CellData = di.Name;
            else
                e.CellData = null;
        }

        void Tree_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e) {
            if(e.Node == null) return;
            string path = e.Node.TreeList.GetDataRecordByNode(e.Node) as string;
            SetSourcePath(path);
        }

        void SetSourcePath(string path) {
            if(this.SourcePath == path) return;
            this.SourcePath = path;
            RaiseSourcePathChanded(path);
        }

        void RaiseSourcePathChanded(string path) {
            if(this.SourcePathCanged != null)
                this.SourcePathCanged(this, new EventArgs());
        }

        void ClearAll() {
            foreach(var element in accordionControl1.Elements) {
                if(element.ContentContainer != null && element.ContentContainer.Controls.Count > 0)
                    UnSubscribeTreeEvents(element.ContentContainer.Controls[0] as TreeList);
            }
            accFilesItem.Elements.Clear();
            accPlacesItem.Elements.Clear();
            _customCaptions.Clear();
        }

        void SubscribeTreeEvents(TreeList tree) {
            if(tree == null) return;
            tree.VirtualTreeGetCellValue += VirtualTreeGetCellValue;
            tree.VirtualTreeGetChildNodes += VirtualTreeGetChildNodes;
            tree.FocusedNodeChanged += Tree_FocusedNodeChanged;
        }

        void UnSubscribeTreeEvents(TreeList tree) {
            if(tree == null) return;
            tree.VirtualTreeGetCellValue -= VirtualTreeGetCellValue;
            tree.VirtualTreeGetChildNodes -= VirtualTreeGetChildNodes;
            tree.FocusedNodeChanged -= Tree_FocusedNodeChanged;
        }
    }

    class TreeListRoots {
        public TreeListRoots(List<string> roots) {
            this.Roots = roots;
        }
        public List<string> Roots { get; private set; }
    }
}
