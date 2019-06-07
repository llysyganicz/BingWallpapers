using BingWallpapers.Services;
using System.Windows;

namespace BingWallpapers.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            Properties.Settings.Default.Save();
            Updater.Instance.Stop();
            Updater.Instance.Start(Properties.Settings.Default.UpdateInterval);
            e.Cancel = true;
        }
    }
}
