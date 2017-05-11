using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core.Model {
    public class DbPropertiesModel : ISupportId {
        public DbPropertiesModel() {
            Id = Guid.NewGuid();
            ImportIndex = 0;
            FileIndex = 0;
            ShouldCalcAspectRatio = true;
            AspectRatio = 1.0f;
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }

        public long ImportIndex { get; set; }
        public long FileIndex { get; set; }
        public float AspectRatio { get; set; }
        bool shouldCalculateAspectRatio;
        public bool ShouldCalcAspectRatio {
            get { return shouldCalculateAspectRatio; }
            set {
                if(ShouldCalcAspectRatio == value)
                    return;
                shouldCalculateAspectRatio = value;
                if(ShouldCalcAspectRatio)
                    AspectRatio = 1.0f;
            }
        }

        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int ProjectOpenCount { get; set; }
        public string ProjectDescription { get; set; }
        public int ProjectFileCount { get; set; }
    }
}
