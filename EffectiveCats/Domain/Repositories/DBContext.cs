using Domain.Models;
using Domain.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    /// <summary>
    /// TODO: Add Identity
    /// </summary>
    public class MainContext : IdentityDbContext<
        User,
        IdentityRole<long>,
        long,
        IdentityUserClaim<long>,
        IdentityUserRole<long>,
        IdentityUserLogin<long>,
        IdentityRoleClaim<long>,
        IdentityUserToken<long>>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<CatType> CatTypes { get; set; }

        public MainContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(p => p.User)
                .WithMany(b => b.RefreshTokens)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Cat>()
                .HasOne(p => p.Type)
                .WithMany(b => b.Cats)
                .HasForeignKey(f=> f.TypeId);
        }
    }
}