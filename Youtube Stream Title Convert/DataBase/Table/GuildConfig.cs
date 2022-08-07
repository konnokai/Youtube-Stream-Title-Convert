namespace Youtube_Stream_Title_Convert.DataBase.Table
{
    public class GuildConfig : DbEntity
    {
        public ulong GuildId { get; set; }
        public ulong LogMemberStatusChannelId { get; set; } = 0;
    }
}
