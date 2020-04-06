using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartSportStore.Infrastructure;
using StartSportStore.Models;
using StartSportStore.Models.ViewModels;
using System.Linq;
namespace StartSportStore.Controllers
{
    public class CarttControllers : Controller
    {
        private IProductReprository reprository;
        public CarttControllers(IProductReprository rep)
        {
            reprository = rep;
        }
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product p = reprository.Products.FirstOrDefault(s => s.ProductID == productId);
            if (p != null)
            {
                Cartt c = GetCart();
                c.AddItem(p, 1);
                SaveCart(c);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product p = reprository.Products.FirstOrDefault(s => s.ProductID == productId);
            if (p != null)
            {
                Cartt c = GetCart();
                c.RemoveLine(p);
                SaveCart(c);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public IActionResult Index(string returnUrl)
        {
            return View(new CartViewModel
            {
                cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        private Cartt GetCart()
        {
            Cartt cart = HttpContext.Session.GetJson<Cartt>("Cart") ?? new Cartt();
            return cart;
        }
        private void SaveCart(Cartt cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
