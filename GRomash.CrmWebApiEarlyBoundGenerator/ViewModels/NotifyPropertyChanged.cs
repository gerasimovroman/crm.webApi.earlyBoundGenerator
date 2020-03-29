using System.ComponentModel;
using System.Runtime.CompilerServices;
using GRomash.CrmWebApiEarlyBoundGenerator.Properties;

namespace GRomash.CrmWebApiEarlyBoundGenerator.ViewModels
{
    /// <summary>
    /// NotifyPropertyChanged class
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
