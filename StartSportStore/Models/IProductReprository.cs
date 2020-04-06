using System.Collections.Generic;
using System.Linq;
using StartSportStore.Models.pages;
namespace StartSportStore.Models
{
    public interface IProductReprository
    {

        IQueryable<Product> Products { get; }
        IEnumerable<Product> Productss { get; }
        PagedList<Product> GetProducts(QueryOption options, int CategoryId=0);
        Product GetProduct(int id);
        void AddProduct(Product p);
        void UpdateProduct(Product p);
        void DeleteProduct(int id);
    }
}
