using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartSportStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public bool Shipped { get; set; }
        public IEnumerable<OrderLine> Lines { get; set; }
    }
}
