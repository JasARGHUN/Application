using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;

        public EFProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public IQueryable<Product> Products => _context.Products;

        public Product Add(Product product) //Метод добавляет обьект в БД.
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public async Task<Product> DeleteProduct(int productID) //Метод удаляет обьект из БД.
        {
            Product dbEntry = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public async Task<Product> GetProduct(int? id) //Метод получает обьект из БД.
        {
            return await _context.Products.FindAsync(id);
        }

        public Product SaveProduct(Product productDataUpdate) //Метод сохраняет обьект в БД.
        {
            var prod = _context.Products.Attach(productDataUpdate);
            prod.State = EntityState.Modified;
            _context.SaveChanges();
            return productDataUpdate;
        }
    }
}
