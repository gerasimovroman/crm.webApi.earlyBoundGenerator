namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes
{
    public class EntityReferenceAttributeModel : CustomAttribute
    {
        public string EntitySetName { get; set; }

        public string ValueField { get; set; }

        public override string Type => "EntityReference";
        public override string[] GetArguments()
        {
            return new string[]
            {
                EntitySetName,
                ValueField
            };
        }
    }
}
