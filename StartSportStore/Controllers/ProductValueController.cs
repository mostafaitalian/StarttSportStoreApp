using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StartSportStore.Models;
namespace StartSportStore.Controllers
{
    [Route("api/products")]
    public class ProductValueController:Controller
    {
        private IWebServiceRepository repository;
        public ProductValueController(IWebServiceRepository rep)
        {
            repository = rep;
        }
        [HttpGet("{id}")]
        public object GetProduct(int id)
        {
            return repository.GetProductt(id)??NotFound();
        }
        [HttpGet]
        public object Products(int take, int skip)
        {
            return repository.GetProducts(take, skip);
        }
        [HttpPost]
        public int StoreProduct([FromBody] Product product)
        {
            return repository.StoreProduct(product);
        }
        [HttpPut]
        public void UpdateProduct([FromBody] Product product)
        {
            repository.UpdateProduct(product);
        }
        [HttpDelete("{id}")]
        public void DeleteProduct(long id)
        {
            repository.DeleteProduct(id);
        }
    }
}
