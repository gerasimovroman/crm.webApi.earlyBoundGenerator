using System.Collections.Generic;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model
{
    public class EarlyBoundSettings
    {
        public string Namespace { get; set; }

        public string ResultFolder { get; set; }

        public IList<string> Entitites { get; set; } = new List<string>();
    }
}
