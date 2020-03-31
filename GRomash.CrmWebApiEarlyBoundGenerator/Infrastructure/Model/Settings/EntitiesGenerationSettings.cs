﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.Settings
{
    public class EntitiesGenerationSettings
    {
        public string NameSpace { get; set; }
        public string OutFolder { get; set; }
        public string[] Entities { get; set; }
        public IEnumerable<EntityMetadata> EntityMetadatas { get; set; }
    }
}
