using System;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model
{
    public class MedadataTemplate
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edmx")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://docs.oasis-open.org/odata/ns/edmx", IsNullable = false)]
        public partial class Edmx
        {

            private EdmxReference[] referenceField;

            private EdmxDataServices dataServicesField;

            private decimal versionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Reference")]
            public EdmxReference[] Reference
            {
                get
                {
                    return this.referenceField;
                }
                set
                {
                    this.referenceField = value;
                }
            }

            /// <remarks/>
            public EdmxDataServices DataServices
            {
                get
                {
                    return this.dataServicesField;
                }
                set
                {
                    this.dataServicesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edmx")]
        public partial class EdmxReference
        {

            private EdmxReferenceInclude includeField;

            private EdmxReferenceIncludeAnnotations includeAnnotationsField;

            private string uriField;

            /// <remarks/>
            public EdmxReferenceInclude Include
            {
                get
                {
                    return this.includeField;
                }
                set
                {
                    this.includeField = value;
                }
            }

            /// <remarks/>
            public EdmxReferenceIncludeAnnotations IncludeAnnotations
            {
                get
                {
                    return this.includeAnnotationsField;
                }
                set
                {
                    this.includeAnnotationsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Uri
            {
                get
                {
                    return this.uriField;
                }
                set
                {
                    this.uriField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edmx")]
        public partial class EdmxReferenceInclude
        {

            private string namespaceField;

            private string aliasField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Alias
            {
                get
                {
                    return this.aliasField;
                }
                set
                {
                    this.aliasField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edmx")]
        public partial class EdmxReferenceIncludeAnnotations
        {

            private string termNamespaceField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string TermNamespace
            {
                get
                {
                    return this.termNamespaceField;
                }
                set
                {
                    this.termNamespaceField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edmx")]
        public partial class EdmxDataServices
        {

            private Schema schemaField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
            public Schema Schema
            {
                get
                {
                    return this.schemaField;
                }
                set
                {
                    this.schemaField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://docs.oasis-open.org/odata/ns/edm", IsNullable = false)]
        public partial class Schema
        {

            private object[] itemsField;

            private string namespaceField;

            private string aliasField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Action", typeof(SchemaAction))]
            [System.Xml.Serialization.XmlElementAttribute("ComplexType", typeof(SchemaComplexType))]
            [System.Xml.Serialization.XmlElementAttribute("EntityContainer", typeof(SchemaEntityContainer))]
            [System.Xml.Serialization.XmlElementAttribute("EntityType", typeof(SchemaEntityType))]
            [System.Xml.Serialization.XmlElementAttribute("EnumType", typeof(SchemaEnumType))]
            [System.Xml.Serialization.XmlElementAttribute("Function", typeof(SchemaFunction))]
            [System.Xml.Serialization.XmlElementAttribute("Term", typeof(SchemaTerm))]
            public object[] Items
            {
                get
                {
                    return this.itemsField;
                }
                set
                {
                    this.itemsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Namespace
            {
                get
                {
                    return this.namespaceField;
                }
                set
                {
                    this.namespaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Alias
            {
                get
                {
                    return this.aliasField;
                }
                set
                {
                    this.aliasField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaAction
        {

            private SchemaActionParameter[] parameterField;

            private SchemaActionReturnType returnTypeField;

            private string nameField;

            private bool isBoundField;

            private bool isBoundFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Parameter")]
            public SchemaActionParameter[] Parameter
            {
                get
                {
                    return this.parameterField;
                }
                set
                {
                    this.parameterField = value;
                }
            }

            /// <remarks/>
            public SchemaActionReturnType ReturnType
            {
                get
                {
                    return this.returnTypeField;
                }
                set
                {
                    this.returnTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool IsBound
            {
                get
                {
                    return this.isBoundField;
                }
                set
                {
                    this.isBoundField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool IsBoundSpecified
            {
                get
                {
                    return this.isBoundFieldSpecified;
                }
                set
                {
                    this.isBoundFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaActionParameter
        {

            private string nameField;

            private string typeField;

            private bool nullableField;

            private bool nullableFieldSpecified;

            private bool unicodeField;

            private bool unicodeFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Nullable
            {
                get
                {
                    return this.nullableField;
                }
                set
                {
                    this.nullableField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool NullableSpecified
            {
                get
                {
                    return this.nullableFieldSpecified;
                }
                set
                {
                    this.nullableFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Unicode
            {
                get
                {
                    return this.unicodeField;
                }
                set
                {
                    this.unicodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool UnicodeSpecified
            {
                get
                {
                    return this.unicodeFieldSpecified;
                }
                set
                {
                    this.unicodeFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaActionReturnType
        {

            private string typeField;

            private bool nullableField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Nullable
            {
                get
                {
                    return this.nullableField;
                }
                set
                {
                    this.nullableField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaComplexType
        {

            private SchemaComplexTypeProperty[] propertyField;

            private string nameField;

            private string baseTypeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Property")]
            public SchemaComplexTypeProperty[] Property
            {
                get
                {
                    return this.propertyField;
                }
                set
                {
                    this.propertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string BaseType
            {
                get
                {
                    return this.baseTypeField;
                }
                set
                {
                    this.baseTypeField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaComplexTypeProperty
        {

            private string nameField;

            private string typeField;

            private bool nullableField;

            private bool nullableFieldSpecified;

            private string scaleField;

            private bool unicodeField;

            private bool unicodeFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Nullable
            {
                get
                {
                    return this.nullableField;
                }
                set
                {
                    this.nullableField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool NullableSpecified
            {
                get
                {
                    return this.nullableFieldSpecified;
                }
                set
                {
                    this.nullableFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Scale
            {
                get
                {
                    return this.scaleField;
                }
                set
                {
                    this.scaleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Unicode
            {
                get
                {
                    return this.unicodeField;
                }
                set
                {
                    this.unicodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool UnicodeSpecified
            {
                get
                {
                    return this.unicodeFieldSpecified;
                }
                set
                {
                    this.unicodeFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityContainer
        {

            private object[] itemsField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("ActionImport", typeof(SchemaEntityContainerActionImport))]
            [System.Xml.Serialization.XmlElementAttribute("Annotation", typeof(SchemaEntityContainerAnnotation))]
            [System.Xml.Serialization.XmlElementAttribute("EntitySet", typeof(SchemaEntityContainerEntitySet))]
            [System.Xml.Serialization.XmlElementAttribute("FunctionImport", typeof(SchemaEntityContainerFunctionImport))]
            public object[] Items
            {
                get
                {
                    return this.itemsField;
                }
                set
                {
                    this.itemsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityContainerActionImport
        {

            private string nameField;

            private string actionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Action
            {
                get
                {
                    return this.actionField;
                }
                set
                {
                    this.actionField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityContainerAnnotation
        {

            private string[] collectionField;

            private string enumMemberField;

            private string termField;

            private bool boolField;

            private bool boolFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("String", IsNullable = false)]
            public string[] Collection
            {
                get
                {
                    return this.collectionField;
                }
                set
                {
                    this.collectionField = value;
                }
            }

            /// <remarks/>
            public string EnumMember
            {
                get
                {
                    return this.enumMemberField;
                }
                set
                {
                    this.enumMemberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Term
            {
                get
                {
                    return this.termField;
                }
                set
                {
                    this.termField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Bool
            {
                get
                {
                    return this.boolField;
                }
                set
                {
                    this.boolField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool BoolSpecified
            {
                get
                {
                    return this.boolFieldSpecified;
                }
                set
                {
                    this.boolFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityContainerEntitySet
        {

            private SchemaEntityContainerEntitySetNavigationPropertyBinding[] navigationPropertyBindingField;

            private SchemaEntityContainerEntitySetAnnotation[] annotationField;

            private string nameField;

            private string entityTypeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("NavigationPropertyBinding")]
            public SchemaEntityContainerEntitySetNavigationPropertyBinding[] NavigationPropertyBinding
            {
                get
                {
                    return this.navigationPropertyBindingField;
                }
                set
                {
                    this.navigationPropertyBindingField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Annotation")]
            public SchemaEntityContainerEntitySetAnnotation[] Annotation
            {
                get
                {
                    return this.annotationField;
                }
                set
                {
                    this.annotationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string EntityType
            {
                get
                {
                    return this.entityTypeField;
                }
                set
                {
                    this.entityTypeField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityContainerEntitySetNavigationPropertyBinding
        {

            private string pathField;

            private string targetField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Path
            {
                get
                {
                    return this.pathField;
                }
                set
                {
                    this.pathField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Target
            {
                get
                {
                    return this.targetField;
                }
                set
                {
                    this.targetField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityContainerEntitySetAnnotation
        {

            private SchemaEntityContainerEntitySetAnnotationRecord recordField;

            private string termField;

            private bool boolField;

            private bool boolFieldSpecified;

            /// <remarks/>
            public SchemaEntityContainerEntitySetAnnotationRecord Record
            {
                get
                {
                    return this.recordField;
                }
                set
                {
                    this.recordField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Term
            {
                get
                {
                    return this.termField;
                }
                set
                {
                    this.termField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Bool
            {
                get
                {
                    return this.boolField;
                }
                set
                {
                    this.boolField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool BoolSpecified
            {
                get
                {
                    return this.boolFieldSpecified;
                }
                set
                {
                    this.boolFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityContainerEntitySetAnnotationRecord
        {

            private SchemaEntityContainerEntitySetAnnotationRecordPropertyValue propertyValueField;

            /// <remarks/>
            public SchemaEntityContainerEntitySetAnnotationRecordPropertyValue PropertyValue
            {
                get
                {
                    return this.propertyValueField;
                }
                set
                {
                    this.propertyValueField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityContainerEntitySetAnnotationRecordPropertyValue
        {

            private string enumMemberField;

            private string propertyField;

            private bool boolField;

            private bool boolFieldSpecified;

            /// <remarks/>
            public string EnumMember
            {
                get
                {
                    return this.enumMemberField;
                }
                set
                {
                    this.enumMemberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Property
            {
                get
                {
                    return this.propertyField;
                }
                set
                {
                    this.propertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Bool
            {
                get
                {
                    return this.boolField;
                }
                set
                {
                    this.boolField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool BoolSpecified
            {
                get
                {
                    return this.boolFieldSpecified;
                }
                set
                {
                    this.boolFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityContainerFunctionImport
        {

            private string nameField;

            private string functionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Function
            {
                get
                {
                    return this.functionField;
                }
                set
                {
                    this.functionField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityType
        {

            private SchemaEntityTypeKey keyField;

            private SchemaEntityTypeProperty[] propertyField;

            private SchemaEntityTypeNavigationProperty[] navigationPropertyField;

            private SchemaEntityTypeAnnotation[] annotationField;

            private string nameField;

            private bool abstractField;

            private bool abstractFieldSpecified;

            private string baseTypeField;

            /// <remarks/>
            public SchemaEntityTypeKey Key
            {
                get
                {
                    return this.keyField;
                }
                set
                {
                    this.keyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Property")]
            public SchemaEntityTypeProperty[] Property
            {
                get
                {
                    return this.propertyField;
                }
                set
                {
                    this.propertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("NavigationProperty")]
            public SchemaEntityTypeNavigationProperty[] NavigationProperty
            {
                get
                {
                    return this.navigationPropertyField;
                }
                set
                {
                    this.navigationPropertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Annotation")]
            public SchemaEntityTypeAnnotation[] Annotation
            {
                get
                {
                    return this.annotationField;
                }
                set
                {
                    this.annotationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Abstract
            {
                get
                {
                    return this.abstractField;
                }
                set
                {
                    this.abstractField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool AbstractSpecified
            {
                get
                {
                    return this.abstractFieldSpecified;
                }
                set
                {
                    this.abstractFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string BaseType
            {
                get
                {
                    return this.baseTypeField;
                }
                set
                {
                    this.baseTypeField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypeKey
        {

            private SchemaEntityTypeKeyPropertyRef propertyRefField;

            /// <remarks/>
            public SchemaEntityTypeKeyPropertyRef PropertyRef
            {
                get
                {
                    return this.propertyRefField;
                }
                set
                {
                    this.propertyRefField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypeKeyPropertyRef
        {

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypeProperty
        {

            private SchemaEntityTypePropertyAnnotation[] annotationField;

            private string nameField;

            private string typeField;

            private string concurrencyModeField;

            private string scaleField;

            private bool unicodeField;

            private bool unicodeFieldSpecified;

            private bool nullableField;

            private bool nullableFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Annotation")]
            public SchemaEntityTypePropertyAnnotation[] Annotation
            {
                get
                {
                    return this.annotationField;
                }
                set
                {
                    this.annotationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ConcurrencyMode
            {
                get
                {
                    return this.concurrencyModeField;
                }
                set
                {
                    this.concurrencyModeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Scale
            {
                get
                {
                    return this.scaleField;
                }
                set
                {
                    this.scaleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Unicode
            {
                get
                {
                    return this.unicodeField;
                }
                set
                {
                    this.unicodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool UnicodeSpecified
            {
                get
                {
                    return this.unicodeFieldSpecified;
                }
                set
                {
                    this.unicodeFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Nullable
            {
                get
                {
                    return this.nullableField;
                }
                set
                {
                    this.nullableField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool NullableSpecified
            {
                get
                {
                    return this.nullableFieldSpecified;
                }
                set
                {
                    this.nullableFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypePropertyAnnotation
        {

            private string enumMemberField;

            private string termField;

            private string stringField;

            private bool boolField;

            private bool boolFieldSpecified;

            /// <remarks/>
            public string EnumMember
            {
                get
                {
                    return this.enumMemberField;
                }
                set
                {
                    this.enumMemberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Term
            {
                get
                {
                    return this.termField;
                }
                set
                {
                    this.termField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string String
            {
                get
                {
                    return this.stringField;
                }
                set
                {
                    this.stringField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Bool
            {
                get
                {
                    return this.boolField;
                }
                set
                {
                    this.boolField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool BoolSpecified
            {
                get
                {
                    return this.boolFieldSpecified;
                }
                set
                {
                    this.boolFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypeNavigationProperty
        {

            private SchemaEntityTypeNavigationPropertyReferentialConstraint referentialConstraintField;

            private string nameField;

            private string typeField;

            private string partnerField;

            private bool nullableField;

            private bool nullableFieldSpecified;

            private bool containsTargetField;

            private bool containsTargetFieldSpecified;

            /// <remarks/>
            public SchemaEntityTypeNavigationPropertyReferentialConstraint ReferentialConstraint
            {
                get
                {
                    return this.referentialConstraintField;
                }
                set
                {
                    this.referentialConstraintField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Partner
            {
                get
                {
                    return this.partnerField;
                }
                set
                {
                    this.partnerField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Nullable
            {
                get
                {
                    return this.nullableField;
                }
                set
                {
                    this.nullableField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool NullableSpecified
            {
                get
                {
                    return this.nullableFieldSpecified;
                }
                set
                {
                    this.nullableFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool ContainsTarget
            {
                get
                {
                    return this.containsTargetField;
                }
                set
                {
                    this.containsTargetField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool ContainsTargetSpecified
            {
                get
                {
                    return this.containsTargetFieldSpecified;
                }
                set
                {
                    this.containsTargetFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypeNavigationPropertyReferentialConstraint
        {

            private string propertyField;

            private string referencedPropertyField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Property
            {
                get
                {
                    return this.propertyField;
                }
                set
                {
                    this.propertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ReferencedProperty
            {
                get
                {
                    return this.referencedPropertyField;
                }
                set
                {
                    this.referencedPropertyField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypeAnnotation
        {

            private SchemaEntityTypeAnnotationRecord[] collectionField;

            private string termField;

            private string stringField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Record", IsNullable = false)]
            public SchemaEntityTypeAnnotationRecord[] Collection
            {
                get
                {
                    return this.collectionField;
                }
                set
                {
                    this.collectionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Term
            {
                get
                {
                    return this.termField;
                }
                set
                {
                    this.termField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string String
            {
                get
                {
                    return this.stringField;
                }
                set
                {
                    this.stringField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypeAnnotationRecord
        {

            private SchemaEntityTypeAnnotationRecordPropertyValue propertyValueField;

            private string typeField;

            /// <remarks/>
            public SchemaEntityTypeAnnotationRecordPropertyValue PropertyValue
            {
                get
                {
                    return this.propertyValueField;
                }
                set
                {
                    this.propertyValueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypeAnnotationRecordPropertyValue
        {

            private SchemaEntityTypeAnnotationRecordPropertyValueCollection collectionField;

            private string propertyField;

            /// <remarks/>
            public SchemaEntityTypeAnnotationRecordPropertyValueCollection Collection
            {
                get
                {
                    return this.collectionField;
                }
                set
                {
                    this.collectionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Property
            {
                get
                {
                    return this.propertyField;
                }
                set
                {
                    this.propertyField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypeAnnotationRecordPropertyValueCollection
        {

            private SchemaEntityTypeAnnotationRecordPropertyValueCollectionRecord recordField;

            /// <remarks/>
            public SchemaEntityTypeAnnotationRecordPropertyValueCollectionRecord Record
            {
                get
                {
                    return this.recordField;
                }
                set
                {
                    this.recordField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypeAnnotationRecordPropertyValueCollectionRecord
        {

            private SchemaEntityTypeAnnotationRecordPropertyValueCollectionRecordPropertyValue[] propertyValueField;

            private string typeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("PropertyValue")]
            public SchemaEntityTypeAnnotationRecordPropertyValueCollectionRecordPropertyValue[] PropertyValue
            {
                get
                {
                    return this.propertyValueField;
                }
                set
                {
                    this.propertyValueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEntityTypeAnnotationRecordPropertyValueCollectionRecordPropertyValue
        {

            private string propertyField;

            private string stringField;

            private string propertyPathField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Property
            {
                get
                {
                    return this.propertyField;
                }
                set
                {
                    this.propertyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string String
            {
                get
                {
                    return this.stringField;
                }
                set
                {
                    this.stringField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string PropertyPath
            {
                get
                {
                    return this.propertyPathField;
                }
                set
                {
                    this.propertyPathField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEnumType
        {

            private SchemaEnumTypeMember[] memberField;

            private string nameField;

            private bool isFlagsField;

            private bool isFlagsFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Member")]
            public SchemaEnumTypeMember[] Member
            {
                get
                {
                    return this.memberField;
                }
                set
                {
                    this.memberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool IsFlags
            {
                get
                {
                    return this.isFlagsField;
                }
                set
                {
                    this.isFlagsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool IsFlagsSpecified
            {
                get
                {
                    return this.isFlagsFieldSpecified;
                }
                set
                {
                    this.isFlagsFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaEnumTypeMember
        {

            private string nameField;

            private uint valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public uint Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaFunction
        {

            private SchemaFunctionParameter[] parameterField;

            private SchemaFunctionReturnType returnTypeField;

            private string nameField;

            private bool isBoundField;

            private bool isBoundFieldSpecified;

            private bool isComposableField;

            private bool isComposableFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Parameter")]
            public SchemaFunctionParameter[] Parameter
            {
                get
                {
                    return this.parameterField;
                }
                set
                {
                    this.parameterField = value;
                }
            }

            /// <remarks/>
            public SchemaFunctionReturnType ReturnType
            {
                get
                {
                    return this.returnTypeField;
                }
                set
                {
                    this.returnTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool IsBound
            {
                get
                {
                    return this.isBoundField;
                }
                set
                {
                    this.isBoundField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool IsBoundSpecified
            {
                get
                {
                    return this.isBoundFieldSpecified;
                }
                set
                {
                    this.isBoundFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool IsComposable
            {
                get
                {
                    return this.isComposableField;
                }
                set
                {
                    this.isComposableField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool IsComposableSpecified
            {
                get
                {
                    return this.isComposableFieldSpecified;
                }
                set
                {
                    this.isComposableFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaFunctionParameter
        {

            private string nameField;

            private string typeField;

            private bool nullableField;

            private bool nullableFieldSpecified;

            private bool unicodeField;

            private bool unicodeFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Nullable
            {
                get
                {
                    return this.nullableField;
                }
                set
                {
                    this.nullableField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool NullableSpecified
            {
                get
                {
                    return this.nullableFieldSpecified;
                }
                set
                {
                    this.nullableFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Unicode
            {
                get
                {
                    return this.unicodeField;
                }
                set
                {
                    this.unicodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool UnicodeSpecified
            {
                get
                {
                    return this.unicodeFieldSpecified;
                }
                set
                {
                    this.unicodeFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaFunctionReturnType
        {

            private string typeField;

            private bool nullableField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Nullable
            {
                get
                {
                    return this.nullableField;
                }
                set
                {
                    this.nullableField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaTerm
        {

            private SchemaTermAnnotation annotationField;

            private string nameField;

            private string typeField;

            private string appliesToField;

            /// <remarks/>
            public SchemaTermAnnotation Annotation
            {
                get
                {
                    return this.annotationField;
                }
                set
                {
                    this.annotationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string AppliesTo
            {
                get
                {
                    return this.appliesToField;
                }
                set
                {
                    this.appliesToField = value;
                }
            }
        }

        /// <remarks/>
        [Serializable()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://docs.oasis-open.org/odata/ns/edm")]
        public partial class SchemaTermAnnotation
        {

            private string termField;

            private string stringField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Term
            {
                get
                {
                    return this.termField;
                }
                set
                {
                    this.termField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string String
            {
                get
                {
                    return this.stringField;
                }
                set
                {
                    this.stringField = value;
                }
            }
        }


    }
}
