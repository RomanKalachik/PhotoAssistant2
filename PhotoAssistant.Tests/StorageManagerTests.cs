

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoAssistant.Core;
using PhotoAssistant.UI.ViewHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace PhotoAssistant.Tests {
    [TestClass]
    public class StorageManagerTests {
        protected TestStorageManager Manager {
            get; set;
        }
        protected TestStorageManagerDialogsProvider DialogsProvider => (TestStorageManagerDialogsProvider)Manager.DialogsProvider;
        [TestInitialize]
        public void Initialize() => Manager = new TestStorageManager();
        [TestMethod]
        public void TestInitialize() {
            Assert.AreEqual(DialogsProvider, Manager.DialogsProvider);
            Assert.AreEqual(Manager, DialogsProvider.Manager);
        }
        [TestMethod]
        public void TestReact_AddingFiles_NoStorage_PressCancel() {
            DialogsProvider.DisplayNoStorageTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.Cancel;
            });
            DialogsProvider.DisplayOperationCanceledTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.OK;
            });

            bool result = Manager.CheckStorage();

            Assert.AreEqual(2, DialogsProvider.Actions.Count);

            Assert.AreEqual(StorageManagerDialogAction.DisplayNoStorageTask, DialogsProvider.Actions[0].Action);
            Assert.AreEqual(DialogResult.Cancel, DialogsProvider.Actions[0].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayOperationCanceledTask, DialogsProvider.Actions[1].Action);
            Assert.AreEqual(DialogResult.OK, DialogsProvider.Actions[1].DialogResult);

            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void TestReact_AddingFiles_NoStorage_PressNo() {
            DialogsProvider.DisplayNoStorageTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.No;
            });
            DialogsProvider.DisplayOperationCanceledTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.OK;
            });

            bool result = Manager.CheckStorage();

            Assert.AreEqual(2, DialogsProvider.Actions.Count);

            Assert.AreEqual(StorageManagerDialogAction.DisplayNoStorageTask, DialogsProvider.Actions[0].Action);
            Assert.AreEqual(DialogResult.No, DialogsProvider.Actions[0].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayOperationCanceledTask, DialogsProvider.Actions[1].Action);
            Assert.AreEqual(DialogResult.OK, DialogsProvider.Actions[1].DialogResult);

            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void TestReact_AddingFiles_NoStorage_PressYes_ButNotAddedStorage() {
            int displayNoStorageDialogCount = 0;
            DialogsProvider.DisplayNoStorageTaskDelegate = new ShowDialogDelegate((buttons) => {
                displayNoStorageDialogCount++;
                return displayNoStorageDialogCount == 1 ? DialogResult.Yes : DialogResult.No;
            });
            DialogsProvider.DisplayOperationCanceledTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.OK;
            });
            DialogsProvider.ShowStorageFormDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.OK;
            });
            DialogsProvider.DisplayNoBackupTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.No;
            });

            bool result = Manager.CheckStorage();

            Assert.AreEqual(4, DialogsProvider.Actions.Count);

            Assert.AreEqual(StorageManagerDialogAction.DisplayNoStorageTask, DialogsProvider.Actions[0].Action);
            Assert.AreEqual(DialogResult.Yes, DialogsProvider.Actions[0].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.ShowStorageForm, DialogsProvider.Actions[1].Action);
            Assert.AreEqual(DialogResult.None, DialogsProvider.Actions[1].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayNoStorageTask, DialogsProvider.Actions[2].Action);
            Assert.AreEqual(DialogResult.No, DialogsProvider.Actions[2].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayOperationCanceledTask, DialogsProvider.Actions[3].Action);
            Assert.AreEqual(DialogResult.OK, DialogsProvider.Actions[3].DialogResult);

            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void TestReact_AddingFiles_NoStorage_PressYes_NoBackup_PressNo() {
            DialogsProvider.DisplayNoStorageTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.Yes;
            });
            DialogsProvider.DisplayOperationCanceledTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.OK;
            });
            DialogsProvider.ShowStorageFormDelegate = new ShowDialogDelegate((buttons) => {
                Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "T:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
                return DialogResult.OK;
            });
            DialogsProvider.DisplayNoBackupTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.No;
            });

            bool result = Manager.CheckStorage();

            Assert.AreEqual(3, DialogsProvider.Actions.Count);

            Assert.AreEqual(StorageManagerDialogAction.DisplayNoStorageTask, DialogsProvider.Actions[0].Action);
            Assert.AreEqual(DialogResult.Yes, DialogsProvider.Actions[0].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.ShowStorageForm, DialogsProvider.Actions[1].Action);
            Assert.AreEqual(DialogResult.None, DialogsProvider.Actions[1].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayNoBackupTask, DialogsProvider.Actions[2].Action);
            Assert.AreEqual(DialogResult.No, DialogsProvider.Actions[2].DialogResult);

            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestReact_AddingFiles_NoStorage_PressYes_NoBackup_PressYes() {
            DialogsProvider.DisplayNoStorageTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.Yes;
            });
            DialogsProvider.DisplayOperationCanceledTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.OK;
            });
            int showStorageDialogCount = 0;
            DialogsProvider.ShowStorageFormDelegate = new ShowDialogDelegate((buttons) => {
                showStorageDialogCount++;
                if(showStorageDialogCount == 1) {
                    Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "T:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
                } else {
                    Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });
                }
                return DialogResult.OK;
            });
            DialogsProvider.DisplayNoBackupTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.Yes;
            });

            bool result = Manager.CheckStorage();

            Assert.AreEqual(4, DialogsProvider.Actions.Count);

            Assert.AreEqual(StorageManagerDialogAction.DisplayNoStorageTask, DialogsProvider.Actions[0].Action);
            Assert.AreEqual(DialogResult.Yes, DialogsProvider.Actions[0].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.ShowStorageForm, DialogsProvider.Actions[1].Action);
            Assert.AreEqual(DialogResult.None, DialogsProvider.Actions[1].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayNoBackupTask, DialogsProvider.Actions[2].Action);
            Assert.AreEqual(DialogResult.Yes, DialogsProvider.Actions[2].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.ShowStorageForm, DialogsProvider.Actions[3].Action);
            Assert.AreEqual(DialogResult.None, DialogsProvider.Actions[3].DialogResult);

            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestReact_AddingFiles_NoStorage_PressYes_NoBackup_PressYes_ButNoBackupAdded() {
            DialogsProvider.DisplayNoStorageTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.Yes;
            });
            DialogsProvider.DisplayOperationCanceledTaskDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.OK;
            });
            int showStorageDialogCount = 0;
            DialogsProvider.ShowStorageFormDelegate = new ShowDialogDelegate((buttons) => {
                showStorageDialogCount++;
                if(showStorageDialogCount == 1) {
                    Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "T:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
                }
                return DialogResult.OK;
            });
            int displayNoBackupDialogCount = 0;
            DialogsProvider.DisplayNoBackupTaskDelegate = new ShowDialogDelegate((buttons) => {
                displayNoBackupDialogCount++;
                return displayNoBackupDialogCount == 1 ? DialogResult.Yes : DialogResult.No;
            });

            bool result = Manager.CheckStorage();

            Assert.AreEqual(5, DialogsProvider.Actions.Count);

            Assert.AreEqual(StorageManagerDialogAction.DisplayNoStorageTask, DialogsProvider.Actions[0].Action);
            Assert.AreEqual(DialogResult.Yes, DialogsProvider.Actions[0].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.ShowStorageForm, DialogsProvider.Actions[1].Action);
            Assert.AreEqual(DialogResult.None, DialogsProvider.Actions[1].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayNoBackupTask, DialogsProvider.Actions[2].Action);
            Assert.AreEqual(DialogResult.Yes, DialogsProvider.Actions[2].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.ShowStorageForm, DialogsProvider.Actions[3].Action);
            Assert.AreEqual(DialogResult.None, DialogsProvider.Actions[3].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayNoBackupTask, DialogsProvider.Actions[4].Action);
            Assert.AreEqual(DialogResult.No, DialogsProvider.Actions[4].DialogResult);

            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestReact_AddingFiles_StorageNotPresent_StorageNotAttached() {
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = false, ActualName = "T:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = false, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            int displayStorageNotPresentTaskCount = 0;
            DialogsProvider.DisplayStorageNotPresentTaskDelegate = new ShowDialogDelegate((buttons) => {
                displayStorageNotPresentTaskCount++;
                return displayStorageNotPresentTaskCount == 1 ? DialogResult.OK : DialogResult.Cancel;
            });

            bool result = Manager.CheckStorage();

            Assert.AreEqual(StorageManagerDialogAction.DisplayStorageNotPresentTask, DialogsProvider.Actions[0].Action);
            Assert.AreEqual(DialogResult.OK, DialogsProvider.Actions[0].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayStorageNotPresentTask, DialogsProvider.Actions[1].Action);
            Assert.AreEqual(DialogResult.Cancel, DialogsProvider.Actions[1].DialogResult);

            Assert.AreEqual(2, DialogsProvider.Actions.Count);

            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void TestReact_AddingFiles_StorageNotPresent_StorageAttached_BackupNotPresent_BackupNoAttached() {
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = false, ActualName = "T:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = false, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            DialogsProvider.DisplayStorageNotPresentTaskDelegate = new ShowDialogDelegate((buttons) => {
                ((TestStorageVolumeInfo)Manager.Storage[0]).TestIsPresent = true;
                return DialogResult.OK;
            });

            int backupStorageNotPresentCount = 0;
            DialogsProvider.DisplayBackupNotPresentTaskDelegate = new ShowDialogDelegate((buttons) => {
                backupStorageNotPresentCount++;
                return backupStorageNotPresentCount == 1 ? DialogResult.OK : DialogResult.Cancel;
            });

            bool result = Manager.CheckStorage();

            Assert.AreEqual(StorageManagerDialogAction.DisplayStorageNotPresentTask, DialogsProvider.Actions[0].Action);
            Assert.AreEqual(DialogResult.OK, DialogsProvider.Actions[0].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayBackupNotPresentTask, DialogsProvider.Actions[1].Action);
            Assert.AreEqual(DialogResult.OK, DialogsProvider.Actions[1].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayBackupNotPresentTask, DialogsProvider.Actions[2].Action);
            Assert.AreEqual(DialogResult.Cancel, DialogsProvider.Actions[2].DialogResult);

            Assert.AreEqual(3, DialogsProvider.Actions.Count);

            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestReact_AddingFiles_StorageNotPresent_StorageAttached_BackupNotPresent_BackupAttached() {
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = false, ActualName = "T:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = false, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            DialogsProvider.DisplayStorageNotPresentTaskDelegate = new ShowDialogDelegate((buttons) => {
                ((TestStorageVolumeInfo)Manager.Storage[0]).TestIsPresent = true;
                return DialogResult.OK;
            });

            DialogsProvider.DisplayBackupNotPresentTaskDelegate = new ShowDialogDelegate((buttons) => {
                ((TestStorageVolumeInfo)Manager.Backup[0]).TestIsPresent = true;
                return DialogResult.OK;
            });

            bool result = Manager.CheckStorage();

            Assert.AreEqual(StorageManagerDialogAction.DisplayStorageNotPresentTask, DialogsProvider.Actions[0].Action);
            Assert.AreEqual(DialogResult.OK, DialogsProvider.Actions[0].DialogResult);

            Assert.AreEqual(StorageManagerDialogAction.DisplayBackupNotPresentTask, DialogsProvider.Actions[1].Action);
            Assert.AreEqual(DialogResult.OK, DialogsProvider.Actions[1].DialogResult);

            Assert.AreEqual(2, DialogsProvider.Actions.Count);

            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestReact_AddingFiles_ThereIsNoFreeSpaceOnStorage_HasAnotherStorageVolume() {
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 100 * 1024 * 1024 });
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "T:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Storage[0], Manager.StorageVolume);

            Manager.RaiseStorageFullExceptionCount = 1;
            Assert.AreEqual(true, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
            Assert.AreEqual(Manager.Storage[1], Manager.StorageVolume);
        }
        [TestMethod]
        public void TestReact_AddingFiles_ThereIsNoFreeSpaceOnStorage_StorageIsNotPresent_StorageIsPresent() {
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 100 * 1024 * 1024 });
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = false, ActualName = "T:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Storage[0], Manager.StorageVolume);

            Manager.RaiseStorageFullExceptionCount = 1;
            DialogsProvider.DisplayAttachNonPresentStorageDevicesDelegate = new ShowDialogDelegate((buttons) => {
                ((TestStorageVolumeInfo)Manager.Storage[1]).TestIsPresent = true;
                    return DialogResult.OK;
            });
            Assert.AreEqual(true, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
            Assert.AreEqual(Manager.Storage[1], Manager.StorageVolume);
        }
        [TestMethod]
        public void TestReact_AddingFiles_ThereIsNoFreeSpaceOnStorage_AllStoragePresent_StorageNotAdded() {
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 100 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Storage[0], Manager.StorageVolume);

            Manager.RaiseStorageFullExceptionCount = 1;
            DialogsProvider.DisplayAddNewDevicesToStorageMessageDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.Cancel;
            });
            Assert.AreEqual(false, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
        }
        [TestMethod]
        public void Test_React_AddingFiles_ThereIsNoFreeSpaceOnStorage_AllStoragePresent_AddNewStorage() {
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 100 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Storage[0], Manager.StorageVolume);

            Manager.RaiseStorageFullExceptionCount = 1;
            DialogsProvider.DisplayAddNewDevicesToStorageMessageDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.Yes;
            });
            DialogsProvider.ShowStorageFormDelegate += new ShowDialogDelegate((buttons) => {
                Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "X:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
                    return DialogResult.OK;
            });
            Assert.AreEqual(true, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
            Assert.AreEqual(Manager.Storage[1], Manager.StorageVolume);
        }
        [TestMethod]
        public void Test_React_AddingFiles_ThereIsNoFreeSpaceOnStorage_AllStoragePresent_StorageAddedSecondTime() {
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 100 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Storage[0], Manager.StorageVolume);

            Manager.RaiseStorageFullExceptionCount = 1;
            DialogsProvider.DisplayAddNewDevicesToStorageMessageDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.Yes;
            });
            int showDialogCount = 0;
            DialogsProvider.ShowStorageFormDelegate += new ShowDialogDelegate((buttons) => {
                showDialogCount++;
                    if(showDialogCount > 3) {
                    Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "X:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
                }

                return DialogResult.OK;
            });
            Assert.AreEqual(true, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
            Assert.AreEqual(Manager.Storage[1], Manager.StorageVolume);
        }
        [TestMethod]
        public void Test_React_AddingFiles_AnotherIOExceptionOccured() {
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 1024 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Storage[0], Manager.StorageVolume);

            Manager.RaiseIoExceptionCount = 1;

            Assert.AreEqual(false, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
        }
        [TestMethod]
        public void Test_React_AddingFiles_UnAutorizedExceptionOccured() {
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 1024 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Storage[0], Manager.StorageVolume);

            Manager.RaiseUnatorizedExceptionCount = 1;

            Assert.AreEqual(false, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
        }
        [TestMethod]
        public void TestReact_AddingFiles_ThereIsNoFreeSpaceOnBackup_HasAnotherBackupVolume() {
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 100 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "T:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Backup[0], Manager.BackupVolume);

            Manager.RaiseBackupFullExceptionCount = 1;
            Assert.AreEqual(true, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
            Assert.AreEqual(Manager.Backup[1], Manager.BackupVolume);
        }
        [TestMethod]
        public void TestReact_AddingFiles_ThereIsNoFreeSpaceOnBackup_BackupIsNotPresent_BackupIsPresent() {
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 100 * 1024 * 1024 });
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "T:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Backup[0], Manager.BackupVolume);

            Manager.RaiseBackupFullExceptionCount = 1;
            DialogsProvider.DisplayAttachNonPresentBackupDevicesDelegate = new ShowDialogDelegate((buttons) => {
                ((TestStorageVolumeInfo)Manager.Backup[1]).TestIsPresent = true;
                    return DialogResult.OK;
            });
            Assert.AreEqual(true, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
            Assert.AreEqual(Manager.Backup[1], Manager.BackupVolume);
        }
        [TestMethod]
        public void TestReact_AddingFiles_ThereIsNoFreeSpaceOnBackup_AllBackupPresent_BackupNotAdded() {
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 100 * 1024 * 1024 });
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Backup[0], Manager.BackupVolume);

            Manager.RaiseBackupFullExceptionCount = 1;
            DialogsProvider.DisplayAddNewDevicesToBackupMessageDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.Cancel;
            });
            Assert.AreEqual(false, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
        }
        [TestMethod]
        public void Test_React_AddingFiles_ThereIsNoFreeSpaceOnBackup_AllBackupPresent_AddNewBackup() {
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 100 * 1024 * 1024 });
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Backup[0], Manager.BackupVolume);

            Manager.RaiseBackupFullExceptionCount = 1;
            DialogsProvider.DisplayAddNewDevicesToBackupMessageDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.Yes;
            });
            DialogsProvider.ShowStorageFormDelegate += new ShowDialogDelegate((buttons) => {
                Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "X:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
                    return DialogResult.OK;
            });
            Assert.AreEqual(true, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
            Assert.AreEqual(Manager.Backup[1], Manager.BackupVolume);
        }
        [TestMethod]
        public void Test_React_AddingFiles_ThereIsNoFreeSpaceOnBackup_AllBackupPresent_BackupAddedSecondTime() {
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 100 * 1024 * 1024 });
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Backup[0], Manager.BackupVolume);

            Manager.RaiseBackupFullExceptionCount = 1;
            DialogsProvider.DisplayAddNewDevicesToBackupMessageDelegate = new ShowDialogDelegate((buttons) => {
                return DialogResult.Yes;
            });
            int showDialogCount = 0;
            DialogsProvider.ShowStorageFormDelegate += new ShowDialogDelegate((buttons) => {
                showDialogCount++;
                    if(showDialogCount > 3) {
                    Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "X:\\", ProjectFolder = "Test", VolumeId = "VolumeId", AvailableFreeSpace = 1024 * 1024 * 1024 });
                }

                return DialogResult.OK;
            });
            Assert.AreEqual(true, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
            Assert.AreEqual(Manager.Backup[1], Manager.BackupVolume);
        }
        [TestMethod]
        public void Test_React_AddingFiles_AnotherIOExceptionOccured_Backup() {
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 1024 * 1024 * 1024 });
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Backup[0], Manager.BackupVolume);

            Manager.RaiseBackupIoExceptionCount = 1;

            Assert.AreEqual(false, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
        }
        [TestMethod]
        public void Test_React_AddingFiles_UnAutorizedExceptionOccured_Backup() {
            Manager.Backup.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Z:\\", ProjectFolder = "Test3", VolumeId = "VolumeId3", AvailableFreeSpace = 1024 * 1024 * 1024 });
            Manager.Storage.Add(new TestStorageVolumeInfo() { TestIsPresent = true, ActualName = "Y:\\", ProjectFolder = "Test2", VolumeId = "VolumeId2", AvailableFreeSpace = 1024 * 1024 * 1024 });

            bool result = Manager.CheckStorage();
            Assert.AreEqual(true, result);

            Manager.CheckInitializeStorageVolumes();
            Assert.AreEqual(Manager.Backup[0], Manager.BackupVolume);

            Manager.RaiseBackupUnatorizedExceptionCount = 1;

            Assert.AreEqual(false, Manager.CopyFile("c:\\temp\\test.txt", "testpath\\"));
        }
    }
    public class TestStorageVolumeInfo : StorageVolumeInfo {
        public TestStorageVolumeInfo() => Device = new StorageDeviceInfo() { DeviceType = 0, DiskNumber = 1, PartitionNumber = 1, ProductId = "TEST_DRIVE", RevisionId = "1.04", SerialNumber = "TEST_001", Vendor = "TEST" };
        public bool TestIsPresent {
            get; set;
        }
        public override bool UpdateIsPresent() {
            IsPresent = TestIsPresent;
            return TestIsPresent;
        }
    }
    public class TestStorageManager : StorageManager {
        public TestStorageManager() => DialogsProvider = new TestStorageManagerDialogsProvider();
        public int RaiseIoExceptionCount {
            get; internal set;
        }
        public int RaiseStorageFullExceptionCount {
            get; set;
        }
        public int RaiseUnatorizedExceptionCount {
            get; internal set;
        }
        public int RaiseBackupIoExceptionCount {
            get; internal set;
        }
        public int RaiseBackupFullExceptionCount {
            get; set;
        }
        public int RaiseBackupUnatorizedExceptionCount {
            get; internal set;
        }
        public override FileInfo CopyFile(string file, StorageVolumeInfo volume, string relativePath, bool isBackup) {
            const int ERROR_DISK_FULL = 0x70;
            if(isBackup) {
                if(RaiseBackupFullExceptionCount > 0) {
                    RaiseBackupFullExceptionCount--;
                    throw new IOException(string.Empty, ERROR_DISK_FULL);
                } else if(RaiseBackupIoExceptionCount > 0) {
                    RaiseBackupIoExceptionCount--;
                    throw new IOException(string.Empty, 0);
                } else if(RaiseBackupUnatorizedExceptionCount > 0) {
                    RaiseBackupUnatorizedExceptionCount--;
                    throw new UnauthorizedAccessException();
                }
            } else {
                if(RaiseStorageFullExceptionCount > 0) {
                    RaiseStorageFullExceptionCount--;
                    throw new IOException(string.Empty, ERROR_DISK_FULL);
                } else if(RaiseIoExceptionCount > 0) {
                    RaiseIoExceptionCount--;
                    throw new IOException(string.Empty, 0);
                } else if(RaiseUnatorizedExceptionCount > 0) {
                    RaiseUnatorizedExceptionCount--;
                    throw new UnauthorizedAccessException();
                }
            }
            string fileName = "c:\\temp\\" + Guid.NewGuid() + " - testFile.txt";
            if(!Directory.Exists("c:\\temp\\")) {
                Directory.CreateDirectory("c:\\temp");
            }

            File.Create(fileName);
            return new FileInfo(fileName);
        }
        protected override long GetAvailableFreeSpace(StorageVolumeInfo volume) => volume.AvailableFreeSpace;
    }
    public delegate DialogResult ShowDialogDelegate(MessageBoxButtons buttons);
    public class TestStorageManagerDialogsProvider : StorageManagerDialogsProvider {
        public override void ShowStorageForm() {
            AddAction(new StorageManagerDialogActionInfo() { Action = StorageManagerDialogAction.ShowStorageForm });
            ShowStorageFormDelegate(MessageBoxButtons.OK);
        }
        protected virtual int MaxActionsCount => 200;
        protected override DialogResult ShowDialog(StorageManagerDialogAction action, string message, MessageBoxButtons buttons, MessageBoxIcon icon) {
            switch(action) {
                case StorageManagerDialogAction.AddBackUpDialogDisplay:
                    return AddBackUpDialogDisplayDelegate(buttons);
                case StorageManagerDialogAction.DisplayAddBackupDialog:
                    return DisplayAddBackupDialogDelegate(buttons);
                case StorageManagerDialogAction.DisplayAddNewDevicesToBackupMessage:
                    return DisplayAddNewDevicesToBackupMessageDelegate(buttons);
                case StorageManagerDialogAction.DisplayAddNewDevicesToStorageMessage:
                    return DisplayAddNewDevicesToStorageMessageDelegate(buttons);
                case StorageManagerDialogAction.DisplayAddStorageDialog:
                    return DisplayAddStorageDialogDelegate(buttons);
                case StorageManagerDialogAction.DisplayAttachNonPresentBackupDevices:
                    return DisplayAttachNonPresentBackupDevicesDelegate(buttons);
                case StorageManagerDialogAction.DisplayAttachNonPresentStorageDevices:
                    return DisplayAttachNonPresentStorageDevicesDelegate(buttons);
                case StorageManagerDialogAction.DisplayBackupNotPresentTask:
                    return DisplayBackupNotPresentTaskDelegate(buttons);
                case StorageManagerDialogAction.DisplayNoBackupTask:
                    return DisplayNoBackupTaskDelegate(buttons);
                case StorageManagerDialogAction.DisplayNoStorageTask:
                    return DisplayNoStorageTaskDelegate(buttons);
                case StorageManagerDialogAction.DisplayOperationCanceledTask:
                    return DisplayOperationCanceledTaskDelegate(buttons);
                case StorageManagerDialogAction.DisplayStorageNotPresentTask:
                    return DisplayStorageNotPresentTaskDelegate(buttons);
                case StorageManagerDialogAction.DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsed:
                    return DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsedDelegate(buttons);
                case StorageManagerDialogAction.DisplayVolumeIsNotCorrect:
                    return DisplayVolumeIsNotCorrectDelegate(buttons);
                case StorageManagerDialogAction.ShowStorageForm:
                    return ShowStorageFormDelegate(buttons);
            }
            return DialogResult.OK;
        }
        public ShowDialogDelegate AddBackUpDialogDisplayDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayAddBackupDialogDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayAddNewDevicesToBackupMessageDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayAddNewDevicesToStorageMessageDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayAddStorageDialogDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayAttachNonPresentBackupDevicesDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayAttachNonPresentStorageDevicesDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayBackupNotPresentTaskDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayNoBackupTaskDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayNoStorageTaskDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayOperationCanceledTaskDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayStorageNotPresentTaskDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayThisStorageNotRecommendedForAddBecauseAlreadyUsedDelegate {
            get; set;
        }
        public ShowDialogDelegate DisplayVolumeIsNotCorrectDelegate {
            get; set;
        }
        public ShowDialogDelegate ShowStorageFormDelegate {
            get; set;
        }
        public override void AddAction(StorageManagerDialogActionInfo info) {
            if(Actions.Count > MaxActionsCount) {
                Actions.Clear();
            }

            Actions.Add(info);
        }
    }
}
