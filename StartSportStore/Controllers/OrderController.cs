using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartSportStore.Models;

namespace StartSportStore.Controllers
{
    public class OrderController:Controller
    {
        private IProductReprository PRepositorty;
        private IOrderRepository ORepository;
        public OrderController(IProductReprository repp, IOrderRepository repo)
        {
            PRepositorty = repp;
            ORepository = repo;
        }

        public IActionResult Index() => View(ORepository.Orders);
        public IActionResult EditOrder(int id)
        {
            var products = PRepositorty.Products;
            Order order = (id == 0) ? new Order() : ORepository.GetOrder(id);
            Dictionary<int, OrderLine> LinesMap = order.Lines
                .ToDictionary(l => l.ProductId) ?? new Dictionary<int, OrderLine>();
            ViewBag.Lines = products
                .Select(p => LinesMap.ContainsKey(p.ProductID) ? LinesMap[p.ProductID]
            : new OrderLine { Product = p, ProductId = p.ProductID, Quantity = 0 });
            return View(order);

        }
        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order)
        {
            if (order.Id == 0)
            {
                ORepository.AddOrder(order);
                return RedirectToAction("Index");
            }
            else
            {
                Order orderr = ORepository.GetOrder(order.Id);
                ORepository.UpdateOrder(order);
                return RedirectToAction(nameof(Index));

            }
        }
        public IActionResult DeleteOrder(Order order)
        {
            ORepository.DeleteOrder(order);
            return RedirectToAction(nameof(Index));
        }
    }
}
