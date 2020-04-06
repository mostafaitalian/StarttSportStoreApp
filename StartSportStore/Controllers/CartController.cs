using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StartSportStore.Models;
using StartSportStore.Infrastructure;

namespace StartSportStore.Controllers
{
    [ViewComponent(Name ="Cart")]
    public class CartController:Controller
    {
        private IProductReprository productRepository;
        private IOrderRepository OrderRepository;
        public CartController(IProductReprository prep, IOrderRepository orep)
        {
            productRepository = prep;
            OrderRepository = orep;
        }
        public IActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(GetCart());
        }

        [HttpPost]
        public IActionResult AddToCart(Product product, string returnUrl)
        {
            SaveCart(GetCart().AddItem(product, 1));
            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId, string returnUrl)
        {
            SaveCart(GetCart().RemoveItem(productId));
            return RedirectToAction(nameof(Index), new { returnUrl });
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
        private Cart GetCart()
        {
           return HttpContext.Session.GetJson<Cart>("Cart")??new Cart();
        }
    }
}
