using System;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Repository
{
    /// <summary>
    /// Get crm version of crm
    /// </summary>
    public class CrmVersionRepository
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly IOrganizationService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrmVersionRepository"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CrmVersionRepository(IOrganizationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <returns></returns>
        public Version GetVersion()
        {
            var request = new RetrieveVersionRequest();
            var response = (RetrieveVersionResponse) _service.Execute(request);
            var version = new Version(response.Version);
            return version;
        }

        /// <summary>
        /// Gets the supported version.
        /// </summary>
        /// <value>
        /// The supported version.
        /// </value>
        public static Version SupportedVersion => new Version("8.0");
    }
}
