using System.Collections.Generic;

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
    }
}
