using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PhotoAssistant.Core {
    public class ModelHelperBase {
        public ModelHelperBase(DmModel model) => Model = model;
        public DmModel Model {
            get; private set;
        }
    }
}
