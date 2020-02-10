using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders
{
    public class EntityModelBuilder : FileBuilder
    {
        public EntityModelBuilder(string outFolder) : base(outFolder)
        {
        }

        public void BuildClass(ClassModel entityModel, string nameSpace)
        {
            GenerateFile(nameSpace, entityModel.EntitySetName, GetClass(entityModel));
        }

        private CodeTypeDeclaration GetClass(ClassModel model)
        {
            var type = new CodeTypeDeclaration(model.EntityName)
            {
                IsClass = true,
            };

            type.BaseTypes.Add(new CodeTypeReference("Entity"));
            type.Members.Add(new CodeMemberField()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.New | MemberAttributes.Const,
                InitExpression = new CodePrimitiveExpression(model.EntityLogicalName),
                Name = "EntityLogicalName",
                Type = new CodeTypeReference(typeof(string))
            });
            type.Members.Add(new CodeMemberField()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.New | MemberAttributes.Const,
                InitExpression = new CodePrimitiveExpression(model.EntitySetName),
                Type = new CodeTypeReference(typeof(string)),
                Name = "EntitySetName",
            });
            type.Members.Add(new CodeMemberField()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.New | MemberAttributes.Const,
                InitExpression = new CodePrimitiveExpression(model.PrimaryIdAttribute),
                Type = new CodeTypeReference(typeof(string)),
                Name = "PrimaryIdAttribute",
            });
            type.Members.Add(new CodeConstructor()
            {
                Attributes = MemberAttributes.Public
            });
            type.Members.Add(new CodeConstructor()
            {
                Attributes = MemberAttributes.Public,
                Parameters =
                {
                    new CodeParameterDeclarationExpression(new CodeTypeReference("ExpandoObject"), "expandoObject")
                },
                BaseConstructorArgs =
                {
                    new CodeVariableReferenceExpression("expandoObject")
                }
            });

            type.Members.Add(GetFieldsClass(model.PrimaryIdAttribute, model.Fields));
            type.Members.Add(GetProps(model.PropertiesFields));
            type.Members.Add(GetSchemas(model.Schemas));
            type.Members.AddRange(GetProperties(model.Properties));


            return type;
        }

        private CodeTypeDeclaration GetFieldsClass(string primaryAttributeId, IEnumerable<FieldModel> fields)
        {
            var fieldModels = fields.ToList();
            fieldModels.Add(new FieldModel()
            {
                FieldName = "Id",
                AttributeName = primaryAttributeId
            });

            return GetSubClass("Fields", fieldModels);
        }

        private CodeTypeDeclaration GetProps(IEnumerable<FieldModel> props)
        {
            return GetSubClass("Properties", props);
        }

        private CodeTypeDeclaration GetSchemas(IEnumerable<FieldModel> schemas)
        {
            return GetSubClass("Schemas", schemas);
        }

        private static CodeTypeDeclaration GetSubClass(string name, IEnumerable<FieldModel> fields)
        {
            var codeType = new CodeTypeDeclaration()
            {
                IsClass = true,
                Attributes = MemberAttributes.Public,
                Name = name
            };

            foreach (var fieldModel in fields)
            {
                codeType.Members.Add(new CodeMemberField()
                {
                    Name = fieldModel.FieldName,
                    Attributes = MemberAttributes.Const | MemberAttributes.Public,
                    InitExpression = new CodePrimitiveExpression(fieldModel.AttributeName),
                    Type = new CodeTypeReference(typeof(string))
                });
            }

            return codeType;
        }

        private CodeTypeMember[] GetProperties(IEnumerable<PropertyModel> properties)
        {
            return properties.Select(x =>
            {
                var codeMemberProperty = new CodeMemberProperty()
                {
                    Name = x.PropertyName,
                    Type = new CodeTypeReference(x.Type),
                    Attributes = MemberAttributes.Public | MemberAttributes.Final,
                    HasGet = true,
                    HasSet = true,
                    GetStatements =
                    {
                        new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(
                                new CodeThisReferenceExpression(),
                                "GetAttributeValue",
                                new[]
                                {
                                    new CodeTypeReference(x.Type)
                                }
                            ),
                            new CodePrimitiveExpression(x.AttributeName)
                        ))
                    },
                    SetStatements =
                    {
                        new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "SetAttributeValue",
                            new CodePrimitiveExpression(x.AttributeName),
                            new CodeVariableReferenceExpression("value")
                        )
                    }
                };



                codeMemberProperty.CustomAttributes.AddRange(x.Attributes.Select(y =>
                {
                    var codeAttributeArguments = y.GetArguments().Select(z => new CodeAttributeArgument(null, new CodePrimitiveExpression(z))).ToArray();
                    return new CodeAttributeDeclaration(y.Type,
                        codeAttributeArguments);
                }).ToArray());

                return codeMemberProperty;
            }).ToArray();
        }
    }
}
