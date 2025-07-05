using Microsoft.EntityFrameworkCore;

namespace FRONTEND.Data
{
    public class VinylDbContext : DbContext
    {
        public VinylDbContext(DbContextOptions<VinylDbContext> options) : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Album entity
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Artist).IsRequired();
                entity.Property(e => e.Rating).HasDefaultValue(0);
                entity.Property(e => e.Release).HasDefaultValue(0);
                entity.Property(e => e.Length).HasDefaultValue(0);
            });

            // Configure Track entity
            modelBuilder.Entity<Track>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Length).HasDefaultValue(0);
                entity.HasOne(e => e.Album)
                      .WithMany(e => e.TrackList)
                      .HasForeignKey(e => e.AlbumId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
} 