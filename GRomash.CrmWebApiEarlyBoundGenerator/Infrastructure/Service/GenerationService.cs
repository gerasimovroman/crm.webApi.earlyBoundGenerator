using System.Collections.Generic;
using System.Linq;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Factory;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Repository;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Service
{
    public class GenerationService
    {
        /// <summary>
        /// The metadata repository
        /// </summary>
        private readonly MetadataRepository _metadataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationService"/> class.
        /// </summary>
        /// <param name="metadataRepository">The metadata repository.</param>
        public GenerationService(MetadataRepository metadataRepository)
        {
            _metadataRepository = metadataRepository;
        }

        /// <summary>
        /// Generates the entities.
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        /// <param name="outFolder">The out folder.</param>
        /// <param name="entities">The entities.</param>
        /// <param name="entityMetadatas">The entity metadatas.</param>
        public void GenerateEntities(string nameSpace, string outFolder, string[] entities, IEnumerable<EntityMetadata> entityMetadatas)
        {
            var classBuilder = new ClassBuilder();
            var fieldsFactory = new FieldsFactory();
            var propsFactory = new PropertiesFactory(entities, _metadataRepository);
            var entityClassBuilder = new EntityClassBuilder(outFolder);
            var entityModelBuilder = new EntityModelBuilder(outFolder);
            var fileBuilder = new FileBuilder(outFolder);

            //entityClassBuilder.Create(nameSpace);
            fileBuilder.BuildBaseClass(nameSpace);

            foreach (var entityMetadata in entityMetadatas)
            {
                var classModel = classBuilder.GetClassModel(entityMetadata, nameSpace);

                var relationshipMetadata = entityMetadata.OneToManyRelationships.Union(entityMetadata.ManyToOneRelationships).ToArray();

                classModel.Properties =
                    propsFactory.GetPropertyModels(entityMetadata.Attributes, relationshipMetadata);
                classModel.Fields =
                    fieldsFactory.GetFields(entityMetadata.Attributes);
                classModel.Schemas = fieldsFactory.GetSchemaNames(entityMetadata.Attributes);
                classModel.PropertiesFields = fieldsFactory.GetProperties(entityMetadata.ManyToManyRelationships, entityMetadata.ManyToOneRelationships);

                fileBuilder.BuildClass(classModel);
                //entityModelBuilder.BuildClass(classModel, nameSpace);
            }
        }

        /// <summary>
        /// Generates the option sets.
        /// </summary>
        /// <param name="nameSpace">The namespace.</param>
        /// <param name="outFolder">The out folder.</param>
        /// <param name="metadata">The metadata.</param>
        public void GenerateOptionSets(string nameSpace, string outFolder, IEnumerable<PicklistAttributeMetadata> metadata)
        {
            var optionSetValueFactory = new OptionSetValueFactory();
            var optionSetBuilder = new OptionSetBuilder(_metadataRepository);
            var fileBuilder = new FileBuilder(outFolder);

            foreach (var optionSetMetadata in metadata)
            {
                var model = optionSetBuilder.GetOptionSetModel(optionSetMetadata, nameSpace);
                model.Values = optionSetValueFactory.GetValues(optionSetMetadata).ToArray();
                fileBuilder.BuildOptionSet(model);
            }
        }
    }
}
