using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCShop.Data.Entities;

namespace PCShop.Data.EntitiesConfiguration
{
    /// <inheritdoc cref="ProductAttribute"/>
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        /// <inheritdoc cref="ProductAttribute"/>
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable(nameof(ProductAttribute))
                .HasKey(k => k.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(511);

            builder.Property(p => p.Unit)
                .IsRequired()
                .HasMaxLength(127);
        }
    }
}