using System.Collections.Generic;
using System.Linq;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Static;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Factory
{
    /// <summary>
    /// Factory for field model
    /// </summary>
    public class FieldsFactory
    {
        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <param name="attributeMetadatas">The attribute metadatas.</param>
        /// <returns></returns>
        public FieldModel[] GetFields(AttributeMetadata[] attributeMetadatas)
        {
            var fieldModels = new List<FieldModel>();

            foreach (var attributeMetadata in attributeMetadatas)
            {
                fieldModels.Add(new FieldModel()
                {
                    FieldName = attributeMetadata.SchemaName,
                    AttributeName = attributeMetadata.LogicalName
                });

                if (IsLookup(attributeMetadata))
                {
                    const string format = "_{0}_value";

                    fieldModels.Add(new FieldModel()
                    {
                        FieldName = string.Format(format, attributeMetadata.SchemaName),
                        AttributeName = string.Format(format, attributeMetadata.LogicalName),
                    });
                }
            }

            return fieldModels.OrderBy(x => x.FieldName).ToArray();
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="metadatas">The metadatas.</param>
        /// <param name="relationshipMetadatas">The relationship metadatas.</param>
        /// <returns></returns>
        public FieldModel[] GetProperties(ManyToManyRelationshipMetadata[] metadatas,
            OneToManyRelationshipMetadata[] relationshipMetadatas)
        {
            var fieldModels = new List<FieldModel>();

            foreach (var manyToManyRelationshipMetadata in metadatas)
            {
                var fieldModel = new FieldModel()
                {
                    AttributeName = manyToManyRelationshipMetadata.SchemaName,
                    FieldName = manyToManyRelationshipMetadata.SchemaName
                };

                fieldModels.Add(fieldModel);
            }

            foreach (var oneToManyRelationshipMetadata in relationshipMetadatas)
            {
                var fieldModel = new FieldModel()
                {
                    AttributeName = oneToManyRelationshipMetadata.ReferencingEntityNavigationPropertyName,
                    FieldName = oneToManyRelationshipMetadata.ReferencingEntityNavigationPropertyName
                };

                fieldModels.Add(fieldModel);
            }

            return fieldModels.OrderBy(x => x.FieldName).ToArray();
        }

        /// <summary>
        /// Gets the schema names.
        /// </summary>
        /// <param name="attributeMetadatas">The attribute metadatas.</param>
        /// <returns></returns>
        public FieldModel[] GetSchemaNames(AttributeMetadata[] attributeMetadatas)
        {
            var fieldModels = new List<FieldModel>();

            foreach (var attributeMetadata in attributeMetadatas)
            {
                fieldModels.Add(new FieldModel()
                {
                    FieldName = attributeMetadata.SchemaName,
                    AttributeName = attributeMetadata.SchemaName
                });
            }

            return fieldModels.OrderBy(x => x.FieldName).ToArray();
        }

        /// <summary>
        /// Determines whether [is supported type] [the specified metadata].
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>
        ///   <c>true</c> if [is supported type] [the specified metadata]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSupportedType(AttributeMetadata metadata)
        {
            if (metadata.AttributeType == AttributeTypeCode.String)
            {
                return string.IsNullOrWhiteSpace(metadata.AttributeOf);
            }

            return metadata.AttributeType.HasValue &&
                   Helpers.Types.ContainsKey(metadata.AttributeType.Value);
        }

        /// <summary>
        /// Determines whether the specified metadata is lookup.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>
        ///   <c>true</c> if the specified metadata is lookup; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLookup(AttributeMetadata metadata)
        {
            return metadata.AttributeType == AttributeTypeCode.Lookup |
                metadata.AttributeType == AttributeTypeCode.Owner;
        }
    }
}
