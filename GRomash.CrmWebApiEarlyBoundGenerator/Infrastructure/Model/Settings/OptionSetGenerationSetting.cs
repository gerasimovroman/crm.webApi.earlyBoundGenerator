using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.Settings
{
    public class OptionSetGenerationSetting
    {
        public string NameSpace { get; set; }
        public string OutFolder { get; set; }
        public IEnumerable<EnumAttributeMetadata> Metadata { get; set; }
    }
}
