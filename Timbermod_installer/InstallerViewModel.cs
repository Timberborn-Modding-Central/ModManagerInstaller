using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Timbermod_installer.LogSystem;

namespace Timbermod_installer
{
    public class InstallerViewModel : INotifyPropertyChanged
    {
        public LogService LogService { get; } = LogService.Instance;

        private string _gamePath = "Timberborn path not found";

        public string GamePath
        {
            get => _gamePath;
            set => SetField(ref _gamePath, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}