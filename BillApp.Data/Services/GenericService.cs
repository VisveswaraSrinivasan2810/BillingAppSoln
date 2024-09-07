using BillApp.Models.Data;
using BillApp.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Services.Services
{
    public class GenericService<T> : IGenericRepository<T> where T : class
    {
        private readonly BillAppDbContext _dbContext;
        public GenericService(BillAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await Save();
        }

        public async Task DeleteAsync(T entity)
        {
             _dbContext.Set<T>().Remove(entity);
             await Save();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
           
        }

        public async Task<T> GetAsync(int id)
        {
            var res = await _dbContext.Set<T>().FindAsync(id);
            return res;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
