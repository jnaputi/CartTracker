using CartTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CartTracker.Database
{
    public partial class CartTrackerContext : DbContext
    {
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Items> Items { get; set; }

        public CartTrackerContext(DbContextOptions<CartTrackerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.CartItemId);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CartItems__CartI__5FB337D6");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__CartItems__ItemI__60A75C0F");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.CartId);

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Carts__737584F64D738149")
                    .IsUnique();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Categori__737584F604E75859")
                    .IsUnique();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__Categorie__ItemI__571DF1D5");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Items__737584F68A4FA5F8")
                    .IsUnique();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });
        }
    }
}
