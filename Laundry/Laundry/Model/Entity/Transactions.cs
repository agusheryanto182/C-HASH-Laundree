using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Model.Entity
{
    public class Transactions
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string CustomerId { get; set; }
        public string ServiceId { get; set; }
        public int Weight { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; } 
    }

}
