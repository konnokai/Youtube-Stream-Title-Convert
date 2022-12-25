using System.ComponentModel.DataAnnotations;
using System;

namespace Youtube_Stream_Title_Convert.Table
{
    public class YoutubeChannelSpider
    {
        [Key]
        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; } = null;
        public ulong GuildId { get; set; }
        public bool IsTrustedChannel { get; set; } = false;
        public DateTime LastSubscribeTime { get; set; } = DateTime.MinValue;
    }
}
