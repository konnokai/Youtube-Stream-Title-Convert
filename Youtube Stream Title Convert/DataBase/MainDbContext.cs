using Discord_Stream_Notify_Bot.DataBase.Table;
using Microsoft.EntityFrameworkCore;

namespace Discord_Stream_Notify_Bot.DataBase
{
    public class MainDbContext : DbContext
    {
        private readonly string _connectionString;

        public MainDbContext(string connectionString
            // 要新增 Migration 的時候再把下面的連線字串註解拿掉
            //= "Server=localhost;Port=3306;User Id=stream_bot;Password=Ch@nge_Me;Database=discord_stream_bot"
            )
        {
            _connectionString = connectionString;
        }

        #region Video
        public DbSet<HoloVideos> HoloVideos { get; set; }
        public DbSet<NijisanjiVideos> NijisanjiVideos { get; set; }
        public DbSet<OtherVideos> OtherVideos { get; set; }
        public DbSet<NonApprovedVideos> NonApprovedVideos { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
                .UseSnakeCaseNamingConvention();
        }
    }
}
