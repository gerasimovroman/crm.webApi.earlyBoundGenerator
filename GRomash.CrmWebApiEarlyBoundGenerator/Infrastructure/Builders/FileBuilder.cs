using System.Collections.Generic;
using System.IO;
using System.Text;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.Properties;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders
{
    /// <summary>
    /// File builder
    /// </summary>
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders.BuilderBase" />
    public class FileBuilder : BuilderBase
    {
        /// <summary>
        /// The out folder
        /// </summary>
        private readonly string _outFolder;
        /// <summary>
        /// The entity class file name
        /// </summary>
        private const string EntityClassFileName = "Entity.cs";


        /// <summary>
        /// Initializes a new instance of the <see cref="FileBuilder"/> class.
        /// </summary>
        /// <param name="outFolder">The out folder.</param>
        public FileBuilder(string outFolder)
        {
            _outFolder = outFolder;
        }

        /// <summary>
        /// Creates the path.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        private string CreatePath(string fileName)
        {
            if (!Directory.Exists(_outFolder))
            {
                Directory.CreateDirectory(_outFolder);
            }

            return Path.Combine(_outFolder, fileName);
        }

        /// <summary>
        /// Builds the base class.
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        public void BuildBaseClass(string nameSpace)
        {
            var contents = Replace(new Dictionary<string, string>()
            {
                {nameof(ClassModel.Namespace), nameSpace}
            }, Resources.Entity);

            File.WriteAllText(CreatePath(EntityClassFileName), contents);
        }

        /// <summary>
        /// Builds the class.
        /// </summary>
        /// <param name="classModel">The class model.</param>
        public void BuildClass(ClassModel classModel)
        {
            var classTemplate = Resources.ClassTemplate;

            var propsBuilder = new PropertiesBuilder();
            var fieldsBuilder = new FieldsBuilder();

            var replaces = new Dictionary<string, string>()
            {
                {nameof(classModel.Namespace), classModel.Namespace},
                {nameof(classModel.EntityName), classModel.EntityName},
                {nameof(classModel.EntitySetName), classModel.EntitySetName},
                {nameof(classModel.EntityLogicalName), classModel.EntityLogicalName},
                {nameof(classModel.PrimaryIdAttribute), classModel.PrimaryIdAttribute},
                {nameof(classModel.Properties), propsBuilder.Build(classModel.Properties)},
                {nameof(classModel.Fields), fieldsBuilder.Build(classModel.Fields)},
                {nameof(classModel.PropertiesFields), fieldsBuilder.Build(classModel.PropertiesFields)},
                {nameof(classModel.Schemas), fieldsBuilder.Build(classModel.Schemas)},
            };

            classTemplate = Replace(replaces, classTemplate);

            File.WriteAllText(CreatePath($"{classModel.EntityName}.cs"), classTemplate, Encoding.Unicode);
        }
    }
}
