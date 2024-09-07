using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Models.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }  
        public DateTime InvoiceDate { get; set; }
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }  
        public ICollection<InvoiceItem> InvoiceItems { get; set; } 

    }
}
