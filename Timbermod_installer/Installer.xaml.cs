using System.Windows;
using Timbermod_installer.LogSystem;

namespace Timbermod_installer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Installer : Window
    {
        public Installer()
        {
            DataContext = LogService.Instance;
            InitializeComponent();
        }
    }
}