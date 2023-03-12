using Microsoft.EntityFrameworkCore;

namespace Youtube_Stream_Title_Convert
{
    public class VideoContext : DbContext
    {
        public DbSet<Table.Video> Video { get; set; }
    }
}
