using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Infrastructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product.Domain.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Product.Domain.Entities.Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description) // Reduced from 1000 to be safer, or keep max
                .HasMaxLength(1000);

            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.HasIndex(p => p.Code)
                .IsUnique();

            builder.Property(p => p.Technology)
                .HasMaxLength(100);

            builder.Property(p => p.Gender)
                .HasMaxLength(50);

            builder.Property(p => p.BasePrice)
                .HasPrecision(18, 2);

            // Relationships
            builder.HasMany(p => p.Variants)
                .WithOne(v => v.Product)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
