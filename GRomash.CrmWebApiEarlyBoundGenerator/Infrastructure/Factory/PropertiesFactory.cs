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
                        var attributeName = $"nameof({attributeMetadata.SchemaName})";
                        var description = GetDescription(attributeMetadata);

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
        /// Gets the description.
        /// </summary>
        /// <param name="attributeMetadata">The attribute metadata.</param>
        /// <returns></returns>
        private string GetDescription(AttributeMetadata attributeMetadata)
        {
            return attributeMetadata.Description.LocalizedLabels.Select(x => x.Label)
                .Aggregate(string.Empty,
                    (x, y) => $"{x} {y}");
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
                if (attributeMetadata.AttributeType == AttributeTypeCode.Lookup)
                {
                    var schemaName = attributeMetadata.SchemaName;
                    var propertyLogicalName = attributeMetadata.LogicalName;
                    var description = GetDescription(attributeMetadata);
                    var valueField = $"_{propertyLogicalName}_value";
                    var relationship = oneToManyRelationshipMetadatas.FirstOrDefault(x => x.ReferencedAttribute == attributeMetadata.LogicalName ||
                                                                                 x.ReferencingAttribute == attributeMetadata.LogicalName);

                    if (relationship != null)
                    {
                        var entityLogicalName = relationship.ReferencedEntity;
                        var entityMetadata = _metadataRepository.GetEntityMetadata(entityLogicalName);
                        var entitySetName = entityMetadata.EntitySetName;
                        var type = Helpers.EntityReference;
                        var attributeName = $"\"{schemaName}@odata.bind\"";


                        var entityReferenceAttributeModel = new EntityReferenceAttributeModel()
                        {
                            ValueField = valueField,
                            EntitySetName = entitySetName
                        };

                        propertyModels.Add(new PropertyModel()
                        {
                            AttributeName = attributeName,
                            Description = description,
                            PropertyName = schemaName,
                            Type = type,
                            Attributes =
                            {
                                entityReferenceAttributeModel
                            }
                        });


                        if (_entities.Contains(entityLogicalName))
                        {
                            propertyModels.Add(new PropertyModel()
                            {
                                AttributeName = $"nameof({schemaName})",
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
                }

            }

            return propertyModels.ToArray();
        }
    }
}
