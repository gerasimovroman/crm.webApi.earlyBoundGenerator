namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes
{
    public class EntityReferenceAttributeModel : CustomAttribute
    {
        public string EntitySetName { get; set; }

        public string ValueField { get; set; }
    }
}
