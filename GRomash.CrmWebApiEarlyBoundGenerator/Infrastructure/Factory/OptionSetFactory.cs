using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Repository;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Static;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Factory
{
    public class OptionSetFactory
    {
        private readonly MetadataRepository _metadataRepository;

        public OptionSetFactory(MetadataRepository metadataRepository)
        {
            _metadataRepository = metadataRepository;
        }

        /// <summary>
        /// Gets the option set model.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="nameSpace">The name space.</param>
        /// <returns></returns>
        public OptionSetModel GetOptionSetModel(PicklistAttributeMetadata metadata, string nameSpace)
        {
            var optionSetName = metadata.SchemaName;

            if (metadata.OptionSet.IsGlobal == true)
            {
                var entityMetadata = _metadataRepository.GetEntityMetadata(metadata.EntityLogicalName);
                optionSetName = $"{entityMetadata.SchemaName}_{metadata.SchemaName}";
            }

            return new OptionSetModel()
            {
                OptionSetName = optionSetName,
                Namespace = nameSpace,
                Description = Helpers.GetDescription(metadata.Description)
            };
        }
    }
}
