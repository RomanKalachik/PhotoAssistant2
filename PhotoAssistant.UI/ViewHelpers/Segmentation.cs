using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
namespace PhotoAssistant.Core {
    public interface IDistanceCalculator {
        double CalcDistance(double x1, double y1, double x2, double y2);
    }
    public class DecartDistanceCalculator : IDistanceCalculator {
        double IDistanceCalculator.CalcDistance(double x1, double y1, double x2, double y2) {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
    public class SimpleGraph {
        ArrayList edges;
        ArrayList vertexes;
        public SimpleGraph() : this(new DecartDistanceCalculator()) { }
        public SimpleGraph(IDistanceCalculator calc) {
            DistanceCalculator = calc;
            edges = new ArrayList();
            vertexes = new ArrayList();
        }
        public IDistanceCalculator DistanceCalculator { get; private set; }
        public void CalculateEdges() {
            edges.Clear();
            foreach(SimpleVertex v1 in vertexes)
                foreach(SimpleVertex v2 in vertexes) {
                    if(v1 == v2) break;
                    edges.Add(new SimpleEdge() { V1 = v1, V2 = v2, Weight = DistanceCalculator.CalcDistance(v1.X, v1.Y, v2.X, v2.Y) });
                    if(v1.ParentSegment == null) v1.ParentSegment = new SimpleSegment() { MstW = 0 };
                    if(v2.ParentSegment == null) v2.ParentSegment = new SimpleSegment() { MstW = 0 };
                }
        }
        public ArrayList EdgesList {
            get { return edges; }
        }
        public ArrayList Vertexes {
            get { return vertexes; }
        }
    }
    public class SimpleSegment : SimpleGraph {
        public double MstW { get; set; }
    }
    public class SimpleVertex {
        public object Tag { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        SimpleSegment parentSegmentCore;
        public SimpleSegment ParentSegment {
            get { return parentSegmentCore; }
            set {
                if(parentSegmentCore == value) return;
                if(parentSegmentCore != null) parentSegmentCore.Vertexes.Remove(this);
                if(value != null) value.Vertexes.Add(this);
                parentSegmentCore = value;
            }
        }
    }
    public class SimpleEdge {
        public double Weight { get; set; }
        public SimpleVertex V1 { get; set; }
        public SimpleVertex V2 { get; set; }
    }
    public class SegmentInfo {
        protected int mstw;
        ArrayList vertexes;
        protected bool isCalculated = false;
        public SegmentInfo() { mstw = 0; vertexes = new ArrayList(); }
        public int MstW { get { return mstw; } set { mstw = value; } }
        public void AddVertex(SimpleVertex v) { vertexes.Add(v); }
        public ArrayList Vertexes { get { return vertexes; } }
        public SegmentInfo Clone() {
            SegmentInfo si = new SegmentInfo();
            si.Vertexes.AddRange(vertexes);
            si.MstW = mstw;
            return si;
        }
    }
    public class HieraicalStorageItem {
        string id;
        string parentId;
        int stage = 1;
        public string Id { get { return id; } set { id = value; } }
        public string ParentId { get { return parentId; } set { parentId = value; } }
        public int Stage { get { return stage; } set { stage = value; } }
    }
    public delegate void ProgressDelegate(int progress);
}
