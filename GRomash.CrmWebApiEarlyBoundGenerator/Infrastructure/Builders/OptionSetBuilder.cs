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
            enumType.Comments.Add(new CodeCommentStatement("<summary>"));
            enumType.Comments.Add(new CodeCommentStatement(optionSetModel.Description));
            enumType.Comments.Add(new CodeCommentStatement("</summary>"));


            foreach (var optionSetValueModel in optionSetModel.Values)
            {
                var f = new CodeMemberField
                {
                    Name = optionSetValueModel.Name,
                    InitExpression = new CodePrimitiveExpression(optionSetValueModel.Value)
                };
                f.Comments.Add(new CodeCommentStatement("<summary>"));
                f.Comments.Add(new CodeCommentStatement(optionSetValueModel.Description));
                f.Comments.Add(new CodeCommentStatement("</summary>"));
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
