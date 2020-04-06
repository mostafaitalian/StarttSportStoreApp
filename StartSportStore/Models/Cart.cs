using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartSportStore.Models;
using StartSportStore.Infrastructure;

namespace StartSportStore.Models
{
    public class Cart
    {
        private List<OrderLine> selections = new List<OrderLine>();
        public Cart AddItem(Product p, int quantity)
        {
            OrderLine line = selections.Where(l => l.Product.ProductID == p.ProductID).FirstOrDefault();
            if(line == null)
            {
                selections.Add(new OrderLine {ProductId=p.ProductID, Product = p, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
            return this;
        }
        public Cart RemoveItem(int productId)
        {
            selections.RemoveAll(l => l.ProductId == productId);
            return this;
        }
        public void Clear()
        {
            selections.Clear();
        }
        public IEnumerable<OrderLine> Selections { get { return selections; } }
    }
}
