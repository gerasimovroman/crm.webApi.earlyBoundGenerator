using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes;
using GRomash.CrmWebApiEarlyBoundGenerator.Properties;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.AttributeBuilders
{
    /// <summary>
    /// Date only attribute builder class
    /// </summary>
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.AttributeBuilders.AttributeBuilderBaseGeneric{GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes.DateOnlyAttributeModel}" />
    public class DateOnlyAttributeBuilder : AttributeBuilderBaseGeneric<DateOnlyAttributeModel>
    {
        /// <summary>
        /// Builds the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public override string Build(DateOnlyAttributeModel attribute)
        {
            return Resources.DateOnlyAttributeTemplate;
        }

        /// <summary>
        /// Builds the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public override string Build(CustomAttribute attribute)
        {
            return Build((DateOnlyAttributeModel) attribute);
        }
    }
}
