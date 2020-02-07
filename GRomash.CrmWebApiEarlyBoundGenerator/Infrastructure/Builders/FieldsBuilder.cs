using System.Collections.Generic;
using System.Text;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.Properties;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders
{
    /// <summary>
    /// Fields builder
    /// </summary>
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.BuilderBase" />
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.ITemplateBuilder{GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.FieldModel}" />
    internal class FieldsBuilder : BuilderBase, ITemplateBuilder<FieldModel>
    {
        /// <summary>
        /// Builds the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public string Build(IEnumerable<FieldModel> items)
        {
            return BuildFields(items);
        }


        /// <summary>
        /// Builds the fields.
        /// </summary>
        /// <param name="fieldModels">The field models.</param>
        /// <returns></returns>
        private string BuildFields(IEnumerable<FieldModel> fieldModels)
        {
            var fieldTemplate = Resources.FieldConstTemplate;
            var stringBuilder = new StringBuilder();

            foreach (var fieldModel in fieldModels)
            {
                var replaces = new Dictionary<string, string>()
                {
                    {nameof(fieldModel.FieldName), fieldModel.FieldName},
                    {nameof(fieldModel.AttributeName), fieldModel.AttributeName},
                };

                var field = Replace(replaces, fieldTemplate);

                stringBuilder.AppendLine(field);
            }

            return stringBuilder.ToString();
        }
    }
}
