using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraMap;
using PhotoAssistant.Core.Model;
using PhotoAssistant.Core;
using PhotoAssistant.UI.View;

namespace PhotoAssistant.UI.ViewHelpers {
    public class MapClasterizationHelper {
        static MapClasterizationHelper defaultHelper;
        public static MapClasterizationHelper Default {
            get {
                if(defaultHelper == null)
                    defaultHelper = new MapClasterizationHelper();
                return defaultHelper;
            }
        }

        public List<PhotoMapItem> GetClasteredItems(MapControl map, List<DmFile> files, double distanceRadius) {
            SimpleGraph graph = new SimpleGraph(EarthDistanceCalculator.Default);
            files.ForEach((file) => {
                if(file.HasGeoLocation)
                    graph.Vertexes.Add(CreateVertex(map, file));
            });
            graph.CalculateEdges();
            GraphSegmentator segmentator = new GraphSegmentator(graph);
            ArrayList segments = segmentator.DoSegmentation(distanceRadius);


            List<PhotoMapItem> items = new List<PhotoMapItem>();
            foreach(SimpleSegment segment in segments) {
                PhotoMapItem item = new PhotoMapItem();
                double x = 0.0, y = 0.0;
                foreach(SimpleVertex v in segment.Vertexes) {
                    item.Files.Add((DmFile)v.Tag);
                    x += v.X;
                    y += v.Y;
                }
                x /= segment.Vertexes.Count;
                y /= segment.Vertexes.Count;

                item.Location = new GeoPoint(x, y);
                item.Image = ThumbHelper.GetIconImage(((DmFile)((SimpleVertex)segment.Vertexes[0]).Tag));
                items.Add(item);
            }
            return items;
        }

        private SimpleVertex CreateVertex(MapControl map, DmFile file) {
            return new SimpleVertex() { Tag = file, X = file.Latitude, Y = file.Longitude };
        }

        private MapPoint GetLocation(MapControl map, DmFile file) {
            return map.CoordPointToScreenPoint(new GeoPoint(file.Latitude, file.Longitude));
        }
    }

    public class EarthDistanceCalculator : IDistanceCalculator {
        static EarthDistanceCalculator defaultCalculator;
        public static EarthDistanceCalculator Default {
            get {
                if(defaultCalculator == null)
                    defaultCalculator = new EarthDistanceCalculator();
                return defaultCalculator;
            }
        }

        double ToRadians(double angle) {
            return angle * Math.PI / 180;
        }

        double CalcDistanceCore(double lat1, double lon1, double lat2, double lon2) {
            double R = 6371000;
            double f1 = ToRadians(lat1);
            double f2 = ToRadians(lat2);
            double df = ToRadians(lat2 - lat1);
            double da = ToRadians(lon2 - lon1);

            double a = Math.Sin(df / 2) * Math.Sin(df / 2) +
                    Math.Cos(f1) * Math.Cos(f2) *
                    Math.Sin(da / 2) * Math.Sin(da / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double d = R * c;
            return d;
        }

        double IDistanceCalculator.CalcDistance(double x1, double y1, double x2, double y2) {
            return CalcDistanceCore(x1, y1, x2, y2);
        }
    }

    public class MapClaster {
        public List<DmFile> Files { get; set; }
        public GeoPoint Pivot { get; set; }
    }

    public class ClasterizationResult {
        public IList<DmFile> Centroids { get; set; }
        public IDictionary<DmFile, IList<DataItemFile>> Clusterization { get; set; }
        public double Cost { get; set; }
    }

    public class DataItemFile {
        public DmFile[] Input { get; set; }
        public DmFile[] Output { get; set; }
    }
}
