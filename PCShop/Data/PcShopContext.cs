using Microsoft.EntityFrameworkCore;
using PCShop.Data.Entities;
using PCShop.Data.EntitiesConfiguration;

namespace PCShop.Data
{
    /// <summary>
    /// Implementation of SqlServer DB context of application
    /// </summary>
    public class PcShopContext : DbContext, IDbContext
    {
        #region ctor

        ///<inheritdoc />
        public PcShopContext(DbContextOptions<PcShopContext> options) : base(options)
        {
        }

        #endregion


        #region Methods

        ///<inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductComponentConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAttributeMappingConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAttributeConfiguration());

            base.OnModelCreating(modelBuilder);

        }

        ///<inheritdoc />
        public virtual DbSet<TEntity> Set<TEntity,T>() where TEntity : BaseEntity<T> where T:struct
        {
            return base.Set<TEntity>();
        }

        #endregion
    }
}