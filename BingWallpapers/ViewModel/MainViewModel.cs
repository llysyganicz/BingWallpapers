using BingWallpapers.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Reactive.Subjects;
using System.Reflection;

namespace BingWallpapers.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly BehaviorSubject<string> pathSubject = new BehaviorSubject<string>(Properties.Settings.Default.WallpapersPath);
        private readonly BehaviorSubject<int> intervalSubject = new BehaviorSubject<int>(Properties.Settings.Default.UpdateInterval);
        private readonly BehaviorSubject<bool> autoStartSubject = new BehaviorSubject<bool>(false);
        private readonly DialogService dialogService;

        private RelayCommand changePath;

        public MainViewModel(DialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public string Path
        {
            get => pathSubject.Value;
            set
            {
                pathSubject.OnNext(value);
                Properties.Settings.Default.WallpapersPath = value;
                RaisePropertyChanged();
            }
        }

        public int Interval
        {
            get => intervalSubject.Value;
            set
            {
                intervalSubject.OnNext(value);
                Properties.Settings.Default.UpdateInterval = value;
                RaisePropertyChanged();
            }
        }

        public bool AutoStart
        {
            get => autoStartSubject.Value;
            set
            {
                autoStartSubject.OnNext(value);
                ChangeAutostart(value);
                RaisePropertyChanged();
            }
        }

        public RelayCommand ChangePath => changePath ?? (changePath = new RelayCommand(ChangePathExecute));

        private void ChangePathExecute()
        {
            dialogService.ChooseFolder().MatchSome(value =>
            {
                pathSubject.OnNext(value);
                RaisePropertyChanged(() => Path);
            });
        }

        private void ChangeAutostart(bool autostart)
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            var value = key.GetValue(name);
            if (autostart)
                key.SetValue(name, Assembly.GetEntryAssembly().Location);
            else if (value != null)
                key.DeleteValue(name);
        }
    }
}