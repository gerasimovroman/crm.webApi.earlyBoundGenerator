using System.CodeDom;
using System.CodeDom.Compiler;
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
    public class FileBuilder 
    {
        /// <summary>
        /// The out folder
        /// </summary>
        private readonly string _outFolder;

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
        protected string CreatePath(string fileName)
        {
            if (!Directory.Exists(_outFolder))
            {
                Directory.CreateDirectory(_outFolder);
            }

            return Path.Combine(_outFolder, fileName);
        }

        protected void GenerateFile(string nameSpace, string fileName, params CodeTypeDeclaration[] codeTypeDeclarations)
        {
            var compileUnit = new CodeCompileUnit();
            var codeNamespace = new CodeNamespace(nameSpace);
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Reflection"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Linq"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Dynamic"));

            var provider =
                CodeDomProvider.CreateProvider("cs");


            codeNamespace.Types.AddRange(codeTypeDeclarations);

            compileUnit.Namespaces.Add(codeNamespace);

            using (var sourceFile = new StreamWriter(CreatePath($"{fileName}.cs")))
            {
                provider.GenerateCodeFromCompileUnit(compileUnit, sourceFile, null);
            }
        }
    }
}
