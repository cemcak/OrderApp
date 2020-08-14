using Microsoft.EntityFrameworkCore;
using OrderApp.DataAccess.Concrete.EFCore.Context;
using OrderApp.DataAccess.Interfaces;
using OrderApp.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.DataAccess.Concrete.EFCore.Repositories
{
    public class EfGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, ITable, new()
    {
        private readonly OrderAppContext _orderAppContext;

        public EfGenericRepository(OrderAppContext orderAppContext)
        {
            _orderAppContext = orderAppContext;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _orderAppContext.AddAsync(entity);
            await _orderAppContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _orderAppContext.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _orderAppContext.Set<TEntity>().Where(filter).ToListAsync();
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _orderAppContext.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _orderAppContext.Remove(entity);
            await _orderAppContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _orderAppContext.Update(entity);
            await _orderAppContext.SaveChangesAsync();
        }
    }
}
