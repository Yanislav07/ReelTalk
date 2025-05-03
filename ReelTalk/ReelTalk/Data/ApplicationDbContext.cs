using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReelTalk.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Production> Productions { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship
            modelBuilder.Entity<WatchlistProduction>()
                .HasKey(wp => new { wp.WatchListId, wp.ProductionId });

            modelBuilder.Entity<WatchlistProduction>()
                .HasOne(w => w.Watchlist)
                .WithMany(w => w.WatchlistProductions)
                .HasForeignKey(wp => wp.WatchListId);

            modelBuilder.Entity<WatchlistProduction>()
                .HasOne(w => w.Production)
                .WithMany(p => p.WatchlistProductions)
                .HasForeignKey(wp => wp.ProductionId);
        }


    }
}
