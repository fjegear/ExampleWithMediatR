using ExampleWithMediatR.Data.Context;
using ExampleWithMediatR.Data.RepositoriesAbstractions;
using ExampleWithMediatR.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExampleWithMediatR.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly CustomerDbContext _dbContext;
        protected readonly DbSet<TEntity> EntitySets;

        public Repository(CustomerDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException();
            EntitySets = _dbContext.Set<TEntity>();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await EntitySets.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await EntitySets.AsNoTracking().Where(predicate).ToListAsync();
        }

        public IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySets.Where(predicate);
        }
        public void Add(TEntity entity)
        {
            EntitySets.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            EntitySets.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                EntitySets.Attach(entity);
            }

            EntitySets.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                {
                    EntitySets.Attach(entity);
                }

                EntitySets.Remove(entity);
            }
        }
        public void Update(TEntity entity)
        {
            EntitySets.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
