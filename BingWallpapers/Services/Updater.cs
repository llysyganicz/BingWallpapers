﻿using BingWallpapers.Helpers;
using System;
using System.Reactive.Linq;

namespace BingWallpapers.Services
{
    public class Updater
    {
        private IDisposable subscription;
        private readonly ImageDownloader imageDownloader = new ImageDownloader();
        private readonly Wallpaper wallpaper = new Wallpaper();

        private static Updater instance = null;
        public static Updater Instance => instance ?? (instance = new Updater());

        public void Start(int interval)
        {
            var task = Observable.Timer(TimeSpan.Zero, TimeSpan.FromHours(interval));
            subscription = task.Subscribe(DownloadImage);
        }

        public void Stop()
        {
            subscription.Dispose();
            subscription = null;
        }

        private async void DownloadImage(long parameter)
        {
            if (await imageDownloader.NeedsDownload().ConfigureAwait(false))
            {
                var filename = await imageDownloader.DownloadImage().ConfigureAwait(false);
                wallpaper.Set(filename);
            }
        }
    }
}