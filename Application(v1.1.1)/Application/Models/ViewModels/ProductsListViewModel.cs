using System.Collections.Generic;

namespace Application.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public int Id { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
