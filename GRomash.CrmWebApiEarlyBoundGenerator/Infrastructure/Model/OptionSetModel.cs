namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model
{
    public class OptionSetModel
    {
        public string OptionSetName { get; set; }

        public string Namespace { get; set; }

        public string Description { get; set; }

        public OptionSetValueModel[] Values { get; set; }
    }
}
