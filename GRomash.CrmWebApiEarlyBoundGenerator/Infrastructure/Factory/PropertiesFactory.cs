using System;
using System.Collections.Generic;
using System.Linq;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Repository;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Static;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Factory
{
    /// <summary>
    /// Factory of properties
    /// </summary>
    public class PropertiesFactory
    {
        /// <summary>
        /// The entities
        /// </summary>
        private readonly string[] _entities;
        /// <summary>
        /// The metadata repository
        /// </summary>
        private readonly MetadataRepository _metadataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesFactory"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="metadataRepository">The metadata repository.</param>
        public PropertiesFactory(string[] entities, MetadataRepository metadataRepository)
        {
            _entities = entities;
            _metadataRepository = metadataRepository;
        }

        /// <summary>
        /// Gets the property models.
        /// </summary>
        /// <param name="attributeMetadatas">The attribute metadatas.</param>
        /// <param name="oneToManyRelationshipMetadatas">The one to many relationship metadatas.</param>
        /// <returns></returns>
        public PropertyModel[] GetPropertyModels(AttributeMetadata[] attributeMetadatas, OneToManyRelationshipMetadata[] oneToManyRelationshipMetadatas)
        {
            var propertyModels = new List<PropertyModel>();

            propertyModels.AddRange(GetProps(attributeMetadatas));
            propertyModels.AddRange(GetEntityReferenceProps(attributeMetadatas, oneToManyRelationshipMetadatas));

            return propertyModels.OrderBy(x => x.PropertyName).ToArray();
        }

        /// <summary>
        /// Gets the props.
        /// </summary>
        /// <param name="attributeMetadatas">The attribute metadatas.</param>
        /// <returns></returns>
        private PropertyModel[] GetProps(IEnumerable<AttributeMetadata> attributeMetadatas)
        {
            var propertyModels = new List<PropertyModel>();

            foreach (var attributeMetadata in attributeMetadatas)
            {
                if (attributeMetadata.AttributeType != AttributeTypeCode.Lookup &&
                    attributeMetadata.AttributeType.HasValue)
                {
                    var attributeType = attributeMetadata.AttributeType.Value;

                    if (FieldsFactory.IsSupportedType(attributeMetadata) &&
                        !FieldsFactory.IsLookup(attributeMetadata))
                    {
                        var type = Helpers.Types[attributeType];
                        var propertyName = attributeMetadata.SchemaName;
                        var attributeName = attributeMetadata.SchemaName;
                        var description = Helpers.GetDescription(attributeMetadata.Description);

                        var propertyModel = new PropertyModel()
                        {
                            Description = description,
                            AttributeName = attributeName,
                            PropertyName = propertyName,
                            Type = type
                        };

                        if (attributeType == AttributeTypeCode.DateTime &&
                            attributeMetadata is DateTimeAttributeMetadata dateTimeAttributeMetadata)
                        {
                            if (dateTimeAttributeMetadata.Format == DateTimeFormat.DateOnly)
                            {
                                propertyModel.Attributes.Add(new DateOnlyAttributeModel());
                            }
                        }

                        propertyModels.Add(propertyModel);
                    }
                }
            }

            return propertyModels.ToArray();
        }

        /// <summary>
        /// Gets the entity reference props.
        /// </summary>
        /// <param name="attributeMetadatas">The attribute metadatas.</param>
        /// <param name="oneToManyRelationshipMetadatas">The one to many relationship metadatas.</param>
        /// <returns></returns>
        private PropertyModel[] GetEntityReferenceProps(AttributeMetadata[] attributeMetadatas,
            OneToManyRelationshipMetadata[] oneToManyRelationshipMetadatas)
        {
            var propertyModels = new List<PropertyModel>();

            foreach (var attributeMetadata in attributeMetadatas)
            {
                if (FieldsFactory.IsLookup(attributeMetadata))
                {
                    var schemaName = attributeMetadata.SchemaName;
                    var propertyLogicalName = attributeMetadata.LogicalName;
                    var lookupTargetEntitys = ((LookupAttributeMetadata)attributeMetadata).Targets;
                    var description = Helpers.GetDescription(attributeMetadata.Description);
                    var valueField = $"_{propertyLogicalName}_value";
                    var isMultipleTargets = lookupTargetEntitys.Length > 1;

                    foreach (var lookupTargetEntity in lookupTargetEntitys)
                    {
                        var entityLogicalName = lookupTargetEntity;
                        var entityMetadata = _metadataRepository.GetEntityMetadata(entityLogicalName);
                        var entitySetName = entityMetadata.EntitySetName;
                        
                        var attributeName = GetAttributeName(oneToManyRelationshipMetadatas, lookupTargetEntity,
                            schemaName);

                        var entityReferenceAttributeModel = new EntityReferenceAttributeModel()
                        {
                            ValueField = valueField,
                            EntitySetName = entitySetName
                        };

                        propertyModels.Add(new PropertyModel()
                        {
                            AttributeName = attributeName,
                            Description = description,
                            PropertyName = isMultipleTargets ? $"{schemaName}_{entityMetadata.SchemaName}" : schemaName,
                            Type = Helpers.EntityReference,
                            Attributes =
                            {
                                entityReferenceAttributeModel
                            }
                        });

                        AddEntityPropertyIfContains(entityLogicalName, propertyModels, schemaName, description,
                            entityMetadata);
                    }
                }
            }

            return propertyModels.ToArray();
        }

        /// <summary>
        /// Adds the entity property if contains.
        /// </summary>
        /// <param name="entityLogicalName">Name of the entity logical.</param>
        /// <param name="propertyModels">The property models.</param>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="description">The description.</param>
        /// <param name="entityMetadata">The entity metadata.</param>
        private void AddEntityPropertyIfContains(string entityLogicalName, List<PropertyModel> propertyModels, string schemaName,
            string description, EntityMetadata entityMetadata)
        {
            if (_entities.Contains(entityLogicalName))
            {
                propertyModels.Add(new PropertyModel()
                {
                    AttributeName = schemaName,
                    Description = description,
                    PropertyName = $"{schemaName}Entity",
                    Type = entityMetadata.SchemaName,
                    Attributes =
                    {
                        new EntityAttributeModel()
                        {
                            EntityLogicalName = entityLogicalName,
                            AttributeName = schemaName
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        /// <param name="oneToManyRelationshipMetadatas">The one to many relationship metadatas.</param>
        /// <param name="lookupTargetEntity">The lookup target entity.</param>
        /// <param name="schemaName">Name of the schema.</param>
        /// <returns></returns>
        private string GetAttributeName(OneToManyRelationshipMetadata[] oneToManyRelationshipMetadatas,
            string lookupTargetEntity, string schemaName)
        {
            var relationships = oneToManyRelationshipMetadatas.Where(x =>
                x.ReferencedEntity.Equals(lookupTargetEntity, StringComparison.OrdinalIgnoreCase)).ToArray();
            var relationship = relationships.FirstOrDefault();

            var forAttribute = schemaName;

            if (relationships.Length > 1)
            {
                relationship = relationships.FirstOrDefault(x =>
                    x.ReferencingAttribute.Equals(schemaName, StringComparison.OrdinalIgnoreCase));
            }


            if (relationship != null)
            {
                forAttribute = relationship.ReferencingEntityNavigationPropertyName;
            }

            var attributeName = $"{forAttribute}@odata.bind";
            return attributeName;
        }
    }
}
