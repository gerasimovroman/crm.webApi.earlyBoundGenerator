using System;
using System.CodeDom;
using System.Dynamic;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Builders
{
    public class EntityClassBuilder : FileBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityClassBuilder"/> class.
        /// </summary>
        /// <param name="outFolder">The out folder.</param>
        public EntityClassBuilder(string outFolder) : base(outFolder)
        {
        }

        /// <summary>
        /// Creates the specified name space.
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        public void Create(string nameSpace)
        {
            GenerateFile(nameSpace, "Entity",
                EntityCreate(),
                EntityReference(),
                EntityReferenceAttribute(),
                GetEntityAttribute(),
                CreateOnlyDateAttribute());
        }


        /// <summary>
        /// Creates the only date attribute.
        /// </summary>
        /// <returns></returns>
        private CodeTypeDeclaration CreateOnlyDateAttribute()
        {
            string name = "OnlyDateAttribute";

            var type = new CodeTypeDeclaration(name)
            {
                IsClass = true,
                BaseTypes =
            {
                typeof(Attribute)
            }
            };

            type.CustomAttributes.Add(new CodeAttributeDeclaration("ExcludeFromCodeCoverageAttribute"));

            type.Members.Add(new CodeMemberField()
            {
                Type = new CodeTypeReference(typeof(string)),
                Attributes = MemberAttributes.Const | MemberAttributes.Public,
                Name = "Format",
                InitExpression = new CodePrimitiveExpression("yyyy-MM-dd")
            });

            return type;
        }
        /// <summary>
        /// Gets the entity attribute.
        /// </summary>
        /// <returns></returns>
        private CodeTypeDeclaration GetEntityAttribute()
        {
            var name = "EntityAttribute";
            var type = new CodeTypeDeclaration(name)
            {
                IsClass = true,
                BaseTypes =
                {
                    typeof(Attribute)
                }
            };

            type.CustomAttributes.Add(new CodeAttributeDeclaration("ExcludeFromCodeCoverageAttribute"));



            type.Members.AddRange(CreateAutoProperty("EntityLogicalName", "_entityLogicalName", typeof(string)));
            type.Members.AddRange(CreateAutoProperty("AttributeName", "_attributeName", typeof(string)));


            type.Members.Add(new CodeConstructor()
            {
                Parameters =
                {
                    new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "entityLogicalName"),
                    new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "attributeName"),
                },
                Attributes = MemberAttributes.Public,
                Statements =
                {
                    new CodeAssignStatement(new CodeVariableReferenceExpression("EntityLogicalName"), new CodeVariableReferenceExpression("entityLogicalName")),
                    new CodeAssignStatement(new CodeVariableReferenceExpression("AttributeName"), new CodeVariableReferenceExpression("attributeName")),
                }

            });

            return type;
        }
        /// <summary>
        /// Entities the reference attribute.
        /// </summary>
        /// <returns></returns>
        private CodeTypeDeclaration EntityReferenceAttribute()
        {
            var name = "EntityReferenceAttribute";
            var type = new CodeTypeDeclaration(name)
            {
                IsClass = true,
                BaseTypes =
                {
                    typeof(Attribute)
                }
            };

            type.CustomAttributes.Add(new CodeAttributeDeclaration("ExcludeFromCodeCoverageAttribute"));


            type.Members.AddRange(CreateAutoProperty("EntitySetName", "_entitySetName", typeof(string)));
            type.Members.AddRange(CreateAutoProperty("ValueField", "_valueField", typeof(string)));


            type.Members.Add(new CodeConstructor()
            {
                Parameters =
                {
                    new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "entitySetName"),
                    new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "valueField"),
                },
                Attributes = MemberAttributes.Public,
                Statements =
                {
                    new CodeAssignStatement(new CodeVariableReferenceExpression("EntitySetName"), new CodeVariableReferenceExpression("entitySetName")),
                    new CodeAssignStatement(new CodeVariableReferenceExpression("ValueField"), new CodeVariableReferenceExpression("valueField")),
                }

            });

            return type;
        }
        /// <summary>
        /// Entities the reference.
        /// </summary>
        /// <returns></returns>
        private CodeTypeDeclaration EntityReference()
        {
            string name = "EntityReference";

            var type = new CodeTypeDeclaration(name)
            {
                IsClass = true,
            };

            type.CustomAttributes.Add(new CodeAttributeDeclaration("ExcludeFromCodeCoverageAttribute"));

            type.Members.AddRange(CreateAutoProperty("EntitySetName", "_entitySetName", typeof(string)));
            type.Members.AddRange(CreateAutoProperty("EntityId", "_entityId", typeof(Guid)));

            type.Members.Add(new CodeConstructor()
            {
                Parameters =
                {
                    new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "entitySetName"),
                    new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Guid)), "entityId"),
                },
                Attributes = MemberAttributes.Public,
                Statements =
                {
                    new CodeAssignStatement(new CodeVariableReferenceExpression("EntitySetName"), new CodeVariableReferenceExpression("entitySetName")),
                    new CodeAssignStatement(new CodeVariableReferenceExpression("EntityId"), new CodeVariableReferenceExpression("entityId")),
                }

            });

            return type;
        }
        /// <summary>
        /// Entities the create.
        /// </summary>
        /// <returns></returns>
        private CodeTypeDeclaration EntityCreate()
        {
            var name = "Entity";

            var type = new CodeTypeDeclaration(name)
            {
                IsClass = true,
            };

            type.CustomAttributes.Add(new CodeAttributeDeclaration("ExcludeFromCodeCoverageAttribute"));

            var publicAndFinal = MemberAttributes.Public | MemberAttributes.Final;

            var attributesMember = new CodeMemberField()
            {
                Attributes = publicAndFinal,
                Name = "Attributes",
                Type = new CodeTypeReference("Dictionary<string, object>"),
                InitExpression = new CodeObjectCreateExpression("Dictionary<string, object>")
            };

            var formattedValuesMemberField = new CodeMemberField()
            {
                Attributes = publicAndFinal,
                Name = "FormattedValues",
                Type = new CodeTypeReference("Dictionary<string, string>"),
                InitExpression = new CodeObjectCreateExpression("Dictionary<string, string>")
            };

            var getIdAttributeMethodReferenceExpression =
                new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "GetIdAttribute");

            var idMember = new CodeMemberProperty()
            {
                Attributes = publicAndFinal,
                Type = new CodeTypeReference("Guid"),
                Name = "Id",
                GetStatements =
                {
                    new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(
                            new CodeThisReferenceExpression(),
                            "GetAttributeValue", new CodeTypeReference(typeof(Guid))),
                        new CodeMethodInvokeExpression(getIdAttributeMethodReferenceExpression)
                    ))
                },
                SetStatements =
                {
                    new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "SetAttributeValue",
                        new CodeMethodInvokeExpression(getIdAttributeMethodReferenceExpression),
                        new CodeVariableReferenceExpression("value")
                    )
                }
            };

            var entitySetNameProp = new CodeMemberProperty()
            {
                Attributes = publicAndFinal,
                Type = new CodeTypeReference(typeof(string)),
                Name = "EntitySetName",
                GetStatements =
                {
                    new CodeMethodReturnStatement(new CodeSnippetExpression(
                        "GetType().GetField(nameof(EntitySetName), " +
                        "BindingFlags.Public | BindingFlags.Static | " +
                        "BindingFlags.FlattenHierarchy)" +
                        "?.GetValue(null)?.ToString()"))
                },
                HasSet = false
            };

            var entityLogicalNameProp = new CodeMemberProperty()
            {
                Attributes = publicAndFinal,
                Type = new CodeTypeReference(typeof(string)),
                Name = "EntityLogicalName",
                GetStatements =
                {
                    new CodeMethodReturnStatement(new CodeSnippetExpression(
                        "GetType().GetField(nameof(EntityLogicalName), " +
                        "BindingFlags.Public | BindingFlags.Static | " +
                        "BindingFlags.FlattenHierarchy)" +
                        "?.GetValue(null)?.ToString()"))
                },
                HasSet = false
            };

            var baseConstructor = new CodeConstructor()
            {
                Attributes = MemberAttributes.Public
            };

            var expandoConstructor = GetConstructor();
            var getAttrValueMethod = GetAttrValueMethod(publicAndFinal);
            var setAttrValueMethod = SetAttrValueMethod(publicAndFinal);
            var toExpandoMethod = ToExpandoMethod(publicAndFinal);
            var getPublicInstancePropertiesMethod = GetPublicInstancePropertiesMethod();
            var getIdAttributeMethod = GetIdAttributeMethod();
            var toEntityReferenceMethod = ToEntityReferenceMethod(publicAndFinal);

            type.Members.Add(attributesMember);
            type.Members.Add(formattedValuesMemberField);
            type.Members.Add(idMember);
            type.Members.Add(entitySetNameProp);
            type.Members.Add(entityLogicalNameProp);
            type.Members.Add(baseConstructor);
            type.Members.Add(expandoConstructor);
            type.Members.Add(getAttrValueMethod);
            type.Members.Add(setAttrValueMethod);
            type.Members.Add(toExpandoMethod);
            type.Members.Add(toEntityReferenceMethod);
            type.Members.Add(getPublicInstancePropertiesMethod);
            type.Members.Add(getIdAttributeMethod);



            return type;
        }

        /// <summary>
        /// Method ToEntityReference
        /// </summary>
        /// <param name="publicAndFinal">The public and final.</param>
        /// <returns></returns>
        private static CodeMemberMethod ToEntityReferenceMethod(MemberAttributes publicAndFinal)
        {
            var toEntityReferenceMethod = new CodeMemberMethod
            {
                Name = "ToEntityReference",
                ReturnType = new CodeTypeReference("EntityReference"),
                Attributes = publicAndFinal,
                Statements =
                {
                    new CodeMethodReturnStatement(new CodeObjectCreateExpression("EntityReference", new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "EntitySetName"), new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Id")))
                }
            };
            return toEntityReferenceMethod;
        }

        /// <summary>
        /// Gets the identifier attribute method.
        /// </summary>
        /// <returns></returns>
        private static CodeMemberMethod GetIdAttributeMethod()
        {
            var getIdAttributeMethod = new CodeMemberMethod
            {
                Name = "GetIdAttribute",
                ReturnType = new CodeTypeReference(typeof(string)),
                Attributes = MemberAttributes.Private,
                Statements =
                {
                    new CodeMethodReturnStatement(new
                        CodeSnippetExpression(
                            "GetType().GetField(\"PrimaryIdAttribute\", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)?.GetValue(null)?.ToString()"))
                }
            };
            return getIdAttributeMethod;
        }

        /// <summary>
        /// Gets the public instance properties method.
        /// </summary>
        /// <returns></returns>
        private static CodeMemberMethod GetPublicInstancePropertiesMethod()
        {
            var getPublicInstancePropertiesMethod = new CodeMemberMethod
            {
                Name = "GetPublicInstanceProperties",
                ReturnType = new CodeTypeReference("PropertyInfo[]"),
                Attributes = MemberAttributes.Private,
                Statements =
                {
                    new CodeMethodReturnStatement(
                        new CodeSnippetExpression(
                            "GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)"))
                }
            };
            return getPublicInstancePropertiesMethod;
        }

        /// <summary>
        /// Converts to expandomethod.
        /// </summary>
        /// <param name="publicAndFinal">The public and final.</param>
        /// <returns></returns>
        private static CodeMemberMethod ToExpandoMethod(MemberAttributes publicAndFinal)
        {
            var toExpandoMethod = new CodeMemberMethod
            {
                Name = "ToExpandoObject",
                Attributes = publicAndFinal,
                ReturnType = new CodeTypeReference("ExpandoObject"),
                Statements =
                {
                    new CodeVariableDeclarationStatement(new CodeTypeReference("dynamic"), "expando")
                    {
                        InitExpression = new CodeObjectCreateExpression("ExpandoObject")
                    },
                    new CodeVariableDeclarationStatement(new CodeTypeReference("var"), "expandoObject")
                    {
                        InitExpression = new CodeCastExpression("IDictionary<string, object>",
                            new CodeFieldReferenceExpression(null, "expando"))
                    },
                    new CodeVariableDeclarationStatement(new CodeTypeReference("var"), "attributes")
                    {
                        InitExpression =
                            new CodeMethodInvokeExpression(new CodePropertyReferenceExpression(null, "Attributes"),
                                "ToArray")
                    },

                    new CodeIterationStatement
                    {
                        TestExpression = new CodeSnippetExpression("i < attributes.Length"),
                        IncrementStatement = new CodeSnippetStatement("i++"),
                        InitStatement =
                            new CodeVariableDeclarationStatement("var", "i", new CodePrimitiveExpression(0)),
                        Statements =
                        {
                            new CodeVariableDeclarationStatement("var", "keyValuePair",
                                new CodeIndexerExpression(new CodeVariableReferenceExpression("attributes"),
                                    new CodeVariableReferenceExpression("i"))),

                            new CodeVariableDeclarationStatement("var", "value",
                                new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("keyValuePair"),
                                    "Value")),
                            new CodeVariableDeclarationStatement("var", "key",
                                new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("keyValuePair"),
                                    "Key")),
                            new CodeConditionStatement(
                                new CodeSnippetExpression("value is EntityReference entityReference"),
                                new CodeStatement[]
                                {
                                    new CodeAssignStatement(new CodeVariableReferenceExpression("key"),
                                        new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("key"),
                                            "ToLower")),
                                    new CodeAssignStatement(new CodeVariableReferenceExpression("value"),
                                        new CodeSnippetExpression(
                                            "$\"/{entityReference.EntitySetName}({entityReference.EntityId})\"")),
                                },
                                new CodeStatement[]
                                {
                                    new CodeAssignStatement(new CodeVariableReferenceExpression("key"),
                                        new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("key"),
                                            "ToLower")),
                                    new CodeConditionStatement(
                                        new CodeSnippetExpression("value is DateTime dateTimeValue"), new CodeVariableDeclarationStatement(new CodeTypeReference("var"),
                                            "propertyForAttribute",
                                            new CodeSnippetExpression(
                                                "GetPublicInstanceProperties().FirstOrDefault(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase))")), new CodeConditionStatement(
                                            new CodeSnippetExpression("propertyForAttribute != null"), new CodeVariableDeclarationStatement(new CodeTypeReference("var"),
                                                "onlyDateAttr",
                                                new CodeMethodInvokeExpression(
                                                    new CodeMethodReferenceExpression(
                                                        new CodeVariableReferenceExpression(
                                                            "propertyForAttribute"), "GetCustomAttribute",
                                                        new CodeTypeReference("OnlyDateAttribute")))), new CodeConditionStatement(
                                                new CodeSnippetExpression("onlyDateAttr != null"), new CodeAssignStatement(
                                                    new CodeVariableReferenceExpression("value"),
                                                    new CodeMethodInvokeExpression(
                                                        new CodeVariableReferenceExpression(
                                                            "dateTimeValue"), "ToString",
                                                        new CodeSnippetExpression(
                                                            "OnlyDateAttribute.Format")))))),
                                }),

                            new CodeExpressionStatement(new CodeMethodInvokeExpression(
                                new CodeVariableReferenceExpression("expandoObject"),
                                "Add",
                                new CodeVariableReferenceExpression("key"),
                                new CodeVariableReferenceExpression("value")
                            ))
                        }
                    },

                    new CodeMethodReturnStatement(new CodeCastExpression("ExpandoObject",
                        new CodeVariableReferenceExpression("expandoObject")))
                }
            };
            return toExpandoMethod;
        }

        /// <summary>
        /// SetAttrValue method
        /// </summary>
        /// <param name="publicAndFinal">The public and final.</param>
        /// <returns></returns>
        private static CodeMemberMethod SetAttrValueMethod(MemberAttributes publicAndFinal)
        {
            var setAttrValueMethod = new CodeMemberMethod
            {
                Name = "SetAttributeValue",
                Attributes = publicAndFinal,
                Parameters =
                {
                    new CodeParameterDeclarationExpression(typeof(string), "attributeName"),
                    new CodeParameterDeclarationExpression(typeof(object), "value"),
                },
                Statements =
                {
                    new CodeConditionStatement(new CodeSnippetExpression("Attributes.ContainsKey(attributeName)"),
                        new CodeStatement[]
                        {
                            new CodeAssignStatement(new CodeSnippetExpression("Attributes[attributeName]"),
                                new CodeArgumentReferenceExpression("value"))
                        },
                        new CodeStatement[]
                        {
                            new CodeExpressionStatement(new CodeMethodInvokeExpression(
                                new CodeMethodReferenceExpression(null, "Attributes"), "Add", new CodeArgumentReferenceExpression("attributeName"), new CodeArgumentReferenceExpression("value")))
                        }),
                }
            };
            return setAttrValueMethod;
        }

        /// <summary>
        ///  GetAttrValue method
        /// </summary>
        /// <param name="publicAndFinal">The public and final.</param>
        /// <returns></returns>
        private static CodeMemberMethod GetAttrValueMethod(MemberAttributes publicAndFinal)
        {
            var getAttrValueMethod = new CodeMemberMethod()
            {
                Name = "GetAttributeValue",
                Attributes = publicAndFinal,
                TypeParameters =
                {
                    new CodeTypeParameter("T")
                },
                ReturnType = new CodeTypeReference()
                {
                    Options = CodeTypeReferenceOptions.GenericTypeParameter,
                    BaseType = "T"
                },
                Parameters =
                {
                    new CodeParameterDeclarationExpression(typeof(string), "attributeName"),
                },

                Statements =
                {
                    new CodeVariableDeclarationStatement("var", "keyValuePair")
                    {
                        InitExpression = new CodeSnippetExpression(
                            "Attributes.FirstOrDefault(x => x.Key.Equals(attributeName, StringComparison.InvariantCultureIgnoreCase))")
                    },
                    new CodeMethodReturnStatement(
                        new CodeSnippetExpression("keyValuePair.Value != null ? (T)keyValuePair.Value : default(T)"))
                }
            };
            return getAttrValueMethod;
        }

        /// <summary>
        /// Gets the constructor.
        /// </summary>
        /// <returns></returns>
        private static CodeConstructor GetConstructor()
        {
            var expandoConstructor = new CodeConstructor()
            {
                Attributes = MemberAttributes.Public,
                Parameters =
                {
                    new CodeParameterDeclarationExpression(typeof(ExpandoObject), "expandoObject")
                },
                Statements =
                {
                    new CodeVariableDeclarationStatement("var", "formattedAttributePostFix")
                    {
                        InitExpression = new CodePrimitiveExpression("@OData.Community.Display.V1.FormattedValue")
                    },
                    new CodeVariableDeclarationStatement(new CodeTypeReference("var"), "attributes")
                    {
                        InitExpression =
                            new CodeMethodInvokeExpression(new CodePropertyReferenceExpression(null, "expandoObject"),
                                "ToArray")
                    },

                    new CodeIterationStatement
                    {
                        TestExpression = new CodeSnippetExpression("i < attributes.Length"),
                        IncrementStatement = new CodeSnippetStatement("i++"),
                        InitStatement =
                            new CodeVariableDeclarationStatement("var", "i", new CodePrimitiveExpression(0)),
                        Statements =
                        {
                            new CodeVariableDeclarationStatement("var", "keyValuePair",
                                new CodeIndexerExpression(new CodeVariableReferenceExpression("attributes"),
                                    new CodeVariableReferenceExpression("i"))),
                            new CodeVariableDeclarationStatement("var", "value",
                                new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("keyValuePair"),
                                    "Value")),
                            new CodeVariableDeclarationStatement("var", "valueFieldName",
                                new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("keyValuePair"),
                                    "Key")),
                            new CodeVariableDeclarationStatement("Type", "convertValueToType",
                                new CodePrimitiveExpression(null)),

                            new CodeConditionStatement(new CodeSnippetExpression("value != null"), new CodeConditionStatement(new CodeSnippetExpression("value is string stringValue && " +
                                                                                                                                                        "Guid.TryParse(stringValue, out var id)"),
                                new CodeStatement[]
                                {
                                    new CodeAssignStatement(new CodeVariableReferenceExpression("value"),
                                        new CodeVariableReferenceExpression("id")),
                                    new CodeVariableDeclarationStatement("var", "propertyWithEntityRefAttribute")
                                    {
                                        InitExpression = new CodeSnippetExpression(
                                            "GetPublicInstanceProperties().FirstOrDefault(x => x.GetCustomAttribute<EntityReferenceAttribute>()?.ValueField?.Equals(valueFieldName, StringComparison.CurrentCultureIgnoreCase) == true)")
                                    },
                                    new CodeConditionStatement(
                                        new CodeSnippetExpression("propertyWithEntityRefAttribute != null"), new CodeVariableDeclarationStatement("var", "entityReferenceAttribute",
                                            new CodeSnippetExpression(
                                                "propertyWithEntityRefAttribute.GetCustomAttribute<EntityReferenceAttribute>()")), new CodeExpressionStatement(new CodeMethodInvokeExpression(
                                            new CodeVariableReferenceExpression(
                                                "propertyWithEntityRefAttribute"), "SetValue",
                                            new CodeThisReferenceExpression(),
                                            new CodeObjectCreateExpression("EntityReference", new CodePropertyReferenceExpression(
                                                new CodeVariableReferenceExpression(
                                                    "entityReferenceAttribute"), "EntitySetName"), new CodeVariableReferenceExpression("id"))))),
                                },
                                new CodeStatement[]
                                {
                                    new CodeConditionStatement(
                                        new CodeSnippetExpression("value is ExpandoObject valueExpandoObject"),
                                        new CodeStatement[]
                                        {
                                            new CodeVariableDeclarationStatement("var", "propertyWithEntityAttribute",
                                                new CodeSnippetExpression(
                                                    "GetPublicInstanceProperties().FirstOrDefault(x => x.GetCustomAttribute<EntityAttribute>()?.AttributeName?.Equals(valueFieldName, StringComparison.CurrentCultureIgnoreCase) == true)")),
                                            new CodeConditionStatement(
                                                new CodeSnippetExpression("propertyWithEntityAttribute != null"), new CodeAssignStatement(new CodeVariableReferenceExpression("value"),
                                                    new CodeSnippetExpression(
                                                        "Activator.CreateInstance(propertyWithEntityAttribute.PropertyType, valueExpandoObject)"))),
                                        },
                                        new CodeStatement[]
                                        {
                                            new CodeVariableDeclarationStatement("var", "propertyForField",
                                                new CodeSnippetExpression(
                                                    "GetPublicInstanceProperties().FirstOrDefault(x => x.Name.Equals(valueFieldName, StringComparison.CurrentCultureIgnoreCase) == true)")),
                                            new CodeConditionStatement(
                                                new CodeSnippetExpression("propertyForField != null"), new CodeAssignStatement(
                                                    new CodeVariableReferenceExpression("convertValueToType"),
                                                    new CodePropertyReferenceExpression(
                                                        new CodeVariableReferenceExpression("propertyForField"),
                                                        "PropertyType"))),
                                        }
                                    ),
                                }), new CodeConditionStatement(new CodeSnippetExpression("convertValueToType != null"), new CodeConditionStatement(
                                    new CodeSnippetExpression(
                                        "convertValueToType.IsGenericType && " +
                                        "convertValueToType.GetGenericTypeDefinition() == typeof(Nullable<>)"), new CodeAssignStatement(
                                        new CodeVariableReferenceExpression("convertValueToType"),
                                        new CodeSnippetExpression(
                                            "Nullable.GetUnderlyingType(convertValueToType)"))), new CodeAssignStatement(new CodeVariableReferenceExpression("value"),
                                    new CodeSnippetExpression("Convert.ChangeType(value, convertValueToType)"))), new CodeConditionStatement(new CodeMethodInvokeExpression(
                                    new CodeVariableReferenceExpression("valueFieldName"),
                                    "EndsWith",
                                    new CodeVariableReferenceExpression("formattedAttributePostFix")),
                                new CodeStatement[]
                                {
                                    new CodeExpressionStatement(new CodeMethodInvokeExpression(
                                        new CodeVariableReferenceExpression("FormattedValues"), "Add", new CodeMethodInvokeExpression(
                                            new CodeVariableReferenceExpression("valueFieldName"), "Replace", new CodeVariableReferenceExpression("formattedAttributePostFix"), new CodeSnippetExpression("string.Empty")), new CodeSnippetExpression("value?.ToString()"))),
                                },
                                new CodeStatement[]
                                {
                                    new CodeExpressionStatement(new CodeMethodInvokeExpression(
                                        new CodeVariableReferenceExpression("Attributes"),
                                        "Add", new CodeVariableReferenceExpression("valueFieldName"), new CodeVariableReferenceExpression("value"))),
                                }))
                        }
                    }
                }
            };
            return expandoConstructor;
        }

        /// <summary>
        /// Creates the automatic property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private CodeTypeMember[] CreateAutoProperty(string propertyName, string fieldName, Type type)
        {
            var entityLogicalNameField = new CodeMemberField()
            {
                Attributes = MemberAttributes.Private,
                Name = fieldName,
                Type = new CodeTypeReference(type),
            };

            var entityLogicalNameProp = new CodeMemberProperty()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = propertyName,
                Type = new CodeTypeReference(type),
                HasGet = true,
                HasSet = true,
                GetStatements =
                {
                    new CodeMethodReturnStatement(new CodeVariableReferenceExpression(entityLogicalNameField.Name))
                },
                SetStatements =
                {
                    new CodeAssignStatement(new CodeVariableReferenceExpression(entityLogicalNameField.Name), new CodeVariableReferenceExpression("value"))
                }
            };

            return new CodeTypeMember[]
            {
                entityLogicalNameField,
                entityLogicalNameProp
            };
        }
    }
}
