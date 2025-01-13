using PetInfo.Database.Data;
using PetInfo.Repositories.Contracts.AllContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetInfo.Repositories.AllRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            this._dbContext =dbContext;
        }
        public virtual async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public virtual async Task<T?> GetByIdAsync(int? id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
        }
    }
}
