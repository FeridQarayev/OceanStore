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
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? await _db.Set<TEntity>().FirstOrDefaultAsync()
                : await _db.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
        }
        public async Task<bool> CheckExist(Expression<Func<TEntity, bool>> filter)
        {
            return await _db.Set<TEntity>().AnyAsync(filter);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
