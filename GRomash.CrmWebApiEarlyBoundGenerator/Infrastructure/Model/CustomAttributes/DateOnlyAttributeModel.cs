namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes
{
    public class DateOnlyAttributeModel : CustomAttribute
    {
        public override string Type => "DateOnly";
        public override string[] GetArguments()
        {
            return new string[0];
        }
    }
}
