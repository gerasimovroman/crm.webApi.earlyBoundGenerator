using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders
{
    public class EntityModelBuilder : FileBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityModelBuilder"/> class.
        /// </summary>
        /// <param name="outFolder">The out folder.</param>
        public EntityModelBuilder(string outFolder) : base(outFolder)
        {
        }

        /// <summary>
        /// Builds the class.
        /// </summary>
        /// <param name="entityModel">The entity model.</param>
        /// <param name="nameSpace">The name space.</param>
        public void BuildClass(ClassModel entityModel, string nameSpace)
        {
            GenerateFile(nameSpace, entityModel.EntityName, GetClass(entityModel));
        }

        /// <summary>
        /// Gets the class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the fields class.
        /// </summary>
        /// <param name="primaryAttributeId">The primary attribute identifier.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the props.
        /// </summary>
        /// <param name="props">The props.</param>
        /// <returns></returns>
        private CodeTypeDeclaration GetProps(IEnumerable<FieldModel> props)
        {
            return GetSubClass("Properties", props);
        }

        /// <summary>
        /// Gets the schemas.
        /// </summary>
        /// <param name="schemas">The schemas.</param>
        /// <returns></returns>
        private CodeTypeDeclaration GetSchemas(IEnumerable<FieldModel> schemas)
        {
            return GetSubClass("Schemas", schemas);
        }

        /// <summary>
        /// Gets the sub class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
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
                                "GetAttributeValue", new CodeTypeReference(x.Type)),
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

                AddSummaryComment(codeMemberProperty, x.Description);

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
