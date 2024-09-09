using BillApp.Models.Data;
using BillApp.Models.Models;
using BillApp.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Services.Services
{
    public class ProductService:GenericService<Product>,IProductRepository
    {
        private readonly BillAppDbContext _dbContext;
        public ProductService(BillAppDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);    
            await _dbContext.SaveChangesAsync();    
        }
    }
}
