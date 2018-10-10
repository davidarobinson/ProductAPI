using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Models
{
    public partial class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {
        }

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> AllProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ProductCode).HasMaxLength(50);

                entity.Property(e => e.ProductGroup).HasMaxLength(255);
            });
        }
    }
}
