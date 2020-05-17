using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Static
{
    public class Helpers
    {
        /// <summary>
        /// The types mapping
        /// </summary>
        public static Dictionary<AttributeTypeCode, string> Types = new Dictionary<AttributeTypeCode, string>()
        {
            {AttributeTypeCode.Integer, "int?" },
            {AttributeTypeCode.State, "int?" },
            {AttributeTypeCode.Status, "int?" },
            {AttributeTypeCode.Picklist, "int?" },
            {AttributeTypeCode.BigInt, "long?" },
            {AttributeTypeCode.DateTime, "DateTime?" },
            {AttributeTypeCode.Lookup, NullableGuid },
            {AttributeTypeCode.Boolean, "bool?"  },
            {AttributeTypeCode.String, "String" },
            {AttributeTypeCode.Decimal, "decimal?" },
            {AttributeTypeCode.Double, "double?" },
            {AttributeTypeCode.Money, "decimal?" },
            {AttributeTypeCode.Owner, NullableGuid },
            {AttributeTypeCode.Uniqueidentifier, NullableGuid},
            {AttributeTypeCode.Memo, "String"},
        };

        /// <summary>
        /// The nullable unique identifier
        /// </summary>
        public const string NullableGuid = "Guid?";

        /// <summary>
        /// The entity reference
        /// </summary>
        public const string EntityReference = "EntityReference";


        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public static string GetDescription(Label label)
        {
            var stringBuilder = new StringBuilder();

            foreach (var labelLocalizedLabel in label.LocalizedLabels)
            {
                if (!string.IsNullOrWhiteSpace(labelLocalizedLabel.Label))
                {
                    stringBuilder.AppendLine(labelLocalizedLabel.Label);
                }    
            }

            return stringBuilder.ToString().Trim();
        }

        public static int EnglishLanguageCode = 1033;

    }
}
