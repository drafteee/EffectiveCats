using Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    /// <summary>
    /// TODO: Add Identity
    /// </summary>
    public class MainContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }
        public DbSet<CatType> CatTypes { get; set; }

        public MainContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}