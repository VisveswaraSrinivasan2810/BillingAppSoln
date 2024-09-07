using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Services.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync (T entity);  
        Task DeleteAsync (T entity);
        Task<IEnumerable<T>> GetAllAsync ();
        Task<T> GetAsync(int id);
        Task Save();
    }
}
