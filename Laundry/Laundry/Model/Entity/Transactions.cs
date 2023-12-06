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
        public int EmployeeId { get; set; }
        public string CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int Weight { get; set; }
        public int Status { get; set; }
        public int Total { get; set; }

    }
}
