using System.ComponentModel.DataAnnotations;

namespace Youtube_Stream_Title_Convert.Table
{
    public class YoutubeChannelOwnedType
    {

        [Key]
        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; } = null;
        public Video.YTChannelType ChannelType { get; set; } = Video.YTChannelType.Other;
    }
}
