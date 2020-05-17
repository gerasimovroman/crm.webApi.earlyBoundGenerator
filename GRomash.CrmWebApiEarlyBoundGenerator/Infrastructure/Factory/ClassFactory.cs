using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Factory
{
    public class ClassFactory
    {
        /// <summary>
        /// Gets the class model.
        /// </summary>
        /// <param name="entityMetadata">The entity metadata.</param>
        /// <param name="nameSpace">The name space.</param>
        /// <returns></returns>
        public ClassModel GetClassModel(EntityMetadata entityMetadata, string nameSpace)
        {
            var classModel = new ClassModel
            {
                Namespace = nameSpace,
                EntityName = entityMetadata.SchemaName,
                PrimaryIdAttribute = entityMetadata.PrimaryIdAttribute,
                EntitySetName = GetEntitySetName(entityMetadata),
                EntityLogicalName = entityMetadata.LogicalName
            };

            return classModel;
        }

        /// <summary>
        /// Gets the name of the entity set.
        /// </summary>
        /// <param name="entityMetadata">The entity metadata.</param>
        /// <returns></returns>
        public static string GetEntitySetName(EntityMetadata entityMetadata)
        {
            return entityMetadata.EntitySetName;
        }
    }
}
