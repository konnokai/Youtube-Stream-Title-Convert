using System.ComponentModel.DataAnnotations;

namespace Youtube_Stream_Title_Convert.Table
{
    public class Video
    {
        public enum YTChannelType
        {
            Holo, Nijisanji, Other, NotVTuber
        }

        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; }
        [Key]
        public string VideoId { get; set; }
        public string VideoTitle { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public YTChannelType ChannelType { get; set; }

        public override int GetHashCode()
        {
            return VideoId.ToCharArray().Sum((x) => x);
        }

        public override string ToString()
        {
            return ChannelTitle + " - " + VideoTitle;
        }
    }
}
