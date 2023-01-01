using Microsoft.EntityFrameworkCore;
using OceanStore.BusinessLayer.Interfaces;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Repositorys
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        private readonly TContext _db;
        public GenericRepository(TContext db)
        {
            _db = db;
        }
        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? await _db.Set<TEntity>().ToListAsync()
                : await _db.Set<TEntity>().Where(filter).ToListAsync();
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? await _db.Set<TEntity>().FirstOrDefaultAsync()
                : await _db.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
        }
        public void AddAsync(TEntity entity)
        {
            var addedEntity = _db.Entry(entity);
            addedEntity.State = EntityState.Added;
        }
        public void UpdateAsync(TEntity entity)
        {
            var addedEntity = _db.Entry(entity);
            addedEntity.State = EntityState.Modified;
        }

        public void DeleteAsync(TEntity entity)
        {
            var addedEntity = _db.Entry(entity);
            addedEntity.State = EntityState.Deleted;
        }

        public bool CheckActive(bool active)
        {
            return active ? false : true;
        }
    }
}
