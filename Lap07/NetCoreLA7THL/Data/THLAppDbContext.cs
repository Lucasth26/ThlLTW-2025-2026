// Author: THL

using Microsoft.EntityFrameworkCore;
using NetCoreLA7THL.Models;

namespace NetCoreLA7THL.Data
{
    public class THLAppDbContext : DbContext
    {
        public THLAppDbContext(DbContextOptions<THLAppDbContext> options) : base(options)
        {
        }

        public DbSet<THLCategory> THLCategories { get; set; }

        public DbSet<THLProduct> THLProducts { get; set; }

        public DbSet<THLBanner> THLBanners { get; set; }

        public DbSet<THLBlog> THLBlogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<THLCategory>(e =>
            {
                e.HasIndex(x => x.Name).IsUnique();
                e.Property(x => x.Status).HasColumnType("tinyint");
                e.Property(x => x.CreatedDate).HasColumnType("date");
                e.Property(x => x.Image).HasColumnType("varchar(100)");
                e.Property(x => x.Description).HasColumnType("nvarchar(350)");
            });

            modelBuilder.Entity<THLProduct>(e =>
            {
                e.HasIndex(x => x.Name).IsUnique();
                e.Property(x => x.Price).HasColumnType("float");
                e.Property(x => x.SalePrice).HasColumnType("float");
                e.Property(x => x.Status).HasColumnType("tinyint");
                e.Property(x => x.CreatedDate).HasColumnType("date");
                e.Property(x => x.Image).HasColumnType("varchar(100)");
                e.Property(x => x.Description).HasColumnType("nvarchar(350)");
                e.HasOne(x => x.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<THLBanner>(e =>
            {
                e.HasIndex(x => x.Name).IsUnique();
                e.Property(x => x.Status).HasColumnType("tinyint");
                e.Property(x => x.Image).HasColumnType("varchar(100)");
                e.Property(x => x.Description).HasColumnType("nvarchar(350)");
            });

            modelBuilder.Entity<THLBlog>(e =>
            {
                e.HasIndex(x => x.Name).IsUnique();
                e.Property(x => x.Status).HasColumnType("tinyint");
                e.Property(x => x.CreatedDate).HasColumnType("date");
                e.Property(x => x.Image).HasColumnType("varchar(100)");
                e.Property(x => x.Description).HasColumnType("nvarchar(350)");
            });
        }
    }
}