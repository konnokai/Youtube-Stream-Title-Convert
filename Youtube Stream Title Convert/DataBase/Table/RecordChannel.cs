using System.ComponentModel.DataAnnotations;

namespace Youtube_Stream_Title_Convert.DataBase.Table
{
    class RecordChannel 
    {
        [Key]
        public string ChannelID { get; set; }
    }
}