using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Model.Entity
{
    public class Service
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Duration { get; set; }
    }
}
