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
    public class InvoiceService : GenericService<Invoice>, IInvoiceRepository
    {
        private readonly BillAppDbContext _dbContext;
        public InvoiceService(BillAppDbContext dnContext):base(dnContext) 
        {
            _dbContext = dnContext; 
        }
        public async Task UpdateAsync(Invoice invoice)
        {
            _dbContext.Invoices.Add(invoice);
            await _dbContext.SaveChangesAsync();
        }
    }
}
