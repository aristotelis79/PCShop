using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCShop.Data.Entities;

namespace PCShop.Data.EntitiesConfiguration
{
    ///<inheritdoc cref="ProductComponentAttributeMap"/>

    public class ProductAttributeMappingConfiguration : IEntityTypeConfiguration<ProductComponentAttributeMap>
    {
        ///<inheritdoc cref="ProductComponentAttributeMap"/>
        public void Configure(EntityTypeBuilder<ProductComponentAttributeMap> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasOne(d => d.ProductAttribute)
                .WithMany(d => d.ProductAttributesMap)
                .HasForeignKey(d => d.ProductAttributeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.ProductComponent)
                .WithMany(d => d.ProductAttributesMap)
                .HasForeignKey(d => d.ProductComponentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}