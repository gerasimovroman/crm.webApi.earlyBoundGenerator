using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Extensions;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Static;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.UnidecodeSharpFork;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Factory
{
    /// <summary>
    /// Factory of OptionSetValueModel class
    /// </summary>
    public class OptionSetValueFactory
    {
        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns></returns>
        public IEnumerable<OptionSetValueModel> GetValues(EnumAttributeMetadata metadata)
        {
            return metadata.OptionSet.Options.Select(metadataOption => 
                new OptionSetValueModel()
            {
                Name = GetName(metadataOption.Label),
                Value = metadataOption.Value ?? 0,
                Description = Helpers.GetDescription(metadataOption.Description)
            });
        }



        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        private string GetName(Label label)
        {
            var localizedLabels = label.LocalizedLabels.OrderByDescending(x => x.LanguageCode == Helpers.EnglishLanguageCode);
            var labelText = localizedLabels.First().Label.Unidecode().RemoveSpecialCharacters();

            if (labelText.Length > 0)
            {
                if (char.IsDigit(labelText[0]))
                {
                    var stringBuilder = new StringBuilder(labelText);
                    stringBuilder.Insert(0, "_");
                    labelText = stringBuilder.ToString();
                }
            }

            return labelText;
        }
    }
}
