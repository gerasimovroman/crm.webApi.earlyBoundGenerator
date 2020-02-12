using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using GRomash.CrmWebApiEarlyBoundGenerator.Command;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Repository;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Service;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using NuGet;
using XrmToolBox.Extensibility;

namespace GRomash.CrmWebApiEarlyBoundGenerator.ViewModels
{
    /// <summary>
    /// View model for Main Control
    /// </summary>
    /// <seealso cref="GRomash.CrmWebApiEarlyBoundGenerator.ViewModels.NotifyPropertyChanged" />
    public class MainControlViewModel : NotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The settings
        /// </summary>
        private readonly Settings _settings;
        /// <summary>
        /// The connection detail
        /// </summary>
        private readonly ConnectionDetail _connectionDetail;
        /// <summary>
        /// The metadata repository
        /// </summary>
        private readonly MetadataRepository _metadataRepository;
        /// <summary>
        /// The CRM version repository
        /// </summary>
        private readonly CrmVersionRepository _crmVersionRepository;
        /// <summary>
        /// The generation service
        /// </summary>
        private readonly GenerationService _generationService;
        /// <summary>
        /// The early bound settings repository
        /// </summary>
        private readonly EarlyBoundSettingsRepository _earlyBoundSettingsRepository;
        /// <summary>
        /// The entities list
        /// </summary>
        private IEnumerable<EntityModel> _entitiesList;
        /// <summary>
        /// The is loading
        /// </summary>
        private bool _isLoading;
        /// <summary>
        /// The settings path
        /// </summary>
        private string _settingsPath;
        /// <summary>
        /// The ns
        /// </summary>
        private string _ns;
        /// <summary>
        /// The early bound settings
        /// </summary>
        private EarlyBoundSettings _earlyBoundSettings;

        /// <summary>
        /// The selected entities
        /// </summary>
        private IEnumerable<EntityModel> _selectedEntities;
        /// <summary>
        /// The result folder
        /// </summary>
        private string _resultFolder;
        /// <summary>
        /// The option sets result folder
        /// </summary>
        private string _optionSetsResultFolder;
        /// <summary>
        /// The output logger service
        /// </summary>
        private readonly OutputLoggerService _outputLoggerService;
        /// <summary>
        /// The output
        /// </summary>
        private string _output;
        /// <summary>
        /// The folder
        /// </summary>
        private const string Folder = nameof(CrmWebApiEarlyBoundGenerator);

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MainControlViewModel"/> class.
        /// </summary>
        /// <param name="organizationService">The organization service.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="connectionDetailOriginalUrl">The connection detail original URL.</param>
        public MainControlViewModel(IOrganizationService organizationService, Settings settings,
            ConnectionDetail connectionDetailOriginalUrl)
        {
            _settings = settings;
            _connectionDetail = connectionDetailOriginalUrl;
            _outputLoggerService = new OutputLoggerService(this);
            _metadataRepository = new MetadataRepository(organizationService);
            _crmVersionRepository = new CrmVersionRepository(organizationService);
            _earlyBoundSettingsRepository = new EarlyBoundSettingsRepository();
            _generationService = new GenerationService(_metadataRepository);
            SettingsPath = settings.CurrentSettingsPath;

            CreateSettingPathIfNotExist();
            CheckVersion();
            LoadSettings();
            RefreshEntities();
        }

        /// <summary>
        /// Creates the setting path if not exist.
        /// </summary>
        private void CreateSettingPathIfNotExist()
        {
            if (string.IsNullOrWhiteSpace(SettingsPath))
            {
                SettingsPath = Path.Combine(Directory.GetCurrentDirectory(), nameof(CrmWebApiEarlyBoundGenerator),
                    nameof(Settings), $"{_connectionDetail.ConnectionName}.json");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value; 
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        /// <summary>
        /// Gets or sets the entities list.
        /// </summary>
        /// <value>
        /// The entities list.
        /// </value>
        public IEnumerable<EntityModel> EntitiesList
        {
            get => _entitiesList;
            set
            {
                _entitiesList = value;
                OnPropertyChanged(nameof(EntitiesList));
                SetSelectedEntities();
            }
        }

        /// <summary>
        /// Gets or sets the selected entities.
        /// </summary>
        /// <value>
        /// The selected entities.
        /// </value>
        public IEnumerable<EntityModel> SelectedEntities
        {
            get => _selectedEntities;
            set
            {
                _selectedEntities = value;
                OnPropertyChanged(nameof(SelectedEntities));
            }
        }

        /// <summary>
        /// Gets the refresh entities command.
        /// </summary>
        /// <value>
        /// The refresh entities command.
        /// </value>
        public ICommand RefreshEntitiesCommand => new CommandBase(RefreshEntities);

        /// <summary>
        /// Gets the generate command.
        /// </summary>
        /// <value>
        /// The generate command.
        /// </value>
        public ICommand GenerateCommand => new CommandBase(Generate);
     
        /// <summary>
        /// Gets the select file command.
        /// </summary>
        /// <value>
        /// The select file command.
        /// </value>
        public ICommand SelectFileCommand => new CommandBase(SelectFile);

        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public ICommand SaveCommand => new CommandBase(SaveSettings);

        /// <summary>
        /// Gets the select result folder command.
        /// </summary>
        /// <value>
        /// The select result folder command.
        /// </value>
        public ICommand SelectResultFolderCommand => new CommandBase(SelectResultFolder);

        /// <summary>
        /// Gets or sets the settings path.
        /// </summary>
        /// <value>
        /// The settings path.
        /// </value>
        public string SettingsPath
        {
            get => _settingsPath;
            set
            {
                _settingsPath = value; 
                OnPropertyChanged(nameof(SettingsPath));
                LoadSettings();
            }
        }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
        public string Namespace
        {
            get => _ns ?? nameof(CrmWebApiEarlyBoundGenerator);
            set
            {
                _ns = value;
                OnPropertyChanged(nameof(Namespace));
            }
        }

        /// <summary>
        /// Gets or sets the result folder.
        /// </summary>
        /// <value>
        /// The result folder.
        /// </value>
        public string ResultFolder
        {
            get => _resultFolder ?? Path.Combine(Directory.GetCurrentDirectory(), "CrmWebApiEarlyBoundGenerator", "Result");
            set
            {
                _resultFolder = value; 
                OnPropertyChanged(nameof(ResultFolder));
            }
        }

        /// <summary>
        /// Gets or sets the result folder.
        /// </summary>
        /// <value>
        /// The result folder.
        /// </value>
        public string OptionSetsResultFolder
        {
            get => Path.Combine(ResultFolder, "OptionSets");
            set {
                _optionSetsResultFolder = value;
                OnPropertyChanged(nameof(OptionSetsResultFolder));
            }
        }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>
        /// The output.
        /// </value>
        public string Output
        {
            get => _output;
            set
            {
                _output = value;
                OnPropertyChanged(nameof(Output));
            }
        }




        /// <summary>
        /// Generates this instance.
        /// </summary>
        private async void Generate()
        {
            await Task.Run(() =>
            {
                try
                {
                    TryGenerateEntities();
                }
                catch (Exception e)
                {
                    _outputLoggerService.Error($"Error {e.Message} {e.StackTrace}");
                }

                IsLoading = false;
            });
        }

        /// <summary>
        /// Tries the generate entities.
        /// </summary>
        private void TryGenerateEntities()
        {
            IsLoading = true;
            _outputLoggerService.Info($"Loading Web Api Metadata from CRM");
            _metadataRepository.ClearCache();

            var selectedEntities = GetSelectedEntities();
            var metadata = _metadataRepository.GetEntitiesMetadata(selectedEntities);
            var optionSetMetadata = _metadataRepository.GetOptionSetMetadata(selectedEntities);

            _outputLoggerService.Info($"Generating entities");

            _generationService.GenerateEntities(Namespace, ResultFolder, selectedEntities, metadata);
            _generationService.GenerateOptionSets(Namespace, OptionSetsResultFolder, optionSetMetadata);
            _outputLoggerService.Info($"Entities generated to {ResultFolder}");
        }

        private string[] GetSelectedEntities()
        {
            return SelectedEntities.Select(x => x.LogicalName).ToArray();
        }

        /// <summary>
        /// Selects the file.
        /// </summary>
        private void SelectFile()
        {
            var fileName = _earlyBoundSettingsRepository.Open();
            SettingsPath = fileName;
        }

        /// <summary>
        /// Refreshes the entities.
        /// </summary>
        private void RefreshEntities()
        {
            IsLoading = true;

            Task.Run(() =>
            {
                try
                {
                    _outputLoggerService.Info("Getting entities metadata from CRM");
                    EntitiesList = _metadataRepository.GetEntities();
                    IsLoading = false;
                }
                catch (Exception e)
                {
                    _outputLoggerService.Error($"Error on loading entities metadata from CRM {e.Message} {e.StackTrace}");
                }
            });
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        private void LoadSettings()
        {
            _earlyBoundSettings = _earlyBoundSettingsRepository.GetSettings(SettingsPath);
            Namespace = _earlyBoundSettings.Namespace;
            ResultFolder = _earlyBoundSettings.ResultFolder;

            SetSelectedEntities();
        }

        /// <summary>
        /// Sets the selected entities.
        /// </summary>
        private void SetSelectedEntities()
        {
            SelectedEntities = EntitiesList?.Where(x => _earlyBoundSettings.Entitites.Contains(x.LogicalName));
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        private void SaveSettings()
        {
            var settingPathPair = _settings.SettingsPathPairs.FirstOrDefault(x => x.ConnectionString == _settings.LastUsedOrganizationWebappUrl);

            if (settingPathPair == null)
            {
                _settings.SettingsPathPairs.Add(new SettingsPathPair()
                {
                    ConnectionString = _settings.LastUsedOrganizationWebappUrl,
                    SettingsPath = SettingsPath
                });
            }
            else
            {
                settingPathPair.SettingsPath = SettingsPath;
            }

            _earlyBoundSettings.Namespace = Namespace;
            _earlyBoundSettings.Entitites = GetSelectedEntities();
            _earlyBoundSettings.ResultFolder = ResultFolder;

            _earlyBoundSettingsRepository.Save(SettingsPath, _earlyBoundSettings);

            SettingsManager.Instance.Save(typeof(MyPluginControl), _settings);

            _outputLoggerService.Info($"Settings saved");
        }

        /// <summary>
        /// Selects the result folder.
        /// </summary>
        private void SelectResultFolder()
        {
            using(var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    ResultFolder = fbd.SelectedPath;
                }
            }
        }

        /// <summary>
        /// Checks the version.
        /// </summary>
        /// <exception cref="Exception">Current Crm version {currentCrmVersion} is not supported!</exception>
        private void CheckVersion()
        {
            var currentCrmVersion = _crmVersionRepository.GetVersion();

            if (currentCrmVersion < CrmVersionRepository.SupportedVersion)
            {
                throw new Exception($"Current Crm version {currentCrmVersion} is not supported!");
            }
        }
    }
}
