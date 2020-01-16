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

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="token">Cancellation token for method</param>
        /// <returns>Entity</returns>
        public virtual async Task<TEntity> GetByIdAsync(T id, CancellationToken token = default)
        {
            return await Entities.FindAsync(new object[]{id},token)
                .ConfigureAwait(false);
        }


        
        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Error message</returns>
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }
            
            try
            { 
                var result =  _context.SaveChangesAsync().Result;
                return exception.ToString();
            }
            catch (Exception ex)
            {
                //if after the rollback of changes the context is still not saving,
                //return the full text of the exception that occurred when saving
                return ex.ToString(); 
            }
        }
        #endregion
    }
}