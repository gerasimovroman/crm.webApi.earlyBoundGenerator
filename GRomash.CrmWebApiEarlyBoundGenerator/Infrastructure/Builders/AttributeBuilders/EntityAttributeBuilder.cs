using System.Collections.Generic;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes;
using GRomash.CrmWebApiEarlyBoundGenerator.Properties;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.AttributeBuilders
{
    /// <summary>
    /// Entity attribute builder class
    /// </summary>
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.AttributeBuilders.AttributeBuilderBaseGeneric{GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes.EntityAttributeModel}" />
    public class EntityAttributeBuilder : AttributeBuilderBaseGeneric<EntityAttributeModel>
    {
        /// <summary>
        /// Builds the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public override string Build(EntityAttributeModel attribute)
        {
            var replaces = new Dictionary<string, string>()
            {
                {nameof(attribute.AttributeName), attribute.AttributeName},
                {nameof(attribute.EntityLogicalName), attribute.EntityLogicalName},
            };

            return Replace(replaces, Resources.EntityAttributeTemplate);
        }

        /// <summary>
        /// Builds the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public override string Build(CustomAttribute attribute)
        {
            return Build((EntityAttributeModel) attribute);
        }
    }
}
