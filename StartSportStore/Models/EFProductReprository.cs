using Microsoft.EntityFrameworkCore;
using StartSportStore.Models.pages;
using System.Collections.Generic;
using System.Linq;
namespace StartSportStore.Models
{
    public class EFProductReprository : IProductReprository
    {
        private ApplicationDbContext context;
        public EFProductReprository(ApplicationDbContext con)
        {
            context = con;
        }

        public IQueryable<Product> Products => context.Products;

        public IEnumerable<Product> Productss { get => context.Products.Include(p => p.Category).ToList<Product>(); }

        public void AddProduct(Product p)
        {
            context.Products.Add(p);
            context.SaveChanges();
        }
        public Product GetProduct(int id)
        {
            Product product = context.Products.Include(p => p.Category).First(p => p.ProductID == id);
            return product;
        }
        public void DeleteProduct(int id)
        {
            Product p = GetProduct(id);
            context.Products.Remove(p);
            context.SaveChanges();
        }

        public void UpdateProduct(Product p)
        {
            // context.Update(GetProduct());
            Product product = GetProduct(p.ProductID);
            product.Name = p.Name;
            product.RetailPrice = p.RetailPrice;
            //product.PurchasePrice = p.PurchasePrice;
            product.Quantity = p.Quantity;
            product.Price = p.Price;
            product.CategoryId = p.CategoryId;
            product.Category = p.Category;
            context.SaveChanges();


        }

        public PagedList<Product> GetProducts(QueryOption options, int CategoryId)
        {
            IQueryable<Product> query = context.Products.Include(p => p.Category);
            if(CategoryId != 0)
            {
                query = query.Where(p => p.CategoryId == CategoryId);
            }

               return new PagedList<Product>(query, options);
        }

    }
}
