using System.Collections.Generic;
using System.Linq;

namespace StartSportStore.Models
{
    public class Cartt
    {
        private IEnumerable<CarttLine> lineCollection = new List<CarttLine>();
        public virtual void AddItem(Product p, int quantity)
        {
            CarttLine c = lineCollection.Where(cv => cv.Product.ProductID == p.ProductID).FirstOrDefault();
            if (c == null)
            {
                ((List<CarttLine>)lineCollection).Add(new CarttLine
                {
                    Product = p,
                    Quantity = quantity
                });
            }
            else
            {
                c.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product p)
        {
            ((List<CarttLine>)lineCollection).RemoveAll(l => l.Product.ProductID == p.ProductID);
        }
        public virtual decimal ComputeTotalValue()
        {
            return ((List<CarttLine>)lineCollection).Sum(l => l.Product.Price * l.Quantity);
        }
        public virtual void Clear() => ((List<CarttLine>)lineCollection).Clear();
        public virtual IEnumerable<CarttLine> lines => lineCollection;
        public virtual IEnumerable<CarttLine> liness { get { return lineCollection; } }


    }
    public class CarttLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
