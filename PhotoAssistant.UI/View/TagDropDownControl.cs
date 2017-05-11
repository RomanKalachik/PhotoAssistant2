using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.UI.View {
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using DevExpress.Data.Filtering;
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Popup;
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Columns;
    using DevExpress.XtraTreeList.Nodes;
    using DevExpress.XtraTreeList.Nodes.Operations;
    using PhotoAssistant.Core.Model;

    namespace TokenEditTest {
        public partial class TagTokenEditDropDownControl : CustomTokenEditDropDownControlBase {
            private CustomTreeList treeList;
            private DevExpress.XtraTreeList.Columns.TreeListColumn colData;
            public TagTokenEditDropDownControl() {
                InitializeComponent();
            }
            private void InitializeComponent() {
                this.treeList = new TokenEditTest.CustomTreeList();
                this.colData = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
                this.SuspendLayout();
                this.treeList.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { this.colData });
                this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
                this.treeList.Location = new System.Drawing.Point(0, 0);
                this.treeList.Name = "treeList";
                this.treeList.OptionsBehavior.Editable = false;
                this.treeList.OptionsBehavior.EnableFiltering = true;
                this.treeList.OptionsBehavior.ReadOnly = true;
                this.treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
                this.treeList.OptionsView.ShowIndentAsRowStyle = true;
                this.treeList.Size = new System.Drawing.Size(331, 209);
                this.treeList.TabIndex = 0;
                this.treeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.OnTreeListFocusedNodeChanged);
                this.treeList.DoubleClick += new System.EventHandler(this.OnTreeListDoubleClick);
                this.treeList.KeyFieldName = "Id";
                this.treeList.ParentFieldName = "ParentId";
                this.colData.Caption = "Data";
                this.colData.FieldName = "Text";
                this.colData.Name = "colData";
                this.colData.Visible = true;
                this.colData.VisibleIndex = 0;
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.Controls.Add(this.treeList);
                this.Name = "CustomTokenEditPopupControl";
                this.Size = new System.Drawing.Size(331, 209);
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
                this.ResumeLayout(false);
            }
            public override void Initialize(TokenEdit ownerEdit, TokenEditPopupForm ownerPopup) {
                base.Initialize(ownerEdit, ownerPopup);
            }
            public override void InitializeAppearances(AppearanceObject appearanceDropDown) {
                base.InitializeAppearances(appearanceDropDown);
            }
            DmModel model;
            public DmModel Model {
                get { return model; }
                set {
                    if(Model == value)
                        return;
                    model = value;
                    OnModelChaged();
                }
            }
            private void OnModelChaged() {
                TreeList.DataSource = Model.GetTagNodeList(TagType);
            }
            public override void SetDataSource(object dataSource) {
                TreeList.ExpandAll();
            }
            string currentFilter = string.Empty;
            public override void SetFilter(string filter, string columnName) {
                this.currentFilter = filter;
                TreeList.ActiveFilterCriteria = new FunctionOperator(FunctionOperatorType.Contains,
                    new FunctionOperator(FunctionOperatorType.Lower, new OperandProperty("Text")),
                    new ConstantValue(filter.Trim().ToLower())
                    );
            }
            public override void OnShowingPopupForm() {
                base.OnShowingPopupForm();
            }
            public override int GetItemCount() {
                return TreeList.VisibleNodesCount;
            }
            public override int CalcFormWidth() {
                return 300;
            }
            public override int CalcFormHeight(int itemCount) {
                return 200;
            }
            public override object GetResultValue() {
                DmTagNode node = GetSelectedDataItem() as DmTagNode;
                if(node == null)
                    return null;
                foreach(TokenEditToken token in OwnerEdit.Properties.Tokens)
                    if(token.Value == node.Tag)
                        return token;
                return null;
            }
            public override bool IsTokenSelected {
                get { return this.selNode != null; }
            }
            public override void ResetSelection() {
                this.selNode = null;
                TreeList.FocusedNode = null;
            }
            public override void ProcessKeyDown(KeyEventArgs e) {
                base.ProcessKeyDown(e);
                TreeList.DoKeyDown(e);
            }
            public override void OnEditorMouseWheel(MouseEventArgs e) {
                int step = SystemInformation.MouseWheelScrollLines > 0 ? SystemInformation.MouseWheelScrollLines : 1;
                if(e.Delta > 0) step *= -1;
                TreeList.TopVisibleNodeIndex += step;
            }
            public override void ResetResultValue() {
                this.selNode = null;
            }
            #region TreeList Handlers
            TreeListNode selNode = null;
            void OnTreeListFocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e) {
                this.selNode = e.Node;
            }
            void OnTreeListDoubleClick(object sender, EventArgs e) {
                TreeListHitInfo hi = TreeList.CalcHitInfo(TreeList.PointToClient(Control.MousePosition));
                if(hi.HitInfoType == HitInfoType.Cell && GetSelectedDataItem() is DmTagNode) {
                    OwnerEdit.ClosePopup(PopupCloseMode.Normal);
                }
            }
            object GetSelectedDataItem() { return this.selNode != null ? TreeList.GetDataRecordByNode(this.selNode) : null; }
            #endregion
            protected CustomTreeList TreeList { get { return treeList; } }
            public TagType TagType { get; set; }
        }

        public class CustomTreeList : TreeList {
            public CustomTreeList() {
                BorderStyle = BorderStyles.NoBorder;
                SetStyle(ControlStyles.UserMouse, false);
                OptionsBehavior.Editable = false;
                OptionsView.ShowColumns = false;
                OptionsView.ShowHorzLines = false;
                OptionsView.ShowVertLines = false;
                OptionsView.ShowIndicator = false;
                OptionsBehavior.EnableFiltering = true;
                OptionsBehavior.ReadOnly = true;
                OptionsSelection.EnableAppearanceFocusedCell = false;
                OptionsView.ShowIndentAsRowStyle = true;
                OptionsFilter.FilterMode = FilterMode.Extended;
                OptionsFilter.AllowFilterEditor = false;
                OptionsFilter.ShowAllValuesInFilterPopup = false;
                OptionsFilter.ShowAllValuesInCheckedFilterPopup = false;
            }
            public void DoKeyDown(KeyEventArgs e) { OnKeyDown(e); }
        }
    }
}
