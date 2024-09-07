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
    public class CustomerService : GenericService<Customer>,ICustomerRepository
    {
        private readonly BillAppDbContext _dbContext;
        public CustomerService(BillAppDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext; 
        }
        public async Task Update(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();    
        }
    }
}
