using System.Collections.Generic;
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
            {AttributeTypeCode.String, "string" },
            {AttributeTypeCode.Decimal, "decimal?" },
            {AttributeTypeCode.Double, "double?" },
            {AttributeTypeCode.Money, "decimal?" },
            {AttributeTypeCode.Owner, NullableGuid },
            {AttributeTypeCode.Uniqueidentifier, NullableGuid},
            {AttributeTypeCode.Memo, "string"},
        };

        /// <summary>
        /// The nullable unique identifier
        /// </summary>
        public const string NullableGuid = "Guid?";

        /// <summary>
        /// The entity reference
        /// </summary>
        public const string EntityReference = "EntityReference";
    }
}
