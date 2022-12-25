using System.ComponentModel.DataAnnotations;

namespace Youtube_Stream_Title_Convert.Table
{
    public class TwitterSpaecSpider
    {
        [Key]
        public string UserId { get; set; }
        public string UserScreenName { get; set; } = null;
        public string UserName { get; set; } = null;
        public ulong GuildId { get; set; }
        public bool IsWarningUser { get; set; } = false;
        public bool IsRecord { get; set; } = false;
    }
}
