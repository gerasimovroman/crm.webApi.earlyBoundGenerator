using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders
{
    /// <summary>
    /// Base builder class
    /// </summary>
    public class BuilderBase
    {
        /// <summary>
        /// Replaces the specified replaces in template.
        /// </summary>
        /// <param name="replaces">The replaces.</param>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        protected string Replace(Dictionary<string, string> replaces, string template)
        {
            var replaced = template;

            foreach (var replace in replaces)
            {
                var replaceKey = "{" + replace.Key + "}";
                replaced = replaced.Replace(replaceKey, replace.Value);
            }

            return replaced;
        }



        protected string FormatDescription(string template, string description)
        {
            const string descriptionFormat = "{Description}";
            var line = template
                .Split(new [] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault(x => x.Contains(descriptionFormat));

            if (line == null)
            {
                return description;
            }
            else
            {
                var indexOfFormatDescription = line.IndexOf(descriptionFormat, StringComparison.Ordinal);
                var preFormat = line.Substring(0, indexOfFormatDescription);
                var descriptionLines = description
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

                var stringBuilder = new StringBuilder();

                foreach (var descriptionLine in descriptionLines)
                {
                    stringBuilder.AppendLine($"{preFormat}{descriptionLine}");
                }

                return stringBuilder.ToString();
            }
        }
    }
}
