using System.IO;
using System.Windows.Forms;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using Newtonsoft.Json;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class EarlyBoundSettingsRepository
    {
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public EarlyBoundSettings GetSettings(string path)
        {
            if (!string.IsNullOrWhiteSpace(path) &&
                File.Exists(path))
            {
                try
                {
                    var settingsText = File.ReadAllText(path);
                    var settings = JsonConvert.DeserializeObject<EarlyBoundSettings>(settingsText);

                    if (settings != null)
                    {
                        return settings;
                    }
                }
                catch
                {
                    // ignored
                }
            }

            return new EarlyBoundSettings();
        }

        /// <summary>
        /// Saves the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="settings">The settings.</param>
        public void Save(string path, EarlyBoundSettings settings)
        {
            if (settings != null &&
                !string.IsNullOrWhiteSpace(path))
            {
                if (!File.Exists(path))
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    }
                }


                var json = JsonConvert.SerializeObject(settings);
                File.WriteAllText(path, json);
            }
        }

        /// <summary>
        /// Opens this instance.
        /// </summary>
        /// <returns></returns>
        public string Open()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }

            return null;
        }
    }
}
