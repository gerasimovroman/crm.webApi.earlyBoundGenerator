using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.Properties;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders
{
    /// <summary>
    /// Builder Option Set Values
    /// </summary>
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.BuilderBase" />
    public class OptionSetValuesBuilder : BuilderBase
    {
        /// <summary>
        /// Builds the values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public string BuildValues(IEnumerable<OptionSetValueModel> values)
        {
            var template = Resources.OptionSetValueTemplate;
            var stringBuilder = new StringBuilder();

            foreach (var model in values)
            {
                var replaces = new Dictionary<string, string>()
                {
                    {nameof(model.Name), model.Name},
                    {nameof(model.Description), model.Description},
                    {nameof(model.Value), model.Value.ToString()},
                };

                var field = Replace(replaces, template);

                stringBuilder.AppendLine(field);
            }

            return stringBuilder.ToString();
        }
    }
}
