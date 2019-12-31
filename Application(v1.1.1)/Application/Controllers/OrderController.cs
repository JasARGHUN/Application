using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repository;
        private Cart _cart;

        public OrderController(IOrderRepository repositoryService, Cart cartService)
        {
            _repository = repositoryService;
            _cart = cartService;
        }

        [Authorize]
        public ViewResult Index(int page = 1)
        {
            var qry = _repository.Orders.AsNoTracking().Where(o => !o.Shipped);
            var model = PagingList.Create(qry, 2, page);
            return View(model);
        }

        [HttpGet]
        public ViewResult ViewDataBase(string filter, int page = 1, string sortExpression = "OrderTime") //Получаем данные из БД для просмотра в представлении.
        {
            var data = _repository.Orders.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                data = data.Where(d => d.Name.Contains(filter));
            }
            var model = PagingList.Create(data, 5, page, sortExpression, "OrderTime");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            model.Action = "ViewDataBase";
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MarkShipped(int orderID)
        {
            Models.Order order = await _repository.Orders
                .FirstOrDefaultAsync(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                _repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(Index));
        }

        public ViewResult Checkout() =>
            View(new Models.Order());

        [HttpPost]
        public IActionResult Checkout(Models.Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Ваша корзина пуста");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray(); //обрабатываем и далее сохраняем покупки сделанные юзером в БД.
                order.TotalAmount = _cart.ComputeTotalValue(); //сохраняем общую сумму покупки в БД.
                _repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}
