using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReelTalk.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<MovieOrShow> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Watchlist>()
                .HasKey(wl => new { wl.UserId, wl.MovieOrShowId });

            modelBuilder.Entity<Watchlist>()
                .HasOne(wl => wl.User)
                .WithMany(u => u.Watchlist)
                .HasForeignKey(wl => wl.UserId);

            modelBuilder.Entity<Watchlist>()
                .HasOne(wl => wl.MovieOrShow)
                .WithMany(m => m.WatchlistedBy)
                .HasForeignKey(wl => wl.MovieOrShowId);
        }
    }
}
