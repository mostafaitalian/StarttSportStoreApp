using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace StartSportStore.Models
{
    public class WebServiceRepository:IWebServiceRepository
    {
        private ApplicationDbContext context;
        public WebServiceRepository(ApplicationDbContext con)
        {
            context = con;
        }
        public object GetProduct(int id)
        {
            return context.Products.Include(p=>p.Category).FirstOrDefault(p=>p.ProductID==id);
        }
        public object GetProductt(int id)
        {
            return context.Products
            .Select(p => new {
                Id = p.ProductID,
                Name = p.Name,
                Description = p.Description,
                PurchasePrice = p.Price,
                Price = p.Price,
                CategoryId = p.CategoryId,
                Category = new
                {
                    Id = p.Category.Id,
                    Name=p.Category.Name,
                    Description = p.Category.Description
                }
            })
            .FirstOrDefault(p => p.Id == id);
        }
        public object GetProducts(int take, int skip)
        {
            return context.Products.Include(p => p.Category)
                .Skip(skip).Take(take)
                .Select(p => new
                {
                    Id = p.ProductID,
                    Name = p.Name,
                    Description = p.Description,
                    PurchasePrice = p.Price,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    Category = new
                    {
                        Id = p.Category.Id,
                        Name = p.Category.Name,
                        Description = p.Category.Description
                    }
                });
        }
        
        public int StoreProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product.ProductID;
        }

        public void UpdateProduct(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();

        }

        public void DeleteProduct(int id)
        {
            Product p = (Product)GetProduct(id);
            if (p != null)
            {
                context.Products.Remove(p);
                context.SaveChanges();
            }
            
        }
    }
}
