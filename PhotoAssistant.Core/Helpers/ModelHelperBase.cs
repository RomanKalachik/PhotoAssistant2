using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core {
    public class ModelHelperBase {
        public ModelHelperBase(DmModel model) {
            Model = model;
        }

        public DmModel Model { get; private set; }
    }
}
