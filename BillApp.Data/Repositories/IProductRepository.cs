﻿using BillApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Services.Repositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task UpdateAsync(Product product);
    }
}
