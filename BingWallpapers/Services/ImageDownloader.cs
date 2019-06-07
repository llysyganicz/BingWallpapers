using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BingWallpapers.Services
{
    public class ImageDownloader
    {
        private const string dailyImageUrl = "http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1";

        private string url;
        private string imageDate;

        public async Task<bool> NeedsDownload()
        {
            using (var client = new HttpClient())
            {
                var jsonString = await client.GetStringAsync(dailyImageUrl).ConfigureAwait(false);
                var jObj = JObject.Parse(jsonString);
                url = jObj["images"][0]["url"].ToString();
                imageDate = jObj["images"][0]["startdate"].ToString();

                var filePath = Path.Combine(Properties.Settings.Default.WallpapersPath, $"Bing-{imageDate}.jpg");
                return !File.Exists(filePath);
            }
        }

        public async Task<string> DownloadImage()
        {
            using (var client = new HttpClient())
            {
                if (url == null || imageDate == null)
                    throw new Exception("You need to call NeedsDownload method first.");

                var buffer = await client.GetByteArrayAsync($"http://www.bing.com{url}").ConfigureAwait(false);

                var path = Properties.Settings.Default.WallpapersPath;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                var filePath = Path.Combine(path, $"Bing-{imageDate}.jpg");
                using (var file = File.Create(filePath))
                {
                    file.Write(buffer, 0, buffer.Length);
                }

                return filePath;
            }
        }
    }
}
