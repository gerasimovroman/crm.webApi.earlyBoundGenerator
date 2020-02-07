using System.Collections.Generic;
using System.Linq;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;

namespace GRomash.CrmWebApiEarlyBoundGenerator
{
    /// <summary>
    /// This class can help you to store settings for your plugin
    /// </summary>
    /// <remarks>
    /// This class must be XML serializable
    /// </remarks>
    public class Settings
    {
        /// <summary>
        /// Gets or sets the last used organization webapp URL.
        /// </summary>
        /// <value>
        /// The last used organization webapp URL.
        /// </value>
        public string LastUsedOrganizationWebappUrl { get; set; }

        /// <summary>
        /// Gets or sets the settings path pairs.
        /// </summary>
        /// <value>
        /// The settings path pairs.
        /// </value>
        public List<SettingsPathPair> SettingsPathPairs { get; set; } = new List<SettingsPathPair>();


        /// <summary>
        /// Gets the current settings path.
        /// </summary>
        /// <value>
        /// The current settings path.
        /// </value>
        internal string CurrentSettingsPath => SettingsPathPairs
            .FirstOrDefault(x => x.ConnectionString == LastUsedOrganizationWebappUrl)
            ?.SettingsPath;
    }
}