using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Краткое описание не может быть больше 100 символов.")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Описание объекта не может быть пустым")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Задайте цену")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Задайте категорию")]
        public string Category { get; set; }

        public List<IFormFile> Images { get; set; }
        public List<IFormFile> Images2 { get; set; }
        public List<IFormFile> Images3 { get; set; }
    }
}
