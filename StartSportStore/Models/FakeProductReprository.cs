using StartSportStore.Models.pages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StartSportStore.Models
{
    public class FakeProductReprository : IProductReprository
    {
        //public IQueryable<Product> products => new List<Product> { new Product { Name = "Football", Price = 25 }, new Product { Name = "Basketball", Price = 30 }, new Product { Name = "Handball", Price = 15 } }.AsQueryable<Product>();

        public IQueryable<Product> Products => new List<Product> { new Product { Name = "Football", Price = 25 }, new Product { Name = "Basketball", Price = 30 }, new Product { Name = "Handball", Price = 15 } }.AsQueryable<Product>();

        public IEnumerable<Product> Productss => throw new NotImplementedException();

        public void AddProduct(Product p)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public PagedList<Product> GetProducts(QueryOption options)
        {
            throw new NotImplementedException();
        }

        public PagedList<Product> GetProducts(QueryOption options, int CategoryId)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product p)
        {
            throw new NotImplementedException();
        }
    }
}
