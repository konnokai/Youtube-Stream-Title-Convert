﻿using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Youtube_Stream_Title_Convert.Table;

namespace Youtube_Stream_Title_Convert
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(GetDataFilePath("HoloVideoDb.db")))
            {
                Log.Error($"無資料庫，請將資料庫放到 \"{GetDataFilePath("HoloVideoDb.db")}\"");
                Console.ReadKey();
                return;
            }
            if (!File.Exists(GetDataFilePath("NijisanjiVideoDb.db")))
            {
                Log.Error($"無資料庫，請將資料庫放到 \"{GetDataFilePath("NijisanjiVideoDb.db")}\"");
                Console.ReadKey();
                return;
            }
            if (!File.Exists(GetDataFilePath("NotVTuberVideoDb.db")))
            {
                Log.Error($"無資料庫，請將資料庫放到 \"{GetDataFilePath("NotVTuberVideoDb.db")}\"");
                Console.ReadKey();
                return;
            }

            Console.OutputEncoding = System.Text.Encoding.Unicode;

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

            using HoloVideoContext holoVideoContext = HoloVideoContext.GetDbContext();
            using NijisanjiVideoContext nijisanjiVideoContext = NijisanjiVideoContext.GetDbContext();
            using OtherVideoContext otherVideoContext = OtherVideoContext.GetDbContext();

            Regex regex = new Regex(@"youtube_(?'ChannelId'[\w\-\\_]{24})_(?'Date'\d{8})_(?'Time'\d{6})_(?'VideoId'[\w\-\\_]{11})\.(?'Ext'[\w]{2,4})");
            var fileList = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).ToList();
            foreach (var item in fileList)
            {
                var regexResult = regex.Match(item);
                if (!regexResult.Success)
                    continue;

                string videoId = regexResult.Groups["VideoId"].ToString();
                Video? streamVideo = null;

                if (holoVideoContext.Video.Any((x) => x.VideoId == videoId))
                    streamVideo = holoVideoContext.Video.First((x) => x.VideoId == videoId);
                else if (nijisanjiVideoContext.Video.Any((x) => x.VideoId == videoId))
                    streamVideo = nijisanjiVideoContext.Video.First((x) => x.VideoId == videoId);
                else if (otherVideoContext.Video.Any((x) => x.VideoId == videoId))
                    streamVideo = otherVideoContext.Video.First((x) => x.VideoId == videoId);

                if (streamVideo == null)
                {
                    Log.Error($"{item} 無對應的資料!");
                    continue;
                }

                string fileName = $"[{regexResult.Groups["Date"]}-{regexResult.Groups["Time"]}] {streamVideo.VideoTitle} - {videoId}.{regexResult.Groups["Ext"]}";

                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    fileName = fileName.Replace(c, '_');
                }

                if (fileName.Length > 245)
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