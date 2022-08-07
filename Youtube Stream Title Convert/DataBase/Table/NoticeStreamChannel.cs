namespace Youtube_Stream_Title_Convert.DataBase.Table
{
    public class NoticeStreamChannel : DbEntity
    {
        public ulong GuildID { get; set; }
        public ulong ChannelID { get; set; }
        public string NoticeStreamChannelID { get; set; }
    }
}
