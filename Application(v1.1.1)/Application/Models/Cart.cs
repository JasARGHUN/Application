using System.Collections.Generic;
using System.Linq;

namespace Application.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity, decimal sum)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                    TotalSum = sum
                });
            }
            else
            {
                line.Quantity += quantity;
                line.TotalSum += sum;
            }
        }

        public virtual void RemoveLine(Product product) =>
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);

        public virtual decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public virtual void Clear() =>
            lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines =>
            lineCollection;
    }
}
