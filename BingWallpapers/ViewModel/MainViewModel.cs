using BingWallpapers.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Reactive.Subjects;

namespace BingWallpapers.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly BehaviorSubject<string> pathSubject = new BehaviorSubject<string>(Properties.Settings.Default.WallpapersPath);
        private readonly BehaviorSubject<int> intervalSubject = new BehaviorSubject<int>(Properties.Settings.Default.UpdateInterval);
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

        public RelayCommand ChangePath => changePath ?? (changePath = new RelayCommand(ChangePathExecute));

        private void ChangePathExecute()
        {
            dialogService.ChooseFolder().MatchSome(value =>
            {
                pathSubject.OnNext(value);
                RaisePropertyChanged(() => Path);
            });
        }
    }
}