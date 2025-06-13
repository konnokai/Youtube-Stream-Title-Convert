using Discord_Stream_Notify_Bot.DataBase;
using Discord_Stream_Notify_Bot.DataBase.Table;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Youtube_Stream_Title_Convert
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            if (!File.Exists("DbConfig.txt"))
            {
                Log.Error("DbConfig.txt 檔案不存在，請先建立此檔案並設定資料庫連線字串。");
                Console.ReadKey();
                return;
            }

            var dbService = new MainDbService(File.ReadAllText("DbConfig.txt").Trim());

            string? path;
            do
            {
                Console.Clear();
                Console.Write("直播存檔路徑: ");
                path = Console.ReadLine();

                if (string.IsNullOrEmpty(path))
                    continue;

                if (!Directory.Exists(path))
                    continue;
            } while (string.IsNullOrEmpty(path));

            var regex = new Regex(@"youtube_(?'ChannelId'[\w\-\\_]{24})_(?'Date'\d{8})_(?'Time'\d{6})_(?'VideoId'[\w\-\\_]{11})\.(?'Ext'[\w]{2,4})");
            var fileList = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).ToList();
            foreach (var item in fileList)
            {
                var regexResult = regex.Match(item);
                if (!regexResult.Success)
                    continue;

                using var db = dbService.GetDbContext();
                string videoId = regexResult.Groups["VideoId"].ToString();
                Video streamVideo;

                if (db.HoloVideos.AsNoTracking().Any((x) => x.VideoId == videoId))
                {
                    streamVideo = db.HoloVideos.AsNoTracking().First((x) => x.VideoId == videoId);
                }
                else if (db.NijisanjiVideos.AsNoTracking().Any((x) => x.VideoId == videoId))
                {
                    streamVideo = db.NijisanjiVideos.AsNoTracking().First((x) => x.VideoId == videoId);
                }
                else if (db.OtherVideos.AsNoTracking().Any((x) => x.VideoId == videoId))
                {
                    streamVideo = db.OtherVideos.AsNoTracking().First((x) => x.VideoId == videoId);
                }
                else if (db.NonApprovedVideos.AsNoTracking().Any((x) => x.VideoId == videoId))
                {
                    streamVideo = db.NonApprovedVideos.AsNoTracking().First((x) => x.VideoId == videoId);
                }
                else
                {
                    Log.Error($"{item} 無對應的資料!");
                    continue;
                }

                string fileName = $"[{regexResult.Groups["Date"]}-{regexResult.Groups["Time"]}] {streamVideo.VideoTitle} - {videoId}.{regexResult.Groups["Ext"]}";

                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    fileName = fileName.Replace(c, '_');
                }

                if ($"{path}\\{fileName}".Length > 120)
                {
                    Log.Error($"{item} => {fileName}: 檔名過長");
                    continue;
                }

                try
                {
                    File.Move(item, $"{Path.GetDirectoryName(item)}{GetPlatformSlash()}{fileName}");
                    Log.NewStream($"{item} => {fileName}");
                }
                catch (Exception ex)
                {
                    Log.Error($"{item} => {fileName}: {ex}");
                }
            }

            Log.NewStream("完成!");
            Console.ReadKey();
        }

        public static string GetDataFilePath(string fileName)
            => $"{AppDomain.CurrentDomain.BaseDirectory}Data\\{fileName}";

        public static string GetPlatformSlash()
            => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\\" : "/";
    }
}