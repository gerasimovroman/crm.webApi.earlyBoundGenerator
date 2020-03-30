using System;
using System.Collections.Generic;
using System.Linq;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class MetadataRepository
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly IOrganizationService _service;
        /// <summary>
        /// The entity metadata
        /// </summary>
        private readonly Dictionary<string, EntityMetadata> _entityMetadata = new Dictionary<string, EntityMetadata>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataRepository"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public MetadataRepository(IOrganizationService service)
        {
            _service = service;
        }


        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void ClearCache()
        {
            _entityMetadata.Clear();
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EntityModel> GetEntities()
        {
            RetrieveAllEntitiesRequest metaDataRequest = new RetrieveAllEntitiesRequest
            {
                EntityFilters = EntityFilters.Entity
            };

            var metaDataResponse = (RetrieveAllEntitiesResponse)_service.Execute(metaDataRequest);
            var entities = metaDataResponse.EntityMetadata;

            return entities.Select(x => new EntityModel()
            {
                DisplayName = x.DisplayName?.UserLocalizedLabel?.Label,
                LogicalName = x.LogicalName
            }).OrderBy(x => x.LogicalName);
        }

        /// <summary>
        /// Gets the entities metadata.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public IEnumerable<EntityMetadata> GetEntitiesMetadata(string[] entities)
        {
            return entities.Select(GetEntityMetadata);
        }


        /// <summary>
        /// Gets the option set metadata.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public IEnumerable<EnumAttributeMetadata> GetOptionSetMetadata(string[] entities)
        {
            var attributeMetadatas = entities.Select(entity => GetEntityMetadata(entity)).SelectMany(entityMetadata => 
                entityMetadata.Attributes.Where(x => x.AttributeType == AttributeTypeCode.Picklist ||
                                                     x.AttributeType == AttributeTypeCode.State ||
                                                     x.AttributeType == AttributeTypeCode.Status));

            return attributeMetadatas.Cast<EnumAttributeMetadata>();
        }

        /// <summary>
        /// Gets the option set metadata.
        /// </summary>
        /// <param name="metadataId">The metadata identifier.</param>
        /// <returns></returns>
        public OptionSetMetadata GetOptionSetMetadata(Guid metadataId)
        {
            var retrieveEntityRequest = new RetrieveOptionSetRequest()
            {
                MetadataId = metadataId
            };

            var response = (RetrieveOptionSetResponse)_service.Execute(retrieveEntityRequest);

            return (OptionSetMetadata)response.OptionSetMetadata;
        }

        /// <summary>
        /// Gets the entity metadata.
        /// </summary>
        /// <param name="entityLogicalName">Name of the entity logical.</param>
        /// <returns></returns>
        public EntityMetadata GetEntityMetadata(string entityLogicalName)
        {
            var metadata = GetFromCache(entityLogicalName);

            if (metadata != null)
            {
                return metadata;
            }

            var retrieveEntityRequest = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Attributes | EntityFilters.Relationships | EntityFilters.Entity,
                LogicalName = entityLogicalName
            };

            var response = (RetrieveEntityResponse)_service.Execute(retrieveEntityRequest);

            _entityMetadata.Add(entityLogicalName, response.EntityMetadata);

            return response.EntityMetadata;
        }

        /// <summary>
        /// Gets from cache.
        /// </summary>
        /// <param name="entityLogicalName">Name of the entity logical.</param>
        /// <returns></returns>
        private EntityMetadata GetFromCache(string entityLogicalName)
        {
            return _entityMetadata.ContainsKey(entityLogicalName) ? _entityMetadata[entityLogicalName] : null;
        }
    }
}
