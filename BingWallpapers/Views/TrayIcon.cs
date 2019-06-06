using System.Windows.Forms;

namespace BingWallpapers.Views
{
    public static class TrayIcon
    {
        public static void Create(MainWindow window)
        {
            var icon = new NotifyIcon
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = Properties.Resources.bing,
                Visible = true
            };
            icon.ContextMenuStrip.Items.Add("Preferneces", null, (sender, e) =>
            {
                if (window.IsVisible) window.Hide();
                else window.Show();
            });
            icon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            icon.ContextMenuStrip.Items.Add("Quit", null, (sender, e) =>
            {
                icon.Visible = false;
                App.Current.Shutdown();
            });

            icon.DoubleClick += (sender, e) =>
            {
                if (window.IsVisible) window.Hide();
                else window.Show();
            };

            icon.MouseUp -= IconClick;
            icon.MouseUp += IconClick;
        }

        private static void IconClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var icon = (sender as NotifyIcon);
            icon.ContextMenuStrip.Show(Control.MousePosition);
        }
    }
}
