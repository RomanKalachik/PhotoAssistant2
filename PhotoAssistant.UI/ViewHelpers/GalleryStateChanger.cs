//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//
//using DevExpress.Utils.Drawing.Animation;
//using DevExpress.XtraGrid.Views.WinExplorer;
//using DevExpress.XtraGrid.Views.WinExplorer.ViewInfo;

//namespace PhotoAssistant.UI.ViewHelpers {
//    public class GalleryStateChanger : IWinExplorerViewStateChanger, ISupportXtraAnimation, ISupportXtraAnimationEx, IXtraAnimationListener {

//        public GalleryStateChanger(WinExplorerView view) {
//            View = view;
//            ViewInfo.StateChanger = this;
//        }

//        WinExplorerView View { get; set; }
//        WinExplorerItemInfoCollection CurrentItems { get; set; }
//        Dictionary<FileInfoModel, WinExplorerItemViewInfo> PrevItems { get; set; }

//        public StateChangeType State { get; set; }

//        void IWinExplorerViewStateChanger.ChangeState(WinExplorerItemInfoCollection prevItems, WinExplorerItemInfoCollection currentItems) {
//            ShouldChangeState = false;
//            CurrentItems = currentItems;
//            FirstTimeAnimation = true;

//            if(State == StateChangeType.CollapseGroup)
//                CollapseGroup();
//            else
//                ExpandGroup();
//        }

//        private void ExpandGroup() {

//        }

//        private void CollapseGroup() {
//            WinExplorerItemViewInfo groupInfo = GetItemInfo(File, CurrentItems);
//            if(groupInfo == null)
//                return;
//            foreach(FileInfoModel file in PrevItems.Keys) {
//                if(file == File)
//                    continue;
//                if(file.IsGrouped && file.GroupId == File.Id) {
//                    AddMoveHideAnimation(file, GetItemInfo(file, PrevItems), groupInfo);
//                }
//                else
//                    AddMoveAnimation(file, GetItemInfo(file, PrevItems), GetItemInfo(file, CurrentItems));
//            }
//        }

//        private void AddMoveAnimation(FileInfoModel file, WinExplorerItemViewInfo prevInfo, WinExplorerItemViewInfo newInfo) {
//            XtraAnimator.Current.AddAnimation(new ItemAnimationInfo(this, file, prevInfo, newInfo, false, false));
//        }

//        private WinExplorerItemViewInfo GetItemInfo(FileInfoModel file, Dictionary<FileInfoModel, WinExplorerItemViewInfo> items) {
//            return items[file];
//        }

//        private void AddMoveHideAnimation(FileInfoModel file, WinExplorerItemViewInfo prevInfo, WinExplorerItemViewInfo newinfo) {
//            XtraAnimator.Current.AddAnimation(new ItemAnimationInfo(this, file, prevInfo, newinfo, true, true));
//        }

//        private WinExplorerItemViewInfo GetItemInfo(FileInfoModel file, WinExplorerItemInfoCollection items) {
//            foreach(WinExplorerItemViewInfo item in items) {
//                FileInfoModel f = (FileInfoModel)View.GetRow(item.Row.RowHandle);
//                if(f.Id == file.Id)
//                    return item;
//            }
//            return null;
//        }

//        FileInfoModel File { get; set; }

//        public void PrepareGroupCollapse(FileInfoModel file) {
//            ViewInfo.StateChanger = this;
//            SaveState();
//            File = file;
//            ShouldChangeState = true;
//            State = StateChangeType.CollapseGroup;
//        }

//        public void PrepareGroupExpand(FileInfoModel file) {
//            ViewInfo.StateChanger = this;
//            SaveState();
//            File = file;
//            ShouldChangeState = true;
//            State = StateChangeType.ExpandGroup;
//        }

//        WinExplorerViewInfo ViewInfo { get { return (WinExplorerViewInfo)View.GetViewInfo(); } }
//        List<FileInfoModel> Files { get; set; }
//        public void SaveState() {
//            ViewInfo.SaveItems();
//            PrevItems = new Dictionary<FileInfoModel, WinExplorerItemViewInfo>();
//            foreach(WinExplorerItemViewInfo itemInfo in ViewInfo.VisibleItems) {
//                FileInfoModel file = (FileInfoModel)View.GetRow(itemInfo.Row.RowHandle);
//                PrevItems.Add(file, itemInfo);
//            }
//        }

//        bool IWinExplorerViewStateChanger.IsStateChanged {
//            get { return XtraAnimator.Current.Animations.GetAnimationsCountByObject(this) == 0; }
//        }

//        bool IWinExplorerViewStateChanger.ShouldDrawItem(WinExplorerItemViewInfo itemInfo) {
//            if(State == StateChangeType.CollapseGroup) {
//                FileInfoModel file = (FileInfoModel)itemInfo.RowData;
//                if(PrevItems.ContainsKey(file) && CurrentItems.Contains(itemInfo))
//                    return false;
//            }
//            return true;
//        }

//        public bool ShouldChangeState { get; set; }

//        bool IWinExplorerViewStateChanger.ShouldChangeState {
//            get { return ShouldChangeState; }
//        }

//        bool ISupportXtraAnimation.CanAnimate {
//            get { return true; }
//        }

//        Control ISupportXtraAnimation.OwnerControl {
//            get { return View.GridControl; }
//        }

//        void ISupportXtraAnimationEx.OnEndAnimation(BaseAnimationInfo info) {

//        }

//        void ISupportXtraAnimationEx.OnFrameStep(BaseAnimationInfo info) {

//        }

//        bool FirstTimeAnimation { get; set; }

//        void IXtraAnimationListener.OnAnimation(BaseAnimationInfo info) {
//            ItemAnimationInfo finfo = info as ItemAnimationInfo;
//            if(finfo != null) {
//                if(FirstTimeAnimation)
//                    View.GridControl.Invalidate(finfo.Bounds);
//                else
//                    View.GridControl.Invalidate();
//                FirstTimeAnimation = false;
//                View.GridControl.Update();
//            }
//        }

//        void IXtraAnimationListener.OnEndAnimation(BaseAnimationInfo info) {
//            View.GridControl.Invalidate();
//            View.GridControl.Update();
//        }
//    }

//    public class ItemAnimationInfo : FloatAnimationInfo {
//        static int ItemAnimationLength = 400;
//        public ItemAnimationInfo(ISupportXtraAnimation owner, FileInfoModel file, WinExplorerItemViewInfo prevInfo, WinExplorerItemViewInfo currentInfo, bool useOpacity, bool disappear)
//            : base(owner, file, ItemAnimationLength, 0.0f, 1.0f, true) {
//            PrevInfo = prevInfo;
//            CurrentInfo = currentInfo;
//            FinalBounds = CurrentInfo.Bounds;
//            Disappear = disappear;
//            UseOpacity = useOpacity;
//            Bounds = PrevInfo.Bounds;
//        }
//        bool UseOpacity { get; set; }
//        bool Disappear { get; set; }
//        Rectangle FinalBounds { get; set; }
//        public Rectangle Bounds { get; private set; }
//        WinExplorerItemViewInfo PrevInfo { get; set; }
//        public WinExplorerItemViewInfo CurrentInfo { get; set; }

//        protected override void FrameStepCore(float k) {
//            base.FrameStepCore(k);

//            Rectangle res = new Rectangle(
//                PrevInfo.Bounds.X + (int)((FinalBounds.X - PrevInfo.Bounds.X) * k),
//                PrevInfo.Bounds.Y + (int)((FinalBounds.Y - PrevInfo.Bounds.Y) * k),
//                FinalBounds.Width, FinalBounds.Height);
//            if(Disappear) {
//                PrevInfo.SetLocation(res.Location);
//                if(UseOpacity) PrevInfo.Opacity = 1.0f - k;
//            }
//            else {
//                PrevInfo.SetLocation(res.Location);
//                if(UseOpacity) PrevInfo.Opacity = k;
//            }
//            int minX = Math.Min(Bounds.X, res.X);
//            int minY = Math.Min(Bounds.Y, res.Y);
//            int width = Math.Max(Bounds.Right, res.Right) - minX;
//            int height = Math.Max(Bounds.Bottom, res.Bottom) - minY;
//            Bounds = new Rectangle(minX, minY, width, height);
//        }
//    }

//    public enum StateChangeType { ExpandGroup, CollapseGroup }
//}
