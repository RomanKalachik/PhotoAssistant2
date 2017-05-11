//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WIA;

//namespace PhotoAssistant.Core {
//    public class WIAHelper {
//        public static WIAHelper Default {
//            get {
//                if(_default == null)
//                    _default = new WIAHelper();
//                return _default;
//            }
//        }
//        static WIAHelper _default;

//        public IList<DeviceInfoWrapper> GetWIADevices() {
//            IList<DeviceInfoWrapper> result = new List<DeviceInfoWrapper>();
//            IDevice device;
//            try {
//                DeviceManager manager = new DeviceManager();
//                foreach(IDeviceInfo deviceInfo in manager.DeviceInfos) {
//                    if(deviceInfo.Type != WiaDeviceType.ScannerDeviceType) {
//                        if(!ConnectToDevice(deviceInfo, out device))
//                            continue; //device is offline

//                        string manufacturer = GetPropertyValue(deviceInfo.Properties, "Manufacturer");
//                        string name = GetPropertyValue(deviceInfo.Properties, "Name");
//                        result.Add(new DeviceInfoWrapper(deviceInfo, string.Format("{0} {1}", manufacturer, name)));
//                    }
//                }
//            } catch { }
//            return result;
//        }

//        protected internal dynamic GetPropertyValue(WIA.Properties props, string propertyName) {
//            foreach(IProperty property in props) {
//                if(property.Name == propertyName) {
//                    return property.get_Value();
//                }
//            }
//            return null;
//        }

//        public bool ConnectToDevice(IDeviceInfo deviceInfo, out IDevice device) {
//            try {
//                device = deviceInfo.Connect();
//                return true;
//            } catch {
//                device = null;
//                return false;
//            }
//        }

//        public Image GetThumbnail(WIAItem wiaItem, string folder) {
//            try {
//                IDevice device;
//                if(ConnectToDevice(wiaItem.DeviceInfo, out device)) {
//                    if(device != null) {
//                        var item = device.GetItem(wiaItem.ItemID);
//                        if(item != null && folder != null) {
//                            string name = GetPropertyValue(item.Properties, WIAConst.ITEM_NAME);
//                            int width = Convert.ToInt32(GetPropertyValue(item.Properties, WIAConst.THUMB_W));
//                            int height = Convert.ToInt32(GetPropertyValue(item.Properties, WIAConst.THUMB_H));
//                            string path = folder + "\\" + name + "_thumb";

//                            Vector thumbData = GetPropertyValue(item.Properties, WIAConst.THUMB_DATA);
//                            ImageFile imageFile = thumbData.get_ImageFile(width, height);

//                            if(Directory.Exists(folder)) {
//                                imageFile.SaveFile(path);
//                                Image img = null;
//                                using(FileStream fs = new FileInfo(path).OpenRead()) {
//                                    img = Image.FromStream(fs);
//                                }
//                                File.Delete(path);
//                                return img;
//                            }
//                        }
//                    }
//                }
//                return null;
//            } catch { return null; }
//        }
//    }

//    public class WIAItemList : IEnumerable<WIAItem> {
//        WIAHelper _help { get { return WIAHelper.Default; } }
//        List<WIAItem> _list = null;
//        public void Populate(DeviceInfoWrapper wrapper) {
//            IDevice device;
//            if(wrapper != null) {
//                if(_help.ConnectToDevice(wrapper.DeviceInfo, out device)) {
//                    if(device != null) {
//                        this._list = new List<WIAItem>();
//                        foreach(Item item in device.Items) {
//                            ProcessWIAItem(wrapper.DeviceInfo, item, null);
//                        }
//                    }
//                }
//            }
//        }
//        void ProcessWIAItem(IDeviceInfo deviceInfo, IItem item, string folder) {
//            string name = _help.GetPropertyValue(item.Properties, WIAConst.ITEM_NAME);
//            string ext = _help.GetPropertyValue(item.Properties, WIAConst.ITEM_FILENAME_EXT);
//            int flags = Convert.ToInt32(_help.GetPropertyValue(item.Properties, WIAConst.ITEM_FLAGS));
//            if(string.IsNullOrEmpty(name)) return;

//            if((flags & 8192) == 8192) { // <- item
//                WIAItem newItem = new WIAItem(deviceInfo, item.ItemID, folder + "\\" + name + "." + ext);
//                this._list.Add(newItem);
//            }

//            if((flags & 4) == 4) { // <- folder
//                foreach(IItem subItem in item.Items) {
//                    ProcessWIAItem(deviceInfo, subItem, string.IsNullOrEmpty(folder) ? name : (folder + "\\" + name));
//                }
//            }
//        }

//        public IEnumerator<WIAItem> GetEnumerator() {
//            return ((IEnumerable<WIAItem>)_list).GetEnumerator();
//        }
//        IEnumerator IEnumerable.GetEnumerator() {
//            return ((IEnumerable<WIAItem>)_list).GetEnumerator();
//        }
//    }
//    public static class WIAConst {
//        public const string ITEM_NAME = "Item Name";
//        public const string ITEM_FLAGS = "Item Flags";
//        public const string ITEM_FILENAME_EXT = "Filename extension";
//        public const string THUMB_W = "Thumbnail Width";
//        public const string THUMB_H = "Thumbnail Height";
//        public const string THUMB_DATA = "Thumbnail Data";
//    }

//    public class DeviceInfoWrapper {
//        public DeviceInfoWrapper(IDeviceInfo info, string displayName) {
//            this.DeviceInfo = info;
//            this.DisplayName = displayName;
//        }
//        public IDeviceInfo DeviceInfo { get; private set; }
//        public string DisplayName { get; private set; }
//        public string Id { get { return this.DeviceInfo.DeviceID; } }
//    }

//    public class WIAItem {
//        public WIAItem(IDeviceInfo deviceInfo, string itemId, string path) {
//            this.DeviceInfo = deviceInfo;
//            this.ItemID = itemId;
//            this.Path = path;
//        }
//        public string ItemID { get; private set; }
//        public string Path { get; private set; }
//        public IDeviceInfo DeviceInfo { get; private set; }
//    }
//}
