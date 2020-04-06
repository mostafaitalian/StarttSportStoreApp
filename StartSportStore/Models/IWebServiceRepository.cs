using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartSportStore.Models
{
    public interface IWebServiceRepository
    {
        object GetProduct(int id);
        object GetProductt(int id);
        object GetProducts(int take, int skip);
        int StoreProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
