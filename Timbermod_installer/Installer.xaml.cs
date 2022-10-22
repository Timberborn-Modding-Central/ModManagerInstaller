using System.Windows;
using System.Windows.Forms;
using Timbermod_installer.GamePathFinder;

namespace Timbermod_installer
{
    public partial class Installer : Window
    {
        private readonly InstallerViewModel _installerViewModel;

        private readonly GamePathFinderService _gamePathFinderService;

        public Installer()
        {
            _installerViewModel = new InstallerViewModel();
            _gamePathFinderService = new GamePathFinderService();

            DataContext = _installerViewModel;

            InitializeComponent();
            FindGamePath();
        }

        public void FindGamePath()
        {
            if (_gamePathFinderService.TryGetGamePath(out string gamePath))
            {
                _installerViewModel.GamePath = gamePath;
            }
        }

        private void OnChangePathClick(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog();


            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            _installerViewModel.GamePath = dlg.SelectedPath;
        }
    }
}