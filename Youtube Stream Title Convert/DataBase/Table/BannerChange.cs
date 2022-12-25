namespace Youtube_Stream_Title_Convert.Table
{
    public class BannerChange : DbEntity
    {
        public ulong GuildId { get; set; }
        public string ChannelId { get; set; }
        public string LastChangeStreamId { get; set; } = null;
    }
}
