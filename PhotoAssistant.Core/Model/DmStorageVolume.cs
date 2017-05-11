using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core.Model {
    public class DmStorageVolume : ISupportId {
        public DmStorageVolume() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }

        public string VolumeId { get; set; }
        [NotMapped]
        public string Name { get; set; }
        public string ProjectFolder { get; set; }
    }
}
