using System.Collections.Generic;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders
{
    /// <summary>
    /// Template builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface ITemplateBuilder<in TModel>
    {
        /// <summary>
        /// Builds the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        string Build(IEnumerable<TModel> items);
    }
}
