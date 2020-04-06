using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartSportStore.Models
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }
        Order GetOrder(int id);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
