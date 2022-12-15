using Microsoft.EntityFrameworkCore;
using Secret_Share.DTO;

namespace Secret_Share
{
    public class DataContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }
    }
}