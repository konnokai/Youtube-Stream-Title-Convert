using System.ComponentModel.DataAnnotations;

namespace Youtube_Stream_Title_Convert.DataBase.Table
{
    public class YoutubeChannelOwnedType
    {

        [Key]
        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; } = null;
        public StreamVideo.YTChannelType ChannelType { get; set; } = StreamVideo.YTChannelType.Other;
    }
}
