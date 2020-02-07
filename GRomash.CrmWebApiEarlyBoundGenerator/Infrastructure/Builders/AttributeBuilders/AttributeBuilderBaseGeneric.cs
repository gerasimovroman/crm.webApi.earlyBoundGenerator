using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.AttributeBuilders
{
    /// <summary>
    /// Abstract class of attribute builder
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.AttributeBuilders.AttributeBuilderBase" />
    public abstract class AttributeBuilderBaseGeneric<TAttribute> : AttributeBuilderBase where TAttribute : CustomAttribute
    {
        /// <summary>
        /// Builds the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public abstract string Build(TAttribute attribute);
    }
}
