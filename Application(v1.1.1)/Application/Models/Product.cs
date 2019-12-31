using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }


        [StringLength(100, ErrorMessage = "Краткое описание не может быть больше 100 символов.")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Описание объекта не может быть пустым")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Укажите цену")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Укажите категорию")]
        public string Category { get; set; }

        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
    }
}
