using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Repository;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Static;
using Microsoft.Xrm.Sdk.Metadata;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders
{
    /// <summary>
    /// Builder of OptionSetModel
    /// </summary>
    public class OptionSetBuilder : FileBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionSetBuilder"/> class.
        /// </summary>
        /// <param name="outFolder">The out folder.</param>
        public OptionSetBuilder(string outFolder) : base(outFolder)
        {
        }

        /// <summary>
        /// Builds the option set.
        /// </summary>
        /// <param name="optionSetModel">The option set model.</param>
        public void BuildOptionSet(OptionSetModel optionSetModel)
        {
            var compileUnit = new CodeCompileUnit();
            var codeNamespace = new CodeNamespace(optionSetModel.Namespace);
            compileUnit.Namespaces.Add(codeNamespace);

            var enumType = new CodeTypeDeclaration(optionSetModel.OptionSetName) { IsEnum = true };
            AddSummaryComment(enumType, optionSetModel.Description);

            foreach (var optionSetValueModel in optionSetModel.Values)
            {
                var f = new CodeMemberField
                {
                    Name = optionSetValueModel.Name,
                    InitExpression = new CodePrimitiveExpression(optionSetValueModel.Value)
                };
      
                AddSummaryComment(f, optionSetValueModel.Description);

                enumType.Members.Add(f);
            }

            var provider =
                CodeDomProvider.CreateProvider("cs");

            codeNamespace.Types.Add(enumType);

            using (var sourceFile = new StreamWriter(CreatePath($"{optionSetModel.OptionSetName}.cs")))
            {
                provider.GenerateCodeFromCompileUnit(compileUnit, sourceFile, null);
            }
        }
    }
}
