namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes
{
    public class DateOnlyAttributeModel : CustomAttribute
    {
        public override string Type => "OnlyDate";
        public override string[] GetArguments()
        {
            return new string[0];
        }
    }
}
