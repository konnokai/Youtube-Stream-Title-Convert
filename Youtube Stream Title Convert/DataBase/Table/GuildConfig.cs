namespace Youtube_Stream_Title_Convert.Table
{
    public class GuildConfig : DbEntity
    {
        public ulong GuildId { get; set; }
        public ulong LogMemberStatusChannelId { get; set; } = 0;
    }
}
