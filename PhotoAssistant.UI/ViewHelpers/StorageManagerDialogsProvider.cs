


using DevExpress.XtraEditors;
using PhotoAssistant.Core;
using PhotoAssistant.Core.Model;
using PhotoAssistant.UI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoAssistant.UI.ViewHelpers {
    public class StorageManagerDialogsProvider : IStorageManagerDialogsProvider {
        static StorageManagerDialogsProvider defaultProvider;
        public static StorageManagerDialogsProvider Default {
            get {
                if(defaultProvider == null)
                    defaultProvider = new StorageManagerDialogsProvider() { Manager = StorageManager.Default };
                return defaultProvider;
            }
        }

        StorageManager manager;
        public StorageManager Manager {
            get { return manager; }
            set {
                if(Manager == value)
                    return;
                StorageManager prev = Manager;
                manager = value;
                OnManagerChanged(prev);
            }
        }

        private void OnManagerChanged(StorageManager prevManager) {
            if(prevManager != null)
                prevManager.DialogsProvider = null;
            if(Manager != null)
                Manager.DialogsProvider = this;
        }

        protected virtual DialogResult ShowDialog(StorageManagerDialogAction action, string message, MessageBoxButtons buttons, MessageBoxIcon icon) {
            return XtraMessageBox.Show(message, SettingsStore.ApplicationName, buttons, icon);
        }

        protected virtual void DisplayBackupNotPresentTaskCore() {
            DialogResult res = DialogResult.OK;
            while(res == DialogResult.OK) {
                res = ShowDialog(StorageManagerDialogAction.DisplayBackupNotPresentTask, "There is no backup media present on your system. Please attach one of the following media devices to your computer and press OK. Or press Cancel to skip backup: " + Manager.GetStorageMediaList(Manager.Backup, true),
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayBackupNotPresentTask, DialogResult = res });
                if(Manager.IsAnyBackupStoragePresent)
                    break;
            }
        }
        void IStorageManagerDialogsProvider.DisplayBackupNotPresentTask() {
            DisplayBackupNotPresentTaskCore();
        }

        protected virtual void DisplayNoBackupTaskCore() {
            while(!Manager.HasAnyBackup) {
                DialogResult res = ShowDialog(
                    StorageManagerDialogAction.DisplayNoBackupTask,
                    "There is no backup media specified. Backup media is used to duplicate your files and prevent them from losing. Would you like to add backup media now?",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                    );
                AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayNoBackupTask, DialogResult = res });
                if(res != DialogResult.Yes)
                    break;
                ShowStorageForm();
            }
        }
        void IStorageManagerDialogsProvider.DisplayNoBackupTask() {
            DisplayNoBackupTaskCore();
        }

        protected virtual void DisplayNoStorageTaskCore() {
            DialogResult res = DialogResult.Yes;
            while(!Manager.HasAnyStorage) {
                res = ShowDialog(
                    StorageManagerDialogAction.DisplayNoStorageTask,
                    "There is no storage media specified. Storage media is used to store your files and prevent them from losing. Would you like to add storage media now?",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayNoStorageTask, DialogResult = res });
                if(res != DialogResult.Yes)
                    break;
                ShowStorageForm();
            }
        }
        void IStorageManagerDialogsProvider.DisplayNoStorageTask() {
            DisplayNoStorageTaskCore();
        }

        protected virtual void DisplayOperationCanceledTaskCore() {
            DialogResult res = ShowDialog(
                StorageManagerDialogAction.DisplayOperationCanceledTask,
                "There is no storage media where files can be copied. Importing will be canceled.",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayOperationCanceledTask, DialogResult = res });
        }
        void IStorageManagerDialogsProvider.DisplayOperationCanceledTask() {
            DisplayOperationCanceledTaskCore();
        }


        protected virtual void DisplayStorageNotPresentTaskCore() {
            DialogResult res = DialogResult.OK;
            while(true) {
                res = ShowDialog(
                    StorageManagerDialogAction.DisplayStorageNotPresentTask,
                    "There is no storage media present on your system. Please attach one of the following media devices to your computer and press OK. Or press Cancel to skip backup: " + Manager.GetStorageMediaList(Manager.Storage, true),
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayStorageNotPresentTask, DialogResult = res });
                if(res != DialogResult.OK)
                    break;
                if(Manager.IsAnyStoragePresent)
                    break;
            }
        }
        void IStorageManagerDialogsProvider.DisplayStorageNotPresentTask() {
            DisplayStorageNotPresentTaskCore();
        }

        public virtual void ShowStorageForm() {
            StorageForm form = new StorageForm();
            form.Manager = Manager;
            form.ShowDialog();
        }

        protected virtual bool DisplayAddNewDevicesToStorageMessageCore() {
            DialogResult res = DialogResult.Yes;
            while(true) {
                res = ShowDialog(
                    StorageManagerDialogAction.DisplayAddNewDevicesToStorageMessage,
                    "All specified storage media are full. Please add new storage media.",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayAddNewDevicesToStorageMessage, DialogResult = res });
                if(res != DialogResult.Yes)
                    return false;
                Manager.SaveStorageState();
                ShowStorageForm();
                if(Manager.StorageStateChanged())
                    break;
            }
            return true;
        }
        bool IStorageManagerDialogsProvider.DisplayAddNewDevicesToStorageMessage() {
            return DisplayAddNewDevicesToStorageMessageCore();
        }

        protected virtual bool DisplayAddStorageDialogCore() {
            AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayAddStorageDialog });
            return false;
        }
        bool IStorageManagerDialogsProvider.DisplayAddStorageDialog() {
            return DisplayAddStorageDialogCore();
        }

        protected virtual bool DisplayAttachNonPresentStorageDevicesCore() {
            DialogResult res = DialogResult.OK;
            res = ShowDialog(
                StorageManagerDialogAction.DisplayAttachNonPresentStorageDevices,
                "Some storage devices not attached to your computer. Please attach one of the following storage media devices to your computer and press OK." + Manager.GetStorageMediaList(Manager.Storage, true),
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayAttachNonPresentStorageDevices, DialogResult = res });
            return res == DialogResult.OK;
        }
        bool IStorageManagerDialogsProvider.DisplayAttachNonPresentStorageDevices() {
            return DisplayAttachNonPresentStorageDevicesCore();
        }

        protected virtual bool DisplayAddNewDevicesToBackupMessageCore() {
            DialogResult res = DialogResult.Yes;
            while(true) {
                res = ShowDialog(
                    StorageManagerDialogAction.DisplayAddNewDevicesToBackupMessage,
                    "All specified backup media are full. Please add new backup media.",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayAddNewDevicesToBackupMessage, DialogResult = res });
                if(res != DialogResult.Yes)
                    return false;
                Manager.SaveBackupState();
                ShowStorageForm();
                if(Manager.BackupStateChanged())
                    break;
            }
            return true;
        }
        bool IStorageManagerDialogsProvider.DisplayAddNewDevicesToBackupMessage() {
            return DisplayAddNewDevicesToBackupMessageCore();
        }

        protected virtual bool DisplayAddBackupDialogCore() {
            AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayAddBackupDialog });
            throw new NotImplementedException();
        }
        bool IStorageManagerDialogsProvider.DisplayAddBackupDialog() {
            return DisplayAddBackupDialogCore();
        }

        protected virtual bool DisplayAttachNonPresentBackupDevicesCore() {
            DialogResult res = DialogResult.OK;
            res = ShowDialog(
                StorageManagerDialogAction.DisplayAttachNonPresentBackupDevices,
                "Some storage devices not attached to your computer. Please attach one of the following backup media devices to your computer and press OK." + Manager.GetStorageMediaList(Manager.Backup, true),
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayAttachNonPresentBackupDevices, DialogResult = res });
            return res == DialogResult.OK;
        }
        bool IStorageManagerDialogsProvider.DisplayAttachNonPresentBackupDevices() {
            return DisplayAttachNonPresentBackupDevicesCore();
        }

        protected virtual DialogResult DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsedCore() {
            DialogResult res = ShowDialog(
                StorageManagerDialogAction.DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsed,
                "This path is not recommended for using, because device containing this path is already used as storage or backup media. Do you still want to add this device as storage media?",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsed, DialogResult = res });
            return res;
        }
        DialogResult IStorageManagerDialogsProvider.DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsed() {
            return DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsedCore();
        }

        protected virtual void DisplayVolumeIsNotCorrectCore(string root) {
            DialogResult res = ShowDialog(
                StorageManagerDialogAction.DisplayVolumeIsNotCorrect,
                "This path cannot be used as storage path because it is located on neither fixed drive nor removable drive",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.DisplayVolumeIsNotCorrect, DialogResult = res });
        }
        public virtual void AddAction(StorageManagerDialogActionInfo info) { }
        void IStorageManagerDialogsProvider.DisplayVolumeIsNotCorrect(string root) {
            DisplayVolumeIsNotCorrectCore(root);
        }

        List<StorageManagerDialogActionInfo> actions;
        public List<StorageManagerDialogActionInfo> Actions {
            get {
                if(actions == null)
                    actions = new List<StorageManagerDialogActionInfo>();
                return actions;
            }
        }
    }

    public enum StorageManagerDialogAction {
        AddBackUpDialogDisplay,
        DisplayBackupNotPresentTask,
        DisplayNoBackupTask,
        DisplayNoStorageTask,
        DisplayOperationCanceledTask,
        DisplayStorageNotPresentTask,
        DisplayAddNewDevicesToStorageMessage,
        DisplayAddStorageDialog,
        DisplayAttachNonPresentStorageDevices,
        DisplayAddNewDevicesToBackupMessage,
        DisplayAddBackupDialog,
        DisplayAttachNonPresentBackupDevices,
        DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsed,
        ShowStorageForm,
        DisplayVolumeIsNotCorrect
    }
    public class StorageManagerDialogActionInfo {
        public StorageManagerDialogAction Action { get; set; }
        public bool Result { get; set; }
        public DialogResult DialogResult { get; set; }
        public int FailCount { get; set; }
    }
}
