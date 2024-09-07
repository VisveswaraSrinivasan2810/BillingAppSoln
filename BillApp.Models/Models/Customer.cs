using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Models.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; } 
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
