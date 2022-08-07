using System.ComponentModel.DataAnnotations;

namespace Youtube_Stream_Title_Convert.DataBase.Table
{
    public class YoutubeChannelSpider
    {
        [Key]
        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; } = null;
        public ulong GuildId { get; set; }
        public bool IsWarningChannel { get; set; } = false;
    }
}
