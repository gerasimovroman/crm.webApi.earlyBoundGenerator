using System.Linq;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Factory;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.Settings;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Repository;

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
        /// <param name="entitiesGenerationSettings">The entities generation settings.</param>
        public void GenerateEntities(EntitiesGenerationSettings entitiesGenerationSettings)
        {
            var fieldsFactory = new FieldsFactory();
            var propsFactory = new PropertiesFactory(entitiesGenerationSettings.Entities, _metadataRepository);
            var entityClassBuilder = new EntityClassBuilder(entitiesGenerationSettings.OutFolder);
            var entityModelBuilder = new EntityModelBuilder(entitiesGenerationSettings.OutFolder);
            var classFactory = new ClassFactory();
            
            //build Entity class
            entityClassBuilder.Create(entitiesGenerationSettings.NameSpace);

            foreach (var entityMetadata in entitiesGenerationSettings.EntityMetadatas)
            {
                var classModel = classFactory.GetClassModel(entityMetadata, entitiesGenerationSettings.NameSpace);

                var relationshipMetadata = entityMetadata.OneToManyRelationships.Union(entityMetadata.ManyToOneRelationships).ToArray();

                classModel.Properties =
                    propsFactory.GetPropertyModels(entityMetadata.Attributes, relationshipMetadata);
                classModel.Fields =
                    fieldsFactory.GetFields(entityMetadata.Attributes);
                classModel.Schemas = fieldsFactory.GetSchemaNames(entityMetadata.Attributes);
                classModel.PropertiesFields = fieldsFactory.GetProperties(entityMetadata.ManyToManyRelationships, entityMetadata.ManyToOneRelationships);

                //build model class
                entityModelBuilder.BuildClass(classModel, entitiesGenerationSettings.NameSpace);
            }
        }

        /// <summary>
        /// Generates the option sets.
        /// </summary>
        /// <param name="optionSetGenerationSetting">The option set generation setting.</param>
        public void GenerateOptionSets(OptionSetGenerationSetting optionSetGenerationSetting)
        {
            var optionSetFactory = new OptionSetFactory(_metadataRepository);
            var optionSetBuilder = new OptionSetBuilder(optionSetGenerationSetting.OutFolder);
            var optionSetValueFactory = new OptionSetValueFactory();

            foreach (var optionSetMetadata in optionSetGenerationSetting.Metadata)
            {
                var model = optionSetFactory.GetOptionSetModel(optionSetMetadata, optionSetGenerationSetting.NameSpace);
                model.Values = optionSetValueFactory.GetValues(optionSetMetadata).ToArray();


                //build option set file
                optionSetBuilder.BuildOptionSet(model);
            }
        }
    }
}
