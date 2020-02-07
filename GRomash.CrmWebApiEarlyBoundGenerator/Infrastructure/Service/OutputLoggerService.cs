using GRomash.CrmWebApiEarlyBoundGenerator.ViewModels;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Service
{
    /// <summary>
    /// Logger in Main Control
    /// </summary>
    public class OutputLoggerService
    {
        /// <summary>
        /// The view model
        /// </summary>
        private readonly MainControlViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputLoggerService"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public OutputLoggerService(MainControlViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(object message)
        {
            Append($"INFO {message}");
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(object message)
        {
            Append($"WARN {message}");
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(object message)
        {
            Append($"ERROR {message}");
        }


        /// <summary>
        /// Appends the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        private void Append(string text)
        {
            _viewModel.Output += $"{text}\r\n";
        }
    }
}
