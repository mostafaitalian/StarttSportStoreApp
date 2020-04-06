using System.Collections.Generic;

namespace StartSportStore.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PageInfo pageInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
