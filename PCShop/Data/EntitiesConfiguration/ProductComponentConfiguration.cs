using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCShop.Data.Entities;

namespace PCShop.Data.EntitiesConfiguration
{
    ///<inheritdoc cref="ProductComponent"/>
    public class ProductComponentConfiguration : IEntityTypeConfiguration<ProductComponent>
    {
        ///<inheritdoc cref="ProductComponent"/>
        public void Configure(EntityTypeBuilder<ProductComponent> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(511);

            builder.HasOne(d=>d.ParentProductComponent)
                .WithMany(d=>d.ChildrenProductComponents)
                .HasForeignKey(d => d.ParentProductComponentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}