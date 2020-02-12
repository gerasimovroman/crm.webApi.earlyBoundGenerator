using System;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model
{
    public class EntityModel
    {
        public string LogicalName { get; set; }

        public string DisplayName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is EntityModel entityModel &&
                !string.IsNullOrWhiteSpace(entityModel.LogicalName))
            {
                return LogicalName?.Equals(entityModel.LogicalName, StringComparison.CurrentCultureIgnoreCase) == true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return LogicalName == null ? base.GetHashCode() : LogicalName.GetHashCode();
        }
    }
}
