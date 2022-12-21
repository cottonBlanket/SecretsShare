using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecretsShare.DTO;

namespace SecretsShare
{
    /// <summary>
    /// a class representing the current database view
    /// </summary>
    public class DataContext: DbContext
    {
        /// <summary>
        /// list of all user table entries
        /// </summary>
        public DbSet<User> Users { get; set; }
        
        /// <summary>
        /// list of all file table entries
        /// </summary>
        public DbSet<File> Files { get; set; }
        
        /// <summary>
        /// constructor class, inherited from the base
        /// </summary>
        /// <param name="options">input parameters</param>
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }
        
        /// <summary>
        /// sends a request to update the database corresponding to the class fields
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        /// <summary>
        /// method for configuring database table fields
        /// </summary>
        /// <param name="modelBuilder">form of entities</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}