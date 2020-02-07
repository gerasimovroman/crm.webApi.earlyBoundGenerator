using System.Collections.Generic;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model
{
    public class PropertyModel
    {
        public string Description { get; set; }

        public string Type { get; set; }

        public string PropertyName { get; set; }

        public string AttributeName { get; set; }

        public List<CustomAttribute> Attributes { get; } = new List<CustomAttribute>();
    }
}
