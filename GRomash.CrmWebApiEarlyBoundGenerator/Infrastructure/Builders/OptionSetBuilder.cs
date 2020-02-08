using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Repository;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Static;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders
{
    /// <summary>
    /// Builder of OptionSetModel
    /// </summary>
    public class OptionSetBuilder
    {
        /// <summary>
        /// The metadata repository
        /// </summary>
        private readonly MetadataRepository _metadataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionSetBuilder"/> class.
        /// </summary>
        /// <param name="metadataRepository">The metadata repository.</param>
        public OptionSetBuilder(MetadataRepository metadataRepository)
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
