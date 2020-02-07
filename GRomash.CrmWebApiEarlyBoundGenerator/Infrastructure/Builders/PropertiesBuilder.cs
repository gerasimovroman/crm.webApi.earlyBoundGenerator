using System.Collections.Generic;
using System.Linq;
using System.Text;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.AttributeBuilders;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.Properties;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders
{
    /// <summary>
    /// Properties builder
    /// </summary>
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.BuilderBase" />
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.ITemplateBuilder{GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.PropertyModel}" />
    public class PropertiesBuilder : BuilderBase, ITemplateBuilder<PropertyModel>
    {
        /// <summary>
        /// Builds the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public string Build(IEnumerable<PropertyModel> items)
        {
            return BuildProperties(items);
        }

        /// <summary>
        /// Builds the properties.
        /// </summary>
        /// <param name="propertyModels">The property models.</param>
        /// <returns></returns>
        private string BuildProperties(IEnumerable<PropertyModel> propertyModels)
        {
            var propertyTemplate = Resources.PropertyTemplate;
            var stringBuilder = new StringBuilder();


            foreach (var propertyModel in propertyModels)
            {
                var replaces = new Dictionary<string, string>()
                {
                    {nameof(propertyModel.Description), propertyModel.Description},
                    {nameof(propertyModel.Type), propertyModel.Type},
                    {nameof(propertyModel.PropertyName), propertyModel.PropertyName},
                    {nameof(propertyModel.AttributeName), propertyModel.AttributeName},
                    {nameof(propertyModel.Attributes),null},
                };

                foreach (var attributeData in propertyModel.Attributes.Select(attribute => AttributeBuilderBase.GetAttributeBuilder(attribute).Build(attribute)))
                {
                    if (replaces[nameof(propertyModel.Attributes)] != null)
                    {
                        replaces[nameof(propertyModel.Attributes)] += attributeData;
                    }
                    else
                    {
                        replaces[nameof(propertyModel.Attributes)] = attributeData;
                    }
                }

                var property = Replace(replaces, propertyTemplate);

                stringBuilder.AppendLine(property);
            }

            return stringBuilder.ToString();
        }
    }
}
