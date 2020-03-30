using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Extensions
{
    /// <summary>
    /// Contains string extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Removes the special characters.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string str)
        {
            var sb = new StringBuilder();
            foreach (var c in str.Where(c => (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_' || char.IsWhiteSpace(c)))
            {
                if (char.IsWhiteSpace(c))
                {
                    sb.Append("_");
                }
                else
                {
                    sb.Append(c);
                }

            }
            return sb.ToString();
        }
    }
}
