using Optional;
using System.Windows.Forms;

namespace BingWallpapers.Services
{
    public class DialogService
    {
        public Option<string> ChooseFolder()
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            return result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath)
                ? Option.Some(dialog.SelectedPath)
                : Option.None<string>();
        }
    }
}
