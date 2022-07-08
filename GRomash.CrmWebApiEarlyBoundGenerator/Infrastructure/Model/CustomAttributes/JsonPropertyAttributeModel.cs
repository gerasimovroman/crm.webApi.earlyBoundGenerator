namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model.CustomAttributes
{
    internal class JsonPropertyAttributeModel : CustomAttribute
    {
        private readonly string name;

        internal JsonPropertyAttributeModel(string name)
        {
            this.name = name;
        }
        public override string Type => "Newtonsoft.Json.JsonProperty";

        public override string[] GetArguments()
        {
            return new string[] { name };
        }
    }
}
