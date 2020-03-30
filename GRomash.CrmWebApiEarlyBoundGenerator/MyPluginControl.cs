using System;
using GRomash.CrmWebApiEarlyBoundGenerator.Controls;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using MessageBox = System.Windows.MessageBox;

namespace GRomash.CrmWebApiEarlyBoundGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="XrmToolBox.Extensibility.PluginControlBase" />
    public partial class MyPluginControl : PluginControlBase
    {
        /// <summary>
        /// My settings
        /// </summary>
        private Settings _mySettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyPluginControl"/> class.
        /// </summary>
        public MyPluginControl()
        {
            InitializeComponent();
            SizeChanged += MyPluginControl_SizeChanged;
            OnCloseTool += MyPluginControl_OnCloseTool;
            Host.Size = Size;
        }

        /// <summary>
        /// Handles the SizeChanged event of the MyPluginControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MyPluginControl_SizeChanged(object sender, EventArgs e)
        {
            Host.Size = Size;
        }

        /// <summary>
        /// Handles the Load event of the MyPluginControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out _mySettings))
            {
                _mySettings = new Settings();
                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }

            _mySettings.LastUsedOrganizationWebappUrl = ConnectionDetail.OrganizationServiceUrl;
            ShowPluginControl(Service);
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), _mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (_mySettings != null && detail != null)
            {
                _mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);

                SettingsManager.Instance.Save(GetType(), _mySettings);
                ShowPluginControl(newService);
            }
        }

        /// <summary>
        /// Shows the plugin control.
        /// </summary>
        /// <param name="newService">The new service.</param>
        private void ShowPluginControl(IOrganizationService newService)
        {
            try
            {
                Host.Child = new MainControl(newService, _mySettings, ConnectionDetail);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                LogError($"Error on show main control {e.Message} {e.StackTrace}");
            }
        }
    }
}