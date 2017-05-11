using System;
using System.Collections;
using System.Text;
using System.Drawing;
namespace PhotoAssistant.Core {
    public delegate void MatrixWalkerDelegate(int x, int y);
    public class MatrixWalker {
        Size sizeCore;
        ProgressDelegate progeressDelegateCore;
        public MatrixWalker(Size size) : this(size, null) { }
        public MatrixWalker(Size size, ProgressDelegate progeressDelegate) {
            sizeCore = size;
            progeressDelegateCore = progeressDelegate;
        }
        public void Walk(MatrixWalkerDelegate representor) {
            for(int i = 0; i < sizeCore.Width; i++) {
                for(int j = 0; j < sizeCore.Height; j++) {
                    representor(i, j);
                }
                if(progeressDelegateCore != null) {
                    progeressDelegateCore(i * 100 / sizeCore.Width);
                }
            }

        }
    }
    public class EdgesComparer : IComparer {
        public int Compare(object a, object b) {
            SimpleEdge e1, e2;
            e1 = a as SimpleEdge;
            e2 = b as SimpleEdge;
            if(e1 == null || e2 == null) return -1;
            return (int)(e1.Weight - e2.Weight);
        }
    }
    public class MergeInfo {
        protected SegmentInfo mergedSmall, mergedLarge, result;
        protected int step;
        public MergeInfo(SegmentInfo mergedSmall, SegmentInfo mergedLarge) { this.mergedSmall = mergedSmall; this.mergedLarge = mergedLarge; }
        public SegmentInfo MegredSmall { get { return mergedSmall; } set { mergedSmall = value; } }
        public SegmentInfo MegredLarge { get { return mergedLarge; } set { this.mergedLarge = value; } }
        public SegmentInfo Result { get { return result; } set { result = value; } }
        public int Step { get { return step; } set { step = value; } }
    }
    public class HistogramHelper {
        int countCore = 1;
        int minVal;
        int maxVal;
        int[] hStore;
        public HistogramHelper(int count, int max, int min) {
            hStore = new int[count + 10];
            countCore = count;
            minVal = min;
            maxVal = max;
        }
        public int[] Histogram { get { return hStore; } }
        public void Add(int newValue) {
            int oneItemw = (maxVal - minVal) / countCore;
            int diff = newValue - minVal;
            if(diff <= 0) diff = countCore - 1;
            int index = diff / oneItemw;
            if(index > countCore - 1) index = countCore - 1;
            hStore[index]++;
        }
    }
    public class GraphSegmentator {
        protected SimpleGraph graphCore;
        public GraphSegmentator(SimpleGraph graph) {
            graphCore = graph;
        }
        public ArrayList DoSegmentation(double maxDistance) {
            tresh2 = tresh1 = maxDistance;
            graphCore.EdgesList.Sort(new EdgesComparer());
            DoSegmentationCore(0);
            Hashtable result = new Hashtable();
            foreach(SimpleVertex v in graphCore.Vertexes) {
                if(!result.ContainsKey(v.ParentSegment)) result.Add(v.ParentSegment, v.ParentSegment);
            }
            return new ArrayList(result.Values);
        }
        public int DoSegmentationCore(int startPos) {
            int i = 0;
            for(i = startPos; i < graphCore.EdgesList.Count; i++) {
                SimpleEdge e = graphCore.EdgesList[i] as SimpleEdge;
                if(e.Weight > tresh1)
                    return i;
                if(CanMerge(e)) Merge(e);
            }
            return i;
        }
        protected void Merge(SimpleEdge e) {
            SimpleVertex v1 = e.V1;
            SimpleVertex v2 = e.V2;
            SimpleSegment si1 = v1.ParentSegment;
            SimpleSegment si2 = v2.ParentSegment;
            if(si1 == null && si2 != null) { v1.ParentSegment = si2; si2.MstW = e.Weight; }
            if(si1 != null && si2 == null) { v2.ParentSegment = si1; si1.MstW = e.Weight; }
            if(si1 == null && si2 == null) {
                v1.ParentSegment = v2.ParentSegment = new SimpleSegment();
                v1.ParentSegment.MstW = e.Weight;
            }
            if(si1 != null && si2 != null) {
                SimpleSegment smallersegment;
                SimpleSegment largerSegment;
                if(si1.Vertexes.Count > si2.Vertexes.Count) { smallersegment = si2; largerSegment = si1; } else { smallersegment = si1; largerSegment = si2; }
                foreach(SimpleVertex v in new ArrayList(smallersegment.Vertexes)) { v.ParentSegment = largerSegment; }
                largerSegment.MstW = Math.Max(e.Weight, Math.Max(largerSegment.MstW, smallersegment.MstW));
            }
        }
        double tresh1 = 1;
        double tresh2 = 1;
        protected bool CanMerge(SimpleEdge e) {
            SimpleVertex v1 = e.V1;
            SimpleVertex v2 = e.V2;
            if(v1.ParentSegment != null && v2.ParentSegment != null && v1.ParentSegment == v2.ParentSegment) return false;
            double v1Segment = v1.ParentSegment == null ? 0 : v1.ParentSegment.MstW;
            double v2Segment = v2.ParentSegment == null ? 0 : v2.ParentSegment.MstW;
            if(e.Weight <= (v1Segment + tresh1) || e.Weight <= (v2Segment + tresh2)) return true;
            return false;
        }
    }
}