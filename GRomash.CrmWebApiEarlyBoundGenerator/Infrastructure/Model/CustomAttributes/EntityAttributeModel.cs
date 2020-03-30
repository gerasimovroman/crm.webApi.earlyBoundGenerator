namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes
{
    public class EntityAttributeModel : CustomAttribute
    {
        public string EntityLogicalName { get; set; }

        public string AttributeName { get; set; }
        public override string Type => "Entity";
        public override string[] GetArguments()
        {
            return new string[]
            {
                EntityLogicalName,
                AttributeName
            };
        }
    }
}
