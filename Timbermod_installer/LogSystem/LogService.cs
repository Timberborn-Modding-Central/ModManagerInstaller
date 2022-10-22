using System.Collections.ObjectModel;

namespace Timbermod_installer.LogSystem
{
    public class LogService
    {
        private static LogService _logService;

        public static LogService Instance => _logService ??= new LogService();

        public ObservableCollection<string> Logs { get; } = new();

        public static void Send(string message)
        {
            _logService.Logs.Add(message);
        }
    }
}