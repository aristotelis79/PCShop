using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCShop.Data.Entities;

namespace PCShop.Data.EntitiesConfiguration
{
    ///<inheritdoc cref="Product"/>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        ///<inheritdoc cref="Product"/>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(511);

            builder.HasOne(d => d.ProductComponent)
                .WithMany(d => d.Products)
                .HasForeignKey(d=>d.ProductComponentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}