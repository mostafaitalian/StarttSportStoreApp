using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartSportStore.Models;
using StartSportStore.Models.pages;
using Microsoft.AspNetCore.Mvc;

namespace StartSportStore.Controllers
{
    public class StoreController:Controller
    {
        private IProductReprository productRepositorty;
        private ICategoryRepository categoryRepository;
        public StoreController(IProductReprository pr, ICategoryRepository cr)
        {
            productRepositorty = pr;
            categoryRepository = cr;
        }

        public IActionResult Index([FromQuery(Name="options")]QueryOption productOptions, QueryOption categoryOptions, int categoryId)
        {
            ViewBag.Categories = categoryRepository.GetCategories(categoryOptions);
            ViewBag.SelectedCategory = categoryId;
            return View(productRepositorty.GetProducts(productOptions, categoryId)); 
        }

    }
}
