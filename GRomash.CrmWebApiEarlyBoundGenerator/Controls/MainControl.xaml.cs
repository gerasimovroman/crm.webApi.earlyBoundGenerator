using System;
using System.Windows.Controls;
using GRomash.CrmWebApiEarlyBoundGenerator.Properties;
using GRomash.CrmWebApiEarlyBoundGenerator.ViewModels;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Controls
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainControl"/> class.
        /// </summary>
        /// <param name="organizationService">The organization service.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="connectionDetailOriginalUrl">The connection detail original URL.</param>
        /// <exception cref="ArgumentNullException">
        /// organizationService
        /// or
        /// settings
        /// </exception>
        public MainControl([NotNull] IOrganizationService organizationService, [NotNull] Settings settings,
            ConnectionDetail connectionDetailOriginalUrl)
        {
            if (organizationService == null) throw new ArgumentNullException(nameof(organizationService));
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            InitializeComponent();
            DataContext = new MainControlViewModel(organizationService, settings, connectionDetailOriginalUrl);
        }
    }
}
