using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        Task<Product> GetProduct(int? id);
        Product Add(Product product);
        Product SaveProduct(Product product);
        Task<Product> DeleteProduct(int productID);
    }
}
