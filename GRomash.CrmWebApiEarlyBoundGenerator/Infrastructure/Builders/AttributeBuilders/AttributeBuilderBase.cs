using System;
using System.Collections.Generic;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.AttributeBuilders
{
    /// <summary>
    /// Attributes builder
    /// </summary>
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.BuilderBase" />
    public abstract class AttributeBuilderBase : BuilderBase
    {
        /// <summary>
        /// Builds the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public abstract string Build(CustomAttribute attribute);

        /// <summary>
        /// Gets the attribute builder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static AttributeBuilderBase GetAttributeBuilder<T>(T item) where T : CustomAttribute
        {
            Dictionary<Type, AttributeBuilderBase> builders = new Dictionary<Type, AttributeBuilderBase>()
            {
                {typeof(EntityReferenceAttributeModel), new EntityReferenceAttributeBuilder()},
                {typeof(EntityAttributeModel), new EntityAttributeBuilder()},
                {typeof(DateOnlyAttributeModel), new DateOnlyAttributeBuilder()},
            };

            return builders[item.GetType()];
        }
    }
}
