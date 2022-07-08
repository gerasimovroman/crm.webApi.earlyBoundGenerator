namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model
{
    public class ClassModel
    {
        public string Namespace { get; set; }

        public string EntityName { get; set; }

        public string EntitySetName { get; set; }

        public string EntityLogicalName { get; set; }

        public string PrimaryIdAttribute { get; set; }

        public PropertyModel[] Properties { get; set; }

        public FieldModel[] Fields { get; set; }

        public FieldModel[] PropertiesFields { get; set; }

        public FieldModel[] Schemas { get; set; }

        public bool GeneratePartialClasses { get; set; }

        public bool IncludeJsonAttribute { get; set; }
    }
}
