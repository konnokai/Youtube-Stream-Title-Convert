using System.ComponentModel.DataAnnotations;

namespace Youtube_Stream_Title_Convert.DataBase.Table
{
    public class RecordYoutubeChannel 
    {
        [Key]
        public string YoutubeChannelId { get; set; }
    }
}