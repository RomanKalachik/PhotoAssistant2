using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils.Serializing;
using DevExpress.Utils.Serializing.Helpers;

namespace PhotoAssistant.Core.Model {
    public class TagNodeDataSource : IXtraSerializable {
        void IXtraSerializable.OnEndDeserializing(string restoredVersion) {
        }

        void IXtraSerializable.OnEndSerializing() {
        }

        void IXtraSerializable.OnStartDeserializing(DevExpress.Utils.LayoutAllowEventArgs e) {
        }

        void IXtraSerializable.OnStartSerializing() {
        }
        List<TagNode> nodes;
        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public List<TagNode> Nodes {
            get {
                if(nodes == null)
                    nodes = new List<TagNode>();
                return nodes;
            }
            set {
                nodes = value;
            }
        }

        object XtraCreateNodesItem(XtraItemEventArgs e) {
            TagNode node = new TagNode();
            XtraPropertyInfo idInfo = e.Item.ChildProperties["Id"];
            XtraPropertyInfo parentIdInfo = e.Item.ChildProperties["ParentId"];
            XtraPropertyInfo valueInfo = e.Item.ChildProperties["Value"];

            node.Id = int.Parse(idInfo.Value.ToString());
            node.ParentId = int.Parse(parentIdInfo.Value.ToString());
            node.Value = valueInfo.Value.ToString();

            Nodes.Add(node);
            return node;
        }

        public virtual void SaveDataToXml(string xmlFile) {
            SaveLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        public virtual void LoadDataFromXml(string xmlFile) {
            RestoreLayoutCore(new XmlXtraSerializer(), xmlFile);
        }
        public virtual void LoadDataFromStream(System.IO.Stream stream) {
            RestoreLayoutCore(new XmlXtraSerializer(), stream);
        }
        protected virtual bool SaveLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                return serializer.SerializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                return serializer.SerializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }

        protected virtual void RestoreLayoutCore(XtraSerializer serializer, object path) {
            System.IO.Stream stream = path as System.IO.Stream;
            if(stream != null)
                serializer.DeserializeObjects(GetXtraObjectInfo(), stream, this.GetType().Name);
            else
                serializer.DeserializeObjects(GetXtraObjectInfo(), path.ToString(), this.GetType().Name);
        }
        protected XtraObjectInfo[] GetXtraObjectInfo() {
            ArrayList result = new ArrayList();
            result.Add(new XtraObjectInfo("TagNodeDataSource", this));
            return (XtraObjectInfo[])result.ToArray(typeof(XtraObjectInfo));
        }
    }

    public class TagNode {
        [XtraSerializableProperty]
        public int Id { get; set; }
        [XtraSerializableProperty]
        public int ParentId { get; set; }
        public int OriginalId { get; set; }

        [XtraSerializableProperty]
        public string Value { get; set; }
        public string Caption { get; set; }

        public Color Color { get; set; }

        public DmTag Tag { get; set; }
        public DmTagNode Node { get; set; }
    }
}
