namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes
{
    public abstract class CustomAttribute
    {
        public abstract string Type { get; }

        public abstract string[] GetArguments();
    }
}
