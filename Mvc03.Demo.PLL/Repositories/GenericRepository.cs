using Microsoft.EntityFrameworkCore;
using Mvc.Demo.DAL.Data.Contexts;
using Mvc.Demo.DAL.Models;
using Mvc03.Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc03.Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        private protected readonly AppDBContext _dbContext;//null
        public GenericRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _dbContext.Employees.Include(E => E.WorkFor).ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetAsync(int? Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }

        public async Task<int> AddAsync(T entity) 
        {
            await _dbContext.Set<T>().AddAsync(entity); 
            return await _dbContext.SaveChangesAsync();  
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return await _dbContext.SaveChangesAsync();  
        }

        public async Task<int> UpdateAsync(T entity) 
        {
            _dbContext.Set<T>().Update(entity);
            return await _dbContext.SaveChangesAsync();  
        }
    }
}
