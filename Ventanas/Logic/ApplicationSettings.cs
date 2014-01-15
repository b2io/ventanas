using System.Reflection;

namespace Base2io.Ventanas.Logic
{
    public class ApplicationSettings
    {
        public static void SetWindowsStartup()
        {
            try
            {
                // Only make registry changes if not in debug mode.
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    Microsoft.Win32.RegistryKey key =
                        Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                            "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (key != null)
                    {
                        Assembly curAssembly = Assembly.GetExecutingAssembly();
                        key.SetValue(curAssembly.GetName().Name, curAssembly.Location);
                    }
                }
            }
            catch
            {
                // TODO: Log error
            }

            Properties.Settings.Default.WindowsStartup = true;
            Properties.Settings.Default.Save();            
        }

        public static void RemoveWindowsStartup()
        {
            try
            {
                // Only make registry changes if not in debug mode.
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    Microsoft.Win32.RegistryKey key =
                        Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                            "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (key != null)
                    {
                        Assembly curAssembly = Assembly.GetExecutingAssembly();
                        key.DeleteValue(curAssembly.GetName().Name);
                    }
                }
            }
            catch
            {
                // TODO: Log error
            }

            Properties.Settings.Default.WindowsStartup = false;
            Properties.Settings.Default.Save();
        }
    }
}
