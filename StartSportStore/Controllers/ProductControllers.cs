using Microsoft.AspNetCore.Mvc;
using StartSportStore.Models;
using StartSportStore.Models.ViewModels;
using System.Linq;
namespace SStore.Controllers
{
    public class ProductController : Controller
    {
        public IProductReprository reprository;
        public ICategoryRepository crepository;
        public int pageSize = 4;

        public ProductController(IProductReprository rep, ICategoryRepository cat)
        {
            reprository = rep;
            crepository = cat;
        }
        public IActionResult List(string category, int productPage = 1)
        {
            ViewBag.url = Url.Action("List", "Product");
            return View(
                new ProductListViewModel
                {
                    Products = reprository.Products.OrderBy(p => p.ProductID)/*.Where(p => category == null ||
                    p.Category == category)*/.Skip((productPage - 1) * pageSize).Take(pageSize)
                ,
                    pageInfo = new PageInfo { CurrentPage = productPage, ItemsPerPage = pageSize, TotalItems = category == null ? reprository.Products.Count() : reprository.Products/*.Where(e=>e.Category==category)*/.Count() },
                    CurrentCategory = category
                });
            //return View(reprository.Products.OrderBy(x => x.ProductID)
            //.Skip((pageNumber-1)*pageSize).Take(pageSize));
        }
        public IActionResult Index(QueryOption options)
        {
            //ViewBag.categories = crepository.Categories;
            return View(reprository.GetProducts(options));
        }
        [HttpPost]
        public IActionResult AddProduct(Product p)
        {
            reprository.AddProduct(p);

            return RedirectToAction("Index");
        }
        public IActionResult UpdateProduct(int id)
        {
            ViewBag.categories = crepository.Categories;

            Product p = id == 0 ? new Product() : reprository.GetProduct(id);
            return View(p);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product p)
        {
            if (p.ProductID == 0)
            {
                reprository.AddProduct(p);
            }
            else
            {
                reprository.UpdateProduct(p);

            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            reprository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
