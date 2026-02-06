using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Configuration
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Color)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.Size)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.Sku)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(v => v.Sku)
                .IsUnique();

            builder.Property(v => v.CurrentPrice)
                .HasPrecision(18, 2);

            builder.Property(v => v.InventoryQuantity)
                .IsRequired();
        }
    }
}
