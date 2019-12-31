using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDBContext _context;

        public EFOrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order) //Метод сохраняет заказы в БД.
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }
    }
}
