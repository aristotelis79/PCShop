using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PCShop.Data.Entities;

namespace PCShop.Data.Repository
{
    /// <summary>
    /// Represents the Entity Framework repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="T">Struct of Id of Base entity</typeparam>
    public class EfRepository<TEntity, T> : IRepository<TEntity, T> where TEntity : BaseEntity<T> where T : struct
    {
        #region Fields

        private readonly IDbContext _context;

        private DbSet<TEntity> _entities;

        #endregion

        #region Properties
  
        /// <inheritdoc />
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        /// <inheritdoc />
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities => _entities ?? (_entities = _context.Set<TEntity,T>());

        #endregion

        #region Ctor

        public EfRepository(IDbContext context)
        {
            _context = context;
        }

        #endregion
    }
}