using System.Collections.Generic;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes;
using GRomash.CrmWebApiEarlyBoundGenerator.Properties;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.AttributeBuilders
{
    /// <summary>
    /// Entity reference builder class
    /// </summary>
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.AttributeBuilders.AttributeBuilderBaseGeneric{GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes.EntityReferenceAttributeModel}" />
    public class EntityReferenceAttributeBuilder : AttributeBuilderBaseGeneric<EntityReferenceAttributeModel>
    {
        /// <summary>
        /// Builds the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public override string Build(EntityReferenceAttributeModel attribute)
        {
            var replaces = new Dictionary<string, string>()
            {
                {nameof(attribute.EntitySetName), attribute.EntitySetName},
                {nameof(attribute.ValueField), attribute.ValueField},
            };

            return Replace(replaces, Resources.EntityReferenceAttributeTemplate);
        }

        /// <summary>
        /// Builds the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public override string Build(CustomAttribute attribute)
        {
            return Build((EntityReferenceAttributeModel)attribute);
        }
    }
}
