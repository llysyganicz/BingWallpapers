using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace BingWallpapers.Helpers
{
    public class Wallpaper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        private const UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private const UInt32 SPIF_UPDATEINIFILE = 0x01;
        private const UInt32 SPIF_SENDWININICHANGE = 0x02;
        private const UInt32 SPI_GETDESKWALLPAPER = 0x73;
        private const int MAX_PATH = 260;

        public void Set(String path)
        {
            try
            {
                if (path != GetWallpaper())
                    SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bing Wallpapers", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private String GetWallpaper()
        {
            String wallpaper = new String('\0', MAX_PATH);
            SystemParametersInfo(SPI_GETDESKWALLPAPER,
                (UInt32)wallpaper.Length, wallpaper, 0);
            return wallpaper.Substring(0, wallpaper.IndexOf('\0'));
        }
    }
}
