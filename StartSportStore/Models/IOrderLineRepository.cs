using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartSportStore.Models
{
    public interface IOrderLineRepository
    {
        IEnumerable<OrderLine> OrderLines { get; }
        OrderLine GetOrderLine(int id);
        void AddOrderLine(OrderLine line);
        void UpdateOrderLine(OrderLine line);
        void DeleteOrderLine(OrderLine line);
    }
}
