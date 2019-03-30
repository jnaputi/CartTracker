using CartTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CartTracker.Database
{
    public class CartTrackerContext : DbContext
    {
        public DbSet<Category> Categories { get; }
        
        public CartTrackerContext(DbContextOptions<CartTrackerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Categori__737584F61DCBF5FA")
                    .IsUnique();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);
            });
        }
    }
}
