using BingWallpapers.Views;
using System;
using System.IO;
using System.Windows;
using Settings = BingWallpapers.Properties.Settings;

namespace BingWallpapers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var window = new MainWindow();
            if (Settings.Default.FirstRun)
            {
                Settings.Default.FirstRun = false;
                Settings.Default.WallpapersPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BingWallpapers");
                Settings.Default.Save();
                window.Show();
            }
            TrayIcon.Create(window);
        }
    }
}
