using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StartSportStore.Models
{
    public class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        public OrderRepository(ApplicationDbContext con)
        {
            context = con;

        }
        public IEnumerable<Order> Orders => context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);

        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();

        }

        public void DeleteOrder(Order order)
        {
            Order o = GetOrder(order.Id);
            context.Orders.Remove(o);

            context.SaveChanges();
        }

        public Order GetOrder(int id)
        {
            Order order = context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product).First(o => o.Id == id);
            return order;
        }

        public void UpdateOrder(Order order)
        {
            context.Orders.Update(GetOrder(order.Id));
            context.SaveChanges();
        }
    }
}
