using System.Windows;

namespace BingWallpapers.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            BingWallpapers.Properties.Settings.Default.Save();
            e.Cancel = true;
        }
    }
}
