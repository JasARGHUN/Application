using Application.Models;
using Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int pageSize = 5;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> List(int? product, string name, string category, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Product> source = _repository.Products.Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID);
            if (product != null && product != 0)
            {
                source = source.Where(p => p.ProductID == product);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Name.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    source = source.OrderByDescending(s => s.Name); break;
                case SortState.PriceAsc:
                    source = source.OrderBy(s => s.Price); break;
                case SortState.PriceDesc:
                    source = source.OrderByDescending(s => s.Price); break;
                case SortState.CategoryAsc:
                    source = source.OrderBy(s => s.Category); break;
                case SortState.CategoryDesc:
                    source = source.OrderByDescending(s => s.Category); break;
                default:
                    source = source.OrderBy(s => s.Name); break;
            }

            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PagingInfo pagingInfo = new PagingInfo(count, page, pageSize);

            ProductsListViewModel productsListView = new ProductsListViewModel
            {
                PagingInfo = pagingInfo,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_repository.Products.ToList(), product, name),
                Products = items
            };

            return View(productsListView);
        }

        public async Task<ViewResult> Details(int? id)
        {
            Product product = await _repository.GetProduct(id.Value);

            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            return View(product);
        }
    }
}
