using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Base2io.Ventanas.Logic;

namespace Base2io.Ventanas
{
    // <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        WindowPlacement _windowPlacement;
        TaskbarIcon _tbi;

        protected override void OnStartup(StartupEventArgs e)
        {
            InitializeInstalledApplication();

            _tbi = (TaskbarIcon)FindResource("TrayIcon");

            _windowPlacement = WindowPlacement.Instance;
            _windowPlacement.RegisterHotkeys();

            base.OnStartup(e);
        }

        private void InitializeInstalledApplication()
        {
            // Check to see if this is the first time the application has been ran.
            if (Ventanas.Properties.Settings.Default.WindowsStartup)
            {
                // Mark the application for windows startup.
                ApplicationSettings.SetWindowsStartup();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _windowPlacement.Dispose();
            _windowPlacement = null;

            _tbi.Dispose();
            _tbi = null;

            base.OnExit(e);
        }
    }
}
