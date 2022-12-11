using OceanStore.DataAccesLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class,IEntity, new()
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>> filter=null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null);
        void AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
    }
}
